using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GPRP.Web.UI;
using System.Data;
using MyADO;
using GPRP.Entity.Basic;
using System.Collections;

namespace GOA.Basic
{
    public partial class NewMaintain : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DropDownListInite();
                AspNetPager1.PageSize = config.PageSize;
                BindGridView();
            }
        }
        protected void btnDelet_Click(object sender, EventArgs e)
        {
            bool checkDele = false;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("Item")).Checked == true)
                {
                        checkDele = true;
                        string szId = GridView1.DataKeys[i].Values["ID"].ToString(); 
                        string szTile =GridView1.DataKeys[i].Values["Title"].ToString(); 
                        string szIspublic =GridView1.DataKeys[i].Values["Isp"].ToString(); 
                        if (szIspublic == "是")
                        {
                            string strScript = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
                            "alert('新闻《" + szTile + " 》已经发布，不可以被删除！！'); \r\n" +
                            "</script> \r\n";
                            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strScript2", strScript, false);
                            ((CheckBox)GridView1.Rows[i].FindControl("Item")).Checked = false;
                            return;
                        }
                        else
                        {
                            DbHelper.GetInstance().DeleteNewsById(Int32.Parse(szId));
                            BindGridView();
                        }
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


        protected void btnAdd_Click(object sender, EventArgs e) 
        {
            Response.Redirect("NewMaintainAdd.aspx");

            
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            showWriteModalPopup.Hide();
            GetSearchParameter();
            BindGridView();
            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ButtonHideScript", strButtonHideScript, false);

        }

        protected void DropDownListInite()
        {
            DataTable dtNewType = DbHelper.GetInstance().GetNewsType();
            if (dtNewType.Rows.Count > 0)
            {
                for (int i = 0; i < dtNewType.Rows.Count; i++)
                    dpType.Items.Add(new ListItem(dtNewType.Rows[i]["NewsTypeDesc"].ToString(), dtNewType.Rows[i]["NewsTypeID"].ToString()));   //类别 

            }
            dpType.Items.Insert(0, "--请选择--");
        }


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
            BindGridView();

            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }

        private void GetSearchParameter()
        {
            ArrayList arylst = new ArrayList();
            arylst.Add(txtTitle.Text);
            arylst.Add(dpType.SelectedValue);
            arylst.Add(chkIsPublish.Checked == true ? '1' : '0');
            ViewState["SearchParameter"] = arylst;

        }



        private void BindGridView()
        {

            string WhereCondition = "1=1";
            if (ViewState["SearchParameter"] != null)
            {
                ArrayList arylst = (ArrayList)ViewState["SearchParameter"];
                string szTitle = arylst[0].ToString();
                if (szTitle != "")
                    WhereCondition += " and a.NewsTitle=" + string.Format("'{0}'",szTitle);
                int iType = Int32.Parse(arylst[1].ToString());
                if (iType != 0)
                    WhereCondition += " and a.NewsTypeID=" + iType; 
                char cIsPublish = Char.Parse(arylst[2].ToString());
                WhereCondition += " and a.IsPublish=" + cIsPublish;
            }
            string tables = @"News_NewsList a left join News_NewsType b on a.NewsTypeID = b.NewsTypeID";
            DataTable dt = DbHelper.GetInstance().GetDBRecords("Title=a.NewsTitle,TypeDesc=b.NewsTypeDesc,case a.Ispublish when 1 then '是' else '否' end as Isp ,Date=a.ExpiredDate,ID=a.NewsID", tables, WhereCondition, "a.NewsID", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            if (dt.Rows.Count > 0)
                AspNetPager1.RecordCount = Convert.ToInt32(dt.Rows[0]["RecordCount"]);
            else
                AspNetPager1.RecordCount = 0;

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
            if (e.CommandName == "select")
            {



            }
        }
        //此类要进行dorpdownlist/chk控件的转换
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
        #endregion

        
    }
}
