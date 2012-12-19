using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyADO;
using System.IO;
using System.Security.AccessControl;
using GPRP.Web.UI;
using System.Diagnostics;
using System.Net;
using GPRP.Entity.Basic;
using System.Collections;

namespace GOA.Basic
{
    public partial class FileManage : BasePage
    {
        private int RootID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)  
            {
               // CopyDsoFramer();
              //  RegeditDsoFramer();
                GenerateRightTree();
               // RightTree.Attributes.Add("onclick", "OnCheckEvent()");

            }

        }

        #region treeview
        //绑定用户权限
        private void BindUserRight(string UserSerialID)
        {
            DataTable dt = DbHelper.GetInstance().GetDBRecords("*", "UserRight", "UserSerialID=" + UserSerialID, "MenuID");
            try
            {
                if (dt != null)
                {
                    setUnCheck(RightTree.Nodes);
                    if (dt.Rows.Count > 0)
                    {
                        setChecked(RightTree.Nodes, dt);
                    }
                }
            }
            catch (Exception err)
            {
                //lblMsg.Text = "获取用户权限数据失败，错误描述为:" + err.Message;
                //lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void setChecked(TreeNodeCollection nc, DataTable dt)
        {
            foreach (TreeNode node in nc)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (node.Value == dt.Rows[j]["MenuID"].ToString())
                    {
                        node.Checked = true;
                        break;
                    }
                }

                if (node.ChildNodes.Count != 0)
                {
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                    {
                        for (int jj = 0; jj < dt.Rows.Count; jj++)
                        {
                            if (node.ChildNodes[i].Value == dt.Rows[jj]["MenuID"].ToString())
                            {
                                node.ChildNodes[i].Checked = true;
                                break;
                            }
                        }
                        if (node.ChildNodes[i].ChildNodes.Count != 0)
                        {
                            setChecked(node.ChildNodes[i].ChildNodes, dt);
                        }
                    }
                }
            }
        }

        private void setUnCheck(TreeNodeCollection nc)
        {
            foreach (TreeNode node in nc)
            {
                node.Checked = false;
                if (node.ChildNodes.Count != 0)
                {
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                    {
                        node.ChildNodes[i].Checked = false;
                        if (node.ChildNodes[i].ChildNodes.Count != 0)
                        {
                            setUnCheck(node.ChildNodes[i].ChildNodes);
                        }
                    }
                }
            }
        }


        private void CopyDsoFramer()
        {
            try {
                string remoteUri = "http://localhost:3139/Basic/image/";
                string fileName = "dsoframer.ocx", myStringWebResource = null;
                WebClient myWebClient = new WebClient();
                myStringWebResource = remoteUri + fileName;
                myWebClient.DownloadFile(myStringWebResource, "C:/windows/system32/"+ fileName);
            }
            catch (Exception e)
            {
            
            
            }
            finally
            {
            
            }

       

        }

        private void RegeditDsoFramer()
        {


            Process pro = new Process();
            try
            {

                pro.StartInfo.FileName = "cmd.exe";
                pro.StartInfo.UseShellExecute = false;
                pro.StartInfo.RedirectStandardInput = true;
                pro.StartInfo.RedirectStandardError = true;
                pro.StartInfo.CreateNoWindow = true;
                pro.Start();
                pro.StandardInput.AutoFlush = true;
                pro.StandardInput.WriteLine("regsvr32 /s dsoframer.ocx");
                pro.StandardInput.WriteLine("exit");
                string str = pro.StandardError.ReadToEnd();
                pro.WaitForExit();
                pro.Close();
   
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                pro.Close();
            }

          

        }

        private void GenerateRightTree()
        {

            string path = "J:\\RD";
            RightTree.Nodes.Clear();
            RightTree.Nodes.Add(new TreeNode());
            DocFolderRight _docFolder = new DocFolderRight();
            _docFolder.FolderName = "RD";
            _docFolder.FullFolderName = path;
            _docFolder.FatherID = 0;
              DataTable dtt = DbHelper.GetInstance().GetFolderFromName(path);
              if (dtt.Rows.Count == 0)
              {
                  DbHelper.GetInstance().AddNewsFolder(_docFolder);
              }
            
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
            foreach (DirectoryInfo folder in dirInfo.GetDirectories())
            {
                if (folder.Name != "FileTemp")
                {
                    int FolderId = 0;
                    TreeNode folderNode = new TreeNode();

                    DocFolderRight _docFolder = new DocFolderRight();
                    _docFolder.FolderName = folder.Name;
                    _docFolder.FullFolderName = folder.FullName;
                    DataTable dtt = DbHelper.GetInstance().GetFolderFromName(folder.FullName);
                    if (dtt.Rows.Count == 0)
                    {
                        //if (RootID == 0)
                        //    _docFolder.FatherID = RootID;
                        //else
                        //{
                        //int iLast = path.LastIndexOf('\\')+1;
                        //path = path.Substring(iLast);

                        DataTable dt = DbHelper.GetInstance().GetFolderFromName(path);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                                _docFolder.FatherID = Int16.Parse(dt.Rows[i]["FolderSerialID"].ToString());
                        }

                        //  }

                        FolderId = DbHelper.GetInstance().AddNewsFolder(_docFolder);

                        ArrayList arylst = GetSearchParameter();
                        DataTable dtUser = DbHelper.GetInstance().sp_userList_1(arylst, 200, 1);

                        for (int i = 0; i < dtUser.Rows.Count; i++)
                        {
                            // UserList.Items.Add(new ListItem(dt.Rows[i]["UserID"].ToString() + "--" + dt.Rows[i]["UserName"].ToString(), dt.Rows[i]["UserSerialID"].ToString()));
                            DocUserRight _docUser = new DocUserRight();
                            _docUser.FolderID = FolderId;
                            _docUser.UserID = Int16.Parse(dtUser.Rows[i]["UserSerialID"].ToString());
                            _docUser.Permission = "0";
                            DbHelper.GetInstance().AddNewsFolderPermission(_docUser);
                        }

                    }
                    else
                    {
                        FolderId = Int16.Parse(dtt.Rows[0]["FolderSerialID"].ToString());
                    }
                    folderNode.Text = folder.Name;
                    folderNode.Value = folder.FullName;
                    folderNode.ToolTip = folder.Name;
                    folderNode.PopulateOnDemand = true;
                    folderNode.NavigateUrl = "javascript:SetPermission('" + FolderId + "')";
                    node.ChildNodes.Add(folderNode);
                }
             // TraversingCatalog(tn.ChildNodes[folderIndex], path + "/" + folder.Name); //递归遍历其它文件夹         
            }
            //foreach (FileInfo file in dirInfo.GetFiles("*.*"))
            //{
            //    TreeNode fileNode = new TreeNode();
            //    fileNode.Text = file.Name;
            //    fileNode.Value = file.FullName;
            //    string pathName = file.FullName.Replace("\\", "/");
            //    fileNode.ToolTip = file.Name;
            //    fileNode.Expanded = false;
            //    fileNode.NavigateUrl = "javascript:GetValue('" + pathName + "')";
            //    node.ChildNodes.Add(fileNode);
            //}

            return true;
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
        #endregion

        protected void SaveUserRight(object sender, EventArgs e)
        { 
        
        
        }




        
    }
}
