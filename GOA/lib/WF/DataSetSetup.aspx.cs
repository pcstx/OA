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
    public partial class DataSetSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            { 
                HiddenQuerySql.Value =Request.QueryString["querysql"];
                HiddenDataSourceID.Value =Request.QueryString["datasourceid"];
                ViewState["datasourceid"]=Request.QueryString["datasourceid"];

                HiddenDataSetID.Value  =Request.QueryString["datasetid"];
                bindTableList(Convert.ToInt32(Request.QueryString["datasourceid"].ToString()));
                if (this.ddlTable.Items.Count>0)
                {
                    bindColumnList(Convert.ToInt32(Request.QueryString["datasourceid"].ToString()), ddlTable.SelectedValue.Trim());
                }
            }
        }

        private void bindTableList(int dataSourceID)
        { 
            DataTable dt = new DataTable();
            dt = DbHelper.GetInstance().GetAllTableNames(dataSourceID);
            this.ddlTable.DataSource = dt;
            ddlTable.DataValueField = "TABLE_NAME";
            ddlTable.DataTextField  = "TABLE_NAME";
            ddlTable.DataBind();
         }

        private void bindColumnList(int dataSourceID,string tableName)
        {
            DataTable dt = new DataTable();
            dt = DbHelper.GetInstance().GetAllColumns(dataSourceID,tableName );
            this.cblColumn.DataSource = dt;
            cblColumn.DataTextField = "COLUMN_NAME";
            cblColumn.DataValueField = "COLUMN_NAME";
            cblColumn.DataBind();
        }

        protected void ddlTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindColumnList(Convert.ToInt32(HiddenDataSourceID.Value.Trim()),ddlTable.SelectedValue.Trim());
        }

        protected void btnSelect_OnClick(object sender, EventArgs e)
        {
            string strColumn = txtFieldList.Text;
            string strtable = ddlTable.SelectedValue;
            for (int i = 0; i < cblColumn.Items.Count; i++)
            {
                if (cblColumn.Items[i].Selected == true)
                {
                    strColumn = (strColumn != "" ?  strColumn+"," + strtable+'.'+cblColumn.Items[i].Value :  strtable+'.'+cblColumn.Items[i].Value);
                }
            }

            if (strColumn != "")
            {
                string txttable = txtTableList.Text;
                txtTableList.Text = (txttable != "" ? txttable + "," + strtable : strtable);
                txtFieldList.Text = strColumn;
            }
            else
            { 
           
            }
        }

        protected void ClearCheckboxlist(object sender, EventArgs e)
        {
            for (int i = 0; i < cblColumn.Items.Count; i++)
            {
                    cblColumn.Items[i].Selected = false;
            }
            this.txtFieldList.Text = "";
            this.txtOrderBy.Text = "";
            this.txtQueryCondition .Text = "";
            this.txtTableList.Text = "";
            this.txtQuerySql.Text = "";

       }
   
  /*      protected void btnOK_Click(object sender, EventArgs e)
        {
            string strsql="";
            string[] strR=new string[6] ; //用作返回数组
            strR[0] = HiddenDataSourceID.Value.Trim();
            if (txtQuerySql.Text.Trim() != "")
            {
                strsql = txtQuerySql.Text.Trim();
                strR[1] = strsql;
                int intselect = strsql.ToLower().IndexOf("select") + 6;
                int intfrom = strsql.ToLower().IndexOf("from") + 4;
                int intwhere = strsql.ToLower().IndexOf("where") + 5;
                int intorder = strsql.ToLower().IndexOf("order ");
                int intorderby = strsql.ToLower().Replace(" ", "").IndexOf("orderby") + 7;
                strR[2] = strsql.Substring(intselect, intfrom - 4 - intselect); //columnlist
                if (intwhere > 0)
                {
                    strR[3] = strsql.Substring(intfrom, intwhere - 5 - intfrom); //tablelist
                    if (intorderby > 0)
                    {
                        strR[4] = strsql.Substring(intwhere, intorder - intwhere);//where
                        strR[5] = strsql.ToLower().Replace(" ", "").Substring(intorderby);//orderby
                    }
                    else
                    {
                        strR[4] = strsql.Substring(intwhere);
                        strR[5] = "";
                    }

                }
                else
                {
                    strR[3] = strsql.Substring(intfrom);
                    strR[4] = "";
                    strR[5] = "";
                }
            }
            else
            {
               // strR[1] = strsql;
                strR[2] = txtFieldList.Text; //columnlist
                strR[3] = txtTableList.Text ;
                strR[4] =txtQueryCondition.Text;
                strR[5] = txtOrderBy.Text;
                strR[1]=" select " +strR[2]+ " from " + strR[3] + (strR[4].Trim().Length>0? "  where " + strR[4]:"") + (strR[5].Trim().Length>0?" order by "+strR[5]:"");
            }

            string strT = DbHelper.GetInstance().GetDataSetSQLTestResult(Convert.ToInt32(strR[0]), strR[1]);
            if (Convert.ToInt32 (strT) > 0)
            {
                ScriptManager.RegisterClientScriptBlock(btnOK , btnOK.GetType(), DateTime.Now.ToString().Replace(":", " "),   " window.returnValue=" + strR + ";window.close();", false);

            }
            else if (strT == "-1")
            {
         //      Response.Write("<script language='javascript'> alert('查询出现异常，请检查SQL语句！'); </script>");
            }
            else
            {
         //     Response.Write("<script language='javascript'> alert('查询不到任何数据，请检查SQL语句！'); </script>");

           }
        }*/

        [WebMethod]
        public static string  GetSelectRecords(string[] DataSourceIDandSql)
        {
            return "";
        }
    }
}
