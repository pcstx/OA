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
using AjaxControlToolkit;
using GOA.UserControl;

namespace GOA
{
    public partial class GG30Info : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            RefreshDetailGroup();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string FormID = DNTRequest.GetString("fmid");
            FormField1.fmid = FormID;
            FormField1.ftid = "1";
            FormField1.gid = "0";
            if (!Page.IsPostBack)
            {
                BindGridView();
            }
        }

        #region gridview 绑定
        //此类需要更改，主要是更改获取数据源的方法
        private void BindGridView()
        {
            string WhereCondition = "1=1";
            string FormID = DNTRequest.GetString("fmid");
            WhereCondition = WhereCondition + " and a.FormID=" + FormID;
            DataTable dt = DbHelper.GetInstance().GetDBRecords("a.*", "Workflow_FormFieldGroup a", WhereCondition, "a.DisplayOrder");
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

        #region gridView 事件 --类型
        protected void GridView1_RowCreated(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                int i = Convert.ToInt32(e.CommandArgument);   //获取行号
                string GroupID = GridView1.DataKeys[i][0].ToString().Trim();
                string FormID = DNTRequest.GetString("fmid");
                DbHelper.GetInstance().DeleteWorkflow_FormFieldGroup(GroupID, FormID);
                BindGridView();
                RefreshDetailGroup();
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtGroupName.Text != string.Empty && txtGroupDesc.Text != string.Empty)
            {
                Workflow_FormFieldGroupEntity _Workflow_FormFieldGroupEntity = new Workflow_FormFieldGroupEntity();
                _Workflow_FormFieldGroupEntity.FormID = Convert.ToInt32(DNTRequest.GetString("fmid"));
                _Workflow_FormFieldGroupEntity.GroupName = txtGroupName.Text;
                _Workflow_FormFieldGroupEntity.GroupDesc = txtGroupDesc.Text;
                _Workflow_FormFieldGroupEntity.DisplayOrder = Int32.Parse(txtDisplayOrder.Text);
                string sResult = DbHelper.GetInstance().AddWorkflow_FormFieldGroup(_Workflow_FormFieldGroupEntity);
                if (sResult == "-1")
                {
                    lblMsg.Text = ResourceManager.GetString("Operation_RECORD");
                }
            }
            BindGridView();
            RefreshDetailGroup();
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                string GroupID = GridView1.DataKeys[i][0].ToString().Trim();
                CheckBox cb = this.GridView1.Rows[i].FindControl("Item") as CheckBox;
                if (cb.Checked)
                {
                    string FormID = DNTRequest.GetString("fmid");
                    DbHelper.GetInstance().DeleteWorkflow_FormFieldGroup(GroupID, FormID);
                }
            }
            BindGridView();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string Prefix = "TabContainer1$TabPanel3$GridView1$ctl" + (i + 2).ToString().PadLeft(2, '0') + "$";

                Workflow_FormFieldGroupEntity _Workflow_FormFieldGroupEntity = new Workflow_FormFieldGroupEntity();
                _Workflow_FormFieldGroupEntity.GroupID = Convert.ToInt32(GridView1.DataKeys[i][0]);
                _Workflow_FormFieldGroupEntity.FormID = Convert.ToInt32(DNTRequest.GetString("fmid"));
                _Workflow_FormFieldGroupEntity.GroupName = DNTRequest.GetString(Prefix + "GroupName");
                _Workflow_FormFieldGroupEntity.GroupDesc = DNTRequest.GetString(Prefix + "GroupDesc");
                _Workflow_FormFieldGroupEntity.DisplayOrder = DNTRequest.GetInt(Prefix + "DisplayOrder", 9990);
                DbHelper.GetInstance().UpdateWorkflow_FormFieldGroup(_Workflow_FormFieldGroupEntity);
            }
            ArrayList arlst = new ArrayList();
            arlst.Add("Workflow_FormFieldGroup");
            arlst.Add(DNTRequest.GetString("fmid"));
            arlst.Add("");
            DbHelper.GetInstance().sp_ReDisplayOrder(arlst);

            BindGridView();
            RefreshDetailGroup();
        }

        private void RefreshDetailGroup()
        {
            for (int i = TabContainer1.Tabs.Count-1; i >=2 ; i--)
            {
                TabContainer1.Tabs.RemoveAt(i);
            }

            string FormID = DNTRequest.GetString("fmid");
            DataTable dtDetailGroup = DbHelper.GetInstance().GetDBRecords("GroupID,GroupName", "Workflow_FormFieldGroup ", "FormID=" + FormID, "DisplayOrder");
            for (int i = 0; i < dtDetailGroup.Rows.Count; i++)
            {
                TabPanel tp = new TabPanel();
                FormField _FormField = (FormField)(Page.LoadControl("UserControl/FormField.ascx"));
                _FormField.fmid = FormID;
                _FormField.ftid = "2";
                _FormField.gid = dtDetailGroup.Rows[i]["GroupID"].ToString();
                tp.Controls.Add(_FormField);
                TabContainer1.Tabs.Add(tp);
                TabContainer1.Tabs[2 + i].HeaderText = string.Format("明细字段({0})", dtDetailGroup.Rows[i]["GroupName"]);
            }
        }
    }
}
