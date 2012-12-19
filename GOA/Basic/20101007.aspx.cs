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
                ViewState["PageSize"] = config.PageSize;//ÿҳ��ʾ��Ĭ��ֵ
                txtPageSize.Text = config.PageSize.ToString();
                AspNetPager1.PageSize = Convert.ToInt32(ViewState["PageSize"]);
                //�޸ı���,��ȡҪ��ʾ�ı�ṹ,
                ViewState["sysTableColumn"] = DbHelper.GetInstance().GetSysTableColumnByTableName("PBPOS", "","1");
                ViewState["sysTable"] = DbHelper.GetInstance().GetSysTable("PBPOS");

              
                //����gridView�Ľ��������
                if (ViewState["Mode"] == null) ViewState["Mode"] = BrowseEditMode.Browse;
                GridView1BrowseUI(BrowseEditMode.Browse);
                 BindGridView();


                //����ҳ������
                SetText();

              
            }
        }
        //����ÿҳ��ʾ��¼����������ģ����Ҫ����Ĭ�ϵ�ҳ��¼������config/geneal.config�и��� PageSize
        protected void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            if (txtPageSize.Text == "" || Convert.ToInt32(txtPageSize.Text) == 0)
            {
                ViewState["PageSize"] = config.PageSize;//ÿҳ��ʾ��Ĭ��ֵ
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
                string strButtonHideScript = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
                   "btnClick('" + row.Cells[3].Text.ToString() + "','" + keyCol + "'); \r\n" +
                  "</script> \r\n";
                System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ButtonSelectPersonScript", strButtonHideScript, false);
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
     
        #endregion
        #region aspnetPage ��ҳ����
        //�����������
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
          
            BindGridView();
        }
        protected void AspNetPager1_PageChanging(object src, EventArgs e)
        {
        }
        #endregion
        #region gridview UI
        //������Ҫ���ģ���Ҫ�Ǹ��Ļ�ȡ����Դ�ķ���
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
        //gridViewUI��ʾ
        private void GridView1BrowseUI(BrowseEditMode Mode)
        {
            //ѡҪ��sysTable���������Щ�ֶ�Ҫ��ʾ����
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
        //����һ�㲻��Ҫ���ģ��Ƕ����ֶ�ǰ������
        private void SetText()
        {
            lblPBPOSPN.Text = TextFromSysTableColumn("PBPOSPN");
            lblPageSize.Text = ResourceManager.GetString("lblPageSize");

        }
        //������ģ���Ϊ��page_Load�����Ѿ����������ݱ�����
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
        //������ģ���Ϊ��page_Load�����Ѿ����������ݱ�����
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
       
        //������Ҫ�ǹ������Ĳ�������
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
