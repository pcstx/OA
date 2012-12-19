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
    public partial class GG30GroupLineFieldMap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGridView();
            }
        }

        #region gridview 绑定

        //此类需要更改，主要是更改获取数据源的方法
        private void BindGridView()
        {
            string WhereCondition = "1=1";
            WhereCondition += " and a.FormID=" + DNTRequest.GetString("fmid");
            WhereCondition += " and f.FieldID=" + DNTRequest.GetString("fdid");
            WhereCondition += " and b.FieldTypeID=2 ";
            DataTable dt = DbHelper.GetInstance().GetDBRecords("a.*,b.FieldName,b.FieldDesc,b.HTMLTypeID,b.BrowseType,c.DataSetName,d.GroupName,e.HtmlTypeID,g.TargetGroupField,g.DataSetColumn", "Workflow_FormField a left join Workflow_FieldDict b on a.FieldID=b.FieldID left join Workflow_DataSet c on a.GroupLineDataSetID=c.DataSetID left join Workflow_FormFieldGroup d on a.TargetGroupID=d.GroupID left join Workflow_FieldDict e on a.FieldID=e.FieldID left join Workflow_FormField f on a.FormID=f.FormID and a.GroupID=f.TargetGroupID left join Workflow_GroupLineFieldMap g on f.FormID=g.FormID and f.FieldID=g.FieldID and b.FieldName=g.TargetGroupField", WhereCondition, "a.DisplayOrder");
            ExtendDatatable(dt);
            ViewState["Workflow_FormField"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            BuildNoRecords(GridView1, dt);
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
                System.Web.UI.WebControls.DropDownList ddlDataSetColumn = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("DataSetColumn");
                ddlDataSetColumn.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DataSetColumn"));
            }
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DbHelper.GetInstance().DeleteWorkflow_GroupLineFieldMap(DNTRequest.GetString("fmid"), DNTRequest.GetString("fdid"));
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string Prefix = "GridView1$ctl" + (i + 2).ToString().PadLeft(2, '0') + "$";

                Workflow_GroupLineFieldMapEntity _GroupLineFieldMapEntity = new Workflow_GroupLineFieldMapEntity();
                _GroupLineFieldMapEntity.FormID = DNTRequest.GetInt("fmid", 0);
                _GroupLineFieldMapEntity.FieldID = DNTRequest.GetInt("fdid", 0);
                _GroupLineFieldMapEntity.DataSetColumn = DNTRequest.GetString(Prefix + "DataSetColumn");
                _GroupLineFieldMapEntity.TargetGroupField = GridView1.DataKeys[i][1].ToString();
                DbHelper.GetInstance().AddWorkflow_GroupLineFieldMap(_GroupLineFieldMapEntity);
            }

            ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "btnSubmit", "alert('设定成功');", true);
            BindGridView();
        }

        public DataTable dtDataSetColumn()
        {
            DataTable dtDataSetColumn = new DataTable();
            dtDataSetColumn.Columns.Add("ColumnName", typeof(System.String));
            DataRow drDataSetColumn = dtDataSetColumn.NewRow();
            drDataSetColumn["ColumnName"] = "";
            dtDataSetColumn.Rows.Add(drDataSetColumn);

            DataTable dt = DbHelper.GetInstance().GetDBRecords("a.ReturnColumns", "Workflow_DataSet a,Workflow_FormField b", "a.DataSetID=b.GroupLineDataSetID and b.FormID=" + DNTRequest.GetString("fmid") + " and b.FieldID=" + DNTRequest.GetString("fdid"), "");
            if (dt.Rows.Count > 0)
            {
                string ReturnColumns = dt.Rows[0]["ReturnColumns"].ToString();
                string[] Columns = ReturnColumns.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < Columns.Length; i++)
                {
                    drDataSetColumn = dtDataSetColumn.NewRow();
                    drDataSetColumn["ColumnName"] = Columns[i];
                    dtDataSetColumn.Rows.Add(drDataSetColumn);
                }
            }

            return dtDataSetColumn;
        }
    }
}
