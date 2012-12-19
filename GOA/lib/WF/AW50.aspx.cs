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
    public partial class AW50 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetControlValue();
                AspNetPager1.PageSize = config.PageSize;
                BindGridView();

            }
        }


        private void SetControlValue()
        {
            if (Request.QueryString["Type"] != null)
                hiddenType.Value = Request.QueryString["Type"];


            hiddenRequestID.Value = (Request.QueryString["RequestID"] != null) ? Request.QueryString["RequestID"] : "";
            hiddenIsFinish.Value = (Request.QueryString["IsFinished"] != null) ? Request.QueryString["IsFinished"] : "";
            /*   hiddenFormTypeID.Value = (Request.QueryString["FormTypeID"] != null) ? Request.QueryString["FormTypeID"] : "";*/
            hiddenDeptID.Value = (Request.QueryString["DeptID"] != null) ? Request.QueryString["DeptID"] : "";
            hiddenTitle.Value = (Request.QueryString["Title"] != null) ? Server.UrlDecode(Request.QueryString["Title"]) : "";

            //4-30改为多选


            if ((Request.QueryString["WorkflowID"] != null) && (Request.QueryString["WorkflowID"] != ""))
            {
                txtWorkflowID.Value = ""; //Request.QueryString["WorkflowID"];
                hiddenWorkflowID.Value = Request.QueryString["WorkflowID"];
                DataTable dtN = DbHelper.GetInstance().GetDBRecords("WorkflowName", "Workflow_Base", " WorkflowID in (" + hiddenWorkflowID.Value + ")", "DisplayOrder");


                if (dtN != null)
                {
                    string sName = "";
                    for (int i = 0; i < dtN.Rows.Count; i++)
                        sName += dtN.Rows[i][0].ToString() + ",";

                    if (sName.Length > 0)
                        txtWorkflowN.Text = sName.Substring(0, sName.Length - 1);
                }
            }

            if ((Request.QueryString["Creator"] != null) && (Request.QueryString["Creator"] != ""))
            {
                txtCreatorID.Value = Request.QueryString["CreatorID"];
                txtCreator.Text = DbHelper.GetInstance().GetUserListEntityByKeyCol(txtCreatorID.Value).UserName;
            }

            if ((Request.QueryString["BeAgentID"] != null) && (Request.QueryString["BeAgentID"] != ""))
            {
                txtBeAgentID.Value = Request.QueryString["BeAgentID"];
                txtBeAgentN.Text = DbHelper.GetInstance().GetUserListEntityByKeyCol(txtBeAgentID.Value).UserName;
            }

            if ((Request.QueryString["AgentID"] != null) && (Request.QueryString["AgentID"] != ""))
            {
                txtAgentID.Value = Request.QueryString["AgentID"];
                txtAgentN.Text = DbHelper.GetInstance().GetUserListEntityByKeyCol(txtAgentID.Value).UserName;
            }


            txtStartDate.Text = (Request.QueryString["StartDate"] != null) ? Request.QueryString["StartDate"] : "";
            txtEndDate.Text = (Request.QueryString["EndDate"] != null) ? Request.QueryString["EndDate"] : "";

            DataTable dtNodeType = DbHelper.GetInstance().GetDBRecords("*", "Workflow_NodeType", "1=1", "DisplayOrder");
            ddlNodeTypeID.AddTableData(dtNodeType, 0, 1, true, "Select");

            DataTable dtFormType = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FormType", "1=1", "DisplayOrder");
            ddlFormType.AddTableData(dtFormType, 0, 1, true, "Select");

            string nodetype = Request.QueryString["NodeType"];
            if ((nodetype != null) && (nodetype != "") && ddlNodeTypeID.SelectedValue.Contains(nodetype))
                ddlNodeTypeID.SelectedValue = Request.QueryString["NodeType"];


            string formtype = Request.QueryString["FormTypeID"];
            hiddenFormTypeID.Value = formtype;
            if ((formtype != null) && (formtype != "") && ddlFormType.SelectedValue.Contains(formtype))
                ddlFormType.SelectedValue = formtype;



            /*   if (hiddenType.Value.Trim() == "1" || hiddenType.Value.Trim() == "2" || hiddenType.Value.Trim() == "3" || hiddenType.Value.Trim() == "4")
               {
               ImgForm.Enabled = false;
             
                
                   }*/
        }

        private void BindGridView()
        {
            DataTable dt = new DataTable();

            dt = DbHelper.GetInstance().GetAgentWorkflowSearchResult(GetArrayListParameter(), AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);

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

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            if (hiddenType.Value == "1" || hiddenType.Value == "2" || hiddenType.Value == "3")
                Response.Redirect("AW20.aspx?type=" + hiddenType.Value);
            else if (hiddenType.Value == "4")
                Response.Redirect("AW30.aspx");
            else
                Response.Redirect("AW40.aspx");
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
            //再进行绑定一次


            BindGridView();
        }

        #region aspnetPage 分页代码
        //此类无须更改
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            BindGridView();
        }
        protected void AspNetPager1_PageChanging(object src, EventArgs e)
        {
        }
        #endregion

        private ArrayList GetArrayListParameter()
        {

            ArrayList al = new ArrayList();

            al.Add(Convert.ToByte(hiddenType.Value));//-区分是待办、已办、办结、我的请求（已完成、未完成）、查询流程



            //我的请求（已完成、未完成）


            if (hiddenIsFinish.Value == "")
                al.Add(System.DBNull.Value);
            else
                al.Add(Convert.ToByte(hiddenIsFinish.Value));

            //表单类型 --待办、已办、办结、我的请求（已完成、未完成）用到


            if (ddlFormType.SelectedValue == "" && hiddenFormTypeID.Value == "")
                al.Add(System.DBNull.Value);
            else if (ddlFormType.SelectedValue != "")
                al.Add(Int32.Parse(ddlFormType.SelectedValue));
            else if (ddlFormType.SelectedValue == "" && hiddenFormTypeID.Value != "")
                al.Add(Int32.Parse(hiddenFormTypeID.Value));

            al.Add(userEntity.UserSerialID);//用户序号

            //流程ID--可多选


            if (txtWorkflowID.Value != "")
                al.Add(txtWorkflowID.Value);
            else
                al.Add((hiddenWorkflowID.Value));

            //--------以下公共查询部分

            //--表单ID 查询时不用到 4-30
            /*    if (txtFormID.Value == "")
                    al.Add(System.DBNull.Value);
                else
                    al.Add(Int32.Parse(txtFormID.Value));*/

            //--操作节点类型（Create,Approve,Realize,Process）


            if (ddlNodeTypeID.SelectedValue == "")
                al.Add(System.DBNull.Value);
            else
                al.Add(Int32.Parse(ddlNodeTypeID.SelectedValue));

            //--开始创建日期


            if (txtStartDate.Text == "")
                al.Add(System.DBNull.Value);
            else
                al.Add(txtStartDate.Text.ToString());

            //--终止创建日期
            if (txtEndDate.Text == "")
                al.Add(System.DBNull.Value);
            else
                al.Add(txtEndDate.Text.ToString());


            //--创建人


            if (txtCreatorID.Value == "")
                al.Add(System.DBNull.Value);
            else
                al.Add(Int32.Parse(txtCreatorID.Value));

            //是否有效
            if (ddlStatus.SelectedValue == "")
                al.Add(System.DBNull.Value);
            else
                al.Add(Convert.ToByte(ddlStatus.SelectedValue));


            //--------以下是@Type=5(查询流程）时会用到


            //---请求编号
            if (hiddenRequestID.Value == "")
                al.Add(System.DBNull.Value);
            else
                al.Add(Int32.Parse(hiddenRequestID.Value));

            al.Add(hiddenTitle.Value);//--表单名称或请求名称，模糊查询
            al.Add(hiddenDeptID.Value);//----创建者所属部门




            //--代理人

            if (txtAgentID.Value == "")
                al.Add(System.DBNull.Value);
            else
                al.Add(Int32.Parse(txtAgentID.Value));


            //--被代理人
            if (txtBeAgentID.Value == "")
                al.Add(System.DBNull.Value);
            else
                al.Add(Int32.Parse(txtBeAgentID.Value));

            return al;
        }

    }
}