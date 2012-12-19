<HTML xmlns:v>
<HEAD>
<META http-equiv="Content-Type" content="text/html; Charset=gb2312">
<META name="GENERATOR" content="网络程序员伴侣(Lshdic)2004">
<META name="GENERATORDOWNLOADADDRESS" content="http://www.lshdic.com/downlshdic.asp">
<META NAME="KEYWORDS" CONTENT="Vml图像画板">
<title>蓝丽网 - Vml图像画板.2003.9</title>
<STYLE>
v\:*{behavior:url(#default#VML);}   /*声明V为VML变量*/
a{text-Decoration:none;color:white}
a:hover{text-Decoration:underline;color:yellow;}
td{font-size:12px;color:white}
.bon1{border-bottom:1 solid eeeeee;border-right:1 solid eeeeee;border-left:1 solid gray;border-top:1 solid gray;background-color:#619CE7;color:yellow;width:54}
.bon2{border-bottom:1 solid gray;border-right:1 solid gray;border-left:1 solid eeeeee;border-top:1 solid eeeeee;background-color:#619CE7;color:white;width:54}
</STYLE>
</HEAD>
<BODY oncontextmenu='return false' style='margin:1;cursor:default' vlink=#3732CD link='#3732CD' onselectstart='if(event.srcElement.tagName!="TEXTAREA"&&event.srcElement.tagName!="INPUT")return false' onhelp='if(help.style.display=="none"){bangzhu.click()}else{guanbibangzhu.click()};return false'>
<v:Line style='position:absolute;z-index:2000;display:none' id='line1'>  <!--钢笔可视化-->
<v:Stroke dashstyle='shortdash'/>
</v:line>
<v:arc startangle='-200' endangle='30' style='position:absolute;z-index:2000;display:none' id='arc1'>  <!--弧形可视化-->
<v:Stroke dashstyle='shortdash'/>
</v:arc>
<v:Oval style='position:absolute;z-index:2000;display:none' id='oval1'>  <!--圆形可视化-->
<v:Stroke dashstyle='shortdash'/>
</v:oval>
<v:rect style='position:absolute;z-index:2000;display:none' id='rect1'>  <!--长方形可视化-->
<v:Stroke dashstyle='shortdash'/>
</v:rect>
<v:roundrect style='position:absolute;z-index:2000;display:none' id='roundrect1'>  <!--圆锯形可视化-->
<v:Stroke dashstyle='shortdash'/>
</v:roundrect>
<span style='position:absolute;z-index:2000;display:none' id='wenzi1'>   <!--插入文字可视化-->
<textarea id='txt1' style='border:1 solid black;width:300;height:100'></textarea><br>
<center>字体:<select style="width:100;" id=wenziziti onchange='gengxinwenzi.click()'><option selected>宋体<option>黑体<option>隶书<option>幼圆<option>楷体_GB2312<option>仿宋_GB2312<option>华文中宋<option>华文行楷<option>华文新魏<option>华文细黑<option>华文彩云<option>方正姚体<option>方正舒体<option>Wingdings<option>Wingdings 2<option>Wingdings 3<option>Webdings<option>System<option>@宋体<option>@黑体<option>@隶书<option>@幼圆<option>@楷体_GB2312<option>@仿宋_GB2312<option>@华文中宋<option>@华文行楷<option>@华文新魏<option>@华文细黑<option>@华文彩云<option>@方正姚体<option>@方正舒体<option>@System</select>有无边框<input type='checkbox' id='wenzibiankuang'><br>
颜色:<input type='text' style='border:1 solid black;width:50;height:17;' value='#000000' onmousedown='colortab.style.left=event.x-50;colortab.style.top=event.y+this.offsetHeight;rndcolor(this.id)' id='wenziyanse' onmousemove='gengxinwenzi.click()'>背景:<input type='text' style='border:1 solid black;width:50;height:17;' value='#FFFFFF' onmousedown='colortab.style.left=event.x-50;colortab.style.top=event.y+this.offsetHeight;rndcolor(this.id)' id='wenzibeijing' onmousemove='gengxinwenzi.click()'>大小:<select style="width:50;" id=wenzidaxiao onchange='gengxinwenzi.click()'><option selected>12<script>for(i=1;i<101;i++)document.write("<option>"+i)</script></select><br>
<input type='button' value='浏览更新' class="bon2" onclick="txt1.style.color=wenziyanse.value;txt1.style.backgroundColor=wenzibeijing.value;txt1.style.fontSize=wenzidaxiao.options[wenzidaxiao.selectedIndex].text;txt1.style.fontFamily=wenziziti.options[wenziziti.selectedIndex].text;" id='gengxinwenzi'><input type='button' value='插入' class="bon2" onclick="charuwenzi()"><input type='button' value='取消' class="bon2" onclick="wenzi1.style.display='none'">
</span>
<span style='position:absolute;z-index:2000;display:none' id='tupian1'>   <!--插入图片可视化-->
<input type='file' id='file1' style='width:400'><br>
<center><select id='duibi'><option selected>默认对比度<script>for(i=0;i<101;i++)document.write("<option>"+i)</script></select><select id='secai'><option selected>默认色彩度<script>for(i=0;i<9.9;i+=0.1)document.write("<option>"+new Number(i).toFixed(1))</script><option>10</select><select id=liangdu><option>默认亮度<script>for(i=-0.5;i<0.6;i+=0.1)document.write("<option>"+new Number(i).toFixed(1))</script></select><input type='checkbox' onclick="duibi.disabled=this.checked;secai.disabled=this.checked;liangdu.disabled=this.checked" id=heibai><label for='heibai'>黑白效果</label><br>
<input type='button' value='插入' class="bon2" onclick="charutupian()">
</span>
<span style='position:absolute;z-index:2000;display:none' id='gaoji1'>   <!--高级改可视化-->
<textarea id='txt2' style='border:1 solid black;width:400;height:150'></textarea><br>
<center><input type='button' value='修改' class="bon2" onclick="gaojiobj.outerHTML=txt2.value;gaojiobj=null;gaoji1.style.display='none'">
</span>
<span style='position:absolute;z-index:2000;display:none' id='yuandaima'>   <!--所有原代码-->
<textarea id='txt3' style='border:1 solid black;width:600;height:400'></textarea><br>
<center><input type='button' value='复制' class="bon2" onclick="window.clipboardData.setData('text',txt3.value);alert('已将数据复制到系统剪切板')"> <input type='button' value='更新修改' class="bon2" onclick="div1.innerHTML=txt3.value;yuandaima.style.display='none'"> <input type='button' value='取消' class="bon2" onclick="yuandaima.style.display='none'">
</span>
<span style='position:absolute;z-index:2000;display:none' id='menu1'>    <!--弹出菜单-->
<input type=button class=bon2 value=置前 onclick='zz+=1;thisobj.style.zIndex=zz;menu1.style.display="none"'><br>
<input type=button class=bon2 value=置后 onclick='zz2-=1;thisobj.style.zIndex=zz2;menu1.style.display="none"'><br>
<input type=button class=bon2 value=复制 onclick='div1.innerHTML+=thisobj.outerHTML;alert("复制完成，请使用选移功能拖动");menu1.style.display="none"' id=fuzhi><br>
<input type=button class=bon2 value=删除 onclick='thisobj.outerHTML="";menu1.style.display="none"' id=shanchu><br>
</span>
<iframe id=web src="about:blank" style="display:none"></iframe>  <!--实现保存-->
<span style='position:absolute;z-index:2000;display:none' id='help'>   <!--帮助信息-->
<textarea readonly='true' style='border:1 solid black;width:700;height:400'>
VML图像画板.2003.9（操作帮助及功能简介）

目前最强的网页VML(网页矢量图形标记语言)编辑工具之一，VML学习者的最佳学习工具
能够完成基本的三维网页的设计，做图及图形处理功能2003.9版相对2003版有了较强的提高（原作者今后有时间会去强化）
基本的画笔、图形处理、全局工具在右方，点选后在画板内使用鼠标左键应用
可将画板内VML代码图形保存为文件，亦可通过“修改原代码”实现“打开文件”等功能
新增功能:
快捷键:CTRL+Z=撤消,CTRL+Y=反撤消,上下左右键=上下左右移,CTRL+上=放大,CTRL+下=缩小,F1=帮助
误操作:关于绘图时单击及鼠标弹起时，如果未移动鼠标，不会在生成垃圾代码
记忆点1:2003版就有的PLAYLINE绘图功能，可惜不完善，仅能完成右下方向的绘图
记忆点2:2003.9版最新增加的连续线段绘图功能，支持8个方向的连续LINE线段，可惜仅为线段不支持背景
图片:就2003.8版新增加了针对JPG等静态图像的对比度，亮度调节的辅助设计功能等
文字化:将图形以文字填充，实现具有轨迹路线效果的文字

VML图像画板.2003.9（版权信息）

原作者:风云舞
主  页:<a href="http://www.lshdic.com" target="_blank">http://www.lshdic.com</a>
最新版:可登陆主页查看
发布于:2003年9月1日
版  本:2003.9（第三版）
问  题:任何VML技术问题可以登陆<a href="http://www.lshdic.com/bbs进行讨论" target="_blank">http://www.lshdic.com/bbs进行讨论</a>
授  权:授权给任何个人使用、应用，自由DHTML代码，可任意修改学习、转载、强化，作者制作本作品出发点是为了提高用户的VML编程水准、开拓一个先例，因而未加密及方便参考仅采用一页代码，为此，未经原作者同意请不要将VML图像画板任何版本用于其他商业用途，侵我版权毁我及作品名誉，公开于网站、软件发布及其他应用请保留原作者的这些声明（但可以追加其他信息，如修改者各项授权信息等）
</textarea><br>
<center><input type='button' value='关闭帮助' class="bon2" onclick="help.style.display='none'" id=guanbibangzhu>
</span>
<TABLE cellspacing=0 cellpadding=3 width=770 align=center bgcolor='#619CE7' style='border-right:3 dashed green;border-left:3 dashed #4735B0;border-top:1 solid blue;border-bottom:gray' id=allform1>
<tr align=center onmouseover='if(event.srcElement.tagName=="TD"&&event.srcElement.width==70)event.srcElement.bgColor="aaaaaa"' onmouseout='if(event.srcElement.tagName=="TD")event.srcElement.bgColor=""' style='cursor:hand'><td width=70 id=toptd1 onclick="yuandaima.x=event.x;yuandaima.y=event.y;txt3.value=div1.innerHTML.replace(/>/g,'>\n').replace(/ = /g,'=').replace(/\: /g,':').replace(/\; /g,';');yuandaima.style.display=''">
修改原代码</td><td width=70 onclick="web.document.write('<HTML xmlns:v>\n<HEAD>\n<META http-equiv=Content-Type content=text/html;charset=gb2312>\n<TITLE>我的杰作</TITLE>\n<META name=Gemeratpr content=蓝丽VML图形编辑器>\n<META name=GemeratprHomePage content=http://www.lshdic.com>\n<STYLE>\nv\\:*{behavior:url(#default#VML);}\n</STYLE>\n</HEAD>\n<BODY>\n'+div1.innerHTML+'\n</BODY>\n</HTML>');web.document.execCommand('SaveAs',false,'我的杰作')">保存为文件</td><td width=70 onclick="if(this.innerText=='最大化视图'){div1.style.width=document.body.offsetWidth-150;div1.style.height=document.body.offsetHeight-35;this.innerText='恢复视图'}else{div1.style.width=650;div1.style.height='100%';this.innerText='最大化视图'}">最大化视图</td><td width=70 onclick="help.x=event.x;help.y=event.y;help.style.display=''" id=bangzhu>操作帮助</td><td width=60> </td><td width=60> </td><td align=right>原作:风云舞,蓝丽程序员网络:<a href='http://www.lshdic.com' target='_blank'><a href="http://www.lshdic.com" target="_blank">http://www.lshdic.com</a></a>
</td></tr><tr>
<td width=100% colspan=10 height=500>
<table cellspacing=0 cellpadding=0 height=100%%><tr><td width=660>

<div style='width:650;height:100%;background-color:white;border:1 solid gray;color:black;' id=div1></div>
</td><td>
<div style='width:110;height:100%;'>
<center><b>画笔选择</b><br>
<button class=bon2 id=huabi>选移<button class=bon2 id=huabi>调大小<button class=bon2 id=huabi>弧形</button><button class=bon1 id=huabi>钢笔</button><button class=bon2 id=huabi>记忆点1</button><button class=bon2 id=huabi>圆型</button><button class=bon2 id=huabi>长方型</button><button class=bon2 id=huabi>圆矩型</button><button class=bon2 id=huabi>文字</button><button class=bon2 id=huabi>图片</button><button class=bon2 id=huabi>填充</button><button class=bon2 id=huabi>立体</button><button class=bon2 id=huabi>边框</button><button class=bon2 id=huabi>高级</button><button class=bon2 id=huabi>记忆点2</button><button class=bon2 id=huabi>文字化</button><br>
<b>画笔基本参数</b><br>
笔边粗度 <select style="width:54;" id=bibiancudu><option selected>1<script>for(i=2;i<101;i++)document.write("<option>"+i)</script></select><br>
画笔颜色 <input style='border:1 solid black;width:54;height:17;' value='#000000' id=huabiyanse onmousedown='colortab.style.left=event.x-50;colortab.style.top=event.y+this.offsetHeight;rndcolor(this.id)'><br>
画笔背景 <input style='border:1 solid black;width:54;height:17;' value='#FFFFFF' id=huabibeijing onmousedown='colortab.style.left=event.x-50;colortab.style.top=event.y+this.offsetHeight;rndcolor(this.id)'><br>
X Y 坐标 <input disabled='true' type='text' value='0,0' id='zuobiao' style='border:1 solid black;width:54;height:17;'><br>
<span id=tianchong1 style='display:none'>
<b> <br>填充基本参数</b><br>
普通背景 <input style='border:1 solid black;width:54;height:17;' value='' id=tianchongbeijing onmousedown='colortab.style.left=event.x-50;colortab.style.top=event.y+this.offsetHeight;rndcolor(this.id)'><br>使用渐变背景<input type='checkbox' id='usejianbian'><br>
渐变色一 <input style='border:1 solid black;width:50;height:17;color:red' value='#FF0000' id=jianbianse1 onmousedown='colortab.style.left=event.x-50;colortab.style.top=event.y+this.offsetHeight;rndcolor(this.id)'>
<br>渐变色二 <input style='border:1 solid black;width:50;height:17;' value='#FFFFFF' id=jianbianse2 onmousedown='colortab.style.left=event.x-50;colortab.style.top=event.y+this.offsetHeight;rndcolor(this.id)'><br>
上下渐变 <input type=radio name='jianbianyangshi' checked><br>
斜向渐变 <input type=radio name='jianbianyangshi'>
</span>
<span id=liti1 style='display:none'>
<b> <br>立体基本参数</b><br>
后向伸展 <select style="width:54;" id=houxiangshenzhan><option selected>20<script>for(i=0;i<101;i++)document.write("<option>"+i)</script></select><br>
<br>前向伸展 <select style="width:54;" id=qianxiangshenzhan><option selected>0<script>for(i=1;i<101;i++)document.write("<option>"+i)</script></select><br>
偏移左边 <select style="width:54;" id=pianyizuobian><option selected>0<script>for(i=1;i<101;i++)document.write("<option>"+i)</script></select><br>
偏移上边 <select style="width:54;" id=pianyishangbian><option selected>0<script>for(i=1;i<101;i++)document.write("<option>"+i)</script></select><br>
立体颜色 <input style='border:1 solid black;width:54;height:17;' value='' id=litiyanse  onmousedown='colortab.style.left=event.x-50;colortab.style.top=event.y+this.offsetHeight;rndcolor(this.id)'><br>
</span>
<span id=biankuang1 style='display:none'>
<b> <br>边框基本参数</b><br>
边框粗度 <select style="width:54;" id=biankuangcudu><option selected>1<script>for(i=0;i<101;i++)document.write("<option>"+i)</script></select><br>
<br>边框样式 <select style="width:54;" id=biankuangyangshi><option selected>none<option>dash<option>dashdot<option>dot<option>longdash<option>longdashdot<option>shortdash<option>shortdashdot<option>shortdashdotdot<option>longdashdotdot<option>shortdot</select><br>
起点尖头 <select style="width:54;" id=qidianjiantou><option selected>none<option>block<option>classic<option>diamond<option>open<option>oval</select><br>
终点尖头 <select style="width:54;" id=zhongdianjiantou><option selected>none<option>block<option>classic<option>diamond<option>open<option>oval</select><br>
边框颜色 <input style='border:1 solid black;width:54;height:17;' value='' id=biankuangyanse   onmousedown='colortab.style.left=event.x-50;colortab.style.top=event.y+this.offsetHeight;rndcolor(this.id)'><br>
</span>
<center><b>全局工具</b><br>
<input type='button' value='撤消' onclick='if(youbiao>0){div1.innerHTML=chexiao[youbiao];youbiao-=1;fanchexiao1=2}' class=bon2 id=chexiaock><input type='button' value='反撤消' onclick='if(youbiao<chexiao.length-fanchexiao1){youbiao+=fanchexiao1;fanchexiao1=1;div1.innerHTML=chexiao[youbiao];}' class=bon2 id=fanchexiaock><br>
<input type='button' value='放大' onclick='for(i=0;i<div1.all.length;i++){try{div1.all[i].style.left=parseInt(div1.all[i].style.left)-5;div1.all[i].style.top=parseInt(div1.all[i].style.top)-5;div1.all[i].style.width=parseInt(div1.all[i].style.width)+10;div1.all[i].style.height=parseInt(div1.all[i].style.height)+10;}catch(e){}}' class=bon2 id=fangda><input type='button' value='缩小' onclick='for(i=0;i<div1.all.length;i++){try{div1.all[i].style.left=parseInt(div1.all[i].style.left)+5;div1.all[i].style.top=parseInt(div1.all[i].style.top)+5;div1.all[i].style.width=parseInt(div1.all[i].style.width)-10;div1.all[i].style.height=parseInt(div1.all[i].style.height)-10;}catch(e){}}' class=bon2 id=suoxiao><br>
<input type='button' value='左移' onclick='for(i=0;i<div1.all.length;i++){try{div1.all[i].style.left=parseInt(div1.all[i].style.left)-10;}catch(e){}}' class=bon2 id=zuoyi><input type='button' value='右移' onclick='for(i=0;i<div1.all.length;i++){try{div1.all[i].style.left=parseInt(div1.all[i].style.left)+10;}catch(e){}}' class=bon2 id=youyi><br>
<input type='button' value='上移' onclick='for(i=0;i<div1.all.length;i++){try{div1.all[i].style.top=parseInt(div1.all[i].style.top)-10;}catch(e){}}' class=bon2 id=shangyi><input type='button' value='下移' onclick='for(i=0;i<div1.all.length;i++){try{div1.all[i].style.top=parseInt(div1.all[i].style.top)+10;}catch(e){}}' class=bon2 id=xiayi><br>
</div>
</td></tr></table>
</td></tr>
</table>
<table cellspacing=0 cellpadding=0 style='position:absolute;width:100;height:100;display:none;background-color:red;z-index:3000' id='colortab'><tr><td id='colorid'></td></tr></table>
<script language='jscript'>
var bi=4    //定义当前使用的画笔工具，3为铅笔
var color1='#000000',color2='#000000',size1=0  //定义缺剩的画笔颜色及画笔填充颜色和笔边粗度
var xx=0,yy=0,zz=1000  //定义缺剩的X及Y坐标
var isok=false  //区分画笔起点是否在DIV内，超出范围则无效
var moveobj=null,ckleft=0,cktop=0,ckwid=0,ckhei=0,ckto=""  //被移动物件、调整大小物件的引用
var poly1=null,oldvalue="",oldx=0,oldy=0  //为了完成记忆点，创建一个可折式线段
var gaojiobj=null  //为了完成“高级”功能，绑定被修改对象
var thisobj=null   //为了完成各种基本编辑功能，如“置前”“复制”“删除”等
var zz2=1000          //为完成“置后”功能，zz2为负数
var huxingtixing=false  //如果为false则ALERT（）提醒弧形使用高级修改，否则不提醒
var chexiao=new Array(),youbiao=0,fanchexiao1=2  //定义一个存储撤消内容的数组以及位移游标,fanchexiao1为0时+2，否则+1
function div1.onmousedown(){
div1.setCapture();
color1=huabiyanse.value;color2=huabibeijing.value;isok=true;size1=bibiancudu.options[bibiancudu.selectedIndex].text
xx=event.x;yy=event.y;zz+=1
for(i=0;i<huabi.length;i++){if(huabi[i].className=="bon1"){bi=i+1;break}}
line1.strokecolor=color1;line1.strokeweight=size1;oval1.strokeweight=size1;oval1.strokecolor=color1
oval1.fillcolor=color2;rect1.strokeweight=size1;rect1.strokecolor=color1
rect1.fillcolor=color2;roundrect1.strokeweight=size1;roundrect1.strokecolor=color1
roundrect1.fillcolor=color2;arc1.strokeweight=size1;arc1.strokecolor=color1
arc1.fillcolor=color2;
if(event.button==1){
switch(bi){
case 1:  //选移
if(event.srcElement.parentElement.id=="div1"){moveobj=event.srcElement;ckleft=xx-parseInt(moveobj.style.left);cktop=yy-parseInt(moveobj.style.top)}
break;
case 2:  //调大小
if(event.srcElement.parentElement.id=="div1"){moveobj=event.srcElement;
ckleft=parseInt(moveobj.style.left);cktop=parseInt(moveobj.style.top);ckwid=moveobj.offsetWidth;ckhei=moveobj.offsetHeight}
break;
case 3:  //弧形
arc1.style.left=event.x;arc1.style.top=event.y;arc1.style.width=0;arc1.style.height=0;arc1.style.display="";
break;
case 4:  //钢笔
line1.style.left=event.x;line1.style.top=event.y;line1.to="0,0";line1.style.display=""
break;
case 5:  //记忆点
if(poly1==null){
oldx=xx;oldy=yy
poly1=div1.appendChild(document.createElement("<v:polyline points='0 0 0 0' style='position:absolute;z-index:"+zz+";left:"+xx+";top:"+yy+";' strokecolor='"+color1+"' strokeweight='"+size1+"' fillcolor='"+color2+"'/>"))
}
if(oldx-tempx<1&&oldy-tempy<1)oldvalue=poly1.points.value.replace(/,/g,' ')
break;
case 6:  //圆形
oval1.style.left=event.x;oval1.style.top=event.y;oval1.style.width=0;oval1.style.height=0;oval1.style.display=""
break;
case 7:  //长方形
rect1.style.left=event.x;rect1.style.top=event.y;rect1.style.width=0;rect1.style.height=0;rect1.style.display=""
break;
case 8:  //圆矩形
roundrect1.style.left=event.x;roundrect1.style.top=event.y;roundrect1.style.width=0;roundrect1.style.height=0;roundrect1.style.display=""
break;
case 9:  //文字
wenzi1.style.left=event.x;wenzi1.style.top=event.y;wenzi1.style.display=""
break;
case 10:  //图片
tupian1.style.left=event.x;tupian1.style.top=event.y;tupian1.style.display=""
break;
case 15:  //记忆点2
oldx=xx;oldy=yy
poly1=div1.appendChild(document.createElement("<v:line to='0,0' style='position:absolute;z-index:"+zz+";left:"+xx+";top:"+yy+";' strokecolor='"+color1+"' strokeweight='"+size1+"'/>"))
}
}}
function div1.onmousemove(){
tempx=event.x;tempy=event.y;temp1=0;temp2=0
zuobiao.value=tempx-allform1.offsetLeft-8+","+parseInt(tempy-toptd1.offsetHeight-7)
if(bi==5&&poly1!=null){   //记忆点
if(oldx-tempx<0&&oldy-tempy<0)poly1.points.value=oldvalue+" "+(tempx-oldx)+" "+(tempy-oldy)
}else if(bi==15&&poly1!=null){   //记忆点2
poly1.to=(tempx-xx)+","+(tempy-yy)
}
if(event.button==1){
switch(bi){
case 1:  //选移
if(moveobj!=null){moveobj.style.left=tempx-ckleft;moveobj.style.top=tempy-cktop}
break;
case 2:  //调大小
if(moveobj!=null){
if(moveobj.tagName!="line"){
if(tempx>ckleft){moveobj.style.width=tempx-ckleft}else{moveobj.style.left=tempx;moveobj.style.width=ckleft-tempx}
if(tempy>cktop){moveobj.style.height=tempy-cktop}else{moveobj.style.top=tempy;moveobj.style.height=cktop-tempy}
}else{moveobj.to=parseInt(tempx-ckleft)+","+parseInt(tempy-cktop);if(ckto=="")ckto=parseInt(tempx-ckleft)+","+parseInt(tempy-cktop);}
}
break;
case 3:  //弧形
if(tempx-xx<0){arc1.style.left=tempx;arc1.style.width=(xx-tempx)}else{arc1.style.width=(tempx-xx)}
if(tempy-yy<0){arc1.style.top=tempy;arc1.style.height=(yy-tempy)}else{arc1.style.height=(tempy-yy)}
break;
case 4:  //钢笔
line1.to=(tempx-xx)+","+(tempy-yy)
break;
case 6:  //圆形
if(tempx-xx<0){oval1.style.left=tempx;oval1.style.width=(xx-tempx)}else{oval1.style.width=(tempx-xx)}
if(tempy-yy<0){oval1.style.top=tempy;oval1.style.height=(yy-tempy)}else{oval1.style.height=(tempy-yy)}
break;
case 7:  //长方形
if(tempx-xx<0){rect1.style.left=tempx;rect1.style.width=(xx-tempx)}else{rect1.style.width=(tempx-xx)}
if(tempy-yy<0){rect1.style.top=tempy;rect1.style.height=(yy-tempy)}else{rect1.style.height=(tempy-yy)}
break;
case 8:  //圆矩形
if(tempx-xx<0){roundrect1.style.left=tempx;roundrect1.style.width=(xx-tempx)}else{roundrect1.style.width=(tempx-xx)}
if(tempy-yy<0){roundrect1.style.top=tempy;roundrect1.style.height=(yy-tempy)}else{roundrect1.style.height=(tempy-yy)}
break;
}}}
function div1.onmouseup(){
savechexiao()
document.releaseCapture();if(isok==false){forerr();return false};isok=false;menu1.style.display='none'
tempx=event.x;tempy=event.y;divwid=div1.offsetWidth;divhei=div1.offsetHeight
if(tempx>allform1.offsetLeft+divwid+5||tempx<allform1.offsetLeft+5){forerr();return alert("X坐标越界")}
if(tempy>allform1.offsetTop+toptd1.offsetHeight+divhei+5||tempy<allform1.offsetTop+toptd1.offsetHeight+5){forerr();return alert("Y坐标越界")}
if(event.button==2&&bi==5&&poly1!=null){
poly1.points.value=oldvalue;oldvalue="";poly1=null;
}else if(event.srcElement.parentElement.id=="div1"&&event.button==2){menu1.style.left=tempx;menu1.style.top=tempy;menu1.style.display='';thisobj=event.srcElement}
else if(event.button==2&&bi==15&&poly1!=null){
poly1.outerHTML="";poly1=null
}
if(event.button==1){
switch(bi){
case 1:  //选移
if(moveobj!=null&&parseInt(moveobj.style.left)<allform1.offsetLeft+5){forerr();return alert("被移动物体X1超出界限")}
if(moveobj!=null&&parseInt(moveobj.style.left)+moveobj.offsetWidth-2>allform1.offsetLeft+divwid+5){forerr();return alert("被移动物体X2超出界限")}
if(moveobj!=null&&parseInt(moveobj.style.top)<allform1.offsetTop+toptd1.offsetHeight+5){forerr();return alert("被移动物体Y1超出界限")}
if(moveobj!=null&&parseInt(moveobj.style.top)+moveobj.offsetHeight-2>allform1.offsetTop+toptd1.offsetHeight+divhei+3){forerr();return alert("被移动物体Y2超出界限")}
moveobj=null
break;
case 2:  //调大小
moveobj=null;ckto=""
break;
case 3:  //弧形
arc1.style.display='none';
if (Math.abs(tempx-xx)<=1||Math.abs(tempy-yy)<=1)return false
div1.appendChild(document.createElement("<v:arc startangle='-200' endangle='30' style='position:absolute;z-index:"+zz+";left:"+arc1.style.left+";top:"+arc1.style.top+";width:"+arc1.style.width+";height:"+arc1.style.height+";' strokecolor='"+color1+"' strokeweight='"+size1+"' fillcolor='"+color2+"'/>"))
if(huxingtixing==false){huxingtixing=true;alert("弧形已绘出,调整弧度 startangle='-200' endangle='30' 请使用“高级”")}
break;
case 4:  //钢笔
line1.style.display="none"
if (Math.abs(tempx-xx)<=1||Math.abs(tempy-yy)<=1)return false
div1.appendChild(document.createElement("<v:line style='position:absolute;z-index:"+zz+";left:"+xx+";top:"+yy+";' to='"+(event.x-xx)+","+(event.y-yy)+"' strokecolor='"+color1+"' strokeweight='"+size1+"'/>"))
break;
case 6:  //圆形
oval1.style.display="none"
if (Math.abs(tempx-xx)<=1||Math.abs(tempy-yy)<=1)return false
div1.appendChild(document.createElement("<v:oval style='position:absolute;z-index:"+zz+";left:"+oval1.style.left+";top:"+oval1.style.top+";width:"+oval1.style.width+";height:"+oval1.style.height+";' strokecolor='"+color1+"' strokeweight='"+size1+"' fillcolor='"+color2+"'/>"))
break;
case 7:  //长方形
rect1.style.display="none"
if (Math.abs(tempx-xx)<=1||Math.abs(tempy-yy)<=1)return false
div1.appendChild(document.createElement("<v:rect style='position:absolute;z-index:"+zz+";left:"+rect1.style.left+";top:"+rect1.style.top+";width:"+rect1.style.width+";height:"+rect1.style.height+";' strokecolor='"+color1+"' strokeweight='"+size1+"' fillcolor='"+color2+"'/>"))
break;
case 8:  //圆矩形
roundrect1.style.display="none"
if (Math.abs(tempx-xx)<=1||Math.abs(tempy-yy)<=1)return false
div1.appendChild(document.createElement("<v:roundrect style='position:absolute;z-index:"+zz+";left:"+roundrect1.style.left+";top:"+roundrect1.style.top+";width:"+roundrect1.style.width+";height:"+roundrect1.style.height+";' strokecolor='"+color1+"' strokeweight='"+size1+"' fillcolor='"+color2+"'/>"))
break;
case 11:  //渐变
if(event.srcElement.parentElement.id=="div1"){
if(!usejianbian.checked){event.srcElement.fillcolor=tianchongbeijing.value}else{
temp1=jianbianyangshi[0].checked?'gradient':'gradientradial'
temp2=event.srcElement.innerHTML.replace(/<v\:fill .*<\/v:fill>/gi,'')
event.srcElement.innerHTML=temp2+"<v:fill color='"+jianbianse2.value+"' color2='"+jianbianse1.value+"' type='"+temp1+"'/>"
}}
break;
case 12:  //立体
if(event.srcElement.parentElement.id=="div1"){
temp2=event.srcElement.innerHTML.replace(/<v\:extrusion .*<\/v:extrusion>/gi,'')
event.srcElement.innerHTML=temp2+"<v:Extrusion on='t' color='"+litiyanse.value+"' backdepth='"+houxiangshenzhan.options[houxiangshenzhan.selectedIndex].text+"' foredepth='"+qianxiangshenzhan.options[qianxiangshenzhan.selectedIndex].text+"' rotationangle='"+pianyishangbian.options[pianyishangbian.selectedIndex].text+","+pianyizuobian.options[pianyizuobian.selectedIndex].text+"'/>"
}
break;
case 13:  //边框
if(event.srcElement.parentElement.id=="div1"){
temp2=event.srcElement.innerHTML.replace(/<v\:stroke .*<\/v:stroke>/gi,'')
event.srcElement.innerHTML=temp2+"<v:Stroke dashstyle='"+biankuangyangshi.options[biankuangyangshi.selectedIndex].text+"' startarrow='"+qidianjiantou.options[qidianjiantou.selectedIndex].text+"' endarrow='"+zhongdianjiantou.options[zhongdianjiantou.selectedIndex].text+"'/>"
event.srcElement.strokecolor=biankuangyanse.value
biankuangcudu.options[biankuangcudu.selectedIndex].text=='0'?event.srcElement.stroked=false:event.srcElement.strokeweight=biankuangcudu.options[biankuangcudu.selectedIndex].text
}
break;
case 14:  //高级
if(event.srcElement.parentElement.id=="div1"){
gaojiobj=event.srcElement
txt2.value=event.srcElement.outerHTML.replace(/>/g,">\n").replace(/ = /g,"=").replace(/\: /g,":").replace(/\; /g,";");gaoji1.style.left=event.x;gaoji1.style.top=event.y;gaoji1.style.display=''
}
break;
case 16:  //文字化
if(event.srcElement.parentElement.id=="div1"){
str1=prompt("请输入一段用于图形轨迹填充的文字(推荐使用英文),字大小依据笔边粗度,字颜色请使用高级直接添加<v:fill></v:fill>修改","")
if(!str1)return false;if(str1=='')return false
if(str1.indexOf("'")!=-1)return alert("输入的文字中不可含有违禁符号 ' 单引号")
temp2=event.srcElement.innerHTML.replace(/<v\:path .*<\/v:path>/gi,'').replace(/<v\:textpath .*<\/v:textpath>/gi,'')
parseInt(bibiancudu.options[bibiancudu.selectedIndex].text)<10?str11=10:str11=bibiancudu.options[bibiancudu.selectedIndex].text
event.srcElement.innerHTML=temp2+"<v:path textpathok='True'/>\n<v:textpath on='True' style='font-size:"+str11+";' string='"+str1+"'/>"
}
break;
}}}
function savechexiao(){
youbiao+=1;chexiao[youbiao]=div1.innerHTML
}
function rndcolor(theobjis){
colortab.style.display="";colorid.innerHTML="";str1="<table cellspacing=0 cellpadding=0>";
for(r=0;r<10;r++){str1+="<tr>"
for(i=0;i<10;i++){
tempcolor1=Math.round(Math.random()*255).toString(16)+Math.round(Math.random()*255).toString(16)+Math.round(Math.random()*255).toString(16);while(tempcolor1.length<6){tempcolor1+=Math.round(Math.random()*9)}
str1+="<td style='width:10;height:10;background-color:#"+tempcolor1+";' onclick="+theobjis+".value='#"+tempcolor1+"';"+theobjis+".style.color='#"+tempcolor1+"';colortab.style.display='none'></td>"
}str1+="</tr>"}
colorid.innerHTML=str1+"</table>"
}
function document.onmouseup(){
if(event.srcElement.tagName=="BUTTON"){
event.srcElement.blur();div1.focus();if(event.srcElement.className=="bon1"||event.button!=1)return true;
for(i=0;i<huabi.length;i++)huabi[i].className="bon2"
event.srcElement.className="bon1";
if(huabi[10].className=="bon1"){tianchong1.style.display='';tianchongbeijing.value=tianchongbeijing.value==''?huabibeijing.value:tianchongbeijing.value}else{tianchong1.style.display='none';}
if(huabi[11].className=="bon1"){liti1.style.display='';litiyanse.value=litiyanse.value==''?huabibeijing.value:litiyanse.value}else{liti1.style.display='none';}
if(huabi[12].className=="bon1"){biankuang1.style.display='';biankuangyanse.value=biankuangyanse.value==''?huabiyanse.value:biankuangyanse.value}else{biankuang1.style.display='none';}
if(poly1!=null){   //清除记忆点
if(poly1.tagName=="polyline"){poly1.points.value=oldvalue;oldvalue="";poly1=null;}else{poly1.outerHTML="";oldvalue="";poly1=null;}
}
wenzi1.style.display='none';tupian1.style.display='none';gaoji1.style.display='none';menu1.style.display='none'
}}
function forerr(){
if(moveobj!=null&&bi==1){moveobj.style.left=xx-ckleft;moveobj.style.top=yy-cktop;ckleft=0;cktop=0;moveobj=null}
if(moveobj!=null&&bi==2){
if(moveobj.tagName!="line"){moveobj.style.left=ckleft;moveobj.style.top=cktop;moveobj.style.width=ckwid;moveobj.style.height=ckhei;}else{moveobj.to=ckto;}
ckleft=0;cktop=0;ckwid=0;ckhei=0;ckto="";moveobj=null
}
line1.style.display='none';oval1.style.display='none';rect1.style.display='none';roundrect1.style.display='none';arc1.style.display='none'
}
function charuwenzi(){   //插入文字
if(txt1.value=="")return alert('请先输入文字，在点击插入')
wenzibiankuang.checked==true?thebiankuang="border:1 solid black;":thebiankuang=""
var newtxt=document.createElement("<span style='position:absolute;z-index:"+zz+";left:"+xx+";top:"+yy+";color:"+wenziyanse.value+";background-color:"+wenzibeijing.value+";font-size:"+wenzidaxiao.options[wenzidaxiao.selectedIndex].text+";font-family:"+wenziziti.options[wenziziti.selectedIndex].text+";"+thebiankuang+"'></span>");newtxt.innerText=txt1.value
div1.appendChild(newtxt);wenzi1.style.display='none'
}
function charutupian(){   //插入图片
if(file1.value=="")return alert('请先输入图片路径(<a href="HTTP://或FILE:///" target="_blank">HTTP://或FILE:///</a>)格式，在点击插入')
if(file1.value.indexOf("'")>-1)return alert("图片地址不可以含有违禁字符 ' 单引号")
str1=""
if(heibai.checked==true){str1=" grayscale='true'"}else{
if(duibi.options[duibi.selectedIndex].text!='默认对比度')str1=" gain='"+duibi.options[duibi.selectedIndex].text+"'"
if(secai.options[secai.selectedIndex].text!='默认色彩度')str1=" gamma='"+secai.options[secai.selectedIndex].text+"'"
if(liangdu.options[liangdu.selectedIndex].text!='默认亮度')str1=" blacklevel='"+liangdu.options[liangdu.selectedIndex].text+"'"
}
var newtxt=document.createElement("<v:Image style='position:absolute;z-index:"+zz+";left:"+xx+";top:"+yy+";width:100;height:100' src='"+file1.value+"'"+str1+"/>");
div1.appendChild(newtxt);tupian1.style.display='none'
}
function document.onkeydown(){   //实现各种快捷键
if(event.srcElement.tagName!='TEXTAREA'){
event.ctrlKey&&event.keyCode==90?chexiaock.click():
event.ctrlKey&&event.keyCode==89?fanchexiaock.click():
event.ctrlKey&&event.keyCode==38?fangda.click():
event.ctrlKey&&event.keyCode==40?suoxiao.click():
!event.ctrlKey&&event.keyCode==37?zuoyi.click():
!event.ctrlKey&&event.keyCode==38?shangyi.click():
!event.ctrlKey&&event.keyCode==39?youyi.click():
!event.ctrlKey&&event.keyCode==40?xiayi.click():str1=1
}}

</script>

