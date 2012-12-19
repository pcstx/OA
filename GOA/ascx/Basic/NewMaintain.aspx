<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewMaintain.aspx.cs" Inherits="GOA.Basic.NewMaintain" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    	    <link href="../styles/StyleSheet.css" type="text/css" rel="stylesheet" />
    	    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>
    	    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>
            <script language="javascript" type="text/javascript" src="../JScript/jquery-latest.pack.js"></script>
              <script language="javascript" type="text/javascript">
                  function pageLoad() {

                      $addHandler($get("btnSearch"), 'click', showWriteModalPopup);


                  }
                  function showWriteModalPopup(ev) {
                      ev.preventDefault();
                      var modalPopupBehavior = $find('PopupBehavior');
                      modalPopupBehavior.show();
                  }

                 
              </script>
</head>
<body>
     <form id="form1" runat="server">
        <cc1:toolkitscriptmanager id="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
        </cc1:toolkitscriptmanager>
        
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          
            <ContentTemplate>
             <div >
                <cc2:Button ID="btnAdd" runat="server" ButtonImgUrl="../images/query.gif" Enabled="false"  ShowPostDiv="false" Text="Button_MoveIn" ValidateForm="false" AutoPostBack="true" OnClick="btnAdd_Click"/>
	            <cc2:Button ID="btnDelet" runat="server" ShowPostDiv="false" ValidateForm="false" AutoPostBack="true" Disable="false" Text="Button_MoveOut"  OnClick="btnDelet_Click"/>
                <cc2:Button ID="btnSearch" runat="server" ShowPostDiv="false" ValidateForm="false" AutoPostBack="true" Disable="false" Text="Button_Search"/>
            </div>
           <div class="ManagerForm">
         	<fieldset>		
		      <legend style="background:url(../images/legendimg.jpg) no-repeat 6px 50%;"><asp:Label ID="lblBigTitle" runat="server" Text=""></asp:Label>
		      </legend>

          <!--page start-->
          <div id="pager" >
            <div class="PagerArea" >
               <webdiyer:AspNetPager ID="AspNetPager1" runat="server"  OnPageChanging="AspNetPager1_PageChanging" onpagechanged="AspNetPager1_PageChanged"  Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Page: " 
                 HorizontalAlign="left"  CenterCurrentPageButton="true" NumericButtonCount="9">
               </webdiyer:AspNetPager>
            </div>
            <div class="PagerText">
                    <asp:Label ID="lblPageSize" runat="server" Text=""></asp:Label>&nbsp;
                       <cc2:TextBox ID="txtPageSize" runat="server" Width="35" TextMode="SingleLine" SetFocusButtonID="btnPageSize"  ></cc2:TextBox>
                       <cc2:Button ID="btnPageSize" Text="Button_GO" runat="server" Width="60" OnClick="txtPageSize_TextChanged" ShowPostDiv="true"/>
             </div>
          </div>
             
                 
           
           
           <cc1:FilteredTextBoxExtender  ID="FilteredTextBoxExtender2" runat="server" 
                    TargetControlID="txtPageSize"
                    FilterType="Numbers"/>   
                                
            <!--page end-->
            <!--gridview start-->
                <!--gridview Browse mode start-->
             <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="Title,Isp,ID">
                    <Columns>
                      <asp:TemplateField>
                        <HeaderTemplate>
                          序号
                        </HeaderTemplate>
                        <ItemTemplate>
                          <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <ItemStyle Width="35px" />
                      </asp:TemplateField>
                      
                      <asp:TemplateField>
                        <HeaderTemplate>
                          选择
                          </HeaderTemplate>
                        <ItemTemplate>
                          <asp:CheckBox ID="Item" runat="server" />
                        </ItemTemplate>
                        <ItemStyle Width="35px" />
                      </asp:TemplateField>
                       <asp:BoundField DataField="Title" HeaderText="信息标题" />
                        <asp:BoundField DataField="TypeDesc" HeaderText="信息类别" />
                       <asp:BoundField DataField="Isp" HeaderText="是否发布" />
                          <asp:BoundField DataField="Date" HeaderText="过期日期" />
                         
                    </Columns>
                    <CascadeCheckboxes>
                      <yyc:CascadeCheckbox ChildCheckboxID="item" ParentCheckboxID="all" />
                    </CascadeCheckboxes>
                    <CheckedRowCssClass CheckBoxID="Item" CssClass="SelectedRow" />
                    <ContextMenus>
                      <yyc:ContextMenu Text="版权所有" NavigateUrl="http://www.geobyev.com" Target="_blank" />
                    </ContextMenus>
                  </yyc:SmartGridView>
             
		</fieldset>
     </div> 
    
        </ContentTemplate>
     </asp:UpdatePanel>
    
      <!--  Hint  start -->
     <cc2:Hint id="Hint1" runat="server" HintImageUrl="../images" WidthUp="270"></cc2:Hint>
      <!--  Hint  end -->
     <cc1:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="UpdatePanel1">
        </cc1:UpdatePanelAnimationExtender> 
        
      
 <asp:Button runat="server" ID="hiddenTargetControl" style="display:none"/>
  <cc1:ModalPopupExtender runat="server" ID="showWriteModalPopup"
      BehaviorID="PopupBehavior"
      TargetControlID="hiddenTargetControl"
      PopupControlID="PopupShow" 
      BackgroundCssClass="modalBackground"
      DropShadow="False"
      CancelControlID="CancelButton"
      PopupDragHandleControlID="PopupDragHandle"
      RepositionMode="RepositionOnWindowScroll" >
  </cc1:ModalPopupExtender>  
        
  <asp:Panel runat="server" CssClass="modalPopup" ID="PopupShow" style="display:none;width:650px;  padding:0 0px 0px 0px">
      <div id="PopupDragHandle" class="programmaticPopupDragHandle">
          <div class="h2blk">
        <h2 id="H2_2">
          <asp:Label ID="lblSearchTitle" runat="server" Text="搜索框"></asp:Label></h2>
      </div>
        <cc2:Button ID="MaxButton" ScriptContent="MaxQueryFrom();" ShowPostDiv="false" runat="server" Text="Label_NULL"  CSS="MaxButtonCSS" AutoPostBack="false" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="true" ToolTip="Max" Width="12" />
		  <cc2:Button ID="CancelButton" ShowPostDiv="false" runat="server" Text="Label_NULL"  CSS="CancelButtonCSS" AutoPostBack="false" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="false" ToolTip="Close" Width="12" />
       </div>
       
       <div>
                <asp:UpdatePanel ID="UpdatePanelSendMail" runat="server">
                   <ContentTemplate> 
                       <div class="">
                           <cc2:Button ID="btnFind"  runat="server"   ShowPostDiv="false" ValidateForm="false" AutoPostBack="true" Disable="false" Text="Button_Search" OnClick="btnFind_Click"/>
                          
                    </div> 
                    
		             <div>
		
		               <div class="clear">
                              <label class="char5">
                                    <asp:Label ID="lblTitle" runat="server" Text="信息标题"></asp:Label>
                               </label>
                               <div class="iptblk">
                                    <cc2:TextBox ID="txtTitle" runat="server" Width="400" ></cc2:TextBox>
                               </div>
                       </div>
                       
                       <div class="clear">
                              <label class="char5">
                                    <asp:Label ID="lblType" runat="server" Text="信息类别"></asp:Label>
                               </label>
                               <div class="iptblk">
                                    <cc2:DropDownList ID="dpType" runat="server" Width="132" Enabled="true"></cc2:DropDownList>
                               </div>
                       </div>
                       
                
		             
		               <div class="clear">
                              <label class="char5">
                                    <asp:Label ID="lblIsPublish" runat="server" Text="是否发布"></asp:Label>
                               </label>
                               <div class="iptblk">
                                    <input type="checkbox" id="chkIsPublish" runat="server"  checked="checked">
                               </div>
                       </div>

		             </div>
                   </ContentTemplate>
                 </asp:UpdatePanel>
       
       </div>
     </asp:Panel>  
    </form>
</body>
</html>
