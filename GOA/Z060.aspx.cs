﻿using System;
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
    public partial class Z060 : BasePage
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
                DataTable dtUserList = DbHelper.GetInstance().sp_GetSysRoleUser(arylstQueryParamter);
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

        #region gridview 绑定
        //此类需要更改，主要是更改获取数据源的方法

        private void BindGridView()
        {
            string WhereCondition = "1=1";
            if (txtQRoleName.Text != string.Empty)
            {
                WhereCondition += "and RoleName like '%" + txtQRoleName.Text + "%'";
            }
            if (txtQRoleDesc.Text != string.Empty)
            {
                WhereCondition += "and RoleDesc like '%" + txtQRoleDesc.Text + "%'";
            }
            DataTable dt = DbHelper.GetInstance().GetDBRecords("*", "SysRole", WhereCondition, "DisplayOrder", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
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
                strOperationState = "Update";
                index = Convert.ToInt32(e.CommandArgument);   //获取行号
                string RoleID = GridView1.DataKeys[index][0].ToString().Trim();
                SysRoleEntity _SysRoleEntity = DbHelper.GetInstance().GetSysRoleEntityByKeyCol(RoleID);
                txtRoleID.Value = _SysRoleEntity.RoleID.ToString();
                txtRoleName.Text = _SysRoleEntity.RoleName;
                txtRoleDesc.Text = _SysRoleEntity.RoleDesc;
                txtDisplayOrder.Text = _SysRoleEntity.DisplayOrder.ToString();

                ArrayList arylstQueryParamter = new ArrayList();
                arylstQueryParamter.Add(_SysRoleEntity.RoleID);
                arylstQueryParamter.Add("");
                arylstQueryParamter.Add("");
                DataTable dtUserList = DbHelper.GetInstance().sp_GetSysRoleUser(arylstQueryParamter);
                GridView2.DataSource = dtUserList;
                GridView2.DataBind();
                ViewState["dtUserList"] = dtUserList;
            }
        }
        //此类要进行dorpdownlist/chk控件的转换

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string UseFlag = ((DataRowView)e.Row.DataItem).Row["Useflag"].ToString();
                System.Web.UI.WebControls.CheckBox chkUseFlag = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chkUseflag");
                chkUseFlag.Checked = UseFlag.Equals("1");

                string KeyCol = ((DataRowView)e.Row.DataItem).Row["RoleID"].ToString();
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
            SysRoleEntity _SysRoleEntity = new SysRoleEntity();
            _SysRoleEntity.RoleID = Convert.ToInt32(txtRoleID.Value != string.Empty ? txtRoleID.Value : "0");
            _SysRoleEntity.RoleName = txtRoleName.Text;
            _SysRoleEntity.RoleDesc = txtRoleDesc.Text;
            _SysRoleEntity.DisplayOrder = Convert.ToInt32(txtDisplayOrder.Text != string.Empty ? txtDisplayOrder.Text : "99990");
            _SysRoleEntity.UseFlag = chkUseFlag.Checked ? "1" : "0";
            _SysRoleEntity.CreateUser = userEntity.UserID;
            _SysRoleEntity.CreateDate = DateTime.Now;
            _SysRoleEntity.LastModifier = userEntity.UserID;
            _SysRoleEntity.LastModifyDate = DateTime.Now;
            _SysRoleEntity.dtUserList = (DataTable)ViewState["dtUserList"];

            string sResult = "-1";
            if (strOperationState == "Add")
                sResult = DbHelper.GetInstance().AddSysRole(_SysRoleEntity);
            else if (strOperationState == "Update")
                sResult = DbHelper.GetInstance().UpdateSysRole(_SysRoleEntity);

            ArrayList arl = new ArrayList();
            arl.Add("SysRole");
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
                //GPRP.GPRPData.DbHelper.GetInstance().DeleteProblemType(selectedLines[i].ToString());
            }

            ArrayList arl = new ArrayList();
            arl.Add("SysRole");
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