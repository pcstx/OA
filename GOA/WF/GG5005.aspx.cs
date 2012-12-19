using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
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
    public partial class GG5005 : BasePage
    {
        private static string strOperationState;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string WorkflowID = DNTRequest.GetString("id");

                txtWFID.Value = WorkflowID;
                SetControlDataSource();

                ViewState["selectedLines"] = new ArrayList();
                AspNetPager1.PageSize = config.PageSize;
                BindGridView();
            }
        }

        private void SetControlDataSource()
        {
            DataTable dtNodeList = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FlowNode", "WorkflowID=" + txtWFID.Value, "DisplayOrder");
            DataTable dtColumnList = DbHelper.GetInstance().GetDBRecords("a.FieldID,a.FieldLabel", "Workflow_FormField a left join Workflow_Base  b on  a.FormID=b.FormID ", "b.WorkflowID=" + txtWFID.Value + " and a.IsDetail=0 ", "a.DisplayOrder");
            DataTable dtTime = DbHelper.GetInstance().GetDBRecords("TimeID,TimeValue", "[Workflow_TriggerOperationTime]", "1=1", "TimeID");

            ddlNodeID.AddTableData(dtNodeList, 0, 1, true, "Select");
            ddlWFCreateNode.AddTableData(dtNodeList, 0, 1, false, "Select");
            ddlWFCreateFieldName.AddTableData(dtColumnList, 0, 1, false, "Select");
            ddlOPTime.AddTableData(dtTime, 0, 1, false, "Select");
        }

        protected void ddlTriggerCreator_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            setDivVisibility(ddlTriggerCreator.SelectedValue);
        }

        private void setDivVisibility(string creatorValue)
        {
            if (creatorValue == "1")
            {
                divNode.Visible = true;
                divField.Visible = false;
            }
            else if (creatorValue == "2")
            {
                divNode.Visible = false;
                divField.Visible = true;
            }
            else
            {
                divNode.Visible = false;
                divField.Visible = false;
            }

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
            CollectSelected();
            BindGridView();
        }

        #region gridview 绑定
        //此类需要更改，主要是更改获取数据源的方法


        private void BindGridView()
        {
            string Tables = @"
 Workflow_NodeTriggerWorkflow t
left join Workflow_Base b2 on t.TriggerWFID=b2.WorkflowID
left join Workflow_FlowNode n on t.WFCreateNode=n.NodeID 
left join Workflow_FlowNode n2 on t.NodeID=n2.NodeID 
left join Workflow_FieldDict f on t.WFCreateFieldID=f.FieldID
left join [Workflow_TriggerOperationTime] ot on t.OPTime=ot.TimeID
";
            string columns = @"  
t.*,
NodeName=n2.NodeName,
OPTimeN=ot.TimeValue,
TWorkflowName=b2.WorkflowName,
TriggerWFCreatorN=case TriggerWFCreator when 0 then '父流程创建者' when 1 then '父流程节点操作者' when 2 then '父流程字段值' end,
WFCreateNodeN=n.NodeName,
WFCreateFieldN=f.FieldDesc
";

            string WhereCondition = "t.IsCancel=0 and t.WorkflowID=" + DNTRequest.GetString("id");
            DataTable dt = DbHelper.GetInstance().GetDBRecords(columns, Tables, WhereCondition, "t.NodeID,t.OPTime", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
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

        #region gridView 事件 --类型
        protected void GridView1_RowCreated(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = -1;
            if (e.CommandName == "select")
            {
                programmaticAddModalPopup.Show();
                strOperationState = "Update";
                index = Convert.ToInt32(e.CommandArgument);   //获取行号
                string TriggerID = GridView1.DataKeys[index][0].ToString().Trim();
                Workflow_NodeTriggerWorkflowEntity _NT = DbHelper.GetInstance().GetWorkflow_NodeTriggerWorkflowEntityByKeyCol(TriggerID);
                txtTriggerID.Value = _NT.TriggerID.ToString();
                txtWFID.Value = _NT.WorkflowID.ToString();
                txtTriggerWFID.Value = _NT.TriggerWFID.ToString();
                txtTriggerWFN.Text = DbHelper.GetInstance().GetWorkflow_BaseEntityByKeyCol(_NT.TriggerWFID.ToString()).WorkflowName;
                ddlNodeID.SelectedValue = _NT.NodeID.ToString();
                ddlTriggerCreator.SelectedValue = _NT.TriggerWFCreator.ToString();
                setDivVisibility(_NT.TriggerWFCreator.ToString());
                if (_NT.TriggerWFCreator == 1)//&& (ddlWFCreateNode.Items.FindByValue(_NT.WFCreateNode.ToString())).
                {
                    ddlWFCreateNode.SelectedValue = _NT.WFCreateNode.ToString();
                }
                else if (_NT.TriggerWFCreator == 2)// && ddlWFCreateFieldName.Items.Contains(_NT.WFCreateFieldName.ToString()))
                {
                    ddlWFCreateFieldName.SelectedValue = _NT.WFCreateFieldID.ToString();
                }

                txtOPCondition.Text = _NT.OPCondition;
                ddlOPTime.SelectedValue = _NT.OPTime.ToString();
                // ddlOPCycleType.SelectedValue = _NT.OPCycleType.ToString();

            }
        }
        //此类要进行dorpdownlist/chk控件的转换


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string KeyCol = ((DataRowView)e.Row.DataItem).Row["TriggerID"].ToString();
                CheckBox cb = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("Item") as CheckBox;
                ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
                if (selectedLines.Contains(KeyCol))
                {
                    cb.Checked = true;
                }
            }
        }
        #endregion

        //此类一般不需要更改,因为保存的工作全部放在下面的SaveData中


        protected void hideModalPopupViaServer_Click(object sender, EventArgs e)
        {
            GPRP.GPRPControls.Button btn = (GPRP.GPRPControls.Button)sender;
            string sResult = "-1";
            if (btn.ID == "btnSubmitAndClose" || btn.ID == "btnSubmit")
            {
                //保存
                sResult = SaveData();
                if (sResult == "-1")
                {
                    lblMsg.Text = ResourceManager.GetString("Operation_RECORD");
                }
                else
                {
                    //refresh gridview
                    if (btn.ID == "btnSubmitAndClose")
                    {
                        setDivVisibility("0");
                        programmaticAddModalPopup.Hide();
                    }
                }
                CollectSelected();
                BindGridView();
            }
            System.Web.UI.ScriptManager.RegisterStartupScript(btn, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }

        private string SaveData()
        {
            Workflow_NodeTriggerWorkflowEntity _NT = new Workflow_NodeTriggerWorkflowEntity();
            _NT.TriggerID = txtTriggerID.Value != string.Empty ? Convert.ToInt32(txtTriggerID.Value) : 0;
            _NT.WorkflowID = DNTRequest.GetInt("id", 0);
            _NT.NodeID = Int32.Parse(ddlNodeID.SelectedValue);
            // _NT.OPCycleType  = Int32.Parse(ddlOPCycleType.SelectedValue);
            _NT.OPTime = Int32.Parse(ddlOPTime.SelectedValue);
            _NT.TriggerWFCreator = Int32.Parse(ddlTriggerCreator.SelectedValue);
            _NT.TriggerWFID = Int32.Parse(txtTriggerWFID.Value);
            _NT.WFCreateFieldID = (ddlTriggerCreator.SelectedValue == "2") ? Int32.Parse(ddlWFCreateFieldName.SelectedValue) : 0;
            _NT.WFCreateNode = (ddlTriggerCreator.SelectedValue == "1") ? Int32.Parse(ddlWFCreateNode.SelectedValue) : 0;
            _NT.OPCondition = "(1=1)";

            string sResult = "-1";
            if (strOperationState == "Add")
            {
                _NT.CreateSID = userEntity.UserSerialID;
                sResult = DbHelper.GetInstance().AddWorkflow_NodeTriggerWorkflow(_NT);
            }
            else if (strOperationState == "Update")
                sResult = DbHelper.GetInstance().UpdateWorkflow_NodeTriggerWorkflow(_NT);
            return sResult;
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            CollectSelected();
            ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
            for (int i = 0; i < selectedLines.Count; i++)
            {
                DbHelper.GetInstance().DeleteWorkflow_NodeTriggerWorkflow(selectedLines[i].ToString(), userEntity.UserSerialID);
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
            return "";
        }
    }
}
