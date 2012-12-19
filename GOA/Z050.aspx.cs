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
    public partial class Z050 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                AspNetPager1.PageSize = config.PageSize;
                ViewState["selectedLines"] = new ArrayList();
                BindGridView();
            }
        }

        //设置每页显示记录数，无须更改；如果要更改默认第页记录数，到config/geneal.config中更改 PageSize
        protected void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            if (txtPageSize.Text == "" || Convert.ToInt32(txtPageSize.Text) == 0)
            {
                ViewState["PageSize"] = config.PageSize;//每页显示的默认值

            }
            else
            {
                ViewState["PageSize"] = Convert.ToInt32(txtPageSize.Text);
            }
            AspNetPager1.PageSize = Convert.ToInt32(ViewState["PageSize"]);
            CollectSelected();
            BindGridView();
        }

        #region gridview 绑定
        //此类需要更改，主要是更改获取数据源的方法

        private void BindGridView()
        {
            string WhereCondition = "a.PBDEPUS='1'";
            if (txtQDeptID.Value != string.Empty)
            {
                WhereCondition += " and a.PBDEPID in (" + txtQDeptID.Value + ")";
            }
            if (txtQUserSerialID.Value != string.Empty)
            {
                WhereCondition += " and b.UserSerialID in (" + txtQUserSerialID.Value + ")";
            }
            string tables = @"
PBDEP a
left join DepartmentLeader b on a.PBDEPID=b.DeptID
left join UserList c on b.UserSerialID=c.UserSerialID
";
            DataTable dt = DbHelper.GetInstance().GetDBRecords("DeptID=a.PBDEPID,DeptName=a.PBDEPDN,b.UserSerialID,c.UserName", tables, WhereCondition, "a.PBDEPDC", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            if (dt.Rows.Count > 0)
                AspNetPager1.RecordCount = Convert.ToInt32(dt.Rows[0]["RecordCount"]);
            else
                AspNetPager1.RecordCount = 0;

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

        #region aspnetPage 分页代码
        //此类无须更改
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            CollectSelected();
            BindGridView();
        }
        protected void AspNetPager1_PageChanging(object src, EventArgs e)
        {
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
            int index = -1;
            if (e.CommandName == "select")
            {
                programmaticAddModalPopup.Show();

                index = Convert.ToInt32(e.CommandArgument);   //获取行号
                string DeptID = GridView1.DataKeys[index][0].ToString().Trim();
                string WhereCondition = "a.PBDEPUS='1'";
                if (DeptID != string.Empty)
                {
                    WhereCondition += " and a.PBDEPID in (" + DeptID + ")";
                }
                string tables = @"
PBDEP a
left join DepartmentLeader b on a.PBDEPID=b.DeptID
left join UserList c on b.UserSerialID=c.UserSerialID
";
                DataTable dt = DbHelper.GetInstance().GetDBRecords("DeptID=a.PBDEPID,DeptName=a.PBDEPDN,b.UserSerialID,c.UserName", tables, WhereCondition, "a.PBDEPDC", 1, 1);
                txtUserSerialID.Value = dt.Rows.Count > 0 ? dt.Rows[0]["UserSerialID"].ToString() : "0";
                txtUserName.Text = dt.Rows.Count > 0 ? dt.Rows[0]["UserName"].ToString() : string.Empty;
                txtDeptID.Value = dt.Rows.Count > 0 ? dt.Rows[0]["DeptID"].ToString() : "0";
                txtDeptName.Text = dt.Rows.Count > 0 ? dt.Rows[0]["DeptName"].ToString() : string.Empty;
            }
        }
        //此类要进行dorpdownlist/chk控件的转换

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string UseFlag = ((DataRowView)e.Row.DataItem).Row["UseFlag"].ToString();
                //System.Web.UI.WebControls.CheckBox chkUseFlag = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chkUseFlag");
                //chkUseFlag.Checked = UseFlag.Equals("1");

                string KeyCol = ((DataRowView)e.Row.DataItem).Row["DeptID"].ToString();
                CheckBox cb = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("Item") as CheckBox;
                ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
                if (selectedLines.Contains(KeyCol))
                {
                    cb.Checked = true;
                }
            }
        }
        #endregion

        protected void btnSearchRecord_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            ViewState["selectedLines"] = new ArrayList();
            BindGridView();
            System.Web.UI.ScriptManager.RegisterStartupScript(btnSearch, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }

        //此类一般不需要更改,因为保存的工作全部放在下面的SaveData中

        protected void hideModalPopupViaServer_Click(object sender, EventArgs e)
        {
            GPRP.GPRPControls.Button btn = (GPRP.GPRPControls.Button)sender;
            string sResult = "-1";
            if (btn.ID == "btnSubmitAndClose" || btn.ID == "btnSubmit")
            {
                //保存
                sResult = SaveData();
                if (sResult == "-1")
                {
                    lblMsg.Text = ResourceManager.GetString("Operation_RECORD");
                }
                else
                {
                    //refresh gridview
                    if (btn.ID == "btnSubmitAndClose")
                    {
                        programmaticAddModalPopup.Hide();
                    }
                }
                CollectSelected();
                BindGridView();
            }
            System.Web.UI.ScriptManager.RegisterStartupScript(btn, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }

        private string SaveData()
        {
            DepartmentLeaderEntity _DepartmentLeaderEntity = new DepartmentLeaderEntity();
            _DepartmentLeaderEntity.DeptID = Convert.ToInt32(txtDeptID.Value);
            _DepartmentLeaderEntity.UserSerialID = Convert.ToInt32(txtUserSerialID.Value);
            _DepartmentLeaderEntity.lastModifier = userEntity.UserID;
            _DepartmentLeaderEntity.lastModifyDate = DateTime.Now;

            string sResult = DbHelper.GetInstance().UpdateDepartmentLeader(_DepartmentLeaderEntity);
            return sResult;
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            bool f = false;
            CollectSelected();
            ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
            for (int i = 0; i < selectedLines.Count; i++)
            {
                //DbHelper.GetInstance().DeleteMaintainStatus(selectedLines[i].ToString());
            }
            if (f)
            { BindGridView(); }
            ViewState["selectedLines"] = new ArrayList();
        }

        private void CollectSelected()
        {
            ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                string KeyCol = GridView1.DataKeys[i][0].ToString().Trim();
                CheckBox cb = this.GridView1.Rows[i].FindControl("Item") as CheckBox;
                if (selectedLines.Contains(KeyCol) && !cb.Checked)
                    selectedLines.Remove(KeyCol);
                if (!selectedLines.Contains(KeyCol) && cb.Checked)
                    selectedLines.Add(KeyCol);
            }
        }
    }
}
