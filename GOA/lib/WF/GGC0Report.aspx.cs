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
    public partial class GGC0Report : BasePage
    {
        SortedList alSum = new SortedList();//用于存储总计所在列及其值信息

        SortedList slPos = new SortedList(); //记录下要统计的行所在的列,以及列的显示名称
        SortedList slColName = new SortedList(); //记录每列,以及列的显示名称
        ArrayList strGroupBy=new ArrayList(); 
        //    ArrayList alsumValue;
        protected void Page_Load(object sender, EventArgs e)
        {
        if (!Page.IsPostBack)
            {
                //设置gridView的界面和数据
                BindGridView(GridView1 );
            }
        }

        #region GridView事件

        protected void GridView1_RowCreated(Object sender, GridViewRowEventArgs e)
            {
            //设置表头中文显示
            if (e.Row.RowType == DataControlRowType.Header && e.Row.Cells.Count == slColName.Count )
                {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                  {
                  e.Row.Cells[i].Text = slColName.GetValueList()[i].ToString();
                  }
                }
            }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                //设置总计
             if (e.Row.RowType == DataControlRowType.DataRow )
                {
                if (strGroupBy != null)
                    {
                    for (int i = 0; i < slPos.Count; i++)
                        {
                        if (alSum.Contains(i))
                            {
                            if (slPos.GetValueList()[i].ToString() == "2")
                                alSum.SetByIndex(i,Convert.ToInt32((alSum.GetValueList()[i].ToString())) + Convert.ToInt32(e.Row.Cells[Int32.Parse(slPos.GetKey(i).ToString())].Text));
                            else
                                alSum.SetByIndex(i, Convert.ToSingle(alSum.GetValueList()[i].ToString()) + Convert.ToSingle(e.Row.Cells[Int32.Parse(slPos.GetKey(i).ToString())].Text));
                            }
                        else
                        {
                        if (slPos.GetValueList()[i].ToString() == "2")
                            alSum.Add(i,  Convert.ToInt32(e.Row.Cells[Int32.Parse(slPos.GetKey(i).ToString())].Text));
                        else
                            alSum.Add(i,  Convert.ToSingle(e.Row.Cells[Int32.Parse(slPos.GetKey(i).ToString())].Text));
                            }
                        }
                    }
                }
            else if (e.Row.RowType == DataControlRowType.Footer )
                {
                if (strGroupBy != null)
                    {
                    if (Int32.Parse(slPos.GetKeyList()[0].ToString()) > 0)
                        {
                        e.Row.Cells[Int32.Parse(slPos.GetKeyList()[0].ToString())-1].Text = "总计";
                        }
                    for (int i = 0; i < slPos.Count; i++)
                        {
                        //  alSum.Add(slPos.GetKey(i),alsumValue[i] );
                        e.Row.Cells[Int32.Parse(slPos.GetKey(i).ToString())].Text = alSum.GetValueList()[i].ToString();
                        }
                    DataTable dt = (DataTable)ViewState["dtValue"];

                    DataRow dr = dt.NewRow();
                
                    for (int j = 0; j < e.Row.Cells.Count; j++)
                    {
                    if (e.Row.Cells[j].Text.ToString() == "&nbsp;" || e.Row.Cells[j].Text.ToString() == "总计")
                        dr[j] = System.DBNull.Value;
                    else
                        dr[j] = e.Row.Cells[j].Text;
                     }

                   dt.Rows.Add(dr);
                   ViewState["dtValue"] = dt;
                    }

                }
            }
        #endregion

        #region gridview UI
        //此类需要更改，主要是更改获取数据源的方法


        private void BindGridView(GridView gridView)
            {
            DataTable dt = new DataTable();
            string strCondition = Server.UrlDecode(DNTRequest.GetQueryString("strCondition"));
            string strColumns = Server.UrlDecode(DNTRequest.GetQueryString("strColumns"));
            string strOrder = Server.UrlDecode(DNTRequest.GetQueryString("strOrder"));
            int ReportID = DNTRequest.GetQueryInt("ReportID", 0);

            if (Server.UrlDecode(DNTRequest.GetQueryString("strGroupBy")) != "")
                {
                string[] strGroup = Server.UrlDecode(DNTRequest.GetQueryString("strGroupBy")).Replace("m.", "").Replace("d.", "").Split(',');
                for (int i = 0; i < strGroup.GetLength(0); i++)
                    strGroupBy.Add(strGroup[i]);
                }
            else
                strGroupBy = null;

            dt = DbHelper.GetInstance().GetWorkflow_UserDefinedReportSearchResult(ReportID, strCondition, strColumns, strOrder);
            DataTable dtColumn = DbHelper.GetInstance().GetWorkflow_UserDefinedReportDynamicDataTableByReportID(DNTRequest.GetQueryString("ReportID"));

            for (int j = 0; j < dt.Columns.Count; j++)
                {
                for (int i = 0; i < dtColumn.Rows.Count; i++)
                    {
                    if (dt.Columns[j].ColumnName == dtColumn.Rows[i]["FieldName"].ToString())
                        {
                        slColName.Add(j, dtColumn.Rows[i]["FieldLabelNoName"].ToString());

                        if (strGroupBy.Contains(dtColumn.Rows[i]["FieldName"].ToString()))
                            {
                            slPos.Add(j, dtColumn.Rows[i]["DataTypeID"]);
                            }

                        /// 在下面加入将Gridview DataSource中的ID换成Value的代码

                        string FieldID = dtColumn.Rows[i]["FieldID"].ToString();
                        string FieldName = dtColumn.Rows[i]["FieldName"].ToString();
                        int DataTypeID = Convert.ToInt32(dtColumn.Rows[i]["DataTypeID"]);
                        int FieldHTMLTypeID = Convert.ToInt32(dtColumn.Rows[i]["HTMLTypeID"].ToString());
                        int IsDynamic = Convert.ToInt32(dtColumn.Rows[i]["IsDynamic"].ToString());
                        int DataSetID = Convert.ToInt32(dtColumn.Rows[i]["DataSetID"].ToString());
                        string ValueColumn = dtColumn.Rows[i]["ValueColumn"].ToString();
                        string TextColumn = dtColumn.Rows[i]["TextColumn"].ToString();

                        ///处理Checkboxlist ,DropDownList,checkbox,TextBrowse的数据,将ID/Value换成Name/Text值

                        switch (FieldHTMLTypeID)
                            {
                            case 4:  //checkboxList
                                DataTable dtList = DbHelper.GetInstance().GetMultiSelectDataSource(Convert.ToInt32(FieldID), IsDynamic, DataSetID, ValueColumn, TextColumn);
                                if (dtList.Rows.Count > 0)
                                    {
                                    for (int k = 0; k < dt.Rows.Count; k++)
                                        {
                                        string FieldValue = dt.Rows[k][j].ToString();
                                        //根据FieldValue，取得相应的TextValue用以替换dt中的FieldValue
                                        if (FieldValue.Trim() != "")
                                            {
                                            if (FieldValue.Split(',').GetLength(0) == 1)//没有多个选项
                                                {
                                                if (IsDynamic == 0)
                                                    dt.Rows[k][j] = dtList.Select("selectNo= " + FieldValue)[0]["LabelWord"];
                                                else if (ValueColumn != "" && TextColumn != "")
                                                    dt.Rows[k][j] = dtList.Select(ValueColumn + "= " + FieldValue)[0][TextColumn];
                                                }
                                            else//选择多项
                                                {
                                                string[] alList = FieldValue.Split(',');
                                                string strR = "";

                                                if (IsDynamic == 0)
                                                    {
                                                    for (int l = 0; l < alList.GetLength(0); l++)
                                                        strR += dtList.Select("selectNo= " + alList[l])[0]["LabelWord"] + ",";
                                                    }
                                                else if (ValueColumn != "" && TextColumn != "")
                                                    {
                                                    for (int l = 0; l < alList.GetLength(0); l++)
                                                        strR += dtList.Select(ValueColumn + "= " + FieldValue)[0][TextColumn] + ",";
                                                    }
                                                strR = strR.Substring(0, strR.Length - 1);
                                                if (strR.Length > 0)
                                                    dt.Rows[k][j] = strR;
                                                }
                                            }
                                        }
                                    }
                                break;

                            case 6: // checkbox
                                for (int k = 0; k < dt.Rows.Count; k++)
                                    {
                                    string FieldValue = dt.Rows[k][j].ToString();
                                    //根据FieldValue，取得相应的TextValue用以替换dt中的FieldValue
                                    if (FieldValue == "1")
                                        {
                                        dt.Rows[k][j] = "是";
                                        }
                                    else
                                        {
                                        dt.Rows[k][j] = "否";
                                        }
                                    }
                                break;
                            case 5: //dropdownlist 

                                DataTable dtDrop = DbHelper.GetInstance().GetMultiSelectDataSource(Convert.ToInt32(FieldID), IsDynamic, DataSetID, ValueColumn, TextColumn);
                                if (dtDrop.Rows.Count > 0)
                                    {
                                    for (int k = 0; k < dt.Rows.Count; k++)
                                        {
                                        string FieldValue = dt.Rows[k][j].ToString();
                                        if (FieldValue.Trim() != "")
                                            if (IsDynamic == 0)
                                                dt.Rows[k][j] = dtDrop.Select("selectNo= " + FieldValue)[0]["LabelWord"];
                                            else if (ValueColumn != "" && TextColumn != "")
                                                dt.Rows[k][j] = dtDrop.Select(ValueColumn + "= " + FieldValue)[0][TextColumn];
                                        }
                                    }
                                break;
                            case 8:

                                string BrowseValueSql = dtColumn.Rows[i]["BrowseValueSql"].ToString();
                                string BrowseSqlParam = dtColumn.Rows[i]["BrowseSqlParam"].ToString();

                                for (int k = 0; k < dt.Rows.Count; k++)
                                    {
                                    string FieldValue = dt.Rows[k][j].ToString();
                                    if (FieldValue.Trim() != "")
                                        dt.Rows[k][j] = DbHelper.GetInstance().GetBrowseFieldText(BrowseValueSql, BrowseSqlParam, FieldValue);
                                    }
                                break;
                            }
                        ///end
                        }
                    }
                }

            ViewState["dtValue"] = dt;
            gridView.DataSource = dt;
            gridView.DataBind();
            BuildNoRecords(gridView, dt);
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
      
        #endregion

        protected void btnReturn_Click(object sender, EventArgs e)
            {
            Response.Redirect("GGC0Set.aspx?ReportID="+DNTRequest.GetQueryString("ReportID"));
            }

        protected void btnExcel_Click(object sender, EventArgs e)
            {

            BindGridView(GridView1);
            if (GridView1.Rows.Count > 0)
                {
              //  string reportName = DbHelper.GetInstance().GetWorkflow_ReportMainEntityByReportID(DNTRequest.GetQueryString("ReportID")).ReportName;
             //   string fileName = reportName + "_ToExcel.xls";

                string fileName =  "Report_ToExcel.xls";

                DataSet ds = new DataSet();
                ds.Merge((DataTable)(ViewState["dtValue"]));
                DataTable dtColumn = GetColoumns(ds.Tables[0]);
                CreateExcel(dtColumn, ds, "1", fileName, "True");
                }

            System.Web.UI.ScriptManager.RegisterStartupScript(btnExcel, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
            }

        private DataTable GetColoumns(DataTable dt)
            {  
            DataTable dtColumn = new DataTable();
            dtColumn.Columns.Add(new DataColumn("ColName", typeof(System.String)));
            dtColumn.Columns.Add(new DataColumn("ColDescriptionCN", typeof(System.String)));
            dtColumn.Columns.Add(new DataColumn("ColIsShow", typeof(System.String)));
            dtColumn.Columns.Add(new DataColumn("TypeName", typeof(System.String)));

            for (int i = 0; i < dt.Columns.Count; i++)
                {
                DataRow row = dtColumn.NewRow();
                row["ColName"] = dt.Columns[i].ColumnName;
                row["ColDescriptionCN"] = slColName.GetValueList()[i].ToString();
                row["ColIsShow"] = "1";
                string colType = dt.Columns[i].DataType.Name.ToString().ToLower();
                ArrayList aryInt = new ArrayList{ "byte", "int32", "float", "decimal", "int16", "double", "int64", "single" };


                if (colType == "datetime")
                    row["TypeName"] = "datetime";
                else if (aryInt.Contains(colType))
                    row["TypeName"] = "int";
                else
                    row["TypeName"] = "varchar";

                dtColumn.Rows.Add(row);
                }
          
           return dtColumn;

            }


    }
}
