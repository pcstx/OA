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
using GPRP.Web.UI;
using GPRP.GPRPComponents;
using GPRP.GPRPControls;
using GPRP.GPRPEnumerations;
using GPRP.GPRPBussiness;
using GPRP.Entity;
using YYControls;
using MyADO;

namespace HRMWeb.aspx
{
    public partial class _0101002 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GenerateDeptTree();
                treeDeptartment.CollapseAll();
                treeDeptartment.Nodes[0].Expanded = true;
                //if ((int)Session["GroupId"] == 2)
                //{
                //    //
                //}

            }
        }

        #region 生成部门树
        private void GenerateDeptTree()
        {
            TreeNode node = new TreeNode();
            node.Value = "";
            node.Text = "选择";
            node.SelectAction = TreeNodeSelectAction.None;
            node.ShowCheckBox = false;
            treeDeptartment.Nodes.Add(node);
            node.PopulateOnDemand = true;
            //node.Expand();
            treeDeptartment.ExpandAll();

        }
        protected void treeDept_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            PBDEPEntity[] deptArray = GetChild(e.Node.Value.ToString());
            if (deptArray.Length > 0)
            {
                foreach (PBDEPEntity dept in deptArray)
                {
                    TreeNode node = new TreeNode();
                    node.Text = dept.DeptName;
                    node.Value = dept.DeptCode;
                    node.PopulateOnDemand = true;
                    //node.SelectAction = TreeNodeSelectAction.Select;//None;
                    node.SelectAction = TreeNodeSelectAction.None;
                    node.NavigateUrl = dept.DeptCode;
                    e.Node.ChildNodes.Add(node);
                }
            }

        }

        private PBDEPEntity[] GetChild(string parentDeptCode)
        {
            DataTable dt = new DataTable();
            if (parentDeptCode == "选择")
            {
                dt = DbHelper.GetInstance().GetChildDeptbyDeptCode("");
            }
            else
            {
                dt = DbHelper.GetInstance().GetChildDeptbyDeptCode(parentDeptCode);
            }
            return DbHelper.GetInstance().GetDeptEntityArray(dt);
        }
        #endregion
    }
}
