<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Default.aspx.cs" Inherits="ImageEditDemo._Default"
    AspCompat="true"%>

<html xmlns:v>
<head runat="server"> 
<title>图片编辑</title>
<base target='_self'>
<script src="../JScript/jquery-1.4.2.min.js"type="text/javascript"></script>
<script src="../JScript/ChooseColor.js" type="text/javascript"></script>
<link href="../styles/Style2.css" type="text/css" rel="Stylesheet" />
<link href="../styles/ChooseColor.css" type="text/css" rel="Stylesheet" />
<style runat="server">
  
      
v\:*{behavior:url(#default#VML);}   /*声明V为VML变量*/
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

<iframe id=web src="about:blank" style="display:none"></iframe>  <!--实现保存-->
<div class="Navgation"><a onclick="InitImageEdit.Save()">存储</a><a onclick="InitImageEdit.Close()">关闭</a><span  class="tipMsg" id="tipMsg"></span></div>
<table border="0" cellpadding="0" cellspacing="0"><tr><td class="editBodyLeft">
<div >
<div  id="penTools" style=" margin-top:0">
<span class="toolsTitle">画笔选择</span>
<a  onClick="InitImageEdit.ChoosePen(1)" class="GetChoose">对象移动</a>
<a  onClick="InitImageEdit.ChoosePen(3)" >弧形</a>
<a  onClick="InitImageEdit.ChoosePen(4)" >钢笔</a>
<a  onClick="InitImageEdit.ChoosePen(16)" >箭头</a>
<a  onClick="InitImageEdit.ChoosePen(17)" >画笔</a>
<a  onClick="InitImageEdit.ChoosePen(6)" >圆形</a>
<a  onClick="InitImageEdit.ChoosePen(7)" >长方形</a>
<a  onClick="InitImageEdit.ChoosePen(8)" >园矩形</a>
<a  onClick="InitImageEdit.ChoosePen(9)" >文字</a>
<a  onClick="InitImageEdit.ChoosePen(10)" >图片</a>
<a  onClick="InitImageEdit.ChoosePen(11)" >填充</a>
<a  onClick="InitImageEdit.ChoosePen(12)">立体</a>
<a  onClick="InitImageEdit.ChoosePen(14)" >高级</a>
<a  onClick="InitImageEdit.ChoosePen(-1)" >全部清除</a>
</div>
<div id="penParameters">
<span class="toolsTitle">画笔基本参数</span>
粗度 <select  id="penWidth" onchange="InitPen.changBorder(this)"><option selected>1<script>for(i=2;i<101;i++)document.write("<option>"+i)</script></select><br>
颜色 <input id="penColor"  value="#000000" onclick="ShowChooseColor(this)"><br>
背景 <input id="penBgColor" value="#FFFFFF"  onclick="ShowChooseColor(this)"><br>

</div>
<div id="fillingParameters"  style="display:none">
<span class="toolsTitle">填充基本参数</span>
普通背景 <input  id="fillColor" value="#000000" onclick="ShowChooseColor(this)"><br>
使用渐变背景<input type='checkbox' id="usegradient" style="width:20px"><br>
渐变色一 <input  id="gradientColor1" value="#ff0000" onclick="ShowChooseColor(this)">
<br>
渐变色二 <input id="gradientColor2" value="blue" onclick="ShowChooseColor(this)"><br>
上下渐变 <input type=radio name='gradientType' checked style="width:20px"><br>
斜向渐变<input type=radio name='gradientType' style="width:20px">
</div>
<div id="stereoParameters" style="display:none">
<span class="toolsTitle" >立体基本参数</span>
向后伸展 <select  id="stereoBefor"><option selected value="20">20</option><script>for(i=0;i<101;i++)document.write("<option value="+i+">"+i+"</option>")</script></select><br>
前向伸展 <select  id="stereoBack"><option selected value="0">0</option><script>for(i=0;i<101;i++)document.write("<option value="+i+">"+i+"</option>")</script></select><br>
偏移左边 <select  id="stereoLeft"><option selected value="0">0</option><script>for(i=0;i<101;i++)document.write("<option value="+i+">"+i+"</option>")</script></select><br>
偏移上边 <select id="stereoTop"><option selected value="0">0</option><script>for(i=0;i<101;i++)document.write("<option value="+i+">"+i+"</option>")</script></select><br>
立体颜色 <input  id="stereoColor" value="#ff0000" onclick="ShowChooseColor(this)"><br>
</div></div>
<
<div id="Globaltools" >
<span class="toolsTitle">全局工具</span>
<input type='button' value='撤销' onclick="InitImageEdit.Revoked()">
<input type='button' value='反撤销' onclick="InitImageEdit.Antirevoked()">
<input type='button' value='放大' onclick="InitImageEdit.ZoomIn(1.2)">
<input type='button' value='缩小'onclick="InitImageEdit.ZoomIn(0.8)">
<input type='button' value='还原'onclick="InitImageEdit.Reduction()">

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