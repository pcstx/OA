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
using MyADO;

namespace GPRPWeb.tools
{
    public partial class Menu : System.Web.UI.Page
    {
        protected DataTable ProductTypeDataTable;
        private static int mMoudleID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                //checkboxlist绑定
                Chk("1");
                ChkIsProcess("2");
                lblTitle.Text = "全部";
                lblForInsert.Text = "所选中节点名称为";
                lblParentCode.Text = "0";
                mMoudleID = 0;
                LoadTree();


            }
        }
        private void clearTxt()
        {

            mMoudleID = 0;
            txtCodeOne.Text = "";
            txtiFrame.Text = "";
            txtLink.Text = "";
            txtOrderby.Text = "";
            txtTitleOne.Text = "";
            chkValiad.DataTableList = ValiadDataTable();
            chkValiad.SetSelected("1");
            txtTitleEn.Text = "";
            txtTitleTW.Text = "";
            chkIsProcess.DataTableList = IsProcessDataTable();
            chkIsProcess.SetSelected("2");
        }
        public void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            lblForInsert.Text = "所选中节点名称为";
            if (TreeView1.SelectedValue.ToString() == "")
            {
                clearTxt();
                lblTitle.Text = " 全部";

                lblParentCode.Text = "";

            }
            else
            {

                PSSMETable dt = DbHelper.GetInstance().GetPSSMEInfo(TreeView1.SelectedValue);

                mMoudleID = dt.ModleID;
                txtCodeOne.Text = dt.ModleCode;
                txtiFrame.Text = dt.MenuLinkTarget;
                txtLink.Text = dt.MenuLink;
                txtOrderby.Text = dt.OrderBy.ToString();
                txtTitleOne.Text = dt.MenuName;
                chkValiad.DataTableList = ValiadDataTable();
                chkValiad.SetSelected(dt.MenuIsValid);
                chkIsProcess.DataTableList = IsProcessDataTable();
                chkIsProcess.SetSelected(dt.IsProcess);
                lblTitle.Text = dt.MenuName;
                lblParentCode.Text = dt.ModleCode;
                txtTitleTW.Text = dt.MouldNameTW;
                txtTitleEn.Text = dt.MouldNameEn;

            }
            ButtonEnable("selectTreeView");
        }
        private void Chk(string selectValue)
        {

            DataTable dt_ChkList = ValiadDataTable();
            chkValiad.AddTableData(dt_ChkList, selectValue);
        }
        private DataTable ValiadDataTable()
        {
            DataTable dt_ChkList = new DataTable();

            dt_ChkList.Columns.Add(new DataColumn("ID", typeof(System.String)));
            dt_ChkList.Columns.Add(new DataColumn("Text", typeof(System.String)));

            DataRow dr_TopMenu;
            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "1";
            dr_TopMenu[1] = "可用";
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "0";
            dr_TopMenu[1] = "不可用";
            dt_ChkList.Rows.Add(dr_TopMenu);

            return dt_ChkList;
        }
        private void ChkIsProcess(string selectValue)
        {

            DataTable dt_ChkList = IsProcessDataTable();
            chkIsProcess.AddTableData(dt_ChkList, selectValue);
        }
        private DataTable IsProcessDataTable()
        {
            DataTable dt_ChkList = new DataTable();

            dt_ChkList.Columns.Add(new DataColumn("ID", typeof(System.String)));
            dt_ChkList.Columns.Add(new DataColumn("Text", typeof(System.String)));

            DataRow dr_TopMenu;
            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "0";
            dr_TopMenu[1] = "关闭流程时不显示";
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "1";
            dr_TopMenu[1] = "开户流程时显示";
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "2";
            dr_TopMenu[1] = "与流程无关";
            dt_ChkList.Rows.Add(dr_TopMenu);
            return dt_ChkList;
        }
        protected void InitTree(TreeNodeCollection Nds, string parentId)//用递归方法动态生成节点
        {
            DataView dv = new DataView();
            TreeNode tmpNode;
            dv.Table = ProductTypeDataTable;
            dv.RowFilter = "PSSMEPMC='" + parentId + "'";
            foreach (DataRowView drv in dv)
            {
                tmpNode = new TreeNode();
                tmpNode.Value = drv["PSSMEMC"].ToString();
                tmpNode.Text = "<font color=red><b>" + drv["PSSMEMC"].ToString() + "</b></font>  " + drv["PSSMEMN"].ToString() + "/" + drv["PSSMEMNTW"].ToString() + "/" + drv["PSSMEMNEN"].ToString();

                Nds.Add(tmpNode);

                this.InitTree(tmpNode.ChildNodes, tmpNode.Value);
            }
        }
        private void LoadTree()
        {
            TreeView1.Nodes.Clear();
            TreeNode tmpNode;
            tmpNode = new TreeNode();
            tmpNode.Value = "";
            tmpNode.Text = "全部";
            TreeView1.Nodes.Add(tmpNode);
            ProductTypeDataTable = DbHelper.GetInstance().GetPSSMEInfo("", "");
            InitTree(TreeView1.Nodes, "");

        }
        protected void OKOne_Click(object sender, EventArgs e)
        {
            if (btnOKOne.Text == "Insert")
            {
                ButtonEnable("insert");
                lblForInsert.Text = "其父项节点是";
                clearTxt();
                txtiFrame.Text = "main";
            }
            else
            {
                int result = SaveData();
                if (result > 0)
                {
                    //如果添加成功则重新加载树型控件 
                    LoadTree();
                    lblMsg.Text = "增加成功！";
                    ButtonEnable("save");
                }
                else if (result == -1)
                {
                    lblMsg.Text = "增加失败，该代码已经存在于数据中";
                }
            }
        }

        private void ButtonEnable(string Operation)
        {
            switch (Operation)
            {
                case ("insert"):
                    btnCancelOne.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnOKOne.Text = "Save";
                    break;
                case ("selectTreeView"):
                    btnCancelOne.Enabled = true;
                    btnUpdate.Enabled = true;
                    btnOKOne.Text = "Insert";
                    break;
                case "save":
                    btnCancelOne.Enabled = true;
                    btnUpdate.Enabled = true;
                    btnOKOne.Text = "Insert";
                    break;

            }

        }

        private int SaveData()
        {
            PSSMETable dt = new PSSMETable();
            dt.ModleID = mMoudleID;
            dt.MenuIsValid = chkValiad.SelectedValue;
            dt.MenuLink = txtLink.Text;
            dt.MenuLinkTarget = txtiFrame.Text;
            dt.MenuName = txtTitleOne.Text;
            dt.ModleCode = txtCodeOne.Text;
            if (lblParentCode.Text.ToString() != "0")
                dt.ModleParentCode = lblParentCode.Text;
            else
                dt.ModleParentCode = "";
            dt.IsProcess = chkIsProcess.SelectedValue;
            dt.MouldNameEn = txtTitleEn.Text;
            if (txtTitleTW.Text == "")
                dt.MouldNameTW = Utils.ToTChinese(txtTitleOne.Text);
            else
                dt.MouldNameTW = txtTitleTW.Text;
            dt.OrderBy = Convert.ToInt16(txtOrderby.Text);
            if (dt.ModleID == 0)
                return DbHelper.GetInstance().CreatePSSME(dt);
            else
                return DbHelper.GetInstance().UpdatePSSME(dt);

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int result = SaveData();
            if (result > 0)
            {
                //如果添加成功则重新加载树型控件 
                LoadTree();
                lblMsg.Text = "修改成功！";

            }
            else if (result == -1)
            {
                lblMsg.Text = "修改失败!";
            }
        }
    }
}
