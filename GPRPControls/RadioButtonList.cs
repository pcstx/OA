using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

using GPRP.GPRPComponents;
using MyADO;

namespace GPRP.GPRPControls
{
	/// <summary>
	/// RadioButtonList控件。
	/// </summary>
	[DefaultProperty("Text"),ToolboxData("<{0}:RadioButtonList runat=server></{0}:RadioButtonList>")]
    public class RadioButtonList : System.Web.UI.WebControls.RadioButtonList, IWebControl, IPostBackDataHandler
	{
        /// <summary>
        /// 构造函数        /// </summary>
		public RadioButtonList(): base()
		{
			//this.Font.Size=FontUnit.Smaller;
			this.BorderStyle=BorderStyle.Dotted; 
			this.BorderWidth=0; 
			this.RepeatColumns=2;
			this.RepeatDirection=RepeatDirection.Vertical;
			this.RepeatLayout = RepeatLayout.Table;
            this.CssClass = "buttonlist";
		}

        /// <summary>
        /// 添加表数据        /// </summary>
        /// <param name="dt">要绑定的表</param>
        public void AddTableData(DataTable dt)
        {
            AddTableData(dt, "");
        }
        public void AddTableData(DataTable dt, string selectid)
        {
            dataTableList = dt;
            selectid = "," + selectid + ",";
            this.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                if (selectid.IndexOf("," + dt.Rows[i][0].ToString() + ",") >= 0) this.Items[i].Selected = true;
            }
            this.DataBind();
        }

        /// <summary>
        /// 值选定
        /// </summary>
        /// <param name="sqlstring">查询字符串</param>
        /// <param name="selectid">选取项</param>
        public void SetSelected( string selectid)
        {
            selectid = "," + selectid + ",";

            for (int i = 0; i < dataTableList.Rows.Count; i++)
            {
                if (selectid.IndexOf("," + dataTableList.Rows[i][0].ToString() + ",") >= 0) this.Items[i].Selected = true;
                else this.Items[i].Selected = false;
            }
            //this.DataBind();
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
        private DataTable dataTableList;
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public DataTable DataTableList
        {
            get { return dataTableList; }
            set { dataTableList = value; }
        
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

        private int _hintHeight = 50;
        /// <summary>
        /// 提示框高度        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(130)]
        public int HintHeight
        {
            get { return _hintHeight; }
            set { _hintHeight = value; }
        }

        private int _topfirefoxoffset = 0;
        /// <summary>
        /// 提示框(firefox下)顶部偏移量        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(0)]
        public int HintTopFirefoxOffset
        {
            get { return _topfirefoxoffset; }
            set { _topfirefoxoffset = value; }
        }

    

        /// <summary> 
        /// 输出html,在浏览器中显示控件        /// </summary>
        /// <param name="output"> 要写出到的 HTML 编写器 </param>
        protected override void Render(HtmlTextWriter output)
        {
            if (this.HintInfo != "")
            {
               output.WriteBeginTag("span id=\"" + this.ClientID + "\"  onmouseover=\"showhintinfo(this," + this.HintLeftOffSet + "," + this.HintTopOffSet + ",'" + this.HintTitle + "','" + this.HintInfo + "','" + this.HintHeight + "','" + this.HintShowType + "','" + HintTopFirefoxOffset + "');\" onmouseout=\"hidehintinfo();\">");
            }

            base.Render(output);

            if (this.HintInfo != "")
            {
                output.WriteEndTag("span");
            }

        }
	}
}
