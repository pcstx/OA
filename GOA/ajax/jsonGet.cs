using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;
using MyADO;
using System.Data;
using GPRP.Entity;
using System.Collections;
using System.Reflection;

namespace GOA.ajax
{
    public partial class json
    {       
        private static string getDetailFieldGroup()
        {
            dbRecordsArg arg = new dbRecordsArg("GroupID,GroupName", "Workflow_FormFieldGroup", "FormID=", "DisplayOrder"); 
            string[] n = { "GroupName", "GroupID" };

            return commandgetValue(arg, n); 
        }

        /// <summary>
        /// 获取CSS样式种类
        /// </summary>
        /// <returns></returns>
        private static string getCssStyleClass()
        {
            dbRecordsArg arg = new dbRecordsArg("CSSStyleID,CSSStyleClass", "Workflow_FieldCSSStyle", "1=1", "CSSStyleID");
            string[] n = { "CSSStyleClass", "CSSStyleID" };
            
            return commandgetValue(arg, n);
        }

        private static string getFormType()
        {
            dbRecordsArg arg = new dbRecordsArg("FormTypeID,FormTypeName", "Workflow_FormType", "Useflag='1'", "DisplayOrder");
            string[] n = { "FormTypeName", "FormTypeID" };

            return commandgetValue(arg, n); 
        }

        /// <summary>
        /// 获取单行文本框类型
        /// </summary>
        /// <returns></returns>
        private static string getFieldDataType()
        {
            dbRecordsArg arg = new dbRecordsArg("*", "Workflow_FieldDataType", "Useflag='1'", "DataTypeID");
            string[] n = { "DataTypeName", "DataTypeID" };

            return commandgetValue(arg, n);             
        }

        private static string getDateFormat(string DateType)
        {
            dbRecordsArg arg = new dbRecordsArg("DateFormatID,DateFormatText", "Workflow_DateFormat", "Useflag='1' and DateType=" + DateType, "DisplayOrder");
            string[] n = { "DateFormatID", "DateFormatText" };

            return commandgetValue(arg, n);  
        }

        private static string getBrowseType()
        {
            dbRecordsArg arg = new dbRecordsArg("*", "Workflow_BrowseType", "Useflag='1'", "BrowseTypeID");
            string[] n = { "BrowseTypeDesc", "BrowseTypeID" };

            return commandgetValue(arg, n);                
        }

        /////////////////////////////

        private static string getFieldDictSelect(string fieldID)
        {
            dbRecordsArg arg = new dbRecordsArg("*", "Workflow_FieldDictSelect", "FieldID=" + fieldID, "DisplayOrder");
            string[] n = { "SelectNo", "LabelWord" };

            return commandgetJson<FieldDictSelect>(arg, n);

            //DataTable dt = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FieldDictSelect", "FieldID=" + fieldID, "DisplayOrder");

            //IList<FieldDictSelect> list = new List<FieldDictSelect>();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    list.Add(new FieldDictSelect()
            //    {
            //        selectNo = int.Parse(dr["SelectNo"].ToString()),
            //        LabelWord = dr["LabelWord"].ToString()
            //    });
            //}

            //var griddata = new { Rows = list };
            //string s = new JavaScriptSerializer().Serialize(griddata);
            //return s;
        }
  
        private static string getHTMLType()
        {
            dbRecordsArg arg = new dbRecordsArg("HTMLTypeID,HTMLTypeDesc", "Workflow_HTMLType", "Useflag='1'", "HTMLTypeID");
            string[] n={"HTMLTypeID","HTMLTypeDesc"};
            return  commandgetJson<HTMLTypeNode>(arg, n);

           
            //DataTable dtHTMLType = DbHelper.GetInstance().GetDBRecords("HTMLTypeID,HTMLTypeDesc", "Workflow_HTMLType", "Useflag='1'", "HTMLTypeID");
            //IList<HTMLTypeNode> list = new List<HTMLTypeNode>();
          
            //foreach (DataRow dr in dtHTMLType.Rows)
            //{
            //    list.Add(new HTMLTypeNode()
            //    {
            //        HTMLTypeID = int.Parse(dr["HTMLTypeID"].ToString()),
            //        HTMLTypeDesc = dr["HTMLTypeDesc"].ToString()
            //    });
            //}

            //var griddata = new { Rows = list };
            //string s = new JavaScriptSerializer().Serialize(griddata);
            //return s;
        }

        /// <summary>
        /// 获取现有元素列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="sortname"></param>
        /// <param name="sortorder"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldDesc"></param>
        /// <param name="HTMLTypeID"></param>
        /// <param name="FieldTypeID"></param>
        /// <returns></returns>
        private string getFieldDict(int page, int pagesize, string sortname, string sortorder, string fieldName, string fieldDesc, string HTMLTypeID, string FieldTypeID)
        {
            string where = "1=1";
            if (fieldName != "" && fieldName != null)
                where += " and FieldName like '%" + fieldName + "%'";
            if (fieldDesc != "" && fieldDesc != null)
                where += " and FieldDesc like '%" + fieldDesc + "%'";
            if (HTMLTypeID != "" && HTMLTypeID != null && HTMLTypeID != "0")
                where += " and HTMLTypeID like " + HTMLTypeID + "";
            if (FieldTypeID != "" && FieldTypeID != null)
                where += " and FieldTypeID= " + FieldTypeID + "";

            string sql = "select count(*) from Workflow_FieldDict where " + where;
            string totalcount = DbHelper.GetInstance().ExecSqlResult(sql);  //总行数

            //dbRecordsArg arg = new dbRecordsArg("*", "Workflow_FieldDict", where, "FieldName", pagesize, page);
            //string[] n = { "FieldID", "FieldName", "FieldDesc", "HTMLTypeID", "HTMLTypeN", "SqlDbLength", "TextLength", "FieldTypeID", "DataTypeID", "Dateformat", "TextHeight", "IsHTML",
            //             "BrowseType","IsDynamic","DataSetID","ValueColumn","TextColumn" };
            //return  commandgetJson<Node>(arg, n,sortname,sortorder,totalcount);

            DataTable dt = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FieldDict", where, "FieldName", pagesize, page);
            ExtendDatatable(dt);

            IList<Node> list = new List<Node>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new Node()
                {
                    FieldID = int.Parse(dr["FieldID"].ToString()),
                    FieldName = dr["FieldName"].ToString(),
                    FieldDesc = dr["FieldDesc"].ToString(),
                    HTMLTypeID = int.Parse(dr["HTMLTypeID"].ToString()),
                    HTMLTypeN = dr["HTMLTypeN"].ToString(),
                    SqlDbLength = int.Parse(dr["SqlDbLength"].ToString()),
                    SqlDbType = dr["SqlDbType"].ToString(),
                    TextLength = int.Parse(dr["TextLength"].ToString()),
                    FieldTypeID = int.Parse(dr["FieldTypeID"].ToString()),
                    DataTypeID = int.Parse(dr["DataTypeID"].ToString()),
                    Dateformat = dr["Dateformat"].ToString(),
                    TextHeight = int.Parse(dr["TextHeight"].ToString()),
                    IsHTML = dr["IsHTML"].ToString(),
                    BrowseType = int.Parse(dr["BrowseType"].ToString()),
                    IsDynamic = int.Parse(dr["IsDynamic"].ToString()),
                    DataSetID = int.Parse(dr["DataSetID"].ToString()),
                    ValueColumn = dr["ValueColumn"].ToString(),
                    TextColumn = dr["TextColumn"].ToString(),
                });
            }

            if (sortorder == "desc")
                list = list.OrderByDescending(c => sortname).ToList();
            else
                list = list.OrderBy(c => sortname).ToList();

            var griddata = new { Rows = list, Total = totalcount };
            string s = new JavaScriptSerializer().Serialize(griddata);
            return s;
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="sortname"></param>
        /// <param name="sortorder"></param>
        /// <returns></returns>
        private static string getDataSet(int page, int pagesize, string sortname, string sortorder, string DataSetName)
        {
            string WhereCondition = " 1=1 ";
            if (DataSetName != "" && DataSetName != null)
            {
                WhereCondition += " and a.DataSetName like '%" + DataSetName + "%'";
            }

            string sql = "select count(*) from Workflow_DataSet a left join Workflow_DataSource b on a.DataSourceID=b.DataSourceID where " + WhereCondition;
            string totalcount = DbHelper.GetInstance().ExecSqlResult(sql);  //总行数

            dbRecordsArg arg = new dbRecordsArg("a.*,b.DataSourceName", "Workflow_DataSet a left join Workflow_DataSource b on a.DataSourceID=b.DataSourceID", WhereCondition, "DataSetID", pagesize, page);
            string[] n = { "DataSetID", "DataSetName", "ReturnColumns","QuerySql" };
            return commandgetJson<DataSetNode>(arg, n, sortname, sortorder, totalcount);


            //DataTable dt = DbHelper.GetInstance().GetDBRecords("a.*,b.DataSourceName", "Workflow_DataSet a left join Workflow_DataSource b on a.DataSourceID=b.DataSourceID", WhereCondition, "DataSetID", pagesize, page);
            //IList<DataSetNode> list = new List<DataSetNode>();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    list.Add(new DataSetNode()
            //    {
            //        DataSetID = int.Parse(dr["DataSetID"].ToString()),
            //        DataSetName = dr["DataSetName"].ToString(),
            //        ReturnColumns = dr["ReturnColumns"].ToString(),
            //        QuerySql = dr["QuerySql"].ToString()
            //    });
            //}

            //if (sortorder == "desc")
            //    list = list.OrderByDescending(c => sortname).ToList();
            //else
            //    list = list.OrderBy(c => sortname).ToList();

            //var griddata = new { Rows = list, Total = totalcount };
            //string s = new JavaScriptSerializer().Serialize(griddata);
            //return s;
        }
              
        /// <summary>
        /// 通用方法
        /// </summary>
        /// <param name="arg">sql语句条件</param>
        /// <param name="need">json输出元素</param>
        /// <returns>json结果</returns>
        private static string commandgetValue(dbRecordsArg arg, IList<string> need)
        {
            string form = "";
            DataTable dt = DbHelper.GetInstance().GetDBRecords(arg.columnList, arg.tableList, arg.WhereCondition, arg.orderby);
            foreach (DataRow dr in dt.Rows)
            {
                string tmp = "";
                foreach (var s in need)
                {
                    tmp += "\"" + dr[s].ToString() + "\",";
                }

                tmp = (tmp != "") ? tmp.Substring(0, tmp.Length - 1) : tmp;
                form += "[" + tmp + "],";
            }

            form = (form != "") ? form.Substring(0, form.Length - 1) : form;

            string json = "[" + form + "]";
            return json;
        }
         
        private static string commandgetJson<T>(dbRecordsArg arg, IList<string> need) where T : new()
        {
            DataTable dtHTMLType = DbHelper.GetInstance().GetDBRecords(arg.columnList, arg.tableList, arg.WhereCondition, arg.orderby); 
            IList<T> list = new List<T>();
 
            foreach (DataRow dr in dtHTMLType.Rows)
            {
                T t=new T();

                Type type = typeof(T);
                for (int i = 0; i < type.GetProperties().Length; i++)
                {
                    PropertyInfo pi = type.GetProperties()[i]; 
                    string tp = pi.PropertyType.Name;

                    if (pi.PropertyType == typeof(Int32))
                    {
                        pi.SetValue(t,int.Parse(dr[need[i]].ToString()), null);
                    }
                    else if (pi.PropertyType == typeof(String))
                    {
                        pi.SetValue(t, dr[need[i]].ToString(), null);
                    } 
                }

                list.Add(t); 
            }  

            var griddata = new { Rows = list };
            string s = new JavaScriptSerializer().Serialize(griddata);
            return s;
        }

        private static string commandgetJson<T>(dbRecordsArg arg, IList<string> need, string sortname, string sortorder, string totalcount) where T : new()
        {
            DataTable dtHTMLType = DbHelper.GetInstance().GetDBRecords(arg.columnList, arg.tableList, arg.WhereCondition, arg.orderby);
            
            IList<T> list = new List<T>();

            foreach (DataRow dr in dtHTMLType.Rows)
            {
                T t = new T();

                Type type = typeof(T);
                for (int i = 0; i < type.GetProperties().Length; i++)
                {
                    PropertyInfo pi = type.GetProperties()[i];
                    string tp = pi.PropertyType.Name;

                    if (pi.PropertyType == typeof(Int32))
                    {
                        pi.SetValue(t, int.Parse(dr[need[i]].ToString()), null);
                    }
                    else if (pi.PropertyType == typeof(String))
                    {
                        pi.SetValue(t, dr[need[i]].ToString(), null);
                    }
                }

                list.Add(t);
            }

            if (sortorder == "desc")
                list = list.OrderByDescending(c => sortname).ToList();
            else
                list = list.OrderBy(c => sortname).ToList();

            var griddata = new { Rows = list, Total = totalcount };
            string s = new JavaScriptSerializer().Serialize(griddata);
            return s;
        }
    }
}
