using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GPRP.Entity.Basic;
using MyADO;
using System.IO;
using System.Collections;
using GPRP.Web.UI;

namespace GOA.Basic
{
    public partial class MyDocument : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GenerateRightTree();
            }

        }

        private void GenerateRightTree()
        {

            string path = "J:\\RD";
            RightTree.Nodes.Clear();
            RightTree.Nodes.Add(new TreeNode());
            //DocFolderRight _docFolder = new DocFolderRight();
            //_docFolder.FolderName = "RD";
            //_docFolder.FullFolderName = path;
            //_docFolder.FatherID = 0;
            //DataTable dtt = DbHelper.GetInstance().GetFolderFromName(path);
            //if (dtt.Rows.Count == 0)
            //{
            //    DbHelper.GetInstance().AddNewsFolder(_docFolder);
            //}
            string[] pathinfo = Path.GetFullPath(path.Trim()).Split(char.Parse("\\")); //得到文件路径数组            
            RightTree.Nodes[0].Text = pathinfo[pathinfo.Length - 1];
            RightTree.Nodes[0].Value = path;
            RightTree.Nodes[0].PopulateOnDemand = true;

        }

        public bool LoadChildNode(TreeNode node, string path)
        {
            if (Directory.Exists(path) == false)
            {
                return false;
            }

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            int allNum = dirInfo.GetDirectories().Length + dirInfo.GetFiles("*.*").Length;
            if (allNum == 0)
            {
                TreeNode empty = new TreeNode();
                empty.Text = "(空白)";
                empty.Value = "";
                empty.ImageUrl = "";
                node.ChildNodes.Add(empty);
                return false;
            }

           int SysRoleID = 0;
           string  DepartName = "" ;

           DataTable dtSysRole = DbHelper.GetInstance().GetSysRoleIDByUserID(userEntity.UserSerialID);  //检查SysRole权限
           if (dtSysRole.Rows.Count > 0)
             SysRoleID  = Int32.Parse(dtSysRole.Rows[0]["RoleID"].ToString());
                 
           DataTable dtDepart = DbHelper.GetInstance().GetDepartIDByUserID(userEntity.UserSerialID);  //检查department权限
           if (dtDepart.Rows.Count > 0)
             DepartName= dtDepart.Rows[0]["PEEBIDEP"].ToString();

            foreach (DirectoryInfo folder in dirInfo.GetDirectories())
            {
                 if (folder.Name != "FileTemp")
                {
                string SysRolePermission,DepartPermission,UserPermission;
               
                DataTable dtFolder = DbHelper.GetInstance().GetFolderFromName(folder.FullName);
                if (dtFolder.Rows.Count > 0)
                {
                    int FolderID = Int32.Parse(dtFolder.Rows[0]["FolderSerialID"].ToString());  //获取文件夹的ID
                    DataTable dtSysRoleRight = DbHelper.GetInstance().GetFolderPermissFromSysRole(SysRoleID, FolderID);
                    if (dtSysRoleRight.Rows.Count > 0)
                    {
                          SysRolePermission = dtSysRoleRight.Rows[0]["Permission"].ToString();
                          if (SysRolePermission != "")
                          {

                              DataTable dtDepartInfo = DbHelper.GetInstance().GetDeptInforbyDeptName("", DepartName);
                              if (dtDepartInfo.Rows.Count > 0)
                              {
                                  int DepartID = 0;
                                  DepartID = Int32.Parse(dtDepartInfo.Rows[0]["PBDEPID"].ToString());
                                  DataTable dtDepartRight = DbHelper.GetInstance().GetFolderPermissFromDepart(DepartID, FolderID);
                                  if (dtDepartRight.Rows.Count > 0)
                                  {
                                      DepartPermission = dtDepartRight.Rows[0]["Permission"].ToString();
                                      if (DepartPermission != "")
                                      {
                                          DataTable dtUserRight = DbHelper.GetInstance().GetFolderPermissFromID(userEntity.UserSerialID, FolderID); //检查user权限
                                          if (dtUserRight.Rows.Count > 0)
                                          {
                                              UserPermission = dtUserRight.Rows[0]["Permission"].ToString();
                                              if (UserPermission != "")
                                              {
                                                  TreeNode folderNode = new TreeNode();
                                                  folderNode.Text = folder.Name;
                                                  folderNode.Value = folder.FullName;
                                                  folderNode.ToolTip = folder.Name;
                                                  folderNode.PopulateOnDemand = true;
                                                  string pathName = folder.FullName.Replace("\\", "/");
                                                  folderNode.NavigateUrl = "javascript:GetFiles('" + pathName + "')";
                                                  node.ChildNodes.Add(folderNode);


                                              }
                                          }
                                      }
                                  }
                              }
                          }
                        }
                            
                           }
                        }
                    }

            /*
             * 通过userEntity.UserSerialID获得SysRoleID,DepartID
             */
            //add

            //

            //DataTable dt = DbHelper.GetInstance().GetFolderByPermiss(userEntity.UserSerialID);
            //if (dt.Rows.Count > 0)
            //{
            //     foreach (DirectoryInfo folder in dirInfo.GetDirectories())
            //     {
            //          for (int i = 0; i < dt.Rows.Count; i++)
            //          {
            //              int FolderID = Int16.Parse(dt.Rows[i]["FolderID"].ToString());
                         
            //              /*
                           
            //               FolderID,SysRoleID 来获取Permission是否为可读
                           
            //               */
            //              // add

            //              //
            //              DataTable dtFolder = DbHelper.GetInstance().GetFolderInfoByID(FolderID);
            //             string ckfullName = dtFolder.Rows[0]["FolderFullName"].ToString();
            //             if (folder.FullName == ckfullName)
            //             {
            //                 TreeNode folderNode = new TreeNode();
            //                 folderNode.Text = folder.Name;
            //                 folderNode.Value = folder.FullName;
            //                 folderNode.ToolTip = folder.Name;
            //                 folderNode.PopulateOnDemand = true;
            //                 string pathName = folder.FullName.Replace("\\", "/");
            //                 folderNode.NavigateUrl = "javascript:GetFiles('" + pathName + "')";
            //                 node.ChildNodes.Add(folderNode);
            //             }
            //             else
            //             {
            //                 if (CheckFatherPermiss(folder.FullName, ckfullName))  //往上一级级的查看上级文件夹是否可读，到根文件夹为止，如果有文件夹不可读，那么下面的子文件夹都不可读
            //                 {
            //                     TreeNode folderNode = new TreeNode();
            //                     folderNode.Text = folder.Name;
            //                     folderNode.Value = folder.FullName;
            //                     folderNode.ToolTip = folder.Name;
            //                     folderNode.PopulateOnDemand = true;
            //                     string pathName = folder.FullName.Replace("\\", "/");
            //                     folderNode.NavigateUrl = "javascript:GetFiles('" + pathName + "')";
            //                     node.ChildNodes.Add(folderNode);
 
            //                 }
            //             }

            //          }
            
            //       }
                   //foreach (FileInfo file in dirInfo.GetFiles("*.*"))
                   //{
                   //   TreeNode fileNode = new TreeNode();
                   //   fileNode.Text = file.Name;
                   //   fileNode.Value = file.FullName;
                   //   string pathName = file.FullName.Replace("\\", "/");
                   //   fileNode.ToolTip = file.Name;
                   //   fileNode.Expanded = false;
                   //   fileNode.NavigateUrl = "javascript:GetValue('" + pathName + "')";
                   //   node.ChildNodes.Add(fileNode);
                   //}
              
           // }
            return true;
        }


        private bool CheckFatherPermiss(string FullName,string checkFullName)
        {
            if(FullName=="J:\\RD")
                return false;
            DataTable dt = DbHelper.GetInstance().GetFolderFromName(FullName);   
            if (dt.Rows.Count > 0)
                return false;

            int index = FullName.LastIndexOf('\\');
            FullName = FullName.Substring(0, index);
           dt = DbHelper.GetInstance().GetFolderFromName(FullName);
          if (dt.Rows.Count > 0)
          {
                    if (FullName == checkFullName)
                    {
                        return true;
                    }
                    else
                    {
                        if (CheckFatherPermiss(FullName, checkFullName))
                            return true;
                    }
            }
            else
            {
                if (CheckFatherPermiss(FullName, checkFullName))
                    return true;
                 
            }
            return false;
        }


        private ArrayList GetSearchParameter()
        {
            ArrayList arylst = new ArrayList();
            arylst.Add("");//序号
            arylst.Add("");//用户ID
            arylst.Add("");//用户姓名
            arylst.Add("");//用户类型
            arylst.Add(""); //员工编号
            arylst.Add("1");
            return arylst;
        }

        protected void TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {

            LoadChildNode(e.Node, e.Node.Value);

        }


    }
}
