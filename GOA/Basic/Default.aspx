<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Default.aspx.cs" Inherits="ImageEditDemo._Default"
    AspCompat="true"%>

<html xmlns:v>
<head runat="server"> 
<title>ͼƬ�༭</title>
<base target='_self'>
<script src="../JScript/jquery-1.4.2.min.js"type="text/javascript"></script>
<script src="../JScript/ChooseColor.js" type="text/javascript"></script>
<link href="../styles/Style2.css" type="text/css" rel="Stylesheet" />
<link href="../styles/ChooseColor.css" type="text/css" rel="Stylesheet" />
<style runat="server">
  
      
v\:*{behavior:url(#default#VML);}   /*����VΪVML����*/
a{text-Decoration:none;color:white}
a:hover{text-Decoration:underline;color:yellow;}
td{font-size:12px;color:white}

</style>

</head>
<script language="javascript" type="text/javascript">
var InitImgPath='<%=strImgPath %>'
var drgEditPanelWidth=<%=iEditWidth %>
var drgEditPanelHeight=<%=iEditHeight %>
var ogiImgWidth=<%=ogiImgWidth %>
var ogiImgHeight=<%=ogiImgHeight %>
</script>
<script src="../JScript/PreviewImage.js" type="text/javascript"></script>

<body oncontextmenu='return false'   onselectstart='if(event.srcElement.tagName!="TEXTAREA"&&event.srcElement.tagName!="INPUT")return false'  onload="PreviewImage('editBodyImg',ogiImgWidth,ogiImgHeight);">

<iframe id=web src="about:blank" style="display:none"></iframe>  <!--ʵ�ֱ���-->
<div class="Navgation"><a onclick="InitImageEdit.Save()">�洢</a><a onclick="InitImageEdit.Close()">�ر�</a><span  class="tipMsg" id="tipMsg"></span></div>
<table border="0" cellpadding="0" cellspacing="0"><tr><td class="editBodyLeft">
<div >
<div  id="penTools" style=" margin-top:0">
<span class="toolsTitle">����ѡ��</span>
<a  onClick="InitImageEdit.ChoosePen(1)" class="GetChoose">�����ƶ�</a>
<a  onClick="InitImageEdit.ChoosePen(3)" >����</a>
<a  onClick="InitImageEdit.ChoosePen(4)" >�ֱ�</a>
<a  onClick="InitImageEdit.ChoosePen(16)" >��ͷ</a>
<a  onClick="InitImageEdit.ChoosePen(17)" >����</a>
<a  onClick="InitImageEdit.ChoosePen(6)" >Բ��</a>
<a  onClick="InitImageEdit.ChoosePen(7)" >������</a>
<a  onClick="InitImageEdit.ChoosePen(8)" >԰����</a>
<a  onClick="InitImageEdit.ChoosePen(9)" >����</a>
<a  onClick="InitImageEdit.ChoosePen(10)" >ͼƬ</a>
<a  onClick="InitImageEdit.ChoosePen(11)" >���</a>
<a  onClick="InitImageEdit.ChoosePen(12)">����</a>
<a  onClick="InitImageEdit.ChoosePen(14)" >�߼�</a>
<a  onClick="InitImageEdit.ChoosePen(-1)" >ȫ�����</a>
</div>
<div id="penParameters">
<span class="toolsTitle">���ʻ�������</span>
�ֶ� <select  id="penWidth" onchange="InitPen.changBorder(this)"><option selected>1<script>for(i=2;i<101;i++)document.write("<option>"+i)</script></select><br>
��ɫ <input id="penColor"  value="#000000" onclick="ShowChooseColor(this)"><br>
���� <input id="penBgColor" value="#FFFFFF"  onclick="ShowChooseColor(this)"><br>

</div>
<div id="fillingParameters"  style="display:none">
<span class="toolsTitle">����������</span>
��ͨ���� <input  id="fillColor" value="#000000" onclick="ShowChooseColor(this)"><br>
ʹ�ý��䱳��<input type='checkbox' id="usegradient" style="width:20px"><br>
����ɫһ <input  id="gradientColor1" value="#ff0000" onclick="ShowChooseColor(this)">
<br>
����ɫ�� <input id="gradientColor2" value="blue" onclick="ShowChooseColor(this)"><br>
���½��� <input type=radio name='gradientType' checked style="width:20px"><br>
б�򽥱�<input type=radio name='gradientType' style="width:20px">
</div>
<div id="stereoParameters" style="display:none">
<span class="toolsTitle" >�����������</span>
�����չ <select  id="stereoBefor"><option selected value="20">20</option><script>for(i=0;i<101;i++)document.write("<option value="+i+">"+i+"</option>")</script></select><br>
ǰ����չ <select  id="stereoBack"><option selected value="0">0</option><script>for(i=0;i<101;i++)document.write("<option value="+i+">"+i+"</option>")</script></select><br>
ƫ����� <select  id="stereoLeft"><option selected value="0">0</option><script>for(i=0;i<101;i++)document.write("<option value="+i+">"+i+"</option>")</script></select><br>
ƫ���ϱ� <select id="stereoTop"><option selected value="0">0</option><script>for(i=0;i<101;i++)document.write("<option value="+i+">"+i+"</option>")</script></select><br>
������ɫ <input  id="stereoColor" value="#ff0000" onclick="ShowChooseColor(this)"><br>
</div></div>
<
<div id="Globaltools" >
<span class="toolsTitle">ȫ�ֹ���</span>
<input type='button' value='����' onclick="InitImageEdit.Revoked()">
<input type='button' value='������' onclick="InitImageEdit.Antirevoked()">
<input type='button' value='�Ŵ�' onclick="InitImageEdit.ZoomIn(1.2)">
<input type='button' value='��С'onclick="InitImageEdit.ZoomIn(0.8)">
<input type='button' value='��ԭ'onclick="InitImageEdit.Reduction()">

</div></td>





<td class="editBodyRight">
<div class="EditBody" id="DivEditBody" >
<image  style="filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale);"  style="filter:alpha(opacity=40); border:solid 1px black"  id="editBodyImg"  /> 
</div

</td></tr></table>

<script language="javascript" type="text/javascript">


  
</script>

<script src="../JScript/ImageEdit.js" type="text/javascript"></script>

</body>

</html>