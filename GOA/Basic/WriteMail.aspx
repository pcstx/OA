<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WriteMail.aspx.cs" Inherits="GOA.Basic.WriteMail"  ValidateRequest="false" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<%@ Register tagPrefix="anthem" namespace="Anthem" assembly="Anthem" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>


<%@ Register Src="../WF/UserControl/FileUploadEmailAttach.ascx" TagName="FileUploadEailAttach" TagPrefix="ucEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    	    <link href="../styles/StyleSheet.css" type="text/css" rel="stylesheet" />
    	    <style type="text/css" >
    	   .btnHide
    	   {
    	   	 display:none; 
    	   	}
    	    
    	    </style>
    	    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>
    	    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>
            <script language="javascript" type="text/javascript" src="../JScript/jquery-latest.pack.js"></script>
            <script src="../JScript/jquery.MultiFile.js" type="text/javascript"></script>

            <script language="javascript" type="text/javascript">
            
                  function pageLoad() {

                  //  $addHandler($get("Attachment1"), 'click', showWriteModalPopup);
                      $addHandler($get("btnSendTo"), 'click', showUserListModalPopup);
                     $addHandler($get("btnScret"), 'click', showUserListModalPopup);


                  }
                  function showWriteModalPopup(ev) {
                      ev.preventDefault();
                      var modalPopupBehavior = $find('PopupBehavior');
                      modalPopupBehavior.show();
                  }

                  function showUserListModalPopup(ev) {
                      ev.preventDefault();
                      var UserListPopupBehavior = $find('UserListPopupBehavior');
                      UserListPopupBehavior.show();
                  }

                  function showUserListScretModalPopup(ev) {
                     ev.preventDefault();
                     var UserListPopupBehavior2 = $find('UserScretListPopupBehavior');
                      UserListPopupBehavior2.show();
                  
                  }

                  function ajaxSend() {
                      if (CheckInputContent()) {
                         var username= $("#TextBox1").val();
                          parent.sendMsg(username,"您有一封新邮件，请注意查收！");
                          return true;
                      }
                      else {
                          return false;
                      }
                  }
              
              
                function CheckInputContent() {

                    if ($("#txtTitle").val() == "") {
                        alert("标题不能为空");
                        $("#txtTitle").focus();
                        return false;
                    }

                    if ($("#dpType").val() == "") {
                        alert("类别不能为空");
                        $("#dpType").focus();
                        return false;
                    }
                     
                     var atContent = FCKeditorAPI.GetInstance("FCKeditor1");
                     var getValue = atContent.GetXHTML(true);
                     if (getValue == "") {
                        alert("内容不能为空");
                        return false;
                    }

                    if ($("#txtExpDate").val() != "") {
                        if (!DoReg($("#txtExpDate").val(), "datetime")) {
                            alert("过期日期格式不正确")
                            $("#txtExpDate").focus();
                            return false;
                        }
                    }

                    return true;
                }


                function DoReg(value, type) {
                    var Reg = "";
                    if (type == "varchar(50)")   //字符串
                        Reg = /^[a-z,A-Z]+$/;
                    else if (type == "int")  //数字
                        Reg = /^\d.+$/
                    else if (type == "datetime") //日期
                        Reg = /^(?:(?!0000)[0-9]{4}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1[0-9]|2[0-8])|(?:0[13-9]|1[0-2])-(?:29|30)|(?:0[13578]|1[02])-31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)-02-29)$/;
                    if (!Reg.exec(value)) {
                        return false;
                    }
                    return true;
                }

                function FCKUpdateLinkedField(id) {
                    try {
                        if (typeof (FCKeditorAPI) == "object") {
                            FCKeditorAPI.GetInstance(id).UpdateLinkedField();
                        }
                    }
                    catch (err) {
                    }
                }

                function AllChecked_Click() {

                    var GridViewtableSearchList = document.getElementById("GridView1");
                    var inputs = GridViewtableSearchList.getElementsByTagName("input");   
                    for (var i = 0; i < inputs.length; i++)  
                    {
                        if (inputs[i].type == "checkbox" && inputs[i].id != "item") {
                            inputs[i].checked = true;
                        }
                    }
                    return false;
          
                }

                function AllNoChecked_Click() {

                    var GridViewtableSearchList = document.getElementById("GridView1");
                    var inputs = GridViewtableSearchList.getElementsByTagName("input"); 
                    for (var i = 0; i < inputs.length; i++)  
                    {
                        if (inputs[i].type == "checkbox" && inputs[i].id != "item") {
                            inputs[i].checked = false;
                        }
                    }
                    return false;

                }

                function addScret() {
                    var ui = document.getElementById("btnScret");
                    ui.style.display="block";
                    var uii = document.getElementById("txtlblScret");
                    uii.style.display="block";
                   
                    return false;
                 
                }


                function CheckInputContent() {

                    if ($("#FreeTextBox1").val() == "") {
                        alert("内容不能为空");
                        return false;
                    }

                    if ($("#txtTitle").val() == "") {
                        alert("主旨不能为空");
                        $("#txtTitle").focus();
                        return false;
                    }
                    return true;
                }

     

                   
                   
            </script>
</head>
<body>
<script language="javascript"  type="text/javascript">
    function change() {
        AllNoChecked_Click();
        $("#Label4").text("添加密件人");
        $("#<%=AddUser.ClientID%>").val("添加密件人");
        $("#HiddenField1").val("添加密件人");
    }

    function changeuser() {
        AllNoChecked_Click();
        $("#Label4").html("添加收件人");
        $("#<%=AddUser.ClientID%>").val("添加收件人");
        $("#HiddenField1").val("添加收件人");
        
        
        
    }

</script>

    <form id="form1" runat="server">
      <cc1:toolkitscriptmanager id="ToolkitScriptManager1" runat="server" EnablePageMethods="True" EnablePartialRendering="true">
        </cc1:toolkitscriptmanager>
              
        <asp:UpdatePanel ID="UpdatePanelSendMail" runat="server"  UpdateMode="Conditional">
                   <ContentTemplate> 
                             <div class="edit">
                                 <cc2:Button ID="btnSendMail" runat="server" ShowPostDiv="false" ValidateForm="false"  AutoPostBack="true" Disable="false" Text="Button_Send" OnClick="btnSendMail_Click" ScriptContent="CheckInputContent()"/>
                             </div> 

		            <div>
		                <fieldset>		
		                  <legend style="background:url(../images/legendimg.jpg) no-repeat 6px 50%;"><asp:Label ID="lblBigTitle" runat="server" Text=""></asp:Label>
		                 </legend>
		                 <br />
		                 <asp:HiddenField ID="HiddenField1" runat="server" /> 
		                 
		               <div class="clear">
                              <label class="char5">
                      
                                    <asp:Label ID="lblSendTo" runat="server" Text="收件人地址"></asp:Label>
                               </label>
                               <div class="iptblk">
                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" >
                                      <ContentTemplate>
                                          <asp:TextBox ID="TextBox1" runat="server" style="display:none"></asp:TextBox> 
                                        <cc2:TextBox ID="txtSendTo" runat="server" Width="400"  Enabled="false"></cc2:TextBox>
                                         <asp:ImageButton ID="btnSendTo" runat="server" ImageAlign="Middle" ImageUrl="../images/arrow_black.gif"  ToolTip="添加"   OnClientClick="return changeuser()"  />    
                                    <asp:Label ID="Label6" runat="server" Text="(必填)"></asp:Label>     
                                       </ContentTemplate>
                                    </asp:UpdatePanel>
                               
                               </div>
                       </div>
             
                       <br />
                       <div class="clear">
                              <label class="char5">
                                    <asp:Label ID="lblTitle" runat="server" Text="主旨"></asp:Label>
                               </label>
                               <div class="iptblk">
                                    <cc2:TextBox ID="txtTitle" runat="server" Width="500" ></cc2:TextBox>
                                    <asp:Label ID="Label7" runat="server" Text="(必填)"></asp:Label>
                               </div>
                       </div>
                       
                       <br />
                        
                       <div class="clear">
                              <label class="char5">
                                    <asp:Label ID="lblScret" runat="server" Text="密件"></asp:Label>
                               </label>
                               <div class="iptblk" >
                                   <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always" >
                                      <ContentTemplate>
                                    <cc2:TextBox ID="txtlblScret" runat="server" Width="400" Enabled="false" ></cc2:TextBox>
                                    <asp:ImageButton ID="btnScret" runat="server" ImageAlign="Middle" ImageUrl="../images/arrow_black.gif"  ToolTip="添加密送"  OnClientClick="return change()"  />
                             
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                                  </div>
                       </div>
         
                       <br />
           
                       <div class="clear">
                              <label class="char5">
                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                               </label>
                               <div class="iptblk">
                                 
                                       <div class="clear">
                                      <ucEmail:FileUploadEailAttach ID="FileUploadEmail" runat="server" />
               
                                       </div>
             
                                   <br>
                                  
                               </div>
                       </div>
                       <br>
                           
  
                       <div class="clear">
                             <label class="char5">
                                    <asp:Label ID="Label1" runat="server" Text="内容"></asp:Label>
                               </label>
                               <div class="iptblk">
                               <!--
                                   <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server"   DefaultLanguage="zh-cn" Width="600" Height="500"  EnableViewState="true"></FCKeditorV2:FCKeditor>
                                   -->
                                   
                                   <FTB:FreeTextBox ID="FreeTextBox1" runat="server"   Width="600" Height="500" 
                                   toolbarlayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu,FontForeColorPicker,FontBackColorsMenu,FontBackColorPicker|Bold,Italic,
                                   Underline,Strikethrough,Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;
                                   CreateLink,Unlink|Cut,Copy,Paste,Delete;Undo,Redo,Print,Save|SymbolsMenu,StylesMenu,InsertHtmlMenu|InsertRule,InsertDate,InsertTime|InsertTable,
                                   EditTable;InsertTableRowAfter,InsertTableRowBefore,DeleteTableRow;InsertTableColumnAfter,InsertTableColumnBefore,DeleteTableColumn|InsertForm,InsertTextBox,
                                   InsertTextArea,InsertRadioButton,InsertCheckBox,InsertDropDownList,InsertButton|InsertDiv,EditStyle,InsertImageFromGallery,Preview,SelectAll,WordClean,NetSpell"
                                   ToolbarStyleConfiguration="Office2000" ImageGalleryPath="~/Basic/image/upload"  >
                                   </FTB:FreeTextBox>
                         
                               </div>
                           
                         </div> 
                         
                       </fieldset>
                      </div>
		         
                   </ContentTemplate>
                 </asp:UpdatePanel>

  
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
          <h2 id="H2_2" >
            <asp:Label ID="lblSearchTitle" runat="server" Text="添加附件"></asp:Label>
            </h2>
        </div>   
        <cc2:Button ID="MaxButton" ScriptContent="MaxQueryFrom();" ShowPostDiv="false" runat="server" Text="Label_NULL"  CSS="MaxButtonCSS" AutoPostBack="false" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="true" ToolTip="Max" Width="12" />
		  <cc2:Button ID="CancelButton" ShowPostDiv="false" runat="server" Text="Label_NULL"  CSS="CancelButtonCSS" AutoPostBack="false" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="false" ToolTip="Close" Width="12" />
       </div>
           
            <div>
                
                <asp:UpdatePanel ID="UpdatePanelAddAttach" runat="server" >
                   <ContentTemplate>  
		     
		         <cc2:Button ID="AddAttach" runat="server" ShowPostDiv="false" ValidateForm="false" AutoPostBack="true" Disable="false" Text="Button_Add"  OnClick="AddAttach_Click"/>
		              
		                 <div class="clear">
                              <label class="char5">
                                    <asp:Label ID="Label3" runat="server" Text="附件:"></asp:Label>
                               </label>
                             
                              <div class="iptblk">
                                   <asp:FileUpload ID="FileUpload1" runat="server" />
                                     
                               </div>
                       </div>
		             </div>
                   </ContentTemplate>
                  <Triggers>
                        <asp:PostBackTrigger ControlID="AddAttach" />
                   </Triggers>
                 </asp:UpdatePanel>
       </div>
     </asp:Panel>  
     
   
     
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
