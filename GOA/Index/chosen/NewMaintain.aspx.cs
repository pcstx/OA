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

namespace GOA.Index.chosen
{
    public partial class NewMaintain :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getNew();
        }

        private void getNew()
        {
            string WhereCondition = " a.IsPublish=1 and a.ExpiredDate >=getdate()";
            
            string tables = @"News_NewsList a left join News_NewsType b on a.NewsTypeID = b.NewsTypeID";
            DataTable dt = DbHelper.GetInstance().GetDBRecords("Title=a.NewsTitle,TypeDesc=b.NewsTypeDesc,Date=a.ExpiredDate,ID=a.NewsID", tables, WhereCondition, "a.NewsID", 5, 1);

            foreach (DataRow d in dt.Rows)
            {
                Response.Write("<a class='request' href='#'  NewsID='" + d["ID"] + "' >" + d["Title"] + "</a><p>");
            }

        }

    }
}
