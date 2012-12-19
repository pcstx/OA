using System;
using System.Web.UI;
using System.ComponentModel;
using System.Xml;
using System.Data;
using GPRP.GPRPComponents;
using GPRP.GPRPBussiness;
using MyADO;

namespace GPRP.GPRPControls
{
	/// <summary>
	/// 导航控件
	/// </summary>
	[DefaultProperty("Text"), ToolboxData("<{0}:NavMenu runat=server></{0}:NavMenu>")]
	public class NavMenu : System.Web.UI.WebControls.WebControl
	{
        /// <summary>
        /// 构造函数        /// </summary>
		public NavMenu()
		{}


	    #region Property ScriptPath

		/// <summary>
		/// Javascript脚本文件所在目录。		/// </summary>
        [Description("Javascript脚本文件所在目录。"), DefaultValue("../../JScript/")]
		public string ScriptPath 
		{
			get
			{
				object obj = ViewState["NavMenuScriptPath"];
                return obj == null ? "../../JScript/Navbar.js" : (string)obj;
			}
			set
			{
				ViewState["NavMenuScriptPath"] = value;
			}
		}

		#endregion


		#region Property ImageUrl

        /// <summary>
        /// 图片路径
        /// </summary>
		[Bindable(true), Category("Appearance"), DefaultValue("")] 
		public string ImageUrl 
		{
			get
			{
				if (base.ViewState["NavMenuimageurl"] != null)
				{
					return (String)base.ViewState["NavMenuimageurl"];
				}
				else
				{
					return "../../images/";//String.Empty;
				}
			}
			set
			{
				base.ViewState["NavMenuimageurl"] = value;
			}
		}

		#endregion


		#region Property CssPath

		/// <summary>
		/// Css文件所在目录。		/// </summary>
        [Description("Css文件所在目录。"), DefaultValue("../../styles/")]
		public string CssPath 
		{
			get
			{
				object obj = ViewState["NavMenuCssPath"];
				return obj == null ? "../../styles/nav.css" :(string) obj;
			}
			set
			{
				ViewState["NavMenuCssPath"] = value;
			}
		}

		#endregion


		#region Property XmlFileFullPathName

		/// <summary>
		/// Xml文件所在目录。		/// </summary>
        [Description("Xml文件所在目录。"), DefaultValue("../../config/")]
		public string XmlFileFullPathName 
		{
			get
			{
				object obj = ViewState["NavMenuXmlFileFullPathName"];
                return obj == null ? "../../config/navmenu.config" : (string)obj;
			}
			set
			{
				ViewState["NavMenuXmlFileFullPathName"] = value;
			}
		}

		#endregion



		#region Property ExpandAll

		/// <summary>
		/// 是否全部展开。		/// </summary>
		[Description("是否全部展开"),DefaultValue(false)]
		public bool ExpandAll
		{
			get
			{
				object obj = ViewState["ExpandAll"];
				return ((obj != null)&&(obj.ToString().ToLower()=="true")) ? true :false;
			}
			set
			{
				ViewState["ExpandAll"] = value;
			}
		}

		#endregion



		/// <summary> 
        /// 输出html,在浏览器中显示控件		/// </summary>
		/// <param name="output"> 要写出到的 HTML 编写器 </param>
		protected override void Render(HtmlTextWriter output)
		{
            //暂时关闭页面级缓存


            if (this.Page.Cache["NavMenuContent"] == null)
            {

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + this.CssPath + "\" />");
                sb.Append("<script type=\"text/javascript\">var imgpath='" + this.ImageUrl + "';var expandall=" + this.ExpandAll.ToString().ToLower() + ";</script>");
                sb.Append("<script type=\"text/javascript\" src=\"" + this.ScriptPath + "\"></script>");
                sb.Append("<div class=\"NavManagerMenu\" id=\"NavManagerMenu\">\r\n");
                sb.Append("<ul>\r\n");

                XmlDocumentExtender doc = new XmlDocumentExtender();
                doc.Load(Page.Server.MapPath(this.XmlFileFullPathName));
                //XmlNodeList subMenus = doc.SelectNodes("/dataset/mainmenu");
                //XmlNodeList subMenuItems = doc.SelectNodes("/dataset/submain");
                DataTable MainMenu = null;// DBOperatorFactory.GetDBOperator(BaseConfigs.GetDBCS).GetPSSMEInfoByPBACCID("", "__");
                DataTable Submain = null;// DBOperatorFactory.GetDBOperator(BaseConfigs.GetDBCS).GetPSSMEInfoByPBACCID("", "____");
                DataTable dt = new DataTable();
                dt.Columns.Add("menuparentid");
                dt.Columns.Add("menutitle");
                dt.Columns.Add("link");
                dt.Columns.Add("frameid");
                //foreach (XmlNode menuItem in subMenuItems)
                //{
                //    DataRow dr = dt.NewRow();
                //    dr["menuparentid"] = menuItem["menuparentid"].InnerText;
                //    dr["menutitle"] = menuItem["menutitle"].InnerText;
                //    dr["link"] = menuItem["link"].InnerText;
                //    dr["frameid"] = menuItem["frameid"].InnerText;
                //    dt.Rows.Add(dr);
                //}
              
                foreach (DataRow subdr in Submain.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["menuparentid"] = subdr["PSSMEPMC"];
                    //language choose                    
                    switch (WebUtils.GetCookie("Language").ToString())
                    {
                        case "zh-CN":
                            dr["menutitle"] = subdr["PSSMEMN"].ToString();
                            break;
                        case "zh-TW":
                            dr["menutitle"] = subdr["PSSMEMNTW"].ToString();
                            break;
                        case "en-US":
                            dr["menutitle"] = subdr["PSSMEMNEN"].ToString();
                            break;
                        case "":
                            dr["menutitle"] = subdr["PSSMEMN"].ToString();
                            break;
                    }
                  
                    dr["link"] = subdr["PSSMEMP"];
                    dr["frameid"] = subdr["PSSMEOWT"];
                    dt.Rows.Add(dr);
                }
                int count = 0;//对显示没有什么影响


                //foreach (XmlNode subMenuItem in subMenus)
                //{

                //    #region 输出主菜单


                //    sb.Append("<li Class=\"CurrentItem\" ><div class=\"current\" onmousedown=\"gomenu(event)\"><cite>&nbsp;&nbsp;&nbsp;<a href=\"#\" style=\"font-weight:bold;\" onfocus=\"this.blur();\">" + subMenuItem["menutitle"].InnerText + "</a></cite><span class=\"title\" id=\"top\" ><img src=\"../../images/dropdown.gif\" class=\"arrow\" style=\"z-Index:-1;\"/></span></div></li>\r\n");
                //    #endregion

                //    #region 输出子菜单


                //    sb.Append("<li class=\"Submenu\">\r\n");
                //    sb.Append("<div class=\"Submenu1\">\r\n");
                //    sb.Append("<table><tbody>\r\n");
                //    foreach (System.Data.DataRow drs in dt.Select("menuparentid='" + subMenuItem["menuid"].InnerText + "'"))
                //    {
                //        sb.Append("<tr>\r\n");
                //        if ((drs["frameid"].ToString().Trim() == "top") || (drs["frameid"].ToString().Trim() == ""))
                //        {
                //            sb.Append("<td><a id='menuitem" + count + "' name='menuitem' href=\"javascript:void(0);\" onclick=\"javascript:document.getElementById('main').src='../" + drs["link"].ToString().Trim() + "';SetMenuItemFocus(this);\" onfocus=\"this.blur();\">" + drs["menutitle"].ToString().Trim() + "</a></td>\r\n");
                //        }
                //        else
                //        {
                //            sb.Append("<td><a id='menuitem" + count + "' name='menuitem' href=\"javascript:void(0);\" onclick=\"javascript:document.getElementById('" + drs["frameid"].ToString().Trim() + "').src='../" + drs["link"].ToString().Trim() + "';SetMenuItemFocus(this);\"  onfocus=\"this.blur();\">" + drs["menutitle"].ToString().Trim() + "</a></td>\r\n");
                //        }

                //        sb.Append("</tr>\r\n");
                //    }
                //    sb.Append("</tbody></table>\r\n");
                //    sb.Append("</div>\r\n");
                //    sb.Append("</li>\r\n");
                //    #endregion

                //    count++;
                //}
                string MenuName = "";

                foreach (DataRow subMenuItem in MainMenu.Rows)
                {

                    #region 输出主菜单


                    //language choose subMenuItem["PSSMEMN"]
                    switch (WebUtils.GetCookie("Language").ToString())
                    {
                        case "zh-CN":
                            MenuName = subMenuItem["PSSMEMN"].ToString();
                            break;
                        case "zh-TW":
                            MenuName = subMenuItem["PSSMEMNTW"].ToString();
                            break;
                        case "en-US":
                            MenuName = subMenuItem["PSSMEMNEN"].ToString();
                            break;
                        case "":
                            MenuName = subMenuItem["PSSMEMN"].ToString();
                            break;
                    }
                    sb.Append("<li Class=\"CurrentItem\" ><div class=\"current\" onmousedown=\"gomenu(event)\"><cite>&nbsp;&nbsp;&nbsp;<a href=\"#\" style=\"font-weight:bold;\" onfocus=\"this.blur();\">" +  MenuName + "</a></cite><span class=\"title\" id=\"top\" ><img src=\"../../images/dropdown.gif\" class=\"arrow\" style=\"z-Index:-1;\"/></span></div></li>\r\n");
                    #endregion

                    #region 输出子菜单


                    sb.Append("<li class=\"Submenu\">\r\n");
                    sb.Append("<div class=\"Submenu1\">\r\n");
                    sb.Append("<table><tbody>\r\n");
                    foreach (System.Data.DataRow drs in dt.Select("menuparentid='" + subMenuItem["PSSMEMC"] + "'"))
                    {
                        sb.Append("<tr>\r\n");
                        if ((drs["frameid"].ToString().Trim() == "top") || (drs["frameid"].ToString().Trim() == ""))
                        {
                            sb.Append("<td><a id='menuitem" + count + "' name='menuitem' href=\"javascript:void(0);\" onclick=\"javascript:document.getElementById('main').src='../" + drs["link"].ToString().Trim() + "';SetMenuItemFocus(this);\" onfocus=\"this.blur();\">" + drs["menutitle"].ToString().Trim() + "</a></td>\r\n");
                        }
                        else
                        {
                            sb.Append("<td><a id='menuitem" + count + "' name='menuitem' href=\"javascript:void(0);\" onclick=\"javascript:document.getElementById('" + drs["frameid"].ToString().Trim() + "').src='../" + drs["link"].ToString().Trim() + "';SetMenuItemFocus(this);\"  onfocus=\"this.blur();\">" + drs["menutitle"].ToString().Trim() + "</a></td>\r\n");
                        }

                        sb.Append("</tr>\r\n");
                    }
                    sb.Append("</tbody></table>\r\n");
                    sb.Append("</div>\r\n");
                    sb.Append("</li>\r\n");
                    #endregion

                    count++;
                }
                sb.Append("</div>\r\n");
                this.Page.Cache["NavMenuContent"] = sb.ToString();
            }
                
            
            output.Write(this.Page.Cache["NavMenuContent"].ToString());
		}
	}
}
