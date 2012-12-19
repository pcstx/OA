using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
  
namespace GOA
{
    public partial class nodeCondition : BasePage
    {
        public static string resultsql = "(1=1)";
        public static string condition = "";
       
        public List<Workflow_NodeConditionEntity> List_nodeCondition = new List<Workflow_NodeConditionEntity>();
        public static DataTable dtCon;
        public static int icount = 1;         
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                dtCon = new DataTable(); 
                createBT();

                DataTable dtConditonSeq = new DataTable();
                dtConditonSeq.Columns.Add("BranchBatchSeq", typeof(System.Int32));
                dtConditonSeq.Columns.Add("FieldID", typeof(System.Int32));
                dtConditonSeq.Columns.Add("SymbolCode", typeof(System.String));
                dtConditonSeq.Columns.Add("CompareToValue", typeof(System.String));
                dtConditonSeq.Columns.Add("AndOr", typeof(System.String));
                DataRow dr = dtConditonSeq.NewRow();
                dr["BranchBatchSeq"] = 1;
                dr["FieldID"] = 0;
                dr["SymbolCode"] = "";
                dr["CompareToValue"] = "";
                dr["AndOr"] = "";
                dtConditonSeq.Rows.Add(dr);
                ViewState["dtConditonSeq"] = dtConditonSeq;
                BindGridView();
                BindGridView2();
            }


            DataTable dt = (DataTable)ViewState["dtConditonSeq"];
            dt.Rows.Clear();
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                string Prefix = "GridView2$ctl" + (i + 2).ToString().PadLeft(2, '0') + "$";
                DataRow dr = dt.NewRow();
                dr["BranchBatchSeq"] = i + 1;
                dr["FieldID"] = DNTRequest.GetInt(Prefix + "FieldID", 0);
                dr["SymbolCode"] = DNTRequest.GetString(Prefix + "SymbolCode");
                dr["CompareToValue"] = DNTRequest.GetString(Prefix + "CompareToValue");
                dr["AndOr"] = DNTRequest.GetString(Prefix + "AndOr");
                dt.Rows.Add(dr);
            }
        }

        #region gridview 绑定
        //此类需要更改，主要是更改获取数据源的方法
                 private void createBT()        {
            DataColumn mycol = new DataColumn();
            dtCon.Columns.Add(new DataColumn("ConditionID", typeof(Int32)));
            dtCon.Columns.Add(new DataColumn("BatchSeq", typeof(Int32)));
            dtCon.Columns.Add(new DataColumn("BranchBatchSeq", typeof(Int32)));
            dtCon.Columns.Add(new DataColumn("LinkID", typeof(Int32)));
            dtCon.Columns.Add(new DataColumn("FieldID", typeof(Int32)));
            dtCon.Columns.Add(new DataColumn("SymbolCode", typeof(String)));
            dtCon.Columns.Add(new DataColumn("CompareToValue", typeof(String)));
            dtCon.Columns.Add(new DataColumn("AndOr", typeof(String)));
            dtCon.Columns.Add(new DataColumn("CreateDate", typeof(DateTime)));

            dtCon.Columns.Add(new DataColumn("SymbolName", typeof(String)));
            dtCon.Columns.Add(new DataColumn("FieldLabel", typeof(String))); 

            dtCon.AcceptChanges();
            dtCon.PrimaryKey = new DataColumn[] { dtCon.Columns[0] };
            dtCon.AcceptChanges();
        }
        private void BindGridView()  //下面的grid
        {
         //   DataTable dt = DbHelper.GetInstance().GetDBRecords("a.*,b.SymbolName,e.FieldLabel", "Workflow_NodeCondition a,Workflow_CompareSymbol b,Workflow_NodeLink c,Workflow_Base d,Workflow_FormField e", "a.SymbolCode=b.SymbolCode and a.LinkID=c.LinkID and c.WorkflowID=d.WorkflowID and d.FormID=e.FormID and a.FieldID=e.FieldID and a.LinkID=" + DNTRequest.GetString("id"), "a.BatchSeq,a.BranchBatchSeq");
            GridView1.DataSource = dtCon;
            GridView1.DataBind();
            BuildNoRecords(GridView1, dtCon);  //没有数据效果
        }

        private void BindGridView2()
        {
            DataTable dtConditonSeq = (DataTable)ViewState["dtConditonSeq"];
            GridView2.DataSource = dtConditonSeq;
            GridView2.DataBind();
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
        #endregion

        #region gridView 事件 --类型
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                int index = Convert.ToInt32(e.CommandArgument);   //获取行号
                string ConditionID = GridView1.DataKeys[index][0].ToString().Trim();
                DbHelper.GetInstance().DeleteWorkflow_NodeCondition(ConditionID);
                UpdateSqlCondition();
            }
            BindGridView();
        }
        //此类要进行dorpdownlist/chk控件的转换
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
          
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
            }
            BindGridView();
        }
        //此类要进行dorpdownlist/chk控件的转换
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.DropDownList ddlFieldID = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("FieldID");
                ddlFieldID.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FieldID"));

                System.Web.UI.WebControls.DropDownList ddlSymbolCode = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("SymbolCode");
                ddlSymbolCode.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "SymbolCode"));

                System.Web.UI.WebControls.DropDownList ddlAndOr = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("AndOr");
                ddlAndOr.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "AndOr"));
            }
        }

        #endregion

        protected void AndOr_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtConditonSeq = (DataTable)ViewState["dtConditonSeq"];
            dtConditonSeq.Rows.Clear();

            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                string Prefix = "GridView2$ctl" + (i + 2).ToString().PadLeft(2, '0') + "$";
                DataRow dr = dtConditonSeq.NewRow();
                dr["BranchBatchSeq"] = i + 1;
                dr["FieldID"] = DNTRequest.GetInt(Prefix + "FieldID", 0);
                dr["SymbolCode"] = DNTRequest.GetString(Prefix + "SymbolCode");
                dr["CompareToValue"] = DNTRequest.GetString(Prefix + "CompareToValue");
                dr["AndOr"] = DNTRequest.GetString(Prefix + "AndOr");
                dtConditonSeq.Rows.Add(dr);
            }

            System.Web.UI.WebControls.DropDownList ddlAndOr = (System.Web.UI.WebControls.DropDownList)sender;
            int RowIndex = Convert.ToInt32(ddlAndOr.ClientID.Replace("GridView2_ctl", "").Replace("_AndOr", "")) - 1;
            if (ddlAndOr.SelectedValue == "" && RowIndex < dtConditonSeq.Rows.Count - 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "submit", "alert('取消一分支，应从最后一行开始');", true);
            }
            else if (ddlAndOr.SelectedValue == "" && RowIndex == dtConditonSeq.Rows.Count - 1)
            {
                dtConditonSeq.Rows.RemoveAt(RowIndex);
            }
            else if (ddlAndOr.SelectedValue != "" && RowIndex == dtConditonSeq.Rows.Count)
            {
                DataRow dr = dtConditonSeq.NewRow();
                dr["BranchBatchSeq"] = dtConditonSeq.Rows.Count + 1;
                dr["FieldID"] = 0;
                dr["SymbolCode"] = "";
                dr["CompareToValue"] = "";
                dr["AndOr"] = "";
                dtConditonSeq.Rows.Add(dr);
            }
            ViewState["dtConditonSeq"] = dtConditonSeq;
            BindGridView();
            BindGridView2();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
             
            DataTable dtConditonSeq = (DataTable)ViewState["dtConditonSeq"];
            //addCondition(dtConditonSeq);

            Workflow_NodeConditionEntity _NodeConditionEntity = new Workflow_NodeConditionEntity();
           // _NodeConditionEntity.LinkID = DNTRequest.GetInt("id", 0);
         //   DataTable dtMaxSeq = DbHelper.GetInstance().GetDBRecords("MaxBatchSeq=isnull(max(BatchSeq),0)+1", "Workflow_NodeCondition", "LinkID=" + DNTRequest.GetString("id"), "");
          //  _NodeConditionEntity.BatchSeq = Convert.ToInt32(dtMaxSeq.Rows[0]["MaxBatchSeq"]);

            for (int i = 0; i < dtConditonSeq.Rows.Count; i++)
            {
                _NodeConditionEntity.BranchBatchSeq = Convert.ToInt32(dtConditonSeq.Rows[i]["BranchBatchSeq"]);
                _NodeConditionEntity.FieldID = Convert.ToInt32(dtConditonSeq.Rows[i]["FieldID"]);
                _NodeConditionEntity.SymbolCode = dtConditonSeq.Rows[i]["SymbolCode"].ToString();
                _NodeConditionEntity.CompareToValue = dtConditonSeq.Rows[i]["CompareToValue"].ToString();
                _NodeConditionEntity.AndOr = dtConditonSeq.Rows[i]["AndOr"].ToString();

                DataRow myrow =dtCon.NewRow();
                myrow["ConditionID"] = dtCon.Rows.Count + 1;
                myrow["BatchSeq"] = icount;
                myrow["BranchBatchSeq"] = _NodeConditionEntity.BranchBatchSeq;
                myrow["LinkID"] = _NodeConditionEntity.LinkID;
                myrow["FieldID"] = _NodeConditionEntity.FieldID;
                myrow["SymbolCode"] = _NodeConditionEntity.SymbolCode;
                myrow["CompareToValue"] = _NodeConditionEntity.CompareToValue;
                myrow["AndOr"] = _NodeConditionEntity.AndOr;
                myrow["CreateDate"] = DateTime.Today; 
             
   DataTable dtSymbolName = DbHelper.GetInstance().GetDBRecords("SymbolName", "Workflow_CompareSymbol","SymbolCode="+_NodeConditionEntity.SymbolCode,"DisplayOrder");
   DataTable dtFieldLabel = DbHelper.GetInstance().GetDBRecords("a.FieldLabel", "Workflow_FormField a,Workflow_Base b", "b.WorkflowID=" + DNTRequest.GetString("id") 
       + " and a.FormID=b.FormID and a.FieldID=" + _NodeConditionEntity.FieldID, "a.DisplayOrder");
  
                   myrow["SymbolName"]= dtSymbolName.Rows[0][0];
                   myrow["FieldLabel"] = dtFieldLabel.Rows[0][0];
 
                dtCon.Rows.Add(myrow);
             //   DbHelper.GetInstance().AddWorkflow_NodeCondition(_NodeConditionEntity);  //增加到NodeConditiion表中
            }
            icount++;

            dtConditonSeq.Rows.Clear();
            DataRow dr = dtConditonSeq.NewRow();
            dr["BranchBatchSeq"] = 1;
            dr["FieldID"] = 0;
            dr["SymbolCode"] = "";
            dr["CompareToValue"] = "";
            dr["AndOr"] = "";
            dtConditonSeq.Rows.Add(dr); 

            BindGridView(); 
            BindGridView2();
        //   UpdateSqlCondition();
        }
          
        private string addCondition(DataTable dtConditonSeq)
        {            
            Workflow_NodeConditionEntity _NodeConditionEntity = new Workflow_NodeConditionEntity();

            DataTable dt = (DataTable)GridView2.DataSource;

            for (int i = 0; i < dtConditonSeq.Rows.Count; i++)
            {
                _NodeConditionEntity.BranchBatchSeq = Convert.ToInt32(dtConditonSeq.Rows[i]["BranchBatchSeq"]);
                _NodeConditionEntity.FieldID = Convert.ToInt32(dtConditonSeq.Rows[i]["FieldID"]);
                _NodeConditionEntity.SymbolCode = dtConditonSeq.Rows[i]["SymbolCode"].ToString();
                _NodeConditionEntity.CompareToValue = dtConditonSeq.Rows[i]["CompareToValue"].ToString();
                _NodeConditionEntity.AndOr = dtConditonSeq.Rows[i]["AndOr"].ToString();

                List_nodeCondition.Add(_NodeConditionEntity);
            }

            return new JavaScriptSerializer().Serialize(List_nodeCondition);
        }

        private string jsonCondition()
        {
            return new JavaScriptSerializer().Serialize(List_nodeCondition);
        }
  
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            UpdateSqlCondition();
            BindGridView();
            this.HiddenSqlCondition.Value = resultsql;
            this.HiddenCondition.Value = condition;
        }

        public DataTable dtFieldID()
        {
          //  DataTable dt = DbHelper.GetInstance().GetDBRecords("c.FieldID,c.FieldLabel", "Workflow_NodeLink a,Workflow_Base b,Workflow_FormField c,Workflow_FieldDict d", "a.WorkflowID=b.WorkflowID and b.FormID=c.FormID and c.FieldID=d.FieldID and d.FieldTypeID=1 and a.LinkID=" + DNTRequest.GetString("id"), "c.DisplayOrder");
            DataTable dt = DbHelper.GetInstance().GetDBRecords("c.FieldID,c.FieldLabel", "Workflow_Base b,Workflow_FormField c,Workflow_FieldDict d", "b.FormID=c.FormID and c.FieldID=d.FieldID and d.FieldTypeID=1 and b.WorkflowID=" + DNTRequest.GetString("id"), "c.DisplayOrder");
            
            DataRow dr = dt.NewRow();
            dr["FieldID"] = 0;
            dr["FieldLabel"] = "--请选择--";
            dt.Rows.InsertAt(dr, 0);
            return dt;
        }
        public DataTable dtSymbolCode()
        {
            DataTable dt = DbHelper.GetInstance().GetDBRecords("a.SymbolCode,a.SymbolName", "Workflow_CompareSymbol a", "1=1", "a.DisplayOrder");
            DataRow dr = dt.NewRow();
            dr["SymbolCode"] = "";
            dr["SymbolName"] = "--请选择--";
            dt.Rows.InsertAt(dr, 0);
            return dt;
        }
        public DataTable dtAndOr()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("AndOrValue", typeof(System.String));
            dt.Columns.Add("AndOrText", typeof(System.String));

            DataRow dr = dt.NewRow();
            dr["AndOrValue"] = "";
            dr["AndOrText"] = "--请选择--";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["AndOrValue"] = "And";
            dr["AndOrText"] = "而且";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["AndOrValue"] = "Or";
            dr["AndOrText"] = "或者";
            dt.Rows.Add(dr);
            return dt;
        }
         

        private void UpdateSqlCondition()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Workflow_NodeConditionEntity> list_lnc = new List<Workflow_NodeConditionEntity>();
            foreach (DataRow dr in dtCon.Rows)
            {
                Workflow_NodeConditionEntity lnc = new Workflow_NodeConditionEntity();
              //  lnc.ConditionID = Convert.ToInt32(dr["ConditionID"]);
                lnc.BatchSeq = Convert.ToInt32(dr["BatchSeq"]);
                lnc.BranchBatchSeq = Convert.ToInt32(dr["BranchBatchSeq"]);
                lnc.LinkID = Convert.ToInt32(dr["LinkID"]);
                lnc.FieldID = Convert.ToInt32(dr["FieldID"]);
                lnc.SymbolCode = dr["SymbolCode"].ToString();
                lnc.CompareToValue = dr["CompareToValue"].ToString();
                lnc.AndOr = dr["AndOr"].ToString();
                
                list_lnc.Add(lnc);
            }             
            condition = js.Serialize(list_lnc);  //插入到表中的数据
            
            string sql = getSQL();  //workflow_nodelink表中的SqlCondition


            resultsql = sql;   //sql语句
         
            //string[] resultString = Regex.Split(resultsql, "or", RegexOptions.None); 
           //string[] r=   resultsql.Split(new char[6] { ')', ' ', 'o', 'r', ' ', '(' }); 
            //DbHelper.GetInstance().ExecSqlText(string.Format("update Workflow_NodeLink set SqlCondition='({0})' where LinkID={1}", sql, DNTRequest.GetString("id")));
           // DbHelper.GetInstance().UpdateSqlCondition(sql, DNTRequest.GetString("id"));
        }

        private static string getSQL()
        {
            DataTable dtSql = dtCon;
            string sql = dtSql.Rows.Count == 0 ? "1=1" : "";
            int BatchSeq = 0;
            for (int i = 0; i < dtSql.Rows.Count; i++)
            {
                if (i == 0)
                {
                    sql += "(";
                }
                else if (BatchSeq != Convert.ToInt32(dtSql.Rows[i]["BatchSeq"]))
                {
                    sql += ") or (";

                }
               
                int FieldID = Convert.ToInt32(dtSql.Rows[i]["FieldID"]); 
                string CompareToValue = dtSql.Rows[i]["CompareToValue"].ToString();
                string AndOr = dtSql.Rows[i]["AndOr"].ToString();                
                string SymbolCode = dtSql.Rows[i]["SymbolCode"].ToString();

                DataTable dtSymbol = DbHelper.GetInstance().GetDBRecords("CompareSymbol", "Workflow_CompareSymbol a", "a.SymbolCode=" + SymbolCode, "DisplayOrder");
                string CompareSymbol = dtSymbol.Rows[0]["CompareSymbol"].ToString();

                DataTable dtFieldDict = DbHelper.GetInstance().GetDBRecords("f.FieldName,f.HTMLTypeID,f.DataTypeID,f.Dateformat",
                    "Workflow_FieldDict f",  "f.FieldID=" + FieldID, "FieldID");
                string FieldName = dtFieldDict.Rows[0]["FieldName"].ToString();
                int HTMLTypeID = Convert.ToInt32(dtFieldDict.Rows[0]["HTMLTypeID"]);
                int DataTypeID = Convert.ToInt32(dtFieldDict.Rows[0]["DataTypeID"]);
                string Dateformat = dtFieldDict.Rows[0]["Dateformat"].ToString();


                if (HTMLTypeID == 2 && (DataTypeID == 2 || DataTypeID == 3))
                {
                    sql += string.Format(" {0} {1} {2} {3} ", FieldName, CompareSymbol, CompareToValue, AndOr);
                }
                else if (HTMLTypeID == 2 && DataTypeID == 4)
                {
                    if (Dateformat.EndsWith("dd"))
                    {
                        sql += string.Format(" datediff(month,\"{2}\",{0}) {1} 0 {3} ", FieldName, CompareSymbol, CompareToValue, AndOr);
                    }
                    else if (Dateformat.EndsWith("mm"))
                    {
                        sql += string.Format(" datediff(minute,\"{2}\",{0}) {1} 0 {3} ", FieldName, CompareSymbol, CompareToValue, AndOr);
                    }
                    else
                    {
                        sql += string.Format(" {0} {1} \"{2}\" {3} ", FieldName, CompareSymbol, CompareToValue, AndOr);
                    }
                }
                else if ("30,40".Contains(SymbolCode))
                {
                    sql += string.Format(" {0} {1} \"%{2}%\" {3} ", FieldName, CompareSymbol, CompareToValue, AndOr);
                }
                else
                {
                    sql += string.Format(" {0} {1} \"{2}\" {3} ", FieldName, CompareSymbol, CompareToValue, AndOr);
                }

                if (i == dtSql.Rows.Count - 1)
                {
                    sql += ")";
                }
                BatchSeq = Convert.ToInt32(dtSql.Rows[i]["BatchSeq"]);
             
            }
         
            return   sql;
        }
         

        protected void Button2_Click(object sender, EventArgs e)
        {
            UpdateSqlCondition();
            this.HiddenSqlCondition.Value = resultsql;
            this.HiddenCondition.Value = condition;
          
        }

    }
}
