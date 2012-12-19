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
using GPRP.GPRPBussiness; 

namespace GOA.ajax
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public partial class json : IHttpHandler
    { 
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain"; 
           string type = context.Request["type"];
              
           switch (type)
           {
               case "toForm":                 
                   int FormID=Convert.ToInt32(context.Request["FormID"]);
                   context.Response.Write(FormBaseToJson(FormID));
                   break;
               case "ValidateFormFieldGroupName":
                   string GroupName = context.Request["GroupName"];
                   context.Response.Write(ValidateFormFieldGroupName(GroupName));
                   break;
               case "ValidateFormName": 
                   string formName=context.Request["FormName"];
                   context.Response.Write(ValidateFormName(formName));
                   break;
               case "ValidateFieldName":
                   string filedName = context.Request["FieldName"];
                   context.Response.Write(ValidateFiledName(filedName));
                   break;
               case "getHTMLType":
                   context.Response.Write(getHTMLType());
                   break;
               case "getBrowseType":                   
                   context.Response.Write(getBrowseType());
                   break;
               case "getDateFormat":
                   string DateType = context.Request["DateType"];  //获取ajax传的值
                   context.Response.Write(getDateFormat(DateType));
                   break;
               case "getCSSStyleClass": 
                   context.Response.Write(getCssStyleClass());
                   break;
               case "getFieldDataType":
                   context.Response.Write(getFieldDataType());  //获取单行文本类型
                   break;   
               case "getFormType":
                   string result = getFormType();
                   context.Response.Write(result);
                   break;
               case "getFieldData":
               case "getDataSet":
                   int page = int.Parse(context.Request["page"]);  //当前页数
                   int pagesize = int.Parse(context.Request["pagesize"]); //每页行数
                   string sortname = context.Request["sortname"]; //排序名称
                   string sortorder = context.Request["sortorder"];   //排序方式 
                  
                   string json = "";
                   if (type == "getFieldData")
                   {
                       string fieldName =System.Web.HttpUtility.UrlDecode( context.Request["fieldName"]);
                       string fieldDesc =System.Web.HttpUtility.UrlDecode( context.Request["fieldDesc"]);
                       string HTMLTypeID =System.Web.HttpUtility.UrlDecode( context.Request["HTMLTypeID"]);
                       string FieldTypeID = context.Request["FieldTypeID"];
                       json = getFieldDict(page, pagesize, sortname, sortorder, fieldName, fieldDesc, HTMLTypeID, FieldTypeID); //返回json格式数据
                   }
                   else if (type == "getDataSet")
                   {
                       string DataSetName =System.Web.HttpUtility.UrlDecode(context.Request["DataSetName"]);
                      json = getDataSet(page, pagesize, sortname, sortorder,DataSetName); //返回json格式数据
                   } 
                   context.Response.Write(json);
                   break;
               case "getDetailFieldGroup": 
                   context.Response.Write(getDetailFieldGroup());
                   break;
               case "getFieldDictSelect":
                   string FieldID = context.Request["FieldID"];  //获取ajax传的值
                    context.Response.Write(getFieldDictSelect(FieldID));
                   break;
               case "submit":
                   string jsonstr=  context.Request["jsonstr"];  //获取ajax传的值
                   submit(jsonstr, context);
                   break; 
           } 
        }
  
        /// <summary>
        /// 提交form写入数据库
        /// </summary>
        /// <param name="jsonstr">传入json数据</param>
        /// <param name="context">返回结果</param>
        private void submit(string jsonstr, HttpContext context)
        {
            List<int> FieldID = new List<int>();   //字段ID
            List<int> FormID = new List<int>();  //表单ID
            List<string> FieldDesc = new List<string>(); //字段描述
            List<int> CSSStyleClass=new List<int>();  //样式类型
            List<string> Group = new List<string>();   //对应明细组名称
            List<int> GroupLineDataSetID = new List<int>();  //对应明细行数据集ID
            Dictionary<string, int> groups = new Dictionary<string, int>();  //明细组名称和ID对应字典

            XmlDocument doc = jsonToXML(jsonstr);  //json转换成XML

            XmlNodeList item = doc.SelectNodes("/root/items/item");   //获取字段
            XmlNodeList form = doc.SelectNodes("/root/form/item");  //获取表单
            XmlNodeList options = doc.SelectNodes("/root/options/item");    //获取选项
            XmlNodeList json = doc.SelectNodes("/root/json/item/items/item");  //获取明细组
             
            if (item.Count > 0)  //如有字段先增加字段
            {
                //增加字段,输出字段ID，字段描述，样式，明细组名称，明细行数据集ID
                FieldID = addFieldDict(out FieldDesc,out CSSStyleClass, out Group,out GroupLineDataSetID,item,options);  
            }
             
            if (item.Count > 0 && options.Count > 0)
            { 
                
            }

            if (form.Count > 0)
            {
                FormID = addFormBase(form);     //增加表单 
                string s= addGroup(out groups, json, FormID[0]);  //增加明细组,输出对应字典
            }

            List<string> result = new List<string>();
            if (FieldID.Count() > 0 && FormID.Count() > 0)  //如果存在字段和表单，将字段增加到表单上
            {
                List<int> g = new List<int>();     //将明细行名称转成ID
                foreach (var i in Group)
                {
                    if (i != string.Empty)
                    {
                        g.Add(groups[i]);
                    }
                    else
                    {
                        g.Add(0);
                    }
                }

                FormField f = new FormField();
                f.FieldID = FieldID;
                f.FieldDesc = FieldDesc;
                f.CSSStyleClass = CSSStyleClass;
                f.FormID = FormID[0];
                f.GroupID = 0;
                f.IsDetail = 0;
                f.GroupLineDataSetID = GroupLineDataSetID;
                f.TargetGroupID = g;

                result= addFormField(f); //将元素添加到表单中
               
                context.Response.Write(result.Count);
            }
            else if (form.Count > 0)
            {
                context.Response.Write(FormID.Count);
            }
        } 

     
        /// <summary>
        ///  增加下拉框选项
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        private static DataTable addFieldSelect(XmlNodeList options)
        {
            DataTable dtSelectList = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FieldDictSelect", "FieldID=0", "DisplayOrder");
                         
            foreach (XmlNode fNode in options)
            {
                XmlNode SelectNo = fNode.SelectSingleNode("SelectNo");
                XmlNode FieldID = fNode.SelectSingleNode("FieldID");
                XmlNode LabelWord = fNode.SelectSingleNode("LabelWord");
                XmlNode DisplayOrder = fNode.SelectSingleNode("DisplayOrder");

                DataRow drSelect = dtSelectList.NewRow();
                drSelect["SelectNo"] = SelectNo.InnerText;
                drSelect["FieldID"] = FieldID.InnerText;
                drSelect["LabelWord"] = LabelWord.InnerText;
                drSelect["DisplayOrder"] = DisplayOrder.InnerText;
                dtSelectList.Rows.Add(drSelect);
                DataView dvSelectList = new DataView(dtSelectList);
                dvSelectList.Sort = "DisplayOrder";
                dtSelectList = dvSelectList.ToTable();
                for (int i = 0; i < dtSelectList.Rows.Count; i++)
                {
                    dtSelectList.Rows[i]["DisplayOrder"] = (i + 1) * 10;
                }
            }

            return dtSelectList;
        }

        /// <summary>
        /// 增加字段
        /// </summary> 
        /// <param name="item">xml数据</param>
        /// <returns>FieldID数组</returns>
        private static List<int> addFieldDict(out List<string> FD, out List<int> CSS, out List<string> Group, out List<int> GroupLineDataSetID, XmlNodeList item, XmlNodeList options)
        {
            FD = new List<string>();  //字段描述
            CSS = new List<int>();      //
            Group = new List<string>();  //
            GroupLineDataSetID = new List<int>();  //
            List<int> result = new List<int>();

            foreach (XmlNode fNode in item)
            {
                GPRP.Entity.Workflow_FieldDictEntity test = new GPRP.Entity.Workflow_FieldDictEntity();

                XmlNode FieldID = fNode.SelectSingleNode("FieldID");
                XmlNode FieldName = fNode.SelectSingleNode("FieldName");
                XmlNode FieldDesc = fNode.SelectSingleNode("FieldDesc");
                XmlNode DataTypeID = fNode.SelectSingleNode("DataTypeID");
                XmlNode FieldDBType = fNode.SelectSingleNode("FieldDBType");
                XmlNode SqlDbType = fNode.SelectSingleNode("SqlDbType");
                XmlNode SqlDbLength = fNode.SelectSingleNode("SqlDbLength");
                XmlNode HTMLTypeID = fNode.SelectSingleNode("HTMLTypeID");
                XmlNode FieldTypeID = fNode.SelectSingleNode("FieldTypeID");
                XmlNode TextLength = fNode.SelectSingleNode("TextLength");
                XmlNode Dateformat = fNode.SelectSingleNode("Dateformat");
                XmlNode IsHTML = fNode.SelectSingleNode("IsHTML");
                XmlNode TextHeight = fNode.SelectSingleNode("TextHeight");
                XmlNode BrowseType = fNode.SelectSingleNode("BrowseType");
                XmlNode IsDynamic = fNode.SelectSingleNode("IsDynamic");
                XmlNode DataSetID = fNode.SelectSingleNode("DataSetID");
                XmlNode BuiltInFlag = fNode.SelectSingleNode("BuiltInFlag");
                XmlNode CSSStyleClass = fNode.SelectSingleNode("CSSStyleClass");
                XmlNode group = fNode.SelectSingleNode("group");  //对应明细组
                XmlNode groupLineDataSetID = fNode.SelectSingleNode("GroupLineDataSetID");  //对应明细行数据集

                test.FieldID = (FieldID.InnerText != string.Empty ? Convert.ToInt32(FieldID.InnerText) : 0);
                test.FieldName = FieldName.InnerText;
                test.FieldDesc = FieldDesc.InnerText; 
                test.DataTypeID =( DataTypeID.InnerText!=null?Convert.ToInt32(DataTypeID.InnerText):0);
                test.FieldDBType = FieldDBType.InnerText;
                test.SqlDbType = SqlDbType.InnerText; 
                test.SqlDbLength =(SqlDbLength.InnerText!=null?Convert.ToInt32(SqlDbLength.InnerText):0);
                test.HTMLTypeID =( HTMLTypeID.InnerText!=null?Convert.ToInt32(HTMLTypeID.InnerText):0);
                test.FieldTypeID =(FieldTypeID.InnerText != null ? Convert.ToInt32(FieldTypeID.InnerText) : 0);
                test.IsDynamic = IsDynamic.InnerText;
                test.TextLength =TextLength.InnerText!=null ? Convert.ToInt32(TextLength.InnerText):0;
                DataTable dt =  addFieldSelect(options);  //下拉框选项
                test.dtSelectList = dt;
                test.Dateformat = Dateformat.InnerText;
                test.IsHTML = IsHTML.InnerText;
                test.TextHeight =TextHeight.InnerText!=null? Convert.ToInt32(TextHeight.InnerText):0;
                test.BrowseType = BrowseType.InnerText!=null? Convert.ToInt32(BrowseType.InnerText):0;
                test.IsDynamic = IsDynamic.InnerText;
                test.DataSetID =DataSetID.InnerText!=null?Convert.ToInt32(DataSetID.InnerText):0;
                test.ValidateType = "";
                test.ValueColumn = "";
                test.TextColumn = "";

                string r = "-1";
                if (test.FieldID != 0)
                {
                   r = DbHelper.GetInstance().UpdateWorkflow_FieldDict(test);  //修改字段
                }
                else
                {
                    r = DbHelper.GetInstance().AddWorkflow_FieldDict(test);  //增加字段
                }

                if (r != "-1")
                {
                    FD.Add(FieldDesc.InnerText);
                    Group.Add(group.InnerText);
                    GroupLineDataSetID.Add(Convert.ToInt32(groupLineDataSetID.InnerText));
                    if (CSSStyleClass.InnerText == "")
                        CSS.Add(-1);
                    else
                        CSS.Add(Convert.ToInt32(CSSStyleClass.InnerText));
                    result.Add(test.FieldID);
                }
              
            }
            return result;
        }

        /// <summary>
        /// 增加表单
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        private static List<int> addFormBase(XmlNodeList form)
        {
            DateTime dt = DateTime.Now;
            List<int> result = new List<int>();

            foreach (XmlNode fNode in form)
            { 
                GPRP.Entity.Workflow_FormBaseEntity wfb = new GPRP.Entity.Workflow_FormBaseEntity();

                XmlNode FormID = fNode.SelectSingleNode("FormID");  //判断是否
                XmlNode FormName = fNode.SelectSingleNode("FormName");
                XmlNode FormDesc = fNode.SelectSingleNode("FormDesc");
                XmlNode FormTypeID = fNode.SelectSingleNode("FormTypeID");
                XmlNode DisplayOrder = fNode.SelectSingleNode("DisplayOrder");
                XmlNode Useflag = fNode.SelectSingleNode("Useflag");
 
                wfb.FormID =(FormID.InnerText!=string.Empty?Convert.ToInt32(FormID.InnerText):0);
                wfb.FormName = FormName.InnerText;
                wfb.FormDesc = FormDesc.InnerText;
                wfb.FormTypeID = Convert.ToInt32(FormTypeID.InnerText);
                wfb.DisplayOrder = Convert.ToInt32(DisplayOrder.InnerText);
                wfb.Useflag = Useflag.InnerText;
                wfb.Creator = WebUtils.GetCookieUser();  //人员，实现上逻辑有问题！
                wfb.CreateDate = dt;
                wfb.lastModifier = WebUtils.GetCookieUser();
                wfb.lastModifyDate = dt;
               
                string r = "-1";
                if (wfb.FormID != 0)  //判断是否有FormId
                {
                    r = DbHelper.GetInstance().UpdateWorkflow_FormBase(wfb);  //更新
                }
                else
                {                  
                     r = DbHelper.GetInstance().AddWorkflow_FormBase(wfb);   //新增
                } 
                 
                if (r != "-1")
                {
                    result.Add(wfb.FormID); 
                }
            } 

            return result;
        }

        /// <summary>
        /// 增加明细组
        /// </summary>
        /// <param name="GroupID"></param>
        /// <param name="group"></param>
        /// <param name="formID"></param>
        /// <returns></returns>
        private static string addGroup(out Dictionary<string, int> groups, XmlNodeList group, int formID) 
        {
         //   GroupID = new List<int>();
            groups = new Dictionary<string, int>();

            string sResult=""; int i = 10;

            foreach (XmlNode fNode in group)
            {
                XmlNode FormName = fNode.SelectSingleNode("FormName");
                XmlNode GroupName = fNode.SelectSingleNode("groupName");
                XmlNode GroupDesc = fNode.SelectSingleNode("groupDesc");
                  
                Workflow_FormFieldGroupEntity _Workflow_FormFieldGroupEntity = new Workflow_FormFieldGroupEntity();
                _Workflow_FormFieldGroupEntity.FormID = formID;
                _Workflow_FormFieldGroupEntity.GroupName = GroupName.InnerText;
                _Workflow_FormFieldGroupEntity.GroupDesc = GroupDesc.InnerText;
                _Workflow_FormFieldGroupEntity.DisplayOrder = i; i += 10;

               sResult = DbHelper.GetInstance().AddWorkflow_FormFieldGroup(_Workflow_FormFieldGroupEntity);  //增加明细组
                  
               if (sResult != "-1") {   //增加明细组内元素
                   groups.Add(_Workflow_FormFieldGroupEntity.GroupName, _Workflow_FormFieldGroupEntity.GroupID);
                 //  GroupID.Add(_Workflow_FormFieldGroupEntity.GroupID);
                   XmlNodeList groupsItem = fNode.SelectNodes("groups/item");
                   List<int> FieldID = new List<int>();
                   List<string> FieldDesc = new List<string>();
                 
                   foreach (XmlNode items in groupsItem)
                   {
                       XmlNode fieldID = items.SelectSingleNode("fieldID");  //元素ID
                       XmlNode fieldDesc = items.SelectSingleNode("fieldDesc"); //元素描述
                       FieldID.Add(Convert.ToInt32(fieldID.InnerText));
                       FieldDesc.Add(fieldDesc.InnerText);
                   }
                   addFormField(FieldID, FieldDesc, null, formID, _Workflow_FormFieldGroupEntity.GroupID, 1);  //将元素添加到明细组
               }
            }
            return sResult;
        }


        private static List<string> addFormField(FormField f)
        {
            List<string> result = new List<string>();
            DateTime dt = new DateTime();
            string txtFieldID = "";

            DataTable dtFormField = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FormField", "FormID="+f.FormID, "DisplayOrder");
                          
            for (int i = 0; i < f.FieldID.Count(); i++)
            {
                txtFieldID +=f.FieldID[i];
                if (i <f.FieldID.Count - 1) txtFieldID += ",";
                GPRP.Entity.Workflow_FormFieldEntity wff = new GPRP.Entity.Workflow_FormFieldEntity();
                wff.FormID =f.FormID;
                wff.FieldID =f.FieldID[i];
                wff.GroupID = Convert.ToBoolean(f.GroupID) ?f.GroupID : 0;              //明细组ID，0不是明细组
                wff.FieldLabel = f.FieldDesc[i];  //描述
                if (f.CSSStyleClass == null)
                {
                    wff.CSSStyle = 0;
                }
                else if (f.CSSStyleClass[i] != -1)
                {
                    wff.CSSStyle =f.CSSStyleClass[i];     //样式
                }

                wff.DisplayOrder = 10 + (i * 10);   //顺序
                wff.IsDetail = Convert.ToBoolean(f.IsDetail) ? f.IsDetail : 0;    //是否是明细组,1是明细组，0不是明细组
                wff.CreateDate = dt;  //创建日期
                wff.GroupLineDataSetID = f.GroupLineDataSetID[i];   //对应数据集
                wff.TargetGroupID = f.TargetGroupID[i];    //对应明细组
                                
                int isflage = 0; 
                for(int k=0;k<dtFormField.Rows.Count;k++)
                {
                    DataRow dr = dtFormField.Rows[k];

                    if (wff.FieldID == Convert.ToInt32(dr["FieldID"]))
                    {
                        isflage = 1;
                        result.Add(DbHelper.GetInstance().UpdateWorkflow_FormField(wff));  //更新
                        dtFormField.Rows.Remove(dr);
                        break;
                    }
                }

                if (isflage == 0) 
                {
                    result.Add(DbHelper.GetInstance().AddWorkflow_FormField2(wff));   //新增        
                  
                }                            
            }

            foreach (DataRow dr in dtFormField.Rows)  //删除
            { 
                  DbHelper.GetInstance().DeleteWorkflow_FormField(dr["FormID"].ToString(), dr["FieldID"].ToString(), dr["GroupID"].ToString());
            }


            ArrayList arlst = new ArrayList();
            arlst.Add("Workflow_FormField");
            arlst.Add(f.FormID);
            arlst.Add(f.GroupID);
            string a = DbHelper.GetInstance().sp_ReDisplayOrder(arlst);
            if (txtFieldID != string.Empty)
            {
                string r = DbHelper.GetInstance().sp_AlterWork_form_TableColumn(txtFieldID,f.GroupID == 0 ? "1" : "2");
            }

            return result;
        }
         
       /// <summary>
        /// 增加表单元素
       /// </summary>
       /// <param name="FieldID">字段ID</param>
       /// <param name="FieldDesc">字段描述</param>
       /// <param name="CSSStyleClass">样式</param>
       /// <param name="FormID">表单字段</param>
       /// <param name="GroupID">明细组ID，0为主字段</param>
       /// <param name="IsDetail">是否是明细组，1为明细组，0为主字段</param>
       /// <returns></returns>
        private static List<string> addFormField(List<int> FieldID, List<string> FieldDesc, List<int> CSSStyleClass, int FormID, int GroupID, int IsDetail)
        {
            List<string> result = new List<string>();
            DateTime dt = new DateTime();
            string txtFieldID = "";
            for(int i=0;i<FieldID.Count();i++) 
            {
                txtFieldID += FieldID[i];
                if (i < FieldID.Count - 1)   txtFieldID += ",";
                GPRP.Entity.Workflow_FormFieldEntity wff = new GPRP.Entity.Workflow_FormFieldEntity();
                wff.FormID = FormID;
                wff.FieldID = FieldID[i];
                wff.GroupID = Convert.ToBoolean(GroupID) ? GroupID : 0;              //明细组ID，0不是明细组
                wff.FieldLabel = FieldDesc[i];  //描述
                if(CSSStyleClass==null)
                {
                    wff.CSSStyle = 0;
                }
                else if (CSSStyleClass[i] != -1) 
                {
                    wff.CSSStyle = CSSStyleClass[i];     //样式
                } 

                wff.DisplayOrder = 10+(i*10);   //顺序
                wff.IsDetail =Convert.ToBoolean(IsDetail)?IsDetail:0;    //是否是明细组,1是明细组，0不是明细组
                wff.CreateDate = dt;  //创建日期
                wff.GroupLineDataSetID =0;   //对应数据集
                wff.TargetGroupID = 0;    //对应明细组

                result.Add(DbHelper.GetInstance().AddWorkflow_FormField(wff));                
            }

            ArrayList arlst = new ArrayList();
            arlst.Add("Workflow_FormField");
            arlst.Add(FormID);
            arlst.Add(GroupID);
            string a= DbHelper.GetInstance().sp_ReDisplayOrder(arlst);
            if ( txtFieldID != string.Empty)
            {
              string r=  DbHelper.GetInstance().sp_AlterWork_form_TableColumn(txtFieldID, GroupID == 0 ? "1" : "2");
            }

            return result;
        }
          
        ///////////////////////////////////////////
        private static int ValidateFormFieldGroupName(string groupName)
        {
            DataTable dtSelectList = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FormFieldGroup", "GroupName='" + groupName + "'", "CreateDate");

            if (dtSelectList.Rows.Count > 0)
                return 1;
            else
                return 0;
        }

        private static int ValidateFiledName(string fieldName)
        {
            DataTable dtSelectList = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FieldDict", "FieldName='" + fieldName + "'", "FieldID");

            if (dtSelectList.Rows.Count > 0)
                return 1;
            else
                return 0;
        }

        private static int ValidateFormName(string formName)
        {
            DataTable dtSelectList = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FormBase", "FormName='" + formName + "'", "FormID");

            if (dtSelectList.Rows.Count > 0)
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// 将json字符串转换成xml
        /// </summary>
        /// <param name="jsonstr"></param>
        /// <returns></returns>
        private static XmlDocument jsonToXML(string jsonstr)
        {
            XmlDictionaryReader reader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(jsonstr), XmlDictionaryReaderQuotas.Max);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            return doc;
        }
         
        private static string deleteEnter(string input)
        {
            var o= input.Where(x => x != '\r'&&x!='\n').ToArray();
            string output = new string(o);
            return output;
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

      //  Workflow_FormBaseEntity
        public string FormBaseToJson(int formID)
        {
            string item = FieldToHtml(formID);
           
            DataTable dtFormBase = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FormBase", "FormID='" + formID + "'", "FormID");
            foreach (DataRow dr in dtFormBase.Rows)
            {
                Workflow_FormBaseEntity wfb = new Workflow_FormBaseEntity();
                wfb.FormID = Convert.ToInt32(dr["FormID"]);
                wfb.FormName = dr["FormName"].ToString();
                wfb.FormDesc = dr["FormDesc"].ToString();
                wfb.FormTypeID =Convert.ToInt32(dr["FormTypeID"]);
                wfb.DisplayOrder = Convert.ToInt32(dr["DisplayOrder"]);
                wfb.Useflag = dr["Useflag"].ToString();

                bool useflag = false ;
                if (wfb.Useflag == "1")
                {
                    useflag = true;
                } 
               
                string html=  "<form formid=\""+wfb.FormID
                    +"\" displayorder=\""+wfb.DisplayOrder
                    +"\" formdesc=\""+wfb.FormDesc
                    +"\" formtypeid=\""+wfb.FormTypeID
                    +"\" name=\""+wfb.FormName
                    +"\" useflag=\""+useflag+"\">"+item+"</form>";


                return html;
            } 

            return "";                   
        }

        /// <summary>
        /// 生成表单内元素
        /// </summary>
        /// <param name="formID"></param>
        /// <returns></returns>
        private string FieldToHtml(int formID)
        { 
            DataTable dt = DbHelper.GetInstance().GetDBRecords("b.CSSStyle,b.FieldLabel,a.*", "workflow_fielddict a,workflow_formfield b", "a.fieldID=b.fieldID and b.formid="+formID, "b.DisplayOrder");
            string value = "";

            foreach (DataRow dr in dt.Rows)
            {
                int FiledID = Convert.ToInt32(dr["FieldID"]);
                string FieldName = dr["FieldName"].ToString();
                string FieldDesc = dr["FieldLabel"].ToString();
                int HTMLTypeID =Convert.ToInt32(dr["HTMLTypeID"]);   //控件类型
                string SqlDbType = dr["SqlDbType"].ToString();
                int SqlDbLength =Convert.ToInt32(dr["SqlDbLength"]);
                int TextLength =Convert.ToInt32(dr["TextLength"]);
                int FieldTypeID =Convert.ToInt32(dr["FieldTypeID"]);
                int DataTypeID =Convert.ToInt32(dr["DataTypeID"]);
                string Dateformat =dr["Dateformat"].ToString();
                int TextHeight =Convert.ToInt32(dr["TextHeight"]);
                int IsHTML =Convert.ToInt32(dr["IsHTML"]);
                int BrowseType =Convert.ToInt32(dr["BrowseType"]);
                int IsDynamic =Convert.ToInt32(dr["IsDynamic"]);
                int DataSetID =Convert.ToInt32(dr["DataSetID"]);
                string ValueColumn = dr["ValueColumn"].ToString();
                string TextColumn =dr["TextColumn"].ToString();
                int FieldID =Convert.ToInt32(dr["FieldID"]);

                string CSSStyle = dr["CSSStyle"].ToString();
               
                string command = "datatypeid='" + DataTypeID + "'  fielddesc='" + FieldDesc + "' fieldtypeid='" + FieldTypeID + "' " +
                     "htmltypeid='" + HTMLTypeID + "' name='" + FieldName + "' sqldblength='" + SqlDbLength + "'" +
                     " sqldbtype='" + SqlDbType + "' textlength='" + TextLength + "'";

                switch (HTMLTypeID)
                {
                    case 1:
                        value += "<label CSSStyleClass='"+CSSStyle+"' FieldID='" + FieldID + "' datatypeid='" + DataTypeID + "'  fielddesc='" + FieldDesc + "' fieldtypeid='" + FieldTypeID + "' " +
                     "htmltypeid='" + HTMLTypeID + "' name='" + FieldName + "' sqldblength='" + SqlDbLength + "'" +
                     " sqldbtype='" + SqlDbType + "' textlength='" + TextLength + "' type='label'>" + FieldName + "</label>";
                        value = cssStyle(value, CSSStyle);     
                        break;   //标签
                    case 2:
                        value += FieldDesc+"<input CSSStyleClass='" + CSSStyle + "' FieldID='" + FieldID + "'  datatypeid='" + DataTypeID + "' fielddesc='" + FieldDesc + "' fieldname='" + FieldName + "'  dateformat='" + Dateformat + "' " +
               " fieldtypeid='" + FieldTypeID + "' htmltypeid='" + HTMLTypeID + "' sqldblength='" + SqlDbLength + "' sqldbtype='" + SqlDbType + "' textlength='" + TextLength + "' type='text' />";
                        value = cssStyle(value, CSSStyle);               
                      break;
                    case 3:
                        value += FieldDesc + "<textarea CSSStyleClass='" + CSSStyle + "' FieldID='" + FieldID + "'  datatypeid='" + DataTypeID + "' fielddesc='" + FieldDesc + "' fieldname='" + FieldName + "' fieldtypeid='" + FieldTypeID + "' " +
                        "htmltypeid='" + HTMLTypeID + "' ishtml='" + IsHTML + "' sqldblength='" + SqlDbLength + "' sqldbtype='" + SqlDbType + "' textheight='" + TextHeight + "' textlength='0'></textarea>";
                        value = cssStyle(value, CSSStyle);     
                        break; //多行文本
                    case 4:
                    case 5:
                        if (IsDynamic == 1)
                        {
                            value += FieldDesc + "<select  CSSStyleClass='" + CSSStyle + "' FieldID='" + FieldID + "'  datasetid='" + DataSetID + "' datatypeid='" + DataTypeID + "' size='" + TextHeight + "' fielddesc='" + FieldDesc + "' fieldname='" + FieldName + "' " +
                    "fieldtypeid='" + FieldTypeID + "' htmltypeid='" + HTMLTypeID + "' isdynamic='" + IsDynamic + "'  sqldblength='" + SqlDbLength + "' " +
                    "sqldbtype='" + SqlDbType + "' textcolumn='" + TextColumn + "' textlength='" + TextLength + "' type='select' valuecolumn='" + ValueColumn + "'></select>";
                        }
                        else
                        { 
                            string option = "";
                            DataTable dtHTMLType = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FieldDictSelect", "FieldID=" + FieldID, "DisplayOrder");

                            foreach (DataRow optionDr in dtHTMLType.Rows)
                            {
                                option += "<option DisplayOrder='" + optionDr["DisplayOrder"] + "' FieldID='" + optionDr["FieldID"] + "' SelectNo='" + optionDr["SelectNo"] + "' value='" + optionDr["LabelWord"] + "'>" + optionDr["LabelWord"] + "</option>";
                            }

                            value += FieldDesc + "<select  CSSStyleClass='" + CSSStyle + "' FieldID='" + FieldID + "'  datasetid='" + DataSetID + "' datatypeid='" + DataTypeID + "' size='" + TextHeight + "' fielddesc='" + FieldDesc + "' fieldname='" + FieldName + "' " +
          "fieldtypeid='" + FieldTypeID + "' htmltypeid='" + HTMLTypeID + "' isdynamic='" + IsDynamic + "'  sqldblength='" + SqlDbLength + "' " +
          "sqldbtype='" + SqlDbType + "' textcolumn='" + TextColumn + "' textlength='" + TextLength + "' type='select' valuecolumn='" + ValueColumn + "'>" + option + "</select>";
                     }
                        value = cssStyle(value, CSSStyle);       
                     break;
                    case 6:
                        value += FieldDesc + "<input  CSSStyleClass='" + CSSStyle + "' FieldID='" + FieldID + "'  datatypeid='" + DataTypeID + "' fielddesc='" + FieldDesc + "' fieldname='" + FieldName + "' fieldtypeid='" + FieldTypeID + "' " +
                        "htmltypeid='" + HTMLTypeID + "' sqldblength='" + SqlDbLength + "' sqldbtype='" + SqlDbType + "' textlength='" + TextLength + "' type='checkbox' />";
                        value = cssStyle(value, CSSStyle);     
                        break;   //checkbox
                    case 7:
                        value += FieldDesc + "<input  CSSStyleClass='" + CSSStyle + "' FieldID='" + FieldID + "'  datatypeid='" + DataTypeID + "' fielddesc='" + FieldDesc + "' fieldname='" + FieldName + "' fieldtypeid='" + FieldTypeID + "' " +
                        "htmltypeid='" + HTMLTypeID + "' sqldblength='" + SqlDbLength + "' sqldbtype='" + SqlDbType + "' textlength='" + TextLength + "' type='file' />";
                        value = cssStyle(value, CSSStyle);     
                        break;  //附件上传
                    case 8:
                        value += FieldDesc + "<input  CSSStyleClass='" + CSSStyle + "' FieldID='" + FieldID + "'  browsetype='" + BrowseType + "' datatypeid='" + DataTypeID + "'  fielddesc='" + FieldDesc + "' fieldname='" + FieldName + "' fieldtypeid='" + FieldTypeID + "' " +
                        "htmltypeid='" + HTMLTypeID + "' name='" + FieldName + "' sqldblength='" + SqlDbLength + "' sqldbtype='" + SqlDbType + "' textlength='" + TextLength + "'  type='update' />";
                        value = cssStyle(value, CSSStyle);     
                        break;   //浏览按钮
                    case 9:
                        value += FieldDesc + "<input  CSSStyleClass='" + CSSStyle + "' FieldID='" + FieldID + "'  datatypeid='" + DataTypeID + "' fielddesc='" + FieldDesc + "' fieldname='" + FieldName + "' " +
                        " fieldtypeid='" + FieldTypeID + "' htmltypeid='" + HTMLTypeID + "' sqldblength='" + SqlDbLength + "' sqldbtype='" + SqlDbType + "' textlength='" + TextLength + "' type='groupLine' />";
                        value = cssStyle(value, CSSStyle);     
                        break;  //明细行
                } 
            }

            return value;
        }
      
        private static  int br = 0;
        //换行控制
        private static string cssStyle(string value, string CSSStyle)
        {
            if (CSSStyle == "0" || CSSStyle == "1" || CSSStyle == "2" || CSSStyle == "3")
            {
                br += 1;
            }
            else if(CSSStyle=="4")
            {
                br += 2;
            }
            else if (CSSStyle == "5")
            {
                br = 0;
                value += "<br>";
            }
            else
            {
                value +="&nbsp;&nbsp;&nbsp;";
            }

            if (br >= 3)
            {
                value += "<br>";
                br = 0;
            }
            else
            {
                value += "&nbsp;&nbsp;&nbsp;";
            }


            return value;
        }
         
        class FormField
        {
            public List<int> FieldID { get; set; }
            public List<string> FieldDesc { get; set; }
            public List<int> CSSStyleClass { get; set; }
            public int FormID { get; set; }
            public int GroupID { get; set; }
            public int IsDetail { get; set; }
            public List<int> GroupLineDataSetID { get; set; }
            public List<int> TargetGroupID { get; set; }
        }

        class DataSetNode 
        {
            public int DataSetID {get;set;}
            public string DataSetName{get;set;}
            public string ReturnColumns{get;set;}
            public string QuerySql { get; set; }
        }

        class Node
        {
            public int FieldID { get; set; }
            public string FieldName { get; set; }
            public string FieldDesc { get; set; }
            public int HTMLTypeID { get; set; }
            public string HTMLTypeN { get; set; }

            public string SqlDbType{get;set;}
            public int SqlDbLength{get;set;}
            public int TextLength{get;set;}
            public int FieldTypeID{get;set;}  

            public int DataTypeID{get;set;}  
            public string Dateformat{get;set;}
            public int TextHeight{get;set;}
            public string IsHTML{get;set;}
            public int BrowseType{get;set;}
            public int IsDynamic{get;set;}
            public int DataSetID{get;set;}
            public string ValueColumn{get;set;}
            public string TextColumn{get;set;} 
        }

        class HTMLTypeNode
        {
           public  int HTMLTypeID{get;set;}
           public string HTMLTypeDesc { get; set; }

           public HTMLTypeNode()
           { }

           public HTMLTypeNode(int id,string desc)
           {
               HTMLTypeID = id;
               HTMLTypeDesc = desc;
           }
        }

        class CssStyleNode
        {
            public int CSSStyleID { get; set; }
            public string CSSStyleClass { get; set; }
        }

        class FieldDictSelect
        {
            public int selectNo { get; set; }
            public string LabelWord { get; set; }
        }
 
        class dbRecordsArg
        {
            public string columnList { get; set; }
            public string tableList { get; set; }
            public string WhereCondition { get; set; }
            public string orderby { get; set; }
            public int PageSize{get;set;}
            public int PageIndex{get;set;}

            public dbRecordsArg()
            { }

            public dbRecordsArg(string c, string t, string w, string o)
            {
                columnList = c;
                tableList = t;
                WhereCondition = w;
                orderby = o;
            }

            public dbRecordsArg(string c, string t, string w, string o,int ps,int pi)
            {
                columnList = c;
                tableList = t;
                WhereCondition = w;
                orderby = o;
                PageSize = ps;
                PageIndex = pi;
            } 
        }
         
    }
}
