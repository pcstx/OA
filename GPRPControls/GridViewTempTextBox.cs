using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GPRP.GPRPComponents;
namespace GPRP.GPRPControls
{
    public class GridViewTempTextBox:ITemplate
    {
        private DataControlRowType templateType;
        private bool m_bReadOnly;
        private string m_strTxtID;
        private string m_strColumnText;
        private string m_strField;
        private int m_Width=100;
        private int m_FieldID = 0;
        private string m_FieldName;
        private string m_FieldDBType;
        private int m_FieldHTMLType = 0;
        private int m_FieldValidType = 0;
        private string m_ValidTimeTypeName;
        private int m_hasRule;
        private DataTable m_dtRule;
        private DataTable m_dtColumnsIndex;
        private bool m_Enabled;
        private int m_IsMandtory;
        private int m_FieldDataType;
        private string m_FieldDateFormat;
        /// <summary>
        /// 此无参构造必须实现，否则出错
        /// </summary>
        public GridViewTempTextBox() : base()
        {
        }

        /// <summary>
        /// 构造表头列对象
        /// </summary>
        /// <param name="strColumnText">表头字符串</param>
        public GridViewTempTextBox(string strColumnText)
        {
            templateType = DataControlRowType.Header;
            m_strColumnText = strColumnText;
        }

        /// <summary>
        /// 构造元素行对象
        /// </summary>
        /// <param name="strTxtID">当前TextBox控件ID</param>
        /// <param name="strField">当前TextBox控件绑定的字段</param>
        /// <param name="bReadOnly">TextBox是否只读</param>
        /// <param name="txtWidth">TextBox的宽度</param>
        public GridViewTempTextBox(string strTxtID, string strHeader,string strField, bool bReadOnly,int txtWidth)
        {
            templateType = DataControlRowType.DataRow;
            m_strTxtID = strTxtID;
            m_strField = strField;
            m_bReadOnly = bReadOnly;
            m_Width = txtWidth;
            m_strColumnText = strHeader;
        }

        /// <summary>
        /// 构造元素行对象
        /// </summary>
        /// <param name="strTxtID">当前TextBox控件ID</param>
        /// <param name="strField">当前TextBox控件绑定的字段</param>
        /// <param name="bReadOnly">TextBox是否只读</param>
        /// <param name="txtWidth">TextBox的宽度</param>
        public GridViewTempTextBox(string strTxtID, string strHeader, string strField, bool bReadOnly, int txtWidth, int FieldID, string FieldName, string FieldDBType, int FieldHTMLType, int FieldValidType, string ValidTimeTypeName, int HasRule, DataTable dtRule, DataTable dtColumnsIndex, int IsMandtory, int FieldDataType, string FieldDateFormat)
        {
            templateType = DataControlRowType.DataRow;
            m_strTxtID = strTxtID;
            m_strField = strField;
            m_bReadOnly = bReadOnly;
            //m_Enabled = Enabled;
            m_Width = txtWidth;
            m_strColumnText = strHeader;
            m_FieldID = FieldID;
            m_FieldName = FieldName;
            m_FieldDBType = FieldDBType;
            m_FieldHTMLType = FieldHTMLType;
            m_FieldValidType = FieldValidType;
            m_ValidTimeTypeName = ValidTimeTypeName;
            m_hasRule = HasRule;
            m_dtRule = dtRule;
            m_dtColumnsIndex = dtColumnsIndex;
            m_IsMandtory = IsMandtory;
            m_FieldDataType = FieldDataType;
            m_FieldDateFormat = FieldDateFormat;
        }


        public void InstantiateIn(System.Web.UI.Control container)
        {
            switch (templateType)
            {
                case DataControlRowType.Header:
                    Literal l = new Literal();
                    l.Text = m_strColumnText;
                    container.Controls.Add(l);
                    break;

                case DataControlRowType.DataRow:
                    TextBox tb = new TextBox();
                    tb.ID = m_strTxtID;
                    tb.Width = Unit.Percentage(m_Width);
                    tb.BorderStyle = BorderStyle.None;
                    tb.DataBinding += new EventHandler(TextBox_DataBinding);

                    

                    string[] strScript = new string[2];
                    strScript[0] = "";  //用来保存字段验证(如果字段验证不是用onblur或没有行列的规则计算时保存字段验证的函数)
                    strScript[1] = "";  //用来保存行列的规则计算函数
                    if (m_dtRule.Rows.Count > 0)
                    {
                        strScript[1] = "if (FormDetailFieldValidate(this,this.value,'" + m_FieldValidType.ToString() + "')) {";
                        if (m_FieldValidType > 0)
                        {    
                             if (m_ValidTimeTypeName.ToLower() != "onchange")
                                 strScript[0] = "FormDetailFieldValidate(this,this.value,'" + m_FieldValidType.ToString() + "')";
                        }


                        for (int j = 0; j < m_dtRule.Rows.Count; j++)
                        {
                            string ruleFieldIndex = "";
                            string ruleTargetFieldIndex = "";
                            string ruleFieldName = "";
                            string ruleFieldDbType = "";
                            string ruleDetail = m_dtRule.Rows[j]["RuleDetail"].ToString();
                            int RuleType = Convert.ToInt32(m_dtRule.Rows[j]["RuleType"].ToString());
                            int TargetFieldID = Convert.ToInt32(m_dtRule.Rows[j]["FieldIDTo"].ToString());
                            int TargetFieldHTMLTypeID = Convert.ToInt32(m_dtRule.Rows[j]["HTMLTypeID"].ToString());
                            string TargetFieldDbType = m_dtRule.Rows[j]["FieldDBType"].ToString();
                            string RuleFunctionName = m_dtRule.Rows[j]["RuleFunctionName"].ToString();
                            string RuleField = m_dtRule.Rows[j]["RuleField"].ToString();

                            if (RuleType == 1) //行规则
                            {
                                //在字段类列表中找出参与计算的字段来
                                for (int jj = 0; jj < m_dtColumnsIndex.Rows.Count; jj++)
                                {
                                    string RuleFieldName = m_dtColumnsIndex.Rows[jj]["FieldName"].ToString();
                                    if (ruleDetail.Contains(RuleFieldName))
                                    {
                                        ruleFieldIndex = ruleFieldIndex + m_dtColumnsIndex.Rows[jj]["ColumnIndex"].ToString() + "|";
                                        ruleFieldName = ruleFieldName + RuleFieldName + "|";
                                        ruleFieldDbType = ruleFieldDbType + m_dtColumnsIndex.Rows[jj]["FieldDBType"].ToString() + "|";
                                    }

                                }

                                DataRow[] rowsTargetField = m_dtColumnsIndex.Select("FieldID='" + TargetFieldID.ToString() + "'");
                                ruleTargetFieldIndex = rowsTargetField[0]["ColumnIndex"].ToString();
                                string ruleTargetFieldTemplateType = rowsTargetField[0]["TemplateType"].ToString();
                                strScript[1] = strScript[1] + "CaculateRowRule(this,this.value,'" + ruleFieldIndex + "','" + ruleTargetFieldIndex + "','" + Utils.UrlEncode(ruleDetail) + "','" + ruleFieldName + "','" + ruleFieldDbType + "','" + ruleTargetFieldTemplateType + "','" + TargetFieldHTMLTypeID + "','"+TargetFieldID.ToString()+"');";



                            }
                            else
                            {
                                //先取原来的数据不采用此方法
                                //DataTable dt = (DataTable)ViewState["dtValue"];
                                //int totalRowsCount = dt.Rows.Count;
                                //int rowIndex = e.Row.DataItemIndex;
                                //string lastValue = dt.Rows[rowIndex][FieldName].ToString();
                                //strScript[1] = strScript[1] + "CaculateColumnRule(this,this.value,'" + lastValue + "','" + FieldDBType + "','" + TargetFieldID + "','" + TargetFieldHTMLTypeID + "','" + TargetFieldDbType + "','" + ruleDetail + "','" + totalRowsCount.ToString() + "','" + rowIndex.ToString() + "','" + RuleFunctionName + "','" + FieldID.ToString()+ "','"+GroupID.ToString()+"');";

                                DataRow[] rows = m_dtColumnsIndex.Select("FieldName='" + RuleField + "'");
                                string RuleFieldColumnIndex = rows[0]["ColumnIndex"].ToString();
                                string RuleFieldDBType = rows[0]["FieldDBType"].ToString();
                                string RuleFieldHTMLType = rows[0]["FieldHTMLType"].ToString();
                                strScript[1] = strScript[1] + "CaculateColumnRule(this,'" + RuleField + "','" + RuleFieldColumnIndex + "','" + RuleFieldDBType + "','" + RuleFieldHTMLType + "','" + TargetFieldID + "','" + TargetFieldHTMLTypeID + "','" + TargetFieldDbType + "','" + Utils.UrlEncode(ruleDetail) + "');";

                            }




                        }

                        

                    }
                    else
                    {
                        if (m_FieldValidType > 0)
                            strScript[0] = "javascript:FormDetailFieldValidate(this,this.value,'" + m_FieldValidType.ToString() + "')";

                    }


                    if (strScript[0] != "")
                    {

                        tb.Attributes.Add(m_ValidTimeTypeName, strScript[0]);
                    }

                    if (strScript[1] != "")
                    {
                        
                        tb.Attributes.Add("onchange", "javascript:" + strScript[1] + "}");
                            
                    }



                    //}
                    //tb.TextChanged+=new EventHandler(TextBox_TextChange);
                    //tb.Enabled = m_Enabled;
                    tb.ReadOnly = m_bReadOnly;
                    //if (m_FieldValidType > 0)
                    //{
                        
                    //        tb.Attributes.Add(m_ValidTimeTypeName, "javascript:FormDetailFieldValidate(this,this.value,'" + m_FieldValidType.ToString() + "')");
                           

                    //}
                    container.Controls.Add(tb);

                    if (m_FieldDataType == 4 || m_FieldDataType == 5) //是日期或时间
                    {

                        //Literal literialImage = new Literal();
                        //literialImage.Text = "<img onclick=\"WdatePicker({el:$dp.$('" + tb.ClientID  + "'),dateFmt:'" + m_FieldDateFormat  + "'})\" src=\"../images/calendar.gif\">";
                        //literialImage.ID = "fieldImage" + m_FieldID ;
                        //ImageButton imageButton = new ImageButton();
                        //imageButton.ID = "fieldImage" + FieldID;
                        //imageButton.ImageAlign = ImageAlign.Middle;
                        //imageButton.ToolTip = "搜索";
                        //imageButton.ImageUrl = "../images/calendar.gif";
                        ////imageButton.OnClientClick = "return btnBrowseFieldClick('" + txtBrowseField.ID + "','" + txtBrowseFieldValue.ID + "','" + BrowsePage + "','" + BrowseTypeName + "');";
                        ////imageButton.Attributes.Add("onclik", "javascript:WdatePicker({dateFmt:'" + FieldDateFormat + "'})");
                        //imageButton.OnClientClick = "return WdatePicker({el:$dp.$('" + txtField .ID+ "'),dateFmt:'" + FieldDateFormat + "'})";
                        //container.Controls.Add(literialImage);
                        tb.AddAttributes("onfocus", "javascript:WdatePicker({dateFmt:'" + m_FieldDateFormat + "'})");
                    
                    
                    }

                    if (m_IsMandtory == 1)
                    {
                        tb.CanBeNull = "必填";
                        //Label literial = new Label();
                        //literial.Text = "*";
                        //literial.ForeColor = System.Drawing.Color.Red;
                        //container.Controls.Add(literial);
                    }
                    break;

                default:
                    break;
            }

        }

        private void TextBox_DataBinding(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender ;
            GridViewRow gvr = tb.NamingContainer as GridViewRow;
            tb.Text = DataBinder.Eval(gvr.DataItem, m_strField).ToString();
        }

        private void TextBox_TextChange(object sender, EventArgs e)
        {

            TextBox tb = (TextBox)sender;
            GridViewRow gvr = tb.NamingContainer as GridViewRow;
            
            //if (m_hasRule == 1)
            //{
            //    DataTable dtRuleField = new DataTable();
                

            //    for (int i = 0; i < m_dtRule.Rows.Count; i++)
            //    {

            //        string ruleDetail = m_dtRule.Rows[i]["RuleDetail"].ToString();
            //        int RuleType = Convert.ToInt32(m_dtRule.Rows[i]["RuleDetail"].ToString());
            //        int TargetFieldID = Convert.ToInt32(m_dtRule.Rows[i]["FieldIDTo"].ToString());

            //        if (RuleType == 1) //行规则
            //        {
            //            //在字段类列表中找出参与计算的字段来
            //            for (int j = 0; j < m_dtColumnsIndex.Rows.Count; j++)
            //            { 
            //                string FieldName=m_dtColumnsIndex.Rows[j]["FieldName"].ToString();
            //                if (ruleDetail.Contains(FieldName))
            //                { 
                                
                            
            //                }
                        
            //            }



            //        }
            //        else
            //        { 
                       
                    
            //        }
                
            //    }
            
            
            
            //}
        }

    }
}
