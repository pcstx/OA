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
    public partial class GGD0 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindControlValues();
                bindGridView();
            }
        }

        private void bindControlValues()
        {

            DataTable dtReportType = DbHelper.GetInstance().GetDBRecords("ReportTypeID,ReportTypeName", "Workflow_ReportType", "1=1", "DisplayOrder");
            ddlReportType.AddTableData(dtReportType, 0, 1, true, "Select");

            txtReportID.Value = DNTRequest.GetString("ReportID");

            DataTable dtMain = DbHelper.GetInstance().GetWorkflow_ReportMainDisplayTableByReportID(DNTRequest.GetString("ReportID"));

            if (dtMain.Rows.Count > 0)
            {
                txtReportName.Text = dtMain.Rows[0]["ReportName"].ToString();
                txtFormN.Text = dtMain.Rows[0]["FormName"].ToString();
                ddlReportType.SelectedValue = dtMain.Rows[0]["ReportTypeID"].ToString();

                for (int i = 0; i < dtMain.Rows.Count; i++)
                {
                    txtWorkflowName.Text += dtMain.Rows[i]["WorkflowName"].ToString() + ",";
                }

                txtWorkflowName.Text = txtWorkflowName.Text.Substring(0, txtWorkflowName.Text.Length - 1);

            }

            else
            {
                txtReportID.Value = "0";
                txtReportName.Text = "";
                txtFormN.Text = "";
                ddlReportType.SelectedIndex = -1;
                txtWorkflowName.Text = "";
            }


        }

        private void bindGridView()
        {
            DataTable dt = new DataTable();
            dt = DbHelper.GetInstance().GetWorkflow_ReportDetailDisplayTableByReportID(txtReportID.Value);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            BuildNoRecords(GridView1, dt);

        }

        //Show Header/Footer of Gridview with Empty Data Source 
        private void BuildNoRecords(GridView gridView, DataTable ds)
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

        protected void GridView1_RowCreated(Object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
        //1。更改key字段在GridView中的哪一行，默认都是第5行，第一二列为button 第三列为checkBox 第四列为edit字段，第五列 关键字列
        //2。通过关键字获取单条记录。


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleteField")
            {
                int index = Convert.ToInt32(e.CommandArgument);   //获取行号
                string ReportID = GridView1.DataKeys[index][0].ToString();
                string FieldID = GridView1.DataKeys[index][1].ToString();
                string strR = DbHelper.GetInstance().DeleteWorkflow_ReportDetailByReportIDandFieldID(Int32.Parse(ReportID), Int32.Parse(FieldID));
                if (Int32.Parse(strR) > 0)
                {
                    bindGridView();
                    lblmsg.InnerText = ResourceManager.GetString("Button_GoComplete");
                }
                else
                {
                    lblmsg.InnerText = ResourceManager.GetString("Operation_RECORD");
                }
            }

        }
        //此类要进行dorpdownlist/chk控件的转换


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Workflow_ReportMainEntity rm = new Workflow_ReportMainEntity();
            rm.ReportID = Int32.Parse(txtReportID.Value);
            rm.ReportTypeID = Int32.Parse(ddlReportType.SelectedValue.Trim());
            string strR = DbHelper.GetInstance().UpdateWorkflow_ReportMainReportType(rm);
            if (Int32.Parse(strR) > 0)
            {
                lblmsg.InnerText = ResourceManager.GetString("Button_GoComplete");
            }
            else
            {
                lblmsg.InnerText = ResourceManager.GetString("Operation_RECORD");
            }

            System.Web.UI.ScriptManager.RegisterStartupScript(btnSave, this.GetType(), "ButtonHideScript", strButtonHideScript, false);

        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            //级联删除，同时会删除子表的数据

            string ReportID = txtReportID.Value;
            int strR = DbHelper.GetInstance().DeleteWorkflow_ReportMain(ReportID);
            if ((strR) > 0)
            {
                bindControlValues();
                bindGridView();
                lblmsg.InnerText = ResourceManager.GetString("Button_GoComplete");
            }
            else
            {
                lblmsg.InnerText = ResourceManager.GetString("Operation_RECORD");
            }

            System.Web.UI.ScriptManager.RegisterStartupScript(btnDel, this.GetType(), "ButtonHideScript", strButtonHideScript, false);

        }
    }
}
