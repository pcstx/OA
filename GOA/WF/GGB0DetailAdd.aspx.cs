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
    public partial class GGB0DetailAdd : BasePage
        {
        protected void Page_Load(object sender, EventArgs e)
            {
            if (!Page.IsPostBack)
                {
                txtReportID.Value = DNTRequest.GetString("ReportID");
                bindGridView();
                }
            }


        private void bindGridView()
            {
            DataTable dt = new DataTable();
            dt = DbHelper.GetInstance().GetWorkflow_ReportDetailEditTableByReportID(txtReportID.Value);
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

            }
        //此类要进行dorpdownlist/chk控件的转换

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
            {
            if (e.Row.RowType == DataControlRowType.DataRow)
                {

                DataRowView dv = (DataRowView)e.Row.DataItem;
                string strIsShow = dv.Row["IsShow"].ToString();
                string strIsStatistics = dv.Row["IsStatistics"].ToString();
                string strDataType = dv.Row["DataTypeID"].ToString();

                System.Web.UI.WebControls.CheckBox cbIsShow = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("cbIsShow");
                cbIsShow.Checked = strIsShow.Equals("1");

                System.Web.UI.WebControls.CheckBox cbIsStatistics = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("cbIsStatistics");
                System.Web.UI.WebControls.CheckBox cbIsOrder = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("cbIsOrder");
                System.Web.UI.WebControls.DropDownList ddlOrderPattern = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ddlOrderPattern");
                GPRP.GPRPControls.TextBox txtOrderIndex = (GPRP.GPRPControls.TextBox)e.Row.FindControl("txtOrderIndex");
                GPRP.GPRPControls.TextBox txtDisplayOrder = (GPRP.GPRPControls.TextBox)e.Row.FindControl("txtDisplayOrder");


                //设置是否统计显示与否
                if (strDataType == "2" || strDataType == "3") //如果字段类型是可统计类型int,float
                    {
                    cbIsStatistics.Visible = true;
                    cbIsStatistics.Checked = strIsStatistics.Equals("1");
                    }
                else
                    {
                    cbIsStatistics.Visible = false;
                    }


                if (cbIsShow.Checked)
                    {  //如果显示
                    cbIsStatistics.Enabled = true;//设置是否统计的可用性
                    //再设置排序字段

                    cbIsOrder.Enabled = true;
                    cbIsOrder.Checked = dv.Row["IsOrder"].ToString().Equals("1");
                    if (cbIsOrder.Checked) //如果有设排序
                        {
                        ddlOrderPattern.Enabled = true;
                        ddlOrderPattern.SelectedValue = dv.Row["OrderPattern"].ToString();

                        txtOrderIndex.Enabled = true;
                        txtOrderIndex.Text = dv.Row["OrderIndex"].ToString();
                        }
                    else
                        {
                        ddlOrderPattern.Enabled = false;
                        ddlOrderPattern.SelectedIndex = 0;

                        txtOrderIndex.Enabled = false;
                        txtOrderIndex.Text = "";
                        }
                    txtDisplayOrder.Enabled = true;
                    txtDisplayOrder.Text = dv.Row["DisplayOrder"].ToString();
                    }
                else
                    {
                    cbIsStatistics.Enabled = false;

                    cbIsOrder.Enabled = false;
                    cbIsOrder.Checked = false;

                    ddlOrderPattern.Enabled = false;
                    ddlOrderPattern.SelectedIndex = 0;

                    txtOrderIndex.Enabled = false;
                    txtOrderIndex.Text = "";

                    txtDisplayOrder.Enabled = false;
                    txtDisplayOrder.Text = "";
                    }
                //        string KeyCol = dv.Row["FieldID"].ToString();
                }
            }

        protected void btnSave_Click(object sender, EventArgs e)
            {
            //新增前先删除
            int ReportID = Int32.Parse(txtReportID.Value);

            DbHelper.GetInstance().DeleteWorkflow_ReportDetail(ReportID);

            for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                Workflow_ReportDetailEntity rd = new Workflow_ReportDetailEntity();

                System.Web.UI.WebControls.CheckBox cbIsShow = (System.Web.UI.WebControls.CheckBox)GridView1.Rows[i].FindControl("cbIsShow");

                if (cbIsShow.Checked)
                    {
                    System.Web.UI.WebControls.CheckBox cbIsStatistics = (System.Web.UI.WebControls.CheckBox)GridView1.Rows[i].FindControl("cbIsStatistics");
                    System.Web.UI.WebControls.CheckBox cbIsOrder = (System.Web.UI.WebControls.CheckBox)GridView1.Rows[i].FindControl("cbIsOrder");
                    System.Web.UI.WebControls.DropDownList ddlOrderPattern = (System.Web.UI.WebControls.DropDownList)GridView1.Rows[i].FindControl("ddlOrderPattern");
                    GPRP.GPRPControls.TextBox txtOrderIndex = (GPRP.GPRPControls.TextBox)GridView1.Rows[i].FindControl("txtOrderIndex");
                    GPRP.GPRPControls.TextBox txtDisplayOrder = (GPRP.GPRPControls.TextBox)GridView1.Rows[i].FindControl("txtDisplayOrder");

                    rd.ReportID = ReportID;
                    rd.FieldID = Convert.ToInt32(GridView1.DataKeys[i][1].ToString());
                    rd.IsStatistics = Convert.ToByte(cbIsStatistics.Checked ? 1 : 0);
                    rd.IsOrder = Convert.ToByte(cbIsOrder.Checked ? 1 : 0);

                    if (cbIsOrder.Checked)
                        {
                        rd.OrderPattern = Convert.ToByte(ddlOrderPattern.SelectedValue == "" ? 1 : 2);
                        rd.OrderIndex = txtOrderIndex.Text.Trim() == "" ? 0 : Int32.Parse(txtOrderIndex.Text);
                        }
                    if (txtDisplayOrder.Text.Trim() != "")
                        rd.DisplayOrder = Int32.Parse(txtDisplayOrder.Text);

                    DbHelper.GetInstance().AddWorkflow_ReportDetail(rd);
                    }
                }


            System.Web.UI.ScriptManager.RegisterStartupScript(btnSave, this.GetType(), "ButtonHideScript", strButtonHideScript, false);

            }


        protected void cbIsShow_OnCheckedChanged(object sender, EventArgs e)
            {
            CheckBox chk = (CheckBox)sender;

         //  第一种方法通过 Parent 获得GridViewRow   
            DataControlFieldCell dcf = (DataControlFieldCell)chk.Parent;    //这个对象的父类为cell   
            GridViewRow gr = (GridViewRow)dcf.Parent;                   //cell的父类就是row，这样就得到了该checkbox所在的该行   

            //另外一种NamingContainer获得 GridViewRow   
            int index = ((GridViewRow)(chk.NamingContainer)).RowIndex;    //通过NamingContainer可以获取当前checkbox所在容器对象，即gridviewrow   

                System.Web.UI.WebControls.CheckBox cbIsStatistics = (System.Web.UI.WebControls.CheckBox)gr.FindControl("cbIsStatistics");
                System.Web.UI.WebControls.CheckBox cbIsOrder = (System.Web.UI.WebControls.CheckBox)gr.FindControl("cbIsOrder");
              System.Web.UI.WebControls.DropDownList ddlOrderPattern = (System.Web.UI.WebControls.DropDownList)gr.FindControl("ddlOrderPattern");
                GPRP.GPRPControls.TextBox txtOrderIndex = (GPRP.GPRPControls.TextBox)gr.FindControl("txtOrderIndex");
                GPRP.GPRPControls.TextBox txtDisplayOrder = (GPRP.GPRPControls.TextBox)gr.FindControl("txtDisplayOrder");

            if (chk.Checked)
                {
                if (cbIsStatistics.Visible)
                    cbIsStatistics.Enabled = true;

                cbIsOrder.Enabled = true;
                ddlOrderPattern.Enabled = false ;
                txtOrderIndex.Enabled = false;
                txtDisplayOrder.Enabled = true  ;
                txtDisplayOrder.Text = "0";
                ddlOrderPattern.SelectedIndex = -1;
                txtOrderIndex.Text = "";
                }
            else
                {
                if (cbIsStatistics.Visible)
                    {
                    cbIsStatistics.Enabled = false;
                    cbIsStatistics.Checked = false;
                    }

                cbIsOrder.Enabled = false;
                cbIsOrder.Checked = false;
                ddlOrderPattern.Enabled = false ;
                txtOrderIndex.Enabled = false;
                txtDisplayOrder.Enabled = false;
                txtDisplayOrder.Text = "";
                ddlOrderPattern.SelectedIndex = -1;
                txtOrderIndex.Text = "";
                }
            
            }

        protected void cbIsOrder_OnCheckedChanged(object sender, EventArgs e)
            {
            CheckBox chk = (CheckBox)sender;

            //  第一种方法通过 Parent 获得GridViewRow   
            DataControlFieldCell dcf = (DataControlFieldCell)chk.Parent;    //这个对象的父类为cell   
            GridViewRow gr = (GridViewRow)dcf.Parent;                   //cell的父类就是row，这样就得到了该checkbox所在的该行   
          
        System.Web.UI.WebControls.DropDownList ddlOrderPattern = (System.Web.UI.WebControls.DropDownList)gr.FindControl("ddlOrderPattern");
            GPRP.GPRPControls.TextBox txtOrderIndex = (GPRP.GPRPControls.TextBox)gr.FindControl("txtOrderIndex");
          
            if (chk.Checked)
                {

                ddlOrderPattern.Enabled = true ;
                txtOrderIndex.Enabled = true ;
                ddlOrderPattern.SelectedIndex = 0;
                txtOrderIndex.Text = "0";
                }
            else
                {
                ddlOrderPattern.Enabled = false;
                txtOrderIndex.Enabled = false;
                ddlOrderPattern.SelectedIndex=-1;
                txtOrderIndex.Text = "";
                }

            }
        }
}
