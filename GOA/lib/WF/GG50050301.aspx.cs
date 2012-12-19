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
using GPRP.GPRPControls;
using GPRP.GPRPEnumerations;
using GPRP.GPRPBussiness;
using GPRP.Entity;
using YYControls;
using VBControls.VBProject;
using MyADO;

namespace GOA
    {
    public partial class GG50050301 : BasePage
        {
        protected void Page_Init(object sender, EventArgs e)
            {
            DataTable dtComputeOperation = DbHelper.GetInstance().GetDBRecords("a.ComputeID,a.ComputeSymbol", "Workflow_ComputeSymbol a,Workflow_ComputeType b", "a.ComputeTypeID=b.ComputeTypeID and b.Useflag=0 and a.Useflag=1", "a.ComputeTypeID,a.DisplayOrder");
            for (int i = 0; i < dtComputeOperation.Rows.Count; i++)
                {
                System.Web.UI.WebControls.Button btn = new System.Web.UI.WebControls.Button();
                btn.ID = "ComputeOperation" + dtComputeOperation.Rows[i]["ComputeID"].ToString();
                btn.Text = dtComputeOperation.Rows[i]["ComputeSymbol"].ToString();
                btn.Width = 30;
                btn.Click += new EventHandler(this.btnComputeOperation_Click);
                phOperationButtonList.Controls.Add(btn);
                }
            }

        protected void Page_Load(object sender, EventArgs e)
            {
            if (!Page.IsPostBack)
                {
                if (DNTRequest.GetString("MappingID") != "")
                    {
                    Workflow_TriggerWFFieldMappingMainEntity _TFM = new Workflow_TriggerWFFieldMappingMainEntity();
                    _TFM = DbHelper.GetInstance().GetWorkflow_TriggerWFFieldMappingMainEntityByKeyCol(DNTRequest.GetString("MappingID"));
                  
                    txtTriggerID.Value = _TFM.TriggerID.ToString();
                    ddlOPCycleType.SelectedValue = _TFM.OPCycleType.ToString();
                    //TargetFieldType:主字段，明细字段组
                    string tableName = @"(
select 0 GroupID,GroupName='主字段'
union
select distinct f.GroupID,GroupName=g.GroupName 
from Workflow_NodeTriggerWorkflow t
inner join Workflow_Base b on t.TriggerWFID=b.WorkflowID
inner join Workflow_FormField f on b.FormID=f.FormID 
inner join Workflow_FormFieldGroup g on f.GroupID=g.GroupID
where t.IsCancel=0 and t.TriggerID=" + _TFM.TriggerID + ")T";
                    rblGroupTo.DataSource = DbHelper.GetInstance().GetDBRecords("GroupID,GroupName", tableName, "1=1", "GroupID");
                    rblGroupTo.DataValueField = "GroupID";
                    rblGroupTo.DataTextField = "GroupName";
                    rblGroupTo.DataBind();
                    rblGroupTo.SelectedValue = _TFM.TargetGroupID.ToString() ;

                    string tblName1 = @"Workflow_NodeTriggerWorkflow t
inner join Workflow_Base b on t.TriggerWFID=b.WorkflowID
inner join Workflow_FormField f on b.FormID=f.FormID 
inner join Workflow_FieldDict d on f.FieldID=d.FieldID";
                    DataTable dtMailField = DbHelper.GetInstance().GetDBRecords("f.FieldID,f.FieldLabel", tblName1, "t.IsCancel=0 and t.TriggerID=" + _TFM.TriggerID.ToString() + " and f.GroupID=" + _TFM.TargetGroupID.ToString(), "f.DisplayOrder");

                    ddlFieldIDTo.DataSource = dtMailField;
                    ddlFieldIDTo.DataValueField = "FieldID";
                    ddlFieldIDTo.DataTextField = "FieldLabel";
                    ddlFieldIDTo.DataBind();

                    DataTable dtComputeType = DbHelper.GetInstance().GetDBRecords("ComputeTypeID, ComputeTypeName", "Workflow_ComputeType", "Useflag=1", "DisplayOrder");
                    lbFunctionType.DataSource = dtComputeType;
                    lbFunctionType.DataValueField = "ComputeTypeID";
                    lbFunctionType.DataTextField = "ComputeTypeName";
                    lbFunctionType.DataBind();


                    //SourceFieldType:主字段，明细字段组
                    string tableName2 = @"(
select 0 GroupID,GroupName='主字段'
union
select distinct f.GroupID,GroupName=g.GroupName 
from Workflow_NodeTriggerWorkflow t
inner join Workflow_Base b on t.WorkflowID=b.WorkflowID
inner join Workflow_FormField f on b.FormID=f.FormID 
inner join Workflow_FormFieldGroup g on f.GroupID=g.GroupID
where t.IsCancel=0 and t.TriggerID=" + _TFM.TriggerID.ToString() + ")T";
                    rblFieldType.DataSource = DbHelper.GetInstance().GetDBRecords("GroupID,GroupName", tableName2, "1=1", "GroupID");
                    rblFieldType.DataValueField = "GroupID";
                    rblFieldType.DataTextField = "GroupName";
                    rblFieldType.DataBind();
                    rblFieldType.SelectedIndex = 0;

                    BindSourceFieldList();

                    ReDisplayFunctionList();

                    DataTable dtExpression = DbHelper.GetInstance().GetDBRecords("ComputeType,ExpressionValue,ExpressionOrder", "Workflow_NodeTriggerExpression", "ExpressionID=0", "ExpressionOrder");
                    ViewState["dtExpression"] = dtExpression;
                    BindGridView();
                    }
                }
            }

        private void BindSourceFieldList()
            {
            //根据rblFieldType的值来绑定SourceFieldName的值
            string tblName2 = @"Workflow_NodeTriggerWorkflow t
inner join Workflow_Base b on t.WorkflowID=b.WorkflowID
inner join Workflow_FormField f on b.FormID=f.FormID 
inner join Workflow_FieldDict d on f.FieldID=d.FieldID";
            DataTable dtFieldType = DbHelper.GetInstance().GetDBRecords(" f.FieldID ,f.FieldLabel", tblName2, "t.IsCancel=0 and t.TriggerID=" + txtTriggerID.Value + " and f.GroupID=" + rblFieldType.SelectedValue, "f.DisplayOrder");
            lbFieldList.DataSource = dtFieldType;
            lbFieldList.DataValueField = "FieldID";
            lbFieldList.DataTextField = "FieldLabel"; 
            lbFieldList.DataBind();


            }

        #region gridview 绑定
        //此类需要更改，主要是更改获取数据源的方法

        private void BindGridView()
            {

            string tableName = @"
Workflow_TriggerWFFieldMappingDetail a
inner join Workflow_TriggerWFFieldMappingMain m on a.TriggerID=m.TriggerID  and a.MappingID=m.MappingID
left join Workflow_NodeTriggerWorkflow n on m.TriggerID=n.TriggerID 
left join Workflow_FieldDict d on a.TargetFieldID=d.FieldID and a.SourceFieldTypeID=d.FieldTypeID
left join Workflow_FieldType e on a.SourceFieldTypeID=e.FieldTypeID
left join Workflow_FormFieldGroup g1 on a.TargetGroupID=g1.GroupID 
left join Workflow_FormFieldGroup g2 on a.SourceGroupID=g2.GroupID 
";
            string columnName = @"a.SubMappingID,a.MappingID,a.TriggerID,
--a.TargetGroupID,a.SourceGroupID,
TargetGroupName=case a.TargetGroupID when 0 then '主字段' else g1.GroupName end,
a.TargetFieldID,
TargetFieldName=d.FieldDesc,
a.SourceFieldName,
a.SourceFieldTypeID,
SourceFieldTypeName=e.FieldTypeName,
SourceGroupName=case a.SourceGroupID when 0 then '主字段' else g2.GroupName end,
OPCycleTypeN=case m.OPCycleType when 0 then '一次' else '按明细行循环执行' end ";

            string whereCondition = "n.IsCancel=0 and  a.MappingID=" + DNTRequest.GetString("MappingID");

            DataTable dt = DbHelper.GetInstance().GetDBRecords(columnName, tableName, whereCondition, "a.MappingID,a.TargetFieldID");
            ExtendDatatable(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            BuildNoRecords(GridView1, dt);
            }

        private void ExtendDatatable(DataTable dt)
            {
            dt.Columns.Add(new DataColumn("ExpressionValueN", typeof(System.String)));
            for (int i = 0; i < dt.Rows.Count; i++)
                {
                dt.Rows[i]["ExpressionValueN"] = GetRuleDetail(dt.Rows[i])[1];
                }
            }

        //Show Header/Footer of Gridview with Empty Data Source 
        public void BuildNoRecords(GridView gridView, DataTable ds)
            {
            try
                {
                if (ds.Rows.Count == 0)
                    {
                    ds.Rows.Add(ds.NewRow());
                    gridView.DataSource = ds;
                    gridView.DataBind();
                    int columnCount = gridView.Rows[0].Cells.Count;
                    gridView.Rows[0].Cells.Clear();
                    gridView.Rows[0].Cells.Add(new TableCell());
                    gridView.Rows[0].Cells[0].ColumnSpan = columnCount;
                    gridView.Rows[0].Cells[0].Text = "No Records Found.";
                    }
                }
            catch
                {
                }
            }
        #endregion

        #region gridView 事件 --类型
        protected void GridView1_RowCreated(Object sender, GridViewRowEventArgs e)
            {
            if (e.Row.RowType == DataControlRowType.DataRow)
                {
                }
            }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
            {

            }
        //此类要进行dorpdownlist/chk控件的转换

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
            {

            }
        #endregion

        protected void btnUndo_Click(object sender, EventArgs e)
            {
            DataTable dtExpression = (DataTable)ViewState["dtExpression"];
            if (dtExpression.Rows.Count >= 1)
                {
                dtExpression.Rows.RemoveAt(dtExpression.Rows.Count - 1);
                }
            string[] RuleDetail = GetRuleDetail();
            lblExpression.Text = RuleDetail[1];

            BindGridView();
            System.Web.UI.ScriptManager.RegisterStartupScript(btnUndo, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
            }

        protected void btnAdd_Click(object sender, EventArgs e)
            {
            string[] RuleDetail = GetRuleDetail();
            if (rblFieldType.SelectedValue == "0")
                {
                try
                    {
                    DbHelper.GetInstance().ExecSqlText(string.Format(" select {0} from Workflow_Form where RequestID=0", RuleDetail[0]));
                    }
                catch
                    {
                    BindGridView();
                    ScriptManager.RegisterStartupScript(btnAdd, this.GetType(), "add", "alert('赋值表达式不正确,无法添加');", true);
                    return;
                    }
                }
            else
                {
                try
                    {
                    DbHelper.GetInstance().ExecSqlText(string.Format(" select {0} from Workflow_FormDetail where RequestID=0", RuleDetail[0]));
                    }
                catch
                    {
                    BindGridView();
                    ScriptManager.RegisterStartupScript(btnAdd, this.GetType(), "add", "alert('赋值表达式不正确,无法添加');", true);
                    return;
                    }
                }

            DataTable dtExpression = (DataTable)ViewState["dtExpression"];
            Workflow_TriggerWFFieldMappingDetailEntity _TFM = new Workflow_TriggerWFFieldMappingDetailEntity();
            _TFM.MappingID = Convert.ToInt32(DNTRequest.GetString("MappingID"));
            _TFM.TriggerID = Convert.ToInt32(txtTriggerID.Value  );
            _TFM.TargetGroupID = Int32.Parse(rblGroupTo.SelectedValue);
            _TFM.TargetFieldID = Int32.Parse(ddlFieldIDTo.SelectedValue);
            _TFM.SourceGroupID = Int32.Parse(rblFieldType.SelectedValue);
            _TFM.SourceFieldTypeID = Convert.ToByte ((rblFieldType.SelectedValue == "0") ?"1" : "2");
            _TFM.SourceFieldName = RuleDetail[0];

            DbHelper.GetInstance().AddWorkflow_TriggerWFFieldMappingDetail(_TFM, dtExpression);

            dtExpression.Rows.Clear();
            ViewState["dtExpression"] = dtExpression;
            lblExpression.Text = "";
            BindGridView();
        System.Web.UI.ScriptManager.RegisterStartupScript(btnAdd, this.GetType(), "ButtonHideScript", strButtonHideScript, false);

            }

        protected void btnDel_Click(object sender, EventArgs e)
            {
            for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                CheckBox cb = this.GridView1.Rows[i].FindControl("Item") as CheckBox;
                if (cb.Checked)
                    {
                    string MappingID = GridView1.DataKeys[i][0].ToString().Trim();
                    DbHelper.GetInstance().DeleteWorkflow_TriggerWFFieldMappingDetail(MappingID);
                    }
                }
            BindGridView();
            System.Web.UI.ScriptManager.RegisterStartupScript(btnDel, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
            }

        protected void lbFieldList_SelectedIndexChanged(object sender, EventArgs e)
            {
            DataTable dtExpression = (DataTable)ViewState["dtExpression"];
            DataRow drComputeRoute = dtExpression.NewRow();
            drComputeRoute["ComputeType"] = 2;
            drComputeRoute["ExpressionValue"] = lbFieldList.SelectedValue;//lbFieldList.SelectedValue.Substring(0, lbFieldList.SelectedValue.IndexOf(","));
            drComputeRoute["ExpressionOrder"] = dtExpression.Rows.Count + 1;
            dtExpression.Rows.Add(drComputeRoute);
            string[] RuleDetail = GetRuleDetail();
            lblExpression.Text = RuleDetail[1];

            BindGridView();
            lbFieldList.SelectedIndex = -1;
            }

        protected void lbFunctionType_SelectedIndexChanged(object sender, EventArgs e)
            {
            BindGridView();
            ReDisplayFunctionList();
            }

        protected void lbFunctionList_SelectedIndexChanged(object sender, EventArgs e)
            {
            DataTable dtExpression = (DataTable)ViewState["dtExpression"];
            DataRow drComputeRoute = dtExpression.NewRow();
            drComputeRoute["ComputeType"] = 1;
            drComputeRoute["ExpressionValue"] = lbFunctionList.SelectedValue;
            drComputeRoute["ExpressionOrder"] = dtExpression.Rows.Count + 1;
            dtExpression.Rows.Add(drComputeRoute);
            string[] RuleDetail = GetRuleDetail();
            lblExpression.Text = RuleDetail[1];

            BindGridView();
            lbFunctionList.SelectedIndex = -1;
            }

        private string[] GetRuleDetail(DataRow dr)
            {
            string[] RuleDetail = new string[2] { "", "" };

            DataTable dtExpression = DbHelper.GetInstance().GetDBRecords("*", "Workflow_NodeTriggerExpression", "MappingID="+dr["SubMappingID"], "MappingID,ExpressionOrder");
            for (int i = 0; i < dtExpression.Rows.Count; i++)
                {
                if (Convert.ToInt32(dtExpression.Rows[i]["ComputeType"]) == 1)
                    {
                    DataTable dtDetail = DbHelper.GetInstance().GetDBRecords("b.ComputeSymbol", "Workflow_NodeTriggerExpression a,Workflow_ComputeSymbol b", string.Format("a.ExpressionValue=b.ComputeID and a.ExpressionID={0}", dtExpression.Rows[i]["ExpressionID"]), "");
                    RuleDetail[0] = RuleDetail[0] + dtDetail.Rows[0]["ComputeSymbol"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dtDetail.Rows[0]["ComputeSymbol"].ToString();
                    }
                else if (Convert.ToInt32(dtExpression.Rows[i]["ComputeType"]) == 2)
                    {
                    string tblName2 = @"Workflow_NodeTriggerWorkflow t
inner join Workflow_Base b on t.WorkflowID=b.WorkflowID
inner join Workflow_FormField f on b.FormID=f.FormID 
inner join Workflow_FieldDict d on f.FieldID=d.FieldID";
                    DataTable dtDetail = DbHelper.GetInstance().GetDBRecords(" f.FieldID,f.FieldLabel,d.FieldName", tblName2, " t.TriggerID=" + txtTriggerID.Value + " and f.FieldID=" + dtExpression.Rows[i]["ExpressionValue"].ToString() + "and f.GroupID=" + rblFieldType.SelectedValue, "f.DisplayOrder");
                   
                    RuleDetail[0] = RuleDetail[0] + dtDetail.Rows[0]["FieldName"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dtDetail.Rows[0]["FieldLabel"].ToString();
                    }
                else
                    {
                    RuleDetail[0] = RuleDetail[0] + dtExpression.Rows[i]["ExpressionValue"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dtExpression.Rows[i]["ExpressionValue"].ToString();
                    }
                }


            RuleDetail[1] = RuleDetail[1].Trim(new char[] { ',' });

            return RuleDetail;
            }

        private string[] GetRuleDetail()
            {
            string[] RuleDetail = new string[2] { "", "" };
            DataTable dtExpression = (DataTable)ViewState["dtExpression"];
            for (int i = 0; i < dtExpression.Rows.Count; i++)
                {
                if (Convert.ToInt32(dtExpression.Rows[i]["ComputeType"]) == 1)
                    {
                    DataTable dtDetail = DbHelper.GetInstance().GetDBRecords("b.ComputeSymbol", "Workflow_ComputeSymbol b", string.Format("ComputeID='{0}'", dtExpression.Rows[i]["ExpressionValue"]), "");
                    RuleDetail[0] = RuleDetail[0] + dtDetail.Rows[0]["ComputeSymbol"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dtDetail.Rows[0]["ComputeSymbol"].ToString();
                    }
                else if (Convert.ToInt32(dtExpression.Rows[i]["ComputeType"]) == 2)
                    {

                    string tblName2 = @"Workflow_NodeTriggerWorkflow t
inner join Workflow_Base b on t.WorkflowID=b.WorkflowID
inner join Workflow_FormField f on b.FormID=f.FormID 
inner join Workflow_FieldDict d on f.FieldID=d.FieldID";
                    DataTable dtDetail = DbHelper.GetInstance().GetDBRecords(" f.FieldID,f.FieldLabel,d.FieldName", tblName2, "t.TriggerID=" + txtTriggerID.Value  + " and f.FieldID=" + dtExpression.Rows[i]["ExpressionValue"].ToString() + "and f.GroupID=" + rblFieldType.SelectedValue, "f.GroupID,f.DisplayOrder");
                   
          
                    RuleDetail[0] = RuleDetail[0] + dtDetail.Rows[0]["FieldName"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dtDetail.Rows[0]["FieldLabel"].ToString();
                    }
                else
                    {
                    RuleDetail[0] = RuleDetail[0] + dtExpression.Rows[i]["ExpressionValue"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dtExpression.Rows[i]["ExpressionValue"].ToString();
                    }
                }
            return RuleDetail;
            }

        private void ReDisplayFunctionList()
            {
            string ComputeTypeID = lbFunctionType.SelectedValue;
            DataTable dtFuncionList = DbHelper.GetInstance().GetDBRecords("ComputeID,ComputeSymbol", "Workflow_ComputeSymbol", string.Format("ComputeTypeID='{0}' and Useflag=1", ComputeTypeID), "DisplayOrder");
            lbFunctionList.DataSource = dtFuncionList;
            lbFunctionList.DataValueField = "ComputeID";
            lbFunctionList.DataTextField = "ComputeSymbol";
            lbFunctionList.DataBind();
            }

        protected void btnComputeOperation_Click(object sender, EventArgs e)
            {
            System.Web.UI.WebControls.Button btnComputeOperation = (System.Web.UI.WebControls.Button)sender;
            DataTable dtExpression = (DataTable)ViewState["dtExpression"];
            DataRow drComputeRoute = dtExpression.NewRow();
            drComputeRoute["ComputeType"] = 1;
            drComputeRoute["ExpressionValue"] = btnComputeOperation.ID.Replace("ComputeOperation", "");
            drComputeRoute["ExpressionOrder"] = dtExpression.Rows.Count + 1;
            dtExpression.Rows.Add(drComputeRoute);
            string[] RuleDetail = GetRuleDetail();
            lblExpression.Text = RuleDetail[1];

            BindGridView();
            }

        protected void btnConstant_Click(object sender, EventArgs e)
            {
            DataTable dtExpression = (DataTable)ViewState["dtExpression"];
            DataRow drComputeRoute = dtExpression.NewRow();
            drComputeRoute["ComputeType"] = rblConstantType.SelectedValue;
            if (rblConstantType.SelectedValue == "3")
                {
                drComputeRoute["ExpressionValue"] = txtConstantValue.Text != "" ? txtConstantValue.Text : "0";
                }
            else if (rblConstantType.SelectedValue == "4")
                {
                drComputeRoute["ExpressionValue"] = "'" + txtConstantValue.Text + "'";
                }
            drComputeRoute["ExpressionOrder"] = dtExpression.Rows.Count + 1;
            dtExpression.Rows.Add(drComputeRoute);
            string[] RuleDetail = GetRuleDetail();
            lblExpression.Text = RuleDetail[1];

            BindGridView();
            }

        protected void ddlFieldIDTo_SelectedIndexChanged(object sender, EventArgs e)
            {
          //  rblFieldType.SelectedValue = "0";
            ResetControlValue();
            }

        protected void rblFieldType_SelectedIndexChanged(object sender, EventArgs e)
            {
            ResetControlValue();
            }

        private void ResetControlValue()
            {
            BindSourceFieldList();
            ReDisplayFunctionList();
            lblExpression.Text = "";
            DataTable dt = (DataTable)(ViewState["dtExpression"]);
            dt.Rows.Clear();
            ViewState["dtExpression"] = dt;
            BindGridView();
            }
        }
    }
