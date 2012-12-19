using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using GPRP.Web.UI;
using GPRP.GPRPComponents;
using MyADO;
using GPRP.GPRPControls;
using GPRP.GPRPEnumerations;
using GPRP.GPRPBussiness;
using GPRP.Entity;
using YYControls;
using VBControls.VBProject;
using GOA.UserControl;
namespace GOA
{
    public partial class RequestAdd : BasePage
    {
        private DataTable dtMainField;
        private DataTable dtMainEdit; //用来保存可编辑的主字段
        private ArrayList detailGroups; // 用来保存明细表
        private ArrayList fileUploadGroup; //用来保存文件上传字段的上传的附件信息
        //获取各明细的数据
        private ArrayList dtGroups;    //明细组字段的值
        private ArrayList dtFieldGroups;//明细组的字段信息
        private Int32[] GroupsID;       //明细组编号
        private ArrayList dtFilesUpLoad;    //字段附件
        private Int32[] FilesUpLoadFieldID; //字段附件字段ID

        private int RequestID;
        private int FormID;
        private int NodeID;
        private int WorkflowID;
        private int NodeType;
        private int IsAgent;
        private int DeptLevel;
        private int RightType;//当前用户对此流程实例的操作权限。（可编辑，只读，无权限），创建流程时为2
        private int BeAgentID;

        public RequestAdd()
        {
            detailGroups = new ArrayList();
            fileUploadGroup = new ArrayList();
            dtMainEdit = new DataTable();
            dtMainEdit.Columns.Add("FieldID", typeof(string));
            dtMainEdit.Columns.Add("FieldName", typeof(string));
            dtMainEdit.Columns.Add("FieldHTMLType", typeof(string));
            dtMainEdit.Columns.Add("FieldDataType", typeof(string));
            dtMainEdit.Columns.Add("SqlDbType", typeof(string));
            dtMainEdit.Columns.Add("SqlDbLength", typeof(int));            
            dtMainEdit.Columns.Add("FieldValue", typeof(string));
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            WorkflowID = DNTRequest.GetInt("WorkflowID", 0);
            IsAgent = DNTRequest.GetInt("IsAgent", 0); ;
            string strLoadingScript = " document.all.loadMsg.style.display = '';";
            string strLoadingFinishedScript = " document.all.loadMsg.style.display = 'none';";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PageLoad", strLoadingScript, true);

            //获取可创建流程的NodeID,对应的表单的FormID
            NodeID = DbHelper.GetInstance().GetCreateNodeIDbyWorkflowID(WorkflowID);
            NodeType = 1;
            RightType = 2;
            DeptLevel = 0;

            if (IsAgent == 1)
            {
                //获取代理的创建人员
                DataTable dtBeAgent = DbHelper.GetInstance().GetBeAgentCreatePerson(WorkflowID, userEntity.UserSerialID);
                if (dtBeAgent.Rows.Count <= 0)
                {
                    Response.Write("抱歉，您无权限进入此画面");
                    Response.End();
                }
                else
                {
                    RightType = 2;
                    BeAgentID = Convert.ToInt32(dtBeAgent.Rows[0]["BeAgentPersonID"].ToString());
                }
            }
            FormID = DbHelper.GetInstance().GetWorkflowFromIDbyWorkflowID(WorkflowID);
            InitPageContent(WorkflowID, FormID, NodeID);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loading", strLoadingFinishedScript, true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RequestID = 0;

                Workflow_BaseEntity _Workflow_BaseEntity = DbHelper.GetInstance().GetWorkflow_BaseEntityByKeyCol(WorkflowID.ToString());
                lblRequestName.Text = _Workflow_BaseEntity.WorkflowName + "-" + userEntity.UserName + "-" + DateTime.Today.ToString("yyyy-MM-dd");
                
                //检查节点前的附加动作
                //附加动作Type0
                DataTable dtPreAddInOperateType0 = DbHelper.GetInstance().GetNodeAddInOperateType0(NodeID, 0);
                for (int i = 0; i < dtPreAddInOperateType0.Rows.Count; i++)
                {
                    int AddInOPID = Convert.ToInt32(dtPreAddInOperateType0.Rows[i]["AddInOPID"].ToString());
                    int CaculateType = Convert.ToInt32(dtPreAddInOperateType0.Rows[i]["CaculateType"].ToString());
                    int DataSourceID = Convert.ToInt32(dtPreAddInOperateType0.Rows[i]["DataSourceID"].ToString());
                    string CaculateValue = dtPreAddInOperateType0.Rows[i]["CaculateValue"].ToString();
                    int DataSetID = Convert.ToInt32(dtPreAddInOperateType0.Rows[i]["DataSetID"].ToString());
                    string ValueField = dtPreAddInOperateType0.Rows[i]["ValueField"].ToString();
                    string SPName = dtPreAddInOperateType0.Rows[i]["StorageProcedure"].ToString();
                    string OutParameter = dtPreAddInOperateType0.Rows[i]["OutputParameter"].ToString();
                    string OPCondition = dtPreAddInOperateType0.Rows[i]["OPCondition"].ToString();
                    int TargetFieldID = Convert.ToInt32(dtPreAddInOperateType0.Rows[i]["TargetFieldID"].ToString());
                    string sResult = DbHelper.GetInstance().ProcessAddInOperateType0(AddInOPID, CaculateType, DataSourceID, CaculateValue, DataSetID, ValueField, SPName, OutParameter, OPCondition, RequestID, TargetFieldID);

                    //检查TargetField的表现形式
                    Workflow_FieldDictEntity FieldDict = DbHelper.GetInstance().GetWorkflow_FieldDictEntityByKeyCol(TargetFieldID.ToString());
                    int HTMLType = FieldDict.HTMLTypeID;
                    if (HTMLType == 1) //Label
                        ((Label)placeHolder.FindControl("field" + TargetFieldID)).Text = sResult;
                    else if (HTMLType == 2)//Textbox
                        ((GPRP.GPRPControls.TextBox)placeHolder.FindControl("field" + TargetFieldID)).Text = sResult;
                    else if (HTMLType == 3) //TextArea
                        ((GPRP.GPRPControls.TextBox)placeHolder.FindControl("field" + TargetFieldID)).Text = sResult;
                    else if (HTMLType == 8)
                    {
                        int BrowseType = FieldDict.BrowseType;
                        Workflow_BrowseTypeEntity browseTypeEntity = DbHelper.GetInstance().GetWorkflow_BrowseTypeEntityByKeyCol(BrowseType.ToString());
                        string BrowseSql = browseTypeEntity.BrowseValueSql;
                        string BrowseSqlParam = browseTypeEntity.BrowseSqlParam;
                        string FieldText = DbHelper.GetInstance().GetBrowseFieldText(BrowseSql, BrowseSqlParam, CaculateValue);
                        string TextFieldID = "fieldText" + TargetFieldID.ToString();
                        GPRP.GPRPControls.TextBox txtBrowseField = (GPRP.GPRPControls.TextBox)placeHolder.FindControl(TextFieldID);
                        txtBrowseField.Text = FieldText;

                        string ValueFieldID = "field" + TargetFieldID.ToString();
                        System.Web.UI.WebControls.HiddenField txtBrowseFieldValue = (System.Web.UI.WebControls.HiddenField)placeHolder.FindControl(ValueFieldID);
                        txtBrowseFieldValue.Value = CaculateValue;
                    }
                    //其他类型的

                }
                //附加动作1               
                DataTable dtPreAddInOperateType1 = DbHelper.GetInstance().GetNodeAddInOperateType1(NodeID, 0);
                for (int j = 0; j < dtPreAddInOperateType1.Rows.Count; j++)
                {
                    int AddInOPID = Convert.ToInt32(dtPreAddInOperateType1.Rows[j]["AddInOPID"].ToString());
                    int CaculateType = Convert.ToInt32(dtPreAddInOperateType1.Rows[j]["CaculateType"].ToString());
                    int DataSourceID = Convert.ToInt32(dtPreAddInOperateType1.Rows[j]["DataSourceID"].ToString());
                    string DataTable = dtPreAddInOperateType1.Rows[j]["DataSourceTable"].ToString();
                    int GroupID = Convert.ToInt32(dtPreAddInOperateType1.Rows[j]["GroupID"].ToString());
                    string selectRange = dtPreAddInOperateType1.Rows[j]["selectRange"].ToString();
                    int OPCycleType = Convert.ToInt32(dtPreAddInOperateType1.Rows[j]["OPCycleType"].ToString());
                    string OPCondition = dtPreAddInOperateType1.Rows[j]["OPCondition"].ToString();

                    string sResult = DbHelper.GetInstance().ProcessAddInOperateType1(AddInOPID, CaculateType, DataSourceID, DataTable, GroupID, selectRange, OPCycleType, OPCondition, RequestID);
                    //判断是否执行成功
                }
            }
            else
            {
                if (hidden_RequestID.Value != "")
                    RequestID = Convert.ToInt32(hidden_RequestID.Value);
                for (int j = 0; j < detailGroups.Count; j++)
                {
                    FormDetailGroup detailGroup = (FormDetailGroup)detailGroups[j];
                    detailGroup.CollectSelected();
                    detailGroup.BindGridView();
                }
            }
        }

        /// <summary>
        /// 页面初始话

        /// </summary>
        /// <param name="WorkflowID"></param>
        /// <param name="IsAgent"></param>
        private void InitPageContent(int WorkflowID, int FormID, int NodeID)
        {
            //placeHolder.Controls.Clear();
            //DataTable dtMainField = DbHelper.GetInstance().sp_GetMainFieldInforByNodeIDAndFormID(FormID, NodeID);
            dtMainField = DbHelper.GetInstance().sp_GetMainFieldInforByNodeIDAndFormID(FormID, NodeID);
            //a.FieldID,a.IsView,a.IsEdit,a.IsMandatory,a.BasicValidType,a.ValidTimeType,e.ValidTimeTypeName,b.FieldLabel,c.FieldName,c.FieldDBType,c.HTMLTypeID,c.TextLength,c.TextHeight,c.BrowseType,c.IsDynamic,c.DataSetID,c.valueColumn,c.TextColumn,c.CSSStyle,f.CSSStyleClass,f.LineLength,c.DefaultValue 
            int IsNewLine = 0;
            int LineLength = 0;

            Literal literialStart = new Literal();
            literialStart.ID = "lblMainFieldStart";
            literialStart.Text = "<div class='clear'>";
            placeHolder.Controls.Add(literialStart);

            for (int i = 0; i < dtMainField.Rows.Count; i++)
            {
                string FieldID = dtMainField.Rows[i]["FieldID"].ToString();
                string FieldName = dtMainField.Rows[i]["FieldName"].ToString();
                string FieldLable = dtMainField.Rows[i]["FieldLabel"].ToString();
                string FieldDataType = dtMainField.Rows[i]["FieldDBType"].ToString();
                string SqlDbType = dtMainField.Rows[i]["SqlDbType"].ToString();
                int SqlDbLength = Convert.ToInt32(dtMainField.Rows[i]["SqlDbLength"]);
                int FieldCCSStyle = Convert.ToInt32(dtMainField.Rows[i]["CSSStyle"].ToString());
                string FieldCSSStyleClass = dtMainField.Rows[i]["CSSStyleClass"].ToString();
                int FieldLineLength = Convert.ToInt32(dtMainField.Rows[i]["LineLength"].ToString());
                string FieldDefaultValue = dtMainField.Rows[i]["DefaultValue"].ToString();
                int FieldHTMLTypeID = Convert.ToInt32(dtMainField.Rows[i]["HTMLTypeID"].ToString());
                int FieldTextLength = Convert.ToInt32(dtMainField.Rows[i]["TextLength"].ToString());
                int FieldTextHeight = Convert.ToInt32(dtMainField.Rows[i]["TextHeight"].ToString());
                int FieldValidType = Convert.ToInt32(dtMainField.Rows[i]["BasicValidType"].ToString());
                string ValidTimeTypeName = dtMainField.Rows[i]["ValidTimeTypeName"].ToString();
                int IsView= Convert.ToInt32(dtMainField.Rows[i]["IsView"].ToString());
                int IsEdit = Convert.ToInt32(dtMainField.Rows[i]["IsEdit"].ToString());
                int IsDynamic = Convert.ToInt32(dtMainField.Rows[i]["IsDynamic"].ToString());
                int DataSetID = Convert.ToInt32(dtMainField.Rows[i]["DataSetID"].ToString());
                string ValueColumn = dtMainField.Rows[i]["valueColumn"].ToString();
                string TextColumn = dtMainField.Rows[i]["TextColumn"].ToString();
                int IsMandatory = Convert.ToInt32(dtMainField.Rows[i]["IsMandatory"].ToString());
                int FieldDataTypeID = Convert.ToInt32(dtMainField.Rows[i]["DataTypeID"].ToString());
                string FieldDateFormat = dtMainField.Rows[i]["Dateformat"].ToString();
                if (IsView == 1)
                {
                    Literal lblFieldDesc = new Literal();
                    lblFieldDesc.ID = "lblField" + FieldID + "Desc";
                    LineLength = LineLength + FieldLineLength;
                    if (LineLength > 3)
                    {
                        IsNewLine = 1;
                        LineLength = FieldLineLength;
                    }
                    else
                    {
                        IsNewLine = 0;
                    }
                    if (IsNewLine == 1)
                    {
                        lblFieldDesc.Text = "</div><div class='clear'><div class=" + FieldCSSStyleClass + "><label class='char5'>" + FieldLable + "</label><div class='iptblk'>";
                    }
                    else
                    {
                        lblFieldDesc.Text = "<div class=" + FieldCSSStyleClass + "><label class='char5'>" + FieldLable + "</label><div class='iptblk'>";
                    }
                    placeHolder.Controls.Add(lblFieldDesc);

                    switch (FieldHTMLTypeID)
                    {
                        case 1:     //Label
                            Label lblField = new Label();
                            lblField.ID = "field" + FieldID;
                            lblField.Text = FieldDefaultValue;
                            placeHolder.Controls.Add(lblField);
                            break;
                        case 2:     //Textbox
                            GPRP.GPRPControls.TextBox txtField = new GPRP.GPRPControls.TextBox();
                            txtField.ID = "field" + FieldID;
                            txtField.Text = FieldDefaultValue;
                            txtField.Width = new Unit(120);
                            if (FieldValidType > 0)   //有数据验证
                            {
                                txtField.Attributes.Add(ValidTimeTypeName, "javascript:FormFieldValidate('" + FieldID + "','" + FieldHTMLTypeID.ToString() + "','" + FieldValidType.ToString() + "')");
                            }
                            if (IsEdit == 0)
                            {
                                txtField.Enabled = false;
                            }
                            else
                            {
                                if (IsMandatory == 1)
                                {
                                    txtField.CanBeNull = "必填";
                                }
                                DataRow row = dtMainEdit.NewRow();
                                row["FieldID"] = FieldID;
                                row["FieldName"] = FieldName;
                                row["FieldDataType"] = FieldDataType;
                                row["SqlDbType"] = SqlDbType;
                                row["SqlDbLength"] = SqlDbLength;
                                row["FieldHTMLType"] = FieldHTMLTypeID;
                                dtMainEdit.Rows.Add(row);
                            }
                            placeHolder.Controls.Add(txtField);
                            if (IsEdit == 1)
                            {
                                //如果是时间或日期
                                if (FieldDataTypeID == 4 || FieldDataTypeID == 5)
                                {
                                    //txtField.AddAttributes("readonly", "true");

                                    Literal literialImage = new Literal();
                                    literialImage.Text = "<img onclick=\"WdatePicker({el:$dp.$('" + txtField.ID + "'),dateFmt:'" + FieldDateFormat + "'})\" src=\"../images/calendar.gif\">";
                                    literialImage.ID = "fieldImage" + FieldID;
                                    //ImageButton imageButton = new ImageButton();
                                    //imageButton.ID = "fieldImage" + FieldID;
                                    //imageButton.ImageAlign = ImageAlign.Middle;
                                    //imageButton.ToolTip = "搜索";
                                    //imageButton.ImageUrl = "../images/calendar.gif";
                                    ////imageButton.OnClientClick = "return btnBrowseFieldClick('" + txtBrowseField.ID + "','" + txtBrowseFieldValue.ID + "','" + BrowsePage + "','" + BrowseTypeName + "');";
                                    ////imageButton.Attributes.Add("onclik", "javascript:WdatePicker({dateFmt:'" + FieldDateFormat + "'})");
                                    //imageButton.OnClientClick = "return WdatePicker({el:$dp.$('" + txtField .ID+ "'),dateFmt:'" + FieldDateFormat + "'})";
                                    placeHolder.Controls.Add(literialImage);
                                    txtField.AddAttributes("onfocus", "javascript:WdatePicker({dateFmt:'" + FieldDateFormat + "'})");
                                }
                            }

                            break;
                        case 3:   //TextArea
                            GPRP.GPRPControls.TextBox txtAreaField = new GPRP.GPRPControls.TextBox();
                            txtAreaField.ID = "field" + FieldID;
                            txtAreaField.TextMode = TextBoxMode.MultiLine;
                            txtAreaField.Width = new Unit(FieldTextLength == 0 ? 300 : FieldTextLength);
                            txtAreaField.Rows = FieldTextHeight;
                            txtAreaField.Text = FieldDefaultValue;
                            if (FieldValidType > 0)   //有数据验证
                            {
                                txtAreaField.Attributes.Add(ValidTimeTypeName, "javascript:FormFieldValidate('" + FieldID + "','" + FieldHTMLTypeID.ToString() + "','" + FieldValidType.ToString() + "')");
                            }
                            if (IsEdit == 0)
                            {
                                txtAreaField.Enabled = false;
                            }
                            else
                            {
                                if (IsMandatory == 1)
                                {
                                    txtAreaField.CanBeNull = "必填";
                                }
                                DataRow row = dtMainEdit.NewRow();
                                row["FieldID"] = FieldID;
                                row["FieldName"] = FieldName;
                                row["FieldDataType"] = FieldDataType;
                                row["SqlDbType"] = SqlDbType;
                                row["SqlDbLength"] = SqlDbLength;
                                row["FieldHTMLType"] = FieldHTMLTypeID;
                                dtMainEdit.Rows.Add(row);
                            }

                            placeHolder.Controls.Add(txtAreaField);
                            break;
                        case 4:  //checkboxList
                            GPRP.GPRPControls.CheckBoxList chkLstField = new GPRP.GPRPControls.CheckBoxList();
                            //chkLstField.CssClass = "buttonlist";
                            chkLstField.ID = "field" + FieldID;
                            chkLstField.RepeatColumns = 1;
                            chkLstField.RepeatDirection = RepeatDirection.Vertical;

                            //获得数据源

                            DataTable dtList = DbHelper.GetInstance().GetMultiSelectDataSource(Convert.ToInt32(FieldID), IsDynamic, DataSetID, ValueColumn, TextColumn);
                            chkLstField.AddTableData(dtList);
                            chkLstField.DataTextField = TextColumn;
                            chkLstField.DataValueField = ValueColumn;

                            //有数据验证

                            if (FieldValidType > 0)
                            {
                                chkLstField.Attributes.Add(ValidTimeTypeName, "javascript:FormFieldValidate('" + FieldID + "','" + FieldHTMLTypeID.ToString() + "','" + FieldValidType.ToString() + "')");
                            }
                            if (IsEdit == 0)
                            {
                                chkLstField.Enabled = false;
                            }
                            else
                            {
                                DataRow row = dtMainEdit.NewRow();
                                row["FieldID"] = FieldID;
                                row["FieldName"] = FieldName;
                                row["FieldDataType"] = FieldDataType;
                                row["SqlDbType"] = SqlDbType;
                                row["SqlDbLength"] = SqlDbLength;
                                row["FieldHTMLType"] = FieldHTMLTypeID;
                                dtMainEdit.Rows.Add(row);
                            }
                            placeHolder.Controls.Add(chkLstField);
                            break;
                        case 5: // dropdownlist
                            GPRP.GPRPControls.DropDownList ddlField = new GPRP.GPRPControls.DropDownList();
                            ddlField.ID = "field" + FieldID;
                            //ddlField.RepeatColumns = 1;
                            //ddlField.RepeatDirection = RepeatDirection.Vertical;
                            //chkLstField.AddTableData()
                            ddlField.Width = new Unit(120);
                            DataTable dtDrop = DbHelper.GetInstance().GetMultiSelectDataSource(Convert.ToInt32(FieldID), IsDynamic, DataSetID, ValueColumn, TextColumn);
                            ddlField.AddTableData(dtDrop);
                            ddlField.DataTextField = TextColumn;
                            ddlField.DataValueField = ValueColumn;

                            ddlField.SelectedValue = FieldDefaultValue;
                            //有数据验证

                            if (FieldValidType > 0)
                            {
                                ddlField.Attributes.Add(ValidTimeTypeName, "javascript:FormFieldValidate('" + FieldID + "','" + FieldHTMLTypeID.ToString() + "','" + FieldValidType.ToString() + "')");
                            }
                            if (IsEdit == 0)
                            {
                                ddlField.Enabled = false;
                            }
                            else
                            {
                                DataRow row = dtMainEdit.NewRow();
                                row["FieldID"] = FieldID;
                                row["FieldName"] = FieldName;
                                row["FieldDataType"] = FieldDataType;
                                row["SqlDbType"] = SqlDbType;
                                row["SqlDbLength"] = SqlDbLength;
                                row["FieldHTMLType"] = FieldHTMLTypeID;
                                dtMainEdit.Rows.Add(row);
                            }
                            placeHolder.Controls.Add(ddlField);
                            break;
                        case 6: //checkbox
                            System.Web.UI.WebControls.CheckBox chkField = new System.Web.UI.WebControls.CheckBox();
                            chkField.ID = "field" + FieldID;
                            //ddlField.RepeatColumns = 1;
                            //ddlField.RepeatDirection = RepeatDirection.Vertical;
                            //chkLstField.AddTableData()
                            if (FieldDefaultValue.ToUpper() == "TRUE" || FieldDefaultValue == "1")
                                chkField.Checked = true;
                            if (FieldValidType > 0)   //有数据验证
                            {
                                chkField.Attributes.Add(ValidTimeTypeName, "javascript:FormFieldValidate('" + FieldID + "','" + FieldHTMLTypeID.ToString() + "','" + FieldValidType.ToString() + "')");
                            }
                            if (IsEdit == 0)
                            {
                                chkField.Enabled = false;
                            }
                            else
                            {
                                DataRow row = dtMainEdit.NewRow();
                                row["FieldID"] = FieldID;
                                row["FieldName"] = FieldName;
                                row["FieldDataType"] = FieldDataType;
                                row["SqlDbType"] = SqlDbType;
                                row["SqlDbLength"] = SqlDbLength;
                                row["FieldHTMLType"] = FieldHTMLTypeID;
                                dtMainEdit.Rows.Add(row);
                            }
                            placeHolder.Controls.Add(chkField);
                            break;
                        case 7:   //uploadFile
                           
                            FileUploadControl fileUpLoad = (FileUploadControl)(Page.LoadControl("UserControl/FileUploadControl.ascx"));
                            fileUpLoad.WorkflowID = WorkflowID;
                            fileUpLoad.RequestID = RequestID;
                            fileUpLoad.Uploader = userEntity.UserSerialID;
                            fileUpLoad.FieldID = Convert.ToInt32(FieldID);
                            fileUpLoad.IsEdit = IsEdit;
                            fileUpLoad.RightType = RightType;
                            if (IsEdit == 1)
                            {
                                fileUploadGroup.Add(fileUpLoad);
                            }
                            placeHolder.Controls.Add(fileUpLoad);
                            
                            break;
                        case 8:  //浏览形式按钮
                            string BrowsePage = dtMainField.Rows[i]["BrowsePage"].ToString();
                            string BrowseTypeName = dtMainField.Rows[i]["BrowseTypeName"].ToString();
                            GPRP.GPRPControls.TextBox txtBrowseField = new GPRP.GPRPControls.TextBox();
                            txtBrowseField.ID = "fieldText" + FieldID;
                            txtBrowseField.Width = new Unit(120);
                            System.Web.UI.WebControls.HiddenField txtBrowseFieldValue = new System.Web.UI.WebControls.HiddenField();
                            txtBrowseFieldValue.ID = "field" + FieldID;

                            //有数据验证

                            if (FieldValidType > 0)
                            {
                                txtBrowseField.Attributes.Add(ValidTimeTypeName, "javascript:FormFieldValidate('" + FieldID + "','" + FieldHTMLTypeID.ToString() + "','" + FieldValidType.ToString() + "')");
                            }
                            txtBrowseField.AddAttributes("readonly", "true");
                            placeHolder.Controls.Add(txtBrowseField);
                            if (IsEdit == 1)
                            {
                                ImageButton imageButton = new ImageButton();
                                imageButton.ID = "fieldImage" + FieldID;
                                imageButton.ImageAlign = ImageAlign.Middle;
                                imageButton.ToolTip = "搜索";
                                imageButton.ImageUrl = "../images/arrow_black.gif";
                                imageButton.OnClientClick = "return btnBrowseFieldClick('" + txtBrowseField.ID + "','" + txtBrowseFieldValue.ID + "','" + BrowsePage + "','" + BrowseTypeName + "');";
                                placeHolder.Controls.Add(imageButton);

                                if (IsMandatory == 1)
                                {
                                    txtBrowseField.CanBeNull = "必填";
                                }
                                placeHolder.Controls.Add(txtBrowseFieldValue);
                                DataRow row = dtMainEdit.NewRow();
                                row["FieldID"] = FieldID;
                                row["FieldName"] = FieldName;
                                row["FieldDataType"] = FieldDataType;
                                row["SqlDbType"] = SqlDbType;
                                row["SqlDbLength"] = SqlDbLength;
                                row["FieldHTMLType"] = FieldHTMLTypeID;
                                dtMainEdit.Rows.Add(row);
                            }
                            else
                            {
                                txtBrowseField.Enabled = false;
                            }

                            break;
                        case 9:  //  GroupLine
                            if (IsEdit == 1)  //可编辑时才用
                            {
                                string GroupLineDataSetID = dtMainField.Rows[i]["GroupLineDataSetID"].ToString();
                                string TargetGroupID = dtMainField.Rows[i]["TargetGroupID"].ToString();
                                ImageButton imageButton = new ImageButton();
                                imageButton.ID = "field" + FieldID;
                                imageButton.ImageAlign = ImageAlign.Middle;
                                imageButton.ToolTip = "搜索";
                                imageButton.ImageUrl = "../images/BacoBrowser.gif";
                                imageButton.OnClientClick = "return btnGroupLineFieldClick('" + FieldID + "','" + GroupLineDataSetID + "','" + TargetGroupID + "');";
                                imageButton.Click += new ImageClickEventHandler(imageGroupLineButton_Click);

                                placeHolder.Controls.Add(imageButton);
                            }

                            break;

                    }

                    if (IsEdit == 1 && IsMandatory == 1)
                    {
                        Label literialMandatory = new Label();
                        literialMandatory.ID = "lblFieldMandatory" + FieldID;
                        literialMandatory.Text = "*";
                        literialMandatory.ForeColor = System.Drawing.Color.Red;
                        placeHolder.Controls.Add(literialMandatory);

                    }

                    Literal literial = new Literal();
                    literial.ID = "lblField" + FieldID + "End";
                    literial.Text = "</div></div>";
                    placeHolder.Controls.Add(literial);
                }
            }
            Literal literialEnd = new Literal();
            literialEnd.ID = "lblMainFieldEnd";
            literialEnd.Text = "</div>";
            placeHolder.Controls.Add(literialEnd);

            //下面是明细字段


            //抓明细字段组
            DataTable dtDetailGroup = DbHelper.GetInstance().GetDetailFieldGroup(FormID, NodeID);
            if (dtDetailGroup.Rows.Count > 0)
            {
                //先添加tabcontainer. 
                AjaxControlToolkit.TabContainer tabContainer = new AjaxControlToolkit.TabContainer();
                tabContainer.ID = "tabContainer";
                for (int j = 0; j < dtDetailGroup.Rows.Count; j++)
                {
                    //添加tab ,tab中添加FormDetailGroup
                    //a.GroupID,a.IsView,a.IsAdd,a.IsEdit,a.IsDelete,b.GroupName
                    int GroupID = Convert.ToInt32(dtDetailGroup.Rows[j]["GroupID"].ToString());
                    int IsView = Convert.ToInt32(dtDetailGroup.Rows[j]["IsView"].ToString());
                    int IsEdit = Convert.ToInt32(dtDetailGroup.Rows[j]["IsEdit"].ToString());
                    int IsDelete = Convert.ToInt32(dtDetailGroup.Rows[j]["IsDelete"].ToString());
                    int IsAdd = Convert.ToInt32(dtDetailGroup.Rows[j]["IsAdd"].ToString());
                    string GroupName = dtDetailGroup.Rows[j]["GroupName"].ToString();
                    AjaxControlToolkit.TabPanel tablePanel = new AjaxControlToolkit.TabPanel();
                    tablePanel.HeaderText = GroupName;
                    tablePanel.ID = "TP" + GroupID.ToString();
                    //tablePanel.Height = new Unit(400);
                    FormDetailGroup detailGroup = (FormDetailGroup)(Page.LoadControl("UserControl/FormDetailGroup.ascx"));
                    detailGroup.FormID = FormID;
                    detailGroup.GroupID = GroupID;
                    detailGroup.WorkflowID = WorkflowID;
                    detailGroup.RequestID = RequestID;
                    detailGroup.IsGroupView = IsView;
                    detailGroup.IsGroupEdit = IsEdit;
                    detailGroup.IsGroupAdd = IsAdd;
                    detailGroup.IsGroupDelete = IsDelete;
                    detailGroup.NodeID = NodeID;
                    detailGroup.dtField = DbHelper.GetInstance().GetDetailFieldInfor(NodeID, GroupID);
                    detailGroup.RightType = RightType;

                    Panel panel = new Panel();

                    panel.Height = new Unit(100);
                    panel.ID = tablePanel.ID + "panel";
                    panel.Controls.Add(detailGroup);

                    tablePanel.Controls.Add(panel);

                    tabContainer.Tabs.Add(tablePanel);
                    if (IsEdit == 1 || IsAdd == 1 || IsDelete == 1)
                    {
                        detailGroups.Add(detailGroup);
                    }
                }
                tabContainer.ActiveTabIndex = 0;
                placeHolderDetail.Controls.Add(tabContainer);
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveRequestData(object sender, EventArgs e)
        {
            try
            {
                int sResult = saveFormData();
                hidden_RequestID.Value = sResult.ToString();
                this.RequestID = sResult;
                Response.Redirect(string.Format("RequestProcess.aspx?loginType={0}&RequestID={1}&IsAgent={2}", DNTRequest.GetInt("loginType", 1), RequestID, IsAgent));
            }
            catch (Exception exp)
            {
                string errMsg = exp.Message.Replace("\r\n", "\\r\\n");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DisplayError", "alert(\"" + errMsg + "\");", true);
            }
        }

        /// <summary>
        /// 提交到下一个节点处理

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SubmitRequestData(object sender, EventArgs e)
        {
            try
            {
                int sResult = saveFormData();
                hidden_RequestID.Value = sResult.ToString();
                this.RequestID = sResult;
                string OperateComment = txtOperateComment.Text;
                //提交到下一个节点
                string ALL_username = "";

                DbHelper.GetInstance().PromoteRequestToNextNode(WorkflowID, RequestID, NodeID, userEntity.UserSerialID, 1, OperateComment, Utils.GetIP(), IsAgent, DeptLevel);
                Workflow_RequestBaseEntity _RequestBaseEntity = DbHelper.GetInstance().GetWorkflow_RequestBaseEntityByKeyCol(RequestID.ToString());
                if (_RequestBaseEntity.CurrentNodeID != NodeID || "25,45".Contains(_RequestBaseEntity.CurrentRuleType))
                {
                    string MailFrom = config.MailFrom;
                    string SmtpServer = config.SmtpServer;
                    string NextOperatorList = _RequestBaseEntity.CurrentOperatorID;
                    DataTable dtAgentDetail = DbHelper.GetInstance().GetDBRecords("AgentID", "Workflow_RequestAgentDetail", string.Format("RequestID={0} and NodeID={1}", RequestID, _RequestBaseEntity.CurrentNodeID), "");
                    for (int i = 0; i < dtAgentDetail.Rows.Count; i++)
                    {
                        string AgentID = dtAgentDetail.Rows[i]["AgentID"].ToString();

                        string usernameList = DbHelper.GetInstance().ExecSqlResult("select UserID from UserList where UserSerialID="+AgentID);
                        ALL_username += usernameList + ",";

                        if (!("," + NextOperatorList + ",").Contains("," + AgentID + ","))
                        {
                            NextOperatorList += "," + AgentID;
                        }
                    }
                    
                    string u= DbHelper.GetInstance().ExecSqlResult("select UserID from UserList where UserSerialID=" + NextOperatorList);
                    ALL_username += u + ",";
                    NextOperatorList = NextOperatorList.Trim(new char[] { ',' });
                    //下一步人员名单
                
                    if (ALL_username != "")
                    {
                        ALL_username = ALL_username.Substring(0, ALL_username.Length - 1);
                    }

                    DataTable dtMailto = DbHelper.GetInstance().GetDBRecords("DISTINCT UserID,UserName,UserCode,UserEmail", "UserList", string.Format("UserSerialID in ({0})", NextOperatorList), "");
                    string[] Mailto = new string[dtMailto.Rows.Count];
                    for (int i = 0; i < dtMailto.Rows.Count; i++)
                    {
                        Mailto[i] = dtMailto.Rows[i]["UserEmail"].ToString();
                    }
                    string MailSubject = string.Format("[WF]您有新的工作流需要处理({0})", _RequestBaseEntity.RequestName);
                    string Parameters = Utils.UrlEncode(string.Format("RequestProcess.aspx?loginType=2&RequestID={0}&IsAgent={1}", RequestID, IsAgent));
                    string MailBody = string.Format("您有新的工作流需要处理(<a href=\"{1}/login.aspx?gopage={2}\">{0}</a>)", _RequestBaseEntity.RequestName, DNTRequest.GetCurrentFullHost(), Parameters);
                    SendEmail.mailSend(SmtpServer, false, MailFrom, Mailto, MailSubject, MailBody);
                }

                //检查节点后的附加动作


                //附加动作0
                DataTable dtPreAddInOperateType0 = DbHelper.GetInstance().GetNodeAddInOperateType0(NodeID, 1);
                for (int i = 0; i < dtPreAddInOperateType0.Rows.Count; i++)
                {
                    int AddInOPID = Convert.ToInt32(dtPreAddInOperateType0.Rows[i]["AddInOPID"].ToString());
                    int CaculateType = Convert.ToInt32(dtPreAddInOperateType0.Rows[i]["CaculateType"].ToString());
                    int DataSourceID = Convert.ToInt32(dtPreAddInOperateType0.Rows[i]["DataSourceID"].ToString());
                    string CaculateValue = dtPreAddInOperateType0.Rows[i]["CaculateValue"].ToString();
                    int DataSetID = Convert.ToInt32(dtPreAddInOperateType0.Rows[i]["DataSetID"].ToString());
                    string ValueField = dtPreAddInOperateType0.Rows[i]["ValueField"].ToString();
                    string SPName = dtPreAddInOperateType0.Rows[i]["StorageProcedure"].ToString();
                    string OutParameter = dtPreAddInOperateType0.Rows[i]["OutputParameter"].ToString();
                    string OPCondition = dtPreAddInOperateType0.Rows[i]["OPCondition"].ToString();
                    int TargetFieldID = Convert.ToInt32(dtPreAddInOperateType0.Rows[i]["TargetFieldID"].ToString());
                    string sResultField = DbHelper.GetInstance().ProcessAddInOperateType0(AddInOPID, CaculateType, DataSourceID, CaculateValue, DataSetID, ValueField, SPName, OutParameter, OPCondition, RequestID, TargetFieldID);

                    //更新表单的TargetField的数据
                    Workflow_FieldDictEntity FieldDict = DbHelper.GetInstance().GetWorkflow_FieldDictEntityByKeyCol(TargetFieldID.ToString());
                    string FieldName = FieldDict.FieldName;
                    string FieldDBType = FieldDict.FieldDBType;

                    DbHelper.GetInstance().UpdateFormFieldValue(RequestID, FieldName, FieldDBType, sResultField);
                }

                //附加动作1
                DataTable dtAddInOperateType1 = DbHelper.GetInstance().GetNodeAddInOperateType1(NodeID, 1);
                for (int j = 0; j < dtAddInOperateType1.Rows.Count; j++)
                {
                    int AddInOPID = Convert.ToInt32(dtAddInOperateType1.Rows[j]["AddInOPID"].ToString());
                    int CaculateType = Convert.ToInt32(dtAddInOperateType1.Rows[j]["CaculateType"].ToString());
                    int DataSourceID = Convert.ToInt32(dtAddInOperateType1.Rows[j]["DataSourceID"].ToString());
                    string DataTable = dtAddInOperateType1.Rows[j]["DataSourceTable"].ToString();
                    int GroupID = Convert.ToInt32(dtAddInOperateType1.Rows[j]["GroupID"].ToString());
                    string selectRange = dtAddInOperateType1.Rows[j]["selectRange"].ToString();
                    int OPCycleType = Convert.ToInt32(dtAddInOperateType1.Rows[j]["OPCycleType"].ToString());
                    string OPCondition = dtAddInOperateType1.Rows[j]["OPCondition"].ToString();

                    string sAddInOperateResult = DbHelper.GetInstance().ProcessAddInOperateType1(AddInOPID, CaculateType, DataSourceID, DataTable, GroupID, selectRange, OPCycleType, OPCondition, RequestID);

                    //判断是否执行成功
                }

                //触发流程的动作
                DataTable dtTriggerWF = DbHelper.GetInstance().GetNodeTriggerWF(NodeID, 1);
                for (int ii = 0; ii < dtTriggerWF.Rows.Count; ii++)
                {
                    int TriggerID = Convert.ToInt32(dtTriggerWF.Rows[ii]["TriggerID"].ToString());
                    string OPCondition = dtTriggerWF.Rows[ii]["OPCondition"].ToString();
                    int TriggerWFID = Convert.ToInt32(dtTriggerWF.Rows[ii]["TriggerWFID"].ToString());
                    int TriggerWFCreator = Convert.ToInt32(dtTriggerWF.Rows[ii]["TriggerWFCreator"].ToString());
                    int WFCreateNode = Convert.ToInt32(dtTriggerWF.Rows[ii]["WFCreateNode"].ToString());
                    int WFCreateFieldID = Convert.ToInt32(dtTriggerWF.Rows[ii]["WFCreateFieldID"].ToString());

                    string sTriggerWFResult = DbHelper.GetInstance().ProcessTriggerWF(TriggerID, OPCondition, TriggerWFID, TriggerWFCreator, WFCreateNode, WFCreateFieldID, RequestID);
                }

                string content = "您有一项待办事宜，请尽快处理！";
                myAsynResult asyncResult = null;
                string[] un = ALL_username != null ? ALL_username.Split(',') : null;
                Messages.Instance().AddMessage(content, asyncResult, un);
                //发送待办事宜
                Response.Redirect(string.Format("RequestProcess.aspx?loginType={0}&RequestID={1}&IsAgent={2}&username={3}", DNTRequest.GetInt("loginType", 1), RequestID, IsAgent,ALL_username));
            }
            catch (Exception exp)
            {
                string errMsg = exp.Message.Replace("\r\n", "\\r\\n");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DisplayError", "alert(\"" + errMsg + "\");", true);
            }
        }

        private int saveFormData()
        {
            //获取表单数据
            GetFormFieldValue();
            string RequestName = lblRequestName.Text;
            //再保存数据

            if (RequestID == 0)
            {
                return DbHelper.GetInstance().saveFormData(dtMainEdit, dtGroups, GroupsID, dtFieldGroups, WorkflowID, NodeID, NodeType, userEntity.UserSerialID, Utils.GetIP(), FormID, dtFilesUpLoad, FilesUpLoadFieldID, RequestName, IsAgent, BeAgentID);
            }
            else
            {
                DbHelper.GetInstance().updateFormData(RequestID, dtMainEdit, dtGroups, GroupsID, dtFieldGroups, dtFilesUpLoad, FilesUpLoadFieldID, RequestName);
                return RequestID;
            }
        }

        private void GetFormFieldValue()
        {
            //先抓取主字段的相关值

            for (int i = 0; i < dtMainEdit.Rows.Count; i++)
            {
                string FieldID = "field" + dtMainEdit.Rows[i]["FieldID"].ToString();
                int FieldHTMLType = Convert.ToInt32(dtMainEdit.Rows[i]["FieldHTMLType"].ToString());
                string FieldValue = "";
                switch (FieldHTMLType)
                {
                    case 1:     //Label
                        System.Web.UI.WebControls.Label lblField = (System.Web.UI.WebControls.Label)placeHolder.FindControl(FieldID);
                        FieldValue = lblField.Text;
                        break;
                    case 2:     //Textbox
                        GPRP.GPRPControls.TextBox txtField = (GPRP.GPRPControls.TextBox)placeHolder.FindControl(FieldID);
                        FieldValue = txtField.Text;
                        break;
                    case 3:   //TextArea
                        GPRP.GPRPControls.TextBox txtAreaField = (GPRP.GPRPControls.TextBox)placeHolder.FindControl(FieldID);
                        FieldValue = txtAreaField.Text;
                        break;
                    case 4: //checkboxList
                        GPRP.GPRPControls.CheckBoxList chkLstField = (GPRP.GPRPControls.CheckBoxList)placeHolder.FindControl(FieldID);
                        for (int j = 0; j < chkLstField.Items.Count; j++)
                        {
                            if (chkLstField.Items[j].Selected)
                            {
                                FieldValue += chkLstField.Items[j].Value + ",";
                            }
                        }
                        FieldValue = FieldValue.Trim(new char[] { ',' });
                        break;
                    case 5: // dropdownlist
                        GPRP.GPRPControls.DropDownList ddlField = (GPRP.GPRPControls.DropDownList)placeHolder.FindControl(FieldID);
                        FieldValue = ddlField.SelectedValue;
                        break;
                    case 6: //checkbox
                        System.Web.UI.WebControls.CheckBox chkField = (System.Web.UI.WebControls.CheckBox)placeHolder.FindControl(FieldID);
                        if (chkField.Checked)
                        {
                            FieldValue = "1";
                        }
                        else
                        {
                            FieldValue = "0";
                        }
                        break;
                    case 7:   //uploadFile
                        //System.Web.UI.WebControls.CheckBox upField = new System.Web.UI.WebControls.CheckBox();

                        break;
                    case 8:
                        HiddenField hiddenField = (System.Web.UI.WebControls.HiddenField)placeHolder.FindControl(FieldID);
                        FieldValue = hiddenField.Value;
                        break;
                }
                dtMainEdit.Rows[i]["FieldValue"] = FieldValue;
            }
            //获取各明细的数据
            dtGroups = new ArrayList();
            dtFieldGroups = new ArrayList();
            GroupsID = new Int32[detailGroups.Count];
            for (int j = 0; j < detailGroups.Count; j++)
            {
                FormDetailGroup detailGroup = (FormDetailGroup)detailGroups[j];
                //detailGroup.CollectSelected();
                dtGroups.Add(detailGroup.dtValue);
                GroupsID[j] = detailGroup.GroupID;
                dtFieldGroups.Add(detailGroup.dtField);
            }

            //获取各上传附件的字段的信息
            dtFilesUpLoad = new ArrayList();
            FilesUpLoadFieldID = new Int32[fileUploadGroup.Count];
            for (int jj = 0; jj < fileUploadGroup.Count; jj++)
            {
                FileUploadControl uploadControl = (FileUploadControl)fileUploadGroup[jj];
                //uploadControl.GetFilesValue();
                dtFilesUpLoad.Add(uploadControl.dtAttach);
                FilesUpLoadFieldID[jj] = uploadControl.FieldID;
            }
        }


        protected void imageGroupLineButton_Click(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            string btnID = btn.ID;
            string FieldID = btnID.Replace("field", "");

            DataRow[] rows = dtMainField.Select("FieldID='" + FieldID + "'");
            if (rows.Length > 0)
            {
                string TargetGroupID = rows[0]["TargetGroupID"].ToString();
                //获取目标明细字段的与选择来的明细组的对应关系
                DataTable dtMapping = DbHelper.GetInstance().GetGroupLineFieldMap(FormID, Convert.ToInt32(FieldID));
                DataTable dtSelect = (DataTable)Session[FieldID + "GroupLine"];
                Session.Remove(FieldID + "GroupLine");
                //获取Group
                for (int j = 0; j < detailGroups.Count; j++)
                {
                    FormDetailGroup detailGroup = (FormDetailGroup)detailGroups[j];
                    if (detailGroup.GroupID == Convert.ToInt32(TargetGroupID))
                    {
                        //detailGroup.dtValue.Rows.Clear();
                        for (int i = 0; i < dtSelect.Rows.Count; i++)
                        {
                            DataRow row = detailGroup.dtValue.NewRow();
                            for (int ii = 0; ii < dtMapping.Rows.Count; ii++)
                            {
                                row[dtMapping.Rows[ii]["TargetGroupField"].ToString()] = dtSelect.Rows[i][dtMapping.Rows[ii]["DataSetColumn"].ToString()];
                            }
                            detailGroup.dtValue.Rows.Add(row);
                        }
                        detailGroup.BindGridView();
                        break;
                    }
                }
            }
        }
    }
}
