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
using GPRP.Web.UI;
using GPRP.GPRPComponents;
using GPRP.GPRPControls;
using GPRP.GPRPEnumerations;
using GPRP.GPRPBussiness;
using GPRP.Entity;
using MyADO;

namespace GOA
{
    public partial class Z030 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string CookiesPwd = WebUtils.GetCookiePassword(config.Passwordkey);
            string oldPwd = txtoldPwd.Text.ToString();
            string newPwd = txtNewPwd.Text.ToString();
            if (String.Compare(oldPwd, CookiesPwd, false) != 0)
            {
                //原始密码错误
                lblMsg.InnerText = "原始密码错误!";
            }
            else
            {
                //修改成功后
                userEntity.PassWord = newPwd;
                DbHelper.GetInstance().UpdateUserList(userEntity);
                WebUtils.WriteUserCookie(userEntity.UserID, userEntity.PassWord, -1);
                lblMsg.InnerText = "修改成功!";
            }
        }
    }
}
