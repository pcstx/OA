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
    public partial class GG50020102 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGridView();
                string NodeID = DNTRequest.GetString("id");
                string GroupID = DNTRequest.GetString("gid");
                Workflow_NodeDetailFieldControlEntity _NodeDetailFieldControlEntity = DbHelper.GetInstance().GetWorkflow_NodeDetailFieldControlEntityByKeyCol(NodeID, GroupID);
                chkIsView.Checked = _NodeDetailFieldControlEntity.IsView == 1;
                chkIsAdd.Checked = _NodeDetailFieldControlEntity.IsAdd == 1;
                chkIsEdit.Checked = _NodeDetailFieldControlEntity.IsEdit == 1;
                chkIsDelete.Checked = _NodeDetailFieldControlEntity.IsDelete == 1;
            }
        }

        #region gridview 绑定
        //此类需要更改，主要是更改获取数据源的方法
        private void BindGridView()
        {
            DataTable dt = DbHelper.GetInstance().sp_GetNodeDetailFieldControlDetail(DNTRequest.GetString("id"), DNTRequest.GetString("gid"));
            ExtendDatatable(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            BuildNoRecords(GridView1, dt);
        }
        private void ExtendDatatable(DataTable dt)
        {
            dt.Columns.Add(new DataColumn("HTMLTypeN", typeof(System.String)));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string FieldID = dt.Rows[i]["FieldID"].ToString();
                Workflow_FieldDictEntity _Workflow_FieldDictEntity = DbHelper.GetInstance().GetWorkflow_FieldDictEntityByKeyCol(FieldID);
                Workflow_HTMLTypeEntity _Workflow_HTMLTypeEntity = DbHelper.GetInstance().GetWorkflow_HTMLTypeEntityByKeyCol(_Workflow_FieldDictEntity.HTMLTypeID.ToString());
                string HTMLTypeN = _Workflow_HTMLTypeEntity.HTMLTypeDesc;
                if (_Workflow_FieldDictEntity.HTMLTypeID == 8
                    && _Workflow_FieldDictEntity.BrowseType > 0)
                {
                    Workflow_BrowseTypeEntity _Workflow_BrowseTypeEntity = DbHelper.GetInstance().GetWorkflow_BrowseTypeEntityByKeyCol(_Workflow_FieldDictEntity.BrowseType.ToString());
                    HTMLTypeN = HTMLTypeN + "-" + _Workflow_BrowseTypeEntity.BrowseTypeDesc;
                }
                dt.Rows[i]["HTMLTypeN"] = HTMLTypeN;
            }
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
            }
        }
        //此类要进行dorpdownlist/chk控件的转换
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string IsView = ((DataRowView)e.Row.DataItem).Row["IsView"].ToString();
                System.Web.UI.WebControls.CheckBox chkIsView = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("IsView");
                chkIsView.Checked = IsView.Equals("1");
                string IsEdit = ((DataRowView)e.Row.DataItem).Row["IsEdit"].ToString();
                System.Web.UI.WebControls.CheckBox chkIsEdit = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("IsEdit");
                chkIsEdit.Checked = IsEdit.Equals("1");
                string IsMandatory = ((DataRowView)e.Row.DataItem).Row["IsMandatory"].ToString();
                System.Web.UI.WebControls.CheckBox chkIsMandatory = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("IsMandatory");
                chkIsMandatory.Checked = IsMandatory.Equals("1");

                System.Web.UI.WebControls.DropDownList ddlValidTimeType = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ValidTimeType");
                ddlValidTimeType.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ValidTimeType"));

                ImageButton ImgValidType = (ImageButton)e.Row.FindControl("ImgValidType");
                ImgValidType.OnClientClick = string.Format("btnValidTypeClick('{0}')", (e.Row.DataItemIndex + 2).ToString().PadLeft(2, '0'));
            }
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int NodeID = DNTRequest.GetInt("id", 0);
            int GroupID = DNTRequest.GetInt("gid", 0);
            Workflow_NodeDetailFieldControlEntity _NodeDetailFieldControlEntity = new Workflow_NodeDetailFieldControlEntity();
            _NodeDetailFieldControlEntity.NodeID = NodeID;
            _NodeDetailFieldControlEntity.GroupID = GroupID;
            _NodeDetailFieldControlEntity.IsView = chkIsView.Checked ? 1 : 0;
            _NodeDetailFieldControlEntity.IsAdd = chkIsAdd.Checked ? 1 : 0;
            _NodeDetailFieldControlEntity.IsEdit = chkIsEdit.Checked ? 1 : 0;
            _NodeDetailFieldControlEntity.IsDelete = chkIsDelete.Checked ? 1 : 0;
            DbHelper.GetInstance().UpdateWorkflow_NodeDetailFieldControl(_NodeDetailFieldControlEntity);

            DbHelper.GetInstance().DeleteWorkflow_NodeDetailFieldControlDetail(DNTRequest.GetString("id"), DNTRequest.GetString("gid"));
            Workflow_NodeDetailFieldControlDetailEntity _NodeDetailFieldControlDetailEntity = new Workflow_NodeDetailFieldControlDetailEntity();
            _NodeDetailFieldControlDetailEntity.NodeID = DNTRequest.GetInt("id", 0);
            _NodeDetailFieldControlDetailEntity.GroupID = DNTRequest.GetInt("gid", 0);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string Prefix = "GridView1$ctl" + (i + 2).ToString().PadLeft(2, '0') + "$";
                _NodeDetailFieldControlDetailEntity.FieldID = Convert.ToInt32(GridView1.DataKeys[i][0]);
                _NodeDetailFieldControlDetailEntity.IsView = DNTRequest.GetString(Prefix + "IsView") == "on" ? 1 : 0;
                _NodeDetailFieldControlDetailEntity.IsEdit = DNTRequest.GetString(Prefix + "IsEdit") == "on" ? 1 : 0;
                _NodeDetailFieldControlDetailEntity.IsMandatory = DNTRequest.GetString(Prefix + "IsMandatory") == "on" ? 1 : 0;
                _NodeDetailFieldControlDetailEntity.BasicValidType = DNTRequest.GetInt("txtBasicValidTypeID" + (i + 2).ToString().PadLeft(2, '0'), 0);
                _NodeDetailFieldControlDetailEntity.ValidTimeType = DNTRequest.GetInt(Prefix + "ValidTimeType", 0);
                DbHelper.GetInstance().AddWorkflow_NodeDetailFieldControlDetail(_NodeDetailFieldControlDetailEntity);
            }
            ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "btnSubmit", "alert('设定成功');", true);
            BindGridView();
        }

        public DataTable dtValidTimeType()
        {
            DataTable dt = DbHelper.GetInstance().GetDBRecords("ValidTimeTypeID,ValidTimeTypeName", "Workflow_FieldValidTimeType", "1=1", "ValidTimeTypeID");
            DataRow dr = dt.NewRow();
            dr["ValidTimeTypeID"] = 0;
            dr["ValidTimeTypeName"] = "    ";
            dt.Rows.InsertAt(dr, 0);
            return dt;
        }

        protected void Page_Init()
        {
            DataTable dt = DbHelper.GetInstance().sp_GetNodeMainFieldControl(DNTRequest.GetString("id"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                HiddenField hf = new HiddenField();
                hf.ID = "txtBasicValidTypeID" + (i + 2).ToString().PadLeft(2, '0');
                hf.Value = dt.Rows[i]["BasicValidType"].ToString();
                PHHiddenField.Controls.Add(hf);
            }
        }
    }
}
