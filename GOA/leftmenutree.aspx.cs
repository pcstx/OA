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
using GPRP.Entity;
using GPRP.GPRPComponents;
using GPRP.Web.UI;
using MyADO;

namespace GOA
{
    public partial class leftmenutree : BasePage
    {
        protected DataTable ProductTypeDataTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GenerateMenuTree();

                TreeView1.CollapseAll();
                TreeView1.Nodes[0].Expanded = true;
            }
        }

        private void GenerateMenuTree()
        {
            ProductTypeDataTable = DbHelper.GetInstance().GetPSSMEInfoOfUser(userEntity.UserSerialID.ToString(), "");
            TreeNode node = new TreeNode();
            node.Value = "";
            node.Text = "总菜单";
            node.SelectAction = TreeNodeSelectAction.Expand;
            TreeView1.Nodes.Add(node);
            node.PopulateOnDemand = true;

            TreeView1.ExpandAll();
        }

        protected void treeMenu_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            string userID = userEntity.UserID;
            try
            {
                DataView dv = new DataView();
                string parentId = e.Node.Value.ToString();
                dv.Table = ProductTypeDataTable.Copy();
                dv.RowFilter = "PSSMEPMC='" + parentId + "' and PSSMEUS='1'";
                foreach (DataRowView drv in dv)
                {
                    TreeNode node = new TreeNode();
                    node.Text = drv["PSSMEMN"].ToString();
                    node.Value = drv["PSSMEMC"].ToString();
                    node.PopulateOnDemand = true;
                    node.SelectAction = TreeNodeSelectAction.Expand;
                    node.NavigateUrl = drv["PSSMEMP"].ToString();
                    node.Target = drv["PSSMEOWT"].ToString();
                    e.Node.ChildNodes.Add(node);
                }
            }
            catch
            {
            }
        }
    }
}
