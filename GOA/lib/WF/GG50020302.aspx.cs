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
    public partial class GG50020302 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
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

        private void BindGridView()
        {
            DataTable dt = DbHelper.GetInstance().GetDBRecords("a.*,b.SymbolName,f.FieldLabel", "Workflow_NodeAddInOperation_Type0Condition a,Workflow_CompareSymbol b,Workflow_NodeAddInOperation_Type0 c,Workflow_FlowNode d,Workflow_Base e,Workflow_FormField f", "a.SymbolCode=b.SymbolCode and a.AddInOPID=c.AddInOPID and c.NodeID=d.NodeID and d.WorkflowID=e.WorkflowID and e.FormID=f.FormID and a.FieldID=f.FieldID and a.AddInOPID=" + DNTRequest.GetString("opid"), "a.BatchSeq,a.BranchBatchSeq");
            GridView1.DataSource = dt;
            GridView1.DataBind();
            BuildNoRecords(GridView1, dt);
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
                DbHelper.GetInstance().DeleteWorkflow_NodeAddInOperation_Type0Condition(ConditionID);
                UpdateOPCondition();
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
            Workflow_NodeAddInOperation_Type0ConditionEntity _Type1ConditionEntity = new Workflow_NodeAddInOperation_Type0ConditionEntity();
            _Type1ConditionEntity.AddInOPID = DNTRequest.GetInt("opid", 0);
            DataTable dtMaxSeq = DbHelper.GetInstance().GetDBRecords("MaxBatchSeq=isnull(max(BatchSeq),0)+1", "Workflow_NodeAddInOperation_Type0Condition", "AddInOPID=" + DNTRequest.GetString("opid"), "");
            _Type1ConditionEntity.BatchSeq = Convert.ToInt32(dtMaxSeq.Rows[0]["MaxBatchSeq"]);
            for (int i = 0; i < dtConditonSeq.Rows.Count; i++)
            {
                _Type1ConditionEntity.BranchBatchSeq = Convert.ToInt32(dtConditonSeq.Rows[i]["BranchBatchSeq"]);
                _Type1ConditionEntity.FieldID = Convert.ToInt32(dtConditonSeq.Rows[i]["FieldID"]);
                _Type1ConditionEntity.SymbolCode = dtConditonSeq.Rows[i]["SymbolCode"].ToString();
                _Type1ConditionEntity.CompareToValue = dtConditonSeq.Rows[i]["CompareToValue"].ToString();
                _Type1ConditionEntity.AndOr = dtConditonSeq.Rows[i]["AndOr"].ToString();
                DbHelper.GetInstance().AddWorkflow_NodeAddInOperation_Type0Condition(_Type1ConditionEntity);
            }
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
            UpdateOPCondition();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            UpdateOPCondition();
            BindGridView();
        }

        public DataTable dtFieldID()
        {
            DataTable dt = DbHelper.GetInstance().GetDBRecords("d.FieldID,d.FieldLabel", "Workflow_NodeAddInOperation_Type0 a,Workflow_FlowNode b,Workflow_Base c,Workflow_FormField d,Workflow_FieldDict e", "a.NodeID=b.NodeID and b.WorkflowID=c.WorkflowID and c.FormID=d.FormID and d.FieldID=e.FieldID and e.FieldTypeID=1 and a.AddInOPID=" + DNTRequest.GetString("opid"), "d.DisplayOrder");
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

        private void UpdateOPCondition()
        {
            DataTable dtSql = DbHelper.GetInstance().GetDBRecords("a.BatchSeq,a.BranchBatchSeq,a.FieldID,g.FieldName,g.HTMLTypeID,g.DataTypeID,g.Dateformat,b.SymbolCode,b.CompareSymbol,a.CompareToValue,a.AndOr", "Workflow_NodeAddInOperation_Type0Condition a,Workflow_CompareSymbol b,Workflow_NodeAddInOperation_Type0 c,Workflow_FlowNode d,Workflow_Base e,Workflow_FormField f,Workflow_FieldDict g", "a.SymbolCode=b.SymbolCode and a.AddInOPID=c.AddInOPID and c.NodeID=d.NodeID and d.WorkflowID=e.WorkflowID and e.FormID=f.FormID and a.FieldID=f.FieldID and f.FieldID=g.FieldID and a.AddInOPID=" + DNTRequest.GetString("opid"), "a.BatchSeq,a.BranchBatchSeq");
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
                string FieldName = dtSql.Rows[i]["FieldName"].ToString();
                string CompareSymbol = dtSql.Rows[i]["CompareSymbol"].ToString();
                string CompareToValue = dtSql.Rows[i]["CompareToValue"].ToString();
                string AndOr = dtSql.Rows[i]["AndOr"].ToString();
                string Dateformat = dtSql.Rows[i]["Dateformat"].ToString();
                string SymbolCode = dtSql.Rows[i]["SymbolCode"].ToString();

                if (Convert.ToInt32(dtSql.Rows[i]["HTMLTypeID"]) == 2
                    && (Convert.ToInt32(dtSql.Rows[i]["DataTypeID"]) == 2 || Convert.ToInt32(dtSql.Rows[i]["DataTypeID"]) == 3))
                {
                    sql += string.Format(" {0} {1} {2} {3} ", FieldName, CompareSymbol, CompareToValue, AndOr);
                }
                else if (Convert.ToInt32(dtSql.Rows[i]["HTMLTypeID"]) == 2
                    && Convert.ToInt32(dtSql.Rows[i]["DataTypeID"]) == 4)
                {
                    if (Dateformat.EndsWith("dd"))
                    {
                        sql += string.Format(" datediff(month,''{2}'',{0}) {1} 0 {3} ", FieldName, CompareSymbol, CompareToValue, AndOr);
                    }
                    else if (Dateformat.EndsWith("mm"))
                    {
                        sql += string.Format(" datediff(minute,''{2}'',{0}) {1} 0 {3} ", FieldName, CompareSymbol, CompareToValue, AndOr);
                    }
                    else
                    {
                        sql += string.Format(" {0} {1} ''{2}'' {3} ", FieldName, CompareSymbol, CompareToValue, AndOr);
                    }
                }
                else if ("30,40".Contains(SymbolCode))
                {
                    sql += string.Format(" {0} {1} ''%{2}%'' {3} ", FieldName, CompareSymbol, CompareToValue, AndOr);
                }
                else
                {
                    sql += string.Format(" {0} {1} ''{2}'' {3} ", FieldName, CompareSymbol, CompareToValue, AndOr);
                }

                if (i == dtSql.Rows.Count - 1)
                {
                    sql += ")";
                }
                BatchSeq = Convert.ToInt32(dtSql.Rows[i]["BatchSeq"]);
            }
            DbHelper.GetInstance().ExecSqlText(string.Format("update Workflow_NodeAddInOperation_Type0 set OPCondition='({0})' where AddInOPID={1}", sql, DNTRequest.GetString("opid")));
        }
    }
}
