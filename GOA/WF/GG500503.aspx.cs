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
    public partial class GG500503 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //TargetFieldType:主字段，明细字段组
                string tableName = @"(
select 0 GroupID,GroupName='主字段'
union
select distinct f.GroupID,GroupName=g.GroupName 
from Workflow_NodeTriggerWorkflow t
inner join Workflow_Base b on t.TriggerWFID=b.WorkflowID
inner join Workflow_FormField f on b.FormID=f.FormID 
inner join Workflow_FormFieldGroup g on f.GroupID=g.GroupID
where t.IsCancel=0 and t.TriggerID=" + DNTRequest.GetString("TriggerID") + ")T";
                rblGroupTo.DataSource = DbHelper.GetInstance().GetDBRecords("GroupID,GroupName", tableName, "1=1", "GroupID");
                rblGroupTo.DataValueField = "GroupID";
                rblGroupTo.DataTextField = "GroupName";
                rblGroupTo.DataBind();
                rblGroupTo.SelectedIndex = 0;

                BindGridView();
            }
        }


        #region gridview 绑定
        //此类需要更改，主要是更改获取数据源的方法


        private void BindGridView()
        {

            string tableName = @"
Workflow_TriggerWFFieldMappingMain a
left join Workflow_FormFieldGroup g1 on a.TargetGroupID=g1.GroupID 

";
            string columnName = @"a.MappingID,a.TriggerID,
TargetGroupName=case a.TargetGroupID when 0 then '主字段' else g1.GroupName end,
OPCycleTypeN=case a.OPCycleType when 0 then '一次' else '按明细行循环执行' end ";

            string whereCondition = " a.TriggerID=" + DNTRequest.GetString("TriggerID");

            DataTable dt = DbHelper.GetInstance().GetDBRecords(columnName, tableName, whereCondition, "a.MappingID,a.TargetGroupID");

            GridView1.DataSource = dt;
            GridView1.DataBind();
            BuildNoRecords(GridView1, dt);
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

            if (e.CommandName == "setMappingField")
            {
                //  strOperationState = "Update";
                int index = Convert.ToInt32(e.CommandArgument);   //获取行号
                string MappingID = GridView1.DataKeys[index][0].ToString().Trim();

                Response.Redirect("GG50050301.aspx?MappingID=" + MappingID);

            }
        }
        //此类要进行dorpdownlist/chk控件的转换


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        #endregion


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Workflow_TriggerWFFieldMappingMainEntity _TFM = new Workflow_TriggerWFFieldMappingMainEntity();
            _TFM.TriggerID = Convert.ToInt32(DNTRequest.GetString("TriggerID"));
            _TFM.TargetGroupID = Int32.Parse(rblGroupTo.SelectedValue);
            _TFM.OPCycleType = Convert.ToByte(ddlOPCycleType.SelectedValue);

            DbHelper.GetInstance().AddWorkflow_TriggerWFFieldMappingMain(_TFM);

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
                    DbHelper.GetInstance().DeleteWorkflow_TriggerWFFieldMappingMain(MappingID);
                }
            }
            BindGridView();
            System.Web.UI.ScriptManager.RegisterStartupScript(btnDel, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }
    }
}
