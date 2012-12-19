<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="GOA.login" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolKit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>�ݰ±�MES</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<meta content="���г�,�綯���г�,�綯��,﮵�ص綯��,����,�綯����,������,�綯������" name="keywords"/>
<meta content="�ú��Ӽ�������1993��ͽ��������г��͵綯ϵ�в�Ʒ����2005�꿪ʼ����������Ʒ���Ʒ���������Ρ������εȣ������ڣ��ú��ӽݰ±Ƚ�����Ʒ���޹�˾��ʽ��֯ר�ŵġ��ִ����������ߣ����ģ�����綯���г����綯�������͵綯���εȲ�Ʒ��" name="description"/>
<meta name="robots" content="index, follow" />
<meta name="googlebot" content="index, follow" />
<script language="Javascript" type="text/javascript">
<!--
var gIEVersion = "";	//���������
var gVersionNum = 0;	//������汾��
function fCheck(){
	var bOk = false;
	var s = "";
	var fm = document.form;
	s = fm.style.value;
	
	fm.user.value = fTrim(fm.user.value); //Trim the input value.
	if(!fCheckCookie()){
		return false;
	}
	if( fm.user.value =="") {
		document.getElementById("error_div").innerHTML = "����������û���";
		fm.user.focus();
		return false;
	}	

	if( fm.pass.value.length =="") {
		document.getElementById("error_div").innerHTML = "�������������";
		fm.pass.focus();
		return false;
	}
	if( fm.vcode.value.length =="") {
		document.getElementById("error_div").innerHTML = "��������֤��";
		fm.vcode.focus();
		return false;
	}
	var remUser = fm.remUser.checked?true:false;
	/*
	if( remUser){
		fm.autocomplete="on";
	}else{
		fm.autocomplete="off";
	}
	
	var secure = fm.secure;
	var url;
	if(secure.checked){
		url = "https://entry.mail.126.com/cgi/login?redirTempName=https.htm&hid=10010102&";
	}else{
		url = "http://entry.mail.126.com/cgi/login?hid=10010102&";
	}
	url +="lightweight=1&";
	if( fm.secure.checked )
		url += "verifycookie=1&";
	fm.action = url + "language=0&style=" + s ;
	var ati = fm.user.value.indexOf( "@"); 
	if( ati != -1 )
		fm.user.value = fm.user.value.substring(0, ati);
	visitordata.setVals( [fm.user.value,fm.style.value,fm.secure.checked?1:0 ], remUser );
	visitordata.store();
	*/
	if(fm.remUser.checked){
	    visitordata.store();
		//setCookie('geoby_dealer_User','true',remUser);
	}
	else
	{
	    visitordata.delVals();
	}
	return true;
}

function fTrim(str)
{
	return str.replace(/(^\s*)|(\s*$)/g, ""); 
}

function fGetIEVersion()
{
	var IEAppName   = window.navigator.appName;						//�õ���ǰ���������.		
	var IEversion	= window.navigator.appVersion;					//�õ���ǰ������İ汾˵��
	if(IEAppName.indexOf("Microsoft")<0){
		gIEVersion = IEAppName;
		return 0;
	}
	var isOpera = window.navigator.userAgent.indexOf("Opera") > -1;
	if(isOpera){
		gIEVersion = "Opera";
		return 0;
	}

	var k=IEversion.indexOf("MSIE");					//����IE�İ汾��
	if(k>=0){
		k+=4;
		var j=IEversion.indexOf(";",k);
		if(j<0){
			j=IEversion.length-1;
		}	
		IEversion	=IEversion.substring(k,j)*1;				//�õ�IE�İ汾�ţ��������ֻ�
		gIEVersion = "MSIE: "+IEversion;
		if(IEversion >= 6){										//����汾��6.0���ϣ�		
			return 6;
		}else if( IEversion >= 5.5 && IEversion < 6 ){
			return 5.5;
		}else if( IEversion >= 5 && IEversion <5.5){
			return 5;
		}
		else{
			return 0;
		}
	}else{
		gIEVersion =  window.navigator.appVersion;
		return 0;
	}
}
gVersionNum = fGetIEVersion();

function Cookie( document, name, domain )
{
	this.$document = document;
	this.$name = name;
	this.$expiration = new Date();
	this.$domain = domain;
	this.$Days = 365;
	this.data = null;
}
Cookie.prototype.store = function()
{
    this.$expiration.setTime(this.$expiration.getTime() + this.$Days*24*60*60*1000);
    document.cookie = this.$name + "="+ escape (document.getElementsByName("user")[0].value) + ";expires=" + this.$expiration.toGMTString();
}
Cookie.prototype.load = function()
{
	var allcookies = this.$document.cookie;
	if( allcookies == "") return false;
	var start = allcookies.indexOf( this.$name + "=" );
	if( start == -1) return false;
	start += this.$name.length +1;
	var end = allcookies.indexOf( ";", start );
	if( end == -1) end = allcookies.length;
	var cookieval= allcookies.substring( start, end );
	var a = cookieval.split("&");
	for( var i=0;i<a.length; i++)
		a[i] = a[i].split(':');
	//�û���:���:��ȫ
	this.data = a;
	
	return true;
}
Cookie.prototype.setVals = function( a, flag )
{
    alert("setVals");

	if( this.data == null)
	{
		if( flag )
		{	
			this.data = [];
			this.data[0] = a;
		}
	}
	else
	{
		this.data[0][0] = a[0];
		if( flag)
			return;
		else
			this.data = null;
	}
}
Cookie.prototype.delVals = function()
{
    var cval=document.getElementsByName("user")[0].value;
    this.$expiration.setTime(this.$expiration.getTime() - this.$Days*24*60*60*1000);
    document.cookie = this.$name + "="+ escape (cval) + ";expires=" + this.$expiration.toGMTString();

}

//*** For Login UserName.
function fInitUser()
{
	var fm = window.document.form;
	var name = "";
	if( visitordata.data != null)
	{	name = visitordata.data[0][0];
		fm.remUser.checked = true;
		fm.autocomplete="on";
	}else{
		fm.autocomplete="off";
		fm.remUser.checked = getCookie("geoby_dealer_User")!="true";
	}

	if( name != ""){
		fm.user.value = name;
		fm.pass.focus();
	}else{
		fm.user.focus();
	}
}
function fCheckCookie(){
	//var secure = document.getElementsByName("secure");
	var cookieEnabled=(navigator.cookieEnabled)? true : false;
	if (typeof navigator.cookieEnabled=="undefined" && !cookieEnabled){ 
		document.cookie="testcookie";
		cookieEnabled=(document.cookie=="testcookie")? true : false;
		document.cookie="";
	}
	//if(secure.length>0){secure[0].checked &&
		if(!cookieEnabled){
			window.alert("���ã�������������ý�ֹʹ��cookie������¼����ʱѡ���ˡ���ǿ��ȫ�ԡ�ѡ���ѡ��Ҫ�����������cookie���á�\n\n������ѡ�����µ�����һ��������¼���䣺\n1���������������������cookie���ã������µ�¼��\n2�����ߵ�¼ʱȡ��ѡ�С���ǿ��ȫ�ԡ�ѡ��������ĵ�¼��ȫ�Խ��ή�͡�");
			return false;
		}
	//}
	return true;
}
function fSetLogType(){
	/*var logType = getCookie("logType");
	var login_select = document.getElementById("login_select");
	if(logType == "jy"){
		login_select.selectedIndex = 4;
	}else if(logType == "dm3"){
		login_select.selectedIndex = 1;
	}else if(logType == "dm"){
		login_select.selectedIndex = 3;
	}else if(logType == "js3"){
		login_select.selectedIndex = 2;
	}else{
		login_select.selectedIndex = 0;	
	}
	*/
}
function getCookie(name) {
   var search = name + "="
   if(document.cookie.length > 0) {
      offset = document.cookie.indexOf(search)
      if(offset != -1) {
         offset += search.length
         end = document.cookie.indexOf(";", offset)
         if(end == -1) end = document.cookie.length
         return unescape(document.cookie.substring(offset, end))
      }
      else return ""
   }
}
function saveLoginType(){
	/*var login_select = document.getElementById("login_select");
	var sType = "";
	switch(login_select.selectedIndex){
		case 0:
			sType = "df";
			break;
		case 1:
			sType = "dm3";
			break;
		case 2:
			sType = "js3";
			break;
		case 3:
			sType = "dm";
			break;
		case 4:
			sType = "jy";
			break;
		default:
			sType = "jy";
	}
	document.cookie = "logType="+ sType +";expires="+  (new Date(2099,12,31)).toGMTString() +";domain=126.com";
	*/
}
var gAppName,gVersion;
function fVoidIE5(){
	fGetUserAgen();
	if(gAppName == "msie" && gVersion < 6){
		//var obj = document.getElementById("secure");
		//obj.checked = false;
		//obj.disabled = true;
	}
}
function fGetUserAgen(){
	var sUserAgent = window.navigator.userAgent;
	var sAppName = "";
	var sVersion = "";
	if(sUserAgent.indexOf("MSIE")>-1){
		sAppName   = "msie";	
		sVersion	= sUserAgent.replace(/.+MSIE/gi,"").replace(/;.+/gi,"") - 0;	
	}else if(sUserAgent.toUpperCase().indexOf("FIREFOX")>-1){
		sAppName = "firefox";
		sVersion = sUserAgent.replace(/.+Firefox\//gi,"").replace(/\(.*\)/g,"") - 0;
	}else if(sUserAgent.toUpperCase().indexOf("NETSCAPE")>-1){
		sAppName = "netscape";
		sVersion = sUserAgent.replace(/.+NETSCAPE\//gi,"").replace(/\(.*\)/g,"") - 0;
	}
	gAppName = sAppName; // ���������
	gVersion = sVersion; // �汾��
}
function $( id ){return document.getElementById( id );}
var gBodyHeight = null;
window.onresize = function(){
	gBodyHeight = document.documentElement.offsetHeight;
	var MainDiv = $("MainDiv");
	var h = gBodyHeight - 72 - 39;
	
	if(h < 323) h = 323;  //��Ч�߶ȳ����������򣬿��ʵ����Ӵ˸߶ȣ�Ĭ��Ϊ310
	if(h > 640 - 92) h = 640 - 92;
	MainDiv.style.height = h + "px";
	gBodyHeight = document.documentElement.offsetHeight;
	var UN = $("UN");
	if(gBodyHeight > 425){
		if(gBodyHeight < 660){
			if(document.all){
				UN.style.top = gBodyHeight - 4 + "px";
			}else{
				UN.style.top = gBodyHeight - 4 + 3 + "px";
			}
		}else{
			UN.style.top = 660 + "px";
		}
	}else{
		UN.style.top = 425 + "px";
	}
	// $("error_div").innerHTML = document.documentElement.offsetHeight;
};

var visitordata = new Cookie( document, "geoby_dealer_User", "geobyparagon.com");
visitordata.load();
/**
 * ��������
 */
function fPosition(oElement) {
	if(!oElement){
		var oElement = this;
	}
    var valueT = 0, valueL = 0;
    do {
      valueT += oElement.offsetTop  || 0;
      valueL += oElement.offsetLeft || 0;
      oElement = oElement.offsetParent;
    } while (oElement);
    return [valueL, valueT];
};
function isChildNode(e, id){
	var e = e || event;
	var target = e.target || e.srcElement;
	if(target.id == "Top_catchword_pic" || target.id == "Top_catchword_words") return true;
	while(target){
		if(target.id == id){
			return true;
		}
		if(target.parentNode){
			target = target.parentNode;
		}else{
			return false;
		}
	}
	return false;
}
function hideTips(){
	if(!window.t){
		t = window.setTimeout('$("J_div").style.display = "none";t=null;', 500);
	}
}
document.onmouseover = function(e){
	if(isChildNode(e, "J_div")){
		window.clearTimeout(t);
		t = null;
	}else{
		hideTips();
	}
}

	function fEvent(sType,oInput){
		switch (sType){
			case "focus" :
				oInput.isfocus = true;
				oInput.style.backgroundColor='#FFFFD8';
			case "mouseover" :
				oInput.style.borderColor = '#99E300';
				break;
			case "blur" :
				oInput.isfocus = false;
				oInput.style.backgroundColor="";
			case "mouseout" :
				if(!oInput.isfocus){
					oInput.style.borderColor='#A1BCA3';
				}
				break;
		}
	}


//-->
</script>
<style type="text/css">
div,p,ul,li{margin:0; padding:0;font:Arial, Helvetica, sans-serif; font-size:12px; color:#626262; text-align:center}
a.reg_2008{display:block; float:left;width:67px; height:23px;background-position:-4px -4px; border:0; color:#464646;line-height:23px; text-align:center; background-image:url(http://mimg.126.com/index/bg_x.jpg); text-decoration:none; margin-left:20px}
a.reg_2008:hover{color:#464646; text-decoration:none; background-position:-4px -30px}

</style>
</head>
<body style="margin: 0px;"> 
<form method="post" id="form"  name="form" runat="server" >
          <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
        </ajaxToolkit:ToolkitScriptManager>

   <!--��������-->
<div id="Top">
  <!--<div id="Top_catchword_pic" onmouseover="document.getElementById('J_div').style.display=''" onmouseout="hideTips()"></div>-->
  <!--<div id="Top_catchword_words" title="����126����ʡ����רҵ�����ʾ�"  onmouseover="document.getElementById('J_div').style.display=''" onmouseout="hideTips()"></div>-->
  <div id="Top_links"><a href="javascript:;" title="���ýݰ±�MESΪ�������ҳ" onClick="this.style.behavior='url(#default#homepage)';this.setHomePage('http://www.126.com');" style="color:#FFDF55">��Ϊ��ҳ</a> | <a href="http://www.geobyparagon.com" target="_blank">�ݰ±ȹٷ���վ</a> | <a href="http://geobyparagon.cn.alibaba.com/" target="_blank">����Ͱ͡��ݰ±������̳�</a> | <a href="http://shop35334500.taobao.com/" target="_blank">�Ա����ݰ±������̳�</a></div>

</div>
<!--����������-->
<div id="J_div" style="display:none;height:auto">
  <div id="J_title"><span>�ݰ±�MES</span></div>
  <div class="ge"></div>
  <div id="J_intr"> </div>

</div>
<!--�����¼��-->
<div id="MainDiv">
  <div id="Assis_Middle">
    <!--�������-->
    <div id="Main_Left">
	  <p class="ML_logo"><img src="images/Geobylogo.jpg" alt="" width="150" height="50" /></p>
    </div>
    <!--�м�����-->
    <div id="Main_Line"></div>
    <!--�ұ�����-->

    <div id="Main_Right">
    
    <span id="tips1"></span>
      
      <div class="ge Ge_h"></div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
    <ContentTemplate>
      <div class="Log_title">
        <div class="MR_Ltitle">��¼ϵͳ</div>
        <div class="error_div" id="error_div" runat="server"></div>
      </div>
      
        <input name="domain" type="hidden" value="geobyparagon.com" />
        <input name="language" type="hidden" value="zh-CN" />
        <input name="bCookie" type="hidden" value="" />
        <div class="ge" style="height:9px"></div>
        <p class="MR_pinp"><span>�û���</span>
          <input runat="server" type="text" id="user" name="user" onMouseOver="fEvent('mouseover',this)" onFocus="fEvent('focus',this)" onBlur="fEvent('blur',this)" onMouseOut="fEvent('mouseout',this)" onkeydown="  document.getElementById('error_div').innerHTML ='';" maxlength="20" tabindex="1"/>
          </p>
        <div class="ge"></div>
        <p class="MR_pinp LH"><span>�ܡ���</span>

          <input runat="server" id="pass" onMouseOver="fEvent('mouseover',this)" onFocus="fEvent('focus',this)" onBlur="fEvent('blur',this)" onMouseOut="fEvent('mouseout',this)" name="pass" type="password" size="16" tabindex="2"  maxlength="20" onkeydown=" document.getElementById('error_div').innerHTML ='';"/>
          <a href="#" target="_blank" style="margin-top:7px">����������?</a></p>
        <div class="ge"></div>
        <p class="MR_pinp LH"><span>��֤��</span><input id="vcode" onMouseOver="fEvent('mouseover',this)" onFocus="fEvent('focus',this)" onBlur="fEvent('blur',this)" onMouseOut="fEvent('mouseout',this)" onkeydown=" document.getElementById('error_div').innerHTML =''; if(event.keyCode==13)  document.getElementById('btnSubmit').focus();" type="text" size="20" name="vcode" autocomplete="off" tabindex="3"> <br />
<span style="padding-left:40px;"><img id="imgVerify" alt="" style="cursor:hand" onclick="this.src=this.src+'?'" title="�����壿�������" align="middle" src="VerifyCode.aspx?" width="120" height="35"/></span>
          
        </p>

        <div class="ge"></div>
        <p class="MR_check" style="margin-top:14px"><span class="wy">��</span>
          <input type="checkbox" value="" name="remUser" checked="checked" id="rem_U" />�ڴ˵����ϼ�ס�û���

        </p>
        
		<div class="ge"></div>
        <p class="MR_But" style="margin-top:8px"><span class="wy">��</span>

          <asp:Button ID="btnSubmit" CssClass="inp_L1" Text="�� ¼" onmouseover="this.className='inp_L2'" onmouseout="this.className='inp_L1'" TabIndex="4" OnClientClick="return fCheck();saveLoginType();" runat="server" OnClick="btnSubmit_Click" />
		  <!--<input type="submit" value="�� ¼" class="inp_L1"  onmouseover="this.className='inp_L2'" onmouseout="this.className='inp_L1'" id="input_btn1"  name="enter.x" tabindex="4" onClick="setCookie('ntes_mail_firstpage','normal');saveLoginType();" style="float:left" />-->
		  <!--<a href="http://reg.126.com/reg1.jsp?from=" target="_blank" title="ע��3G�������" id="lnkReg" class="reg_2008">ע ��</a>-->
         
        </p>
        <script type="text/javascript">
        fInitUser();
        //fSetLogType();
        fVoidIE5();
        </script>
      </ContentTemplate>
        <Triggers >
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
        </Triggers>
     </asp:UpdatePanel>  
    </div>
  </div>
</div>
<!--������Ϣ-->

<div id="UN">

  <!--�ײ�����-->
  <div id="Footer"><a href="http://www.geobyev.com/cn/aboutGeoby.aspx" target="_blank">���ڽݰ±�</a>&nbsp;&nbsp;<a href="http://eCommerce.geobyev.com" target="_blank">�����̳�</a>&nbsp;&nbsp;<%--<a href="http://safesurf.china.cn/data_form.php" target="_blank">�ٱ�Υ����Ϣ</a>&nbsp;&nbsp;--%><a href="http://www.geobyev.com/cn/Service.aspx" target="_blank">�ͻ�����</a><br />
    <%--<a href="http://corp.163.com/gb/legal/legal.html" target="_blank">��ط���</a> |--%> �ݰ±Ȱ�Ȩ���� <span class="fcopy">&copy; </span>2007-2010 <br/>������1024X768��IE7���������Ϊ������ģʽ</div>

</div>
</form>
</body>
<script language="Javascript" type="text/javascript">
<!--
function setCookie(name, value, isForever) {
    var Days = 30; //�� cookie �������� 30 ��
    //var value= 159;
    var exp  = new Date();    //new Date("December 31, 9998");
    exp.setTime(exp.getTime() + Days*24*60*60*1000);
    document.cookie =name +"="+ escape (value) + ";expires=" + exp.toGMTString();

    //alert(name + "=" + escape(value) + ";domain=geobypargon.com"  + (isForever?";expires="+  (new Date(2099,12,31)).toGMTString():""));
	//document.cookie = name + "=" + escape(value) + ";domain=geobypargon.com"  + (isForever?";expires="+  (new Date(2099,12,31)).toGMTString():"");
}

var sRef = location.search;
var ArrFrom = sRef.split("from=");
if(ArrFrom[1] == null || ArrFrom[1] == "undefined"){
	ArrFrom[1] = "";
}
var gFrom = ArrFrom[1];
//document.getElementById("lnkReg").href += gFrom;
window.onresize();
//-->
</script>

</html>