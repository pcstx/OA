function showhint(iconid, str) {
    var imgUrl = '../images/hint.gif';
    if (iconid != 0) {
        imgUrl = '../images/warning.gif';
    }
    document.write('<div style="background:url(' + imgUrl + ') no-repeat 20px 10px;border:1px dotted #DBDDD3; background-color:#FDFFF2; margin-bottom:10px; padding:10px 10px 10px 56px; text-align: left; font-size: 12px;">');
    document.write(str + '</div><div style="clear:both;"></div>');
}

function showloadinghint(divid, str) {
    if (divid == '') {
        divid = 'PostInfo';
    }
    document.write('<div id="' + divid + '" style="display:none;position:relative;border:1px dotted #DBDDD3; background-color:#FDFFF2; margin:auto;padding:10px" width="90%"  ><img border="0" src="../images/ajax_loading.gif" /> ' + str + '</div>');
}

function CheckByName(form,tname,noname)
{
  for (var i=0;i<form.elements.length;i++)
    {
	    var e = form.elements[i];
	    if(!e.name) continue;
	    if(e.name.indexOf(tname)>=0)
		{
		   if(noname!="")
           {
              if(e.name.indexOf(noname)>=0) ;
              else
              {
                 e.checked = form.chkall.checked;
              }
             
		   }	  
		   else
		   {
		      e.checked = form.chkall.checked;   
		   }
	    }
	}
}

function SH_SelectOne(obj)
{
	//var obj = window.event.srcElement;
	if( obj.checked == false)
	{
		document.getElementById('chkall').checked = obj.chcked;
	}
}

var xmlhttp;
function getReturn(Url)  //提交为aspx,aspx页面路径, 返回页面的值
{
  try
  {
    xmlhttp=new ActiveXObject("Msxml2.XMLHTTP")
  } 
  catch(e)
  {
    try
    {
    xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
    }
    catch (E)
    {
    //alert("请安装Microsofts XML parsers")
    }
  }     
  if ( !xmlhttp && typeof XMLHttpRequest != "undefined" ) 
  {
    xmlhttp=new XMLHttpRequest() 
  }
  try 
  {
    xmlhttp.open('GET',Url,false);   
    xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded')
    xmlhttp.send(null);
    if((xmlhttp.readyState == 4)&&(xmlhttp.status ==200)){
       return xmlhttp.responseText;
    }
    else{
     return null;
    }
  }
  catch (e) 
  {
    alert("你的浏览器不支持XMLHttpRequest对象, 请升级"); 
  }
  return null;
}
       

function isMaxLen(o)
{
	var nMaxLen=o.getAttribute? parseInt(o.getAttribute("maxlength")):"";
	if(o.getAttribute && o.value.length>nMaxLen)
	{
		o.value=o.value.substring(0,nMaxLen)
	}
}
   
function isie()
{
   if(navigator.userAgent.toLowerCase().indexOf('msie') != -1)
   {
       return true;
   }
   else
   {
       return false;
   }
}

function $( id ){return document.getElementById( id );}

/*** 删除首尾空格 ***/ 
String.prototype.Trim = function() {return this.replace(/(^\s*)|(\s*$)/g, ''); };

/*** 统计指定字符出现的次数 ***/ 
String.prototype.Occurs = function(ch) { 
// var re = eval("/[^"+ch+"]/g"); 
// return this.replace(re, "").length; 
return this.split(ch).length-1; 
}

/*** 检查是否由数字组成 ***/ 
String.prototype.isDigit = function() { 
var s = this.Trim(); 
return (s.replace(/\d/g, "").length == 0); 
}

/*** 检查是否由数字字母和下划线组成 ***/ 
String.prototype.isAlpha = function() { 
return (this.replace(/\w/g, "").length == 0); 
} 
/*** 检查是否为数 ***/ 
String.prototype.isNumber = function() { 
var s = this.Trim(); 
return (s.search(/^[+-]?[0-9.]*$/) >= 0); 
}

/*** 返回字节数 ***/ 
String.prototype.lenb = function() { 
return this.replace(/[^\x00-\xff]/g,"**").length; 
}

/*** 检查是否包含汉字 ***/ 
String.prototype.isInChinese = function() { 
return (this.length != this.replace(/[^\x00-\xff]/g,"**").length); 
}

/*** 简单的email检查 ***/ 
String.prototype.isEmail = function() { 
　var strr; 
var mail = this; 
　var re = /(\w+@\w+\.\w+)(\.{0,1}\w*)(\.{0,1}\w*)/i; 
　re.exec(mail); 
　if(RegExp.$3!="" && RegExp.$3!="." && RegExp.$2!=".") 
strr = RegExp.$1+RegExp.$2+RegExp.$3; 
　else 
　　if(RegExp.$2!="" && RegExp.$2!=".") 
strr = RegExp.$1+RegExp.$2; 
　　else 
　strr = RegExp.$1; 
　return (strr==mail); 
}


//用于浏览的主字段弹出相关的选择程序
function btnBrowseFieldClick(TextFieldID, ValueFieldID, BrowsePage, BrowseTypeName) {


    var url = BrowsePage + '?GUID=' + Math.random();
    var ret = window.showModalDialog(url, BrowseTypeName, 'scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px');

    if (ret != null) {
        window.document.getElementById(ValueFieldID).value = ret[0];
        window.document.getElementById(TextFieldID).value = ret[1];
    }
    return false;



}
//用于浏览的主字段弹出相关的选择程序
function btnBrowseDetailFieldClick(element, BrowsePage, BrowseTypeName,cellIndex)
{

    var url = BrowsePage + '?GUID=' + Math.random();
    var ret = window.showModalDialog(url, BrowseTypeName, 'scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px');

    if (ret != null) {
        //window.document.getElementById(ValueFieldID).value = ret[0];
        //window.document.getElementById(TextFieldID).value = ret[1];
        var theTableRow  = element.parentElement.parentElement;
        var theTable=element.parentElement.parentElement.parentElement;
        var rowIndex=theTableRow.rowIndex;
        //var cellIndex = element.cellIndex;
        theTable.rows[rowIndex].cells[parseInt(cellIndex)].children[0].value = ret[1];
        theTable.rows[rowIndex].cells[parseInt(cellIndex)].children[1].value = ret[0];
        
        
    }
    return false;


}

function FormFieldValidate(elementID, elementHTMLType, ValidType) 
{
    var element = document.getElementById("field" + elementID);
    var elementValue='';
    switch (elementHTMLType) 
    { 
       case '1':     //Label
            elementValue=element.innerText;
            break;
        case '2':     //Textbox
            elementValue=element.value;
            break;
        case '3':   //TextArea
            elementValue=element.value;
            break;
        case '4':  //checkboxList
            elementValue=element.value;
            break;
        case '5': // dropdownlist
            elementValue=element.value;
            break;
        case '6': //checkbox
            
            if (element.Checked)
            {
                elementValue = "1";
            }
            else
            {
                elementValue = "0";
            }
            break;
        case '7':   //uploadFile
            //System.Web.UI.WebControls.CheckBox upField = new System.Web.UI.WebControls.CheckBox();

            break;
        case '8':
            elementValue = element.value;
            break;
        default:
            elementValue = element.value;
            break;
            

    }
    
    //调用验证处理程序
    var url = "FormFieldValidateHandler.ashx?ValidType=" + ValidType + '&ValidValue=' + elementValue;
    
    var sResult = getReturn(url);
    var dataarray = new Array();
    dataarray = sResult.split("|");
    if (dataarray[0] == "true") {
        return true;
    }
    else {

        alert(dataarray[1]);
        element.focus();
        return false;
    
    }
}


//明细字段的验证

function FormDetailFieldValidate(element,elementValue, ValidType) {
    
    //调用验证处理程序

    var url = "FormFieldValidateHandler.ashx?ValidType=" + ValidType + '&ValidValue=' + elementValue;

    var sResult = getReturn(url);
    var dataarray = new Array();
    dataarray = sResult.split("|");
    if (dataarray[0] == "true") {
        return true;
    }
    else {

        alert(dataarray[1]);
        element.focus();
        return false;

    }
}


function CaculateRowRule(element, elementValue, RuleFieldIndex, TargetFieldIndex, RuleDetail, RuleFieldName, FieldDBType, ruleTargetFieldTemplateType, TargetFieldHTMLTypeID, TargetFieldID) {
   var theTableRow  = element.parentElement.parentElement;
   var theTable=element.parentElement.parentElement.parentElement;
   var rowIndex=theTableRow.rowIndex;
   var FieldValueList='';
   if (rowIndex>0)
   {   
        var indexArray = new Array();
        indexArray = RuleFieldIndex.split("|");
        for (i=0;i<indexArray.length;i++)
        {
            var FieldIndex = indexArray[i];
            if (FieldIndex != '') {
                var FieldVale = theTable.rows[rowIndex].cells[parseInt(FieldIndex)].firstChild.value;//取值要耕具后天HTMLType
                FieldValueList = FieldValueList + FieldVale + "|";
            }
        
        }

        var url = "FormDetailFieldRowRuleHandler.ashx?RuleFieldName=" + RuleFieldName + '&FieldValue=' + FieldValueList + '&RuleDetail=' + escape(RuleDetail) + '&FieldDBType=' + FieldDBType;

        var sResult = getReturn(url);
        //alert(sResult);

        //theTable.rows[rowIndex].cells[parseInt(TargetFieldIndex)].firstChild.value = sResult;
        if (TargetFieldHTMLTypeID == "1")
            theTable.rows[rowIndex].cells[parseInt(TargetFieldIndex)].innerText = sResult;
        else if (TargetFieldHTMLTypeID == "2") {

        theTable.rows[rowIndex].cells[parseInt(TargetFieldIndex)].firstChild.value = sResult;
          if (ruleTargetFieldTemplateType.toLowerCase() == "textboxincludehidden") {

              //theTable.rows[rowIndex].cells[parseInt(TargetFieldIndex)].secondChild.value = sResult;
              theTable.rows[rowIndex].cells[parseInt(TargetFieldIndex)].children[1].value = sResult;
           }
        
        
        }
        else
            theTable.rows[rowIndex].cells[parseInt(TargetFieldIndex)].firstChild.value = sResult;
               
     }




 }




// function CaculateColumnRule(element, elementValue, lastValue, FieldDBType, TargetFieldID, TargetFieldHTMLTypeID, TargetFieldDbType, ruleDetail, totalRowsCount, rowIndex, RuleFunctionName, FieldID,GroupID)
//  {

//      

//         //获得TargetField的值
//         //可能要判断HTMLType.暂时直接取值
//         var TargetFieldValue = document.getElementById("field" + TargetFieldID).value;
//         var FieldPrevousValue = document.getElementById("Group"+GroupID+"field" + FieldID+"Previous").value; 
//         //获得函数名称
//         var caculateResult;
//         var functionName = RuleFunctionName;
//         if (functionName!='')
//         {
//             if (functionName.toUpperCase() == 'COUNT')
//              {
//             
//                if (totalRowsCount>=rowIndex)
//                {
//                   document.getElementById("field"+TargetFieldID).value=totalRowsCount;
//                
//                }
//                else
//                {
//                   document.getElementById("field"+TargetFieldID).value=rowIndex;
//                
//                }

//             }
//             else if (functionName.toUpperCase() == 'MAX')
//             {
//             
//                 if (parseFloat(TargetFieldValue)<parseFloat(elementValue))
//                 {
//                    document.getElementById("field"+TargetFieldID).value=elementValue;
//                 }
//             
//             }
//             else if (functionName.toUpperCase() == 'MIN')
//             {
//             
//             if (parseFloat(TargetFieldValue)>parseFloat(elementValue))
//                 {
//                    document.getElementById("field"+TargetFieldID).value=elementValue;
//                 }
//             
//             }
//             else if (functionName.toUpperCase() == 'SUM')
//             {

//                 document.getElementById("field" + TargetFieldID).value = parseFloat(TargetFieldValue) - parseFloat(FieldPrevousValue) + parseFloat(elementValue);
//             
//             }
//             else if (functionName.toUpperCase() == 'AVG')
//             {

//                 document.getElementById("field" + TargetFieldID).value = (parseInt(totalRowsCount) * parseFloat(parseFloat(TargetFieldValue)) - parseFloat(FieldPrevousValue) + parseFloat(elementValue)) / parseInt(totalRowsCount);
//               
//             
//             }
//             else
//             {
//                 ;
//             
//             }
//     
//        }


//    }


//    function BackUpColumnRuleFieldValue(ColumnRuleFieldValue,FieldID,GroupID)
//    {

//        document.getElementById("Group" + GroupID + "field" + FieldID + "Previous").value = ColumnRuleFieldValue;

//    }


    function CaculateColumnRule(element, RuleFieldName, RuleFieldColumnIndex, RuleFieldDBType, RuleFieldHTMLType, TargetFieldID, TargetFieldHTMLTypeID, TargetFieldDbType, RuleDetail) 
    { 
    
        
        var theTableRow  = element.parentElement.parentElement;
        var theTable=element.parentElement.parentElement.parentElement;
        var rowIndex=theTableRow.rowIndex;
        var FieldValueList='';
        
        if (rowIndex>0)
        {

          for (j = 1; j < theTable.rows.length; j++)
          {
              var FieldVale = theTable.rows[j].cells[parseInt(RuleFieldColumnIndex)].firstChild.value;
              FieldValueList = FieldValueList + FieldVale + "|";
          }

         var url = "FormDetailFieldColumnRuleHandler.ashx?RuleFieldName="+RuleFieldName+"&RuleFieldDBType=" + RuleFieldDBType + '&FieldValue=' + FieldValueList + '&RuleDetail=' + escape(RuleDetail);

         var sResult = getReturn(url);
         document.getElementById("field" + TargetFieldID).value = sResult;        
       }


   }

   function setGroupPanelHeight(GroupID,actionName) {

       var element = document.getElementById('tabContainer_TP' + GroupID + '_TP' + GroupID + 'panel');
       var elementPageSize = document.getElementById('tabContainer_TP' + GroupID + '_ctl00_txtPageSize');
       
       var height = element.style.height;
       var PageSize = elementPageSize.value;
       if (actionName=='ADD')
       {
           height = parseInt(height.substring(0, height.length - 2)) + 20;
           if (height > (PageSize * 20 + 100)) {

               height = PageSize * 20 + 100
           }
       }
       else
       {
           height = parseInt(height.substring(0, height.length - 2)) - 20;
           if (height < 100) {
               height = 100;
           }
       
       }
       element.style.height = height+"px";
       return true;




   }

   function selectAll(obj) {
       var theTable = obj.parentElement.parentElement.parentElement;
       var i;
       var j = obj.parentElement.cellIndex;

       for (i = 0; i < theTable.rows.length; i++) {
           var objCheckBox = theTable.rows[i].cells[j].firstChild;
           if (objCheckBox.checked != null) objCheckBox.checked = obj.checked;
       }
   }

   function btnGroupLineFieldClick(FieldID,DataSetID,GroupID) {

       var url = 'GroupLineSelect.aspx?DataSetID='+DataSetID+'&FieldID='+FieldID+'&GUID=' + Math.random();
       var ret = window.showModalDialog(url, 'GroupLineSelect', 'scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px');

       if (ret != null) {

           if (ret == "0") 
           {
               return false;
           }
           else 
           {
               //设置Group的高度
               var element = document.getElementById('tabContainer_TP' + GroupID + '_TP' + GroupID + 'panel');
               var elementPageSize = document.getElementById('tabContainer_TP' + GroupID + '_ctl00_txtPageSize');

               var height = element.style.height;
               var PageSize = elementPageSize.value;

               height = parseInt(height.substring(0, height.length - 2)) + 20 * parseInt(ret);
               if (height > (PageSize * 20 + 100)) 
               {
                height = PageSize * 20 + 100   
               }
               element.style.height = height + "px";

               return true;
           }
           
       }
       else
       {
           return false;
       }
   
   
   }