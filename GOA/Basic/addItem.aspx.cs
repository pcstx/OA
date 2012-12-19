using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyADO;
using System.Data;
using GPRP.Web.UI;
using GPRP.GPRPEnumerations;
using GPRP.GPRPBussiness;
using System.Collections;

namespace GOA.Basic
{
    public partial class addItem : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["010101PageSize"] == null)
                    Session["010101PageSize"] = config.PageSize;//每页显示的默认值

                if (ViewState["Mode"] == null)
                    ViewState["Mode"] = BrowseEditMode.Browse;

             //   GridView1BrowseUI(BrowseEditMode.Browse);//列表

                if (Session["010101AspNetPageCurPage"] == null)
                {
                    Session["010101AspNetPageCurPage"] = 1;
                    BindGridView();
                }
                else
                {
                    BindGridView();

                }

                BindDropDownList();
            }
        }

        protected void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            //if (txtPageSize.Text == "" || Convert.ToInt32(txtPageSize.Text) == 0)
            //{
            //    Session["010101PageSize"] = config.PageSize;//每页显示的默认值
            //}
            //else
            //{
            //    Session["010101PageSize"] = Convert.ToInt32(txtPageSize.Text);
            //}
            //AspNetPager1.PageSize = Convert.ToInt32(Session["010101PageSize"]);
            ////再进行绑定一次
            //BrowseEditMode mode = ((BrowseEditMode)ViewState["Mode"]);

            //BindGridView();

            //System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }



        protected void btnDelete_Click(object sender, EventArgs e)
        {
            bool checkDele = false;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("Item")).Checked == true)
                {
                    checkDele = true;
                    string keyCol  = GridView1.DataKeys[i].Value.ToString();
                    DbHelper.GetInstance().DeletePEEBIITEM(keyCol);
                    DbHelper.GetInstance().DeleteSysTableColumnInfor(keyCol);
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
            BindGridView();
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
            if (e.CommandName == "Del")
            {

                int index = Convert.ToInt32(e.CommandArgument);   //获取行号
                GridViewRow row = GridView1.Rows[index];       //所在的行
                //第一处待修改位置
                string keyCol = row.Cells[1].Text.ToString();
                //第二处待修改位置
                //PEEBIEntity _PEEBIEntity = new PEEBIEntity();
                //_PEEBIEntity = DatabaseProvider.GetInstance().GetPEEBIEntityByKeyCol(keyCol);
                //if (_PEEBIEntity != null) SetPannelData(_PEEBIEntity);
                Session["010101SearchParameter"] = ViewState["SearchParameter"];
              //  Session["010101AspNetPageCurPage"] = AspNetPager1.CurrentPageIndex;

                //DbHelper.GetInstance().DeletePEEBIITEM(keyCol);
                //DbHelper.GetInstance().DeleteSysTableColumnInfor(keyCol);
                //BindGridView();
                //add bear    Response.Redirect("20101001.aspx?EmployeeID=" + keyCol);

            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewCommandEventArgs e)
        { 
        
        
        
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


        private void GridView1BrowseUI(BrowseEditMode Mode)
        {
            string[] ColumnName = { "ColName", "ColType", "ColDescriptionCN" };
            string[] ColText = { "字段名", "字段类型", "字段说明" };
            int [] ColWidth ={50,50,100};
            ButtonField btnSel = new ButtonField();
            btnSel.Text = "删除";
            btnSel.CommandName = "Del";
            btnSel.ShowHeader = true;
            btnSel.HeaderText = "操作";
            btnSel.ButtonType = ButtonType.Button;
            btnSel.ItemStyle.Width = 5;
            GridView1.Columns.Add(btnSel);
            for (int i = 0; i < ColumnName.Length; i++)
            {
                BoundField mBoundField = new BoundField();
                mBoundField.HeaderText = ColText[i];
                mBoundField.DataField = ColumnName[i];
                mBoundField.ItemStyle.Width = ColWidth[i];
                GridView1.Columns.Add(mBoundField);
 
            }
        
        }


        private void BindGridView()
        {
            
            string szTableName = "PEEBI";
            DataTable dt = DbHelper.GetInstance().GetNameAllList(szTableName);
            szTableName = "PEEBITEMP";
            dt.Merge(DbHelper.GetInstance().GetNameAllList(szTableName));
            string s = "";
            if (dt.Rows.Count > 0)
            {
              //  AspNetPager1.RecordCount = Convert.ToInt32(dt.Rows.Count);
               // s = "符合条件记录情况:";
             //   s += "总记录为:" + AspNetPager1.RecordCount.ToString();
                //s += "正式员工数:" + dt.Select("PEEBIES='01'").Length.ToString() +";";
                //s += "试用期员工数:" + dt.Select("PEEBIES='03'").Length.ToString() + ";";
                //s += "离职员工数:" + dt.Select("PEEBIES='02'").Length.ToString() + ";";
            }
            else
            {
               // AspNetPager1.RecordCount = 0;
            }
            StatisticDate.InnerHtml = s;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            BuildNoRecords(GridView1, dt);
        
        
        }

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

        private void BindDropDownList()
        {

            string[] DataType;
            DataType = new string[] { "varchar(50)",  "datetime",  "int"};
            for (int i = 0; i < DataType.Length; i++)
            {
                dpdPEEBITYPE.Items.Add(new ListItem(DataType[i], i.ToString()));
            }

            dpdPEEBITYPE.Items.Insert(0, "--请选择数据类型--"); 

        }




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

        protected void Btn_Click(object sender, EventArgs e)
        {
            int result = 0;
            string addItemFlagName = "PEEBI"+TextBox1.Text;
            addItemFlagName = addItemFlagName.ToUpper();
            string type = dpdPEEBITYPE.SelectedItem.Text;
            string MsgValue = TextBox2.Text;
            result = DbHelper.GetInstance().DoInsertItem(addItemFlagName, type);
            string TableName = "PEEBITEMP";
            string szresult = DbHelper.GetInstance().DoInsertSysTable(TableName, addItemFlagName, type, MsgValue);
            BindGridView();
        }



    }

}
