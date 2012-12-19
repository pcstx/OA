<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG60Select.aspx.cs" Inherits="GOA.GG60Select" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>数据源配置</title>

    <script language="javascript" type="text/javascript">
    
function $( id ){return document.getElementById( id );}
String.prototype.Trim = function() {return this.replace(/(^\s*)|(\s*$)/g, ''); };

     function CheckInputContent()
      {
            if($("txtDBName").value.Trim() == "")
            {
              alert("请输入服务器主机名！");
              return false;
            }
                
            if($("txtUserName").value.Trim() == "")
            {
              alert("请输入用户名！");
             return false;         
             }
             
            if($("txtPassword").value.Trim() == "")
            {
              alert("请输入密码！");
              return false;
            }

             return true;
       }      
       
       function ReturnGG60()
       {
          if ($("hiddenConnectString").value.Trim()=="-1")
          {
          window.returnValue="";
          window.close();
          }
          else  
          {
          window.returnValue=escape ($("hiddenConnectString").value.Trim());
          window.close();
          }
       } 
       
       function CloseWindow()
       {
       if ($("hiddenConnectStringOriginal").value.Trim()=="-1")
          {
          window.returnValue="";
          window.close();
          }
          else  
          {
          window.returnValue=escape ($("hiddenConnectStringOriginal").value.Trim());
          window.close();
          }
       }
   /*    
      function GetDataBaseName()
      {
      var dataSource=$("txtDBName").value.Trim();
      var userName=$("txtUserName").value.Trim();
      var password=$("txtPassword").value.Trim();
      
      if(dataSource!="" && userName!="" && password!="")
      {
          var ary=new Array();
          ary[0]=dataSource ;
          ary[1]=userName ;
          ary[2]=password ;
          ary[3]=$("ddlDBName") ;
          ary[4]=$("hiddenConnectString");
          PageMethods.SetDataBaseName(ary,getSucceeded);
        }
        else 
        {
        return false;
        }
      }

      //调用成功后，把每一个字段进行赋空，因为是新加后的调用
      function getSucceeded(result)
      {
         return true;
        }*/
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="ManagerForm">
                    <fieldset>
                        <legend style="background: url(../images/legendimg.jpg) no-repeat 6px 30%;">
                            <asp:Label ID="lblBigTitle" runat="server" Text="数据源配置"></asp:Label></legend>
                        <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                            <div class="con">
                                <div class="clear">
                                    <div class="oneline">
                                        <label class="char5" id="lblDS">
                                            服务器主机名：</label><div class="iptblk">
                                                <cc2:TextBox ID="txtDBName" runat="server" AutoPostBack="true" Width="150" OnTextChanged="onTextChanged"></cc2:TextBox><span style="color: Red;">*</span></div>
                                    </div>
                                </div>
                                <div class="clear">
                                    <div class="oneline">
                                        <label class="char5">
                                            用户名：</label><div class="iptblk">
                                                <cc2:TextBox ID="txtUserName" runat="server" Width="150" AutoPostBack="true" OnTextChanged="onTextChanged"></cc2:TextBox><span style="color: Red;">*</span></div>
                                    </div>
                                </div>
                                <div class="clear">
                                    <div class="oneline">
                                        <label class="char5">
                                            密码：</label><div class="iptblk">
                                                <cc2:TextBox ID="txtPassword" runat="server" Width="150" AutoPostBack="true" OnTextChanged="onTextChanged"></cc2:TextBox><span style="color: Red;">*</span>
                                            </div>
                                    </div>
                                </div>
                                <div class="clear" id="divDB" runat="server">
                                    <div class="oneline">
                                        <label class="char5">
                                            数据库名称：</label><div class="iptblk">
                                                <cc2:DropDownList ID="ddlDBName" runat="server" OnSelectedIndexChanged="ddlDBName_OnSelectedIndexChanged" AutoPostBack="true" Width="150">
                                                </cc2:DropDownList>
                                                <span style="color: Red;">*</span></div>
                                    </div>
                                </div>
                                <div>
                                    <cc2:Button runat="server" ID="btnTestConnect" ShowPostDiv="false" ButtonImgUrl="../images/cache_resetall.gif" Text="Button_TestConnect" ScriptContent="CheckInputContent()" ButtontypeMode="Normal" AutoPostBack="true" OnClick="btnConnect_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <cc2:Button runat="server" ID="btnOK" ButtontypeMode="Normal" ButtonImgUrl="../images/OK.gif" Text="Button_GoComplete" ScriptContent="CheckInputContent();ReturnGG60()" ShowPostDiv="false" AutoPostBack="false" Page_ClientValidate="false" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <cc2:Button runat="server" ID="btnCancel" ButtontypeMode="Normal" ButtonImgUrl="../images/cancel.gif" Text="Button_Cancel" ScriptContent="CloseWindow()" AutoPostBack="false" ShowPostDiv="false" />
                                </div>
                                <div class="clear">
                                    <label class="char9">
                                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label></label></div>
                            </div>
                        </div>
                    </fieldset>
                    <asp:HiddenField ID="hiddenConnectString" runat="server" />
                    <asp:HiddenField ID="hiddenConnectStringOriginal" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!--<Triggers>
<asp:AsyncPostBackTrigger  ControlID="txtDBName" EventName="TextChanged"/>
<asp:AsyncPostBackTrigger  ControlID="txtUserName" EventName="TextChanged"/>
<asp:AsyncPostBackTrigger  ControlID="txtPassword" EventName="TextChanged"/>
</Triggers>-->
    </form>
</body>
</html>
