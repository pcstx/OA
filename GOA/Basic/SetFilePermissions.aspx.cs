using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using MyADO;
using GPRP.Entity.Basic;
using GPRP.Web.UI;

namespace GOA.Basic
{
    public partial class SetFilePermissions : BasePage
    {
        private int FolderId;
        protected void Page_Load(object sender, EventArgs e)
        {

            FolderId = Int16.Parse(HttpUtility.UrlDecode(Context.Request.Params["FolderId"]));

            if (!Page.IsPostBack)
            {
                //绑定列表
                BindList();

               
                //选中第一个用户，抓其权限，绑定

                if (MyList.Items.Count > 0)
                {
                    MyList.Items[0].Selected = true;
                    BindRight(MyList.SelectedValue, FolderId,ListChoice.SelectedValue);
                }
                else
                {
                   // RightTree.Visible = false;
                }
            }

           
        }

        protected void Bind(object sender, EventArgs e)
        {
            BindList();
        }

        private void BindList()
        {

            string selectVaue = ListChoice.SelectedValue;
            DataTable dt = new DataTable();
            if (selectVaue == "1") //用户
            {
                try
                {
                    ArrayList arylst = GetSearchParameter();
                    dt = DbHelper.GetInstance().sp_userList_1(arylst, 200, 1);
                    MyList.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MyList.Items.Add(new ListItem(dt.Rows[i]["UserID"].ToString() + "--" + dt.Rows[i]["UserName"].ToString(), dt.Rows[i]["UserSerialID"].ToString()));
                    }
                    if (MyList.Items.Count > 0)
                    {
                        MyList.Items[0].Selected = true;
                        BindRight(MyList.SelectedValue, FolderId, ListChoice.SelectedValue);
                    }
                }
                catch (Exception err)
                {
                    lblMsg.Text = "获取用户数据失败，错误描述为:" + err.Message;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }else if(selectVaue == "2") //部门
            {
                try
                {

                   dt = DbHelper.GetInstance().GetDeptInfor();
                    MyList.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MyList.Items.Add(new ListItem(dt.Rows[i]["PBDEPDN"].ToString(), dt.Rows[i]["PBDEPID"].ToString()));
                    }
                    if (MyList.Items.Count > 0)
                    {
                        MyList.Items[0].Selected = true;
                        BindRight(MyList.SelectedValue, FolderId, ListChoice.SelectedValue);
                    }
                }
                catch (Exception err)
                {
                    lblMsg.Text = "获取用户数据失败，错误描述为:" + err.Message;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }

            }
            else if (selectVaue == "3") //角色
            {
                try
                {

                    dt = DbHelper.GetInstance().GetSysRole();
                    MyList.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MyList.Items.Add(new ListItem(dt.Rows[i]["RoleName"].ToString(), dt.Rows[i]["RoleID"].ToString()));
                    }
                    if (MyList.Items.Count > 0)
                    {
                        MyList.Items[0].Selected = true;
                        BindRight(MyList.SelectedValue, FolderId, ListChoice.SelectedValue);
                    }
                }
                catch (Exception err)
                {
                    lblMsg.Text = "获取用户数据失败，错误描述为:" + err.Message;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }

            
            }

        }

        private ArrayList GetSearchParameter()
        {
           ArrayList arylst = new ArrayList();
           arylst.Add("");//序号
           arylst.Add(txtQUserID.Text);//用户ID
           arylst.Add(txtQUserName.Text);//用户姓名
           arylst.Add(ddlQUserType.SelectedValue);//用户类型
           arylst.Add(txtQUserCode.Text); //员工编号
           arylst.Add("1");
           return arylst;
        }

        protected void showRight(object sender, EventArgs e)
        {
            string SelectVaue = ListChoice.SelectedValue;
            string SerialID = MyList.SelectedValue;
            BindRight(SerialID, FolderId, SelectVaue);
        }


        //private bool CheckFatherPermiss(string FullName, string checkFullName)
        //{
        //    if (FullName == "J:\\RD")
        //        return false;
        //    DataTable dt = DbHelper.GetInstance().GetFolderFromName(FullName);
        //    if (dt.Rows.Count > 0)
        //        return false;

        //    int index = FullName.LastIndexOf('\\');
        //    FullName = FullName.Substring(0, index);
        //    dt = DbHelper.GetInstance().GetFolderFromName(FullName);
        //    if (dt.Rows.Count > 0)
        //    {
        //        if (FullName == checkFullName)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            if (CheckFatherPermiss(FullName, checkFullName))
        //                return true;
        //        }
        //    }
        //    else
        //    {
        //        if (CheckFatherPermiss(FullName, checkFullName))
        //            return true;

        //    }
        //    return false;
        //}


        protected string GetFatherPermission(string SerialID, int ID, string SelectValue)
        {
            string FatherPermission = "";
            int FatherID = 0;
            DataTable dtFolderInfo = DbHelper.GetInstance().GetFolderInfoByID(ID);
            if (dtFolderInfo.Rows.Count > 0)
            {
                FatherID = Int32.Parse(dtFolderInfo.Rows[0]["FatherID"].ToString());
                if (dtFolderInfo.Rows[0]["FolderFullName"].ToString() == "J:\\RD")
                    return "1,2,3,4";

            }
            DataTable dt = new DataTable();
            if (SelectValue == "1")
            {
                dt = DbHelper.GetInstance().GetFolderPermissFromID(Int32.Parse(SerialID), ID);
            }
            else if (SelectValue == "2")
            {
                dt = DbHelper.GetInstance().GetFolderPermissFromDepart(Int32.Parse(SerialID), ID);
            }
            else if (SelectValue == "3")
            {
                dt = DbHelper.GetInstance().GetFolderPermissFromSysRole(Int32.Parse(SerialID), ID);
            }

            try
            {
                if (dt != null)
                {

                    if (dt.Rows.Count > 0)
                    {
                        FatherPermission = dt.Rows[0]["Permission"].ToString();
                        return FatherPermission;
                    }
                }
            }
            catch (Exception err)
            { 
            }
            return "";
        }

        private void BindRight(string SerialID, int FolderId, string SelectVaue)
        {
           
            string Permission = "";
            int ID = 0;
            lblMsg.Text = "";
            DataTable dtFull = new DataTable();
            dtFull = DbHelper.GetInstance().GetFolderInfoByID(FolderId);
            if (dtFull.Rows.Count > 0)
                ID = Int32.Parse(dtFull.Rows[0]["FatherID"].ToString());

            Permission = GetFatherPermission(SerialID, ID, SelectVaue);

            string [] szSplitPermission = Permission.Split(',');
            foreach (string finalPermission in szSplitPermission)
            {
                if (finalPermission != "")
                {
                   if (finalPermission == "1")
                    {
                        ckRead.Enabled = true;
                    }
                    else if (finalPermission == "2")
                    {
                        ckWrite.Enabled = true;
                    }
                    else if (finalPermission == "3")
                    {
                        ckDelete.Enabled = true;
                    }
                    else if (finalPermission == "4")
                    {
                        ckCancel.Enabled = true;
                    }
                }

            }


            DataTable dt= new DataTable();
            if (SelectVaue == "1")
            {
                dt = DbHelper.GetInstance().GetFolderPermissFromID(Int32.Parse(SerialID), FolderId);
            }
            else if (SelectVaue == "2")
            {
                dt = DbHelper.GetInstance().GetFolderPermissFromDepart(Int32.Parse(SerialID), FolderId);
            }
            else if (SelectVaue == "3")
            {
                dt = DbHelper.GetInstance().GetFolderPermissFromSysRole(Int32.Parse(SerialID), FolderId);
            }

            try
            {
                if (dt != null)
                {
                     
                    if (dt.Rows.Count > 0)
                    {
                        Permission = dt.Rows[0]["Permission"].ToString();
                        szSplitPermission = Permission.Split(',');
                        foreach (string finalPermission in szSplitPermission)
                        {
                            if (finalPermission != "")
                            {
                                if (finalPermission == "1")
                                    ckRead.Checked = true;
                                else if (finalPermission == "2")
                                    ckWrite.Checked = true;
                                else if (finalPermission == "3")
                                    ckDelete.Checked = true;
                                else if (finalPermission == "4")
                                    ckCancel.Checked = true;
                                else
                                {
                                    ckWrite.Checked = false;
                                    ckRead.Checked = false;
                                    ckDelete.Checked = false;
                                    ckCancel.Checked = false;
                                }
                            }
                        }

                    }
                    else
                    {

                        ckWrite.Checked = false;
                        ckRead.Checked = false;
                        ckDelete.Checked = false;
                        ckCancel.Checked = false;
                    }
                    
                }
            }
            catch (Exception err)
            {
                lblMsg.Text = "获取用户权限数据失败，错误描述为:" + err.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            BindList();
            if (MyList.Items.Count > 0)
            {
                MyList.Items[0].Selected = true;
                BindRight(MyList.SelectedValue, FolderId, ListChoice.SelectedValue);
            }
            else
            {

            }
            programmaticQueryModalPopup.Hide();
        }

      




        protected void SaveRight(object sender, EventArgs e)
        {
            string result = "-1";
            string Permission = "";
            string SelectVaue = ListChoice.SelectedValue;
            string AllPermission = "";
            if (ckRead.Checked)
            {
                Permission = "1";  //可读
                AllPermission = AllPermission + Permission + ",";
            }
            if (ckWrite.Checked)
            {
                Permission = "2";  //可写
                AllPermission = AllPermission  + Permission + ",";

            }
            if (ckDelete.Checked)
            {
                Permission = "3";  //可删除
                AllPermission = AllPermission + Permission + ",";
            }

            if (ckCancel.Checked)  // 可作废
            {
                Permission = "4";
                AllPermission = AllPermission  + Permission + ",";
            }
    

            if (SelectVaue == "1")
            {
                DocUserRight _docUser = new DocUserRight();
                _docUser.UserID = Int32.Parse(MyList.SelectedValue);
                _docUser.FolderID = FolderId;
                _docUser.Permission = AllPermission;
                if (DbHelper.GetInstance().GetFolderPermissFromID(Int32.Parse(MyList.SelectedValue), FolderId).Rows.Count > 0)
                    result = DbHelper.GetInstance().UpDateFolderPermission(_docUser);
                else
                    result = DbHelper.GetInstance().AddNewsFolderPermission(_docUser);
            }
            else if (SelectVaue == "2")
            {
                DocDepartRight _docDepartRight = new DocDepartRight();
                _docDepartRight.DepartMentID = Int32.Parse(MyList.SelectedValue);
                _docDepartRight.FolderID = FolderId;
                _docDepartRight.Permission = AllPermission;
                if (DbHelper.GetInstance().GetFolderPermissFromDepart(Int32.Parse(MyList.SelectedValue), FolderId).Rows.Count > 0)
                    result = DbHelper.GetInstance().UpDateFolderPermissionByDepart(_docDepartRight);
                else
                    result = DbHelper.GetInstance().AddNewFolderPermissionByDepart(_docDepartRight);

            }
            else if (SelectVaue == "3")
            {
                DocSysRoleRight _docSysRoleRight = new DocSysRoleRight();
                _docSysRoleRight.SysRoldID = Int32.Parse(MyList.SelectedValue);
                _docSysRoleRight.FolderID = FolderId;
                _docSysRoleRight.Permission = AllPermission;
                if (DbHelper.GetInstance().GetFolderPermissFromSysRole(Int32.Parse(MyList.SelectedValue), FolderId).Rows.Count > 0)
                    result = DbHelper.GetInstance().UpDateFolderPermissionBySysRole(_docSysRoleRight);
                else
                    result = DbHelper.GetInstance().AddNewFolderPermissionBySysRole(_docSysRoleRight);

            }

         if (result != "-1")
         {
             lblMsg.Text = "数据保存成功";
             lblMsg.ForeColor = System.Drawing.Color.Blue;
         }
         else
         {
             lblMsg.Text = "数据保存失败";
             lblMsg.ForeColor = System.Drawing.Color.Red;
         }
        }

   



    }
}
