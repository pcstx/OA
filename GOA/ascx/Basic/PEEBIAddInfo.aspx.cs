using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GOA.Basic
{
    public partial class PEEBIAddInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDropDownList();
        }

        private void BindDropDownList()
        {

            string[] DataType;
            DataType = new string[] {"varchar(50)", "datetime", "int"};
            for (int i = 0; i < DataType.Length ; i++)
            {
                dpdPEEBITYPE.Items.Add(new ListItem(DataType[i], i.ToString()));
            }

            dpdPEEBITYPE.Items.Insert(0, "--请选择数据类型--");

        }
    }
}
