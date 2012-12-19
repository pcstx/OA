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
    public partial class GG60Select : System.Web.UI.Page
    {
        private static string dbType = "";
        private static string conStr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string strDBtype = DNTRequest.GetString("dbtype");
                int dsid = DNTRequest.GetInt("dsid", 0);

                string strCon = "-1";
                Workflow_DataSourceEntity _dse = DbHelper.GetInstance().GetDataSourceByID(dsid);
                strCon = _dse != null ? _dse.ConnectString : "-1";

                dbType = strDBtype;
                conStr = strCon;

                hiddenConnectString.Value = strCon;
                hiddenConnectStringOriginal.Value = strCon;
                if (strDBtype == "Oracle")
                {
                    divDB.Visible = false;
                    if (strCon != "-1")
                    {
                        string[] strsplit = strCon.Split(new Char[] { ';' });
                        txtDBName.Text = (strsplit[0].ToString()).Split(new Char[] { '=' })[1].ToString();
                        txtUserName.Text = (strsplit[1].ToString()).Split(new Char[] { '=' })[1].ToString();
                        txtPassword.Text = (strsplit[2].ToString()).Split(new Char[] { '=' })[1].ToString();
                    }
                }
                else
                {
                    if (strCon != "-1")
                    {
                        this.divDB.Visible = true;
                        string[] strsplit = strCon.Split(new Char[] { ';' });
                        txtDBName.Text = (strsplit[0].ToString()).Split(new Char[] { '=' })[1].ToString();
                        txtUserName.Text = (strsplit[3].ToString()).Split(new Char[] { '=' })[1].ToString();
                        txtPassword.Text = (strsplit[4].ToString()).Split(new Char[] { '=' })[1].ToString();

                        bindDDLDatabaseName(txtDBName.Text.Trim(), txtUserName.Text.Trim(), txtPassword.Text.Trim());
                        string strDBName = (strsplit[1].ToString()).Split(new Char[] { '=' })[1].ToString();

                        if (ddlDBName.Items.Contains(new ListItem(strDBName)))
                            ddlDBName.SelectedValue = strDBName;
                        else
                            ddlDBName.SelectedIndex = -1;
                    }
                }
            }
        }

        private void bindDDLDatabaseName(string dataSource, string userName, string password)
        {
            DataTable dt = new DataTable();
            dt = DbHelper.GetInstance().GetAllDatabaseNameByDataSourceUserNamePassword(dataSource, userName, password);
            if (dt != null)
            {
                ddlDBName.DataSource = dt;
                ddlDBName.DataTextField = "name";
                ddlDBName.DataValueField = "name";
                ddlDBName.DataBind();
                hiddenConnectString.Value = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", this.txtDBName.Text.Trim(), this.ddlDBName.SelectedValue, txtUserName.Text.Trim(), txtPassword.Text.Trim());
            }
            else
            {
                hiddenConnectString.Value = "-1";
                lblMsg.Text = "连接失败！";

            }

        }

        protected void onTextChanged(object sender, EventArgs e)
        {
            if (dbType != "Oracle")
            {
                if (txtDBName.Text.Trim() != "" && txtPassword.Text.Trim() != "" && txtUserName.Text.Trim() != "")
                {
                    bindDDLDatabaseName(txtDBName.Text.Trim(), txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }

            }

        }

        protected void btnConnect_Click(object sender, EventArgs e)
        {
            string strR = ConnectTest();
            if (strR == "1")
            {
                if ((dbType != "Oracle") && ((conStr == "-1")))
                {
                    divDB.Visible = true;
                    lblMsg.Text = "连接成功，请选择数据库。";
                }
                else
                {
                    lblMsg.Text = "连接成功！";
                }
            }
            else
                lblMsg.Text = "连接失败！";
        }

        private string ConnectTest()
        {
            string strCon, strsql;
            //If the DB type is Oracle,then select all databases'data from the master database
            if (dbType == "Oracle")
            {
                strCon = string.Format("Data Source={0};User ID={1};Password={2}", this.txtDBName.Text.Trim(), txtUserName.Text.Trim(), txtPassword.Text.Trim());
                strsql = "select count(*) from v$database";
                try
                {
                    DbHelper.GetInstance(dbType, strCon).ExecSqlResult(strsql);
                    hiddenConnectString.Value = strCon;
                    return "1";
                }
                catch
                {
                    return "0";
                }
            }
            else
            {
                //if the DB type is sqlserver, then select data from the v$database view
                if (conStr == "-1")
                {
                    strCon = string.Format("Data Source={0};Initial Catalog=master;Persist Security Info=True;User ID={1};Password={2}", this.txtDBName.Text.Trim(), txtUserName.Text.Trim(), txtPassword.Text.Trim());
                    strsql = "select * from sys.databases";
                    try
                    {
                        DbHelper.GetInstance(dbType, strCon).ExecDataTable(strsql);
                        bindDDLDatabaseName(txtDBName.Text.Trim(), txtUserName.Text.Trim(), txtPassword.Text.Trim());
                        hiddenConnectString.Value = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", this.txtDBName.Text.Trim(), this.ddlDBName.SelectedValue, txtUserName.Text.Trim(), txtPassword.Text.Trim());

                        return "1";
                    }
                    catch
                    {
                        return "0";
                    }
                }
                else
                {
                    strCon = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", this.txtDBName.Text.Trim(), ddlDBName.SelectedValue.Trim(), txtUserName.Text.Trim(), txtPassword.Text.Trim());
                    strsql = "select * from sys.databases";
                    try
                    {
                        DbHelper.GetInstance(dbType, strCon).ExecDataTable(strsql);
                        hiddenConnectString.Value = strCon;
                        return "1";
                    }
                    catch
                    {
                        return "0";
                    }
                }
            }
        }

        protected void ddlDBName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            hiddenConnectString.Value = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", this.txtDBName.Text.Trim(), this.ddlDBName.SelectedValue, txtUserName.Text.Trim(), txtPassword.Text.Trim());
        }
    }
}
