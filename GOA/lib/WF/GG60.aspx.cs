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
    public partial class GG60 : BasePage
    {
        private static string strOperationState;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                AspNetPager1.PageSize = config.PageSize;

                DDLDataBind();

                ViewState["selectedLines"] = new ArrayList();

                //设置gridView的界面和数据

                BindGridView();

            }
        }
        //绑定数据库类型

        private void DDLDataBind()
        {
            string[] str = { "SqlServer", "Oracle" };
            ddlDBType.DataSource = str;
            ddlDBType.DataBind();
            string[] str2 = { "", "SqlServer", "Oracle" };
            ddlQDBType.DataSource = str2;
            ddlQDBType.DataBind();

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
            //再进行绑定一次

            CollectSelected();
            BindGridView();
        }

        #region gridView 事件

        protected void GridView1_RowCreated(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }
        //1。更改key字段在GridView中的哪一行，默认都是第5行，第一二列为button 第三列为checkBox 第四列为edit字段，第五列 关键字列
        //2。通过关键字获取单条记录。

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = -1;
            if (e.CommandName == "select")
            {
                programmaticAddModalPopup.Show();
                strOperationState = "Update";
                index = Convert.ToInt32(e.CommandArgument);   //获取行号

                string keyCol = GridView1.DataKeys[index].Value.ToString();
                //第二处待修改位置
                Workflow_DataSourceEntity _Workflow_DataSourceEntity = new Workflow_DataSourceEntity();
                _Workflow_DataSourceEntity = DbHelper.GetInstance().GetDataSourceByID(Convert.ToInt32(keyCol));
                if (_Workflow_DataSourceEntity != null) //SetPannelData(_Workflow_DataSourceEntity);
                {
                    txtDSID.Value = _Workflow_DataSourceEntity.DataSourceID.ToString();
                    txtDSName.Text = _Workflow_DataSourceEntity.DataSourceName;
                    txtConnectString.Text = _Workflow_DataSourceEntity.ConnectString;
                    ddlDBType.SelectedValue = _Workflow_DataSourceEntity.DataSourceDBType;
                }
            }
        }
        //此类要进行dorpdownlist/chk控件的转换

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string KeyCol = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
                CheckBox cb = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("Item") as CheckBox;
                ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
                if (selectedLines.Contains(KeyCol))
                {
                    cb.Checked = true;
                }
            }
        }

        #endregion

        #region aspnetPage 分页代码
        //此类无须更改
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            CollectSelected();
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
            string WhereCondition = "1=1";
            if (txtQDataSourceName.Text != string.Empty)
            {
                WhereCondition += "and  DataSourceName like '%" + txtQDataSourceName.Text + "%'";
            }
            if (ddlQDBType.SelectedIndex != 0)
            {
                WhereCondition += "and DataSourceDBType ='" + ddlQDBType.SelectedValue + "'";
            }


            DataTable dt = DbHelper.GetInstance().GetDBRecords("*", "Workflow_DataSource", WhereCondition, "DataSourceID", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
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

        protected void btnSearchRecord_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            ViewState["selectedLines"] = new ArrayList();
            BindGridView();
            System.Web.UI.ScriptManager.RegisterStartupScript(btnQuery, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
            programmaticQueryModalPopup.Hide();
        }

        //此类一般不需要更改,因为保存的工作全部放在下面的SaveData中
        protected void hideModalPopupViaServer_Click(object sender, EventArgs e)
        {
            GPRP.GPRPControls.Button btn = (GPRP.GPRPControls.Button)sender;
            int sResult = -1;
            if (btn.ID == "btnSubmitAndClose" || btn.ID == "btnSubmit")
            {
                //保存
                sResult = SaveData();
                if (sResult == -1)
                {
                    lblMsg.Text = ResourceManager.GetString("Operation_RECORD");
                }
                else
                {
                    if (btn.ID == "btnSubmitAndClose")
                    {
                        //hide
                        this.programmaticAddModalPopup.Hide();
                    }
                }
                CollectSelected();
                BindGridView();
            }
            System.Web.UI.ScriptManager.RegisterStartupScript(btn, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }

        //此类要更改,完成赋值工作
        private int SaveData()
        {
            Workflow_DataSourceEntity _Workflow_DataSourceEntity = new Workflow_DataSourceEntity();
            _Workflow_DataSourceEntity.DataSourceID = (txtDSID.Value == null || txtDSID.Value == "") ? 0 : Convert.ToInt32(txtDSID.Value);//Convert.ToInt32(txtDSID.Value );
            _Workflow_DataSourceEntity.DataSourceName = txtDSName.Text;
            _Workflow_DataSourceEntity.DataSourceDBType = ddlDBType.SelectedValue;
            _Workflow_DataSourceEntity.ConnectString = txtConnectString.Text;
            int sResult = -1;
            if (strOperationState == "Add")
                sResult = DbHelper.GetInstance().AddWorkflow_DataSource(_Workflow_DataSourceEntity);
            else if (strOperationState == "Update")
                sResult = DbHelper.GetInstance().UpdateWorkflow_DataSource(_Workflow_DataSourceEntity);
            return sResult;
        }
        #endregion

        protected void btnDel_Click(object sender, EventArgs e)
        {
            CollectSelected();
            ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
            for (int i = 0; i < selectedLines.Count; i++)
            {
                DbHelper.GetInstance().DeleteWorkflow_DataSource(Convert.ToInt32(selectedLines[i].ToString()));
            }
            BindGridView();
            ViewState["selectedLines"] = new ArrayList();
        }


        private void CollectSelected()
        {
            ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                string KeyCol = GridView1.DataKeys[i][0].ToString().Trim();
                CheckBox cb = this.GridView1.Rows[i].FindControl("Item") as CheckBox;
                if (selectedLines.Contains(KeyCol) && !cb.Checked)
                    selectedLines.Remove(KeyCol);
                if (!selectedLines.Contains(KeyCol) && cb.Checked)
                    selectedLines.Add(KeyCol);
            }
        }

        [WebMethod]
        public static string SetAddViewState()
        {
            strOperationState = "Add";
            return ""; //DbHelper.GetInstance().px_Sequence("Workflow_DataSourceCODE", "0");
        }

    }
}