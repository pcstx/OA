using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyADO;
using System.Data;
using GPRP.GPRPBussiness;
using GPRP.Web.UI;
using GPRP.Entity;

namespace GOA.Basic
{
    public partial class EmployeeInfo : BasePage
    {

        protected void Page_Init(object sender, EventArgs e)
        { 
               string action =Context.Request.Params["action"];
                ViewState["sysPEEBITableColumn"] = DbHelper.GetInstance().GetSysTableColumnByTableName("PEEBI", "", "1");
                DataTable dt = (DataTable)ViewState["sysPEEBITableColumn"];
                BindDropDownList();
                if (action == "show")
                {
                    btnSubmit.Attributes["style"] = "display:block";
                    string EmpId = Context.Request["EmployeeID"];
                    string strScript = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
                   "GetPEEIBIID('" + EmpId + "'); \r\n" +
                  "</script> \r\n";
                    System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strScript", strScript, false);
                    DataTable dtName = DbHelper.GetInstance().GetSysTableByTableName("PEEBITEMP");
                   if (dtName.Rows.Count > 0)
                   {
                       for (int i = 0; i < dtName.Rows.Count; i++)
                       {
                           string szColName = dtName.Rows[i]["ColName"].ToString();   //控件id
                           string szColDescriptionCN = dtName.Rows[i]["ColDescriptionCN"].ToString(); //控件说明
                           string szColType = dtName.Rows[i]["ColType"].ToString(); //控件中要输入值的类型
                           DataTable dt2 = DbHelper.GetInstance().DoGetAddItem(EmpId);
                           if (dt2 != null)
                           {
                               for (int j = 0; j < dt2.Rows.Count; j++)
                               {
                                   string Value = dt2.Rows[j][szColName].ToString();  //控件值
                                   CreateControl(szColName, szColDescriptionCN, szColType, Value);
                               }
                           }
                       }

                    }

                    DataTable EmpDT = DbHelper.GetInstance().GetPEEBIInfoById(EmpId);
                    SetData(EmpDT);
                }
                else if (action == "add")
                {
                    DataTable DtId = DbHelper.GetInstance().GetLastEmplyeeId();
                    if (DtId.Rows.Count > 0)
                    {
                        int NewId =Int32.Parse(DtId.Rows[0]["PEEBIEC"].ToString());
                        NewId += 1;
                        txtPEEBIEC.Text = NewId.ToString();
                    }

                    btnAdd.Attributes["style"] = " display:block";
                    dpPEEBIDT.Items.Insert(0, "--请选择--");
                    dpPEEBIES.Items.Insert(0, "--请选择--");
                    dpPEEBIPC.Items.Insert(0, "--请选择--");
                    dpPEEBIDEP.Items.Insert(0, "--请选择--");
                }

                SetText();

            }

        
     

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }


        private void CreateControl(string szColName, string szColDescriptionCN, string szColType , string Value)
        {
            string FieldDateFormat = "yyyy-MM-dd";
            Label lblField = new Label();
            lblField.ID = "field" + szColName;
            lblField.Text = szColDescriptionCN;
            placeHolder.Controls.Add(lblField);
            GPRP.GPRPControls.TextBox txtField = new GPRP.GPRPControls.TextBox();
            txtField.ID = szColName;
            txtField.Text = Value;
            txtField.Width = new Unit(120);
            placeHolder.Controls.Add(txtField);
            if (szColType == "datetime")
            {
                Literal literialImage = new Literal();
                literialImage.Text = "<img onclick=\"WdatePicker({el:$dp.$('" + txtField.ID + "'),dateFmt:'" + FieldDateFormat + "'})\" src=\"../images/calendar.gif\">";
                literialImage.ID = "fieldImage" + szColName;
                placeHolder.Controls.Add(literialImage);
                txtField.AddAttributes("onfocus", "javascript:WdatePicker({dateFmt:'" + FieldDateFormat + "'})");
            }
        
        
        }

        private void BindDropDownList()
        {
            DataTable dtDT = DbHelper.GetInstance().GetPBPOS("Basic", "");
            if (dtDT.Rows.Count > 0)
            {
                for (int i = 0; i < dtDT.Rows.Count; i++)
                    dpPEEBIPC.Items.Add(new ListItem(dtDT.Rows[i]["PBPOSPN"].ToString(), i.ToString()));   //岗位 

            }

            DataTable dtDep = DbHelper.GetInstance().GetDeptInfor();
            if (dtDep.Rows.Count > 0)
            {
                for (int i = 0; i < dtDep.Rows.Count; i++)
                    dpPEEBIDEP.Items.Add(new ListItem(dtDep.Rows[i]["PBDEPDN"].ToString(), i.ToString()));  //所属部门
            }

            DataTable dtEst = DbHelper.GetInstance().GetPBESTInfo();
            if (dtEst.Rows.Count > 0)
            {
                for (int i = 0; i < dtEst.Rows.Count; i++)
                    dpPEEBIES.Items.Add(new ListItem(dtEst.Rows[i]["PBESTTN"].ToString(), i.ToString()));  //员工状态
            }

            DataTable dtPOS = DbHelper.GetInstance().GetPCPOS();
            if (dtPOS.Rows.Count > 0)
            {
                for (int i = 0; i < dtPOS.Rows.Count; i++)
                    dpPEEBIDT.Items.Add(new ListItem(dtPOS.Rows[i]["PCPOSDN"].ToString(), i.ToString()));   //职位
            }

     
           
           
        }

        private void SetText()
        {
        
            lblPEEBIEC.Text = TextFromSysTableColumn("PEEBIEC", (DataTable)ViewState["sysPEEBITableColumn"]);
            lblPEEBIEN.Text = TextFromSysTableColumn("PEEBIEN", (DataTable)ViewState["sysPEEBITableColumn"]);
            lblPEEBIBD.Text = TextFromSysTableColumn("PEEBIBD", (DataTable)ViewState["sysPEEBITableColumn"]);
            lblPEEBIDEP.Text = TextFromSysTableColumn("PEEBIDEP", (DataTable)ViewState["sysPEEBITableColumn"]);
            lblPEEBIPC.Text = TextFromSysTableColumn("PEEBIPC", (DataTable)ViewState["sysPEEBITableColumn"]);
            lblPEEBIDT.Text = TextFromSysTableColumn("PEEBIDT", (DataTable)ViewState["sysPEEBITableColumn"]);
            lblPEEBIED.Text = TextFromSysTableColumn("PEEBIED", (DataTable)ViewState["sysPEEBITableColumn"]);
            lblPEEBIES.Text = TextFromSysTableColumn("PEEBIES", (DataTable)ViewState["sysPEEBITableColumn"]);
            lblPEEBICP.Text = TextFromSysTableColumn("PEEBICP", (DataTable)ViewState["sysPEEBITableColumn"]);
            lblPEEBIDL.Text = TextFromSysTableColumn("PEEBIDL", (DataTable)ViewState["sysPEEBITableColumn"]);
        }

        private void SetData(DataTable dt)
        { 
            txtPEEBIEC.Text =TetxtFromDataTable("PEEBIEC",dt);
            txtPEEBIEN.Text = TetxtFromDataTable("PEEBIEN", dt);
            txtPEEBIBD.Text = TetxtFromDataTable("PEEBIBD", dt);
            txtPEEBIED.Text = TetxtFromDataTable("PEEBIED", dt);
            txtPEEBICP.Text = TetxtFromDataTable("PEEBICP", dt);
            txtPEEBIDL.Text = TetxtFromDataTable("PEEBIDL", dt);
            dpPEEBIES.SelectedIndex = Int32.Parse(TetxtFromDataTable("PEEBIES", dt));
            dpPEEBIDT.Items.FindByText(TetxtFromDataTable("PEEBIDT", dt)).Selected = true;
            dpPEEBIPC.Items.FindByText(TetxtFromDataTable("PEEBIPC", dt)).Selected = true;
            dpPEEBIDEP.Items.FindByText(TetxtFromDataTable("PEEBIDEP", dt)).Selected = true;


        }


        private string TetxtFromDataTable(string ColName , DataTable dt)
        {
            if (dt == null)
            {
                return "";
            }
            return dt.Rows[0][ColName].ToString();
        }

        //无须更改，因为在page_Load里面已经读出了数据表内容
        private string TextFromSysTableColumn(string ColName, DataTable dt)
        {
            if (dt == null)
            {
                return "";
            }
            string strResult = "";

            //DataTable dt = (DataTable)ViewState["sysTableColumn"];
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

        protected void ButtonHidde_Click(object sender, EventArgs e)
        {
          
        
        
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            PEEBIEntity PEEBIEntity = new PEEBIEntity();
            PEEBIEntity.PEEBIBD = DateTime.Parse(txtPEEBIBD.Text);
            PEEBIEntity.PEEBICP = txtPEEBICP.Text;
            PEEBIEntity.PEEBIDEP = txtPEEBIDEP.Text;
            PEEBIEntity.PEEBIDL = txtPEEBIDL.Text;
            PEEBIEntity.PEEBIDT = dpPEEBIDT.SelectedItem.Text;
            PEEBIEntity.PEEBIEC = txtPEEBIEC.Text;
            PEEBIEntity.PEEBIED = DateTime.Parse(txtPEEBIED.Text);
            PEEBIEntity.PEEBIEN = txtPEEBIEN.Text;
            PEEBIEntity.PEEBIES = dpPEEBIES.SelectedItem.Value;
            PEEBIEntity.PEEBIPC = txtPEEBIPC.Text;
            string szResult = DbHelper.GetInstance().UpdatePEEBI(PEEBIEntity);
          
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        { 

            PEEBIEntity  PEEBIEntity = new PEEBIEntity();
            PEEBIEntity.PEEBIBD = DateTime.Parse(txtPEEBIBD.Text) ;
            PEEBIEntity.PEEBICP = txtPEEBICP.Text;
            PEEBIEntity.PEEBIDEP = txtPEEBIDEP.Text;
            PEEBIEntity.PEEBIDL = txtPEEBIDL.Text;
            PEEBIEntity.PEEBIDT = dpPEEBIDT.SelectedItem.Text;
            PEEBIEntity.PEEBIEC = txtPEEBIEC.Text;
            PEEBIEntity.PEEBIED = DateTime.Parse(txtPEEBIED.Text);
            PEEBIEntity.PEEBIEN = txtPEEBIEN.Text ;
            PEEBIEntity.PEEBIES = dpPEEBIES.SelectedItem.Value;
            PEEBIEntity.PEEBIPC = txtPEEBIPC.Text;
            string szResult= DbHelper.GetInstance().AddPEEBI(PEEBIEntity);
            szResult = DbHelper.GetInstance().AddPEEBITEMP(PEEBIEntity.PEEBIEC);
        }

    }


}
