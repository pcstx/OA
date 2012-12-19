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

namespace GOA
{
    public partial class DeptSelect : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GenerateDeptTree();
                treeDeptartment.CollapseAll();
                treeDeptartment.Nodes[0].Expanded = true;
            }
        }

        #region 生成部门树

        private void GenerateDeptTree()
        {
            TreeNode node = new TreeNode();
            node.Value = "0";
            node.Text = "捷奥比";
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
                    node.Value = dept.DeptID;
                    node.PopulateOnDemand = true;
                    //node.SelectAction = TreeNodeSelectAction.Select;//None;
                    node.SelectAction = TreeNodeSelectAction.None;
                    node.NavigateUrl = dept.DeptID;
                    e.Node.ChildNodes.Add(node);
                }
            }

        }

        private PBDEPEntity[] GetChild(string parentDeptID)
        {
            DataTable dt = new DataTable();
            if (parentDeptID == "捷奥比")
            {
                dt = DbHelper.GetInstance().GetChildDeptbyDeptID("0");
            }
            else
            {
                dt = DbHelper.GetInstance().GetChildDeptbyDeptID(parentDeptID);
            }
            return DbHelper.GetInstance().GetDeptEntityArray(dt);
        }
        #endregion
    }
}
