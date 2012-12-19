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
                    Session["010101PageSize"] = config.PageSize;//每页显示的默认值

                //if (Session["GroupId"].ToString() == "2") //系统管理员
                //{
                //   // Button5.Attributes["style"] = " display:block";
                //}
                //else
                //{
                //   // btnSubmit.Attributes["style"] = " display:block";
                //}
              
             
                txtPageSize.Text = Session["010101PageSize"].ToString();
                AspNetPager1.PageSize = Convert.ToInt32(Session["010101PageSize"]);
               
                //修改表名,获取要显示的表结构,

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
                //绑定Pannel中需要绑定的控件，这样可以为下面的GridView中转换的时候提供数据源。
                //此类放在panel节点中
                BindDropDownList("");
                //部门树的生成
                //GenerateDeptTree();
                //treeDeptartment.CollapseAll();
                //treeDeptartment.Nodes[0].Expanded = true;
                //设置gridView的界面和数据
                if (ViewState["Mode"] == null) 
                    ViewState["Mode"] = BrowseEditMode.Browse;

                GridView1BrowseUI(BrowseEditMode.Browse);//列表

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
               
                //设置页面内容
               SetText();

            }
            //BindDropDownListTree();
            //System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }
        //设置每页显示记录数，无须更改；如果要更改默认第页记录数，到config/geneal.config中更改 PageSize
        protected void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            if (txtPageSize.Text == "" || Convert.ToInt32(txtPageSize.Text) == 0)
            {
                Session["010101PageSize"] = config.PageSize;//每页显示的默认值
            }
            else
            {
                Session["010101PageSize"] = Convert.ToInt32(txtPageSize.Text);
            }
            AspNetPager1.PageSize = Convert.ToInt32(Session["010101PageSize"]);
            //再进行绑定一次
            BrowseEditMode mode = ((BrowseEditMode)ViewState["Mode"]);
          
            BindGridView();

            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }
        #region gridView 事件
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
        //1。更改key字段在GridView中的哪一行，默认都是第5行，第一二列为button 第三列为checkBox 第四列为edit字段，第五列 关键字列
        //2。通过关键字获取单条记录。
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
             
                int index = Convert.ToInt32(e.CommandArgument);   //获取行号
                GridViewRow row = GridView1.Rows[index];       //所在的行
                //第一处待修改位置
                string keyCol = row.Cells[2].Text.ToString();
                //第二处待修改位置
                //PEEBIEntity _PEEBIEntity = new PEEBIEntity();
                //_PEEBIEntity = DatabaseProvider.GetInstance().GetPEEBIEntityByKeyCol(keyCol);
                //if (_PEEBIEntity != null) SetPannelData(_PEEBIEntity);
                Session["010101SearchParameter"] = ViewState["SearchParameter"];
                Session["010101AspNetPageCurPage"] =AspNetPager1.CurrentPageIndex;
               
            //add bear    Response.Redirect("20101001.aspx?EmployeeID=" + keyCol);
                Response.Redirect("EmployeeInfo.aspx?action=show&EmployeeID=" + keyCol);
             

            }
        }
        //此类要进行dorpdownlist/chk控件的转换
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当为编辑模式的情况下，要进行下拉框的转换
                //再进行绑定一次
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
        //编辑状态下的GridView一般不会用到该方法
        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        //此类要进行dorpdownlist/chk控件的转换
        //需要进行更改
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
        }

        protected void chkValid_CheckedChanged(object sender, EventArgs e)
       {
         CheckBox checkbox = (CheckBox)sender;
         GridViewRow row = (GridViewRow)checkbox.NamingContainer;
         //int index = row.RowIndex;  获取点击的行
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
        #region aspnetPage 分页代码
        //此类无须更改
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
      

        //此类需要更改，主要是更改获取数据源的方法
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
                s = "符合条件记录情况:";
                s += "总记录为:" + AspNetPager1.RecordCount.ToString();
                //s += "正式员工数:" + dt.Select("PEEBIES='01'").Length.ToString() +";";
                //s += "试用期员工数:" + dt.Select("PEEBIES='03'").Length.ToString() + ";";
                //s += "离职员工数:" + dt.Select("PEEBIES='02'").Length.ToString() + ";";
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
        //gridViewUI显示
        private void GridView1BrowseUI(BrowseEditMode Mode)
        {
            //选要从sysTable里面读出哪些字段要显示出来
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
                            btnSel.Text = "详细";
                            btnSel.CommandName = "select";
                            btnSel.ShowHeader = true;
                            btnSel.HeaderText = "详细";
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
        //此类一般不需要更改，是读出字段前的名称
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
        //无须更改，因为在page_Load里面已经读出了数据表内容
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
        

     
        #endregion
        #region pannel 's dropdownlist
        //此类主要是dropdownlist的初始化
        private void BindDropDownList(string selectValue)
        {
            // add bear
            //性别
            //DataTable dt_Sex = DbHelper.GetInstance().GetPBSEX("", ""); //SexDataTable();
            //ViewState["Sextable"] = dt_Sex;
            ////dpqPEEBIEG.AddTableData(dt_Sex, 0, 1, true, "Null");

            ////婚姻状况 --PBMAR
            //DataTable dtPBMAR = DbHelper.GetInstance().GetPBMAR("", "");
            //ViewState["PBMARtable"] = dtPBMAR;

            ////员工民族-PBNAT
            //DataTable dtPBNAT = DbHelper.GetInstance().GetPBNAT("", "");
            //ViewState["PBNATtable"] = dtPBNAT;

            //员工户口性质-PBNAT
            //DataTable dtPBRET = DbHelper.GetInstance().GetPBRET("", "");
            //ViewState["PBRETtable"] = dtPBRET;

            //岗位 --PBPOS
            DataTable dtPBPOS = DbHelper.GetInstance().GetPBPOS("", "");
            ViewState["PBPOStable"] = dtPBPOS;

            //职务 -- PEEBI
            DataTable dtPBDUT = DbHelper.GetInstance().GetPEEBI("", "");
            ViewState["PBDUTtable"] = dtPBDUT;

            ////学历 --PBEDU
            //DataTable dtPBEDU = DbHelper.GetInstance().GetPBEDU("", "");
            //ViewState["PBEDUtable"] = dtPBEDU;

            //所学专业 -- PBPRO
            //DataTable dtPBPRO = DbHelper.GetInstance().GetPBPRO("", "");
            //ViewState["PBPROtable"] = dtPBPRO;

            ////岗位性质  -- PBPOT
            //DataTable dtPBPOT = DbHelper.GetInstance().GetPBPOT("", "");
            //ViewState["PBPOTtable"] = dtPBPOT;

            ////员工来源类别信息  --PBESE
            //DataTable dtPBESE = DbHelper.GetInstance().GetPBESE("", "");
            //ViewState["PBESEtable"] = dtPBESE;

            ////员工状态类别信息  --PBEST
            DataTable dtPBEST = DbHelper.GetInstance().GetPBEST("", "");
            ViewState["PBESTtable"] = dtPBEST;

            ////员工工种类别  --PBEWT
            //DataTable dtPBEWT = DbHelper.GetInstance().GetPBEWT("", "");
            //ViewState["PBEWTtable"] = dtPBEWT;

            //身份证有效期限--PEEBIVY
            //DataTable dtPEEBIVY = PEEBIVYDataTable();
            //ViewState["PEEBIVYtable"] = dtPEEBIVY;

            //出生月份--PEEBIIBDMONTH
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
            //所属部门--3.	PBDEP

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
        //此类为dropdownlist服务的。因为有些数据源没有写入到数据库中，就是比较固定的内容。
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
        //此类为dropdownlist服务的。因为有些数据源没有写入到数据库中，就是比较固定的内容。
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

            arylst.Add(txtqPEEBIEC.Text);//员工编号
            arylst.Add(txtqPEEBIEN.Text);//员工姓名
            arylst.Add(txtPEEBIBDStart.Text);	//员工出生日期
          
            //arylst.Add(txtqPEEBIIN.Text);//员工身份证编号
            //arylst.Add(dpqPEEBIEG.SelectedValue);//员工性别
            //arylst.Add(dpqPEEBIMI.SelectedValue);//婚姻状况
            //arylst.Add(txtqPEEBIENACode.Value);//员工民族
            //arylst.Add(txtqPEEBINP.Text);//员工籍贯
            //arylst.Add(dpqPEEBIRT.SelectedValue);//员工户口性质
           // arylst.Add(txtqPEEBIDEPCode.Value);//员工所属部门
            arylst.Add(txtqPEEBIDEP.Text); //员工所属部门
           // arylst.Add(txtqPEEBIPCCode.Value);//岗位
            arylst.Add(txtqPEEBIPC.Text);//岗位 
            //arylst.Add(txtqPEEBIPTCode.Value);//岗位性质
            //arylst.Add(txtqPEEBIELCode.Value);//学历
            //arylst.Add(dpqPEEBILM.SelectedValue);//所学专业
            //arylst.Add(txtPEEBIGDStart.Text);//毕业时间
            //arylst.Add(txtPEEBIGDEnd.Text);//毕业时间
            //arylst.Add(txtqPEEBIDTCode.Value);//职务
            arylst.Add(txtqPEEBIDT.Text);//职务
            arylst.Add(txtPEEBIEDStart.Text);//员工入厂时间
          
            //arylst.Add(txtqPEEBIWTCode.Value);//员工工种类别
            //arylst.Add(txtqPEEBIESTCode.Value);//来源类别信息
            arylst.Add(txtqPEEBIESCode.Value);//状态类别信息
            
            //arylst.Add(dpqPEEBIVY.SelectedValue);//员工身份证有效期限 
            //arylst.Add(txtqPEEBIEA.Text.ToString().Trim());//员工进厂年龄
            //arylst.Add(dpqPEEBIIBDMONTH.SelectedValue.ToString());//员工生日月份
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

        //#region 生成部门树
        //private void GenerateDeptTree()
        //{
        //    TreeNode node = new TreeNode();
        //    node.Value = "";
        //    node.Text = "好孩子百瑞康";
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
        //    if (parentDeptCode == "好孩子百瑞康")
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
        
        //此类主要是工具条的操作界面
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
              "alert('请选择要删除的项！'); \r\n" +
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
        /// 比较两个DataTable数据（结构相同）
        /// </summary>
        /// <param name="dt1">来自数据库的DataTable</param>
        /// <param name="dt2">来自文件的DataTable</param>
        /// <param name="keyField">关键字段名</param>
        /// <param name="dtRetAdd">新增数据（dt2中的数据）</param>
        /// <param name="dtRetDif1">不同的数据（数据库中的数据）</param>
        /// <param name="dtRetDif2">不同的数据（图2中的数据）</param>
        /// <param name="dtRetDel">删除的数据（dt2中的数据）</param>
        public  void CompareDt(DataTable dt1, DataTable dt2, string keyField,
            out DataTable dtRetAdd, out DataTable dtRetDif1, out DataTable dtRetDif2,
            out DataTable dtRetDel)
        {
            //为三个表拷贝表结构
            dtRetDel = dt1.Clone();
            dtRetAdd = dtRetDel.Clone();
            dtRetDif1 = dtRetDel.Clone();
            dtRetDif2 = dtRetDel.Clone();

            int colCount = dt1.Columns.Count;

            DataView dv1 = dt1.DefaultView;
            DataView dv2 = dt2.DefaultView;

            //先以第一个表为参照，看第二个表是修改了还是删除了
            foreach (DataRowView dr1 in dv1)
            {
                dv2.RowFilter = keyField + " = '" + dr1[keyField].ToString() + "'";
                if (dv2.Count > 0)
                {
                    if (!CompareUpdate(dr1, dv2[0]))//比较是否有不同的
                    {
                        dtRetDif1.Rows.Add(dr1.Row.ItemArray);//修改前
                        dtRetDif2.Rows.Add(dv2[0].Row.ItemArray);//修改后
                       // dtRetDif2.Rows[dtRetDif2.Rows.Count - 1]["FID"] = dr1.Row["FID"];//将ID赋给来自文件的表，因为它的ID全部==0
                        continue;
                    }
                }
                else
                {
                    //已经被删除的
                    dtRetDel.Rows.Add(dr1.Row.ItemArray);
                }
            }

            //以第一个表为参照，看记录是否是新增的
            dv2.RowFilter = "";//清空条件
            foreach (DataRowView dr2 in dv2)
            {
                dv1.RowFilter = keyField + " = '" + dr2[keyField].ToString() + "'";
                if (dv1.Count == 0)
                {
                    //新增的
                    dtRetAdd.Rows.Add(dr2.Row.ItemArray);
                }
            }
        }

        //比较是否有不同的
        private static bool CompareUpdate(DataRowView dr1, DataRowView dr2)
        {
            //行里只要有一项不一样，整个行就不一样,无需比较其它
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