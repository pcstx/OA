<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowMailList.aspx.cs" Inherits="GOA.Basic.ShowMailList" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>

<%@ Register Src="../WF/UserControl/FileDownLoadAttach.ascx" TagName="FileDownLoadAttach" TagPrefix="ucDown" %>


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
            
//                  function pageLoad() {

//                      $addHandler($get(""), 'click', showWriteModalPopup);


//                  }
                  function showWriteModalPopup(ev) {
                      ev.preventDefault();
                      var modalPopupBehavior = $find('PopupBehavior');
                      modalPopupBehavior.show();
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


            </script>
</head>
<body>
    <form id="form1" runat="server">
      <cc1:toolkitscriptmanager id="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
        </cc1:toolkitscriptmanager>
              
        <asp:UpdatePanel ID="UpdatePanelSendMail" runat="server">
                   <ContentTemplate> 
          
                             <div>
                               <cc2:Button ID="btnReSendMail"   Visible="true"  runat="server" ShowPostDiv="false" ValidateForm="false" AutoPostBack="true" Disable="true" Text="Button_ReSend" OnClick="btnReSendMail_Click"/>
                               <cc2:Button ID="btnAllSendMail"  Visible="false" runat="server" ShowPostDiv="false" ValidateForm="false" AutoPostBack="true" Disable="true" Text="Button_ALLSend" OnClick="btnAllSendMail_Click"/>
                               <cc2:Button ID="btnRecv"  Visible="false" runat="server" ShowPostDiv="false" ValidateForm="false" AutoPostBack="true" Disable="true" Text="Button_Recv" OnClick="btnRecv_Click"/>
                               <cc2:Button ID="btnChangeSendMail"   Visible="true"  runat="server" ShowPostDiv="false" ValidateForm="false" AutoPostBack="true" Disable="true" Text="Button_ChangeSend" OnClick="btnChangeSendMail_Click"/>
                             </div> 

                   
		              <div class="ManagerForm">
		                <fieldset>		
		                  <legend style="background:url(../images/legendimg.jpg) no-repeat 6px 50%;"><asp:Label ID="lblBigTitle" runat="server" Text=""></asp:Label>
		                 </legend>
		                  
		               <br />
                       <div class="clear">
                       <label class="char5">
                                    <asp:Label ID="lblName" runat="server" Text="主旨:"></asp:Label>
                               </label>
                               <div class="iptblk">
                                    <asp:Label ID="lblTitle" runat="server" Width="500"   Font-Size=Medium Font-Bold=true  Text=""></asp:Label>
                               </div>
                       </div>
		               <br />
		               <div class="clear">
                              <label class="char5">
                                    <asp:Label ID="lbUser" runat="server" Text="发件人:"></asp:Label>
                               </label>
                               <div class="iptblk">
                                     <asp:Label ID="lbUserName" runat="server" Text=""></asp:Label>
                               </div>
                       </div>

                       <br />
                       
                         <div class="clear">
                              <label class="char5">
                                    <asp:Label ID="time" runat="server" Text="时间:"></asp:Label>
                               </label>
                               <div class="iptblk">
                                     <asp:Label ID="lbltime" runat="server" Text=""></asp:Label>
                               </div>
                         </div>
                       <br>
                
                       
                           <div class="clear">
                              <label class="char5">
                                    <asp:Label ID="lblSendTo" runat="server" Text="收件人:"></asp:Label>
                               </label>
                               <div class="iptblk">
                                     <asp:Label ID="lblSender" runat="server" Text=""></asp:Label>
                               </div>
                           </div>
                       <br>
                       
                       
                        <div class="clear">
                              <label class="char5">
                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                               </label>
                               <div class="iptblk">
                                  <%-- <asp:LinkButton ID="Attachment" runat="server"  OnClick="Attachment_Click">
                                     </asp:LinkButton>
                                   <asp:PlaceHolder ID="placeHolder" runat="server"></asp:PlaceHolder> --%>
                                   
                                 <ucDown:FileDownLoadAttach  ID="FileDownLoadEmail" runat="server" />
                                 
                               </div>
                        </div>
                        
                        
                       <br />
      
                       
                       
                       <div class="clear">
                             <label class="char5">
                                    <asp:Label ID="Label1" runat="server" Text="内容"></asp:Label>
                               </label>
                               <div class="iptblk">

                                    <asp:Label ID="lbContent" runat="server" Width="500" Height="500"  TextMode="MultiLine" ></asp:Label> 
                                    
                               </div>
                       </div> 
                       
                       
                       </fieldset>
		             </div>
		         
                   </ContentTemplate>
                 </asp:UpdatePanel>

   
    </form>
</body>
</html>
