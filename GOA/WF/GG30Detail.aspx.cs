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
    public partial class GG30Detail : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string FieldTypeID = DNTRequest.GetString("ftid") != string.Empty ? DNTRequest.GetString("ftid") : "1";
                txtFieldTypeID.Value = FieldTypeID;
                lblFieldTypeName.InnerText = FieldTypeID == "1" ? "主字段:" : "明细字段:";
                lblBigTitle.Text = "已选" + (FieldTypeID == "1" ? "主字段" : "明细字段");
                BindGridView();

                GridView1.Columns[7].Visible = (FieldTypeID == "1");
                GridView1.Columns[8].Visible = (FieldTypeID == "1");
                GridView1.Columns[9].Visible = (FieldTypeID == "1");
                GridView1.Columns[10].Visible = (FieldTypeID == "1");
                txtFieldNameDesc.AddAttributes("readonly", "true");
            }
        }

        #region gridview 绑定

        //此类需要更改，主要是更改获取数据源的方法
        private void BindGridView()
        {
            string WhereCondition = "1=1";
            WhereCondition += " and a.FormID=" + DNTRequest.GetString("fmid");
            WhereCondition += " and a.GroupID=" + DNTRequest.GetString("gid");
            WhereCondition += " and b.FieldTypeID=" + txtFieldTypeID.Value;
            DataTable dt = DbHelper.GetInstance().GetDBRecords("a.*,b.FieldName,b.FieldDesc,b.HTMLTypeID,b.BrowseType,c.DataSetName,d.GroupName,e.HtmlTypeID", "Workflow_FormField a left join Workflow_FieldDict b on a.FieldID=b.FieldID left join Workflow_DataSet c on a.GroupLineDataSetID=c.DataSetID left join Workflow_FormFieldGroup d on a.TargetGroupID=d.GroupID left join Workflow_FieldDict e on a.FieldID=e.FieldID", WhereCondition, "a.DisplayOrder");
            ExtendDatatable(dt);
            ViewState["Workflow_FormField"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        private void ExtendDatatable(DataTable dt)
        {
            dt.Columns.Add(new DataColumn("HTMLTypeN", typeof(System.String)));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string FieldID = dt.Rows[i]["FieldID"].ToString();
                Workflow_FieldDictEntity _Workflow_FieldDictEntity = DbHelper.GetInstance().GetWorkflow_FieldDictEntityByKeyCol(FieldID);
                Workflow_HTMLTypeEntity _Workflow_HTMLTypeEntity = DbHelper.GetInstance().GetWorkflow_HTMLTypeEntityByKeyCol(_Workflow_FieldDictEntity.HTMLTypeID.ToString());
                string HTMLTypeN = _Workflow_HTMLTypeEntity.HTMLTypeDesc;
                if (_Workflow_FieldDictEntity.HTMLTypeID == 8
                    && _Workflow_FieldDictEntity.BrowseType > 0)
                {
                    Workflow_BrowseTypeEntity _Workflow_BrowseTypeEntity = DbHelper.GetInstance().GetWorkflow_BrowseTypeEntityByKeyCol(_Workflow_FieldDictEntity.BrowseType.ToString());
                    HTMLTypeN = HTMLTypeN + "-" + _Workflow_BrowseTypeEntity.BrowseTypeDesc;
                }
                dt.Rows[i]["HTMLTypeN"] = HTMLTypeN;
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
            if (e.CommandName == "select")
            {
            }
        }

        //此类要进行dorpdownlist/chk控件的转换
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.DropDownList ddlCSSStyle = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("CSSStyle");
                ddlCSSStyle.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CSSStyle"));

                System.Web.UI.WebControls.DropDownList ddlGroupID = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("TargetGroupID");
                ddlGroupID.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TargetGroupID"));

                System.Web.UI.WebControls.TextBox txtDataSetN = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("DataSetN");
                ImageButton ImgDataSet = (ImageButton)e.Row.FindControl("ImgDataSet");
                ImgDataSet.OnClientClick = string.Format("btnDataSetClick('{0}')", (e.Row.DataItemIndex + 2).ToString().PadLeft(2, '0'));
                ImageButton btnGroupLineField = (ImageButton)e.Row.FindControl("btnGroupLineField");
                if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "HtmlTypeID")) == 9)
                {
                    ddlGroupID.Visible = true;
                    ImgDataSet.Visible = true;
                    txtDataSetN.Visible = true;
                    btnGroupLineField.Visible = true;
                }
                else
                {
                    ddlGroupID.Visible = false;
                    ImgDataSet.Visible = false;
                    txtDataSetN.Visible = false;
                    btnGroupLineField.Visible = false;
                }
            }
        }
        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["Workflow_FormField"];
            string[] FieldIDList = txtFieldID.Value.Split(new char[] { ',' });
            for (int i = 0; i < FieldIDList.Length && txtFieldID.Value != string.Empty; i++)
            {
                string FieldID = FieldIDList.GetValue(i).ToString();
                if (dt.Select("FieldID=" + FieldID).Length == 0)
                {
                    Workflow_FormFieldEntity _Workflow_FormFieldEntity = new Workflow_FormFieldEntity();
                    _Workflow_FormFieldEntity.FormID = DNTRequest.GetInt("fmid", 0);
                    _Workflow_FormFieldEntity.FieldID = Convert.ToInt32(FieldID);
                    _Workflow_FormFieldEntity.GroupID = DNTRequest.GetInt("gid", 0);
                    _Workflow_FormFieldEntity.IsDetail = DNTRequest.GetInt("gid", 0) == 0 ? 0 : 1;
                    Workflow_FieldDictEntity _Workflow_FieldDictEntity = DbHelper.GetInstance().GetWorkflow_FieldDictEntityByKeyCol(FieldID);
                    _Workflow_FormFieldEntity.FieldLabel = _Workflow_FieldDictEntity.FieldDesc;
                    _Workflow_FormFieldEntity.DisplayOrder = (dt.Rows.Count + 1 + i) * 10;
                    DbHelper.GetInstance().AddWorkflow_FormField(_Workflow_FormFieldEntity);
                }
            }

            ArrayList arlst = new ArrayList();
            arlst.Add("Workflow_FormField");
            arlst.Add(DNTRequest.GetInt("fmid", 0));
            arlst.Add(DNTRequest.GetInt("gid", 0));
            DbHelper.GetInstance().sp_ReDisplayOrder(arlst);
            if (FieldIDList.Length > 0 && txtFieldID.Value != string.Empty)
            {
                DbHelper.GetInstance().sp_AlterWork_form_TableColumn(txtFieldID.Value, DNTRequest.GetInt("gid", 0) == 0 ? "1" : "2");
            }
            BindGridView();
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                string Field = GridView1.DataKeys[i][0].ToString().Trim();
                CheckBox cb = this.GridView1.Rows[i].FindControl("Item") as CheckBox;
                if (cb.Checked)
                {
                    string FormID = DNTRequest.GetString("fmid");
                    string GroupID = DNTRequest.GetString("gid");
                    DbHelper.GetInstance().DeleteWorkflow_FormField(FormID, Field, GroupID);
                }
            }
            ArrayList arlst = new ArrayList();
            arlst.Add("Workflow_FormField");
            arlst.Add(DNTRequest.GetInt("fmid", 0));
            arlst.Add(DNTRequest.GetInt("gid", 0));
            DbHelper.GetInstance().sp_ReDisplayOrder(arlst);
            BindGridView();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string Prefix = "GridView1$ctl" + (i + 2).ToString().PadLeft(2, '0') + "$";

                Workflow_FormFieldEntity _Workflow_FormFieldEntity = new Workflow_FormFieldEntity();
                _Workflow_FormFieldEntity.FormID = DNTRequest.GetInt("fmid", 0);
                _Workflow_FormFieldEntity.GroupID = DNTRequest.GetInt("gid", 0);
                _Workflow_FormFieldEntity.FieldID = Convert.ToInt32(GridView1.DataKeys[i][0]);
                _Workflow_FormFieldEntity.FieldLabel = DNTRequest.GetString(Prefix + "FieldLabel");
                _Workflow_FormFieldEntity.CSSStyle = DNTRequest.GetInt(Prefix + "CSSStyle", 0);
                _Workflow_FormFieldEntity.GroupLineDataSetID = DNTRequest.GetInt("txtDataSetID" + (i + 2).ToString().PadLeft(2, '0'), 0);
                _Workflow_FormFieldEntity.TargetGroupID = DNTRequest.GetInt(Prefix + "TargetGroupID", 0);
                _Workflow_FormFieldEntity.DisplayOrder = DNTRequest.GetInt(Prefix + "DisplayOrder", 9990);
                DbHelper.GetInstance().UpdateWorkflow_FormField(_Workflow_FormFieldEntity);
            }
            ArrayList arlst = new ArrayList();
            arlst.Add("Workflow_FormField");
            arlst.Add(DNTRequest.GetInt("fmid", 0));
            arlst.Add(DNTRequest.GetInt("gid", 0));
            DbHelper.GetInstance().sp_ReDisplayOrder(arlst);
            ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "btnSubmit", "alert('设定成功');", true);
            BindGridView();
        }

        public DataTable dtCSSStyle()
        {
            DataTable dt = DbHelper.GetInstance().GetDBRecords("CSSStyleID,CSSStyleClass", "Workflow_FieldCSSStyle", "1=1", "CSSStyleID");
            DataRow dr = dt.NewRow();
            dr["CSSStyleID"] = 0;
            dr["CSSStyleClass"] = "    ";
            dt.Rows.InsertAt(dr, 0);
            return dt;
        }

        public DataTable dtDetailFieldGroup()
        {
            DataTable dt = DbHelper.GetInstance().GetDBRecords("GroupID,GroupName", "Workflow_FormFieldGroup", "FormID=" + DNTRequest.GetInt("fmid", 0), "DisplayOrder");
            DataRow dr = dt.NewRow();
            dr["GroupID"] = 0;
            dr["GroupName"] = "    ";
            dt.Rows.InsertAt(dr, 0);
            return dt;
        }

        protected void Page_Init()
        {
            string WhereCondition = "1=1";
            WhereCondition += " and a.FormID=" + DNTRequest.GetString("fmid");
            WhereCondition += " and a.GroupID=" + DNTRequest.GetString("gid");
            WhereCondition += " and b.FieldTypeID=" + (DNTRequest.GetString("ftid") != "" ? DNTRequest.GetString("ftid") : "1");
            DataTable dt = DbHelper.GetInstance().GetDBRecords("a.*,b.FieldName,b.FieldDesc,b.HTMLTypeID,b.BrowseType,c.DataSetName,d.GroupName,e.HtmlTypeID", "Workflow_FormField a left join Workflow_FieldDict b on a.FieldID=b.FieldID left join Workflow_DataSet c on a.GroupLineDataSetID=c.DataSetID left join Workflow_FormFieldGroup d on a.TargetGroupID=d.GroupID left join Workflow_FieldDict e on a.FieldID=e.FieldID", WhereCondition, "a.DisplayOrder");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                HiddenField hf = new HiddenField();
                hf.ID = "txtDataSetID" + (i + 2).ToString().PadLeft(2, '0');
                hf.Value = dt.Rows[i]["GroupLineDataSetID"].ToString();
                PHHiddenField.Controls.Add(hf);
            }
        }
    }
}
