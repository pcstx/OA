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
using MyADO;
using GPRP.GPRPControls;
using GPRP.GPRPEnumerations;
using GPRP.GPRPBussiness;
using GPRP.Entity;
using YYControls;
using VBControls.VBProject;
using System.DirectoryServices;

namespace GOA
{
    public partial class login : System.Web.UI.Page
    {

       

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string user = DNTRequest.GetString("user").Trim();
            string pwd = DNTRequest.GetString("pass").Trim();
            

            string vCode = DNTRequest.GetString("vcode");
            if (Request.Cookies["CheckCode"] == null)
            {
                error_div.InnerText = "您的浏览器设置已被禁用 Cookies，您必须设置浏览器允许使用 Cookies 选项后才能使用本系统。";
                return;
            }

            if (String.Compare(Request.Cookies["CheckCode"].Value, vCode.Trim(), true) != 0)
            {
                error_div.InnerText = "验证码错误！";
                return;
            }

            UserListEntity u = DbHelper.GetInstance().GetUserListEntityByUserID(user);
            if (u != null && "1".Equals(u.UseFlag))
                {

                //判断是否公司内部用户，是的话用AD验证
                if (u.UserType == "0")
                    {
                    try
                        {
                            //string ldapPath = GeneralConfigs.GetConfig().LdapPath;
                            //DirectoryEntry entry = new DirectoryEntry(ldapPath, user, pwd, AuthenticationTypes.Secure);
                            ////DirectorySearcher searcher = new DirectorySearcher("(&(objectCategory=person)(objectClass=user))");
                            //DirectorySearcher searcher = new DirectorySearcher(entry);
                            ////SearchResult sResultSet;
                            //searcher.Filter = "(sAMAccountName=" + user + ")";
                            //searcher.PropertiesToLoad.Add("cn");
                            //searcher.PropertiesToLoad.Add("name");
                            //searcher.PropertiesToLoad.Add("telephoneNumber");
                            //searcher.PropertiesToLoad.Add("mail");
                            //SearchResult result = searcher.FindOne();
                            //if (result != null)
                            if (1 == 1 )
                            {

                            //登录成功,保存 cookies
                            WebUtils.WriteUserCookie(user, pwd, -1);
                            string gopage = DNTRequest.GetString("gopage");
                            Session["gopage"] = gopage;
                            Response.Redirect("index.aspx");
                            }
                        else
                            {
                            //error 密码错误
                            error_div.InnerText = "帐号密码错误验证错误";
                            }
                        }
                    catch
                        {
                        error_div.InnerText = "帐号密码错误验证错误";
                        }
                    }
                else
                    {
                    try
                        {

                        if (String.Compare(u.PassWord, pwd, false) != 0)
                            {
                            //error 密码错误
                            error_div.InnerText = "密码错误";
                            }
                        else
                            {
                            //登录成功,保存 cookies
                            WebUtils.WriteUserCookie(user, pwd, -1);
                            string gopage = DNTRequest.GetString("gopage");
                            Session["gopage"] = gopage;
                            Session["UserID"] = u.UserSerialID;
                            Response.Redirect("index/index.aspx");
                            }

                        }
                    catch
                        {
                        error_div.InnerText = "帐号密码错误验证错误";
                        }
                    }
                }
            else
                {
                //用户名不存在
                error_div.InnerText = "用户名不存在或被禁用";
                }
        }
    }
}
