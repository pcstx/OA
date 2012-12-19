using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MyADO;
using System.Data;
using FredCK.FCKeditorV2;
using GPRP.Entity.Basic;
using GPRP.Web.UI;

namespace GOA.Basic
{
    public partial class NewMaintainAdd : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DropListInite();
                ReDisplayOperatorTypeDetail();
                ScriptManager.RegisterOnSubmitStatement(this.FCKeditor1, FCKeditor1.GetType(), "FCKeditor1", "FCKUpdateLinkedField('" + FCKeditor1.ClientID + "');");   
               
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            NewsListEntity _NewsListEntity = new NewsListEntity();
            _NewsListEntity.CreateDate = DateTime.Now;
            _NewsListEntity.Creator = userEntity.UserName;
            if (txtExpDate.Text == "")
                _NewsListEntity.ExpireDate = DateTime.Parse("2038-01-01");
            else
                _NewsListEntity.ExpireDate = DateTime.Parse(txtExpDate.Text);

            _NewsListEntity.IsPublish = chkIsPublish.Checked==true ? '1':'0';
            _NewsListEntity.LastModifier = userEntity.UserName;
            _NewsListEntity.LastModifyDate = DateTime.Now;
            _NewsListEntity.NewsBody = FreeTextBox1.Text;
           

            _NewsListEntity.NewsTitle = txtTitle.Text;
            _NewsListEntity.NewsTypeID = Int32.Parse(dpType.SelectedValue);
            _NewsListEntity.PublishDate = DateTime.Parse("2012-01-01");
            string szResult = DbHelper.GetInstance().AddNewsListInfor(_NewsListEntity);

            NewsUser _NewsUser = new NewsUser();
            _NewsUser.NewsID =Convert.ToInt32(szResult);
            _NewsUser.NewsTypeDetailName = ddlOperatorTypeDetail.SelectedItem.Text;
            _NewsUser.NewsTypeuserName = txtObjectValueN.Text;
            DbHelper.GetInstance().AddNewsUser(_NewsUser);

            Context.Response.Redirect("NewMaintain.aspx");
        
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Context.Response.Redirect("NewMaintain.aspx");
        }

        protected void DropListInite()
        {
            DataTable dtNewType = DbHelper.GetInstance().GetNewsType();
            if (dtNewType.Rows.Count > 0)
            {
                for (int i = 0; i < dtNewType.Rows.Count; i++)
                    dpType.Items.Add(new ListItem(dtNewType.Rows[i]["NewsTypeDesc"].ToString(), dtNewType.Rows[i]["NewsTypeID"].ToString()));   //类别 

            }
            dpType.Items.Insert(0, "--请选择--");
        }



        private void ReDisplayOperatorTypeDetail()
        {
            string TypeCode = "10";
            DataTable dtTypeDetail = DbHelper.GetInstance().GetDBRecords("TypeDetailCode,DetailTypeName", "Workflow_OperatorTypeDetail", "TypeCode='" + TypeCode + "'", "TypeDetailCode");
            ddlOperatorTypeDetail.AddTableData(dtTypeDetail, true, "Null");
        }

    }
}
