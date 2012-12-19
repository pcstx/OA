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
using MyADO;
using GPRP.GPRPControls;
using GPRP.GPRPEnumerations;
using GPRP.GPRPBussiness;
using GPRP.Entity;
namespace AJAXWeb.aspx
{
    public partial class _01010 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (DNTRequest.GetString("NUTV") != "1")
                {
                    btnNotUsedToView.Text = "NotUsedToView";
                }
                else
                {
                    btnNotUsedToView.Text = "NotUsedToNotView";
                }

                //获取公司名称    add bear
                //DataTable dtCMP = DbHelper.GetInstance().GetPBCMP("", 1, 10);

                //if (dtCMP.Rows.Count > 0)
                //{
                //    if (dtCMP.Rows[0]["PBCMPCN"].ToString() == "")
                //    {
                //        TreeView1.Nodes[0].Text = "公司";
                //    }
                //    else
                //    {
                //        TreeView1.Nodes[0].Text = dtCMP.Rows[0]["PBCMPCN"].ToString();

                //    }
                
                //}
                TreeView1.Nodes[0].Text = "公司";
                TreeView1.Nodes[0].Value = "";
                //获取要显示的表结构
                DataTable dt = DbHelper.GetInstance().GetSysTableColumnByTableName("PBDEP", "","1");
                ViewState["sysTableColumn"] = dt;
                ViewState["sysTable"] = DbHelper.GetInstance().GetSysTable("PBDEP");
                //设置字段名称
                SetText();
                TreeView1.CollapseAll();
                TreeView1.Nodes[0].Expanded = true;

                EnableOperationByUserRight();
            }
        }

        private void EnableOperationByUserRight()
        {
            //menuID = "101010"; 
            //dtUserRight = DbHelper.GetInstance().GetSpUserRight(menuID, userid);  bear
            //if (Convert.ToInt32(dtUserRight.Rows[0]["ViewRight"]) == 0)
            //{
            //    Response.Redirect("NoPrivilege.aspx");
            //    return;
            //}
        }

        protected void TreeView1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            string NotUsedToView = DNTRequest.GetString("NUTV");
            PBDEPEntity[] deptArray = GetChild(e.Node.Value.ToString(), NotUsedToView);
            if (deptArray.Length > 0)
            {
                foreach (PBDEPEntity dept in deptArray)
                {
                    TreeNode node = new TreeNode();
                    node.Text = dept.DeptName ;
                    node.Value = dept.DeptCode;

                    node.PopulateOnDemand = true;
                    //node.SelectAction = TreeNodeSelectAction.Select;//None;
                    node.SelectAction = TreeNodeSelectAction.None;
                    node.NavigateUrl = dept.DeptCode;
                    e.Node.ChildNodes.Add(node);
                }
            }
           
        }

        private PBDEPEntity[] GetChild(string parentDeptCode, string NotUsedToView)
        {
            DataTable dt = DbHelper.GetInstance().GetChildDeptbyDeptCode(parentDeptCode, NotUsedToView);
            return DbHelper.GetInstance().GetDeptEntityArray(dt);
        }

        private void SetText()
        {
            lblPBDEPDC.Text = TextFromSysTableColumn("PBDEPDC");
            lblPBDEPDN.Text = TextFromSysTableColumn("PBDEPDN");
            lblPBDEPDEN.Text = TextFromSysTableColumn("PBDEPDEN");
            lblPBDEPDTWN.Text = TextFromSysTableColumn("PBDEPDTWN");
            lblPBDEPUS.Text = TextFromSysTableColumn("PBDEPUS");
            lblPBDEPOI.Text = TextFromSysTableColumn("PBDEPOI");
            lblPBDEPPDC.Text = TextFromSysTableColumn("PBDEPPDC");
            //lblTitle.Text = TextFromSysTable("PBCMP");
            //lblBigTitle.Text = TextFromSysTable("PBCMP");
            //lblPageSize.Text = ResourceManager.GetString("lblPageSize");
        }

        private string TextFromSysTable(string TableName)
        {
            if (ViewState["sysTable"] == null)
            {
                return "";
            }
            string strResult = "";
            DataTable dt = (DataTable)ViewState["sysTable"];
            string filer = "TableName='" + TableName + "'";

            DataRow[] dr = dt.Select(filer);
            if (dr.Length > 0)
            {
                switch (WebUtils.GetCookie("Language"))
                {
                    case "zh-CN":
                        strResult = dr[0]["TableDescription"].ToString();
                        break;
                    case "en-US":
                        strResult = dr[0]["TableDescriptionEN"].ToString();
                        break;
                    case "zh-TW":
                        strResult = dr[0]["TableDescriptionTW"].ToString();
                        break;
                    default:
                        strResult = dr[0]["TableDescription"].ToString();
                        break;
                }
            }
            return strResult;
        }
        private string TextFromSysTableColumn(string ColName)
        {
            if (ViewState["sysTableColumn"] == null)
            {
                return "";
            }
            string strResult = "";

            DataTable dt = (DataTable)ViewState["sysTableColumn"];
            string filer = "ColName='" + ColName + "'";

            DataRow[] dr = dt.Select(filer);
            if (dr.Length > 0)
            {
                switch (WebUtils.GetCookie("Language"))
                {
                    case "zh-CN":
                        strResult = dr[0]["ColDescriptionCN"].ToString();
                        break;
                    case "en-US":
                        strResult = dr[0]["ColDescriptionUS"].ToString();
                        break;
                    case "zh-TW":
                        strResult = dr[0]["ColDescriptionTW"].ToString();
                        break;
                    default:
                        strResult = dr[0]["ColDescriptionCN"].ToString();
                        break;
                }
            }

            return strResult;
        }
        private void SetPannelData()//PBCMPEntity _pbcmpEntity)  add bear
        {
            //BindDropDownList(_psacnEntity.PSACNDAT);
            //txtPBCMPCA.Text = _pbcmpEntity.PBCMPCA;
            //txtPBCMPCAN.Text = _pbcmpEntity.PBCMPCAN;
            //txtPBCMPCANEN.Text = _pbcmpEntity.PBCMPCANEN;
            //txtPBCMPCANTW.Text = _pbcmpEntity.PBCMPCANTW;
            //txtPBCMPCE.Text = _pbcmpEntity.PBCMPCC;
            //txtPBCMPCEN.Text = _pbcmpEntity.PBCMPCE;
            //txtPBCMPCI.Text = _pbcmpEntity.PBCMPCI; ;
            //txtPBCMPCN.Text = _pbcmpEntity.PBCMPCN;
            //txtPBCMPCP.Text = _pbcmpEntity.PBCMPCP;
            //txtPBCMPCPH.Text = _pbcmpEntity.PBCMPCPH;
            //txtPBCMPCTWN.Text = _pbcmpEntity.PBCMPCTWN;
            //txtPBCMPLG.Text = _pbcmpEntity.PBCMPLG;
            //txtPBCMPZC.Text = _pbcmpEntity.PBCMPZC;

            ViewState["state"] = "Update";

        }
        private void BindDropDownList(string selectValue)
        {
            //DataTable dt_DateType = DateTypeDataTable();
            //ViewState["PSACNDATtable"] = dt_DateType;
            ////ddlPSACNDAT.AddTableData(dt_DateType, selectValue);

        }
        //操作按钮的处理
        protected void btnBrowseMode_Click(object sender, EventArgs e)
        {
            //GPRP.GPRPControls.Button btn = (GPRP.GPRPControls.Button)sender;
            ////UCOperationBanner1.ButtnEnable(ClientID);
            ////System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
            
            //switch (btn.ID)
            //{
            //    case "btnBrowseMode":
                   
            //        break;
            //    case "btnEditMode":
                    
            //        txtPBDEPDN.ReadOnly = false;
            //        txtPBDEPDTWN.ReadOnly = false;
            //        txtPBDEPDEN.ReadOnly = false;
            //        txtPBDEPOI.ReadOnly = false;
            //        chkPBDEPUS.Enabled = true;
            //        break;
            //    case "btnAddDept":

            //        string deptCode = TreeView1.SelectedValue;
            //        PBDEPEntity DeptEntity = DatabaseProvider.GetInstance().GetPBDEPEntityByKeyCol(deptCode);
            //        txtPBDEPPDC.Text = DeptEntity.ParentDeptCode;
            //        txtPBDEPDC.Text = "";
            //        txtPBDEPDN.ReadOnly = false;
            //        txtPBDEPDTWN.ReadOnly = false;
            //        txtPBDEPDEN.ReadOnly = false;
            //        txtPBDEPOI.ReadOnly = false;
            //        chkPBDEPUS.Enabled = true;
            //        txtPBDEPDN.Text = "";
            //        txtPBDEPDTWN.Text = "";
            //        txtPBDEPDEN.Text = "";
            //        chkPBDEPUS.Checked = true;
            //        //btnDel.Disable = true;
            //        btnDel.Enabled = false;
            //        break;
            //    case "btnAddChildDept":
            //        //txtPBDEPPDC.Text = txtPBDEPDC.Text.ToString();
            //        txtPBDEPPDC.Text = TreeView1.SelectedValue;
            //        txtPBDEPDC.Text = "";
            //        txtPBDEPDN.ReadOnly = false;
            //        txtPBDEPDTWN.ReadOnly = false;
            //        txtPBDEPDEN.ReadOnly = false;
            //        txtPBDEPOI.ReadOnly = false;
            //        chkPBDEPUS.Enabled = true;
            //        txtPBDEPDN.Text = "";
            //        txtPBDEPDTWN.Text = "";
            //        txtPBDEPDEN.Text = "";
            //        chkPBDEPUS.Checked = true;
            //        //btnDel.Disable = true;
            //        btnDel.Enabled = false;
            //        break;
            //    case "btnSubmit":

            //        Page.RegisterStartupScript("Test", "<script language='javascript'>$('#btnModify').click();alert('OK')</script>");
            //        break;
            //    case "btnDel":
            //        //BrowseEditMode mode = ((BrowseEditMode)ViewState["Mode"]);
            //        //if (mode == BrowseEditMode.Browse)
            //        //{
            //        //    string sID = "";
            //        //    for (int i = 0; i < GridView1.Rows.Count; i++)
            //        //    {
            //        //        GridViewRow row = GridView1.Rows[i];
            //        //        CheckBox check = (CheckBox)row.FindControl("item");
            //        //        if (check.Checked)
            //        //        {
            //        //            sID += row.Cells[4].Text + ",";
            //        //        }
            //        //    }
            //        //    if (sID.Length > 0)
            //        //    {
            //        //        //delete the record that you  choose
            //        //        //DatabaseProvider.GetInstance().DeletePBEDU(sID);
            //        //        //refresh the edit mode gridview ui
            //        //        GridView1EditUI(BrowseEditMode.Edit);//
            //        //        //refresh the datasource.
            //        //        BindGridView();
            //        //    }
            //        //}
            //        break;
            //    case "btnImportExcel":
            //        //word
            //        break;
            //    case "btnExportExcel":
            //        //excel
            //        break;
            //}
        }
        protected void SelectDeptChange(Object sender, EventArgs e)
        {
            //string deptCode = TreeView1.SelectedValue;
            //PBDEPEntity DeptEntity = DatabaseProvider.GetInstance().GetPBDEPEntityByKeyCol(deptCode);
            //this.txtPBDEPDC.Text = DeptEntity.DeptCode;
            //this.txtPBDEPDN.Text = DeptEntity.DeptName;
            //this.txtPBDEPDEN.Text = DeptEntity.DeptEName;
            //this.txtPBDEPDTWN.Text = DeptEntity.DeptTWName;
            //this.txtPBDEPOI.Text = DeptEntity.DeptOrderItem.ToString();
            //this.txtPBDEPPDC.Text = DeptEntity.ParentDeptCode.ToString();
            //if (DeptEntity.DeptIsValid == "1")
            //{
            //    this.chkPBDEPUS.Checked = true;
            //}
            //else
            //{
            //    this.chkPBDEPUS.Checked = false;
            //}
            
        }

        protected void btnNotUsedToView_Click(object sender, EventArgs e)
        {
            string nextNUTV;
            if (DNTRequest.GetString("NUTV") != "1")
            {
                nextNUTV = "1";
            }
            else
            {
                nextNUTV = "0";
            }
            Response.Redirect("101010.aspx?NUTV=" + nextNUTV);
        }

        


    }
}
