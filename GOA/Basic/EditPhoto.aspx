<%@ Page Language="C#"  AutoEventWireup="true"  Codebehind="EditPhoto.aspx.cs"  Inherits="GOA.Basic.EditPhoto" AspCompat="true"%>
<html> 
<head runat="server"> 
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" /> 
<title>ͼƬ�ϴ�Ԥ�� ie6,7,8, firefox</title> 
<script src="../JScript/jquery-1.4.2.min.js"type="text/javascript"></script>
<script src="../JScript/ChooseColor.js" type="text/javascript"></script>

<link href="../styles/Style2.css" type="text/css" rel="Stylesheet" />
<link href="../styles/ChooseColor.css" type="text/css" rel="Stylesheet" />

<style id="Style1" runat="server">
  
      
v\:*{behavior:url(#default#VML);}   /*����VΪVML����*/
a{text-Decoration:none;color:white}
a:hover{text-Decoration:underline;color:yellow;}
td{font-size:12px;color:white}

</style>



</head>
<script language="javascript">

   var InitImgPath='<%=strImgPath %>'
   var drgEditPanelWidth=<%=iEditWidth %>
   var drgEditPanelHeight=<%=iEditHeight %>
   var ogiImgWidth=<%=ogiImgWidth %>
   var ogiImgHeight=<%=ogiImgHeight %>
   
</script> 
<script src="../JScript/PreviewImage.js" type="text/javascript"></script>
 <body  onload="PreviewImage('Preview','chooseImg',this,120,120);"  oncontextmenu='return false'   onselectstart='if(event.srcElement.tagName!="TEXTAREA"&&event.srcElement.tagName!="INPUT")return false' > 
 <iframe id=web src="about:blank" style="display:none"></iframe>  <!--ʵ�ֱ���-->
<div class="Navgation"><a onclick="InitImageEdit.Save()">�惦</a><a onclick="InitImageEdit.Close()">�P�]</a><span  class="tipMsg" id="tipMsg"></span></div>
<table border="0" cellpadding="0" cellspacing="0"><tr><td class="editBodyLeft">
<div >
<div  id="penTools" style=" margin-top:0">
<span class="toolsTitle">���P�x��</span>
<a  onClick="InitImageEdit.ChoosePen(1)"   class="GetChoose">�����Ƅ�</a>
<a  onClick="InitImageEdit.ChoosePen(3)" >����</a>
<a  onClick="InitImageEdit.ChoosePen(4)" >䓹P</a>
<a  onClick="InitImageEdit.ChoosePen(16)" >���^</a>
<a  onClick="InitImageEdit.ChoosePen(17)" >���P</a>
<a  onClick="InitImageEdit.ChoosePen(6)" >�A��</a>
<a  onClick="InitImageEdit.ChoosePen(7)" >�L����</a>
<a  onClick="InitImageEdit.ChoosePen(8)" >�A����</a>
<a  onClick="InitImageEdit.ChoosePen(9)" >����</a>
<a  onClick="InitImageEdit.ChoosePen(10)" >�DƬ</a>
<a  onClick="InitImageEdit.ChoosePen(11)" >���</a>
<a  onClick="InitImageEdit.ChoosePen(12)">���w</a>
<a  onClick="InitImageEdit.ChoosePen(14)" >�߼�</a>
<a  onClick="InitImageEdit.ChoosePen(-1)" >ȫ�����</a>
</div>
<div id="penParameters">
<span class="toolsTitle">���P��������</span>
�P߅�ֶ� <select  id="penWidth" onchange="InitPen.changBorder(this)"><option selected>1<script>for(i=2;i<101;i++)document.write("<option>"+i)</script></select><br>
���P�ɫ <input id="penColor"  value="#000000" onclick="ShowChooseColor(this)"><br>
���P���� <input id="penBgColor" value="#FFFFFF"  onclick="ShowChooseColor(this)"><br>

</div>
<div id="fillingParameters"  style="display:none">
<span class="toolsTitle">����������</span>
��ͨ���� <input  id="fillColor" value="#000000" onclick="ShowChooseColor(this)"><br>
ʹ�Ýu׃����<input type='checkbox' id="usegradient" style="width:20px"><br>
�u׃ɫһ <input  id="gradientColor1" value="#ff0000" onclick="ShowChooseColor(this)">
<br>
�u׃ɫ�� <input id="gradientColor2" value="blue" onclick="ShowChooseColor(this)"><br>
���u׃ <input type=radio name='gradientType' checked style="width:20px"><br>
б��u׃ <input type=radio name='gradientType' style="width:20px">
</div>
<div id="stereoParameters" style="display:none">
<span class="toolsTitle" >���w��������</span>
������չ <select  id="stereoBefor"><option selected value="20">20</option><script>for(i=0;i<101;i++)document.write("<option value="+i+">"+i+"</option>")</script></select><br>
ǰ����չ <select  id="stereoBack"><option selected value="0">0</option><script>for(i=0;i<101;i++)document.write("<option value="+i+">"+i+"</option>")</script></select><br>
ƫ����߅ <select  id="stereoLeft"><option selected value="0">0</option><script>for(i=0;i<101;i++)document.write("<option value="+i+">"+i+"</option>")</script></select><br>
ƫ����߅ <select id="stereoTop"><option selected value="0">0</option><script>for(i=0;i<101;i++)document.write("<option value="+i+">"+i+"</option>")</script></select><br>
���w�ɫ <input  id="stereoColor" value="#ff0000" onclick="ShowChooseColor(this)"><br>
</div></div>
<
<div id="Globaltools" >
<span class="toolsTitle">ȫ�ֹ���</span>
<input type='button' value='���N' onclick="InitImageEdit.Revoked()">
<input type='button' value='�����N' onclick="InitImageEdit.Antirevoked()">
<input type='button' value='�Ŵ�' onclick="InitImageEdit.ZoomIn(1.2)">
<input type='button' value='�sС'onclick="InitImageEdit.ZoomIn(0.8)">
<input type='button' value='߀ԭ'onclick="InitImageEdit.Reduction()">

</div></td>
<td class="editBodyRight">
<img id="Preview"  style="filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale);" />
<div class="EditBody" id="DivEditBody" style="position:absolute; overflow:hidden; left:140; top:30;">
<image    style="position:absolute; left:0; top:0 ;"  id="chooseImg"  style="filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale);"/>
</div>

</td>

</tr></table>
<script src="../JScript/ImageEdit.js" type="text/javascript"></script>
</body> 

</html> 