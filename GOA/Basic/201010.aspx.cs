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
using System.Text;
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
    public partial class _010101 : BasePage
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            { 
                // string MyKeyStr = "";
               //  ClientScript.RegisterClientScriptBlock(this.GetType(), null, "<script language=\'javascript\'> var MyKeyStr=\'" + MyKeyStr + "\'</script>");
              
                enableOperationByUserRight();
       
                if(Session["010101PageSize"] ==null)
                    Session["010101PageSize"] = config.PageSize;//ÿҳ��ʾ��Ĭ��ֵ

                //if (Session["GroupId"].ToString() == "2") //ϵͳ����Ա
                //{
                //   // Button5.Attributes["style"] = " display:block";
                //}
                //else
                //{
                //   // btnSubmit.Attributes["style"] = " display:block";
                //}
              
             
                txtPageSize.Text = Session["010101PageSize"].ToString();
                AspNetPager1.PageSize = Convert.ToInt32(Session["010101PageSize"]);
               
                //�޸ı���,��ȡҪ��ʾ�ı�ṹ,

                ViewState["sysPEEBITableColumn"] = DbHelper.GetInstance().GetSysTableColumnByTableName("PEEBI", "","1");
               // ViewState["sysPEEBITable"] = DbHelper.GetInstance().GetSysTable("PEEBI");
                ViewState["sysPEEBITableColumnTEMP"] = DbHelper.GetInstance().GetSysTableColumnByTableName("PEEBITEMP", "","1");
                ViewState["sysPEEBITableColumnTEMP0"] = DbHelper.GetInstance().GetSysTableColumnByTableName("PEEBITEMP", "", "0");
                ViewState["sysPEEBITableColumnTEMP1"] = DbHelper.GetInstance().GetSysTableColumnByTableName("PEEBITEMP", "", "1");
                ViewState["g_sysPEEBITableColumnTEMP0"] = ViewState["sysPEEBITableColumnTEMP0"];
                ViewState["g_sysPEEBITableColumnTEMP1"] = ViewState["sysPEEBITableColumnTEMP1"];

                ViewState["sysPEEBITableColumnTEMPAdd"] = new DataTable();
               // ViewState["sysPEEBITableTEMP"] = DbHelper.GetInstance().GetSysTable("PEEBITEMP");
                //ViewState["sysPEEEITableColumn"] = DbHelper.GetInstance().GetSysTableColumnByTableName("PEEEI", "");
                //ViewState["sysPEEEITable"] = DatabaseProvider.GetInstance().GetSysTable("PEEEI");
                //ViewState["sysPEEWETableColumn"] = DatabaseProvider.GetInstance().GetSysTableColumnByTableName("PEEWE", "");
                //ViewState["sysPEEWETable"] = DatabaseProvider.GetInstance().GetSysTable("PEEWE");
                //ViewState["sysPEESRTableColumn"] = DatabaseProvider.GetInstance().GetSysTableColumnByTableName("PEESR", "");
                //ViewState["sysPEESRTable"] = DatabaseProvider.GetInstance().GetSysTable("PEESR");
                //ViewState["sysPEECITableColumn"] = DatabaseProvider.GetInstance().GetSysTableColumnByTableName("PEECI", "");
                //ViewState["sysPEECITable"] = DatabaseProvider.GetInstance().GetSysTable("PEECI");
                //��Pannel����Ҫ�󶨵Ŀؼ�����������Ϊ�����GridView��ת����ʱ���ṩ����Դ��
                //�������panel�ڵ���
                BindDropDownList("");
                //������������
                //GenerateDeptTree();
                //treeDeptartment.CollapseAll();
                //treeDeptartment.Nodes[0].Expanded = true;
                //����gridView�Ľ��������
                if (ViewState["Mode"] == null) 
                    ViewState["Mode"] = BrowseEditMode.Browse;

                GridView1BrowseUI(BrowseEditMode.Browse);//�б�

                if (Session["010101SearchParameter"] != null)
                {   ViewState["SearchParameter"] = Session["010101SearchParameter"];}
                else
                {  GetSearchParameter();
                }

                if (Session["010101AspNetPageCurPage"] == null)
                {
                    Session["010101AspNetPageCurPage"] = 1;
                    BindGridView();
                }
                else
                {
                    BindGridView();
                    AspNetPager1.CurrentPageIndex = (int)Session["010101AspNetPageCurPage"];
                }
               
                //����ҳ������
               SetText();

            }
            //BindDropDownListTree();
            //System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }
        //����ÿҳ��ʾ��¼����������ģ����Ҫ����Ĭ�ϵ�ҳ��¼������config/geneal.config�и��� PageSize
        protected void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            if (txtPageSize.Text == "" || Convert.ToInt32(txtPageSize.Text) == 0)
            {
                Session["010101PageSize"] = config.PageSize;//ÿҳ��ʾ��Ĭ��ֵ
            }
            else
            {
                Session["010101PageSize"] = Convert.ToInt32(txtPageSize.Text);
            }
            AspNetPager1.PageSize = Convert.ToInt32(Session["010101PageSize"]);
            //�ٽ��а�һ��
            BrowseEditMode mode = ((BrowseEditMode)ViewState["Mode"]);
          
            BindGridView();

            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }
        #region gridView �¼�
        protected void GridView1_RowCreated(Object sender, GridViewRowEventArgs e)
        {
            // The GridViewCommandEventArgs class does not contain a 
            // property that indicates which row's command button was
            // clicked. To identify which row's button was clicked, use 
            // the button's CommandArgument property by setting it to the 
            // row's index.
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
       
            }
        }
        //1������key�ֶ���GridView�е���һ�У�Ĭ�϶��ǵ�5�У���һ����Ϊbutton ������ΪcheckBox ������Ϊedit�ֶΣ������� �ؼ�����
        //2��ͨ���ؼ��ֻ�ȡ������¼��
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
             
                int index = Convert.ToInt32(e.CommandArgument);   //��ȡ�к�
                GridViewRow row = GridView1.Rows[index];       //���ڵ���
                //��һ�����޸�λ��
                string keyCol = row.Cells[2].Text.ToString();
                //�ڶ������޸�λ��
                //PEEBIEntity _PEEBIEntity = new PEEBIEntity();
                //_PEEBIEntity = DatabaseProvider.GetInstance().GetPEEBIEntityByKeyCol(keyCol);
                //if (_PEEBIEntity != null) SetPannelData(_PEEBIEntity);
                Session["010101SearchParameter"] = ViewState["SearchParameter"];
                Session["010101AspNetPageCurPage"] =AspNetPager1.CurrentPageIndex;
               
            //add bear    Response.Redirect("20101001.aspx?EmployeeID=" + keyCol);
                Response.Redirect("EmployeeInfo.aspx?action=show&EmployeeID=" + keyCol);
             

            }
        }
        //����Ҫ����dorpdownlist/chk�ؼ���ת��
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //��Ϊ�༭ģʽ������£�Ҫ�����������ת��
                //�ٽ��а�һ��
                //// Retrieve the LinkButton control from the first column.
                //System.Web.UI.WebControls.Button addButton = (System.Web.UI.WebControls.Button)e.Row.Cells[4].Controls[0];
                //// Set the LinkButton's CommandArgument property with the
                //// row's index.
                //addButton.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
        protected void GridView2_RowCreated(Object sender, GridViewRowEventArgs e)
        {
            // The GridViewCommandEventArgs class does not contain a 
            // property that indicates which row's command button was
            // clicked. To identify which row's button was clicked, use 
            // the button's CommandArgument property by setting it to the 
            // row's index.
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }
        //�༭״̬�µ�GridViewһ�㲻���õ��÷���
        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        //����Ҫ����dorpdownlist/chk�ؼ���ת��
        //��Ҫ���и���
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
        }

        protected void chkValid_CheckedChanged(object sender, EventArgs e)
       {
         CheckBox checkbox = (CheckBox)sender;
         GridViewRow row = (GridViewRow)checkbox.NamingContainer;
         //int index = row.RowIndex;  ��ȡ�������
         string szMykeyCol = "";
         string szAllkeyCol = "";
         int iCount = 0;
         for (int i = 0; i < GridView1.Rows.Count; i++)
         {
             if (((CheckBox)GridView1.Rows[i].FindControl("item")).Checked == true)
             {
                 iCount++;
                 szMykeyCol = GridView1.Rows[i].Cells[2].Text.ToString();
                 if (iCount == 1)
                     szAllkeyCol = szMykeyCol;
                 else if (iCount == 2)
                     szAllkeyCol = szAllkeyCol + '|' + szMykeyCol + '|';
                 else
                    szAllkeyCol = szAllkeyCol + szMykeyCol + '|';
             }
         }
            
            string strScript = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
                 "GetPEEIBIID('" + szAllkeyCol + "'); \r\n" +
                "</script> \r\n";
             System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strScript", strScript, false);
             string strScript2 = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
                "GetPEEBITEMP(); \r\n" +
               "</script> \r\n";
             System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strScript2", strScript2, false);

    
       
       }
        #endregion
        #region aspnetPage ��ҳ����
        //�����������
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            
            BindGridView();
           
        }
        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            Session["010101AspNetPageCurPage"] = e.NewPageIndex;
        }
        #endregion




        #region gridview UI
      

        //������Ҫ���ģ���Ҫ�Ǹ��Ļ�ȡ����Դ�ķ���
        private void BindGridView()
        {
            //GetSearchParameter();
           // DataTable dt = DbHelper.GetInstance().sp_GetPEEBINameList((ArrayList)ViewState["SearchParameter"], AspNetPager1.PageSize, (int)Session["010101AspNetPageCurPage"]);
            DataTable dt = DbHelper.GetInstance().sp_GetPEEBINameAllList((ArrayList)ViewState["SearchParameter"], AspNetPager1.PageSize, (int)Session["010101AspNetPageCurPage"]);
            //DataTable dt = DbHelper.GetInstance().GetDBRecord("", "a.*,b.PBDEPDN,c.PBPOSPN,d.PBDUTDN,e.PBNATNN", "PEEBI a,PBDEP b,PBPOS c,PBDUT d,PBNAT e", "a.PEEBIDEP=b.PBDEPDC and a.PEEBIPC=c.PBPOSPC and a.PEEBIDT=d.PBDUTDC and a.PEEBIENA=e.PBNATNC", "PEEBIEC", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            string s = "";
            if (dt.Rows.Count > 0)
            {
                AspNetPager1.RecordCount = Convert.ToInt32(dt.Rows[0]["RecordCount"]);
                s = "����������¼���:";
                s += "�ܼ�¼Ϊ:" + AspNetPager1.RecordCount.ToString();
                //s += "��ʽԱ����:" + dt.Select("PEEBIES='01'").Length.ToString() +";";
                //s += "������Ա����:" + dt.Select("PEEBIES='03'").Length.ToString() + ";";
                //s += "��ְԱ����:" + dt.Select("PEEBIES='02'").Length.ToString() + ";";
            }
            else
            {
                AspNetPager1.RecordCount = 0;
            }
            StatisticDate.InnerHtml = s;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            
            BuildNoRecords(GridView1, dt);


        }
        //Show Header/Footer of Gridview with Empty Data Source 
        public void BuildNoRecords(GridView gridView, DataTable ds)
        {
            try
            {
                if (ds.Rows.Count == 0)
                {
                    ds.Rows.Add(ds.NewRow());
                    gridView.DataSource = ds;
                    gridView.DataBind();
                    int columnCount = gridView.Rows[0].Cells.Count;
                    gridView.Rows[0].Cells.Clear();
                    gridView.Rows[0].Cells.Add(new TableCell());
                    gridView.Rows[0].Cells[0].ColumnSpan = columnCount;
                    gridView.Rows[0].Cells[0].Text = "No Records Found.";
                }
            }
            catch
            {
            }
        }
        //gridViewUI��ʾ
        private void GridView1BrowseUI(BrowseEditMode Mode)
        {
            //ѡҪ��sysTable���������Щ�ֶ�Ҫ��ʾ����
            if (ViewState["sysPEEBITableColumn"] == null)
            {
                return;
            }
            DataTable dt = (DataTable)ViewState["sysPEEBITableColumn"];
            if (ViewState["sysPEEBITableColumnTEMP"] == null)
            {
                return;
            }
            DataTable dt2 = (DataTable)ViewState["sysPEEBITableColumnTEMP"];
            if (ViewState["sysPEEBITableColumnTEMPAdd"] == null)
            {
                return;
            }
            DataTable dt3 = (DataTable)ViewState["sysPEEBITableColumnTEMPAdd"];
            int iCount = 1;
            for (int i = 0; i < 3; i++)
            {
                DataTable dtCopy = new DataTable();
                switch (i)
                {
                    case 0:
                        dtCopy = dt.Copy();
                        break;
                    case 1:
                        dtCopy = dt2.Copy();
                        iCount = 0;
                        break;
                    case 2:
                        dtCopy = dt3.Copy();
                        iCount = 0;
                        break;
                }
                if (dtCopy != null)
                {
                    if (dtCopy.Rows.Count > 0)
                    {
                        int gvWidth = 25;
                        if (iCount == 1)
                        {
                            ButtonField btnSel = new ButtonField();
                            btnSel.Text = "��ϸ";
                            btnSel.CommandName = "select";
                            btnSel.ShowHeader = true;
                            btnSel.HeaderText = "��ϸ";
                            btnSel.ButtonType = ButtonType.Button;
                            btnSel.ItemStyle.Width = 25;
                            GridView1.Columns.Add(btnSel);
                            iCount = 0;
                        }
                        foreach (DataRow dr in dtCopy.Select(""))
                        {
                            if (dr["ColIsShow"] != null)
                            {
                                if (dr["ColIsShow"].ToString() == "1")
                                {
                                    if (Mode == BrowseEditMode.Browse)
                                    {
                                        switch (dr["ColType"].ToString())
                                        {
                                            case "CheckBox":
                                                VBControls.VBProject.WebControls.TBCheckBoxField mCheckBoxField = new VBControls.VBProject.WebControls.TBCheckBoxField();
                                                switch (WebUtils.GetCookie("Language"))
                                                {
                                                    case "zh-CN":
                                                        mCheckBoxField.HeaderText = dr["ColDescriptionCN"].ToString();
                                                        break;
                                                    case "en-US":
                                                        mCheckBoxField.HeaderText = dr["ColDescriptionUS"].ToString();
                                                        break;
                                                    case "zh-TW":
                                                        mCheckBoxField.HeaderText = dr["ColDescriptionTW"].ToString();
                                                        break;
                                                    default:
                                                        mCheckBoxField.HeaderText = dr["ColDescriptionCN"].ToString();
                                                        break;
                                                }
                                                mCheckBoxField.DataField = dr["ColName"].ToString();
                                                mCheckBoxField.DefineValue = "1/0";
                                                mCheckBoxField.ItemStyle.Width = (dr["ColWidth"] == null) ? 50 : Convert.ToInt32(dr["ColWidth"].ToString());
                                                GridView1.Columns.Add(mCheckBoxField);
                                                break;
                                            default:
                                                BoundField mBoundField = new BoundField();
                                                switch (WebUtils.GetCookie("Language"))
                                                {
                                                    case "zh-CN":
                                                        mBoundField.HeaderText = dr["ColDescriptionCN"].ToString();
                                                        break;
                                                    case "en-US":
                                                        mBoundField.HeaderText = dr["ColDescriptionUS"].ToString();
                                                        break;
                                                    case "zh-TW":
                                                        mBoundField.HeaderText = dr["ColDescriptionTW"].ToString();
                                                        break;
                                                    default:
                                                        mBoundField.HeaderText = dr["ColDescriptionCN"].ToString();
                                                        break;
                                                }

                                                if (dr["TypeName"].ToString() == "smalldatetime" || dr["TypeName"].ToString() == "datetime")
                                                {
                                                    mBoundField.HtmlEncode = false;
                                                    mBoundField.DataFormatString = "{0:d}";
                                                }

                                                mBoundField.DataField = dr["ColName"].ToString();
                                                mBoundField.ItemStyle.Width = dr["ColWidth"] == null ? 100 : Convert.ToInt32(dr["ColWidth"].ToString());
                                                gvWidth += Convert.ToInt16(mBoundField.ItemStyle.Width.Value);
                                                GridView1.Columns.Add(mBoundField);
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                        if (gvWidth > 650) { GridView1.Width = gvWidth; }
                        else { GridView1.Width = Unit.Percentage(100); }
                    }
                    else { GridView1.Width = Unit.Percentage(100); }
                }
                else { GridView1.Width = Unit.Percentage(100); }

            }
          


        }
 
        
        #endregion
        #region pannel
        //����һ�㲻��Ҫ���ģ��Ƕ����ֶ�ǰ������
        private void SetText()
        {
            lblqPEEBIEC.Text = TextFromSysTableColumn("PEEBIEC", (DataTable)ViewState["sysPEEBITableColumn"]);
            lblqPEEBIEN.Text = TextFromSysTableColumn("PEEBIEN", (DataTable)ViewState["sysPEEBITableColumn"]);
            lblqPEEBIBD.Text = TextFromSysTableColumn("PEEBIBD", (DataTable)ViewState["sysPEEBITableColumn"]);
            lblqPEEBIDEP.Text = TextFromSysTableColumn("PEEBIDEP", (DataTable)ViewState["sysPEEBITableColumn"]);
            lblqPEEBIPC.Text = TextFromSysTableColumn("PEEBIPC", (DataTable)ViewState["sysPEEBITableColumn"]);
            lblqPEEBIDT.Text = TextFromSysTableColumn("PEEBIDT", (DataTable)ViewState["sysPEEBITableColumn"]);
            lblqPEEBIED.Text = TextFromSysTableColumn("PEEBIED", (DataTable)ViewState["sysPEEBITableColumn"]);
            lblqPEEBIES.Text = TextFromSysTableColumn("PEEBIES", (DataTable)ViewState["sysPEEBITableColumn"]);
            lblBigTitle.Text = TextFromSysTable("PEEBI", (DataTable)ViewState["sysPEEBITable"]);          
            lblPageSize.Text = ResourceManager.GetString("lblPageSize");
            lblSearchTitle.Text = ResourceManager.GetString("lblSearchTitle");

        }
        //������ģ���Ϊ��page_Load�����Ѿ����������ݱ�����
        private string TextFromSysTable(string TableName, DataTable dt)
        {
            if (dt == null)
            {
                return "";
            }
            string strResult = "";
            //DataTable dt = (DataTable)ViewState["sysTable"];
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
                       // strResult = dr[0]["TableDescription"].ToString();
                        break;
                }
            }
            return strResult;
        }
        //������ģ���Ϊ��page_Load�����Ѿ����������ݱ�����
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
        

     
        #endregion
        #region pannel 's dropdownlist
        //������Ҫ��dropdownlist�ĳ�ʼ��
        private void BindDropDownList(string selectValue)
        {
            // add bear
            //�Ա�
            //DataTable dt_Sex = DbHelper.GetInstance().GetPBSEX("", ""); //SexDataTable();
            //ViewState["Sextable"] = dt_Sex;
            ////dpqPEEBIEG.AddTableData(dt_Sex, 0, 1, true, "Null");

            ////����״�� --PBMAR
            //DataTable dtPBMAR = DbHelper.GetInstance().GetPBMAR("", "");
            //ViewState["PBMARtable"] = dtPBMAR;

            ////Ա������-PBNAT
            //DataTable dtPBNAT = DbHelper.GetInstance().GetPBNAT("", "");
            //ViewState["PBNATtable"] = dtPBNAT;

            //Ա����������-PBNAT
            //DataTable dtPBRET = DbHelper.GetInstance().GetPBRET("", "");
            //ViewState["PBRETtable"] = dtPBRET;

            //��λ --PBPOS
            DataTable dtPBPOS = DbHelper.GetInstance().GetPBPOS("", "");
            ViewState["PBPOStable"] = dtPBPOS;

            //ְ�� -- PEEBI
            DataTable dtPBDUT = DbHelper.GetInstance().GetPEEBI("", "");
            ViewState["PBDUTtable"] = dtPBDUT;

            ////ѧ�� --PBEDU
            //DataTable dtPBEDU = DbHelper.GetInstance().GetPBEDU("", "");
            //ViewState["PBEDUtable"] = dtPBEDU;

            //��ѧרҵ -- PBPRO
            //DataTable dtPBPRO = DbHelper.GetInstance().GetPBPRO("", "");
            //ViewState["PBPROtable"] = dtPBPRO;

            ////��λ����  -- PBPOT
            //DataTable dtPBPOT = DbHelper.GetInstance().GetPBPOT("", "");
            //ViewState["PBPOTtable"] = dtPBPOT;

            ////Ա����Դ�����Ϣ  --PBESE
            //DataTable dtPBESE = DbHelper.GetInstance().GetPBESE("", "");
            //ViewState["PBESEtable"] = dtPBESE;

            ////Ա��״̬�����Ϣ  --PBEST
            DataTable dtPBEST = DbHelper.GetInstance().GetPBEST("", "");
            ViewState["PBESTtable"] = dtPBEST;

            ////Ա���������  --PBEWT
            //DataTable dtPBEWT = DbHelper.GetInstance().GetPBEWT("", "");
            //ViewState["PBEWTtable"] = dtPBEWT;

            //���֤��Ч����--PEEBIVY
            //DataTable dtPEEBIVY = PEEBIVYDataTable();
            //ViewState["PEEBIVYtable"] = dtPEEBIVY;

            //�����·�--PEEBIIBDMONTH
            //DataTable dtPEEBIIBDMONTH = PEEBIIBDMONTHDataTable();
            //ViewState["PEEBIIBDMONTHtable"] = dtPEEBIIBDMONTH;
            switch (WebUtils.GetCookie("Language"))
            {
                case "zh-CN":
                    //dpqPEEBIEG.AddTableData(dt_Sex, 0, 1, true, "Null");
                    //dpqPEEBIMI.AddTableData(dtPBMAR, 0, 1, true, "Null");
                    //dpqPEEBIENA.AddTableData(dtPBNAT, 0, 1, true, "Null");
                    //dpqPEEBIRT.AddTableData(dtPBRET, 0, 1, true, "Null");
                    //dpqPEEBIPC.AddTableData(dtPBPOS, 0, 1, true, "Null");
                    //dpqPEEBIPT.AddTableData(dtPBPOT, 0, 1, true, "Null");
                    //dpqPEEBIEL.AddTableData(dtPBEDU, 0, 1, true, "Null");
                    //dpqPEEBILM.AddTableData(dtPBPRO, 0, 1, true, "Null");
                    //dpqPEEBIDT.AddTableData(dtPBDUT, 0, 1, true, "Null");
                    //dpqPEEBIWT.AddTableData(dtPBEWT, 0, 1, true, "Null");
                    //dpqPEEBIEST.AddTableData(dtPBESE, 0, 1, true, "Null");
                    //dpqPEEBIES.AddTableData(dtPBEST, 0, 1, true, "Null");
                    //dpqPEEBIVY.AddTableData(dtPEEBIVY, 0, 1, true, "Null");
                   // dpqPEEBIIBDMONTH.AddTableData(dtPEEBIIBDMONTH, 0, 1, true, "Null");
                    break;
                case "en-US":
                   // dpqPEEBIEG.AddTableData(dt_Sex, 0, 2, true, "Null");
                    //dpqPEEBIMI.AddTableData(dtPBMAR, 0, 2, true, "Null");
                    //dpqPEEBIENA.AddTableData(dtPBNAT, 0, 2, true, "Null");
                    //dpqPEEBIRT.AddTableData(dtPBRET, 0, 2, true, "Null");
                    //dpqPEEBIPC.AddTableData(dtPBPOS, 0, 2, true, "Null");
                    //dpqPEEBIPT.AddTableData(dtPBPOT, 0, 2, true, "Null");
                    //dpqPEEBIEL.AddTableData(dtPBEDU, 0, 2, true, "Null");
                    //dpqPEEBILM.AddTableData(dtPBPRO, 0, 2, true, "Null");
                    //dpqPEEBIDT.AddTableData(dtPBDUT, 0, 2, true, "Null");
                    //dpqPEEBIWT.AddTableData(dtPBEWT, 0, 2, true, "Null");
                    //dpqPEEBIEST.AddTableData(dtPBESE, 0, 2, true, "Null");
                    //dpqPEEBIES.AddTableData(dtPBEST, 0, 2, true, "Null");
                    //dpqPEEBIVY.AddTableData(dtPEEBIVY, 0, 1, true, "Null");
                    //dpqPEEBIIBDMONTH.AddTableData(dtPEEBIIBDMONTH, 0, 1, true, "Null");
                    break;
                case "zh-TW":
                    //dpqPEEBIEG.AddTableData(dt_Sex, 0, 3, true, "Null");
                    //dpqPEEBIMI.AddTableData(dtPBMAR, 0, 3, true, "Null");
                    //dpqPEEBIENA.AddTableData(dtPBNAT, 0, 3, true, "Null");
                    //dpqPEEBIRT.AddTableData(dtPBRET, 0, 3, true, "Null");
                    //dpqPEEBIPC.AddTableData(dtPBPOS, 0, 3, true, "Null");
                    //dpqPEEBIPT.AddTableData(dtPBPOT, 0, 3, true, "Null");
                    //dpqPEEBIEL.AddTableData(dtPBEDU, 0, 3, true, "Null");
                    //dpqPEEBILM.AddTableData(dtPBPRO, 0, 3, true, "Null");
                    //dpqPEEBIDT.AddTableData(dtPBDUT, 0, 3, true, "Null");
                    //dpqPEEBIWT.AddTableData(dtPBEWT, 0, 3, true, "Null");
                   // dpqPEEBIEST.AddTableData(dtPBESE, 0, 3, true, "Null");
                    //dpqPEEBIES.AddTableData(dtPBEST, 0, 3, true, "Null");
                    //dpqPEEBIVY.AddTableData(dtPEEBIVY, 0, 1, true, "Null");
                    //dpqPEEBIIBDMONTH.AddTableData(dtPEEBIIBDMONTH, 0, 1, true, "Null");
                    break;
                default:
                    //dpqPEEBIEG.AddTableData(dt_Sex, 0, 1, true, "Null");
                   // dpqPEEBIMI.AddTableData(dtPBMAR, 0, 1, true, "Null");
                    //dpqPEEBIENA.AddTableData(dtPBNAT, 0, 1, true, "Null");
                    //dpqPEEBIRT.AddTableData(dtPBRET, 0, 1, true, "Null");
                    //dpqPEEBIPC.AddTableData(dtPBPOS, 0, 1, true, "Null");
                    //dpqPEEBIPT.AddTableData(dtPBPOT, 0, 1, true, "Null");
                    //dpqPEEBIEL.AddTableData(dtPBEDU, 0, 1, true, "Null");
                    //dpqPEEBILM.AddTableData(dtPBPRO, 0, 1, true, "Null");
                    //dpqPEEBIDT.AddTableData(dtPBDUT, 0, 1, true, "Null");
                    //dpqPEEBIWT.AddTableData(dtPBEWT, 0, 1, true, "Null");
                    //dpqPEEBIEST.AddTableData(dtPBESE, 0, 1, true, "Null");
                    //dpqPEEBIES.AddTableData(dtPBEST, 0, 1, true, "Null");
                    //dpqPEEBIVY.AddTableData(dtPEEBIVY, 0, 1, true, "Null");
                    //dpqPEEBIIBDMONTH.AddTableData(dtPEEBIIBDMONTH, 0, 1, true, "Null");
                    break;
            }

        }
        private void BindDropDownListTree()
        {
            //��������--3.	PBDEP

            DataTable dtPBDEP = null;
            dpqPEEBIDEP.ParentID = "PBDEPPDC";
            if (ViewState["PBDEPtable"] == null)
            {
                dtPBDEP = DbHelper.GetInstance().GetDeptInforbyDeptName("1", "");
                ViewState["PBDEPtable"] = dtPBDEP;
            }
            else
            {
                dtPBDEP = (DataTable)ViewState["PBDEPtable"];
            }
            switch (WebUtils.GetCookie("Language"))
            {
                case "zh-CN":
                    dpqPEEBIDEP.BuildTree(dtPBDEP, 0, 1, "", true, "Null");
                    break;
                case "en-US":
                    dpqPEEBIDEP.BuildTree(dtPBDEP, 0, 2, "", true, "Null");
                    break;
                case "zh-TW":
                    dpqPEEBIDEP.BuildTree(dtPBDEP, 0, 3, "", true, "Null");
                    break;
                default:
                    dpqPEEBIDEP.BuildTree(dtPBDEP, 0, 1, "", true, "Null");
                    break;
            }

        }
        //����Ϊdropdownlist����ġ���Ϊ��Щ����Դû��д�뵽���ݿ��У����ǱȽϹ̶������ݡ�
        private DataTable SexDataTable()
        {
            DataTable dt_ChkList = new DataTable();

            dt_ChkList.Columns.Add(new DataColumn("ID", typeof(System.String)));
            dt_ChkList.Columns.Add(new DataColumn("Text", typeof(System.String)));

            DataRow dr_TopMenu;

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "2";
            dr_TopMenu[1] = ResourceManager.GetString("Num_FeMale");
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "1";
            dr_TopMenu[1] = ResourceManager.GetString("Num_Male");
            dt_ChkList.Rows.Add(dr_TopMenu);


            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "9";
            dr_TopMenu[1] = ResourceManager.GetString("Num_NotSure");
            dt_ChkList.Rows.Add(dr_TopMenu);
            return dt_ChkList;
        }
        //����Ϊdropdownlist����ġ���Ϊ��Щ����Դû��д�뵽���ݿ��У����ǱȽϹ̶������ݡ�
        private DataTable PEEBIVYDataTable()
        {
            DataTable dt_ChkList = new DataTable();

            dt_ChkList.Columns.Add(new DataColumn("ID", typeof(System.String)));
            dt_ChkList.Columns.Add(new DataColumn("Text", typeof(System.String)));

            DataRow dr_TopMenu;
            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "0";
            dr_TopMenu[1] = ResourceManager.GetString("ZeroMonth");
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "30";
            dr_TopMenu[1] = ResourceManager.GetString("OneMonth");
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "60";
            dr_TopMenu[1] = ResourceManager.GetString("TwoMonth");
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "90";
            dr_TopMenu[1] = ResourceManager.GetString("ThreeMonth");
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "180";
            dr_TopMenu[1] = ResourceManager.GetString("SixMonth");
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "365";
            dr_TopMenu[1] = ResourceManager.GetString("OneYear");
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "730";
            dr_TopMenu[1] = ResourceManager.GetString("TwoYear");
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "1095";
            dr_TopMenu[1] = ResourceManager.GetString("ThreeYear");
            dt_ChkList.Rows.Add(dr_TopMenu);

            return dt_ChkList;
        }
        private DataTable PEEBIIBDMONTHDataTable()
        {
            DataTable dt_ChkList = new DataTable();

            dt_ChkList.Columns.Add(new DataColumn("ID", typeof(System.String)));
            dt_ChkList.Columns.Add(new DataColumn("Text", typeof(System.String)));

            DataRow dr_TopMenu;
            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "1";
            dr_TopMenu[1] = ResourceManager.GetString("January");
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "2";
            dr_TopMenu[1] = ResourceManager.GetString("February");
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "3";
            dr_TopMenu[1] = ResourceManager.GetString("March");
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "4";
            dr_TopMenu[1] = ResourceManager.GetString("April");
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "5";
            dr_TopMenu[1] = ResourceManager.GetString("May");
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "6";
            dr_TopMenu[1] = ResourceManager.GetString("June");
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "7";
            dr_TopMenu[1] = ResourceManager.GetString("July");
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "8";
            dr_TopMenu[1] = ResourceManager.GetString("Aguest");
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "9";
            dr_TopMenu[1] = ResourceManager.GetString("September");
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "10";
            dr_TopMenu[1] = ResourceManager.GetString("October");
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "11";
            dr_TopMenu[1] = ResourceManager.GetString("November");
            dt_ChkList.Rows.Add(dr_TopMenu);

            dr_TopMenu = dt_ChkList.NewRow();
            dr_TopMenu[0] = "12";
            dr_TopMenu[1] = ResourceManager.GetString("December");
            dt_ChkList.Rows.Add(dr_TopMenu);
            return dt_ChkList;
        }
        #endregion
        #region "Searchpannel"
        private void GetSearchParameter()
        {
            ArrayList arylst = new ArrayList();

            arylst.Add(txtqPEEBIEC.Text);//Ա�����
            arylst.Add(txtqPEEBIEN.Text);//Ա������
            arylst.Add(txtPEEBIBDStart.Text);	//Ա����������
          
            //arylst.Add(txtqPEEBIIN.Text);//Ա�����֤���
            //arylst.Add(dpqPEEBIEG.SelectedValue);//Ա���Ա�
            //arylst.Add(dpqPEEBIMI.SelectedValue);//����״��
            //arylst.Add(txtqPEEBIENACode.Value);//Ա������
            //arylst.Add(txtqPEEBINP.Text);//Ա������
            //arylst.Add(dpqPEEBIRT.SelectedValue);//Ա����������
           // arylst.Add(txtqPEEBIDEPCode.Value);//Ա����������
            arylst.Add(txtqPEEBIDEP.Text); //Ա����������
           // arylst.Add(txtqPEEBIPCCode.Value);//��λ
            arylst.Add(txtqPEEBIPC.Text);//��λ 
            //arylst.Add(txtqPEEBIPTCode.Value);//��λ����
            //arylst.Add(txtqPEEBIELCode.Value);//ѧ��
            //arylst.Add(dpqPEEBILM.SelectedValue);//��ѧרҵ
            //arylst.Add(txtPEEBIGDStart.Text);//��ҵʱ��
            //arylst.Add(txtPEEBIGDEnd.Text);//��ҵʱ��
            //arylst.Add(txtqPEEBIDTCode.Value);//ְ��
            arylst.Add(txtqPEEBIDT.Text);//ְ��
            arylst.Add(txtPEEBIEDStart.Text);//Ա���볧ʱ��
          
            //arylst.Add(txtqPEEBIWTCode.Value);//Ա���������
            //arylst.Add(txtqPEEBIESTCode.Value);//��Դ�����Ϣ
            arylst.Add(txtqPEEBIESCode.Value);//״̬�����Ϣ
            
            //arylst.Add(dpqPEEBIVY.SelectedValue);//Ա�����֤��Ч���� 
            //arylst.Add(txtqPEEBIEA.Text.ToString().Trim());//Ա����������
            //arylst.Add(dpqPEEBIIBDMONTH.SelectedValue.ToString());//Ա�������·�
            ViewState["SearchParameter"] = arylst;

        }
        protected void btnSubmitSearch_Click(object sender, EventArgs e)
        {

            programmaticQueryModalPopup.Hide();

            //AspNetPager1.CurrentPageIndex = 1;
            Session["010101AspNetPageCurPage"] = 1;
            GetSearchParameter();
            BindGridView();


            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }

        //#region ���ɲ�����
        //private void GenerateDeptTree()
        //{
        //    TreeNode node = new TreeNode();
        //    node.Value = "";
        //    node.Text = "�ú��Ӱ���";
        //    node.SelectAction = TreeNodeSelectAction.None;
        //    node.ShowCheckBox = false;
        //    treeDeptartment.Nodes.Add(node);
        //    node.PopulateOnDemand = true;
        //    //node.Expand();
        //    treeDeptartment.ExpandAll();
        //}
        //protected void treeDept_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        //{
        //    PBDEPEntity[] deptArray = GetChild(e.Node.Value.ToString());
        //    if (deptArray.Length > 0)
        //    {
        //        foreach (PBDEPEntity dept in deptArray)
        //        {
        //            TreeNode node = new TreeNode();
        //            node.Text = dept.DeptName;
        //            node.Value = dept.DeptCode;

        //            node.PopulateOnDemand = true;
        //            //node.SelectAction = TreeNodeSelectAction.Select;//None;
        //            node.SelectAction = TreeNodeSelectAction.None;
        //            node.NavigateUrl = dept.DeptCode;

        //            e.Node.ChildNodes.Add(node);
        //        }
        //    }

        //}

        //private PBDEPEntity[] GetChild(string parentDeptCode)
        //{
        //    DataTable dt = new DataTable();
        //    if (parentDeptCode == "�ú��Ӱ���")
        //    {
        //        dt = DbHelper.GetInstance().GetChildDeptbyDeptCode("");
        //    }
        //    else
        //    {
        //        dt = DbHelper.GetInstance().GetChildDeptbyDeptCode(parentDeptCode);
        //    }
        //    return DbHelper.GetInstance().GetDeptEntityArray(dt);
        //}

        //#endregion
        #endregion
        
        //������Ҫ�ǹ������Ĳ�������
        protected void btnBrowseMode_Click(object sender, EventArgs e)
        {
            GPRP.GPRPControls.Button btn = (GPRP.GPRPControls.Button)sender;
            //UCOperationBanner1.ButtnEnable(ClientID);
            switch (btn.ID)
            {
                case "btnBrowseMode":
                   
                    
                    break;
                case "btnAdd":
                  Context.Response.Redirect("20101001.aspx?EmployeeID=0");
                  
                    break;
                case "btnSubmit":
                    break;
                case "btnDel":
                    BrowseEditMode mode = ((BrowseEditMode)ViewState["Mode"]);
                    if (mode == BrowseEditMode.Browse)
                    {
                        string sID = "";
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            GridViewRow row = GridView1.Rows[i];
                            CheckBox check = (CheckBox)row.FindControl("item");
                            if (check.Checked)
                            {
                                sID += row.Cells[4].Text + ",";
                            }
                        }
                        if (sID.Length > 0)
                        {
                          
                            //refresh the datasource.
                            BindGridView();
                        }
                    }
                    break;
                case "btnImportExcel":
                    //word
                    
                    break;
                case "btnExportExcel":
                    //excel
                    //System.Web.UI.ScriptManager.RegisterStartupScript(btn, this.GetType(), "ButtonHideScript", strButtonHideScript, true);
                    DataSet ds = DbHelper.GetInstance().sp_GetPEEBINameList((ArrayList)ViewState["SearchParameter"]);
                    //Context.Response.Redirect("20101005.aspx?arryList=" + (ArrayList)ViewState["SearchParameter"]);
                    System.Web.UI.ScriptManager.RegisterStartupScript(btn, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
                    CreateExcel((DataTable)ViewState["sysPEEBITableColumn"], ds, "1", "ToExcel.xls", "True");
                    //Random random1 = new Random();
                    //int intRandom = random1.Next(100);
                    //string fileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0') + DateTime.Now.Millisecond.ToString().PadLeft(3, '0') + intRandom.ToString().PadLeft(3, '0') + ".xls";
                    //Export(this.Page, ds.Tables[0], fileName);
                    
                   
                    break;
            }
        }

        private void enableOperationByUserRight()
        {
            //menuID = "201010";
            //dtUserRight = DbHelper.GetInstance().GetSpUserRight(menuID, userid);   bear
            //if (Convert.ToInt32(dtUserRight.Rows[0]["ViewRight"]) == 0)
            //{
            //    Response.Redirect("NoPrivilege.aspx");
            //    return;
            //}
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeInfo.aspx?action=add");
 
        }

        protected void btnDelet_Click(object sender, EventArgs e)  
        {
            bool checkDele = false;
            string szMykeyCol = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("item")).Checked == true)
                {
                    checkDele = true;
                    szMykeyCol = GridView1.Rows[i].Cells[2].Text.ToString();
                   string szResult = DbHelper.GetInstance().DeletePEEBI(szMykeyCol);
                   if (szResult == "0")
                   {
                   }
                   szResult = DbHelper.GetInstance().DeletePEEBITEMP(szMykeyCol);
                   if (szResult == "0")
                   { 
                   }
                   BindGridView();

                }
            }

            if (!checkDele)
            {
                string strScript = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
              "alert('��ѡ��Ҫɾ�����'); \r\n" +
             "</script> \r\n";
                System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strScript2", strScript, false);
            }
       

        
        
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            DataTable dtNull = new DataTable();
            DataTable dtAdd = new DataTable();
            DataTable dtRet = new DataTable();
            if (Session["GroupId"].ToString() == "2")
            {

                //DataTable dt0 = (DataTable)ViewState["g_sysPEEBITableColumnTEMP0"];
                //DataTable dt1 = (DataTable)ViewState["g_sysPEEBITableColumnTEMP1"];
                //dt1.Merge(dt0);
                //DataTable dt2 = DbHelper.GetInstance().GetSysTableColumnByTableName("PEEBITEMP", "", "0");
                //dt2.Merge(DbHelper.GetInstance().GetSysTableColumnByTableName("PEEBITEMP", "", "1"));
                //string keyField = "ColumnId";
                //CompareDt(dt1, dt2, keyField, out dtAdd, out dtRet, out dtRet, out dtRet);
                //ViewState["sysPEEBITableColumnTEMPAdd"] = dtAdd;
                //if (dtAdd.Rows.Count > 0)
                //{
                //    if (dtAdd.Rows[0]["ColIsShow"].ToString() == "1")
                //        ViewState["g_sysPEEBITableColumnTEMP"] = dt2;
                //}
          
            }
            else {
                string keyField = "ColumnId";
                DataTable dtNosys = (DataTable)ViewState["sysPEEBITableColumnTEMP0"];
                DataTable dtNosys1 = (DataTable)ViewState["sysPEEBITableColumnTEMP1"];
                DataTable dt2 = DbHelper.GetInstance().GetSysTableColumnByTableName("PEEBITEMP", "", "1");
                if (dtNosys.Rows.Count > 0)
                {
                    if (dtNosys1.Rows.Count > 0)
                    {
                        CompareDt(dtNosys1, dt2, keyField, out dtAdd, out dtRet, out dtRet, out dtRet);
                        ViewState["sysPEEBITableColumnTEMPAdd"] = dtAdd;
                        ViewState["sysPEEBITableColumnTEMP1"] = dt2;
                    }
                    else
                    {
                        ViewState["sysPEEBITableColumnTEMPAdd"] = dt2;
                        ViewState["sysPEEBITableColumnTEMPT"] = dt2;
                        ViewState["sysPEEBITableColumnTEMP0"] = dtNull;
     
                    }
                }
                else
                {
                    DataTable dt1 = (DataTable)ViewState["sysPEEBITableColumnTEMPT"];
                    if (dt1.Rows.Count > 0)
                    {
                        if (dt1.Rows.Count >= dt2.Rows.Count)
                        {
                            ViewState["sysPEEBITableColumnTEMPAdd"] = dtNull;
                        }
                        else
                        {
                            CompareDt(dt1, dt2, keyField, out dtAdd, out dtRet, out dtRet, out dtRet);
                            ViewState["sysPEEBITableColumnTEMPAdd"] = dtAdd;
                        }
                        ViewState["sysPEEBITableColumnTEMPT"] = dt2;
                    }
                }
            }
          
            ViewState["sysPEEBITableColumn"] = dtNull;
            ViewState["sysPEEBITableColumnTEMP"] = dtNull;
            GridView1BrowseUI(BrowseEditMode.Browse);
            BindGridView();
        }

        /// <summary>
        /// �Ƚ�����DataTable���ݣ��ṹ��ͬ��
        /// </summary>
        /// <param name="dt1">�������ݿ��DataTable</param>
        /// <param name="dt2">�����ļ���DataTable</param>
        /// <param name="keyField">�ؼ��ֶ���</param>
        /// <param name="dtRetAdd">�������ݣ�dt2�е����ݣ�</param>
        /// <param name="dtRetDif1">��ͬ�����ݣ����ݿ��е����ݣ�</param>
        /// <param name="dtRetDif2">��ͬ�����ݣ�ͼ2�е����ݣ�</param>
        /// <param name="dtRetDel">ɾ�������ݣ�dt2�е����ݣ�</param>
        public  void CompareDt(DataTable dt1, DataTable dt2, string keyField,
            out DataTable dtRetAdd, out DataTable dtRetDif1, out DataTable dtRetDif2,
            out DataTable dtRetDel)
        {
            //Ϊ����������ṹ
            dtRetDel = dt1.Clone();
            dtRetAdd = dtRetDel.Clone();
            dtRetDif1 = dtRetDel.Clone();
            dtRetDif2 = dtRetDel.Clone();

            int colCount = dt1.Columns.Count;

            DataView dv1 = dt1.DefaultView;
            DataView dv2 = dt2.DefaultView;

            //���Ե�һ����Ϊ���գ����ڶ��������޸��˻���ɾ����
            foreach (DataRowView dr1 in dv1)
            {
                dv2.RowFilter = keyField + " = '" + dr1[keyField].ToString() + "'";
                if (dv2.Count > 0)
                {
                    if (!CompareUpdate(dr1, dv2[0]))//�Ƚ��Ƿ��в�ͬ��
                    {
                        dtRetDif1.Rows.Add(dr1.Row.ItemArray);//�޸�ǰ
                        dtRetDif2.Rows.Add(dv2[0].Row.ItemArray);//�޸ĺ�
                       // dtRetDif2.Rows[dtRetDif2.Rows.Count - 1]["FID"] = dr1.Row["FID"];//��ID���������ļ��ı���Ϊ����IDȫ��==0
                        continue;
                    }
                }
                else
                {
                    //�Ѿ���ɾ����
                    dtRetDel.Rows.Add(dr1.Row.ItemArray);
                }
            }

            //�Ե�һ����Ϊ���գ�����¼�Ƿ���������
            dv2.RowFilter = "";//�������
            foreach (DataRowView dr2 in dv2)
            {
                dv1.RowFilter = keyField + " = '" + dr2[keyField].ToString() + "'";
                if (dv1.Count == 0)
                {
                    //������
                    dtRetAdd.Rows.Add(dr2.Row.ItemArray);
                }
            }
        }

        //�Ƚ��Ƿ��в�ͬ��
        private static bool CompareUpdate(DataRowView dr1, DataRowView dr2)
        {
            //����ֻҪ��һ�һ���������оͲ�һ��,����Ƚ�����
            object val1;
            object val2;
            for (int i = 1; i < dr1.Row.ItemArray.Length; i++)
            {
                val1 = dr1[i];
                val2 = dr2[i];
                if (!val1.Equals(val2))
                {
                    return false;
                }
            }
            return true;
        }

 


    }
}