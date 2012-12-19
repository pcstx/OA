using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using GPRP.Web.UI;
using GPRP.GPRPComponents;
using GPRP.GPRPControls;
using GPRP.GPRPEnumerations;
using GPRP.GPRPBussiness;
using GPRP.Entity;
using YYControls;
using VBControls.VBProject;
using MyADO;


namespace GOA
{
    public partial class GroupLineSelect : BasePage
    {

        private int DataSetID;
        //private int RequestID;
        private int FieldID;
        private DataTable dtParameters;
        private Workflow_DataSetEntity dsEntity;
        private NameValueCollection nvc = new NameValueCollection();
        private string[] dsKeyColumns;
        private DataTable dtIniteSearchResult;
        private string[] SelectColumns;

        public GroupLineSelect()
        {
            FieldID = DNTRequest.GetInt("FieldID", 0);
            DataSetID = DNTRequest.GetInt("DataSetID", 0);
            //FormID = DNTRequest.GetInt("FormID", 0); ;
            dsEntity = DbHelper.GetInstance().GetWorkflow_DataSetEntityByKeyCol(DataSetID.ToString());
            string[] splitStr = new string[1];
            splitStr[0] = ",";
            string[] returnColumns = dsEntity.ReturnColumns.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
            SelectColumns = returnColumns;
            string[] returnColumnsName = dsEntity.ReturnColumnsName.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < returnColumns.Length; j++)
            {
                nvc.Add(returnColumns[j], returnColumnsName[j]);

            }
            dsKeyColumns = dsEntity.DataSetPKColumns.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);

            //获取参数列表
            dtParameters = DbHelper.GetInstance().GetWorkflow_DataSetParameterEntityByKeyCol(DataSetID.ToString());
            dtIniteSearchResult = DbHelper.GetInstance().GetDataSetTable(DataSetID, dtParameters);

            DataColumn[] PKdtSearchResult = new DataColumn[dsKeyColumns.Length];
            for (int p = 0; p < dsKeyColumns.Length; p++)
            {
                PKdtSearchResult[p] = dtIniteSearchResult.Columns[dsKeyColumns[p]];
            }

            dtIniteSearchResult.PrimaryKey = PKdtSearchResult;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            InitPage();
        }

        private void InitPage()
        {
            int IsNewLine = 0;
            Literal literialStart = new Literal();
            literialStart.ID = "lblMainFieldStart";
            literialStart.Text = "<div class='clear'>";
            placeHolder.Controls.Add(literialStart);

            GridView1.DataKeyNames = dsKeyColumns;

            //生成输入查询条件的区域

            for (int i = 0; i < dtParameters.Rows.Count; i++)
            {

                string ParameterName = dtParameters.Rows[i]["ParameterName"].ToString();
                string ParameterDesc = dtParameters.Rows[i]["ParameterDesc"].ToString();

                Literal lblFieldDesc = new Literal();
                lblFieldDesc.ID = "lblField" + ParameterName + "Desc";

                if (i > 0 && i % 2 == 0)
                {
                    IsNewLine = 1;
                }
                else
                {
                    IsNewLine = 0;
                }
                if (IsNewLine == 1)
                {
                    lblFieldDesc.Text = "</div><div class='clear'><div class='half'><label class='char5'>" + ParameterDesc + "</label><div class='iptblk'>";

                }
                else
                {
                    lblFieldDesc.Text = "<div class='half'><label class='char5'>" + ParameterDesc + "</label><div class='iptblk'>";
                }
                placeHolder.Controls.Add(lblFieldDesc);

                GPRP.GPRPControls.TextBox txtField = new GPRP.GPRPControls.TextBox();
                txtField.ID = "field" + ParameterName;
                txtField.Text = dtParameters.Rows[i]["ParameterValue"].ToString();
                txtField.Width = new Unit(120);

                if (dtParameters.Rows[i]["ParameterValue"].ToString() != "")
                {
                    txtField.CanBeNull = "必填";
                }
                placeHolder.Controls.Add(txtField);
                Literal literial = new Literal();
                literial.ID = "lblField" + ParameterName + "End";
                literial.Text = "</div></div>";
                placeHolder.Controls.Add(literial);
            }




            Literal literialEnd = new Literal();
            literialEnd.ID = "lblMainFieldEnd";
            literialEnd.Text = "</div>";
            placeHolder.Controls.Add(literialEnd);


            //加操作按钮

            Literal lblButtonListDesc = new Literal();
            lblButtonListDesc.ID = "lblbtnListDesc";
            lblButtonListDesc.Text = "<div class='clear'><div class='half'>";
            placeHolder.Controls.Add(lblButtonListDesc);

            GPRP.GPRPControls.Button btnQuery = new GPRP.GPRPControls.Button();
            btnQuery.ID = "btnQuery";
            btnQuery.ButtonImgUrl = "images/query.gif";
            btnQuery.ShowPostDiv = true;
            btnQuery.Text = "Button_Search";
            btnQuery.Width = new Unit(100);
            btnQuery.Click += new EventHandler(btnQueryClick);
            placeHolder.Controls.Add(btnQuery);


            Literal lblButtonListBlank = new Literal();
            lblButtonListBlank.ID = "lblbtnListBlank";
            lblButtonListBlank.Text = "&nbsp;";
            placeHolder.Controls.Add(lblButtonListBlank);

            GPRP.GPRPControls.Button btnConfirm = new GPRP.GPRPControls.Button();
            btnConfirm.ID = "btnConfirm";
            btnConfirm.ButtonImgUrl = "images/query.gif";
            btnConfirm.ShowPostDiv = false;
            btnConfirm.Text = "Button_Confirm";
            btnConfirm.Width = new Unit(100);
            btnConfirm.Click += new EventHandler(btnConfirmClick);
            placeHolder.Controls.Add(btnConfirm);

            Literal lblButtonListEnd = new Literal();
            lblButtonListEnd.ID = "lblbtnListEnd";
            lblButtonListEnd.Text = "</div></div>";
            placeHolder.Controls.Add(lblButtonListEnd);

            //   GridView1.Columns.Clear();
            for (int i = 0; i < dtIniteSearchResult.Columns.Count; i++)
            {
                BoundField mBoundField = new BoundField();
                mBoundField.HeaderText = nvc[dtIniteSearchResult.Columns[i].ColumnName].ToString();
                mBoundField.DataField = dtIniteSearchResult.Columns[i].ColumnName;
                GridView1.Columns.Add(mBoundField);
            }

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable dtSelectResult = dtIniteSearchResult.Clone();
                DataColumn[] PKdtSelectResult = new DataColumn[dsKeyColumns.Length];
                for (int p = 0; p < dsKeyColumns.Length; p++)
                {
                    PKdtSelectResult[p] = dtSelectResult.Columns[dsKeyColumns[p]];
                }
                dtSelectResult.PrimaryKey = PKdtSelectResult;
                ViewState["dtSelectResult"] = dtSelectResult;
                ViewState["dtSearchResult"] = dtIniteSearchResult;
                ViewState["selectedLines"] = new ArrayList();
                txtPageSize.Text = GeneralConfigs.GetConfig().PageSize.ToString();
                GridView1.PageSize = Convert.ToInt32(txtPageSize.Text);

                BindGridView();
            }
        }

        protected void btnQueryClick(object sender, EventArgs e)
        {
            //获取参数的值

            BindGridView();
        }

        protected void btnConfirmClick(object sender, EventArgs e)
        {
            CollectSelected();
            DataTable dtSelectResult = (DataTable)ViewState["dtSelectResult"];
            Session[FieldID.ToString() + "GroupLine"] = dtSelectResult;
            string strButtonSelectScript = "btnConfirmClick('" + dtSelectResult.Rows.Count.ToString() + "');";
            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strButtonSelectScript", strButtonSelectScript, true);
        }

        #region aspnetPage 分页代码
        //此类无须更改
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            GridView1.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            CollectSelected();
            BindGridView();
        }
        protected void AspNetPager1_PageChanging(object src, EventArgs e)
        {
        }

        //设置每页显示记录数，无须更改；如果要更改默认第页记录数，到config/geneal.config中更改 PageSize
        protected void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            if (txtPageSize.Text == "" || Convert.ToInt32(txtPageSize.Text) == 0)
            {
                ViewState["PageSize"] = GeneralConfigs.GetConfig().PageSize;//每页显示的默认值

            }
            else
            {
                ViewState["PageSize"] = Convert.ToInt32(txtPageSize.Text);
            }
            AspNetPager1.PageSize = Convert.ToInt32(ViewState["PageSize"]);
            GridView1.PageSize = (int)ViewState["PageSize"];
            CollectSelected();
            BindGridView();
        }
        #endregion

        private void BindGridView()
        {
            //先获取参数的值
            for (int i = 0; i < dtParameters.Rows.Count; i++)
            {
                string ParameterName = dtParameters.Rows[i]["ParameterName"].ToString();
                GPRP.GPRPControls.TextBox txtField = (GPRP.GPRPControls.TextBox)placeHolder.FindControl("field" + ParameterName);
                dtParameters.Rows[i]["ParameterValue"] = txtField.Text;
            }

            DataTable dt = DbHelper.GetInstance().GetDataSetTable(DataSetID, dtParameters);
            if (dt.Rows.Count > 0)
                AspNetPager1.RecordCount = Convert.ToInt32(dt.Rows.Count);
            else
                AspNetPager1.RecordCount = 0;

            GridView1.DataSource = dt;
            GridView1.DataBind();

            DataColumn[] PKdtSearchResult = new DataColumn[dsKeyColumns.Length];
            for (int p = 0; p < dsKeyColumns.Length; p++)
            {
                PKdtSearchResult[p] = dt.Columns[dsKeyColumns[p]];
            }

            dt.PrimaryKey = PKdtSearchResult;

            ViewState["dtSearchResult"] = dt;
            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string[] PKString = new string[dsKeyColumns.Length];
                for (int j = 0; j < dsKeyColumns.Length; j++)
                    PKString[j] = GridView1.DataKeys[e.Row.RowIndex][j].ToString().Trim();

                CheckBox cb = (CheckBox)e.Row.FindControl("Item");
                string IsCheck = "N";
                for (int j = 0; j < selectedLines.Count; j++)
                {
                    string[] PKStringTemp = new string[dsKeyColumns.Length];
                    PKStringTemp = (String[])selectedLines[j];
                    for (int ii = 0; ii < dsKeyColumns.Length; ii++)
                    {
                        if (PKString[ii] == PKStringTemp[ii])
                        {
                            IsCheck = "Y";
                            continue;
                        }
                        else
                        {
                            IsCheck = "N";
                            break;
                        }
                    }

                    if (IsCheck == "Y")
                        cb.Checked = true;
                }
            }
        }


        private void CollectSelected()
        {

            ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
            DataTable dtSelectResult = (DataTable)ViewState["dtSelectResult"];
            DataTable dtSearchResult = (DataTable)ViewState["dtSearchResult"];
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                string[] PKString = new string[dsKeyColumns.Length];
                CheckBox cb = this.GridView1.Rows[i].FindControl("Item") as CheckBox;
                for (int j = 0; j < dsKeyColumns.Length; j++)
                {
                    PKString[j] = GridView1.DataKeys[i][j].ToString().Trim();
                }
                if (cb.Checked)
                {
                    //如果ArrayList 没有所选种的Key值，则加入

                    string HasAdd = "N";
                    for (int jj = 0; jj < selectedLines.Count; jj++)
                    {
                        string[] PKStringTemp = new string[dsKeyColumns.Length];
                        PKStringTemp = (String[])selectedLines[jj];
                        for (int ii = 0; ii < dsKeyColumns.Length; ii++)
                        {
                            if (PKString[ii] == PKStringTemp[ii])
                            {
                                HasAdd = "Y";
                                continue;
                            }
                            else
                            {
                                HasAdd = "N";
                                break;
                            }
                        }
                        if (HasAdd == "Y")
                        {
                            break;
                        }
                    }
                    if (HasAdd == "N")
                    {
                        selectedLines.Add(PKString);
                        DataRow row = dtSearchResult.Rows.Find(PKString);
                        DataRow selectRow = dtSelectResult.NewRow();

                        for (int c = 0; c < SelectColumns.Length; c++)
                        {
                            selectRow[SelectColumns[c]] = row[SelectColumns[c]];
                        }
                        dtSelectResult.Rows.Add(selectRow);
                    }
                }
                else
                {
                    //如果ArrayList 有所选种的Key值，则Remove
                    string ToBeRemove = "N";
                    int jj = 0;
                    for (jj = 0; jj < selectedLines.Count; jj++)
                    {
                        string[] PKStringTempRemove = new string[dsKeyColumns.Length];
                        PKStringTempRemove = (String[])selectedLines[jj];
                        for (int ii = 0; ii < dsKeyColumns.Length; ii++)
                        {
                            if (PKString[ii] == PKStringTempRemove[ii])
                            {
                                ToBeRemove = "Y";
                                continue;
                            }
                            else
                            {
                                ToBeRemove = "N";
                                break;
                            }
                        }
                        if (ToBeRemove == "Y")
                        {
                            break;
                        }
                    }
                    if (ToBeRemove == "Y")
                    {
                        DataRow row = dtSelectResult.Rows.Find(PKString);
                        dtSelectResult.Rows.Remove(row);
                        selectedLines.RemoveAt(jj);
                    }
                }
            }

            ViewState["selectedLines"] = selectedLines;
            ViewState["dtSelectResult"] = dtSelectResult;
        }
    }
}
