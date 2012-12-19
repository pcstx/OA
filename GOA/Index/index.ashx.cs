using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyADO;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.Services;
using GPRP.GPRPBussiness;
using GPRP.Entity;

namespace GOA.Index
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class index : IHttpHandler
    {
        protected DataTable ProductTypeDataTable;
        private int id = 0;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request["type"];

            if (type == "menuList")
            {
                string json = treeMenuTree();
                 context.Response.Write(json);
            }
            else if (type == "treeList")
            {
                string parentId = context.Request["parentId"];
                string json = treeList(parentId);
                context.Response.Write(json);
            }
                       
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 返回顶级菜单
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<Menu> GenerateMenuTree(string parentId)
        {
            DataView dv = new DataView();
            List<Menu> l_m = new List<Menu>();

            dv.Table = ProductTypeDataTable.Copy();
            dv.RowFilter = "PSSMEPMC='" + parentId + "' and PSSMEUS='1' and PSSMEMP=''";  //顶级目录

            foreach (DataRowView drv in dv)
            {
                Menu m = new Menu();
                m.id = id;
                m.text = drv["PSSMEMN"].ToString();
                m.value = drv["PSSMEMC"].ToString();
                m.navigateUrl = drv["PSSMEMP"].ToString();
                m.target = drv["PSSMEOWT"].ToString();
                id++;
                m.children = GenerateMenuTree(drv["PSSMEMC"].ToString());

                l_m.Add(m);
            }

            return l_m;
        }

        /// <summary>
        /// 返回最终菜单
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<MenuList> GenerateMenuList(string parentId)
        {
            DataView dv = new DataView();
            List<MenuList> l_m = new List<MenuList>();

            dv.Table = ProductTypeDataTable.Copy();
            dv.RowFilter = "PSSMEPMC='" + parentId + "' and PSSMEUS='1'";  //顶级目录

            foreach (DataRowView drv in dv)
            {
                MenuList m = new MenuList();
                m.id = id;
                m.text = drv["PSSMEMN"].ToString();
                m.value = drv["PSSMEMC"].ToString();
                m.navigateUrl = drv["PSSMEMP"].ToString();
                m.target = drv["PSSMEOWT"].ToString();
                id++;

                l_m.Add(m);
            }

            return l_m;
        }

        protected string treeMenuTree()
        {
            string userName = WebUtils.GetCookieUser();
            UserListEntity userEntity = DbHelper.GetInstance().GetUserListEntityByUserID(userName);           

            ProductTypeDataTable = DbHelper.GetInstance().GetPSSMEInfoOfUser(userEntity.UserSerialID.ToString(), "");//获取所有目录 
            List<Menu> list_menu = GenerateMenuTree("");
            return new JavaScriptSerializer().Serialize(list_menu);
        }

        protected string treeList(string parentId)
        {
            string userName = WebUtils.GetCookieUser();
            UserListEntity userEntity = DbHelper.GetInstance().GetUserListEntityByUserID(userName);

            ProductTypeDataTable = DbHelper.GetInstance().GetPSSMEInfoOfUser(userEntity.UserSerialID.ToString(), "");//获取所有目录 
            List<MenuList> list_menu = GenerateMenuList(parentId);
            return new JavaScriptSerializer().Serialize(list_menu);
        }

    }

    class MenuList
    {
        public int id { get; set; }
        public string text { get; set; }
        public string value { get; set; }
        public string navigateUrl { get; set; }
        public string target { get; set; }
    }

    class Menu : MenuList
    {
        public List<Menu> children { get; set; }
    }

}
