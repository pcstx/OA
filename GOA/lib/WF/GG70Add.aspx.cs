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
    public partial class GG70Add : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                if (Request.QueryString["DataSetID"] != null && Request.QueryString["DataSetID"].ToString() != "")
                {
                    string keycol = Request.QueryString["DataSetID"];
                    ViewState["status"] = "Update";
                    SetControls(keycol);
                }
                else
                {
                    ViewState["status"] = "Add";
                    SetControls("0");
                }
            }
        }

        private void SetControls(string keycol)
        {

            Workflow_DataSetEntity _dse = DbHelper.GetInstance().GetWorkflow_DataSetEntityByKeyCol(keycol);

            if (_dse != null)
            {
                txtDataSetID.Value = _dse.DataSetID.ToString();
                txtDataSourceID.Value = _dse.DataSourceID.ToString();

                Workflow_DataSourceEntity _dsource = DbHelper.GetInstance().GetDataSourceByID(_dse.DataSourceID);
                txtDataSourceName.Text = _dsource.DataSourceName;
                txtDSName.Text = _dse.DataSetName;

                if (_dse.DataSetType == 1)//数据集类型为表查询语句
                {
                    setVisiablecontrol(1);

                    ddlDSType.SelectedValue = _dse.DataSetType.ToString();
                    txtFieldList.Text = _dse.FieldList;
                    txtTableList.Text = _dse.TableList;
                    txtQueryCondition.Text = _dse.QueryCondition;
                    txtOrder.Text = _dse.OrderBy;
                    txtSql.Text = _dse.QuerySql;
                    txtColumnsName.Text = _dse.ReturnColumnsName;
                    txtPK.Text = _dse.DataSetPKColumns;

                    BindDDLType(_dsource.DataSourceID, 1);
                    bindColumnList(_dsource.DataSourceID, ddlTable.SelectedValue);
                    bindParameterGridView1(_dsource.DataSourceID, _dse.QuerySql);
                }
                else if (_dse.DataSetType == 2) //数据集类型为存储过程
                {
                    setVisiablecontrol(2);

                    txtColumnsName.Text = _dse.ReturnColumnsName;
                    txtPK.Text = _dse.DataSetPKColumns;
                    txtSPCol.Text = _dse.ReturnColumns;
                    BindDDLType(_dsource.DataSourceID, 2);
                    bindParameterGridView(_dsource.DataSourceID, ddlTable.SelectedValue);
                }
            }
            else
            {
                txtDataSetID.Value = "";
                txtDataSourceID.Value = "";
                this.txtDSName.Text = "";
                txtDataSourceName.Text = "";
                ddlDSType.SelectedIndex = -1;
                ViewState["status"] = "Add";
                ddlTable.Enabled = false;
                fdTable.Visible = false;
                fdSP.Visible = false;
                lblTable.Text = "表名:";
                divColumn.Visible = false;

                txtFieldList.Text = "";
                txtTableList.Text = "";
                txtQueryCondition.Text = "";
                txtOrder.Text = "";
                txtSql.Text = "";
                txtColumnsName.Text = "";
                txtPK.Text = "";
                txtSPCol.Text = "";
            }
        }

        private void setVisiablecontrol(int type)
        {
            if (type == 2)
            {
                ddlTable.Enabled = true;
                fdTable.Visible = false;
                fdSP.Visible = true;
                lblTable.Text = "存储过程名:";
                divColumn.Visible = false;
            }
            else
            {
                ddlTable.Enabled = true;
                fdTable.Visible = true;
                fdSP.Visible = false;
                lblTable.Text = "表名:";
                divColumn.Visible = true;
            }
        }

        private void BindDDLType(int dataSourceID, int dataSetType)
        {
            if (dataSetType == 1)
            {
                ddlTable.DataSource = DbHelper.GetInstance().GetAllTableNames(dataSourceID);
                ddlTable.DataTextField = "TABLE_NAME";
                ddlTable.DataValueField = "TABLE_NAME";
                ddlTable.DataBind();
            }
            else if (dataSetType == 2)
            {
                ddlTable.DataSource = DbHelper.GetInstance().GetAllStoredProcedureNames(dataSourceID);
                ddlTable.DataTextField = "SP_NAME";
                ddlTable.DataValueField = "SP_NAME";
                ddlTable.DataBind();
            }
        }

        private void bindColumnList(int dataSourceID, string tableName)
        {
            this.cblColumn.DataSource = DbHelper.GetInstance().GetAllColumns(dataSourceID, tableName);
            cblColumn.DataTextField = "COLUMN_NAME";
            cblColumn.DataValueField = "COLUMN_NAME";
            cblColumn.DataBind();
        }


        private void bindParameterGridView(int dataSourceID, string SpName)
            {
            DataTable dt = new DataTable();
            if (txtDataSetID.Value != "")
                {
                dt = DbHelper.GetInstance().GetWorkflow_DataSetParameterEntityByKeyCol(txtDataSetID.Value);
                dt.Columns.Add(new DataColumn("ParameterDirectionN"));
                //   dt.Columns["ParameterDirection"].DefaultValue = "Input";
                }
            else if (dataSourceID != 0)
                {
                dt = DbHelper.GetInstance().GetAllStoredProcedureParameters(dataSourceID, SpName);
                dt.Columns.Add(new DataColumn("ParameterValue"));
                dt.Columns.Add(new DataColumn("ParameterDesc"));
                }
            this.GridView2.DataSource = dt;
            GridView2.DataBind();
            BuildNoRecords(GridView2, dt);
            }

        private void bindParameterGridView1(int dataSourceID, string QuerySql)
            {
            DataTable dt = new DataTable();
            if (txtDataSetID.Value != "")
                {
                dt = DbHelper.GetInstance().GetWorkflow_DataSetParameterEntityByKeyCol(txtDataSetID.Value);
                }
            else
                {
                dt.Columns.Add(new DataColumn("ParameterName"));
                dt.Columns.Add(new DataColumn("ParameterType"));
                dt.Columns.Add(new DataColumn("ParameterDirection"));
                dt.Columns.Add(new DataColumn("ParameterSize"));
                dt.Columns.Add(new DataColumn("ParameterValue"));
                dt.Columns.Add(new DataColumn("ParameterDesc"));
                }

            this.GridView1.DataSource = dt;
            GridView1.DataBind();
            BuildNoRecords(GridView1, dt);
            }

        //数据集类型改变时,改变相应控件值及状态

        protected void ddlDSType_SelectedIndexChanged(object sender, EventArgs e)
            {
            if (txtDataSourceID.Value.Trim() == "")
                {
                string strScript = "<script type='text/javascript'  lanuage='javascript'> alert('请先选择数据源!'); </script>";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", strScript, false);
                return;
                }
            else
                {
                BindDDLType(Int32.Parse(txtDataSourceID.Value.Trim()), Int32.Parse(ddlDSType.SelectedValue));
                setVisiablecontrol(Int32.Parse(ddlDSType.SelectedValue));
                }
            }

        protected void ddlTable_SelectedIndexChanged(object sender, EventArgs e)
            {
            if (txtDataSourceID.Value.Trim() == "")
                {
                string strScript = "<script type='text/javascript'  lanuage='javascript'> alert('请先选择数据源!'); </script>";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", strScript, false);
                return;
                }
            else
                {
                int dsid = Int32.Parse(txtDataSourceID.Value.Trim());

                if (ddlDSType.SelectedValue == "1")
                    {
                    setVisiablecontrol(1);
                    bindColumnList(dsid, ddlTable.SelectedValue);
                    }

                else if ((ddlDSType.SelectedValue == "2"))
                    {
                    setVisiablecontrol(2);
                    bindParameterGridView(dsid, ddlTable.SelectedValue);
                    }
                else
                    {
                    setVisiablecontrol(0);
                    }
                }
            }

        //Show Header/Footer of Gridview with Empty Data Source 
        private void BuildNoRecords(GridView gridView, DataTable ds)
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

        protected void btnSelect_OnClick(object sender, EventArgs e)
            {
            string strColumn = txtFieldList.Text;
            string strtable = ddlTable.SelectedValue;
            for (int i = 0; i < cblColumn.Items.Count; i++)
                {
                if (cblColumn.Items[i].Selected == true)
                    {
                    strColumn = (strColumn != "" ? strColumn + "," + strtable + '.' + cblColumn.Items[i].Value : strtable + '.' + cblColumn.Items[i].Value);
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

            System.Web.UI.ScriptManager.RegisterStartupScript(btnSelect, this.GetType(), "ButtonHideScript", strButtonHideScript, false);

            }

        protected void ClearCheckboxlist(object sender, EventArgs e)
            {
            for (int i = 0; i < cblColumn.Items.Count; i++)
                {
                cblColumn.Items[i].Selected = false;
                }
            this.txtFieldList.Text = "";
            this.txtOrder.Text = "";
            this.txtQueryCondition.Text = "";
            this.txtTableList.Text = "";
            this.txtSql.Text = "";

            }

        #region "save data"

        protected void btnOK_Click(object sender, EventArgs e)
            {
            if (ddlDSType.SelectedValue == "1")
                {
                SaveSqlStringData();
                }
            else
                {
                SaveSPData();
                }
            System.Web.UI.ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
            }

        private void SaveSPData()
            {
            Workflow_DataSetEntity _dse = new Workflow_DataSetEntity();
            _dse.DataSetID = (txtDataSetID.Value == null || txtDataSetID.Value == "") ? 0 : Convert.ToInt32(txtDataSetID.Value);
            _dse.DataSourceID = Convert.ToInt32(txtDataSourceID.Value);
            _dse.DataSetName = this.txtDSName.Text.Trim();
            _dse.DataSetType = Int32.Parse(ddlDSType.SelectedValue);
            _dse.TableList = ddlTable.SelectedValue;

            _dse.DataSetPKColumns = txtPK.Text.Trim();
            _dse.ReturnColumnsName = txtColumnsName.Text.Trim();

            _dse.QueryCondition = "";

            //--------以下为得到返回列的列表 及查询条件值
            int gvRowCnt = GridView2.Rows.Count;
            if (gvRowCnt == 1 && GridView2.Rows[0].Cells[0].ColumnSpan == 7)
                {
                gvRowCnt = 0;
                }

            Workflow_DataSetParameterEntity[] param = new Workflow_DataSetParameterEntity[gvRowCnt];
            string queryCondition = "";
            for (int i = 0; i < gvRowCnt; i++)
                {
                Workflow_DataSetParameterEntity para = new Workflow_DataSetParameterEntity();

                para.ParameterName = GridView2.Rows[i].Cells[0].Text.ToString();
                para.ParameterType = GridView2.Rows[i].Cells[1].Text;
                para.ParameterDirection = GridView2.Rows[i].Cells[2].Text;
                para.ParameterSize = Convert.ToInt32(GridView2.Rows[i].Cells[3].Text);
                para.ParameterValue = ((System.Web.UI.WebControls.TextBox)GridView2.Rows[i].FindControl("txtSelect")).Text;
                para.ParameterDesc = ((System.Web.UI.WebControls.TextBox)GridView2.Rows[i].FindControl("txtDesc")).Text;
                queryCondition = (queryCondition == "") ? ("'" + para.ParameterValue + "'") : (queryCondition + ",'" + para.ParameterValue + "'");
                param[i] = para;
                }

            DataTable dt = new DataTable();
            dt = DbHelper.GetInstance().GetDataSetSPReturnColumns(_dse.DataSourceID, _dse.TableList, param);

            string rc = "";
            foreach (DataColumn dc in dt.Columns)
                {
                rc = (rc == "" ? dc.ColumnName : rc + "," + dc.ColumnName);
                }

            _dse.ReturnColumns = rc;
            _dse.QueryCondition = queryCondition;

            //、================

            if (_dse.ReturnColumns.Contains(_dse.DataSetPKColumns) && (_dse.ReturnColumns.Split(',').GetLength(0) == _dse.ReturnColumnsName.Split(',').GetLength(0)))
                {

                if (ViewState["status"].ToString() == "Add")
                    {

                    int DataSetID = DbHelper.GetInstance().AddWorkflow_DataSetSP(_dse);

                    if (DataSetID > 0)
                        {

                        DbHelper.GetInstance().DeleteWorkflow_DataSetParameter(DataSetID);

                        foreach (Workflow_DataSetParameterEntity _dspe in param)
                            {
                            _dspe.DataSetID = DataSetID;
                            DbHelper.GetInstance().AddWorkflow_DataSetParameter(_dspe);
                            }
                        }

                    }
                else if (ViewState["status"].ToString() == "Update")
                    {
                    int r = DbHelper.GetInstance().UpdateWorkflow_DataSetForStoredProcedure(_dse);
                    if (r > 0)
                        {

                        DbHelper.GetInstance().DeleteWorkflow_DataSetParameter(_dse.DataSetID);
                        foreach (Workflow_DataSetParameterEntity _dspe in param)
                            {
                            _dspe.DataSetID = _dse.DataSetID;
                            DbHelper.GetInstance().AddWorkflow_DataSetParameter(_dspe);
                            }
                        }
                    }
                }
            else
                {
                string strScript = "<script type='text/javascript'  lanuage='javascript'> alert('数据集PK不在返回字段中，或字段描述与返回字段个数不一致，请确认！'); </script>";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", strScript, false);
                }
            }

        private string[] GenerateSQLArray()
            {
            string strsql = "";
            string[] strR = new string[6]; //用作返回数组
            strR[0] = txtDataSourceID.Value.Trim();
            if (Utils.ClearEnterAndBR(txtSql.Text.Trim()).Trim() != "") //如果有自定义查询语句,只需用这一Textbox中的
                {
                strsql = Utils.ClearEnterAndBR(txtSql.Text.Trim());
                strR[1] = strsql;
                int intselect = strsql.ToLower().IndexOf("select") + 6;
                int intfrom = strsql.ToLower().IndexOf("from") + 4;
                int intwhere = strsql.ToLower().LastIndexOf("where");
                int intorder = strsql.ToLower().IndexOf("order ", (intwhere == -1 ? intfrom : intwhere));
                int intorderby = strsql.ToLower().Replace(" ", "").IndexOf("orderby", (intwhere == -1 ? intfrom : intwhere));

                strR[2] = strsql.Substring(intselect, intfrom - 4 - intselect); //columnlist
                if (intwhere > 0) //有Where
                    {
                    strR[3] = strsql.Substring(intfrom, intwhere - intfrom); //tablelist
                    if (intorderby > 0)  //有Orderby
                        {
                        strR[4] = strsql.Substring(intwhere, intorder - intwhere - 5);//where
                        strR[5] = strsql.ToLower().Replace(" ", "").Substring(intorderby + 7);//orderby
                        }
                    else
                        {
                        strR[4] = strsql.Substring(intwhere + 5);
                        strR[5] = "";
                        }
                    }
                else//无
                    {
                    if (intorderby > 0)  //有Orderby
                        {
                        strR[3] = strsql.Substring(intfrom, intorder - intfrom);//tablelist
                        strR[4] = "";//where
                        strR[5] = strsql.ToLower().Replace(" ", "").Substring(intorderby + 7);//orderby
                        }
                    else
                        {
                        strR[3] = strsql.Substring(intfrom);
                        strR[4] = "";
                        strR[5] = "";
                        }
                    }
                }
            else
                {
                strR[2] = Utils.ClearEnterAndBR(txtFieldList.Text); //columnlist
                strR[3] = Utils.ClearEnterAndBR(txtTableList.Text);
                strR[4] = Utils.ClearEnterAndBR(txtQueryCondition.Text);
                strR[5] = Utils.ClearEnterAndBR(txtOrder.Text);
                strR[1] = " select " + strR[2] + " from " + strR[3] + (strR[4].Trim().Length > 0 ? "  where " + strR[4] : "") + (strR[5].Trim().Length > 0 ? " order by " + strR[5] : "");
                }
            return strR;
            }

        private void SaveSqlStringData()
            {

            string[] strR = GenerateSQLArray();

            //---begin-----增加查询参数 2010-05-04
            int gvRowCnt = GridView1.Rows.Count;
            if (gvRowCnt == 1 && GridView1.Rows[0].Cells[0].ColumnSpan == 7)
                {
                gvRowCnt = 0;
                }

            Workflow_DataSetParameterEntity[] param = new Workflow_DataSetParameterEntity[gvRowCnt];
            for (int i = 0; i < gvRowCnt; i++)
                {
                Workflow_DataSetParameterEntity para = new Workflow_DataSetParameterEntity();

                para.ParameterName = ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("txtName")).Text.ToString();
                para.ParameterType = ((System.Web.UI.WebControls.DropDownList)GridView1.Rows[i].FindControl("ddlType")).SelectedValue;
                para.ParameterDirection = ((System.Web.UI.WebControls.DropDownList)GridView1.Rows[i].FindControl("ddlDirection")).SelectedValue;
                para.ParameterSize = Convert.ToInt32(((GPRP.GPRPControls.TextBox)GridView1.Rows[i].FindControl("txtSize")).Text);
                para.ParameterValue = ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("txtValue")).Text;
                para.ParameterDesc = ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("txtDesc1")).Text;

                param[i] = para;
                }

            //---end---------------------

            //-----------判断查询语句能否得到值：能，则可进行增加、修改操作；不能，则提示之--------------------
            //string strT = DbHelper.GetInstance().GetDataSetSQLTestResult(Convert.ToInt32(strR[0]), strR[1], param);

            //if (Convert.ToInt32(strT) > 0)
            //    {
                Workflow_DataSetEntity _dse = new Workflow_DataSetEntity();
                _dse.DataSetID = (txtDataSetID.Value == null || txtDataSetID.Value == "") ? 0 : Convert.ToInt32(txtDataSetID.Value);
                _dse.DataSourceID = Convert.ToInt32(txtDataSourceID.Value);
                _dse.DataSetName = this.txtDSName.Text.Trim();
                _dse.DataSetType = Int32.Parse(ddlDSType.SelectedValue);
                _dse.TableList = strR[3];
                _dse.FieldList = strR[2];
                _dse.QueryCondition = strR[4];
                _dse.OrderBy = strR[5];
                _dse.QuerySql = strR[1];

                _dse.DataSetPKColumns = txtPK.Text.Trim();
                _dse.ReturnColumnsName = txtColumnsName.Text.Trim();

                //最终返回的字段，执行后返回列名。

                DataTable dt = new DataTable();
                dt = DbHelper.GetInstance().GetDataSetSQLReturnColumns(_dse.DataSourceID, " select " + strR[2] + " from " + strR[3]);

                string rc = "";
                foreach (DataColumn dc in dt.Columns)
                    {
                    rc = (rc == "" ? dc.ColumnName : rc + "," + dc.ColumnName);
                    }

                _dse.ReturnColumns = rc;

                if (_dse.ReturnColumns.Contains(_dse.DataSetPKColumns) && (_dse.ReturnColumns.Split(',').GetLength(0) == _dse.ReturnColumnsName.Split(',').GetLength(0)))
                    {
                    if (ViewState["status"].ToString() == "Add")
                        {
                        int intAdd = DbHelper.GetInstance().AddWorkflow_DataSet(_dse);
                        if (intAdd > 0)
                            {
                            //增加DataSet参数,先删除，再新加

                            if (param.GetLength(0) > 0)
                                {
                                DbHelper.GetInstance().DeleteWorkflow_DataSetParameter(intAdd);

                                foreach (Workflow_DataSetParameterEntity _dspe in param)
                                    {
                                    _dspe.DataSetID = intAdd;
                                    DbHelper.GetInstance().AddWorkflow_DataSetParameter(_dspe);
                                    }
                                }
                            string strScript = "<script type='text/javascript'  lanuage='javascript'> alert('添加成功！'); </script>";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", strScript, false);
                            }
                        else
                            {

                            }
                        }
                    else if (ViewState["status"].ToString() == "Update")
                        {
                        int intAdd = DbHelper.GetInstance().UpdateWorkflow_DataSet(_dse);

                        if (intAdd > 0)
                            {
                            //增加DataSet参数
                            if (param.GetLength(0) > 0)
                                {
                                DbHelper.GetInstance().DeleteWorkflow_DataSetParameter(_dse.DataSetID);
                                foreach (Workflow_DataSetParameterEntity _dspe in param)
                                    {
                                    _dspe.DataSetID = _dse.DataSetID;
                                    DbHelper.GetInstance().AddWorkflow_DataSetParameter(_dspe);
                                    }
                                }
                            string strScript = "<script type='text/javascript'  lanuage='javascript'> alert('修改成功！'); </script>";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", strScript, false);
                            }
                        else
                            {

                            }
                        }

                    }
                else
                    {
                    string strScript = "<script type='text/javascript'  lanuage='javascript'> alert('数据集PK不在返回字段中，或字段描述与返回字段个数不一致，请确认！'); </script>";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", strScript, false);
                    }
           //     }

            //else if (strT == "-1")
            //    {
            //    string strScript = "<script type='text/javascript'  lanuage='javascript'> alert('查询出现异常，请检查SQL语句！'); </script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", strScript, false);
            //    }
            //else
            //    {
            //    string strScript = "<script type='text/javascript'  lanuage='javascript'> alert('查询不到任何数据，请检查SQL语句！'); </script>";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", strScript, false);

       //      }
            }

        #endregion


        #region gridView1 事件

        //此类要进行dorpdownlist/chk控件的转换


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
            {
            if (e.Row.RowType == DataControlRowType.DataRow)
                {
                System.Web.UI.WebControls.DropDownList ddl = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ddlType");

                DataTable dt = new DataTable();
                dt = DbHelper.GetInstance().GetAllDataBaseDataType(Int32.Parse(txtDataSourceID.Value));
                ddl.DataSource = dt;
                ddl.DataTextField = "DATA_TYPE";
                ddl.DataValueField = "DATA_TYPE";
                ddl.DataBind();


                System.Web.UI.WebControls.DropDownList ddlD = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ddlDirection");
                DataTable dt2 = new DataTable();
                dt2 = DbHelper.GetInstance().ExecDataTable("select * from Workflow_DataSetParameterDirection");
                ddlD.DataSource = dt2;
                ddlD.DataTextField = "DirectionName";
                ddlD.DataValueField = "DirectionID";
                ddlD.DataBind();

                DataRowView drv = (DataRowView)e.Row.DataItem;
                if (ddl.Items.Contains(new ListItem(drv.Row["ParameterType"].ToString())))
                    ddl.SelectedValue = drv.Row["ParameterType"].ToString();

                if (ddlD.Items.Contains(new ListItem(drv.Row["ParameterDirection"].ToString())))
                    ddlD.SelectedValue = drv.Row["ParameterDirection"].ToString();

                }
            }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
            {
            int index = -1;
            if (e.CommandName == "deleterow")
                {
                index = Convert.ToInt32(e.CommandArgument);   //获取行号

                DataTable dt = new DataTable();

                dt.Columns.Add(new DataColumn("ParameterName"));
                dt.Columns.Add(new DataColumn("ParameterType"));
                dt.Columns.Add(new DataColumn("ParameterDirection"));
                dt.Columns.Add(new DataColumn("ParameterSize"));
                dt.Columns.Add(new DataColumn("ParameterValue"));
                dt.Columns.Add(new DataColumn("ParameterDesc"));

                for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                    if (i != index)
                        {
                        GridViewRow gRow = GridView1.Rows[i];
                        DataRow newRow = dt.NewRow();
                        newRow[0] = ((System.Web.UI.WebControls.TextBox)gRow.FindControl("txtName")).Text;
                        newRow[1] = ((System.Web.UI.WebControls.DropDownList)gRow.FindControl("ddlType")).SelectedValue;
                        newRow[2] = ((System.Web.UI.WebControls.DropDownList)gRow.FindControl("ddlDirection")).SelectedValue;
                        newRow[3] = ((GPRP.GPRPControls.TextBox)gRow.FindControl("txtSize")).Text;
                        newRow[4] = ((System.Web.UI.WebControls.TextBox)gRow.FindControl("txtValue")).Text;
                        newRow[5] = ((System.Web.UI.WebControls.TextBox)gRow.FindControl("txtDesc1")).Text;
                        dt.Rows.Add(newRow);
                        }
                    }
                dt.AcceptChanges();
                this.GridView1.DataSource = dt;
                this.GridView1.DataBind();
                }
            }
        #endregion

        protected void btnClear_Click(object sender, EventArgs e)
            {
            SetControls("0");
            }

        protected void btnParam_Click(object sender, EventArgs e)
            {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("ParameterName"));
            dt.Columns.Add(new DataColumn("ParameterType"));
            dt.Columns.Add(new DataColumn("ParameterDirection"));
            dt.Columns.Add(new DataColumn("ParameterSize"));
            dt.Columns.Add(new DataColumn("ParameterValue"));
            dt.Columns.Add(new DataColumn("ParameterDesc"));


            for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                GridViewRow gRow = GridView1.Rows[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((System.Web.UI.WebControls.TextBox)gRow.FindControl("txtName")).Text;
                newRow[1] = ((System.Web.UI.WebControls.DropDownList)gRow.FindControl("ddlType")).SelectedValue;
                newRow[2] = ((System.Web.UI.WebControls.DropDownList)gRow.FindControl("ddlDirection")).SelectedValue;
                newRow[3] = ((GPRP.GPRPControls.TextBox)gRow.FindControl("txtSize")).Text;
                newRow[4] = ((System.Web.UI.WebControls.TextBox)gRow.FindControl("txtValue")).Text;
                newRow[5] = ((System.Web.UI.WebControls.TextBox)gRow.FindControl("txtDesc1")).Text;
                dt.Rows.Add(newRow);
                }
            dt.AcceptChanges();

            DataRow newRow2 = dt.NewRow();
            newRow2["ParameterValue"] = "";
            newRow2["ParameterSize"] = 0;
            newRow2["ParameterName"] = "";
            newRow2["ParameterDesc"] = "";

            dt.Rows.Add(newRow2);
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();

            System.Web.UI.ScriptManager.RegisterStartupScript(btnParam, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
            }
       

    }
}

