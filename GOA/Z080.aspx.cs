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
    public partial class Z080 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //绑定用户组列表
                BindGroupList();
                //生成权限树
                GenerateRightTree();
                RightTree.Attributes.Add("onclick", "OnCheckEvent()");
                //选中第一个用户，抓其权限，绑定
                if (GroupList.Items.Count > 0)
                {
                    RightTree.Visible = true;
                    GroupList.Items[0].Selected = true;
                    BindGroupRight(GroupList.SelectedValue);
                }
                else
                {
                    RightTree.Visible = false;
                }
            }
        }

        #region searchPannel
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGroupList();
            if (GroupList.Items.Count > 0)
            {
                RightTree.Visible = true;
                GroupList.Items[0].Selected = true;
                BindGroupRight(GroupList.SelectedValue);
            }
            else
            {
                RightTree.Visible = false;
            }
            programmaticQueryModalPopup.Hide();
        }
        #endregion

        #region listbox
        //绑定
        private void BindGroupList()
        {
            try
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
                DataTable dt = DbHelper.GetInstance().GetDBRecords("a.*,b.GroupTypeName", "SysGroup a,GroupType b", WhereCondition, "a.DisplayOrder");
                GroupList.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    GroupList.Items.Add(new ListItem(dt.Rows[i]["GroupName"].ToString(), dt.Rows[i]["GroupID"].ToString()));
                }
            }
            catch (Exception err)
            {
                lblMsg.Text = "获取用户数据失败，错误描述为:" + err.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        //选择单个用户组绑定树节点
        protected void GroupList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string GroupID = GroupList.SelectedValue;
            BindGroupRight(GroupID);
        }
        #endregion

        #region treeview

        //绑定用户权限
        private void BindGroupRight(string GroupID)
        {
            DataTable dt = DbHelper.GetInstance().GetDBRecords("GroupID, MenuID", "GroupRight", "GroupID=" + GroupID, "MenuID");
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
                lblMsg.Text = "获取用户权限数据失败，错误描述为:" + err.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
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

        private void GenerateRightTree()
        {
            TreeNode node = new TreeNode();
            node.Value = "";
            node.Text = "菜单";
            node.SelectAction = TreeNodeSelectAction.None;
            node.ShowCheckBox = false;
            node.SelectAction = TreeNodeSelectAction.None;
            RightTree.Nodes.Add(node);
            node.PopulateOnDemand = true;
            RightTree.ExpandAll();
        }

        protected void TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            DataTable dt = null;
            try
            {
                dt = DbHelper.GetInstance().GetPSSMEInfoOnlyNext("1", e.Node.Value.ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TreeNode node = new TreeNode();
                    node.Text = dt.Rows[i]["PSSMEMN"].ToString();
                    node.Value = dt.Rows[i]["PSSMEMC"].ToString();
                    node.PopulateOnDemand = true;
                    node.SelectAction = TreeNodeSelectAction.None;
                    //node.NavigateUrl = dt.Rows[i]["MenuProgramm"].ToString();
                    e.Node.ChildNodes.Add(node);
                }
            }
            catch (Exception err)
            {
                lblMsg.Text = "获取权限节点失败，错误描述为:" + err.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string result = "-1";
            string GroupID = GroupList.SelectedValue;
            ArrayList ArlMenu = new ArrayList();
            for (int i = 0; i < RightTree.CheckedNodes.Count; i++)
            {
                ArlMenu.Add(RightTree.CheckedNodes[i].Value);
            }
            result = DbHelper.GetInstance().AddGroupRight(GroupID, ArlMenu);
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
