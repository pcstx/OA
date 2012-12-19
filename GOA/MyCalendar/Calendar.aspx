<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="GOA.MyCalendar.Calendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function PostValue(UserListName) {

            left.window.SetValue(UserListName);
        }


        function showUserListModalPopup() {
            var UserListPopupBehavior = $find('UserListPopupBehavior');
            UserListPopupBehavior.show();
        }
        
        
    </script>
</head>
<body STYLE='OVERFLOW:SCROLL;OVERFLOW-Y:HIDDEN'>
    <form id="form1" runat="server">
       <cc1:toolkitscriptmanager id="ToolkitScriptManager1" runat="server" EnablePageMethods="True" EnablePartialRendering="true">
        </cc1:toolkitscriptmanager>
          <asp:UpdatePanel ID="UpdatePanel" runat="server"  UpdateMode="Conditional">
                   <ContentTemplate> 
    <div>
      <table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%">
      <tr>
        <td valign="top" align="center" nowrap id="frmTitle">
            <iframe frameborder="0" id="left" name="left" scrolling="no"  style="width: 300px; height: 700px;" src="jscalendar/LeftCalendar.aspx"></iframe>
        </td>
       
      <td width="768" style="width: 100%; height: 650px; vertical-align: top; padding: 5px 10px 5px 10px; overflow:auto ">
           <div id="loadMsg" style=" position: relative; border: 1px dotted #DBDDD3; background-color: #FDFFF2; margin: auto; padding: 10px" width="90%">
             <iframe frameborder="0" runat="server" id="right" name="right" scrolling="auto" width="100%" height="630px" src="index.html" ></iframe>
           </div>
     </td>
      </tr>
    </table>
   </div>
   
   </ContentTemplate>
   </asp:UpdatePanel>
    
    
     <asp:Button runat="server" ID="hiddenTargetControl2" style="display:none"/>
  <cc1:ModalPopupExtender runat="server" ID="UserListModalPopupExtender"
      BehaviorID="UserListPopupBehavior"
      TargetControlID="hiddenTargetControl2"
      PopupControlID="PopupShowUserList" 
      BackgroundCssClass="modalBackground"
      DropShadow="False"
      CancelControlID="CancelButton2"
      PopupDragHandleControlID="PopupDragHandle"
      RepositionMode="RepositionOnWindowScroll" >
  </cc1:ModalPopupExtender>  
        
  <asp:Panel runat="server" CssClass="modalPopup" ID="PopupShowUserList" style="display:none;width:650px;  padding:0 0px 0px 0px">
      <div id="Div1" class="programmaticPopupDragHandle">
          <div class="h2blk">
          <h2 id="H1" >
            <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
            </h2>
        </div>   
        <cc2:Button ID="Button2" ScriptContent="MaxQueryFrom();" ShowPostDiv="false" runat="server" Text="Label_NULL"  CSS="MaxButtonCSS" AutoPostBack="false" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="true" ToolTip="Max" Width="12" />
		<cc2:Button ID="CancelButton2" ShowPostDiv="false" runat="server" Text="Label_NULL"  CSS="CancelButtonCSS" AutoPostBack="false" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="false" ToolTip="Close" Width="12" />
       </div>
           
            <div>
                  <asp:LinkButton ID="AllChecked" runat="server"  OnClientClick ="return AllChecked_Click()">
                  全选
                  </asp:LinkButton>
                  
                  <asp:LinkButton ID="AllNoChecked" runat="server" OnClientClick="return AllNoChecked_Click()">
                  全不选
                  </asp:LinkButton>
                  
          <asp:UpdatePanel ID="UpdatePanel3" runat="server">
              <ContentTemplate>  
		                   
          <div >
         	<fieldset>		
		      <legend style="background:url(../images/legendimg.jpg) no-repeat 6px 50%;"><asp:Label ID="Label5" runat="server" Text=""></asp:Label>
		      </legend>

          <!--page start-->
          <div id="pager" >
            <div class="PagerArea" >
               <webdiyer:AspNetPager ID="AspNetPager1" runat="server"  OnPageChanging="AspNetPager1_PageChanging" onpagechanged="AspNetPager1_PageChanged"  Width="50%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Page: " 
                 HorizontalAlign="left"  CenterCurrentPageButton="true" NumericButtonCount="9">
               </webdiyer:AspNetPager>
            </div>
            <div class="PagerText">
                    <asp:Label ID="lblPageSize" runat="server" Text=""></asp:Label>&nbsp;
                       <cc2:TextBox ID="txtPageSize" runat="server" Width="35" TextMode="SingleLine" SetFocusButtonID="btnPageSize"  ></cc2:TextBox>
                       <cc2:Button ID="btnPageSize" Text="Button_GO" runat="server" Width="60" OnClick="txtPageSize_TextChanged" ShowPostDiv="true"/>
             </div>    
           <cc1:FilteredTextBoxExtender  ID="FilteredTextBoxExtender3" runat="server" 
                    TargetControlID="txtPageSize"
                    FilterType="Numbers"/>   
          </div>
             
                  <cc2:Button ID="AddUser" runat="server" ShowPostDiv="false" ValidateForm="false" AutoPostBack="true" Disable="false" Text="Button_Add"  OnClick="AddUser_Click" />
                
           
       <br />
                                
            <!--page end-->
            <!--gridview start-->
                <!--gridview Browse mode start-->
             <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="">
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
                          <asp:CheckBox ID="Item" runat="server" Checked="false" />
                        </ItemTemplate>
                        <ItemStyle Width="35px" />
                      </asp:TemplateField>
              
                       <asp:BoundField DataField="UserName" HeaderText="用户名" />
                       <asp:BoundField DataField="UserAddress" HeaderText="地址" />
                       
                       <asp:TemplateField>
                         <HeaderTemplate>
                           权限
                         </HeaderTemplate>
                         
                         <ItemTemplate>
                             <asp:DropDownList ID="DropDownList1" runat="server"  DataValueField="permission" DataTextField="permission">
                                <asp:ListItem Text="无权限" Value="无权限"></asp:ListItem>
                                 <asp:ListItem Text="可读事件" Value="可读事件"></asp:ListItem>
                                 <asp:ListItem Text="可删除修改事件" Value="可删除修改事件"></asp:ListItem>
                                <asp:ListItem Text="可创建事件" Value="可创建事件"></asp:ListItem>
                             </asp:DropDownList>
                         </ItemTemplate>
                       </asp:TemplateField>
                       
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
       </div>
     </asp:Panel> 
     
     
      
    </form>
</body>
</html>
