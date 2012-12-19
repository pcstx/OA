using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;

using GPRP.GPRPComponents;
using MyADO;

namespace GPRP.GPRPControls
{
	/// <summary>
	/// 下拉列表控件。
	/// </summary>
	[DefaultProperty("Text"), ToolboxData("<{0}:DropDownList runat=server></{0}:DropDownList>")]
    public class DropDownList : System.Web.UI.WebControls.DropDownList, IWebControl, IPostBackDataHandler
	{
		/// <summary>
		/// 构造函数		/// </summary>
		public DropDownList(): base()
		{
    	}

        /// <summary>
        /// 添加表数据        /// </summary>
        /// <param name="dt">要绑定的表</param>
        public void AddTableData(DataTable dt, string selectid)
        {
            AddTableData(dt);
            this.SelectedValue=selectid;
        }

        public void AddTableData(DataTable dt)
        {
            this.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                
            }
   
            this.DataBind();
        }
        public void AddTableData(DataTable dt, bool IsAddNullRow, string NullOrSelect)
        {
            this.Items.Clear();
            if (IsAddNullRow)
            {
                if (NullOrSelect == "Null")
                    this.Items.Add(new ListItem(ResourceManager.GetString("DropDowList_SelectZero"), ""));
                else
                    this.Items.Add(new ListItem(ResourceManager.GetString("DropDowList_SelectNull"), "0"));
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
            }

            this.DataBind();
        }

        public void AddTableData(DataTable dt, bool IsAddNullRow, string NullOrSelect,bool AllItem)
        {
            this.Items.Clear();
            if (IsAddNullRow)
            {
                if (NullOrSelect == "Null")
                    this.Items.Add(new ListItem(ResourceManager.GetString("DropDowList_SelectZero"), ""));
                else
                    this.Items.Add(new ListItem(ResourceManager.GetString("DropDowList_SelectNull"), "0"));
            }

            if (AllItem)
                  this.Items.Add(new ListItem("All", "All"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));

            }
            this.DataBind();
        }

        public void AddTableData(DataTable dt,int dtValueCol,int dtTextCol, bool IsAddNullRow,string NullOrSelect)
        {
            this.Items.Clear();
            if (IsAddNullRow )
                if(NullOrSelect=="Null")
                    this.Items.Add(new ListItem(ResourceManager.GetString("DropDowList_SelectNull"), ""));
                else
                    this.Items.Add(new ListItem(ResourceManager.GetString("DropDowList_SelectZero"), "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.Items.Add(new ListItem(dt.Rows[i][dtTextCol].ToString(), dt.Rows[i][dtValueCol].ToString()));
            }

            this.DataBind();
        }
        public void AddTableData(DataTable dt, int dtValueCol, int dtTextCol, bool IsAddNullRow, string NullOrSelect, string selectid)
        {
           AddTableData( dt, dtValueCol, dtTextCol,  IsAddNullRow, NullOrSelect);
           this.SelectedValue = selectid;
        }
        public void AddTableData(DataTable dt,bool IfDataBinding)
        {
            this.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
            }
            if(IfDataBinding)//我都忘记了什么时候要用到这个玩意儿，好像是在gridview中绑定这样的控件时不需要再绑定
            this.DataBind();
        }
        /// <summary>
        /// 添加表数据        /// </summary>
        /// <param name="dt">要绑定的表</param>
        public void AddTableData(NameValueCollection dt, string selectid)
        {
            AddTableData(dt);
            this.SelectedValue = selectid;
        }
        public void AddTableData(NameValueCollection dt)
        {
            this.Items.Clear();
            for (int i = 0; i < dt.Count; i++)
            {
                this.Items.Add(new ListItem(dt.GetKey(i).ToString(), dt.Get(i).ToString()));
            }

            this.DataBind();
        }

        #region SQL字符串







        /// <summary>
        /// SQL字符串变量        /// </summary>
        private string sqltext;


        /// <summary>
        /// SQL字符串属性        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string SqlText
        {
            get
            {
                return sqltext;
            }

            set
            {
                sqltext = value;
            }
        }

        #endregion
       
        /// <summary>
        /// 当某选项被选中后,获取焦点的控件ID(如提交按钮等)
        /// </summary>
		[Bindable(true),Category("Appearance"),DefaultValue("")] 
		public string SetFocusButtonID
		{
			get
			{
				object o = ViewState[this.ClientID+"_SetFocusButtonID"];
				return (o==null)?"":o.ToString(); 
			}
			set
			{
				ViewState[this.ClientID+"_SetFocusButtonID"] = value;
				if(value!="")
				{
					this.Attributes.Add("onChange","document.getElementById('"+value+"').focus();");
				}
			}
		}


        private string _hintTitle = "";
        /// <summary>
        /// 提示框标题        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string HintTitle
        {
            get { return _hintTitle; }
            set { _hintTitle = value; }
        }


        private string _hintInfo = "";
        /// <summary>
        /// 提示框内容        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string HintInfo
        {
            get { return _hintInfo; }
            set { _hintInfo = value; }
        }

        private int _hintLeftOffSet = 0;

        /// <summary>
        /// 提示框左侧偏移量
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(0)]
        public int HintLeftOffSet
        {
            get { return _hintLeftOffSet; }
            set { _hintLeftOffSet = value; }
        }

        private int _hintTopOffSet = 0;

        /// <summary>
        /// 提示框顶部偏移量
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(0)]
        public int HintTopOffSet
        {
            get { return _hintTopOffSet; }
            set { _hintTopOffSet = value; }
        }

        private string _hintShowType = "up";//或"down"

        /// <summary>
        /// 提示框风格,up(上方显示)或down(下方显示)
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("up")]
        public string HintShowType
        {
            get { return _hintShowType; }
            set { _hintShowType = value; }
        }

        /// <summary>
        /// 提示框高度        /// </summary>
        private int _hintHeight = 50;
        [Bindable(true), Category("Appearance"), DefaultValue(130)]
        public int HintHeight
        {
            get { return _hintHeight; }
            set { _hintHeight = value; }
        }

        /// <summary> 
        /// 输出html,在浏览器中显示控件        /// </summary>
        /// <param name="output"> 要写出到的 HTML 编写器 </param>
        protected override void Render(HtmlTextWriter output)
        {
            if (this.HintInfo != "")
            {
                output.WriteBeginTag("span id=\"" + this.ClientID + "\"  onmouseover=\"showhintinfo(this," + this.HintLeftOffSet + "," + this.HintTopOffSet + ",'" + this.HintTitle + "','" + this.HintInfo + "','" + this.HintHeight + "','" + this.HintShowType + "');\" onmouseout=\"hidehintinfo();\">");
            }

            base.Render(output);

            if (this.HintInfo != "")
            {
                output.WriteEndTag("span");
            }
        }
	
	}

}
