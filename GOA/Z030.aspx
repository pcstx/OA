<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Z030.aspx.cs" Inherits="GOA.Z030" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>修改密码</title>

  <script language="Javascript" type="text/javascript">
<!--
var gIEVersion = "";	//浏览器名字

var gVersionNum = 0;	//浏览器版本号
function fCheck(){
	var bOk = false;
	
	if( document.getElementById("txtNewPwd").value !=document.getElementById("txtNewSecPwd").value) {
	 
	    
		document.getElementById("lblMsg").innerHTML = "两次输入的密码不同";
		document.getElementById("txtNewSecPwd").value="";
		document.getElementById("txtNewPwd").focus();
		return false;
	}
	return true;
}

function fTrim(str)
{
	return str.replace(/(^\s*)|(\s*$)/g, ""); 
}
//-->
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
  </cc1:ToolkitScriptManager>
  <div style="width: 450px; padding: 30px 50px 0 0;">
    <div id="programmaticQueryPopupDragHandle" class="programmaticPopupDragHandle">
      <div class="h2blk">
        <h2 id="H2_2">
          <asp:Label ID="lblSearchTitle" runat="server" Text="修改密码"></asp:Label></h2>
      </div>
    </div>
    <div>
      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
          <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
            <div class="con">
              <div class="formblk">
                <div class="textblk">
                  <div class="clear">
                    <div class="oneline">
                      <label class="char5">
                        <!--label-->
                        原始密码</label>
                      <div class="iptblk">
                        <!--textbox-->
                        <cc2:TextBox ID="txtoldPwd" runat="server" Width="100" CanBeNull="必填" TextMode="Password"></cc2:TextBox></div>
                    </div>
                  </div>
                  <div class="clear">
                    <div class="oneline">
                      <label class="char5">
                        <!--label-->
                        新密码</label>
                      <div class="iptblk">
                        <!--textbox-->
                        <cc2:TextBox ID="txtNewPwd" runat="server" Width="100" CanBeNull="必填" TextMode="Password" MinimumValue="4"></cc2:TextBox>
                        <asp:Label ID="TextBox2_HelpLabel" runat="server" />
                        <cc1:PasswordStrength ID="PasswordStrength2" runat="server" TargetControlID="txtNewPwd" DisplayPosition="RightSide" StrengthIndicatorType="BarIndicator" PreferredPasswordLength="6" HelpStatusLabelID="" StrengthStyles="BarIndicator_TextBox2_weak;BarIndicator_TextBox2_average;BarIndicator_TextBox2_good" BarBorderCssClass="BarBorder_TextBox2" MinimumNumericCharacters="1" MinimumSymbolCharacters="1" TextStrengthDescriptions="Very Poor;Weak;Average;Strong;Excellent" RequiresUpperAndLowerCaseCharacters="true" />
                      </div>
                    </div>
                  </div>
                  <div class="clear">
                    <div class="oneline">
                      <label class="char5">
                        <!--label-->
                        确认新密码</label>
                      <div class="iptblk">
                        <!--textbox-->
                        <cc2:TextBox ID="txtNewSecPwd" runat="server" Width="100" CanBeNull="必填" TextMode="Password" MinimumValue="4" SetFocusButtonID="btnSubmit"></cc2:TextBox></div>
                    </div>
                  </div>
                  <div class="clear">
                    <div class="oneline">
                      <label class="char5">
                        <!--label-->
                      </label>
                      <div class="iptblk">
                        <!--textbox-->
                        <asp:Button ID="btnSubmit" CssClass="inp_L1" Text="提 交" onmouseover="this.className='inp_L2'" onmouseout="this.className='inp_L1'" TabIndex="4" OnClientClick="return fCheck();" runat="server" OnClick="btnSubmit_Click" />
                      </div>
                    </div>
                  </div>
                  <div class="clear">
                    <div id="lblMsg" runat="server" style="color: Red; font-weight: bold;">
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <!--validate start-->
          <!--validate end-->
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </div>
  </form>
</body>
</html>

