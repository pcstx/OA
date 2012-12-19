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
using System.IO;
using GPRP.Web.UI;
using GPRP.GPRPComponents;
using GPRP.GPRPControls;
using GPRP.GPRPEnumerations;
using GPRP.GPRPBussiness;
using GPRP.Entity;
using YYControls;
using Bestcomy.Web.Controls.Upload;
using MyADO;

namespace HRMWeb.aspx
{
    public partial class _0101007 : BasePage
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
           if (!Page.IsPostBack)
            {
                ViewState["PageSize"] = config.PageSize;//每页显示的默认值
                txtPageSize.Text = config.PageSize.ToString();
                AspNetPager1.PageSize = Convert.ToInt32(ViewState["PageSize"]);
                //修改表名,获取要显示的表结构,
                ViewState["sysTableColumn"] = DbHelper.GetInstance().GetSysTableColumnByTableName("PBPOS", "","1");
                ViewState["sysTable"] = DbHelper.GetInstance().GetSysTable("PBPOS");

              
                //设置gridView的界面和数据
                if (ViewState["Mode"] == null) ViewState["Mode"] = BrowseEditMode.Browse;
                GridView1BrowseUI(BrowseEditMode.Browse);
                 BindGridView();


                //设置页面内容
                SetText();

              
            }
        }
        //设置每页显示记录数，无须更改；如果要更改默认第页记录数，到config/geneal.config中更改 PageSize
        protected void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            if (txtPageSize.Text == "" || Convert.ToInt32(txtPageSize.Text) == 0)
            {
                ViewState["PageSize"] = config.PageSize;//每页显示的默认值
            }
            else
            {
                ViewState["PageSize"] = Convert.ToInt32(txtPageSize.Text);
            }
            AspNetPager1.PageSize = Convert.ToInt32(ViewState["PageSize"]);
           
            BindGridView();
            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }

        protected void txtPBPOSPC_TextChanged(object sender, EventArgs e)
        {

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
                string strButtonHideScript = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
                   "btnClick('" + row.Cells[3].Text.ToString() + "','" + keyCol + "'); \r\n" +
                  "</script> \r\n";
                System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ButtonSelectPersonScript", strButtonHideScript, false);
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
     
        #endregion
        #region aspnetPage 分页代码
        //此类无须更改
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
          
            BindGridView();
        }
        protected void AspNetPager1_PageChanging(object src, EventArgs e)
        {
        }
        #endregion
        #region gridview UI
        //此类需要更改，主要是更改获取数据源的方法
        private void BindGridView()
        {
        //    string strCol = "PBPOS.[PBPOSPC],PBPOS.[PBPOSPN],PBPOS.[PBPOSPEN],PBPOS.[PBPOSPTWN],PBPOS.[PBPOSBD],PBPOS.[PBPOSPD],PBPOS.[PBPOSRB] "
        //                  + ",PBPOS.[PBPOSRBN],PBPOS.[PBPOSUS],PBPOS.[PBPOSOI]"
        //                     + ",PBDUT.*,PBPOT.PBPOTTC,PBPOT.PBPOTTN,PBPOC.[PBPOCCC],PBPOC.[PBPOCCN],PBPOS.[PBPOSWP],PBPOSWPPN=PBWOP.[PBWOPPN],PBPOS.[PBPOSET],PBPOSETN=PBET.[PBETTN]";
            //string strTable = "[PBPOS] "
            //            + "left join PBDUT on PBDUT.[PBDUTDC]=PBPOS.[PBDUTDC] "
            //            + "left join PBPOT on PBPOT.[PBPOTTC]=PBPOS.[PBPOTTC] "
            //            + "left join PBPOC on PBPOC.[PBPOCCC]=PBPOS.[PBPOCCC] "
            //            + "left join PBET on PBET.[PBETTC]=PBPOS.[PBPOSET] "
            //            +"left join PBWOP on PBWOP.[PBWOPPC]=PBPOS.[PBPOSWP] ";
             string strCol = "PBPOS.[PBPOSPC],PBPOS.[PBPOSPN],PBPOS.[PBPOSPEN],PBPOS.[PBPOSPTWN],PBPOS.[PBPOSBD],PBPOS.[PBPOSPD],PBPOS.[PBPOSRB] "
                         + ",PBPOS.[PBPOSRBN],PBPOS.[PBPOSUS],PBPOS.[PBPOSOI]"
                            + ",PBPOS.[PBPOSWP],PBPOS.[PBDUTDC]";
             string strTable = "[PBPOS]";
            string strwhere = "";
            if(txtPBPOSPN.Text.Trim()!="")  strwhere = " PBPOS.[PBPOSPN] like '%" + txtPBPOSPN.Text.Trim() + "%'";

            DataTable dt  = DbHelper.GetInstance().GetDBRecord("", strCol, strTable, strwhere, "PBPOSOI", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            if (dt.Rows.Count > 0)
                AspNetPager1.RecordCount = Convert.ToInt32(dt.Rows[0]["RecordCount"]);
            else
                AspNetPager1.RecordCount = 0;
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
            if (ViewState["sysTableColumn"] == null)
            {
                return;
            }
            DataTable dt = (DataTable)ViewState["sysTableColumn"];
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    int gvWidth = 25;
                    ButtonField btnSel = new ButtonField();
                    btnSel.Text = "Select";
                    btnSel.CommandName = "select";
                    btnSel.ShowHeader = true;
                    btnSel.HeaderText = "Select";
                    btnSel.ButtonType = ButtonType.Button;
                    btnSel.ItemStyle.Width = 30;
                    GridView1.Columns.Add(btnSel);
                    foreach (DataRow dr in dt.Select(""))
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
       
        #endregion
        #region pannel
        //此类一般不需要更改，是读出字段前的名称
        private void SetText()
        {
            lblPBPOSPN.Text = TextFromSysTableColumn("PBPOSPN");
            lblPageSize.Text = ResourceManager.GetString("lblPageSize");

        }
        //无须更改，因为在page_Load里面已经读出了数据表内容
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
        //无须更改，因为在page_Load里面已经读出了数据表内容
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
       
        #endregion
       
        //此类主要是工具条的操作界面
        protected void btnBrowseMode_Click(object sender, EventArgs e)
        {
            GPRP.GPRPControls.Button btn = (GPRP.GPRPControls.Button)sender;
            switch (btn.ID)
            {
                case "btnSelect":
                    BrowseEditMode mode = ((BrowseEditMode)ViewState["Mode"]);
                    if (mode == BrowseEditMode.Browse)
                    {
                        string sID = "";
                        string sName = "";
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            GridViewRow row = GridView1.Rows[i];
                            CheckBox check = (CheckBox)row.FindControl("item");
                            if (check.Checked)
                            {
                                sID += row.Cells[2].Text + ",";
                                sName += row.Cells[3].Text + ",";
                            }
                        }

                        if (sID.Length > 0)
                            sID = sID.Substring(0, sID.Length - 1);
                        if (sName.Length > 0)
                            sName = sName.Substring(0, sName.Length - 1);

                        string strButtonHideScript = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
                            "btnClick('" + sName + "','" + sID + "'); \r\n" +
                           "</script> \r\n";
                        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ButtonSelectPersonScript", strButtonHideScript, false);
                    }
                    break;
                case "btnImportWord":
                    //word
                    break;
                case "btnExprotExcel":
                    //excel
                    break;

            }
        }
    }
}
