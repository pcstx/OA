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
using System.Text;
using GPRP.Web.UI;
using GPRP.GPRPComponents;
using GPRP.GPRPControls;
using GPRP.GPRPEnumerations;
using GPRP.GPRPBussiness;
using MyADO;

namespace GPRPWeb.tools
{
    public partial class AutoEntity : System.Web.UI.Page
    {
        public string s = "";
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        void QuichSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int k = Parttion(arr, low, high);

                for (int jj = 0; jj < arr.Length; jj++)
                {
                    s += arr[jj].ToString();
                }
                s += "\n";
                QuichSort(arr, low, k - 1);
                QuichSort(arr, k + 1, high);
            }
        }

        int Parttion(int[] arr, int low, int high)
        {
            int x = arr[low];

            while (low < high)
            {
                while (low < high && arr[high] >= x)
                    high--;
                arr[low] = arr[high];

                while (low < high && arr[low] < x)
                    low++;
                arr[high] = arr[low];
            }

            arr[low] = x;
            return low;
        }

        protected void Button_aspx_Click(object sender, EventArgs e)
        {
            string tableName = TextBox1.Text;
            s = "";
            if (tableName != "")
            {
               
                DataTable dt = DbHelper.GetInstance().GetSysTableColumnByTableName(tableName, "","1");
                s += "\n";
                s += "<%@ Register Assembly=\"YYControls\" Namespace=\"YYControls\" TagPrefix=\"yyc\" %>";
                s += "\n";
                s += "<%@ Register Assembly=\"AspNetPager\" Namespace=\"Wuqi.Webdiyer\" TagPrefix=\"webdiyer\" %>";
                s += "\n";
                s += "<%@ Register Assembly=\"AjaxControlToolkit\" Namespace=\"AjaxControlToolkit\" TagPrefix=\"cc1\" %>";
                s += "\n";
                s += "<%@ Register Assembly=\"GPRPControls\" Namespace=\"GPRP.GPRPControls\" TagPrefix=\"cc2\" %>";
                s += "\n";
                s += "<%@ Register Src=\"../ascx/UCOperationBanner.ascx\" TagName=\"UCOperationBanner\" TagPrefix=\"uc1\" %>";
                s += "\n";
                s += "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">";
                s += "\n";

                s += "\n";
                s += "<html xmlns=\"http://www.w3.org/1999/xhtml\" >";
                s += "\n";
                s += "<head id=\"Head1\" runat=\"server\">";
                s += "\n";
                s += "    <title>" + tableName + "</title>";
                s += "\n";
                s += "     <link href=\"../styles/dntmanager.css\" type=\"text/css\" rel=\"stylesheet\" />";
                s += "\n";
                s += "    	    <link href=\"../styles/StyleSheet.css\" type=\"text/css\" rel=\"stylesheet\" />";
                s += "\n";
                s += "    	    <script language=\"JavaScript1.2\" src=\"../JScript/modalpopup.js\" type=\"text/javascript\"></script>";
                s += "\n";
                s += "    	    <script language=\"JavaScript1.2\" src=\"../JScript/common.js\" type=\"text/javascript\"></script>";
                s += "\n";
                s += "    	  <script language=\"javascript\" type=\"text/javascript\">";

                s += "\n";
                s += "        function SetViewState()";
                s += "\n";
                s += "        {";
                s += "\n";
                s += "            PageMethods.SetAddViewState(getSucceeded);";
                s += "\n";
                s += "        }";
                s += "\n";
                s += "         //���óɹ��󣬰�ÿһ���ֶν��и��գ���Ϊ���¼Ӻ�ĵ���";
                s += "\n";
                s += "\n";
                s += "         function getSucceeded(result)";
                s += "\n";
                s += "         {";
                s += "         //**";
                s += "\n";
                s += "           document.getElementById('txtPSACNCO').value=result;";
                s += "\n";
                s += "         }";
                s += "\n";
                s += "\n";
                s += "    </script>  ";
                s += "\n";


                s += "\n";
                s += "</head>";
                s += "\n";
                s += "<body>";
                s += "\n";
                s += "    <form id=\"form1\" runat=\"server\">";
                s += "\n";
                s += "        <cc1:toolkitscriptmanager id=\"ToolkitScriptManager1\" runat=\"server\" EnablePageMethods=\"True\"></cc1:toolkitscriptmanager>";
                s += "\n";
                s += "          <asp:UpdatePanel ID=\"UpdatePanel1\" runat=\"server\">";
                s += "\n";
                s += "            <ContentTemplate>";
                s += "\n";
                s += "         <div >";
                s += "\n";
                s += "      ";
                s += "\n";
                s += "             <cc2:Button ID=\"btnSearch\" runat=\"server\" ButtonImgUrl=\"../images/query.gif\" Enabled=\"false\"  ShowPostDiv=\"false\" Text=\"Button_Search\" ValidateForm=\"false\" AutoPostBack=\"false\"/>";
                s += "\n";
                s += "            <cc2:Button ID=\"btnBrowseMode\" runat=\"server\" ShowPostDiv=\"false\" ValidateForm=\"false\" Text=\"Button_BrowseMode\" Enabled=\"false\" OnClick=\"btnBrowseMode_Click\" ButtonImgUrl=\"../images/browse.gif\"/>";
                s += "\n";
                s += "            <cc2:Button ID=\"btnEditMode\" runat=\"server\" ShowPostDiv=\"false\" ValidateForm=\"false\" Text=\"Button_EditMode\"  OnClick=\"btnBrowseMode_Click\"  ButtonImgUrl=\"../images/edit.gif\" />";
                s += "\n";
                s += "            <cc2:Button ID=\"btnAdd\" runat=\"server\" ShowPostDiv=\"false\" ValidateForm=\"false\" Text=\"Button_Add\" Disable=\"false\" ScriptContent=\"SetViewState()\" AutoPostBack=\"false\"  ButtonImgUrl=\"../images/add.gif\" />";
                s += "\n";
                s += "	        <cc2:Button ID=\"btnSubmit\" runat=\"server\" Text=\"Button_DafaultSubmit\" OnClick=\"btnBrowseMode_Click\"  ButtonImgUrl=\"../images/submit.gif\"/>";
                s += "\n";
                s += "	        <cc2:Button ID=\"btnDel\" runat=\"server\" ShowPostDiv=\"false\" ValidateForm=\"false\" AutoPostBack=\"false\" Text=\"Button_Del\" ButtonImgUrl=\"../images/del.gif\" OnClick=\"btnBrowseMode_Click\"  />";
                s += "\n";
                s += "             <cc2:Button ID=\"btnExport\" runat=\"server\" AutoPostBack=\"false\" ButtonImgUrl=\"../images/excel.gif\"";
                s += "\n";
                s += "                 ShowPostDiv=\"false\" Text=\"Button_Export\" ValidateForm=\"false\" IfShowDivOfButton=\"True\" HintHeight=\"60\" Width=\"100\"  />";
                s += "\n";
                s += "             <!--  Hint  start -->";
                s += "\n";
                s += "                 <!--������ť��ʾ�㲿�ֿ�ʼ-->";
                s += "\n";
                s += "                 <span id=\"hintExport\" style=\"display:none; position:absolute;z-index:500;\" onmouseover=\"showhintinfo_btn(btnExport,0,0,60,'up');\" onmouseout=\"hidehintinfo_btn();\">";
                s += "\n";
                s += "                    <div style=\"position:absolute; visibility: visible; width: 100px;z-index:501; height:60px;\" >";
                s += "\n";
                s += "                 <cc2:Button ID=\"btnExportExcel\" runat=\"server\" ShowPostDiv=\"false\" ValidateForm=\"false\" AutoPostBack=\"True\" Text=\"Button_ExportExcel\" ButtonImgUrl=\"../images/uparrow.gif\" OnClick=\"btnBrowseMode_Click\"  Width=\"100\"/>";
                s += "\n";
                s += "                 <cc2:Button ID=\"btnImportExcel\" runat=\"server\" ShowPostDiv=\"false\" ValidateForm=\"false\" AutoPostBack=\"True\" Text=\"Button_ImportExcel\" ButtonImgUrl=\"../images/downarrow.gif\" OnClick=\"btnBrowseMode_Click\"  Width=\"100\"/>";

                s += "                    </div>";
                s += "\n";
                s += "                   </span>";
                s += "\n";
                s += "                 <!--������ť��ʾ�㲿�ֿ�ʼ-->";
                s += "\n";
                s += "            <!--  Hint  end -->";
                s += "\n";
                s += "         </div>";
                s += "\n";
                s += "      <div class=\"ManagerForm\">";
                s += "\n";
                s += "    	<fieldset>		";
                s += "\n";
                s += "		<legend style=\"background:url(../images/legendimg.jpg) no-repeat 6px 50%;\"><asp:Label ID=\"lblBigTitle\" runat=\"server\" Text=\"\"></asp:Label></legend>";
                s += "\n";

                s += "\n";
                s += "          <!--page start-->";
                s += "\n";
                s += "            <div id=\"pager\" >";
                s += "\n";
                s += "            <div class=\"PagerArea\" ><webdiyer:AspNetPager ID=\"AspNetPager1\" runat=\"server\"  OnPageChanging=\"AspNetPager1_PageChanging\" onpagechanged=\"AspNetPager1_PageChanged\"  Width=\"100%\" ShowPageIndexBox=\"Always\" PageIndexBoxType=\"DropDownList\" TextBeforePageIndexBox=\"Page: \" ";
                s += "\n";
                s += "        HorizontalAlign=\"left\"  CenterCurrentPageButton=\"true\" NumericButtonCount=\"9\"></webdiyer:AspNetPager></div>";
                s += "\n";
                s += "            <div class=\"PagerText\">";
                s += "\n";
                s += "                    <asp:Label ID=\"lblPageSize\" runat=\"server\" Text=\"\"></asp:Label>&nbsp;<cc2:TextBox ID=\"txtPageSize\" runat=\"server\" Width=\"35\" TextMode=\"SingleLine\" SetFocusButtonID=\"btnPageSize\"  ></cc2:TextBox>";
                s += "\n";
                s += "                                <cc2:Button ID=\"btnPageSize\" Text=\"Button_GO\" runat=\"server\" Width=\"60\" OnClick=\"txtPageSize_TextChanged\" ShowPostDiv=\"false\"/>";
                s += "\n";
                s += "                                </div>";
                s += "\n";
                s += "                            ";
                s += "\n";
                s += "                                </div>";
                s += "\n";
                s += "                  <cc1:FilteredTextBoxExtender  ID=\"FilteredTextBoxExtender2\" runat=\"server\" ";
                s += "\n";
                s += "                    TargetControlID=\"txtPageSize\"";
                s += "\n";
                s += "                    FilterType=\"Numbers\"/>   ";
                s += "\n";
                s += "                                ";
                s += "\n";
                s += "            <!--page end-->";
                s += "\n";
                s += "            <!--gridview start-->";
                s += "\n";
                s += "                <!--gridview Browse mode start-->";
                s += "\n";
                s += "            <yyc:SmartGridView ID=\"GridView1\" runat=\"server\" AllowPaging=\"False\" AllowSorting=\"True\" AutoGenerateColumns=\"False\" ";
                s += "\n";
                s += "        PageSize=\"20\"  OnRowCommand=\"GridView1_RowCommand\" onrowcreated=\"GridView1_RowCreated\" OnRowDataBound=\"GridView1_RowDataBound\"";
                s += "\n";
                s += "        Width=\"100%\" BoundRowDoubleClickCommandName=\"\" MergeCells=\"\" >";
                s += "\n";
                s += "              <Columns>";
                s += "\n";
                s += "                    <asp:ButtonField CommandName=\"DoubleClick\" Visible=\"False\" />";
                s += "\n";
                s += "                    <asp:ButtonField CommandName=\"RightMenuClick\" Visible=\"False\" />";
                s += "\n";
                s += "                    <asp:TemplateField>";
                s += "\n";
                s += "                        <headertemplate>";
                s += "\n";
                s += "                            <asp:CheckBox ID=\"all\" runat=\"server\" />";
                s += "\n";
                s += "                        </headertemplate>";
                s += "\n";
                s += "                        <itemtemplate>";
                s += "\n";
                s += "                                <asp:CheckBox ID=\"item\" runat=\"server\" />";
                s += "\n";
                s += "                        </itemtemplate>";
                s += "\n";
                s += "                        <itemstyle width=\"50px\" />";
                s += "\n";
                s += "                    </asp:TemplateField>";
                s += "\n";
                s += "                   ";
                s += "\n";
                s += "              </Columns>";
                s += "\n";
                s += "            <SmartSorting AllowMultiSorting=\"True\" AllowSortTip=\"True\" />";
                s += "\n";
                s += "            <ClientButtons>";
                s += "\n";
                s += "                <yyc:ClientButton BoundCommandName=\"Sort\" Position=\"First\" AttributeKey=\"onclick\"";
                s += "\n";
                s += "                    AttributeValue=\"return confirm('ȷ�϶��ֶΡ�{1}��������')\" />";
                s += "\n";
                s += "            </ClientButtons>";
                s += "\n";
                s += "            <CascadeCheckboxes>";
                s += "\n";
                s += "                <yyc:CascadeCheckbox ChildCheckboxID=\"item\" ParentCheckboxID=\"all\" />";
                s += "\n";
                s += "            </CascadeCheckboxes>";
                s += "\n";
                s += "            <FixRowColumn FixRowType=\"Header,Pager\" TableHeight=\"408px\" TableWidth=\"100%\" FixColumns=\"2,3,4\" />";
                s += "\n";
                s += "            <CheckedRowCssClass CheckBoxID=\"item\" CssClass=\"SelectedRow\" />";
                s += "\n";
                s += "            <ContextMenus>";
                s += "\n";
                s += "                <yyc:ContextMenu Text=\"RightMenuClick\" BoundCommandName=\"RightMenuClick\" />";
                s += "\n";
                s += "                <yyc:ContextMenu Text=\"&lt;hr /&gt;\" />";
                s += "\n";
                s += "                <yyc:ContextMenu Text=\"webabcd blog\" NavigateUrl=\"http://webabcd.cnblogs.com\" Target=\"_blank\" />";
                s += "\n";
                s += "            </ContextMenus>";
                s += "\n";

                s += "\n";
                s += "            </yyc:SmartGridView>";
                s += "\n";
                s += "             <!--gridview Browse mode end-->";
                s += "\n";
                s += "              <!--gridview edit mode start-->";
                s += "\n";
                s += "            <yyc:SmartGridView ID=\"GridView2\" runat=\"server\" AllowPaging=\"False\" AllowSorting=\"True\" AutoGenerateColumns=\"False\" ";
                s += "\n";
                s += "        PageSize=\"20\"  OnRowCommand=\"GridView2_RowCommand\" onrowcreated=\"GridView2_RowCreated\" OnRowDataBound=\"GridView2_RowDataBound\"";
                s += "\n";
                s += "        Width=\"100%\" BoundRowDoubleClickCommandName=\"\" MergeCells=\"\">";
                s += "\n";
                s += "              <Columns>";
                s += "\n";

                s += "\n";
                s += "              </Columns>";
                s += "\n";
                s += "            <SmartSorting AllowMultiSorting=\"True\" AllowSortTip=\"True\" />";
                s += "\n";
                s += "            <ClientButtons>";
                s += "\n";
                s += "                <yyc:ClientButton BoundCommandName=\"Sort\" Position=\"First\" AttributeKey=\"onclick\"";
                s += "\n";
                s += "                    AttributeValue=\"return confirm('ȷ�϶��ֶΡ�{1}��������')\" />";
                s += "\n";
                s += "            </ClientButtons>";
                s += "\n";
                s += "            <CascadeCheckboxes>";
                s += "\n";
                s += "                <yyc:CascadeCheckbox ChildCheckboxID=\"item2\" ParentCheckboxID=\"all2\" />";
                s += "\n";
                s += "            </CascadeCheckboxes>";
                s += "\n";
                s += "            <FixRowColumn FixRowType=\"Header,Pager\" TableHeight=\"408px\" TableWidth=\"100%\" FixColumns=\"0,1\" />";
                s += "\n";
                s += "            <CheckedRowCssClass CheckBoxID=\"item\" CssClass=\"SelectedRow\" />";
                s += "\n";
                s += "            <ContextMenus>";
                s += "\n";
                s += "                <yyc:ContextMenu Text=\"RightMenuClick\" BoundCommandName=\"RightMenuClick\" />";
                s += "\n";
                s += "                <yyc:ContextMenu Text=\"&lt;hr /&gt;\" />";
                s += "\n";
                s += "                <yyc:ContextMenu Text=\"webabcd blog\" NavigateUrl=\"http://webabcd.cnblogs.com\" Target=\"_blank\" />";
                s += "\n";
                s += "            </ContextMenus>";
                s += "\n";

                s += "\n";
                s += "            </yyc:SmartGridView>";
                s += "\n";
                s += "             <!--gridview edit mode end-->";
                s += "\n";
                s += "             <!--grid view end--> ";
                s += "\n";

                s += "\n";
                s += "		</fieldset>";
                s += "\n";
                s += "    </div> ";
                s += "\n";
                s += "        </ContentTemplate>";
                s += "\n";
                s += "              <Triggers>";
                s += "\n";
                s += "                            <asp:AsyncPostBackTrigger ControlID=\"btnSumbit\" EventName=\"Click\" />";
                s += "\n";
                s += "               </Triggers>";
                s += "\n";
                s += "     </asp:UpdatePanel>";
                s += "\n";
                s += "      <!--  Hint  start -->";
                s += "\n";
                s += "     <cc2:Hint id=\"Hint1\" runat=\"server\" HintImageUrl=\"../images\" WidthUp=\"270\"></cc2:Hint>";
                s += "\n";
                s += "      <!--  Hint  end -->";
                s += "\n";
                s += "     <cc1:UpdatePanelAnimationExtender ID=\"upae\" BehaviorID=\"animation\" runat=\"server\" TargetControlID=\"UpdatePanel1\">";
                s += "\n";
                s += "        </cc1:UpdatePanelAnimationExtender>";
                s += "\n";

                s += "\n";
                s += "        <asp:Button runat=\"server\" ID=\"hiddenTargetControlForModalPopup\" style=\"display:none\"/>";
                s += "\n";
                s += "        <cc1:ModalPopupExtender runat=\"server\" ID=\"programmaticModalPopup\"";
                s += "\n";
                s += "            BehaviorID=\"programmaticModalPopupBehavior\"";
                s += "\n";
                s += "            TargetControlID=\"hiddenTargetControlForModalPopup\"";
                s += "\n";
                s += "            PopupControlID=\"programmaticPopup\" ";
                s += "\n";
                s += "            BackgroundCssClass=\"modalBackground\"";
                s += "\n";
                s += "            DropShadow=\"True\"";
                s += "\n";
                s += "            CancelControlID=\"CancelButton\"";
                s += "\n";
                s += "            PopupDragHandleControlID=\"programmaticPopupDragHandle\"";
                s += "\n";
                s += "            RepositionMode=\"RepositionOnWindowScroll\" >";
                s += "\n";
                s += "        </cc1:ModalPopupExtender>";
                s += "\n";

                s += "\n";
                s += "          <asp:Panel runat=\"server\" CssClass=\"modalPopup\" ID=\"programmaticPopup\" style=\"display:none;width:650px;padding:0 0px 0px 0px\">";
                s += "\n";
                s += "           <div id=\"programmaticPopupDragHandle\" class=\"programmaticPopupDragHandle\">";
                s += "\n";
                s += "             <div class=\"h2blk\">";
                s += "\n";
                s += "             <h2 id=\"Profile_title\" ><asp:Label ID=\"lblTitle\" runat=\"server\" Text=\"Label\"></asp:Label></h2>";
                s += "\n";
                s += "             </div>";
                s += "\n";
                s += "              <cc2:Button ID=\"MaxAddButton\" ScriptContent=\"MaxAddFrom();\" ShowPostDiv=\"false\" runat=\"server\" Text=\"Label_NULL\"  CSS=\"MaxButtonCSS\" AutoPostBack=\"false\" ButtontypeMode=\"Normal\" Page_ClientValidate=\"false\" Disable=\"true\" ToolTip=\"Max\" Width=\"12\" />";
                s += "\n";
                s += "		        <cc2:Button ID=\"CancelButton\" ShowPostDiv=\"false\" runat=\"server\" Text=\"Label_NULL\"  CSS=\"CancelButtonCSS\" AutoPostBack=\"false\" ButtontypeMode=\"Normal\" Page_ClientValidate=\"false\" Disable=\"false\" ToolTip=\"Close\" Width=\"12\" />";
                s += "\n";
                s += "           </div>";
                s += "\n";
                s += "           <div id=\"wrapResumeContent\">";
                s += "\n";
                s += "                    <asp:UpdatePanel ID=\"UpdatePanel2\" runat=\"server\">";
                s += "\n";
                s += "                         <ContentTemplate>   ";
                s += "\n";
                s += "                         <div >";
                s += "\n";
                s += "                              <cc2:Button ID=\"btnSumbit\" ShowPostDiv=\"false\" runat=\"server\" Text=\"Label_NULL\" CSS=\"SaveButtonCSS\" AutoPostBack=\"true\" ButtontypeMode=\"Normal\" OnClick=\"hideModalPopupViaServer_Click\" Page_ClientValidate=\"true\" Disable=\"false\" Width=\"26\" />";
                s += "\n";
                s += "                              <cc2:Button ID=\"btnSubmitAndClose\" ShowPostDiv=\"false\" runat=\"server\" Text=\"Label_NULL\" CSS=\"SaveAndCloseCSS\" AutoPostBack=\"true\" ButtontypeMode=\"Normal\" OnClick=\"hideModalPopupViaServer_Click\" Page_ClientValidate=\"true\" Disable=\"false\" Width=\"26\" />";
                s += "\n";
                s += " 		                   </div>";
                s += "\n";
                s += "                           <div class=\"conblk2\" id=\"ProfileContainer\" xmlns:fo=\"http://www.w3.org/1999/XSL/Format\">";
                s += "\n";
                s += "		                    <div class=\"con\" id=\"wrapProfile\" \">";
                s += "\n";
                s += "					                    <div id=\"ProfileView\">";
                s += "\n";
                s += "					                    <div class=\"formblk\">";
                s += "\n";
                s += "					                    <div class=\"textblk\">";

                s += "\n";
                s += "                                             <div class=\"clear\"><div class=\"half\"><label class=\"char5\"></label><div class=\"iptblk\"></div></div>";
                s += "\n";
                s += "                                                                <div class=\"half\"><label class=\"char5\"></label><div class=\"iptblk\"></div></div>";
                s += "\n";
                s += "                                             </div>";
                s += "\n";
                s += "                                                             <div  class=\"clear\"><label class=\"char5\"> <asp:Label ID=\"lblMsg\" runat=\"server\" Text=\"\"></asp:Label></label></div>";

                s += "\n";
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Select(""))
                        {
                            s += "\n";
                            s += "   <!--" + dr["ColDescriptionCN"] + "-->   <asp:Label ID=\"lbl" + dr["ColumnName"] + "\" runat=\"server\" Text=\"\"></asp:Label><cc2:TextBox ID=\"txt" + dr["ColumnName"] + "\" runat=\"server\" Width=\"150\" ></cc2:TextBox>";
                        }
                    }
                }
                s += "					                    </div>";
                s += "\n";
                s += "					                    </div>";
                s += "\n";
                s += "					                    </div>";
                s += "\n";

                s += "\n";
                s += "	                               </div>";
                s += "\n";
                s += "	                            </div>";
                s += "\n";
                s += "	                            <!--validate start-->";
                s += "\n";

                s += "\n";
                s += "	                            <!--validate end-->  ";
                s += "\n";
                s += "\n";
                s += "                 </ContentTemplate>";
                s += "\n";

                s += "\n";
                s += "            </asp:UpdatePanel>";
                s += "\n";

                s += "\n";
                s += "        </div>";
                s += "\n";
                s += "   </asp:Panel>";

                s += "\n";
                s += "         <asp:Button runat=\"server\" ID=\"hiddenTargetControlForQueryModalPopup\" style=\"display:none\"/>";
                s += "\n";
                s += "  <cc1:ModalPopupExtender runat=\"server\" ID=\"programmaticQueryModalPopup\"";
                s += "\n";
                s += "      BehaviorID=\"programmaticQueryModalPopupBehavior\"";
                s += "\n";
                s += "      TargetControlID=\"hiddenTargetControlForQueryModalPopup\"";
                s += "\n";
                s += "      PopupControlID=\"programmaticPopupQuery\" ";
                s += "\n";
                s += "      BackgroundCssClass=\"modalBackground\"";
                s += "\n";
                s += "      DropShadow=\"False\"";
                s += "\n";
                s += "      CancelControlID=\"QueryCancelButton\"";
                s += "\n";
                s += "      PopupDragHandleControlID=\"programmaticQueryPopupDragHandle\"";
                s += "\n";
                s += "      RepositionMode=\"RepositionOnWindowScroll\" >";
                s += "\n";
                s += "  </cc1:ModalPopupExtender>";

                s += "\n";
                s += "    <asp:Panel runat=\"server\" CssClass=\"modalPopup\" ID=\"programmaticPopupQuery\" style=\"display:none;width:650px;  padding:0 0px 0px 0px\">";
                s += "\n";
                s += "      <div id=\"programmaticQueryPopupDragHandle\" class=\"programmaticPopupDragHandle\">";
                s += "\n";
                s += "          <div class=\"h2blk\">";
                s += "\n";
                s += "          <h2 id=\"H2_2\" ><asp:Label ID=\"lblSearchTitle\" runat=\"server\" Text=\"Label\"></asp:Label></h2>";
                s += "\n";
                s += "           </div>   ";
                s += "\n";
                s += "        <cc2:Button ID=\"MaxButton\" ScriptContent=\"MaxQueryFrom();\" ShowPostDiv=\"false\" runat=\"server\" Text=\"Label_NULL\"  CSS=\"MaxButtonCSS\" AutoPostBack=\"false\" ButtontypeMode=\"Normal\" Page_ClientValidate=\"false\" Disable=\"true\" ToolTip=\"Max\" Width=\"12\" />";
                s += "\n";
                s += "		  <cc2:Button ID=\"QueryCancelButton\" ShowPostDiv=\"false\" runat=\"server\" Text=\"Label_NULL\"  CSS=\"CancelButtonCSS\" AutoPostBack=\"false\" ButtontypeMode=\"Normal\" Page_ClientValidate=\"false\" Disable=\"false\" ToolTip=\"Close\" Width=\"12\" />";

                s += "\n";
                s += "       </div>";
                s += "\n";
                s += "     <div >";
                s += "\n";
                s += "              <asp:UpdatePanel ID=\"UpdatePanel3\" runat=\"server\">";
                s += "\n";
                s += "                   <ContentTemplate>   ";
                s += "\n";
                s += "                   <div class=\"edit\">";
                s += "\n";
                s += "                          <cc2:Button ID=\"btnSubmitSearch\" ShowPostDiv=\"false\" runat=\"server\" Text=\"Label_NULL\" AutoPostBack=\"true\" ButtontypeMode=\"Normal\"   Page_ClientValidate=\"false\" Disable=\"true\"  Width=\"26\" CSS=\"SearchButtonCSS\" />";
                s += "\n";
                s += "		             </div> ";
                s += "\n";
                s += "                     <div class=\"conblk2\" xmlns:fo=\"http://www.w3.org/1999/XSL/Format\">";
                s += "\n";
                s += "	                    <div class=\"con\" >";
                s += "\n";
                s += "			                        <div class=\"h2blk\">";
                s += "\n";
                s += "			                        </div>";
                s += "\n";
                s += "				                    <div id=\"Div5\">";
                s += "\n";
                s += "				                    <div class=\"formblk\">";
                s += "\n";
                s += "				                    <div class=\"textblk\">";

                s += "\n";
                s += "				                    </div>";
                s += "\n";
                s += "				                    </div>";
                s += "\n";
                s += "				                    </div>";

                s += "\n";
                s += "                             </div>";

                s += "\n";
                s += "                          </div>";
                s += "\n";
                s += "                          <!--validate start-->";

                s += "\n";
                s += "                          <!--validate end-->     ";
                s += "\n";
                s += "           </ContentTemplate>";

                s += "\n";
                s += "      </asp:UpdatePanel>";

                s += "\n";
                s += "  </div>";
                s += "\n";
                s += "</asp:Panel>";
                s += "\n";
                s += "    </form>";
                s += "\n";
                s += "</body>";
                s += "\n";
                s += "</html>";
                TextBox2.Text = s;

            }
            else
            {
                TextBox2.Text = "";
            }

        }
        protected void Button_aspxcs_Click(object sender, EventArgs e)
        {
            string tableName = TextBox1.Text;
            s = "";
            if (tableName != "")
            {
                DataTable dt = DbHelper.GetInstance().GetSysTableColumnByTableName(tableName, "","1");

                s += "\n";
                s += "using System;";
                s += "\n";
                s += "using System.Data;";
                s += "\n";
                s += "using System.Configuration;";
                s += "\n";
                s += "using System.Collections;";
                s += "\n";
                s += "using System.Web;";
                s += "\n";
                s += "using System.Web.Security;";
                s += "\n";
                s += "using System.Web.UI;";
                s += "\n";
                s += "using System.Web.UI.WebControls;";
                s += "\n";
                s += "using System.Web.UI.WebControls.WebParts;";
                s += "\n";
                s += "using System.Web.UI.HtmlControls;";
                s += "\n";
                s += "using System.Web.Services;";
                s += "\n";
                s += "using GPRP.Web.UI;";
                s += "\n";
                s += "using GPRP.GPRPComponents;";
                s += "\n";
                s += "using GPRP.GPRPData;";
                s += "\n";
                s += "using GPRP.GPRPControls;";
                s += "\n";
                s += "using GPRP.GPRPEnumerations;";
                s += "\n";
                s += "using GPRP.GPRPBussiness;";
                s += "\n";
                s += "using GPRP.Entity;";
                s += "\n";
                s += "using YYControls;";
                s += "\n";
                s += "using VBControls.VBProject;";
                s += "\n";
                s += "namespace HRMWeb.aspx";
                s += "\n";
                s += "{";
                s += "\n";
                s += "    public partial class _01011 :BasePage";
                s += "\n";
                s += "    {";
                s += "\n";
                s += "        private static string strOperationState;";
                s += "\n";
                s += "        protected void Page_Load(object sender, EventArgs e)";
                s += "\n";
                s += "        {";
                s += "\n";
                s += "            //UCOperationBanner1.btnClick += new AJAXWeb.ascx.UCOperationBanner.ButtonClick(UCOperationBanner1_btnClick);";
                s += "\n";
                s += "            if (!Page.IsPostBack)";
                s += "\n";
                s += "            {";
                s += "\n";
                s += "                ViewState[\"PageSize\"] = GeneralConfigs.GetConfig().PageSize;//ÿҳ��ʾ��Ĭ��ֵ";
                s += "\n";
                s += "                txtPageSize.Text = GeneralConfigs.GetConfig().PageSize.ToString();";
                s += "\n";
                s += "                //�޸ı���,��ȡҪ��ʾ�ı�ṹ,";
                s += "\n";
                s += "                ViewState[\"sysTableColumn\"] = DbHelper.GetInstance().GetSysTableColumnByTableName(\"" + tableName + "\", \"\");";
                s += "\n";
                s += "                ViewState[\"sysTable\"] = DbHelper.GetInstance().GetSysTable(\"" + tableName + "\");";
                s += "\n";

                s += "\n";
                s += "                //��Pannel����Ҫ�󶨵Ŀؼ�����������Ϊ�����GridView��ת����ʱ���ṩ����Դ��";
                s += "\n";
                s += "                //�������panel�ڵ���";
                s += "\n";
                s += "                BindDropDownList(\"\");";
                s += "\n";

                s += "\n";
                s += "                //����gridView�Ľ��������";
                s += "\n";
                s += "                if (ViewState[\"Mode\"] == null) ViewState[\"Mode\"] = BrowseEditMode.Browse;";
                s += "\n";
                s += "                GridView1BrowseUI(BrowseEditMode.Browse);";
                s += "\n";
                s += "                GridView1EditUI(BrowseEditMode.Edit);";
                s += "\n";
                s += "                GridView2.Visible = false;";
                s += "\n";
                s += "                BindGridView();";
                s += "\n";
                s += "\n";
                s += "\n";
                s += "                //����ҳ������";
                s += "\n";
                s += "                SetText();";

                s += "\n";
                s += "            }";

                s += "\n";
                s += "        }";
                s += "\n";
                s += "        //����ÿҳ��ʾ��¼����������ģ����Ҫ����Ĭ�ϵ�ҳ��¼������config/geneal.config�и��� PageSize";
                s += "\n";
                s += "       protected void txtPageSize_TextChanged(object sender, EventArgs e)";
                s += "\n";
                s += "        {";
                s += "\n";
                s += "           if (txtPageSize.Text == \"\" || Convert.ToInt32(txtPageSize.Text) == 0)";
                s += "\n";
                s += "            {";
                s += "\n";
                s += "                ViewState[\"PageSize\"] = GeneralConfigs.GetConfig().PageSize;//ÿҳ��ʾ��Ĭ��ֵ";
                s += "\n";
                s += "            }";
                s += "\n";
                s += "            else";
                s += "\n";
                s += "            {";
                s += "\n";
                s += "                ViewState[\"PageSize\"] = Convert.ToInt32(txtPageSize.Text);";
                s += "\n";
                s += "            }";
                s += "\n";
                s += "            AspNetPager1.PageSize = Convert.ToInt32(ViewState[\"PageSize\"]);";
                s += "\n";
                s += "            //�ٽ��а�һ��";
                s += "\n";
                s += "            BrowseEditMode mode = ((BrowseEditMode)ViewState[\"Mode\"]);";
                s += "\n";
                s += "            //if (mode == BrowseEditMode.Edit)";
                s += "\n";
                s += "            GridView1EditUI(BrowseEditMode.Edit);";

                s += "\n";
                s += "            BindGridView();";


                s += "\n";
                s += "        }";
                s += "\n";
                s += "        #region gridView �¼�";
                s += "\n";
                s += "         protected void GridView1_RowCreated(Object sender, GridViewRowEventArgs e)";
                s += "\n";
                s += "        {";
                s += "\n";
                s += "            // The GridViewCommandEventArgs class does not contain a ";
                s += "\n";
                s += "            // property that indicates which row's command button was";
                s += "\n";
                s += "            // clicked. To identify which row's button was clicked, use ";
                s += "\n";
                s += "           // the button's CommandArgument property by setting it to the ";
                s += "\n";
                s += "            // row's index.";
                s += "\n";
                s += "            if (e.Row.RowType == DataControlRowType.DataRow)";
                s += "\n";
                s += "            {";

                s += "\n";
                s += "            }";
                s += "\n";
                s += "        }";
                s += "\n";
                s += "        //1������key�ֶ���GridView�е���һ�У�Ĭ�϶��ǵ�5�У���һ����Ϊbutton ������ΪcheckBox ������Ϊedit�ֶΣ������� �ؼ�����";
                s += "\n";
                s += "        //2��ͨ���ؼ��ֻ�ȡ������¼��";
                s += "\n";
                s += "        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)";
                s += "\n";
                s += "       {";
                s += "\n";
                s += "            if (e.CommandName == \"select\")";
                s += "\n";
                s += "            {";
                s += "\n";
                s += "                programmaticModalPopup.Show();";
                s += "\n";
                s += "                int index = Convert.ToInt32(e.CommandArgument);   //��ȡ�к�";

                s += "\n";
                s += "                GridViewRow row = GridView1.Rows[index];       //���ڵ���";

                s += "\n";
                s += "                //��һ�����޸�λ��";
                s += "\n";
                s += "                string keyCol = row.Cells[4].Text.ToString();";
                s += "\n";
                s += "                //�ڶ������޸�λ��";
                s += "\n";
                s += "                " + tableName + "Entity _" + tableName + "Entity = new " + tableName + "Entity();";
                s += "\n";
                s += "                _" + tableName + "Entity = DbHelper.GetInstance().Get" + tableName + "EntityByKeyCol(keyCol);";
                s += "\n";
                s += "                if (_" + tableName + "Entity != null) SetPannelData(_" + tableName + "Entity);";

                s += "\n";
                s += "            }";
                s += "\n";
                s += "        }";
                s += "\n";
                s += "        //����Ҫ����dorpdownlist/chk�ؼ���ת��";
                s += "\n";
                s += "        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)";
                s += "\n";
                s += "        {";
                s += "\n";
                s += "            if (e.Row.RowType == DataControlRowType.DataRow)";
                s += "\n";
                s += "            {";
                s += "\n";
                s += "                //��Ϊ�༭ģʽ������£�Ҫ�����������ת��";
                s += "\n";
                s += "                //�ٽ��а�һ��";

                s += "\n";
                s += "                //// Retrieve the LinkButton control from the first column.";
                s += "\n";
                s += "                //System.Web.UI.WebControls.Button addButton = (System.Web.UI.WebControls.Button)e.Row.Cells[4].Controls[0];";

                s += "\n";
                s += "                //// Set the LinkButton's CommandArgument property with the";
                s += "\n";
                s += "                //// row's index.";
                s += "\n";
                s += "                //addButton.CommandArgument = e.Row.RowIndex.ToString();";


                s += "\n";
                s += "            }";
                s += "\n";
                s += "        }";

                s += "\n";
                s += "        protected void GridView2_RowCreated(Object sender, GridViewRowEventArgs e)";
                s += "\n";
                s += "        {";
                s += "\n";
                s += "            // The GridViewCommandEventArgs class does not contain a ";
                s += "\n";
                s += "            // property that indicates which row's command button was";
                s += "\n";
                s += "            // clicked. To identify which row's button was clicked, use ";
                s += "\n";
                s += "            // the button's CommandArgument property by setting it to the ";
                s += "\n";
                s += "            // row's index.";
                s += "\n";
                s += "            if (e.Row.RowType == DataControlRowType.DataRow)";
                s += "\n";
                s += "            {";

                s += "\n";
                s += "            }";

                s += "\n";
                s += "        }";
                s += "\n";
                s += "        //�༭״̬�µ�GridViewһ�㲻���õ��÷���";
                s += "\n";
                s += "        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)";
                s += "\n";
                s += "        {";

                s += "\n";
                s += "        }";
                s += "\n";
                s += "        //����Ҫ����dorpdownlist/chk�ؼ���ת��";
                s += "\n";
                s += "        //��Ҫ���и���";
                s += "\n";
                s += "        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)";
                s += "\n";
                s += "        {";
                s += "\n";
                s += "            if (e.Row.RowType == DataControlRowType.DataRow)";
                s += "\n";
                s += "            {";
                s += "\n";
                s += "                /*����gridview �� dropdownlist/checkbox �����ݵĻ�ԭ��*/";
                s += "\n";
                s += "                //GPRP.GPRPControls.DropDownList a = (GPRP.GPRPControls.DropDownList)e.Row.FindControl(\"PSACNDAT\");";
                s += "\n";
                s += "                //DataTable dt = null;";
                s += "\n";
                s += "                //if (ViewState[\"PSACNDATtable\"] != null)";
                s += "\n";
                s += "                //{";
                s += "\n";
                s += "                    //dt = (DataTable)ViewState[\"PSACNDATtable\"];";
                s += "\n";
                s += "                    //a.AddTableData(dt, DataBinder.Eval(e.Row.DataItem, \"PSACNDAT\").ToString());";
                s += "\n";
                s += "                //}";
                s += "\n";
                s += "                System.Web.UI.HtmlControls.HtmlInputCheckBox check = (System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Row.FindControl(\"check\");";
                s += "\n";
                s += "                check.Value = DataBinder.Eval(e.Row.DataItem, \"PSACNCO\").ToString();";
                s += "\n";
                s += "            }";
                s += "\n";
                s += "        }";
                s += "\n";
                s += "        #endregion";
                s += "\n";
                s += "        #region aspnetPage ��ҳ����";
                s += "\n";
                s += "        //�����������";
                s += "\n";
                s += "        protected void AspNetPager1_PageChanged(object src, EventArgs e)";
                s += "\n";
                s += "        {";
                s += "\n";
                s += "            BrowseEditMode mode = ((BrowseEditMode)ViewState[\"Mode\"]);";
                s += "\n";
                s += "            GridView1EditUI(BrowseEditMode.Edit);//�༭ģʽ��ÿ�ζ�����������ҳ�档";
                s += "\n";
                s += "            BindGridView();";
                s += "\n";
                s += "        }";
                s += "\n";
                s += "        protected void AspNetPager1_PageChanging(object src, EventArgs e)";
                s += "\n";
                s += "        {";

                s += "\n";
                s += "        }";
                s += "\n";
                s += "        #endregion";

                s += "\n";
                s += "        #region gridview UI";
                s += "\n";
                s += "        //������Ҫ���ģ���Ҫ�Ǹ��Ļ�ȡ����Դ�ķ���";
                s += "\n";
                s += "        private void BindGridView()";
                s += "\n";
                s += "        {";
                s += "\n";
                s += "            DataTable dt = DbHelper.GetInstance().GetDBRecord(\"\",\"*\",\"" + tableName + "\",\"where\",\"" + tableName + "OI\" AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);";
                s += "\n";
                s += "            if (dt.Rows.Count > 0)";
                s += "\n";
                s += "                AspNetPager1.RecordCount = Convert.ToInt32(dt.Rows[0][\"RecordCount\"]);";
                s += "\n";
                s += "            else";
                s += "\n";
                s += "                AspNetPager1.RecordCount = 0;";
                s += "\n";
                s += "            GridView1.DataSource = dt;";
                s += "\n";
                s += "            GridView1.DataBind();";
                s += "\n";
                s += "            BuildNoRecords(GridView1, dt);";

                s += "\n";
                s += "            GridView2.DataSource = dt;";
                s += "\n";
                s += "            GridView2.DataBind();";
                s += "\n";
                s += "            BuildNoRecords(GridView2, dt);";
                s += "\n";
                s += "        }";
                s += "\n";
                s += "        //Show Header/Footer of Gridview with Empty Data Source ";
                s += "\n";
                s += "        public void BuildNoRecords(GridView gridView, DataTable ds)";
                s += "\n";
                s += "        {";
                s += "\n";
                s += "            try";
                s += "\n";
                s += "            {";
                s += "\n";
                s += "                if (ds.Rows.Count == 0)";
                s += "\n";
                s += "                {";
                s += "\n";
                s += "                    ds.Rows.Add(ds.NewRow());";
                s += "\n";
                s += "                    gridView.DataSource = ds;";
                s += "\n";
                s += "                    gridView.DataBind();";
                s += "\n";
                s += "                    int columnCount = gridView.Rows[0].Cells.Count;";
                s += "\n";
                s += "                    gridView.Rows[0].Cells.Clear();";
                s += "\n";
                s += "                    gridView.Rows[0].Cells.Add(new TableCell());";
                s += "\n";
                s += "                    gridView.Rows[0].Cells[0].ColumnSpan = columnCount;";
                s += "\n";
                s += "                    gridView.Rows[0].Cells[0].Text = \"No Records Found.\";";
                s += "\n";
                s += "                }";
                s += "\n";
                s += "            }";
                s += "\n";
                s += "            catch (Exception ex)";
                s += "\n";
                s += "            {";
                s += "\n";
                s += "            }";
                s += "\n";
                s += "        }";
                s += "\n";
                s += "        //gridViewUI��ʾ";
                s += "\n";
                s += "        private void GridView1BrowseUI(BrowseEditMode Mode)";
                s += "\n";
                s += "        {";
                s += "\n";
                s += "            //ѡҪ��sysTable���������Щ�ֶ�Ҫ��ʾ����";
                s += "\n";
                s += "            if (ViewState[\"sysTableColumn\"] == null)";
                s += "\n";
                s += "            {";
                s += "\n";
                s += "                return;";
                s += "\n";
                s += "            }";
                s += "\n";
                s += "            DataTable dt = (DataTable)ViewState[\"sysTableColumn\"];";
                s += "\n";
                s += "            if (dt != null)";
                s += "\n";
                s += "            {";
                s += "\n";
                s += "                if (dt.Rows.Count > 0)";
                s += "\n";
                s += "                {";
                s += "\n";
                s += "                    int gvWidth = 25;";
                s += "\n";
                s += "                    ButtonField btnSel = new ButtonField();";
                s += "\n";
                s += "                    btnSel.Text = \"Edit\";";
                s += "\n";
                s += "                    btnSel.CommandName = \"select\";";
                s += "\n";
                s += "                    btnSel.ShowHeader = true;";
                s += "\n";
                s += "                    btnSel.HeaderText = \"Edit\";";
                s += "\n";
                s += "                    btnSel.ButtonType = ButtonType.Button;";
                s += "\n";
                s += "                    btnSel.ItemStyle.Width = 25;";
                s += "\n";
                s += "                    GridView1.Columns.Add(btnSel);";

                s += "\n";
                s += "                    foreach (DataRow dr in dt.Select(\"\"))";
                s += "\n";
                s += "                    {";
                s += "\n";
                s += "                        if (dr[\"ColIsShow\"] != null)";
                s += "\n";
                s += "                        {";
                s += "\n";
                s += "                            if (dr[\"ColIsShow\"].ToString() == \"1\")";
                s += "\n";
                s += "                            {";
                s += "\n";
                s += "                                        if (Mode == BrowseEditMode.Browse)";
                s += "\n";
                s += "                                       {";
                s += "\n";
                s += "                                      switch (dr[\"ColType\"].ToString())";
                s += "\n";
                s += "                                      {";
                s += "\n";
                s += "                                          case \"CheckBox\":";
                s += "\n";
                s += "                                              VBControls.VBProject.WebControls.TBCheckBoxField mCheckBoxField = new VBControls.VBProject.WebControls.TBCheckBoxField();";
                s += "\n";
                s += "                                              switch (ForumUtils.GetCookie(\"Language\"))";
                s += "\n";
                s += "                                              {";
                s += "\n";
                s += "                                                  case \"zh-CN\":";
                s += "\n";
                s += "                                                      mCheckBoxField.HeaderText = dr[\"ColDescriptionCN\"].ToString();";
                s += "\n";
                s += "                                                      break;";
                s += "\n";
                s += "                                                  case \"en-US\":";
                s += "\n";
                s += "                                                      mCheckBoxField.HeaderText = dr[\"ColDescriptionUS\"].ToString();";
                s += "\n";
                s += "                                                      break;";
                s += "\n";
                s += "                                                  case \"zh-TW\":";
                s += "\n";
                s += "                                                      mCheckBoxField.HeaderText = dr[\"ColDescriptionTW\"].ToString();";
                s += "\n";
                s += "                                                      break;";
                s += "\n";
                s += "                                                  default:";
                s += "\n";
                s += "                                                      mCheckBoxField.HeaderText = dr[\"ColDescriptionCN\"].ToString();";
                s += "\n";
                s += "                                                      break;";
                s += "\n";
                s += "                                              }";
                s += "\n";
                s += "                                              mCheckBoxField.DataField = dr[\"ColName\"].ToString();";
                s += "\n";
                s += "                                              mCheckBoxField.DefineValue = \"1/0\";";
                s += "\n";
                s += "                                              mCheckBoxField.ItemStyle.Width = (dr[\"ColWidth\"] == null) ? 50 : Convert.ToInt32(dr[\"ColWidth\"].ToString());";
                s += "\n";
                s += "                                              GridView1.Columns.Add(mCheckBoxField);";
                s += "\n";
                s += "                                              break;";
                s += "\n";
                s += "                                          default:";
                s += "\n";
                s += "                                              BoundField mBoundField = new BoundField();";
                s += "\n";
                s += "                                              switch (ForumUtils.GetCookie(\"Language\"))";
                s += "\n";
                s += "                                              {";
                s += "\n";
                s += "                                                  case \"zh-CN\":";
                s += "\n";
                s += "                                                      mBoundField.HeaderText = dr[\"ColDescriptionCN\"].ToString();";
                s += "\n";
                s += "                                                      break;";
                s += "\n";
                s += "                                                  case \"en-US\":";
                s += "\n";
                s += "                                                      mBoundField.HeaderText = dr[\"ColDescriptionUS\"].ToString();";
                s += "\n";
                s += "                                                      break;";
                s += "\n";
                s += "                                                  case \"zh-TW\":";
                s += "\n";
                s += "                                                      mBoundField.HeaderText = dr[\"ColDescriptionTW\"].ToString();";
                s += "\n";
                s += "                                                      break;";
                s += "\n";
                s += "                                                  default:";
                s += "\n";
                s += "                                                      mBoundField.HeaderText = dr[\"ColDescriptionCN\"].ToString();";
                s += "\n";
                s += "                                                      break;";
                s += "\n";
                s += "                                              }";
                s += "\n";
                s += "                                              mBoundField.DataField = dr[\"ColName\"].ToString();";
                s += "\n";
                s += "                                              mBoundField.ItemStyle.Width = dr[\"ColWidth\"] == null ? 100 : Convert.ToInt32(dr[\"ColWidth\"].ToString());";
                s += "\n";
                s += "                                        gvWidth += Convert.ToInt16(mBoundField.ItemStyle.Width.Value);";
                s += "\n";
                s += "                                              GridView1.Columns.Add(mBoundField);";

                s += "\n";
                s += "                                              break;";
                s += "\n";
                s += "                                      }";


                s += "\n";
                s += "                                  }";
                s += "\n";
                s += "                            }";
                s += "\n";
                s += "                        }";
                s += "\n";
                s += "                   }";
                s += "\n";
                s += "                   if (gvWidth > 650)  { GridView1.Width = gvWidth;}";
                s += "\n";
                s += "                   else   { GridView1.Width = Unit.Percentage(100); }";
                s += "\n";
                s += "                }";
                s += "\n";
                s += "                else   { GridView1.Width = Unit.Percentage(100); }";
                s += "\n";
                s += "            }";
                s += "\n";
                s += "            else   { GridView1.Width = Unit.Percentage(100); }";
                s += "\n";
                s += "        }";
                s += "\n";
                s += "        //��Ҫ���ĵĵط����ǵ�Ϊdropdownlist �ؼ���ʱ�򣬰󶨵����⡣";
                s += "\n";
                s += "        private void GridView1EditUI(BrowseEditMode Mode)";
                s += "\n";
                s += "        {";
                s += "\n";
                s += "            //ѡҪ��sysTable���������Щ�ֶ�Ҫ��ʾ����";
                s += "\n";
                s += "            if (ViewState[\"sysTableColumn\"] == null)";
                s += "\n";
                s += "            {";
                s += "\n";
                s += "                return;";
                s += "\n";
                s += "            }";
                s += "\n";
                s += "            DataTable dt = (DataTable)ViewState[\"sysTableColumn\"];";
                s += "\n";
                s += "            if (dt != null)";
                s += "\n";
                s += "            {";
                s += "\n";
                s += "                if (dt.Rows.Count > 0)";
                s += "\n";
                s += "                {";

                s += "\n";
                s += "                    GridView2.Columns.Clear();";
                s += "\n";
                s += "                    int gvWidth = 20;";
                s += "\n";
                s += "                   string mColumnHearText = \"     <input id=\\\"checkAll\\\" title=\\\"\" + ResourceManager.GetString(\"lblSelectAll\") + \"\\\" onclick=\\\"javascript:CheckAll(this);\\\" runat=\\\"server\\\" type=\\\"checkbox\\\" />\";";
                s += "\n";
                s += "                    TemplateField chk = new TemplateField();";
                s += "\n";
                s += "                    chk.HeaderTemplate = new GridViewTempHtmlCheckBox(mColumnHearText);";
                s += "\n";
                s += "                   chk.ItemTemplate = new GridViewTempHtmlCheckBox(\"check\", mColumnHearText, \"\", false, 20);";
                s += "\n";
                s += "                  chk.ItemStyle.Width = 20;";
                s += "\n";
                s += "                   GridView2.Columns.Add(chk);";

                s += "\n";
                s += "                    foreach (DataRow dr in dt.Select(\"\"))";
                s += "\n";
                s += "                    {";
                s += "\n";
                s += "                        if (dr[\"ColIsShow\"] != null)";
                s += "\n";
                s += "                       {";
                s += "\n";
                s += "                           if (dr[\"ColIsShow\"].ToString() == \"1\")";
                s += "\n";
                s += "                           {";
                s += "\n";
                s += "                               if (Mode == BrowseEditMode.Edit)";
                s += "\n";
                s += "                               {";
                s += "\n";
                s += "                                   GridViewTemplate GVTColumnId = new GridViewTemplate();";
                s += "\n";
                s += "                                   GVTColumnId.ColumnID = dr[\"ColName\"].ToString();";
                s += "\n";
                s += "                                   switch (ForumUtils.GetCookie(\"Language\"))";
                s += "\n";
                s += "                                   {";
                s += "\n";
                s += "                                       case \"zh-CN\":";
                s += "\n";
                s += "                                          GVTColumnId.ColumnHearText = dr[\"ColDescriptionCN\"].ToString();";
                s += "\n";
                s += "                                            break;";
                s += "\n";
                s += "                                       case \"en-US\":";
                s += "\n";
                s += "                                            GVTColumnId.ColumnHearText = dr[\"ColDescriptionUS\"].ToString();";
                s += "\n";
                s += "                                            break;";
                s += "\n";
                s += "                                        case \"zh-TW\":";
                s += "\n";
                s += "                                           GVTColumnId.ColumnHearText = dr[\"ColDescriptionTW\"].ToString();";
                s += "\n";
                s += "                                           break;";
                s += "\n";
                s += "                                       default:";
                s += "\n";
                s += "                                           GVTColumnId.ColumnHearText = dr[\"ColDescriptionCN\"].ToString();";
                s += "\n";
                s += "                                          break;";
                s += "\n";
                s += "                                   }";
                s += "\n";
                s += "                                   GVTColumnId.ColumnValue = dr[\"ColName\"].ToString();";
                s += "\n";
                s += "                                   switch (dr[\"ColType\"].ToString())";
                s += "\n";
                s += "                                   {";
                s += "\n";
                s += "                                      case \"Label\":";
                s += "\n";
                s += "                                           GVTColumnId.WebControlType = ControlType.Label;";
                s += "\n";
                s += "                                           break;";
                s += "\n";
                s += "                                      case \"TextBox\":";
                s += "\n";
                s += "                                          GVTColumnId.WebControlType = ControlType.TextBox;";
                s += "\n";
                s += "                                          break;";
                s += "\n";
                s += "                                     case \"CheckBox\":";
                s += "\n";
                s += "                                         GVTColumnId.WebControlType = ControlType.CheckBox;";
                s += "\n";
                s += "                                        break;";
                s += "\n";
                s += "                                    case \"DropDownList\":";
                s += "\n";
                s += "                                        GVTColumnId.WebControlType = ControlType.DropDownList;";
                s += "\n";
                s += "                                        //GVTColumnId.dataTable = MakeControlTypeTable(dr[\"ColMatchTable\"].ToString(), dr[\"MatchTableColValue\"].ToString(), dr[\"MatchTableColTextCN\"].ToString(), dr[\"MatchTableColTextEN\"].ToString(), dr[\"MatchTableColTextTW\"].ToString(), dr[\"MatchTableSqlText\"].ToString());";
                s += "\n";
                s += "                                        GVTColumnId.dataTable = DateTypeDataTable();//����Ĵ����е�����,��Ϊд���ˡ�";
                s += "\n";
                s += "                                      break;";
                s += "\n";
                s += "                                  default:";
                s += "\n";
                s += "                                      GVTColumnId.WebControlType = ControlType.Label;";
                s += "\n";
                s += "                                      break;";
                s += "\n";
                s += "                              }";
                s += "\n";
                s += "                              if (dr[\"IsKey\"] == null || dr[\"IsKey\"].ToString() == \"0\")";
                s += "\n";
                s += "                                  GVTColumnId.ReadOnly = false;";
                s += "\n";
                s += "                              else";
                s += "\n";
                s += "                                  GVTColumnId.ReadOnly = true;";
                s += "\n";
                s += "                              GVTColumnId.Width = (dr[\"ColWidth\"] == null) ? 100 : Convert.ToInt32(dr[\"ColWidth\"].ToString());";
                s += "\n";
                s += "                              gvWidth += GVTColumnId.Width;";
                s += "\n";
                s += "                              GVTColumnId.ShowTemplate();";
                s += "\n";
                s += "                              GridView2.Columns.Add(GVTColumnId);";

                s += "\n";
                s += "                          }";

                s += "\n";
                s += "                      }";
                s += "\n";
                s += "                  }";
                s += "\n";
                s += "              }";
                s += "\n";
                s += "                   if (gvWidth > 650)  { GridView2.Width = gvWidth;}";
                s += "\n";
                s += "                   else   { GridView2.Width = Unit.Percentage(100); }";
                s += "\n";
                s += "                }";
                s += "\n";
                s += "                else   { GridView2.Width = Unit.Percentage(100); }";
                s += "\n";
                s += "            }";
                s += "\n";
                s += "            else   { GridView2.Width = Unit.Percentage(100); }";
                s += "\n";
                s += "        }";

                s += "\n";
                s += "  #endregion";

                s += "\n";
                s += "#region pannel";
                s += "\n";
                s += "  //����һ�㲻��Ҫ���ģ��Ƕ����ֶ�ǰ������";
                s += "\n";
                s += "  private void SetText()";
                s += "\n";
                s += "  {";
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Select(""))
                        {
                            s += "\n";
                            s += "      lbl" + dr["ColumnName"] + ".Text = TextFromSysTableColumn(\"" + dr["ColumnName"] + "\");";
                        }
                    }
                }


                s += "\n";
                s += "      lblTitle.Text = TextFromSysTable(\"" + tableName + "\");";
                s += "\n";
                s += "      lblBigTitle.Text = TextFromSysTable(\"" + tableName + "\");";

                s += "\n";
                s += "      lblPageSize.Text = ResourceManager.GetString(\"lblPageSize\");";
                s += "\n";
                s += "lblSearchTitle.Text = ResourceManager.GetString(\"lblSearchTitle\");";

                s += "\n";
                s += "  }";
                s += "\n";
                s += "  //������ģ���Ϊ��page_Load�����Ѿ����������ݱ�����";
                s += "\n";
                s += "  private string TextFromSysTable(string TableName)";
                s += "\n";
                s += "  {";
                s += "\n";
                s += "      if (ViewState[\"sysTable\"] == null)";
                s += "\n";
                s += "      {";
                s += "\n";
                s += "          return \"\";";
                s += "\n";
                s += "      }";
                s += "\n";
                s += "      string strResult = \"\";";
                s += "\n";
                s += "      DataTable dt = (DataTable)ViewState[\"sysTable\"];";
                s += "\n";
                s += "      string filer = \"TableName='\" + TableName + \"'\";";

                s += "\n";
                s += "DataRow[] dr = dt.Select(filer);";
                s += "\n";
                s += "      if (dr.Length > 0)";
                s += "\n";
                s += "      {";
                s += "\n";
                s += "          switch (ForumUtils.GetCookie(\"Language\"))";
                s += "\n";
                s += "          {";
                s += "\n";
                s += "              case \"zh-CN\":";
                s += "\n";
                s += "                  strResult = dr[0][\"TableDescription\"].ToString();";
                s += "\n";
                s += "                  break;";
                s += "\n";
                s += "              case \"en-US\":";
                s += "\n";
                s += "                  strResult = dr[0][\"TableDescriptionEN\"].ToString();";
                s += "\n";
                s += "                  break;";
                s += "\n";
                s += "              case \"zh-TW\":";
                s += "\n";
                s += "                  strResult = dr[0][\"TableDescriptionTW\"].ToString();";
                s += "\n";
                s += "                  break;";
                s += "\n";
                s += "              default:";
                s += "\n";
                s += "                  strResult = dr[0][\"TableDescription\"].ToString();";
                s += "\n";
                s += "                  break;";
                s += "\n";
                s += "          }";
                s += "\n";
                s += "      }";
                s += "\n";
                s += "      return strResult;";
                s += "\n";
                s += "  }";
                s += "\n";
                s += "  //������ģ���Ϊ��page_Load�����Ѿ����������ݱ�����";
                s += "\n";
                s += "  private string TextFromSysTableColumn(string ColName)";
                s += "\n";
                s += "  {";
                s += "\n";
                s += "      if (ViewState[\"sysTableColumn\"] == null)";
                s += "\n";
                s += "      {";
                s += "\n";
                s += "          return \"\";";
                s += "\n";
                s += "      }";
                s += "\n";
                s += "      string strResult = \"\";";

                s += "\n";
                s += "DataTable dt = (DataTable)ViewState[\"sysTableColumn\"];";
                s += "\n";
                s += "      string filer = \"ColName='\" + ColName + \"'\";";

                s += "\n";
                s += "      DataRow[] dr = dt.Select(filer);";
                s += "\n";
                s += "      if (dr.Length > 0)";
                s += "\n";
                s += "      {";
                s += "\n";
                s += "          switch (ForumUtils.GetCookie(\"Language\"))";
                s += "\n";
                s += "          {";
                s += "\n";
                s += "              case \"zh-CN\":";
                s += "\n";
                s += "                  strResult = dr[0][\"ColDescriptionCN\"].ToString();";
                s += "\n";
                s += "                  break;";
                s += "\n";
                s += "              case \"en-US\":";
                s += "\n";
                s += "                  strResult = dr[0][\"ColDescriptionUS\"].ToString();";
                s += "\n";
                s += "                  break;";
                s += "\n";
                s += "              case \"zh-TW\":";
                s += "\n";
                s += "                  strResult = dr[0][\"ColDescriptionTW\"].ToString();";
                s += "\n";
                s += "                  break;";
                s += "\n";
                s += "              default:";
                s += "\n";
                s += "                  strResult = dr[0][\"ColDescriptionCN\"].ToString();";
                s += "\n";
                s += "                  break;";
                s += "\n";
                s += "          }";
                s += "\n";
                s += "      }";

                s += "\n";
                s += "      return strResult;";
                s += "\n";
                s += "  }";
                s += "\n";
                s += "  //����Ҫ���ģ���Ϊֻ������TextBoxֵ�ĸ�ֵ����";
                s += "\n";
                s += "  private void SetPannelData(" + tableName + "Entity _" + tableName + "Entity)";
                s += "\n";
                s += "  {";
                s += "\n";
                s += "      //BindDropDownList(_psacnEntity.PSACNDAT);";
                // DataTable dt = DbHelper.GetInstance().GetSysTableColumnByTableName(tableName, "");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Select(""))
                        {
                            s += "\n";
                            s += "if(_" + tableName + "Entity." + dr["ColumnName"] + "!=null)";
                            s += "      txt" + dr["ColumnName"] + ".Text = _" + tableName + "Entity." + dr["ColumnName"] + ".ToString(); //" + dr["ColDescriptionCN"];
                        }
                    }
                }
                s += "\n";
                s += "      //Page.ClientScript.RegisterStartupScript(this.GetType(), \"setData\", \"<script language=\\\"javascript\\\" type=\\\"text/javascript\\\">document.getElementById('txtPSACNCO').value='\" + _" + tableName + "Entity.PSACNCO + \"';</script>\");";
                s += "\n";
                s += "      //ViewState[\"state\"] = \"Update\";";
                s += "\n";
                s += "      strOperationState = \"Update\";";
                s += "\n";
                s += "  }";
                s += "\n";
                s += "  //������Ҫ��dropdownlist�ĳ�ʼ��";
                s += "\n";
                s += "  private void BindDropDownList(string selectValue)";
                s += "\n";
                s += "  {";
                s += "\n";
                s += "      //DataTable dt_DateType = DateTypeDataTable();";
                s += "\n";
                s += "      //ViewState[\"PSACNDATtable\"] = dt_DateType;";
                s += "\n";
                s += "      //ddlPSACNDAT.AddTableData(dt_DateType, selectValue);";

                s += "\n";
                s += "}";
                s += "\n";
                s += "  //����Ϊdropdownlist����ġ���Ϊ��Щ����Դû��д�뵽���ݿ��У����ǱȽϹ̶������ݡ�";
                s += "\n";
                s += "  private DataTable DateTypeDataTable()";
                s += "\n";
                s += "  {";
                s += "\n";
                s += "      DataTable dt_ChkList = new DataTable();";

                s += "\n";
                s += "            return dt_ChkList;";
                s += "\n";
                s += "  }";
                s += "\n";
                s += "  //����һ�㲻��Ҫ����,��Ϊ����Ĺ���ȫ�����������SaveData��";
                s += "\n";
                s += "  protected void hideModalPopupViaServer_Click(object sender, EventArgs e)";
                s += "\n";
                s += "  {   ";
                s += "\n";
                s += "      GPRP.GPRPControls.Button btn = (GPRP.GPRPControls.Button)sender;";
                s += "\n";
                s += "      if (btn.ID == \"CancelButton\")";
                s += "\n";
                s += "      {";
                s += "\n";
                s += "          //hide";
                s += "\n";
                s += "          this.programmaticModalPopup.Hide();";
                s += "\n";
                s += "      }";
                s += "\n";
                s += "      else";
                s += "\n";
                s += "      {";
                s += "\n";
                s += "          //����";
                s += "\n";
                s += "          string sResult = SaveData();";
                s += "\n";
                s += "          if (sResult == \"-1\")";
                s += "\n";
                s += "          {";
                s += "\n";
                s += "              lblMsg.Text = ResourceManager.GetString(\"Operation_RECORD\");";
                s += "\n";
                s += "          }";
                s += "\n";
                s += "          else";
                s += "\n";
                s += "          {";
                s += "\n";
                s += "              //refresh gridview";
                s += "\n";
                s += "              GridView1EditUI(BrowseEditMode.Edit);//";
                s += "\n";
                s += "              BindGridView();";

                s += "\n";
                s += "                  if (btn.ID == \"btnSubmitAndClose\")";
                s += "\n";
                s += "                  {";
                s += "\n";
                s += "                      //hide";
                s += "\n";
                s += "                      this.programmaticModalPopup.Hide();";
                s += "\n";
                s += "                  }";
                s += "\n";
                s += "                  else";
                s += "\n";
                s += "                  {";
                s += "\n";
                s += "                      if (strOperationState == \"Update\")";
                s += "\n";
                s += "                      {";
                s += "\n";
                s += "                      }";
                s += "\n";
                s += "                      else";
                s += "\n";
                s += "                      {";
                s += "\n";
                s += "                          //continue add";
                s += "\n";
                s += "                          //����Ӧ����գ�������Ӧ�����";
                s += "\n";
                s += "                          txtPSACNCO.Text = \"\";";
                s += "\n";
                s += "                      }";

                s += "\n";
                s += "                  }";


                s += "\n";
                s += "          }";
                s += "\n";
                s += "      }";
                s += "\n";
                s += "      Page.ClientScript.RegisterStartupScript(this.GetType(), btnEditMode.ClientID, \"<script>document.getElementById('success').style.display = \\\"none\\\";</script>\");";

                s += "\n";
                s += "  }";
                s += "\n";
                s += "  //����Ҫ����,��ɸ�ֵ����";
                s += "\n";
                s += "  private string SaveData()";
                s += "\n";
                s += "  {";
                s += "\n";
                s += "      " + tableName + "Entity _" + tableName + "Entity = new " + tableName + "Entity();";
                // DataTable dt = DbHelper.GetInstance().GetSysTableColumnByTableName(tableName, "");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Select(""))
                        {
                            s += "\n";
                            switch (dr["TypeName"].ToString())
                            {
                                case "varchar":
                                    s += "      _" + tableName + "Entity." + dr["ColumnName"] + " = txt" + dr["ColumnName"] + ".Text;";
                                    break;
                                case "char":
                                    s += "      _" + tableName + "Entity." + dr["ColumnName"] + " = txt" + dr["ColumnName"] + ".Text;";
                                    break;
                                case "int":
                                    s += "      _" + tableName + "Entity." + dr["ColumnName"] + " = Convert.ToInt32(txt" + dr["ColumnName"] + ".Text);";
                                    break;
                                default:
                                    s += "      _" + tableName + "Entity." + dr["ColumnName"] + " = txt" + dr["ColumnName"] + ".Text;";
                                    break;
                            };

                        }
                    }
                }

                s += "\n";
                s += "      string sResult = \"-1\";";
                s += "\n";
                s += "      if (strOperationState == \"Add\")";
                s += "\n";
                s += "          sResult = DbHelper.GetInstance().Add" + tableName + "(_" + tableName + "Entity);";
                s += "\n";
                s += "      else if (strOperationState == \"Update\")";
                s += "\n";
                s += "          sResult = DbHelper.GetInstance().Update" + tableName + "(_" + tableName + "Entity);";

                s += "\n";
                s += "      return sResult;";

                s += "\n";
                s += "  }";
                s += "\n";
                s += "  #endregion";
                s += "\n";
                s += "  [WebMethod]";
                s += "\n";
                s += "  public static string SetAddViewState()";
                s += "\n";
                s += "  {";
                s += "\n";
                s += "      strOperationState = \"Add\";";
                s += "\n";
                s += "      return DbHelper.GetInstance().px_Sequence(\"" + tableName + "CODE\", \"0\");";
                s += "\n";
                s += "  }";
                s += "\n";
                s += "  [WebMethod]";
                s += "\n";
                s += "  public static void WriteCookies(string Fixresult,string MergeResult)";
                s += "\n";
                s += "  { ";

                s += "\n";
                s += "  }";
                s += "\n";
                s += "  //������Ҫ�ǹ������Ĳ�������";
                s += "\n";
                s += "  protected void btnBrowseMode_Click(object sender, EventArgs e)";
                s += "\n";
                s += "  {";

                s += "\n";
                s += "      GPRP.GPRPControls.Button btn = (GPRP.GPRPControls.Button)sender;";

                s += "\n";
                s += "      //UCOperationBanner1.ButtnEnable(ClientID);";

                s += "\n";
                s += "      switch (btn.ID)";
                s += "\n";
                s += "      {";
                s += "\n";
                s += "          case \"btnBrowseMode\":";
                s += "\n";
                s += "              ViewState[\"Mode\"] = BrowseEditMode.Browse;";
                s += "\n";
                s += "              GridView1.Visible = true;";
                s += "\n";
                s += "              GridView2.Visible = false;";
                s += "\n";
                s += "              break;";
                s += "\n";
                s += "          case \"btnEditMode\":";
                s += "\n";
                s += "              ViewState[\"Mode\"] = BrowseEditMode.Edit;";
                s += "\n";
                s += "              GridView1EditUI(BrowseEditMode.Edit);//";
                s += "\n";
                s += "              BindGridView();";
                s += "\n";
                s += "              GridView1.Visible = false;";
                s += "\n";
                s += "              GridView2.Visible = true;";
                s += "\n";
                s += "              break;";
                s += "\n";
                s += "          case \"btnSubmit\":";

                s += "\n";
                s += "              break;";
                s += "\n";
                s += "          case \"btnDel\":";
                s += "\n";
                s += "              BrowseEditMode mode = ((BrowseEditMode)ViewState[\"Mode\"]);";
                s += "\n";
                s += "              if (mode == BrowseEditMode.Browse)";
                s += "\n";
                s += "              {";
                s += "\n";
                s += "                  string sID = \"\";";
                s += "\n";
                s += "                  for (int i = 0; i < GridView1.Rows.Count; i++)";
                s += "\n";
                s += "                  {";
                s += "\n";
                s += "                      GridViewRow row = GridView1.Rows[i];";
                s += "\n";
                s += "                      CheckBox check = (CheckBox)row.FindControl(\"item\");";
                s += "\n";
                s += "                      if (check.Checked)";
                s += "\n";
                s += "                      {";
                s += "\n";
                s += "                          sID += row.Cells[4].Text + \",\";";
                s += "\n";
                s += "                      }";
                s += "\n";
                s += "                  }";
                s += "\n";
                s += "                  if (sID.Length > 0)";
                s += "\n";
                s += "                  {";
                s += "\n";
                s += "                      //delete the record that you  choose";
                s += "\n";
                s += "                      DbHelper.GetInstance().Delete" + tableName + "(sID);";
                s += "\n";
                s += "                      //refresh the edit mode gridview ui";
                s += "\n";
                s += "                      GridView1EditUI(BrowseEditMode.Edit);//";
                s += "\n";
                s += "                      //refresh the datasource.";
                s += "\n";
                s += "                      BindGridView();";
                s += "\n";
                s += "                  }";
                s += "\n";
                s += "              }";
                s += "\n";
                s += "              break;";
                s += "\n";
                s += "          case \"btnImportExcel\":";
                s += "\n";
                s += "              //word";
                s += "\n";
                s += "              break;";
                s += "\n";
                s += "          case \"btnExportExcel\":";
                s += "\n";
                s += "              //excel";
                s += "\n";
                s += "              break;";
                s += "\n";
                s += "      }";

                s += "\n";
                s += "        }";

                s += "\n";
                s += " }";
                s += "\n";
                s += "}";
                TextBox2.Text = s;

            }
            else
            {
                TextBox2.Text = "";
            }
        }
        protected void Button_AddEntity_Click(object sender, EventArgs e)
        {
            string tableName = TextBox1.Text;
            s = "";
            if (tableName != "")
            {
                DataTable dt = DbHelper.GetInstance().GetSysTableColumnByTableName(tableName, "","1");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        s += "\n";
                        s += "        /// <summary>";
                        s += "\n";
                        s += "        /// ������Ϣ";
                        s += "\n";
                        s += "        /// </summary>";
                        s += "\n";
                        s += "        /// <param name=\"_" + tableName + "Entity\"></param>";
                        s += "\n";
                        s += "        /// <returns>����string \"-1\"��ʾ���Ѿ����ڣ�����ɹ� </returns>";
                        s += "\n";
                        s += "        public string Add" + tableName + "(" + tableName + "Entity _" + tableName + "Entity)";
                        s += "\n";
                        s += "        {";
                        s += "\n";
                        s += "            //�жϸü�¼�Ƿ��Ѿ�����";
                        s += "\n";
                        s += "            DbParameter[] prams = { DbHelper.MakeInParam(\"@\",(DbType)SqlDbType.����,����,_" + tableName + "Entity.�ֶ�),";
                        s += "\n";
                        s += "                                     };";
                        s += "\n";
                        s += "            string sql = \"sql \";";
                        s += "\n";
                        s += "            if (DbHelper.ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)";
                        s += "\n";
                        s += "            {";
                        s += "\n";
                        s += "                return \"-1\";//�ü�¼�Ѿ�����";
                        s += "\n";
                        s += "            }";
                        s += "\n";
                        s += "            else";
                        s += "\n";
                        s += "            {";
                        s += "\n";
                        s += "             _" + tableName + "Entity.PBEWTTC = px_Sequence(\"" + tableName + "CODE\", \"1\");";
                        s += "\n";
                        s += "                DbParameter[] pramsInsert = {";
                        foreach (DataRow dr in dt.Select(""))
                        {
                            s += "\n";
                            switch (dr["TypeName"].ToString())
                            {
                                case "varchar":
                                    s += "									   DbHelper.MakeInParam(\"@" + dr["ColumnName"] + "\",(DbType)SqlDbType.VarChar," + dr["Prec"] + ",_" + tableName + "Entity." + dr["ColumnName"] + " ),";
                                    break;
                                case "char":
                                    s += "									   DbHelper.MakeInParam(\"@" + dr["ColumnName"] + "\",(DbType)SqlDbType.Char," + dr["Prec"] + ",_" + tableName + "Entity." + dr["ColumnName"] + " ),";
                                    break;
                                case "int":
                                    s += "									   DbHelper.MakeInParam(\"@" + dr["ColumnName"] + "\",(DbType)SqlDbType.Int,4,_" + tableName + "Entity." + dr["ColumnName"] + " ),";
                                    break;
                                default:
                                    s += "									   DbHelper.MakeInParam(\"@" + dr["ColumnName"] + "\",(DbType)SqlDbType." + dr["TypeName"] + "," + dr["Prec"] + ",_" + tableName + "Entity." + dr["ColumnName"] + " ),";
                                    break;
                            };

                        }
                        s += "\n";
                        s += "                                             };";
                        s += "\n";
                        s += "                StringBuilder sb = new StringBuilder();";
                        s += "\n";
                        s += "                sb.Append(\"INSERT INTO [dbo].[" + tableName + "]\");";
                        s += "\n";
                        s += "                sb.Append(\"(\");";
                        foreach (DataRow dr in dt.Select(""))
                        {
                            s += "\n";
                            s += "                sb.Append(\",[" + dr["ColumnName"] + "]\");";
                        }
                        s += "\n";
                        s += "                sb.Append(\") \");";
                        s += "\n";
                        s += "                sb.Append(\" VALUES (\");";

                        foreach (DataRow dr in dt.Select(""))
                        {
                            s += "\n";
                            s += "                sb.Append(\"@" + dr["ColumnName"] + ",\");";
                        }
                        s += "\n";
                        s += "                sb.Append(\"select @@identity;\");";
                        s += "\n";
                        s += "                return DbHelper.ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();";
                        s += "\n";
                        s += "            }";
                        s += "\n";
                        s += "       }";

                        TextBox2.Text = s;

                    }
                    else
                    {
                        TextBox2.Text = "";
                    }
                }
            }
        }

        protected void Button_UpdateEntity_Click(object sender, EventArgs e)
        {
            string tableName = TextBox1.Text;
            s = "";
            if (tableName != "")
            {
                DataTable dt = DbHelper.GetInstance().GetSysTableColumnByTableName(tableName, "","1");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        s += "\n";
                        s += "        /// <summary>";
                        s += "\n";
                        s += "        /// �޸���Ϣ";
                        s += "\n";
                        s += "        /// </summary>";
                        s += "\n";
                        s += "        /// <param name=\"_" + tableName + "Entity\"></param>";
                        s += "\n";
                        s += "        /// <returns>����string \"-1\"��ʾ���Ѿ����ڣ�����ɹ� </returns>";
                        s += "\n";
                        s += "        public string Update" + tableName + "(" + tableName + "Entity _" + tableName + "Entity)";
                        s += "\n";
                        s += "        {";
                        s += "\n";
                        s += "                DbParameter[] pramsUpdate = {";
                        foreach (DataRow dr in dt.Select(""))
                        {
                            s += "\n";
                            switch (dr["TypeName"].ToString())
                            {
                                case "varchar":
                                    s += "									   DbHelper.MakeInParam(\"@" + dr["ColumnName"] + "\",(DbType)SqlDbType.VarChar," + dr["Prec"] + ",_" + tableName + "Entity." + dr["ColumnName"] + " ),";
                                    break;
                                case "char":
                                    s += "									   DbHelper.MakeInParam(\"@" + dr["ColumnName"] + "\",(DbType)SqlDbType.Char," + dr["Prec"] + ",_" + tableName + "Entity." + dr["ColumnName"] + " ),";
                                    break;
                                case "int":
                                    s += "									   DbHelper.MakeInParam(\"@" + dr["ColumnName"] + "\",(DbType)SqlDbType.Int,4,_" + tableName + "Entity." + dr["ColumnName"] + " ),";
                                    break;
                                default:
                                    s += "									   DbHelper.MakeInParam(\"@" + dr["ColumnName"] + "\",(DbType)SqlDbType." + dr["TypeName"] + "," + dr["Prec"] + ",_" + tableName + "Entity." + dr["ColumnName"] + " ),";
                                    break;
                            };

                        }
                        s += "\n";
                        s += "                                             };";
                        s += "\n";
                        s += "                StringBuilder sb = new StringBuilder();";
                        s += "\n";
                        s += "                sb.Append(\"Update [dbo].[" + tableName + "]\");";
                        s += "\n";
                        s += "                sb.Append(\" set \");";
                        foreach (DataRow dr in dt.Select(""))
                        {
                            s += "\n";
                            s += "                sb.Append(\" [" + dr["ColumnName"] + "]=@" + dr["ColumnName"] + ",\");";
                        }

                        s += "\n";
                        s += "                sb.Append(\" where \");";
                        s += "\n";
                        s += "                return DbHelper.ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpdate).ToString();";
                        s += "\n";
                        s += "       }";

                        TextBox2.Text = s;

                    }
                    else
                    {
                        TextBox2.Text = "";
                    }
                }
            }
        }

        protected void Button_GetEntity_Click(object sender, EventArgs e)
        {
            string tableName = TextBox1.Text;
            s = "";
            if (tableName != "")
            {
                DataTable dt = DbHelper.GetInstance().GetSysTableColumnByTableName(tableName, "","1");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        s += "\n";
                        s += "  /// <summary>";
                        s += "\n";
                        s += "  /// ����ʵ������";
                        s += "\n";
                        s += "        /// </summary>";
                        s += "\n";
                        s += "        /// <param name=\"KeyCol\"></param>";
                        s += "\n";
                        s += "        /// <returns></returns>";
                        s += "\n";
                        s += "        public " + tableName + "Entity Get" + tableName + "EntityByKeyCol(string KeyCol)";
                        s += "\n";
                        s += "        {";
                        s += "\n";
                        s += "            string sql = \"select * from  Where =@KeyCol\";";
                        s += "\n";
                        s += "            DbParameter[] pramsGet = {";
                        s += "\n";
                        s += "									   DbHelper.MakeInParam(\"@KeyCol\",(DbType)SqlDbType.VarChar,50,KeyCol ),";
                        s += "\n";
                        s += "                                             };";
                        s += "\n";
                        s += "            DbDataReader sr = null;";
                        s += "\n";
                        s += "            try";
                        s += "\n";
                        s += "            {";
                        s += "\n";
                        s += "                sr = DbHelper.ExecuteReader(CommandType.Text, sql, pramsGet);";
                        s += "\n";
                        s += "                if (sr.Read())";
                        s += "\n";
                        s += "                {";
                        s += "\n";
                        s += "                    return Get" + tableName + "FromIDataReader(sr);";
                        s += "\n";
                        s += "                }";
                        s += "\n";
                        s += "                throw new Exception(ResourceManager.GetString(\"CANNOT_FIND_RECORD\"));";
                        s += "\n";
                        s += "            }";
                        s += "\n";
                        s += "            catch (Exception exp)";
                        s += "\n";
                        s += "            {";
                        s += "\n";
                        s += "                throw exp;";
                        s += "\n";
                        s += "            }";
                        s += "\n";
                        s += "            finally";
                        s += "\n";
                        s += "            {";
                        s += "\n";
                        s += "                if (sr != null)";
                        s += "\n";
                        s += "                    sr.Close();";
                        s += "\n";
                        s += "            }";
                        s += "\n";
                        s += "        }";
                        s += "\n";
                        s += "        private " + tableName + "Entity Get" + tableName + "FromIDataReader(DbDataReader dr)";
                        s += "\n";
                        s += "        {";
                        s += "\n";
                        s += "            " + tableName + "Entity dt = new " + tableName + "Entity();";
                        s += "\n";
                        s += "            if (dr.FieldCount>0)";
                        s += "\n";
                        s += "            {";

                        foreach (DataRow dr in dt.Select(""))
                        {
                            s += "\n";
                            switch (dr["TypeName"].ToString())
                            {
                                case "varchar":
                                    s += "                dt." + dr["ColumnName"] + " = dr[\"" + dr["ColumnName"] + "\"].ToString();";
                                    break;
                                case "char":
                                    s += "                dt." + dr["ColumnName"] + " = dr[\"" + dr["ColumnName"] + "\"].ToString();";
                                    break;
                                case "int":
                                    s += "                if(dr[\"" + dr["ColumnName"] + "\"].ToString() !=\"\" || dr[\"" + dr["ColumnName"] + "\"] !=null) dt." + dr["ColumnName"] + " = Int32.Parse(dr[\"" + dr["ColumnName"] + "\"].ToString());";
                                    break;
                                default:
                                    s += "                dt." + dr["ColumnName"] + " = dr[\"" + dr["ColumnName"] + "\"].ToString();";
                                    break;
                            };

                        }
                        s += "\n";
                        s += "                dr.Close();";
                        s += "\n";
                        s += "                return dt;";
                        s += "\n";
                        s += "            }";
                        s += "\n";
                        s += "            dr.Close();";
                        s += "\n";
                        s += "            return null;";
                        s += "\n";
                        s += "        }";

                        TextBox2.Text = s;

                    }
                    else
                    {
                        TextBox2.Text = "";
                    }
                }
            }
        }
        protected void hideModalPopupViaServer_Click(object sender, EventArgs e)
        {


            string tableName = TextBox1.Text;
            s = "";
            if (tableName != "")
            {
                DataTable dt = DbHelper.GetInstance().GetSysTableColumnByTableName(tableName, "","1");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        s += "\n";
                        s += "using System;";
                        s += "\n";
                        s += "using System.Collections.Generic;";
                        s += "\n";
                        s += "using System.Text;";
                        s += "\n";
                        s += "namespace GPRP.Entity";
                        s += "\n";
                        s += "{";
                        s += "\n";
                        s += "   /// <summary>";
                        s += "\n";
                        s += "   /// ʵ��";
                        s += "\n";
                        s += "   /// </summary>";
                        s += "\n";
                        s += "   public class " + tableName + "Entity";
                        s += "\n";
                        s += "  {";
                        s += "\n";
                        foreach (DataRow dr in dt.Select(""))
                        {
                            s += "\n";
                            switch (dr["TypeName"].ToString())
                            {
                                case "varchar":
                                    s += "        private string m_" + dr["ColumnName"] + ";//" + dr["ColDescriptionCN"];
                                    break;
                                case "int":
                                    s += "        private int m_" + dr["ColumnName"] + ";//" + dr["ColDescriptionCN"];
                                    break;
                                case "numeric":
                                    s += "        private decimal m_" + dr["ColumnName"] + ";//" + dr["ColDescriptionCN"];
                                    break;
                                case "datetime":
                                    s += "        private DateTime m_" + dr["ColumnName"] + ";//" + dr["ColDescriptionCN"];
                                    break;
                                default:
                                    s += "        private string m_" + dr["ColumnName"] + ";//" + dr["ColDescriptionCN"];
                                    break;

                            };

                        }

                        foreach (DataRow dr in dt.Select(""))
                        {
                            s += "\n";
                            s += "        /// <summary>";
                            s += "\n";
                            s += "        ///" + dr["ColDescriptionCN"];
                            s += "\n";
                            s += "        /// </summary>";
                            s += "\n";
                            switch (dr["TypeName"].ToString())
                            {
                                case "varchar":
                                    s += "        public string " + dr["ColumnName"];
                                    break;
                                case "int":
                                    s += "        public int " + dr["ColumnName"];
                                    break;
                                case "numeric":
                                    s += "        public decimal " + dr["ColumnName"] ;
                                    break;
                                case "datetime":
                                    s += "        public DateTime " + dr["ColumnName"] ;
                                    break;
                                default:
                                    s += "        public string " + dr["ColumnName"];
                                    break;

                            };
                            s += "\n";
                            s += "        {";
                            s += "\n";
                            s += "             get { return m_" + dr["ColumnName"] + "; }";
                            s += "\n";
                            s += "             set { m_" + dr["ColumnName"] + " = value;}";
                            s += "\n";
                            s += "        }";
                        }
                        s += "\n";
                        s += "  }";
                        s += "\n";
                        s += "}";

                        TextBox2.Text = s;
                    }
                    else
                    {
                        TextBox2.Text = "";
                    }

                }


            }



        }
    }
}
