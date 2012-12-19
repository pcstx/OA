<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="201010.aspx.cs" Inherits="HRMWeb.aspx._010101" EnableEventValidation="false" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
     <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    	    <link href="../styles/StyleSheet.css" type="text/css" rel="stylesheet" />
    	    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>
    	    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>
            <script language="javascript" type="text/javascript" src="../JScript/jquery-latest.pack.js"></script>
            <script language="javascript" type="text/javascript">
                        var styleToSelect;
                        // Add click handlers for buttons to show and hide modal popup on pageLoad
                        function pageLoad() {

                            $addHandler($get("btnSearch"), 'click', showQueryModalPopupViaClient);
                            
                        
                        }
                        function showQueryModalPopupViaClient(ev) {
                            ev.preventDefault();
                            var modalPopupBehavior = $find('programmaticQueryModalPopupBehavior');
                            modalPopupBehavior.show();
                        }

                        function showDepartmentModalPopupViaClient(ev) {
                            ev.preventDefault();
                            var modalPopupBehavior = $find('programmaticDepartmentModalPopupBehavior');
                            modalPopupBehavior.show();
                        }

             
                    
                        function onDepartmentSelYes()
                        {
                            //从树上找出所选择的值
                            var tree =document.all['treeDeptartment'];
                          
                            findSelectedNodeValue();
                            //var parentNode = tree.getTreeNode( tree.clickedNodeIndex); 

                            //document.getElementById('Tabs_Panel1_txtdepartmentCode').value="01";
                            //document.getElementById('Tabs_Panel1_txtPEEBIDEP').value="11432423";
            
                        }
                        function findSelectedNodeValue()
                        {
                            // 节点值存储方式
                            var sltNVSite = document.getElementById('treeDeptartment');
                           
                            var ptn = sltNVSite.selectedIndex == 0 ? "nv=(\\S+)" : "javascript:void\\('(\\S+)'\\)"
                            var regx = new RegExp(ptn, "i");    
                            
                            // 获取指定 TreeView 内的 checkbox
                            var eles = treeDeptartment.getElementsByTagName("input");
                            
                            var e1, e2;
                            var eId;
                            var departmentIds = "";
                            var departmentNames = "";
                            var mt;
                            // 遍历
                            
                            for(var i = 0; i < eles.length; i ++)
                            {
                                e1 = eles[i];        
                                if(e1.type == "checkbox" && e1.checked) 
                                {
                                    eId = "treeDeptartmentt" + e1.id.match(/treeDeptartmentn(\d+)CheckBox/i)[1];
                                    //alert(e1.title);
                                    //document.getElementById('Tabs_Panel1_txtPEEBIDEP').value=e1.title;
                                    //departmentNames += e1.title + ",";
                                    e2 = document.getElementById(eId);  
                                    if(e2)
                                    {   
                                        //document.getElementById('Tabs_Panel1_txtdepartmentCode').value= e2.href;   
                                        //window.document.getElementById("Tabs_Panel1_txtPersonCode").value =""; 
                                        //window.document.getElementById("Tabs_Panel1_txtPEEBIDL").value ="";    
                                        //alert(e2.href);  
                                        departmentIds += e2.href + ",";
                                        departmentNames += e2.innerText + ",";
                                    }
                                }
                            }    
                            //alert("您选中的节点值：" + departmentIds);
                            //alert("您选中的节点值：" + departmentNames);
                            if(departmentIds.length == 0) 
                            {    
                                window.document.getElementById("txtqPEEBIDEPCode").value =""; 
                                window.document.getElementById("txtqPEEBIDEP").value =""; 
                            }
                            else
                            {
                                // 去掉末尾 ,
                                departmentIds = departmentIds.slice(0, departmentIds.length - 1);
                                departmentNames = departmentNames.slice(0, departmentNames.length - 1);
                                //alert("您选中的节点值：" + cityIds);
                                window.document.getElementById("txtqPEEBIDEPCode").value =departmentIds; 
                                window.document.getElementById("txtqPEEBIDEP").value =departmentNames;  
                                //return cityIds;
                                /*
                                
                                */
                            }
                        }
                        //用Treeview chekbox节点单选的处理事件
                        function TreeSingleSelect(treeID,checkNode)
                        {
                            if(!treeID)
                            return;
                            var objs = document.getElementsByTagName("input");
                            for(var i=0;i<objs.length;i++)
                            {
                                if(objs[i].type=='checkbox')
                                {
                                    var obj=objs[i];
                                    
                                    if(obj.id.indexOf(treeID)!=-1)
                                    {
                                        if(obj!=checkNode)
                                        {
                                            obj.checked=false;
                                        }
                                    }
                                }
                            }    
                        }
                        //用于给TreeView的 chebox添加 单击事件(如果要将某一TreeView变为单选择 只需调用下面方法)
                        function SetTreeNodeClickHander(treeID)
                        {
                           
                            var objs = document.getElementsByTagName("input");
                            for(var i=0;i<objs.length;i++)
                            {
                                if(objs[i].type=='checkbox')
                                {
                                    var obj=objs[i];
                                    if(obj.id.indexOf(treeID)!=-1)
                                    {
                                        objs[i].onclick=function(){TreeSingleSelect(treeID,this);};
                                    }
                                }
                            }
                        }
                        //用Treeview chekbox节点自动选择父节点的处理事件
                        function AutoSelectParentNode(obj)
                        {
                           if(obj.checked)
                           {
                            var p=obj.parentNode.parentNode.parentNode.parentNode.parentNode;
                            var pCheckNodeID=p.id.replace("Nodes","CheckBox");
                            var checkNode=document.getElementById(pCheckNodeID);
                            if(checkNode)
                            { 
                              checkNode.click();//如果不需要选中所有父节点的话（如父的父等）把本行代码去掉及可
                              checkNode.checked=true;
                            }
                           }
                        }
                        //用于给TreeView的 chebox添加 自动选择父节点的处理事件(如果要将某一TreeView变为自动选择父节点 只需调用下面方法)
                        function SetTreeNodeAutoSelectParentNodeHandle(treeID)
                        {
                            var objs = document.getElementsByTagName("input");
                            for(var i=0;i<objs.length;i++)
                            {
                                if(objs[i].type=='checkbox')
                                {
                                    var obj=objs[i];
                                    if(obj.id.indexOf(treeID)!=-1)
                                    {
                                        objs[i].onclick=function(){AutoSelectParentNode(this);};
                                    }
                                }
                            }
                        }
                        
                        
                        //用于给TreeView的 chebox添加 子父节点的交替出现处理事件( 只需调用下面方法)
                        function SetTreeNodeChildrenParentNodeHandle(treeID)
                        {
                            var objs = document.getElementsByTagName("input");
                            for(var i=0;i<objs.length;i++)
                            {
                                if(objs[i].type=='checkbox')
                                {
                                    var obj=objs[i];
                                    if(obj.id.indexOf(treeID)!=-1)
                                    {
                                        objs[i].onclick=function(){client_OnTreeNodeChecked(this);};
                                    }
                                }
                            }
                        }
                        
                        //用Treeview chekbox节点自动选择父节点的处理事件
                        function AutoSelectChildrenParentNode(obj)
                        {
                           //如果选择，要看其父节点的所有子节点是否全选，如果全选，父结点选中，所以子节点非选
                           if(obj.checked)
                           {
                                var p=obj.parentNode.parentNode.parentNode.parentNode.parentNode;//父结点
                                
                                //var ins = p.getElementsByTagName('input'); //有多少个子结点
                                var ins=p.childNodes;
                                var c=obj.childNodes;//其所有子节点为false
                                alert("该节点有子节点个数为："+c.count); 
                                if(c.length>0)
                                {
                                    for(ic=0;ic<c.length;ic++)
                                    {
                                        c[ic].checked=false;
                                    }
                                }
                                var chall = true; 
                                //alert("该节点有兄弟节点个数为："+ins.length); 
                                for(i = 0; i < ins.length; i++)
                                {   //alert("第" + i + "个节点的选中状态为：" +ins[i].checked); 
                                    if(ins[i].checked == false)
                                    { 
                                        chall = false; 
                                      
                                        break; 
                                     } 
                                }
                                if ( chall )//如果所有兄弟节点都被选中，那么兄弟节点都不选，只选中父结点
                                {
                                    //alert("所有节点被选中");
                                    for(i = 0; i < ins.length; i++)
                                    {   
                                        ins[i].checked=false;
                                    }
                                   
                                    var pCheckNodeID=p.id.replace("Nodes","CheckBox");
                                    var checkNode=document.getElementById(pCheckNodeID);
                                    if(checkNode)
                                    { 
                                      //checkNode.click();//如果不需要选中所有父节点的话（如父的父等）把本行代码去掉及可
                                      checkNode.checked=true;
                                    }
                                }
                               
                            
                           }
                        }

         function client_OnTreeNodeChecked(evt){
            var obj = getCurrentNode(evt);
            var treeNodeFound = false;
            var checkedState;
            if (obj.tagName == "INPUT" && obj.type == "checkbox" ){
                checkedState = obj.checked;
                var curNode = getParentNode(obj);
                changeNodeState(curNode,checkedState,obj);
            }
        }

        function getCurrentNode(evt){
            var obj;
            if(window.event)    obj = window.event.srcElement;
            else                obj = (evt ? evt : (window.event ? window.event : null)).target; 
            return obj;
        }

        function getParentNode(node){
            do
            {
              node = node.parentNode;
            } 
            while (node.tagName != "TABLE")
            return node;
        }

        function changeNodeState(node,state,curNode){

           //change all the children
           if(node.nextSibling!=null && node.nextSibling.tagName=="DIV"){

                var cbArr = node.nextSibling.getElementsByTagName("INPUT");//得到所有的子结点
                if(state){//如果为真，所有子结点为假
                
                    for(var i=0; i<cbArr.length; i++){

                        if(cbArr[i].type == "checkbox")   cbArr[i].checked = false;

                    } 
                }
                //否则不动   
           }

           //change its parents' state
                           
           var flag = true;

           var inputArr = node.parentNode.getElementsByTagName("INPUT");//父结点
           if(state){
              
                for(var i=0; i<inputArr.length; i++){
                   
                    if(GetNodeValue(inputArr[i]).length==GetNodeValue(curNode).length) //和其是兄弟节点
                    {
                        if(inputArr[i].type == "checkbox" && !inputArr[i].checked)  flag = false;
                        
                    }
                    
                }
           }
           else{
            flag = true;
            
           }
           var parentArr;
           if(flag){ //其兄弟节点全部选中，则父结点选中，兄弟节点全部不选
              
                parentArr = node.parentNode.previousSibling.getElementsByTagName("INPUT");

                for(var i=0; i<parentArr.length; i++){

                    if(parentArr[i].type == "checkbox"){parentArr[i].checked = true; }

                }
                
                inputArr = node.parentNode.getElementsByTagName("INPUT");
                for(var i=0; i<inputArr.length; i++){
                        if(inputArr[i].type == "checkbox" ) { inputArr[i].checked = false;}
                }
           }
           else
           {    //否则父结点消失
                parentArr = node.parentNode.previousSibling.getElementsByTagName("INPUT");
               
                for(var i=0; i<parentArr.length; i++){

                    if(parentArr[i].type == "checkbox") parentArr[i].checked = false;

                }
                
           }

        }
        function GetNodeValue(curNode)
        {
            var e1, e2;
            e1 = curNode; 
            
            if(e1.type == "checkbox")  //&& e1.checked
            {
               eId = "treeDeptartmentt" + e1.id.match(/treeDeptartmentn(\d+)CheckBox/i)[1];
              
               e2 = document.getElementById(eId);  
               if(e2)
               {   
                 return e2.href;  
               }
            }
        }

            </script>  
            
    <script language="javascript" type="text/javascript">

                      
    
                         function btnqPEEBIDEPPopuClick()
                        {
                           
                            var url='20101002.aspx';
                            url += '?GUID=' + Math.random() ;
                            var ret = window.showModalDialog(url,'HI20101002','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:485px') ; 

                            if (ret != null) 
                            { 
                              window.document.getElementById("txtqPEEBIDEP").value =ret[0]; 
                              window.document.getElementById("txtqPEEBIDEPCode").value =ret[1]; 
                            } 
                            return false;
                        }
                        
                        function btnqPEEBIPCPopuClick()
                        {
                           
                            var url='20101007.aspx';
                            url += '?GUID=' + Math.random() ;
                            var ret = window.showModalDialog(url,'HI014','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:485px') ; 

                            if (ret != null) 
                            { 
                              window.document.getElementById("txtqPEEBIPC").value =ret[0]; 
                              window.document.getElementById("txtqPEEBIPCCode").value =ret[1]; 
                            } 
                            return false;
                        }
                        function btnqPEEBIENAPopuClick()
                        {
                           
                            var url='20101006.aspx';
                            url += '?GUID=' + Math.random() ;
                            var ret = window.showModalDialog(url,'HI014','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:485px') ; 

                            if (ret != null) 
                            { 
                              window.document.getElementById("txtqPEEBIENA").value =ret[0]; 
                              window.document.getElementById("txtqPEEBIENACode").value =ret[1]; 
                            } 
                            return false;
                        }
                        function btnqPEEBIPTPopuClick()
                        {
                           
                            var url='20101008.aspx';
                            url += '?GUID=' + Math.random() ;
                            var ret = window.showModalDialog(url,'HI014','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:485px') ; 

                            if (ret != null) 
                            { 
                              window.document.getElementById("txtqPEEBIPT").value =ret[0]; 
                              window.document.getElementById("txtqPEEBIPTCode").value =ret[1]; 
                            } 
                            return false;
                        }
                         function btnqPEEBIDTPopuClick()
                        {
                           
                            var url='20101009.aspx';
                            url += '?GUID=' + Math.random() ;
                            var ret = window.showModalDialog(url,'HI014','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:485px') ; 

                            if (ret != null) 
                            { 
                              window.document.getElementById("txtqPEEBIDT").value =ret[0]; 
                              window.document.getElementById("txtqPEEBIDTCode").value =ret[1]; 
                            } 
                            return false;
                        }
                             function btnqPEEBIESTPopuClick()
                        {
                           
                            var url='20101010.aspx';
                            url += '?GUID=' + Math.random() ;
                            var ret = window.showModalDialog(url,'HI014','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:485px') ; 

                            if (ret != null) 
                            { 
                              window.document.getElementById("txtqPEEBIEST").value =ret[0]; 
                              window.document.getElementById("txtqPEEBIESTCode").value =ret[1]; 
                            } 
                            return false;
                        }
                          function btnqPEEBIWTPopuClick()
                        {
                           
                            var url='20101011.aspx';
                            url += '?GUID=' + Math.random() ;
                            var ret = window.showModalDialog(url,'HI014','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:485px') ; 

                            if (ret != null) 
                            { 
                              window.document.getElementById("txtqPEEBIWT").value =ret[0]; 
                              window.document.getElementById("txtqPEEBIWTCode").value =ret[1]; 
                            } 
                            return false;
                        }
                           function btnqPEEBIESPopuClick()
                        {
                           
                            var url='20101012.aspx';
                            url += '?GUID=' + Math.random() ;
                            var ret = window.showModalDialog(url,'HI014','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:485px') ; 

                            if (ret != null) 
                            { 
                              window.document.getElementById("txtqPEEBIES").value =ret[0]; 
                              window.document.getElementById("txtqPEEBIESCode").value =ret[1]; 
                            } 
                            return false;
                        }
                           function btnqPEEBIELPopuClick()
                        {
                           
                            var url='20101013.aspx';
                            url += '?GUID=' + Math.random() ;
                            var ret = window.showModalDialog(url,'HI014','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:485px') ; 

                            if (ret != null) 
                            { 
                              window.document.getElementById("txtqPEEBIEL").value =ret[0]; 
                              window.document.getElementById("txtqPEEBIELCode").value =ret[1]; 
                            } 
                            return false;
                        }
                        
                        
    </script>  
    <div runat="server">
    <script language="javascript" type="text/javascript">
        var checkFlag;
        function ChooseAll()
       {
 　　　　　　if(checkFlag) // 全选　
　　　　　　{
  　　　　　　　　var inputs = document.all.tags("INPUT");
  　　　　　　　　for (var i=0; i < inputs.length; i++) // 遍历页面上所有的 input 
  　　　　　　　　{
  　　　　　　　　　　if (inputs[i].type == "checkbox" && inputs[i].id != "all" )
   　　　　　　　　　　{
    　　　　　　　　　　　　inputs[i].checked = true;
   　　　　　　　　　　}     
  　　　　　　　　}
 　　　　　　}
 　　　　　　else  // 取消全选
             {
  　　　　　　　　var inputs = document.all.tags("INPUT");
  　　　　　　　　for (var i=0; i < inputs.length; i++) // 遍历页面上所有的 input 
                 {
   　　　　　　　　　　if (inputs[i].type == "checkbox" && inputs[i].id != "all" )
   　　　　　　　　　　 {
    　　　　　　　　　　　　inputs[i].checked = false;
   　　　　　　　　　　}     
  　　　　　　　　}
 　　　　　　}
　　　　}


function GetPEEIBIID(MykeyCol) {
    strPEEIBIID = MykeyCol;

            return false;
        }


        $(function() {
            $("#btnSubmit").click(function() {
                alert("dd");
                var str = "";
                var icount = 0;
                var valueStr = "";
                var hideValuStr = "";
                var txt = $("#MyAddItem > input");
                for (var i = 0; i < txt.size(); i++) {
                    icount++;
                    if (icount == 1) {

                        valueStr = escape(txt[i].value);
                        if (valueStr == "") {
                            alert("请为添加的栏位输入值");
                            return;
                        }
                        else
                            str = str + txt.eq(i).attr("id") + "=" + escape(txt[i].value) + "|";

                    }
                    else if (icount == 2) {
                        icount = 0;
                        hideValuStr = txt[i].value;
                        if (!DoReg(valueStr, hideValuStr)) {
                            alert("输入的格式不正确");
                            return;
                        }
                    }

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
                            alert("新增附加信息成功!");
                            $("#Button1").click();

                        }
                        else
                            alert("新增附加信息失败!");
                    }
                });
            });

        });

        function addPopuClick() {
          var url = 'PEEBIAddInfo.aspx';
          url += '?GUID=' + Math.random();
          var ret = window.showModalDialog(url, 'HI014', 'scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:480px');

            if (ret != null) {
                $.ajax({
                    type: "post",
                    dataType: "text",
                    url: "PEEBIAddInfo.ashx",
                    data: "addItemFlagName=PEEBI" + ret[0].toUpperCase() + "&MsgValue=" + escape(ret[1]) + "&type=" + ret[2] + "&PEEBITEMPID=" + strPEEIBIID,
                    complete: function() { $("#load").hide(); },
                    success: function(msg) {
                    alert("附加项添加成功 ！！");
                    $("#Button1").click();


                    }
                });
            }

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
                                    var ItemTxt = document.createElement("span");
                                    ItemTxt.innerHTML = name + ":";
                                    var inputTxt = document.createElement("input");
                                    inputTxt.type = "text";
                                    inputTxt.value = value;
                                    inputTxt.name = id;
                                    inputTxt.setAttribute("id", id);
                                    var HideText = document.createElement("input");
                                    HideText.type = "hidden";
                                    HideText.value = type;
                                    var inputBr = document.createElement("<br>");
                                    div.appendChild(ItemTxt);
                                    div.appendChild(inputTxt);
                                    div.appendChild(HideText);
                
                                    if (iRowCount == 3) {
                                        iRowCount = 0;
                                        div.appendChild(inputBr);
                                    }
                                }
                            }
                       }



                    }
                });
            }
       }

        function DoReg(value, type) {
            var Reg = "";
            if (type == "varchar(50)")   //字符串
                Reg = /^[a-z,A-Z]+$/;
            else if (type == "int")  //数字
                Reg = /^\d.+$/
            else if (type == "datetime") //日期
                Reg = /^((((19){1}|(20){1})(\\d){2})|(\\d){2})[-\\s]{1}[01]{1}(\\d){1}[-\\s]{1}[0-3]{1}(\\d){1}$/;
            if (!Reg.exec(value)) {
                return false;
            }
            return true;
        }
	</script>  
    
 </div>
 
 <style>
 .btn
 {
 	display:none;

 	
 	}
 
 </style>
</head>


<body>
    <form id="form1" runat="server">
        <cc1:toolkitscriptmanager id="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
        </cc1:toolkitscriptmanager>
        
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          
            <ContentTemplate>
             <div >
                <cc2:Button ID="btnSearch" runat="server" ButtonImgUrl="../images/query.gif" Enabled="false"  ShowPostDiv="false" Text="Button_Search" ValidateForm="false" AutoPostBack="false"/>
	            <cc2:Button ID="btnExportExcel" runat="server" ShowPostDiv="false" ValidateForm="false" AutoPostBack="true" Disable="false" Text="Button_ExportExcel" ButtonImgUrl="../images/excel.gif" OnClick="btnBrowseMode_Click"  />
                <cc2:Button ID="btnAdd" runat="server" ShowPostDiv="false" ValidateForm="false" AutoPostBack="true" Disable="false" Text="Button_Add"  OnClick="btnAdd_Click"  />
                <cc2:Button ID="btnDelet" runat="server" ShowPostDiv="false" ValidateForm="false" AutoPostBack="true" Disable="false" Text="Button_MoveOut"  OnClick="btnDelet_Click"  />
            </div>
           <div class="ManagerForm">
         	<fieldset>		
		      <legend style="background:url(../images/legendimg.jpg) no-repeat 6px 50%;"><asp:Label ID="lblBigTitle" runat="server" Text=""></asp:Label>
		      </legend>

          <!--page start-->
          <div id="pager" >
            <div class="PagerArea" >
               <webdiyer:AspNetPager ID="AspNetPager1" runat="server"  OnPageChanging="AspNetPager1_PageChanging" onpagechanged="AspNetPager1_PageChanged"  Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Page: " 
                 HorizontalAlign="left"  CenterCurrentPageButton="true" NumericButtonCount="9">
               </webdiyer:AspNetPager>
            </div>
            <div class="PagerText">
                    <asp:Label ID="lblPageSize" runat="server" Text=""></asp:Label>&nbsp;
                       <cc2:TextBox ID="txtPageSize" runat="server" Width="35" TextMode="SingleLine" SetFocusButtonID="btnPageSize"  ></cc2:TextBox>
                       <cc2:Button ID="btnPageSize" Text="Button_GO" runat="server" Width="60" OnClick="txtPageSize_TextChanged" ShowPostDiv="true"/>
             </div>
          </div>
             
                 
           
           
           <cc1:FilteredTextBoxExtender  ID="FilteredTextBoxExtender2" runat="server" 
                    TargetControlID="txtPageSize"
                    FilterType="Numbers"/>   
                                
            <!--page end-->
            <!--gridview start-->
                <!--gridview Browse mode start-->
            <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20"  OnRowCommand="GridView1_RowCommand" onrowcreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound"
                           Width="100%" BoundRowDoubleClickCommandName="" MergeCells="" >
                  <Columns>
                        <asp:TemplateField>
                            <headertemplate>
                                <asp:CheckBox ID="all" runat="server"  />
                            </headertemplate>
                            <itemtemplate>
                                    <asp:CheckBox  ID="item" runat="server"/>
                            </itemtemplate>
                            <itemstyle width="50px" />
                        </asp:TemplateField>
                  </Columns>
                  
                  <SmartSorting AllowMultiSorting="True" AllowSortTip="True" />
                       <CascadeCheckboxes>
                          <yyc:CascadeCheckbox ChildCheckboxID="item" ParentCheckboxID="all" />
                      </CascadeCheckboxes>
                      <FixRowColumn FixRowType="Header,Pager" TableHeight="408px" TableWidth="100%" FixColumns=""/>
                      <CheckedRowCssClass CheckBoxID="item" CssClass="SelectedRow" />
                       
             </yyc:SmartGridView>
             <div id="MyAddItem" >
             </div>  
             
             <!--gridview Browse mode end-->
          
             <!--grid view end--> 

		</fieldset>
     </div> 
     
       <div id="StatisticDate"  runat="server">
       </div>
        </ContentTemplate>
            <Triggers> 
               <asp:PostBackTrigger ControlID="btnExportExcel" /> 
           </Triggers>  
           
                  


     </asp:UpdatePanel>
     
      <!--  Hint  start -->
     <cc2:Hint id="Hint1" runat="server" HintImageUrl="../images" WidthUp="270"></cc2:Hint>
      <!--  Hint  end -->
     <cc1:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="UpdatePanel1">
        </cc1:UpdatePanelAnimationExtender> 
 
  <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" CssClass="btn" />
<div id="addInfo">
<asp:Button ID="Button5" runat="server" Text="增加"  OnClientClick="addPopuClick()" CssClass="btn" />
<asp:Button ID="btnSubmit" runat="server" Text="更新"   CssClass="btn"/>
  </div>


 <asp:Button runat="server" ID="hiddenTargetControlForQueryModalPopup" style="display:none"/>
  <cc1:ModalPopupExtender runat="server" ID="programmaticQueryModalPopup"
      BehaviorID="programmaticQueryModalPopupBehavior"
      TargetControlID="hiddenTargetControlForQueryModalPopup"
      PopupControlID="programmaticPopupQuery" 
      BackgroundCssClass="modalBackground"
      DropShadow="False"
      CancelControlID="QueryCancelButton"
      PopupDragHandleControlID="programmaticQueryPopupDragHandle"
      RepositionMode="RepositionOnWindowScroll" >
  </cc1:ModalPopupExtender>
  
    <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopupQuery" style="display:none;width:650px;  padding:0 0px 0px 0px">
      <div id="programmaticQueryPopupDragHandle" class="programmaticPopupDragHandle">
          <div class="h2blk">
          <h2 id="H2_2" ><asp:Label ID="lblSearchTitle" runat="server" Text=""></asp:Label></h2>
           </div>   
        <cc2:Button ID="MaxButton" ScriptContent="MaxQueryFrom();" ShowPostDiv="false" runat="server" Text="Label_NULL"  CSS="MaxButtonCSS" AutoPostBack="false" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="true" ToolTip="Max" Width="12" />
		  <cc2:Button ID="QueryCancelButton" ShowPostDiv="false" runat="server" Text="Label_NULL"  CSS="CancelButtonCSS" AutoPostBack="false" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="false" ToolTip="Close" Width="12" />
       </div>
     <div >
              <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                   <ContentTemplate>   
                   <div class="edit">
                          <cc2:Button ID="btnSearchPBEEI" ShowPostDiv="true" runat="server" Text="Label_Search" AutoPostBack="true" ButtontypeMode="Normal"   Page_ClientValidate="false" Disable="true"  Width="26" CSS="SearchButtonCSS" OnClick="btnSubmitSearch_Click" />
		             </div> 
                     <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
	                    <div class="con" >

				                    <div class="formblk">
				                    <div class="textblk">
				                          
                                           <div class="clear">
                                                                 <!--label员工编号-->
                                                                <div class="third1">
                                                                    <label class="char5">
                                                                       <asp:Label ID="lblqPEEBIEC" runat="server" Text=""></asp:Label>
                                                                     </label>
                                                                     <div class="iptblk">
                                                                        <cc2:TextBox ID="txtqPEEBIEC" runat="server" Width="85" ></cc2:TextBox>
                                                                     </div>
                                                                </div>
                                                                <!--label员工姓名-->
                                                                <div class="third2">
                                                                      <label class="char5">
                                                                          <asp:Label ID="lblqPEEBIEN" runat="server" Text=""></asp:Label>
                                                                       </label>
                                                                       <div class="iptblk">
                                                                          <cc2:TextBox ID="txtqPEEBIEN" runat="server" Width="85" ></cc2:TextBox>
                                                                       </div>
                                                                 </div>     
                                             </div>
                                                 <cc1:FilteredTextBoxExtender  ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtqPEEBIEC" FilterType="Numbers"/>  
                                                
                                             <div class="clear">
                                                                  <!--label员工出生日期-->
                                                                  <div class="third1">
                                                                      <label class="char5">
                                                                         <asp:Label ID="lblqPEEBIBD" runat="server" Text="">
                                                                         </asp:Label>
                                                                      </label>
                                                                      <div class="iptblk">
                                                                          <asp:TextBox runat="server" ID="txtPEEBIBDStart"  Width="85" MaxLength="1" style="text-align:justify" ValidationGroup="MKE"/> 
                                               <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                                                        TargetControlID="txtPEEBIBDStart"
                                                        Mask="9999/99/99"
                                                        MessageValidatorTip="true"
                                                        OnFocusCssClass="MaskedEditFocus"
                                                        OnInvalidCssClass="MaskedEditError"
                                                        MaskType="Date"
                                                         ClearTextOnInvalid="True"
                                                        DisplayMoney="Left"
                                                        AcceptNegative="Left"
                                                        ErrorTooltipEnabled="True" />
                                                        
                                           <cc1:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                            ControlExtender="MaskedEditExtender5"
                                            ControlToValidate="txtPEEBIBDStart"
                                            EmptyValueMessage="Date is required"
                                            InvalidValueMessage="Date is invalid"
                                            Display="Dynamic"
                                            TooltipMessage=""
                                            EmptyValueBlurredText="*"
                                            InvalidValueBlurredMessage="*"
                                            ValidationGroup="MKE" />
                                            </div></div>
                                                                
                                                               
                                                                
                                             </div>
                                        
                                             <div class="clear">
                                                               
                                                                 <!--label员工所属部门-->
                                                                <div class="third22">
                                                                   <label class="char5">
                                                                      <asp:Label ID="lblqPEEBIDEP" runat="server" Text=""></asp:Label>
                                                                    </label>
                                                                <div class="iptblk"><!--textbox<cc2:DropDownTreeList ID="dpqPEEBIDEP" runat="server" Width="250" />-->
                                                                    <cc2:TextBox ID="txtqPEEBIDEP" runat="server" Width="290" ></cc2:TextBox>
                                                                    <input runat="server" id="txtqPEEBIDEPCode" type="hidden" />
                                                                     <asp:ImageButton ID="btnImgPEEBIDEP" runat="server" ImageAlign="Middle" ImageUrl="../images/arrow_black.gif" OnClientClick="return btnqPEEBIDEPPopuClick();" ToolTip="搜索" />
                                                                </div></div>

                                             </div>  
                                                   
                                             <div class="clear">
                                                                  <!--label岗位-->
                                                                <div class="third1"><label class="char5"><asp:Label ID="lblqPEEBIPC" runat="server" Text=""></asp:Label></label><div class="iptblk"><cc2:TextBox ID="txtqPEEBIPC" runat="server" Width="85" ></cc2:TextBox>
                                                                    <input runat="server" id="txtqPEEBIPCCode" type="hidden" />
                                                                <asp:ImageButton ImageUrl="../images/arrow_black.gif"  ImageAlign="Middle" ID="ImageButton2"  ToolTip="搜索" runat="server"  OnClientClick="return btnqPEEBIPCPopuClick();"/>
                                                                 </div></div>
                                                              
                                                                <!--label职务-->
                                                                <div class="third3"><label class="char5"><asp:Label ID="lblqPEEBIDT" runat="server" Text=""></asp:Label></label><div class="iptblk"><!--textbox--><cc2:TextBox ID="txtqPEEBIDT" runat="server" Width="85" ></cc2:TextBox>
                                                                <input runat="server" id="txtqPEEBIDTCode" type="hidden" />
                                                                <asp:ImageButton ImageUrl="../images/arrow_black.gif"  ImageAlign="Middle" ID="ImageButton4"  ToolTip="搜索" runat="server"  OnClientClick="return btnqPEEBIDTPopuClick();"/>
                                                                  </div></div>

                                             </div> 
                                             
                                             
                                             
                                             <div class="clear">
                                                                
                                               
                                                                 <!--label状态类别信息--> 
                                                                  
                                                                <div class="third3"><label class="char5"><asp:Label ID="lblqPEEBIES" runat="server" Text=""></asp:Label></label><div class="iptblk">
                                                                   <cc2:TextBox ID="txtqPEEBIES" runat="server" Width="85" Text="在职,试用" ></cc2:TextBox>
                                                                <input runat="server" id="txtqPEEBIESCode" type="hidden" value="" />
                                                                <asp:ImageButton ImageUrl="../images/arrow_black.gif"  ImageAlign="Middle" ID="ImageButton8"  ToolTip="搜索" runat="server"  OnClientClick="return btnqPEEBIESPopuClick();"/>
                                                                </div></div>
                                                                 
                                             </div>
                                             
                                             
                                             <div class="clear">
                                                       <!--label员工入厂时间-->
                                                      <div class="third1">
                                                        <label class="char5"><asp:Label ID="lblqPEEBIED" runat="server" Text=""></asp:Label>
                                                        </label>
                                                           <div class="iptblk"><asp:TextBox runat="server" ID="txtPEEBIEDStart" Width="85" MaxLength="1" style="text-align:justify" ValidationGroup="MKE" /></div>
                                                      </div>
                                                      
                                                      <div class="third2">
                                                         <div class="iptblk">
                                                         
                                                                <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                                                        TargetControlID="txtPEEBIEDStart"
                                                        Mask="9999/99/99"
                                                        MessageValidatorTip="true"
                                                        OnFocusCssClass="MaskedEditFocus"
                                                        OnInvalidCssClass="MaskedEditError"
                                                        MaskType="Date"
                                                        DisplayMoney="Left"
                                                        AcceptNegative="Left"
                                                         ClearTextOnInvalid="True"
                                                        ErrorTooltipEnabled="True" />
                                                        <cc1:MaskedEditValidator ID="MaskedEditValidator4" runat="server"
                                            ControlExtender="MaskedEditExtender4"
                                            ControlToValidate="txtPEEBIEDStart"
                                            EmptyValueMessage="Date is required"
                                            InvalidValueMessage="Date is invalid"
                                            Display="Dynamic"
                                            TooltipMessage=""
                                            EmptyValueBlurredText="*"
                                            InvalidValueBlurredMessage="*"
                                            ValidationGroup="MKE" />
                                                                </div></div>

                                             </div>    
  
                                                                                                              
                                  
                                             <div  class="clear"><label class="char5"> <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></label></div>				                    
				                    
				                    
				                    </div>
				                    </div>
                             </div>
                          </div>
                          <!--validate start-->
                          <!--validate end-->      
           </ContentTemplate>
      </asp:UpdatePanel>
  </div>
</asp:Panel>

<%-- 
<asp:Button runat="server" ID="hiddenTargetControlForDepartmentModalPopup" style="display:none"/>
  <cc1:ModalPopupExtender runat="server" ID="programmaticDepartmentModalPopup"
      BehaviorID="programmaticDepartmentModalPopupBehavior"
      TargetControlID="hiddenTargetControlForDepartmentModalPopup"
      PopupControlID="programmaticPopupDepartment" 
      BackgroundCssClass="modalBackground"
      DropShadow="False"
       OkControlID="btnDepartmentSelect"
       OnOkScript="onDepartmentSelYes()" 
      CancelControlID="departmentCancelButton"
      PopupDragHandleControlID="programmaticDepartmentPopupDragHandle"
      RepositionMode="RepositionOnWindowScroll" >
  </cc1:ModalPopupExtender>
    <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopupDepartment" style="display:none;width:650px;  padding:0 0px 0px 0px">
      <div id="programmaticDepartmentPopupDragHandle" class="programmaticPopupDragHandle">
          <div class="h2blk">
          <h2 id="H2_1" ><asp:Label ID="lblDepartmentModalPopup" runat="server" Text="部门选择"></asp:Label></h2>
           </div>   
		  <cc2:Button ID="departmentCancelButton" ShowPostDiv="false" runat="server" Text="Label_NULL"  CSS="CancelButtonCSS" AutoPostBack="false" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="false" ToolTip="Close" Width="12" />
       </div>
     <div >
              <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                   <ContentTemplate>   
                   <div class="edit">
                          <cc2:Button ID="btnDepartmentSelect" ShowPostDiv="false" runat="server" Text="Label_NULL" AutoPostBack="false" ButtontypeMode="Normal"   Page_ClientValidate="false" Disable="true"  Width="26" CSS="OkButtonCSS" />
		             </div> 
                         
                          <asp:Panel ID="panelTree" runat="server"  ScrollBars="Auto" Height="400"  >
                          <asp:TreeView ID="treeDeptartment" runat="server"  ShowCheckBoxes="All" OnTreeNodePopulate="treeDept_TreeNodePopulate" ExpandDepth="1"  ShowLines="true"></asp:TreeView>
                            <script type="text/javascript" language="javascript">
                                SetTreeNodeChildrenParentNodeHandle("<=treeDeptartment.ClientID>");
                            </script>
                          </asp:Panel>
                          
                          
           </ContentTemplate>
      </asp:UpdatePanel>
  </div>
</asp:Panel>    
--%>
    </form>
</body>
</html>