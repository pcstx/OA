<%@ Page Language="C#"  AutoEventWireup="true"  Codebehind="EditPhoto.aspx.cs"  Inherits="GOA.Basic.EditPhoto" AspCompat="true"%>
<html> 
<head runat="server"> 
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" /> 
<title>图片上传预览 ie6,7,8, firefox</title> 
<script src="../JScript/jquery-1.4.2.min.js"type="text/javascript"></script>
<script src="../JScript/ChooseColor.js" type="text/javascript"></script>

<link href="../styles/Style2.css" type="text/css" rel="Stylesheet" />
<link href="../styles/ChooseColor.css" type="text/css" rel="Stylesheet" />

<style id="Style1" runat="server">
  
      
v\:*{behavior:url(#default#VML);}   /*声明V为VML变量*/
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
 <iframe id=web src="about:blank" style="display:none"></iframe>  <!--实现保存-->
<div class="Navgation"><a onclick="InitImageEdit.Save()">存儲</a><a onclick="InitImageEdit.Close()">關閉</a><span  class="tipMsg" id="tipMsg"></span></div>
<table border="0" cellpadding="0" cellspacing="0"><tr><td class="editBodyLeft">
<div >
<div  id="penTools" style=" margin-top:0">
<span class="toolsTitle">畫筆選擇</span>
<a  onClick="InitImageEdit.ChoosePen(1)"   class="GetChoose">對象移動</a>
<a  onClick="InitImageEdit.ChoosePen(3)" >弧形</a>
<a  onClick="InitImageEdit.ChoosePen(4)" >鋼筆</a>
<a  onClick="InitImageEdit.ChoosePen(16)" >箭頭</a>
<a  onClick="InitImageEdit.ChoosePen(17)" >畫筆</a>
<a  onClick="InitImageEdit.ChoosePen(6)" >圓形</a>
<a  onClick="InitImageEdit.ChoosePen(7)" >長方形</a>
<a  onClick="InitImageEdit.ChoosePen(8)" >圓矩形</a>
<a  onClick="InitImageEdit.ChoosePen(9)" >文字</a>
<a  onClick="InitImageEdit.ChoosePen(10)" >圖片</a>
<a  onClick="InitImageEdit.ChoosePen(11)" >填充</a>
<a  onClick="InitImageEdit.ChoosePen(12)">立體</a>
<a  onClick="InitImageEdit.ChoosePen(14)" >高級</a>
<a  onClick="InitImageEdit.ChoosePen(-1)" >全部清除</a>
</div>
<div id="penParameters">
<span class="toolsTitle">畫筆基本參數</span>
筆邊粗度 <select  id="penWidth" onchange="InitPen.changBorder(this)"><option selected>1<script>for(i=2;i<101;i++)document.write("<option>"+i)</script></select><br>
畫筆顏色 <input id="penColor"  value="#000000" onclick="ShowChooseColor(this)"><br>
畫筆背景 <input id="penBgColor" value="#FFFFFF"  onclick="ShowChooseColor(this)"><br>

</div>
<div id="fillingParameters"  style="display:none">
<span class="toolsTitle">填充基本參數</span>
普通背景 <input  id="fillColor" value="#000000" onclick="ShowChooseColor(this)"><br>
使用漸變背景<input type='checkbox' id="usegradient" style="width:20px"><br>
漸變色一 <input  id="gradientColor1" value="#ff0000" onclick="ShowChooseColor(this)">
<br>
漸變色二 <input id="gradientColor2" value="blue" onclick="ShowChooseColor(this)"><br>
上下漸變 <input type=radio name='gradientType' checked style="width:20px"><br>
斜向漸變 <input type=radio name='gradientType' style="width:20px">
</div>
<div id="stereoParameters" style="display:none">
<span class="toolsTitle" >立體基本參數</span>
向後伸展 <select  id="stereoBefor"><option selected value="20">20</option><script>for(i=0;i<101;i++)document.write("<option value="+i+">"+i+"</option>")</script></select><br>
前向伸展 <select  id="stereoBack"><option selected value="0">0</option><script>for(i=0;i<101;i++)document.write("<option value="+i+">"+i+"</option>")</script></select><br>
偏移左邊 <select  id="stereoLeft"><option selected value="0">0</option><script>for(i=0;i<101;i++)document.write("<option value="+i+">"+i+"</option>")</script></select><br>
偏移上邊 <select id="stereoTop"><option selected value="0">0</option><script>for(i=0;i<101;i++)document.write("<option value="+i+">"+i+"</option>")</script></select><br>
立體顏色 <input  id="stereoColor" value="#ff0000" onclick="ShowChooseColor(this)"><br>
</div></div>
<
<div id="Globaltools" >
<span class="toolsTitle">全局工具</span>
<input type='button' value='撤銷' onclick="InitImageEdit.Revoked()">
<input type='button' value='反撤銷' onclick="InitImageEdit.Antirevoked()">
<input type='button' value='放大' onclick="InitImageEdit.ZoomIn(1.2)">
<input type='button' value='縮小'onclick="InitImageEdit.ZoomIn(0.8)">
<input type='button' value='還原'onclick="InitImageEdit.Reduction()">

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