using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using GPRP.GPRPEnumerations;

namespace GPRP.GPRPControls
{
    public class GridViewTemplate: TemplateField
    {
        /// <summary>
        /// ���޲ι������ʵ�֣��������
        /// </summary>
        public GridViewTemplate():base()
        {
        }
        private int mWidth = 100;
        private string mColumnHearText = "";
        private string mColumnValue;
        private string mColumnID;
        private bool mReadOnly = false ;
        private bool mShowHeader = true;
        private ControlType mControlType = ControlType.Label;
        private DataTable dt = null;
        private bool mAutoPostBack = false;

        //��������Ҫ���ӵ����� 
        private int mFieldID;
        private string mFieldName;
        private int mFieldHTMLType;
        private string mFieldDBType;
        private int mFieldValidType;
        private string mValidTimeTypeName;
        private int mHasRule;
        private DataTable mdtRule = null;
        private DataTable mdtColumnsIndex = null;
        private bool mEnabled = true;
        private string mBrowsePage;
        private string mBrowseType;
        private int mcellIndex;
        private int mIsMandtory;
        private int mFieldDataType;//�ֶ���������
        private string mFieldDateFormat;//�����ʱ��/���ڵ��ֶζ�Ӧ�ĸ�ʽ

        public delegate void ValueChange(Object sender,string SelectValue);
        public event ValueChange valueChange;

        /// <summary>
        /// ��ǰ���޶����
        /// </summary>
        public int Width
        {
            get { return mWidth; }
            set { mWidth = value; }
        }
        /// <summary>
        /// �б�ͷ����
        /// </summary>
        public string ColumnHearText
        {
            get { return  mColumnHearText; }
            set { mColumnHearText = value; }
        }
        /// <summary>
        /// ��Ӧ����ֵ���ֶ�
        /// </summary>
        public string ColumnValue
        {
            get { return mColumnValue; }
            set { mColumnValue = value; }
        }
        /// <summary>
        /// ����ı༭��ID��ע�����ظ�
        /// </summary>
        public string ColumnID
        {
            get { return mColumnID; }
            set { mColumnID = value; }
        }
        /// <summary>
        ///�༭���Ƿ�ֻ�� 
        /// </summary>
        public bool ReadOnly
        {
            get { return mReadOnly; }
            set { mReadOnly = value; }
        }
        /// <summary>
        /// ��ͷ�Ƿ���ʾ
        /// </summary>
        public bool ShowHeaderBool
        {
            get { return mShowHeader; }
            set { mShowHeader = value; }
        }
        /// <summary>
        /// ���е�����,textbox.label,checkbox,....
        /// </summary>
        public ControlType WebControlType
        {
            get { return mControlType; }
            set { mControlType = value; }
        }
        /// <summary>
        /// �Ƿ�ط�����
        /// </summary>
        public bool boolAutoPostBack
        {
            get { return mAutoPostBack; }
            set { mAutoPostBack = value; }
        }
        public DataTable dataTable
        {
            get { return dt; }
            set { dt = value; }
        }


        public int FieldID
        {
            get { return mFieldID ; }
            set { mFieldID = value; }
        }

        public string FieldName
        {
            get { return mFieldName; }
            set { mFieldName = value; }
        }

        public int  FieldHTMLType
        {
            get { return mFieldHTMLType ; }
            set { mFieldHTMLType = value; }
        }

        public string FieldDBType
        {
            get { return mFieldDBType; }
            set { mFieldDBType = value; }
        
        }

        public int FieldValidType
        {

            get { return mFieldValidType ; }
            set { mFieldValidType = value; }
        }

        public string ValidTimeTypeName
        {
            get { return mValidTimeTypeName; }
            set { mValidTimeTypeName = value; }
        
        }
        public int HasRule
        {

            get { return mHasRule; }
            set { mHasRule = value; }
        }
        public DataTable dtRule
        {
            get { return mdtRule; }
            set { mdtRule = value; }
        }

        public DataTable dtColumnsIndex
        {
            get { return mdtColumnsIndex ; }
            set { mdtColumnsIndex = value; }
        }
        public bool Enabled
        {
            get { return mEnabled; }
            set { mEnabled = value; }
        }

        public string BrowsePage
        {
            get { return mBrowsePage; }
            set { mBrowsePage = value; }
        }

        public string BrowseType
        {
            get { return mBrowseType; }
            set { mBrowseType = value; }
        
        }

        public int cellIndex
        {
            get { return mcellIndex; }
            set { mcellIndex = value; }
        
        }

        public int IsMandtory
        {
            get { return mIsMandtory; }
            set { mIsMandtory = value; }


        }

        public int FieldDataType
        {
            get { return mFieldDataType; }
            set { mFieldDataType = value; }
        
        }

        public string FieldDateFormat
        {
            get { return mFieldDateFormat; }
            set { mFieldDateFormat = value; }
        
        }


        public DataTable dtData
        {
            get { return dt; }
            set { dt = value; }
           
        }

        //public void ShowTemplate()
        //{
        //    this.ControlStyle.Width = Unit.Pixel(mWidth);
        //    this.ItemStyle.Width = Unit.Pixel(mWidth);
        //    this.ShowHeader = mShowHeader;
        //    switch (mControlType)
        //    {
        //        case ControlType.TextBox:
        //            this.HeaderTemplate = new GridViewTempTextBox(mColumnHearText);
        //            this.ItemTemplate = new GridViewTempTextBox(mColumnID, mColumnHearText, mColumnValue, mReadOnly, mWidth);
        //            break;
        //        case ControlType.DropDownList:
        //            this.HeaderTemplate = new GridViewTempDropDownList(mColumnHearText);
        //            GridViewTempDropDownList a = new GridViewTempDropDownList(mColumnID, mColumnHearText, mColumnValue, mReadOnly, mWidth, dt, mAutoPostBack);
        //            a.selectDropDownList += new GridViewTempDropDownList.SelectedDropDownList(a_selectDropDownList);
        //            this.ItemTemplate = a;

        //            break;
        //        case ControlType.CheckBox:
        //            this.HeaderTemplate = new GridViewTempCheckBox(mColumnHearText);
        //            this.ItemTemplate = new GridViewTempCheckBox(mColumnID, mColumnHearText, mColumnValue, mReadOnly, mWidth);
        //            break;
        //        default:
        //            this.HeaderTemplate = new GridViewTempLabel(mColumnHearText);
        //            this.ItemTemplate = new GridViewTempLabel(mColumnID, mColumnHearText, mColumnValue, mReadOnly, mWidth);
        //            break;

        //    }

        //}

        public void ShowTemplate()
        {
            this.ControlStyle.Width = Unit.Pixel(mWidth);
            this.ItemStyle.Width = Unit.Pixel(mWidth);
            this.ShowHeader = mShowHeader;
            switch (mControlType)
            {
                case ControlType.TextBox:
                    this.HeaderTemplate = new GridViewTempTextBox(mColumnHearText);
                    this.ItemTemplate = new GridViewTempTextBox(mColumnID, mColumnHearText, mColumnValue, mReadOnly, mWidth, mFieldID, mFieldName, mFieldDBType, mFieldHTMLType, mFieldValidType, mValidTimeTypeName, mHasRule, mdtRule, mdtColumnsIndex, mIsMandtory, mFieldDataType, mFieldDateFormat);
                    break;
                case ControlType.DropDownList:
                    this.HeaderTemplate = new GridViewTempDropDownList(mColumnHearText);
                    GridViewTempDropDownList a = new GridViewTempDropDownList(mColumnID, mColumnHearText, mColumnValue, mReadOnly, mWidth, dt, mAutoPostBack, mFieldID, mFieldHTMLType, mFieldValidType, mValidTimeTypeName);
                    a.selectDropDownList += new GridViewTempDropDownList.SelectedDropDownList(a_selectDropDownList);
                    this.ItemTemplate = a;
                    break;
                case ControlType.CheckBox:
                    this.HeaderTemplate = new GridViewTempCheckBox(mColumnHearText);
                    this.ItemTemplate = new GridViewTempCheckBox(mColumnID, mColumnHearText, mColumnValue, mReadOnly, mWidth);
                    break;
                case ControlType.TextBoxIncludeHidden:
                    this.HeaderTemplate = new GridViewTempTextBoxIncludeHiddenValue(mColumnHearText);
                    this.ItemTemplate = new GridViewTempTextBoxIncludeHiddenValue(mColumnID, mColumnHearText, mColumnValue, mReadOnly, mWidth, mFieldID, mFieldName, mFieldDBType, mFieldHTMLType, mFieldValidType, mValidTimeTypeName, mHasRule, mdtRule, mdtColumnsIndex);
                    break;
                case ControlType.TextBrowse:
                    this.HeaderTemplate = new GridViewTempBrowse(mColumnHearText);
                    this.ItemTemplate = new GridViewTempBrowse(mColumnID, mColumnHearText, mColumnValue, mReadOnly, mWidth, mFieldID, mFieldName, mFieldDBType, mFieldHTMLType, mFieldValidType, mValidTimeTypeName, mHasRule, mdtRule, mdtColumnsIndex, mBrowsePage, mBrowseType, mcellIndex);
                    break;
                default:
                    this.HeaderTemplate = new GridViewTempLabel(mColumnHearText);
                    this.ItemTemplate = new GridViewTempLabel(mColumnID, mColumnHearText, mColumnValue, mReadOnly, mWidth);
                    break;

            }

        }


        void a_selectDropDownList(Object sender,string SelectValue)
        {
            if (valueChange != null)
            {

                valueChange(sender,SelectValue);
            }
        }

    
    }
}
