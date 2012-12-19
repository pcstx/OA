<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeInfo.aspx.cs" Inherits="GOA.Basic.EmployeeInfo" %>


<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
      <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    	    <link href="../styles/StyleSheet.css" type="text/css" rel="stylesheet" />
    	    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>
    	    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>
            <script language="javascript" type="text/javascript" src="../JScript/jquery-latest.pack.js"></script>
                <script language="javascript" type="text/javascript" src="../JScript/DateTimePicker/WdatePicker.js"></script>
            <script language="javascript" type="text/javascript">
                function CheckInfo() {
                    
                    
                    if ($("#txtPEEBIEN").val() == "") {
                        alert("姓名不能为空");
                        $("#txtPEEBIEN").focus();
                        return false;
                    }
                    if ($("#txtPEEBIBD").val() == "") {
                        alert("出生日期不能为空");
           
                        $("#txtPEEBIBD").focus();
                        return false;
                    }
                    if (!DoReg($("#txtPEEBIBD").val(), "datetime")) {
                        alert("出生日期格式不正确")
                        $("#txtPEEBIBD").focus();
                        return false;
                    }
                    if ($("#txtPEEBICP").val() == "") {
                        alert("联系电话不能为空");
                        $("#txtPEEBICP").focus();
                        return false;
                    }
                    if ($("#txtPEEBIDEP").val() == "--请选择--") {
                        alert("所属部门不能为空");
                        $("#txtPEEBIDEP").focus();
                        return false;
                    }
                    if ($("#txtPEEBIDL").val() == "") {
                        alert("直接主管不能为空");
                        $("#txtPEEBIDL").focus();
                        return false;
                    }
                    if ($("#txtPEEBIDT").val() == "--请选择--") {
                        alert("请选择职务");
                        $("#txtPEEBIDT").focus();
                        return false;
                    }
                    if ($("#txtPEEBIED").val() == "") {
                        alert("入厂时间不能为空");
                        $("#txtPEEBIED").focus();
                        return false;
                    }
                    if (!DoReg($("#txtPEEBIED").val(), "datetime")) {
                        alert("入厂日期格式不正确")
                        $("#txtPEEBIED").focus();
                        return false;
                    }
                    
                    if ($("#txtPEEBIES").val() == "--请选择--") {
                        alert("请选择员工状态");
                        $("#txtPEEBIES").focus();
                        return false;
                    }
                    if ($("#txtPEEBIPC").val() == "--请选择--") {
                        alert("岗位不能为空");
                        $("#txtPEEBIPC").focus();
                        return false;
                    }

                    return true;
                        
                  
                }
            
            
                function GetPEEIBIID(MykeyCol) {
                    strPEEIBIID = MykeyCol;

                    return false;
                }
                //获取添加的个人信息
                function GetPEEBITEMP() {
                    var iRowCount = 0;
                    if (strPEEIBIID != "") {
                        $.ajax({
                            type: "POST",
                            dataType: "text",
                            url: "GetAddItem.ashx",
                            data: "action=Get&PEEBITEMPID=" + strPEEIBIID,
                            complete: function() { $("#load").hide(); },
                            success: function(msg) {
                                var input = $("#MyAddItem >input");
                                input.remove();
                                var span = $("#MyAddItem >span");
                                span.remove();
                                var br = $("#MyAddItem >br");
                                br.remove();
                                if (msg != "") {

                                    var div = document.getElementById("MyAddItem");
                                    var dataArray = new Array();
                                    var id, name, value, type;
                                    var iCount = 0;
                                    dataArray = msg.split("|");
                                    for (var i = 0; i < dataArray.length - 1; i++) {
                                        iCount++;
                                        if (iCount == 1)
                                            id = dataArray[i];
                                        if (iCount == 2)
                                            name = dataArray[i];
                                        if (iCount == 3)
                                            type = dataArray[i];
                                        if (iCount == 4) {
                                            iCount = 0;
                                            value = dataArray[i];
                                            //var ItemTxt = document.createTextNode(name);
                                            iRowCount++;
                                            var divOne = document.createElement("div");
                                            divOne.setAttribute("class", "third" + i);

                                            var ItemTxt = document.createElement("span");
                                            ItemTxt.innerHTML = name + ":";
                                            ItemTxt.setAttribute("class", "char5");

                                            var inputTxt = document.createElement("input");
                                            inputTxt.type = "text";
                                            inputTxt.value = value;
                                            inputTxt.name = id;
                                            inputTxt.setAttribute("id", id);
                                            inputTxt.setAttribute("class", "iptblk");
                                            
                                            var HideText = document.createElement("input");
                                            HideText.type = "hidden";
                                            HideText.value = type;
                                            var inputBr = document.createElement("<br>");
                                            div.appendChild(divOne);
                                            div.appendChild(ItemTxt);
                                            div.appendChild(inputTxt);
                                            div.appendChild(HideText);


                                        }
                                    }
                                }



                            }
                        });
                    }
                }

                $(function() {
                    $("#btnSubmit").click(function() {

                        var str = "";
                        var icount = 0;
                        var valueStr = "";
                        var hideValuStr = "";
                        var txt = $("#MyAddItem > input");
                        for (var i = 0; i < txt.size(); i++) {

                                valueStr = escape(txt[i].value);
                                if (valueStr == "") {
                                    alert("请为添加的栏位输入值");
                                    return false;
                                }
                                else
                                    str = str + txt.eq(i).attr("id") + "=" + valueStr + "|";
                        }
                        $.ajax({
                            type: "Get",
                            dataType: "text",
                            url: "AddPEEBIInfo.ashx",
                            data: "action=add&PEEBITEMPID=" + strPEEIBIID + "&str=" + str,
                            complete: function() { $("#load").hide(); },
                            success: function(msg) {
                                value = parseInt(msg);
                                if (value > 0) {
                                    alert("操作成功!");

                                }
                                else
                                    alert("操作失败!");
                            }
                        });
                    });

                });


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


                function btnIUserListClick() {
                    var url = 'UserSelect.aspx?GUID=' + Math.random();
                    var ret = window.showModalDialog(url, 'UserSelect', 'scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px');

                    if (ret != null) {
                        window.document.getElementById("txtUserSerialID").value = ret[0];
                        window.document.getElementById("txtPEEBIDL").value = ret[1];
                    }
                    return false;
                }
        </script>
         <style type="text/css">
 .btn
 {
 	display:none;

 	
 	}
 
 </style>
</head>
<body>
   
   <form id="form1" runat="server" >
      <cc1:toolkitscriptmanager id="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
        </cc1:toolkitscriptmanager>
        
         <asp:UpdatePanel ID="UpdatePanel" runat="server">
           <ContentTemplate>
            <div id="addInfo">
                                       <asp:Button  ID="ButtonHidde" runat="server" Text="Button" OnClick="ButtonHidde_Click" CssClass="btn"  />
                                       <asp:Button ID="btnSubmit" runat="server" Text="更新" OnClick="btnSubmit_Click" CssClass="btn"  />
                                       <asp:Button ID="btnAdd" runat="server" Text="添加"  OnClick="btnAdd_Click"   ScriptContent="return CheckInfo()" CssClass="btn"  />
             </div>
           <fieldset>		
		      <legend style="background:url(../images/legendimg.jpg) no-repeat 6px 100%;"><asp:Label ID="lblBigTitle" runat="server" Text="用户信息"></asp:Label>
		      </legend>
                 <div class="clear">
                 
                          <div class="conblk3" id="ProfileContainer" >
		                         <div class="con" id="wrapProfile">
		                                               
					                    <div class="textblk">            
                                                <div class="third2">
                                                   <label class="char5"><!--员工编号-->   
                                                        <asp:Label ID="lblPEEBIEC" runat="server" Text=""></asp:Label>
                                                   </label>
                                                
                                                <div class="iptblk">
                                                       <cc2:TextBox ID="txtPEEBIEC" runat="server" Width="100" ReadOnly="true" ></cc2:TextBox>
                                                        </div>
                                                 </div>
                                                               
                                                <div class="third3"><label class="char5"><!--员工姓名-->   
                                                     <asp:Label ID="lblPEEBIEN" runat="server" Text=""></asp:Label>
                                                     <span id="Span1" style="color:Red;">*</span></label>
                                                     <div class="iptblk">
                                                       <cc2:TextBox ID="txtPEEBIEN" runat="server" Width="130" ></cc2:TextBox>
                                                      </div>
                                                 </div>
                                              </div> 
                                       
                                           
                                              <div class="clear">
                                                                <div class="third2"><label class="char5"> <!--员工出生日期-->   <asp:Label ID="lblPEEBIBD" runat="server" Text=""></asp:Label><span id="Span2" style="color:Red;">*</span></label><div class="iptblk"><cc2:TextBox ID="txtPEEBIBD" runat="server" Width="100"  ></cc2:TextBox>
                                                                                    <asp:ImageButton runat="Server" ID="ImagePEEBIBD" ImageUrl="../images/calendar.gif" AlternateText="Click to show calendar" /><br />
                                                                                    <cc1:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="txtPEEBIBD" PopupButtonID="ImagePEEBIBD" Format="yyyy-MM-dd" Animated="true" PopupPosition="TopLeft" /></div></div>
                                                               
                                               </div>
                                               
                                               
                                               
                                               
                                               
                                                <div class="clear"><div class="third1"><label class="char5"><!--联系电话-->   <asp:Label ID="lblPEEBICP" runat="server" Text=""></asp:Label><span id="Span9" style="color:Red;">*</span></label><div class="iptblk"><cc2:TextBox ID="txtPEEBICP" runat="server" Width="130" ></cc2:TextBox></div></div>
                                                               </div>
                                               
                                        
                                               
                                                <div class="clear">
                                                   <div class="third1">
                                                      <label class="char5"><!--所属部门-->   
                                                         <asp:Label ID="lblPEEBIDEP" runat="server" Text=""></asp:Label>
                                                             <span id="Span13" style="color:Red;">*</span>
                                                      </label>
                                                          <div class="iptblk">
                                                              <!--
                                                              <cc2:TextBox ID="txtPEEBIDEP" runat="server" Width="120" ></cc2:TextBox>
                                                              -->
                                                                 <cc2:DropDownList ID="dpPEEBIDEP" runat="server" Width="132" >
                                                                   </cc2:DropDownList>
                                                                <input id="txtdepartmentCode" runat="server" type="hidden" />
                                                          </div>
                                                   </div>
                                                   
                                                                <div class="third2"><label class="char5"><!--直接主管-->  
                                                                 <asp:Label ID="lblPEEBIDL" runat="server" Text=""></asp:Label>
                                                                 <span id="Span14" style="color:Red;">*</span></label>
                                                                 <div class="iptblk">
                                                                    <cc2:TextBox ID="txtPEEBIDL" runat="server" Width="100" ></cc2:TextBox>
                                                                   <asp:ImageButton ImageUrl="../../images/arrow_black.gif" ImageAlign="Middle" ID="ImageButton1" ToolTip="搜索" runat="server" OnClientClick="return btnUserListClick();" /></div>
                                                                    <input id="txtPersonCode" runat="server" type="hidden" />
                                                                    </div>
                                                                    </div>
                                                                 </div>
                                                <div class="clear"><div class="third1"><label class="char5"><!--岗位-->   <asp:Label ID="lblPEEBIPC" runat="server" Text=""></asp:Label><span id="Span16" style="color:Red;">*</span></label>
                                                              <div class="iptblk">
                                                              
                                                              <cc2:DropDownList ID="dpPEEBIPC" runat="server" Width="132">
                                                                   </cc2:DropDownList>
                                                              <!--
                                                                  <cc2:TextBox ID="txtPEEBIPC" runat="server" Width="120"></cc2:TextBox>-->
                                                                  
                                                                  
                                                              <input id="txtPEEBIPCCode" runat="server" type="hidden" /></div></div>
                                                              
                                                              
                                                                 <div class="third3"><label class="char5"><!--职务-->   <asp:Label ID="lblPEEBIDT" runat="server" Text=""></asp:Label><span id="Span18" style="color:Red;">*</span></label><div class="iptblk">
                                                             
                                                                   <cc2:DropDownList ID="dpPEEBIDT" runat="server" Width="132">
                                                                   </cc2:DropDownList>
                                                                      <!--  <cc2:TextBox ID="txtPEEBIDT" runat="server" Width="120"></cc2:TextBox>-->
                                                                   </div></div>
                                               </div>
                                               
                                              
                                                <div class="clear">
                                                  <div class="third2">
                                                   <label class="char5"> <!--入厂时间-->   
                                                      <asp:Label ID="lblPEEBIED" runat="server" Text=""></asp:Label>
                                                      <span id="Span21" style="color:Red;">*</span>
                                                   </label>
                                                   
                                                     <div class="iptblk">
                                                        <cc2:TextBox ID="txtPEEBIED" runat="server" Width="100" onkeyup="settxtPEETNTD();" onchange="settxtPEETNTD();"  >
                                                        </cc2:TextBox>
                                                          <asp:ImageButton runat="Server" ID="ImagePEEBIED" ImageUrl="../images/calendar.gif" AlternateText="Click to show calendar" /><br />
                                                          <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtPEEBIED" PopupButtonID="ImagePEEBIED" Format="yyyy-MM-dd" Animated="true" PopupPosition="TopLeft"/>
                                                        </div>
                                                     </div>
                                                   </div>
                                               <div class="clear">
                                                <div class="third3"><label class="char5"><!--员工状态-->   <asp:Label ID="lblPEEBIES" runat="server" Text=""></asp:Label><span id="Span20" style="color:Red;">*</span></label><div class="iptblk">
                                            
                                                <cc2:DropDownList ID="dpPEEBIES" runat="server" Width="132" Enabled="true"></cc2:DropDownList>
                                               <!--
                                                      <cc2:TextBox ID="txtPEEBIES" runat="server" Width="120"></cc2:TextBox>-->
                                                      
                                                </div></div>
                                                  </div>
                                        </div>
                                  </div>
	                            </div>
	                            </div>
	                            </fieldset> 
	      </ContentTemplate>
                     
</asp:UpdatePanel>
                   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                      <ContentTemplate>
	                         <div class="clear">
	                          <div id="MyAddItem">
                                   <asp:PlaceHolder ID="placeHolder" runat="server"></asp:PlaceHolder>
                                   </div>
                             </div>
	     
	                             </ContentTemplate>
                  </asp:UpdatePanel>

        </form>
 
</body>
</html>
