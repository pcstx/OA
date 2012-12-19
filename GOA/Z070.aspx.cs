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
    public partial class Z070 : BasePage
    {
        private static string strOperationState;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["selectedLines"] = new ArrayList();
                AspNetPager1.PageSize = config.PageSize;
                BindGridView();
                ArrayList arylstQueryParamter = new ArrayList();
                arylstQueryParamter.Add("0");
                arylstQueryParamter.Add("");
                arylstQueryParamter.Add("");
                DataTable dtUserList = DbHelper.GetInstance().sp_GetSysGroupUser(arylstQueryParamter);
                ViewState["dtUserList"] = dtUserList;
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

        protected void txtPageSize2_TextChanged(object sender, EventArgs e)
            {
            if (txtPageSize2.Text == "" || Convert.ToInt32(txtPageSize2.Text) == 0)
                {
                ViewState["PageSize"] = config.PageSize;//每页显示的默认值
                AspNetPager2.PageSize = Convert.ToInt32(ViewState["PageSize"]);
                GridView2.PageSize = Convert.ToInt32(ViewState["PageSize"]);

                }
            else
                {
                AspNetPager2.PageSize = Convert.ToInt32(txtPageSize2.Text);
                GridView2.PageSize = Convert.ToInt32(txtPageSize2.Text);
                //ViewState["PageSize"] = Convert.ToInt32(txtPageSize2.Text);
                }
            
           // CollectSelected();
            BindGridView2();
            }

        #region gridview 绑定

        //此类需要更改，主要是更改获取数据源的方法
        private void BindGridView()
        {
            string WhereCondition = "a.GroupType=b.GroupTypeID ";
            if (txtQGroupName.Text != string.Empty)
            {
                WhereCondition += " and a.GroupName like '%" + txtQGroupName.Text + "%'";
            }
            if (txtQGroupDesc.Text != string.Empty)
            {
                WhereCondition += " and a.GroupDesc like '%" + txtQGroupDesc.Text + "%'";
            }
            DataTable dt = DbHelper.GetInstance().GetDBRecords("a.*,b.GroupTypeName", "SysGroup a,GroupType b", WhereCondition, "a.DisplayOrder", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
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

        protected void AspNetPager2_PageChanged(object src, EventArgs e)
            {
            //CollectSelected();
                GridView2.PageIndex = AspNetPager2.CurrentPageIndex - 1;
                BindGridView2();
            }
        protected void AspNetPager2_PageChanging(object src, EventArgs e)
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
                strOperationState = "Update";
                index = Convert.ToInt32(e.CommandArgument);   //获取行号
                string GroupID = GridView1.DataKeys[index][0].ToString().Trim();
                SysGroupEntity _SysGroupEntity = DbHelper.GetInstance().GetSysGroupEntityByKeyCol(GroupID);
                txtGroupID.Value = _SysGroupEntity.GroupID.ToString();
                txtGroupName.Text = _SysGroupEntity.GroupName;
                txtGroupDesc.Text = _SysGroupEntity.GroupDesc;
                txtDisplayOrder.Text = _SysGroupEntity.DisplayOrder.ToString();

                ArrayList arylstQueryParamter = new ArrayList();
                arylstQueryParamter.Add(_SysGroupEntity.GroupID);
                arylstQueryParamter.Add("");
                arylstQueryParamter.Add("");
                DataTable dtUserList = DbHelper.GetInstance().sp_GetSysGroupUser(arylstQueryParamter);
                if (dtUserList.Rows.Count > 0)
                    AspNetPager2.RecordCount = Convert.ToInt32(dtUserList.Rows.Count);
                else
                    AspNetPager2.RecordCount = 0;
                GridView2.DataSource = dtUserList;
                GridView2.DataBind();
                ViewState["dtUserList"] = dtUserList;
            }
        }

        private void BindGridView2()
        {
        string GroupID = txtGroupID.Value;
        
        SysGroupEntity _SysGroupEntity = DbHelper.GetInstance().GetSysGroupEntityByKeyCol(GroupID);
                ArrayList arylstQueryParamter = new ArrayList();
        arylstQueryParamter.Add(_SysGroupEntity.GroupID);
        arylstQueryParamter.Add("");
        arylstQueryParamter.Add("");
        DataTable dtUserList = DbHelper.GetInstance().sp_GetSysGroupUser(arylstQueryParamter);

        if (dtUserList.Rows.Count > 0)
            AspNetPager2.RecordCount = Convert.ToInt32(dtUserList.Rows.Count );
        else
            AspNetPager2.RecordCount = 0;
        GridView2.DataSource = dtUserList;
        GridView2.DataBind();
        ViewState["dtUserList"] = dtUserList;
            
            }


        //此类要进行dorpdownlist/chk控件的转换
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string UseFlag = ((DataRowView)e.Row.DataItem).Row["Useflag"].ToString();
                System.Web.UI.WebControls.CheckBox chkUseFlag = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chkUseflag");
                chkUseFlag.Checked = UseFlag.Equals("1");

                string KeyCol = ((DataRowView)e.Row.DataItem).Row["GroupID"].ToString();
                CheckBox cb = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("Item");
                ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
                if (selectedLines.Contains(KeyCol))
                {
                    cb.Checked = true;
                }

                string GroupType = ((DataRowView)e.Row.DataItem).Row["GroupType"].ToString();
                LinkButton btnSelect = (LinkButton)e.Row.FindControl("btnSelect");
                btnSelect.Enabled = GroupType != "9";
                cb.Enabled = GroupType != "9";
            }
        }
        #endregion

        protected void btnSearchRecord_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            ViewState["selectedLines"] = new ArrayList();
            BindGridView();
            System.Web.UI.ScriptManager.RegisterStartupScript(btnQuery, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
            programmaticQueryModalPopup.Hide();
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
            SysGroupEntity _SysGroupEntity = new SysGroupEntity();
            _SysGroupEntity.GroupID = Convert.ToInt32(txtGroupID.Value != string.Empty ? txtGroupID.Value : "0");
            _SysGroupEntity.GroupName = txtGroupName.Text;
            _SysGroupEntity.GroupDesc = txtGroupDesc.Text;
            _SysGroupEntity.GroupType = 1;
            _SysGroupEntity.DisplayOrder = Convert.ToInt32(txtDisplayOrder.Text != string.Empty ? txtDisplayOrder.Text : "99990");
            _SysGroupEntity.UseFlag = chkUseFlag.Checked ? "1" : "0";
            _SysGroupEntity.CreateUser = userEntity.UserID;
            _SysGroupEntity.CreateDate = DateTime.Now;
            _SysGroupEntity.LastModifier = userEntity.UserID;
            _SysGroupEntity.LastModifyDate = DateTime.Now;
            _SysGroupEntity.dtUserList = (DataTable)ViewState["dtUserList"];

            string sResult = "-1";
            if (strOperationState == "Add")
                sResult = DbHelper.GetInstance().AddSysGroup(_SysGroupEntity);
            else if (strOperationState == "Update")
                sResult = DbHelper.GetInstance().UpdateSysGroup(_SysGroupEntity);

            ArrayList arl = new ArrayList();
            arl.Add("SysGroup");
            arl.Add("");
            arl.Add("");
            sResult = DbHelper.GetInstance().sp_ReDisplayOrder(arl);
            return sResult;
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            CollectSelected();
            ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
            for (int i = 0; i < selectedLines.Count; i++)
            {
                DbHelper.GetInstance().DeleteSysGroup(selectedLines[i].ToString());
            }
            ArrayList arl = new ArrayList();
            arl.Add("SysGroup");
            arl.Add("");
            arl.Add("");
            DbHelper.GetInstance().sp_ReDisplayOrder(arl);
            BindGridView();
            ViewState["selectedLines"] = new ArrayList();
        }

        protected void btn_AddRow(object sender, EventArgs e)
        {
            DataTable dtUserList = (DataTable)ViewState["dtUserList"];

            if (txtUserSerialID.Value != string.Empty)
            {
                string[] ArrUserSerialID = txtUserSerialID.Value.Split(new char[] { ',' });
                for (int i = 0; i < ArrUserSerialID.Length; i++)
                {
                    if (ArrUserSerialID[i] != string.Empty && dtUserList.Select("UserSerialID=" + ArrUserSerialID[i]).Length == 0)
                    {
                        DataRow drUser = dtUserList.NewRow();
                        UserListEntity _UserListEntity = DbHelper.GetInstance().GetUserListEntityByKeyCol(ArrUserSerialID[i]);
                        drUser["UserSerialID"] = _UserListEntity.UserSerialID;
                        drUser["UserID"] = _UserListEntity.UserID;
                        drUser["UserName"] = _UserListEntity.UserName;
                        drUser["UserCode"] = _UserListEntity.UserCode;
                        drUser["DeptName"] = _UserListEntity.DeptName;
                        dtUserList.Rows.Add(drUser);
                    }
                }
            }
            ViewState["dtUserList"] = dtUserList;
            GridView2.DataSource = dtUserList;
            GridView2.DataBind();
            BindGridView();
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = -1;
            if (e.CommandName == "select")
            {
                index = Convert.ToInt32(e.CommandArgument);   //获取行号
                string UserSerialID = GridView2.DataKeys[index][0].ToString().Trim();
                DataTable dtUserList = (DataTable)ViewState["dtUserList"];
                for (int i = 0; i < dtUserList.Rows.Count; i++)
                {
                    if (dtUserList.Rows[i]["UserSerialID"].ToString().Equals(UserSerialID))
                    {
                        dtUserList.Rows.RemoveAt(i);
                    }
                }
                ViewState["dtUserList"] = dtUserList;
                GridView2.DataSource = dtUserList;
                GridView2.DataBind();
                BindGridView();
            }
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

        [WebMethod]
        public static string SetAddViewState()
        {
            strOperationState = "Add";
            return "";
        }
    }
}
