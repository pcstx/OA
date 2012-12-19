using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using GPRP.GPRPComponents;
using MyADO;
using GPRP.GPRPControls;
using GPRP.GPRPEnumerations;
using GPRP.GPRPBussiness;
using GPRP.Entity;
namespace HRMWeb.aspx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public  class _01010 : IHttpHandler
    {
        //private string dbfile = ConfigurationManager.AppSettings["dbfile"];
        //private string http = ConfigurationManager.AppSettings["ip"];
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //if (context.Request.ServerVariables["HTTP_REFERER"] == null)
            //{
            //    context.Response.Write("不要这样访问");
            //    context.Response.End();
            //}
            //else if (!context.Request.ServerVariables["HTTP_REFERER"].StartsWith(http))
            //{
            //    context.Response.Write("不要这样访问");
            //    context.Response.End();
            //}

            //想看阻止操作效果时，可以加上下面这行
            //Thread.Sleep(2000);

            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            context.Response.AddHeader("pragma", "no-cache");
            context.Response.AddHeader("cache-control", "");
            context.Response.CacheControl = "no-cache";

            string action = "";
            string result = "false";
            if (context.Request.Params["action"] != null)
            {
                action = context.Request.Params["action"];
                if (action == "edit") result = DoUpdate(context);
                if (action == "Get") result = GetPBDEPEntity(context);
                if (action == "add") result = DoInsert(context);
                if (action == "del") result = DoDelete(context);
            }
            context.Response.Write(result);
        }
        private string GetPBDEPEntity(HttpContext context)
        {
            string PBDEPDC = context.Request.Params["PBDEPDC"];
            PBDEPEntity DeptEntity = DbHelper.GetInstance().GetPBDEPEntityByKeyCol(PBDEPDC);
            //string[] DeptArray=new string[7];
            //DeptArray[0] = DeptEntity.DeptCode;
            //DeptArray[1] = DeptEntity.DeptName;
            //DeptArray[2] = DeptEntity.DeptEName;
            //DeptArray[3] = DeptEntity.DeptTWName;
            //DeptArray[4] = DeptEntity.DeptIsValid;
            //DeptArray[5] = DeptEntity.DeptOrderItem.ToString();
            //DeptArray[6] = DeptEntity.ParentDeptCode;
            //DeptArray[7] = Convert.ToString(DeptEntity.DeptID);
            //DeptArray[8] = Convert.ToString(DeptEntity.ParentDeptID);
            string ParentDeptName="";
            if (DeptEntity.ParentDeptCode != "")
            {
                PBDEPEntity ParentDeptEntity = DbHelper.GetInstance().GetPBDEPEntityByKeyCol(DeptEntity.ParentDeptCode);
                ParentDeptName = ParentDeptEntity.DeptName;
            }
            else
                ParentDeptName = "";
            return DeptEntity.DeptCode + "|" + DeptEntity.DeptName + "|" + DeptEntity.DeptEName + "|" + DeptEntity.DeptTWName + "|" + DeptEntity.DeptIsValid + "|" + DeptEntity.DeptOrderItem.ToString() + "|" + DeptEntity.ParentDeptCode + "|" + Convert.ToString(DeptEntity.DeptID) + "|" + Convert.ToString(DeptEntity.ParentDeptID) + "|" + ParentDeptName;
            //return DeptArray;
        }
        private string DoUpdate(HttpContext context)
        {
            string result = "";
            string PBDEPDC = context.Request.Params["PBDEPDC"];
            string PBDEPDN = context.Request.Params["PBDEPDN"];
            string PBDEPDEN = context.Request.Params["PBDEPDEN"];
            string PBDEPDTWN = context.Request.Params["PBDEPDTWN"];
            string PBDEPUS = context.Request.Params["PBDEPUS"];
            string PBDEPOI = context.Request.Params["PBDEPOI"];
            string PBDEPPDC = context.Request.Params["PBDEPPDC"];
            string PBDEPID = context.Request.Params["PBDEPID"];
            string PBDEPPID = context.Request.Params["PBDEPPID"];
            string NewPBDEPPID = context.Request.Params["NewPBDEPPID"];
            string OldPBDEPPDC = context.Request.Params["OldPBDEPPDC"];
            if (PBDEPOI == "")
            {
                PBDEPOI = "0";
            }
            PBDEPEntity DeptEntity = new PBDEPEntity();
            DeptEntity.DeptID = PBDEPID;
            DeptEntity.DeptCode = PBDEPDC;
            DeptEntity.DeptName = PBDEPDN;
            DeptEntity.DeptEName = PBDEPDEN;
            DeptEntity.DeptTWName = PBDEPDTWN;
            DeptEntity.DeptIsValid = PBDEPUS;
            DeptEntity.DeptOrderItem = Convert.ToInt16(PBDEPOI);
            DeptEntity.ParentDeptCode = OldPBDEPPDC;
            DeptEntity.ParentDeptID = PBDEPPID;
            if (PBDEPPID == NewPBDEPPID)
            {
                result = DbHelper.GetInstance().UpDateDeptInfor(DeptEntity);
            }
            else
            {
                result = DbHelper.GetInstance().UpDateDeptInfor(DeptEntity, NewPBDEPPID);
                
            }
            //add Rio
            int icount = 0;
            string szItemName = "";
            string szItemValue = "";
            string AddItemStr = context.Request.Params["str"];
            string[] ItemStr = AddItemStr.Split('|');
            foreach (string istr in ItemStr)
            {
                string[] ItemValue = istr.Split('=');
                foreach (string i in ItemValue)
                {
                    icount++;
                    if (icount % 2 != 0)
                        szItemName = i.ToString();
                    else
                    {
                        szItemValue = i.ToString();
                        //DataTable dt = DbHelper.GetInstance().DoGetAddItem(PBDEPDC);
                        //if (dt.Rows.Count == 0)
                        result = DbHelper.GetInstance().AddTempDeptInforTemp(PBDEPDC, szItemName, szItemValue);  //PBDEPDC表中没有相应的ID值，没有则添加，有则更新。
                        //else
                        //    result = DbHelper.GetInstance().UpdateTempDeptInforTemp(PBDEPDC, szItemName, szItemValue);  
                         
                        if (result == "-1")
                        {
                            result = "-1";

                        }
                    }
                }
            }
          
            return result;
        }

        private string DoDelete(HttpContext context)
        {
            string result = "";
            string szResult = "";
            string PBDEPDC = context.Request.Params["PBDEPDC"];
            result = DbHelper.GetInstance().DeleteDeptInfor(PBDEPDC);
            szResult = DbHelper.GetInstance().DeleteDeptTempInfor(PBDEPDC);
            return result;
        }

        private string DoInsert(HttpContext context)
        {
            string result = "";
            
            string PBDEPDN = context.Request.Params["PBDEPDN"];
            string PBDEPDEN = context.Request.Params["PBDEPDEN"];
            string PBDEPDTWN = context.Request.Params["PBDEPDTWN"];
            string PBDEPUS = context.Request.Params["PBDEPUS"];
            string PBDEPOI = context.Request.Params["PBDEPOI"];
            string PBDEPPDC = context.Request.Params["PBDEPPDC"];
            string PBDEPPID = context.Request.Params["PBDEPPID"];
            if (PBDEPOI == "")
            {
                PBDEPOI = "0";
            }
            string PBDEPDC = DbHelper.GetInstance().GetPBDEPDCbyPBDEPPDC(PBDEPPDC);
            if (PBDEPDC == "-1")
            {
                result = "-1";
                return result;
            }
            PBDEPEntity DeptEntity = new PBDEPEntity();
            DeptEntity.DeptCode = PBDEPDC;
            DeptEntity.DeptName = PBDEPDN;
            DeptEntity.DeptEName = PBDEPDEN;
            DeptEntity.DeptTWName = PBDEPDTWN;
            DeptEntity.DeptIsValid = PBDEPUS;
            DeptEntity.DeptOrderItem = Convert.ToInt16(PBDEPOI);
            DeptEntity.ParentDeptCode = PBDEPPDC;
            DeptEntity.ParentDeptID = PBDEPPID;
            result = DbHelper.GetInstance().AddDeptInfor(DeptEntity);
            if (result == "-1")
             {
                 result = "-1";
             }
             
             else
             {
                 result = PBDEPDC ;
             }
            //add Rio
           int icount = 0;
           string szItemName="";
           string szItemValue="";
           string AddItemStr = context.Request.Params["str"];
           string[] ItemStr = AddItemStr.Split('|');
           foreach (string istr in ItemStr)
           {
               string[] ItemValue = istr.Split('=');
               foreach (string i in ItemValue)
               {
                   icount++;
                   if (icount % 2 != 0)
                       szItemName = i.ToString();
                   else
                   {
                       szItemValue = i.ToString();
                       result = DbHelper.GetInstance().AddTempDeptInforTemp(PBDEPDC, szItemName, szItemValue);
                       if (result == "-1")
                       {
                           result = "-1";

                       }
                   }
               }
           }

            return result;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
