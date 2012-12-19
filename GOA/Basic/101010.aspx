<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"  CodeBehind="101010.aspx.cs" Inherits="AJAXWeb.aspx._01010" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Src="../ascx/UCOperationBanner.ascx" TagName="UCOperationBanner" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>部门资料</title>
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <link href="../styles/StyleSheet.css" type="text/css" rel="stylesheet" />
  <script language="javascript" type="text/javascript" src="../JScript/jquery-latest.pack.js"></script>

	<script language="javascript" type="text/javascript">
	   
	    
	    
	  function Redirect(NotUsedToView)
	  {
	    window.location.href = "101010.aspx?NUTV=" +NotUsedToView;
	  }
      
      //function pageLoad()
      //{
          //document.getElementById("TreeView1t0").innerText = "测试";
          //alert($("#TreeView1t0").html());
          //alert
          //GetCompanyInfor
      
      //}

	  $(function() {
	      //$("#TreeView1t0").innerHTML = "测试";


	      selectedNode = null;

	      $("#load").hide();

	      //newNodeTemplete = '<table cellpadding="0" cellspacing="0" style="border-width:0;"><tr><td><img src='+TreeView1_Data.images[10]+' alt="" /></td><td style="white-space:nowrap;"><span onclick=SelectNode(this) class="TreeView1_0" href="$nvalue$" id="$nid$">$ntext$</span></td></tr></table>'
	      //newNodeTemplete = '<table cellpadding="0" cellspacing="0" style="border-width:0;"><tr><td><img src='+TreeView1_Data.images[3]+' alt="" /></td><td style="white-space:nowrap; text-align:left" ><span onclick=SelectNode(this) class="TreeView1_0" href="$nvalue$" id="$nid$">$ntext$</span></td></tr></table>'
	      newNodeTemplete = '<table cellpadding="0" cellspacing="0" style="border-width:0;"><tr><td><img src=' + TreeView1_Data.images[3] + ' alt="" /></td><td style="white-space:nowrap; text-align:left" ><span onclick=SelectNode(this) class="TreeView1_0" href="$nvalue$" id="$nid$">$ntext$</span></td></tr></table>'
	      indent = '<td><div style="width:20px;height:1px;"></div></td>'

	      toggle = '<a id=\"$nid$\" href=\"javascript:TreeView_ToggleNode(TreeView1_Data,$idnum$,document.getElementById(\'$nid$\'),\' \',document.getElementById(\'$ndivid$\'))\"><img src=' + TreeView1_Data.images[5] + ' alt=\"折叠 $ntext$\" style=\"border-width:0;\" /></a>';

	      divContainer = '<div id="$nid$" style="display:block;"></div>'

	      $("td[@class=TreeView1_1]").attr("class", "TreeView1_0");

	      if ($("#TreeView1_SelectedNode").val().length > 0) {
	          if ($("#" + ($("#TreeView1_SelectedNode").val())).html() != null) {
	              SelectNode(document.getElementById($("#TreeView1_SelectedNode").val()));
	          }
	          else
	              UnSelectNode();
	      }








	      //展开全部节点
	      $("#ExpandAll").click(function() {
	          $("//td/a[img]").each(function() {
	              if ($(this).find("img").attr("alt").indexOf("折叠") == -1)
	                  eval($(this).attr("href"));
	          });
	      });

	      //收缩全部节点
	      $("#CloseAll").click(function() {
	          $("//td/a[img]").each(function() {
	              if ($(this).find("img").attr("alt").indexOf("折叠") >= 0)
	                  eval($(this).attr("href"));
	          });
	      });

	      //选择节点
	      $("//td/span[@id]").click(function() {
	          SelectNode(this);
	      });


	      //新增部门按钮的click事件
	      $("#btnAddDept").click(function() {
	          if (selectedNode == null) {
	              alert("请先选择节点");
	              return;
	          }
	          $("#txtPBDEPDN").attr("readonly", "");
	          $("#txtPBDEPDEN").attr("readonly", "");
	          $("#txtPBDEPDTWN").attr("readonly", "");
	          $("#txtPBDEPDOI").attr("readonly", "");
	          $("#chkPBDEPUS").attr("disabled", "");

	          $("#txtPBDEPPDC").val($("#txtPBDEPDC").val());
	          $("#txtPBDEPPDN").val($("#txtPBDEPDN").val());
	          $("#txtPBDEPPID").val($("#txtPBDEPID").val());
	          $("#txtNewPBDEPPID").val($("#txtPBDEPID").val());

	          $("#txtPBDEPDC").val("");
	          $("#txtPBDEPDN").val("");
	          $("#txtPBDEPDEN").val("");
	          $("#txtPBDEPDTWN").val("");
	          $("#chkPBDEPUS").attr("checked", 'true');
	          $("#txtPBDEPOI").val("");
	          //$("#ImageqPEDECCR").attr("visible","false");
	          var input = $("#MyAddItem >input");
	          input.remove();
	          var span = $("#MyAddItem >span");
	          span.remove();
	          var br = $("#MyAddItem >br");
	          br.remove();


	      });

	      //新增部门后保存按钮的click事件
	      $("#btnSubmit").click(function() {
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
	                  hideValuStr = escape(txt[i].value);
	                  if (!DoReg(valueStr, hideValuStr)) {
	                      alert("输入的格式不正确");
	                      return;
	                  }

	              }

	          }

	          if (selectedNode == null) {
	              alert("请先选择节点");
	              return;
	          }
	          if ($("#txtPBDEPDN").val().trim().length == 0) {
	              alert("请输入部门名称");
	              return;
	          }
	          $("#load").show();
	          if ($("#chkPBDEPUS").attr("checked")) {
	              chkPBDEPUS = "1"
	          }
	          else {
	              chkPBDEPUS = "0"

	          }

	          value = 0;
	          $.ajax({
	              type: "Get",
	              dataType: "text",
	              url: "101010.ashx",
	              data: "action=add&PBDEPDN=" + escape($("#txtPBDEPDN").val()) + "&PBDEPDEN=" + escape($("#txtPBDEPDEN").val()) + "&PBDEPDTWN=" + escape($("#txtPBDEPDTWN").val()) + "&PBDEPUS=" + escape(chkPBDEPUS) + "&PBDEPOI=" + escape($("#txtPBDEPOI").val()) + "&PBDEPPDC=" + escape($("#txtPBDEPPDC").val()) + "&PBDEPPID=" + escape($("#txtPBDEPPID").val()) + "&str=" + str,
	              complete: function() { $("#load").hide(); },
	              success: function(msg) {
	                  value = parseInt(msg);
	                  if (value > 0) {
	                      alert("新增部门信息成功!");
	                      AddNode($("#txtPBDEPDN").val(), value);
	                  }
	                  else
	                      alert("新增部门信息失败!");
	              }
	          });
	      });

	      //删除节点按钮的click事件
	      $("#btnDel").click(function() {
	          if (selectedNode != null) {
	              var subdiv = "#" + selectedNode.attr("id").replace("t", "n") + "Nodes";
	              if (selectedNode.attr("id") == "TreeView1t0") {
	                  alert("不要删除根节点");
	                  return;
	              }

	              if ($(subdiv).html() != null) {
	                  alert("请先删除子节点");
	                  return;
	              }

	              $("#load").show();
	              $.ajax({
	                  type: "Get",
	                  dataType: "text",
	                  url: "101010.ashx",
	                  data: "action=del&PBDEPDC=" + selectedNode.attr("href"),
	                  complete: function() { $("#load").hide(); },
	                  success: function(msgCode) {
	                      if (msgCode == "1") {
	                          var input = $("#MyAddItem >input");
	                          input.remove();
	                          var span = $("#MyAddItem >span");
	                          span.remove();
	                          var br = $("#MyAddItem >br");
	                          br.remove();
	                          DelNode();
	                      }
	                      else if (msgCode == "-3")
	                          alert("删除部门失败,请调离该部门员工.");
	                      else if (msgCode == "-2")
	                          alert("删除部门失败,存在离职员工,无法删除.");
	                  }
	              });
	          }
	          else
	              alert("请先选择节点");
	      });

	      //更新按钮的click事件
	      $("#btnModify").click(function() {
	          if ($("#txtPBDEPDN").val().trim().length == 0) {
	              alert("请输入部门名称");
	              return;
	          }
	          if (selectedNode == null) {
	              alert("请先选择节点");
	              return;
	          }
	          $("#load").show();

	          if ($("#chkPBDEPUS").attr("checked")) {
	              chkPBDEPUS = "1"
	          }
	          else {
	              chkPBDEPUS = "0"

	          }
	          
	          var str = "";
	          var icount = 0;
	          var valueStr = "";
	          var hideValuStr = "";
	          var txt = $("#MyAddItem > input");
	          for (var i = 0; i < txt.size(); i++) {
	             
	              icount++;
	              if (icount == 1) {
	    
	                  valueStr = txt[i].value;
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
	              type: "post",
	              dataType: "text",
	              url: "101010.ashx",
	              data: "action=edit&PBDEPDC=" + escape($("#txtPBDEPDC").val()) + "&PBDEPDN=" + escape($("#txtPBDEPDN").val()) + "&PBDEPDEN=" + escape($("#txtPBDEPDEN").val()) + "&PBDEPDTWN=" + escape($("#txtPBDEPDTWN").val()) + "&PBDEPUS=" + escape(chkPBDEPUS) + "&PBDEPOI=" + escape($("#txtPBDEPOI").val()) + "&PBDEPPDC=" + escape($("#txtPBDEPPDC").val()) + "&PBDEPID=" + escape($("#txtPBDEPID").val()) + "&PBDEPPID=" + escape($("#txtPBDEPPID").val()) + "&NewPBDEPPID=" + escape($("#txtNewPBDEPPID").val()) + "&OldPBDEPPDC=" + escape($("#txtOldPBDEPPDC").val()) + "&str=" + str,
	              complete: function() { $("#load").hide(); },
	              success: function(msgCode) {

	                  if (msgCode == "1") {

	                      alert("更新成功");
	                      //this.location.reload();
	                      if ($("#txtPBDEPPID").val() != $("#txtNewPBDEPPID").val()) {
	                          this.location.reload();
	                      }
	                      else {
	                          EditNode();
	                      }

	                  }
	                  else if (msgCode == "-2")
	                      alert("更新部门失败,请调离该部门员工.");
	                  else if (msgCode == "-1")
	                      alert("更新部门失败.");
	              }
	          });
	      });

	      //编辑模式按钮的click事件
	      $("#btnEditMode").click(function() {


	          $("#txtPBDEPDN").attr("readonly", "");
	          $("#txtPBDEPDEN").attr("readonly", "");
	          $("#txtPBDEPDTWN").attr("readonly", "");
	          $("#txtPBDEPDOI").attr("readonly", "");
	          $("#chkPBDEPUS").attr("disabled", "");
	          //$("#ImageqPEDECCR").attr("Visible","true");
	          //$("#ImageqPEDECCR").attr("style","style='display:''");

	      });

	      //取消选择按钮的click事件
	      $("#unselect").click(function() {
	          UnSelectNode();
	      });

	  });
    
    ///下面是上面操作需要调用的函数

	  function DoReg(value, type) {
	      var Reg = "";
	      if (type == "varchar(50)")   //字符串
	          Reg = /^[a-z,A-Z]+$/;  
	      else if (type == "int")  //数字
	          Reg =/^\d{3}$/
	      else if (type == "datetime") //日期
	           Reg =  /^((((19){1}|(20){1})(\\d){2})|(\\d){2})[-\\s]{1}[01]{1}(\\d){1}[-\\s]{1}[0-3]{1}(\\d){1}$/;


	       if (!Reg.exec(value)) {
	           return false;
	       }

	       return true;
	       
	  
	  }
    
    //展开正在操作的节点
    function DoExpand()
    {
        table = selectedNode.parents("table[a][span]");
        if (table.length > 0) {
            if (table.children().find("img").attr("alt").indexOf("折叠") == -1) {
                eval(table.children().find("a").attr("href"));
            }
           
        }    
        selectedNode.parents("div").each(function(){
            parentNodeID = this.id.replace("t","n").replace("Nodes","");                
            if($("#"+parentNodeID).parents("table[a][span]").length >0)
            {
                table = $($("#"+parentNodeID).parents("table[a][span]")[0]);
                if(table.children().find("img").attr("alt").indexOf("折叠") == -1)
                    eval(table.children().find("a").attr("href"));
            }
        });
    }
    
    //获得最大的节点id
    function GetMaxID()
    {
        var maxid = 0;
        var id = "";
        $("span[@id]").each(function(){
            idnum = parseInt(this.id.split('t')[1]);
            if(idnum > maxid)
            {
                maxid = idnum;
                id = this.id;
            }
        });
        return id;
    }
    
    //设置节点的缩进
    function SetIndent(subdiv)
    {
        n = $("#"+subdiv).find("table:last");
        var newIndent = indent;
        selectedNode.parents("table").find("td").each(function(i){
                if(i-1>0)newIndent += indent;
            });
        n.find("td:first").before(newIndent);
    }
    
    //选择节点
    function SelectNode(node)
    {   //alert($("#"+node.id).attr("href"));
        if(selectedNode != null)selectedNode.parent().attr("class","TreeView1_0");//去掉上一次选择的节点的选择样式
        $(node).parent().attr("class","TreeView1_1");//给新选择的节点加上选择样式
        $("#TreeView1_SelectedNode").val(node.id)//把新选择的节点的id放到id为TreeView1_SelectedNode隐藏域
        if ($("#" + node.id).attr("href") != "#") {
        
            GetPBDEPEntity($("#" + node.id).attr("href"));
            GetPBDEPADDTEMP($("#" + node.id).attr("href"));
        }
        else {
            $("#txtPBDEPDC").val("");
            $("#txtPBDEPDN").val("");
            $("#txtPBDEPDEN").val("");
            $("#txtPBDEPDTWN").val("");
            $("#chkPBDEPUS").attr("checked", '');
            $("#txtPBDEPOI").val("");
            $("#txtPBDEPPDC").val("");

        }
        //$("#selectedvalue").val($("#"+node.id).attr("href"));//把新选择的节点的值放到id为selectedvalue文本框里
        selectedNode = $(node);
        //$("#nName").val(selectedNode.text());

    }
     
     //add Rio
    //获取添加的部门信息
    function GetPBDEPADDTEMP(PBDEPDC) {
        $.ajax({
            type: "POST",
            dataType: "text",
            url: "GetAddItem.ashx",
            data: "action=Get&PBDEPDC=" + PBDEPDC,
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
                            var ItemTxt = document.createElement("span");
                            ItemTxt.innerHTML = name;
                            var inputTxt = document.createElement("input");
                            inputTxt.type = "text";
                            inputTxt.value = value;
                            inputTxt.name = id;
                            inputTxt.setAttribute("id", id);
                            var HideText = document.createElement("input");
                            HideText.type = "hidden";
                           HideText.value = type;
                            var inputBr = document.createElement("<br><br>");
                            div.appendChild(ItemTxt);
                            div.appendChild(inputTxt);
                           div.appendChild(HideText);
                            div.appendChild(inputBr);
                        }
                    }
                }



            }
        });
    
    }
    
    
    
    //获取选种的部门信息

    function GetPBDEPEntity(PBDEPDC) {
        $.ajax({
                type: "post",
                dataType: "text",
                url: "101010.ashx",            
                data: "action=Get&PBDEPDC=" + PBDEPDC,
                complete :function(){$("#load").hide();},
                success: function(msg){
                
                var dataarray=new Array();
                dataarray=msg.split("|");
                
                 $("#txtPBDEPDC").val(dataarray[0]);
                 $("#txtPBDEPDN").val(dataarray[1]);
                 $("#txtPBDEPDEN").val(dataarray[2]);
                 $("#txtPBDEPDTWN").val(dataarray[3]);
                 
                 if (dataarray[4]=="1")
                 {
                        $("#chkPBDEPUS").attr("checked",true);         
                 }
                 else
                 {
                         $("#chkPBDEPUS").attr("checked",'');     
                 
                 }
                 $("#txtPBDEPOI").val(dataarray[5]);
                 $("#txtPBDEPPDC").val(dataarray[6]);
                 $("#txtPBDEPID").val(dataarray[7]);
                 $("#txtPBDEPPID").val(dataarray[8]);
                 $("#txtPBDEPPDN").val(dataarray[9]);
                 $("#txtNewPBDEPPID").val(dataarray[8])
                 $("#txtOldPBDEPPDC").val(dataarray[6]);
                }
            });
    
    }
    //取消选择
    function UnSelectNode()
    {
        if(selectedNode != null)selectedNode.parent().attr("class","TreeView1_0");
        selectedNode = null;
        $("#TreeView1_SelectedNode").val("")
        $("#selectedvalue").val("");
        $("#nName").val("");
    }
    
    //改变节点文本
    function EditNode()
    {
        DoExpand();
        selectedNode.text($("#txtPBDEPDN").val());
    }
    
    //删除节点
    function DelNode() {

        //add Rio  DoExpand();

	    var parentdiv = $(selectedNode.parents("div")[0]);
      if(parentdiv.find("table").length == 1)//这个用来判断是否有兄弟节点
      {
            //如果只有一个节点,没有其他兄弟节点的话，就需要把这个节点所在的div一起删掉
   
            parentdiv.remove();
            var parentNodeID = parentdiv.attr("id").replace("t","n").replace("Nodes","");
           // $("#"+parentNodeID).parent().html("<img src="+TreeView1_Data.images[10]+" alt=\"\" />");
           // $("#"+parentNodeID).parent().html("<img src="+TreeView1_Data.images[5]+" alt=\"\" />");
            $("#"+parentNodeID).parent().html("<img src="+TreeView1_Data.images[3]+" alt=\"\" />");
            UnSelectNode();
      }
      else {
           
            delnode = selectedNode;
            if($(selectedNode.parents("table")[0]).next().html() == null)
            {                    
                SelectNode($(selectedNode.parents("table")[0]).prev().find("span")[0]);
            }
            else
            {
                
                SelectNode($(selectedNode.parents("table")[0]).next().find("span")[0]);
            }
            delnode.parents("table").remove();//有兄弟节点，只删除节点本身的table
      }
    }
    
    //添加节点
    function AddNode(nodeText,nodeValue)
    {        
        var id = GetMaxID();
        var num = parseInt(id.split('t')[1]);
        var newNode = newNodeTemplete.replace("$nid$",id.split('t')[0] + "t" + (++num));
        newNode = newNode.replace("$ntext$",nodeText);
        newNode = newNode.replace("$nvalue$",nodeValue);
        var subdiv = selectedNode.attr("id").replace("t","n") + "Nodes";
        DoExpand();
        if($("#"+subdiv).text().length>0)//
        {
            $("#"+subdiv).append(newNode);                
        }
        else
        {
            ndiv = divContainer.replace("$nid$",subdiv);
            selectedNode.parents("table").after(ndiv);
            $("#"+subdiv).html(newNode);
            ntogle = toggle;
            ntogle = ntogle.replace("$ndivid$",subdiv);                
            ntogle = ntogle.replace("$nid$",subdiv.substring(subdiv.length-5,0));
            ntogle = ntogle.replace("$nid$",subdiv.substring(subdiv.length-5,0));
            ntogle = ntogle.replace("$idnum$",++num);
            ntogle = ntogle.replace("$ntext$",selectedNode.text());                
            selectedNode.parents("table").find("td[img]").html(ntogle)
        }        
        SetIndent(subdiv);
    }
    
    String.prototype.trim= function()  
    {  
        return this.replace(/(^\s*)|(\s*$)/g, "");  
    }

    
    
    function btnqPEDECCRPopuClick()
                        {
                           
                            var url='20101002.aspx';
                            url += '?GUID=' + Math.random() ;
                            var ret = window.showModalDialog(url,'HI014','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:480px') ; 

                            if (ret != null) 
                            { 
                              $("#txtOldPBDEPPDC").val(window.document.getElementById("txtPBDEPPDC").value);
                              window.document.getElementById("txtPBDEPPDC").value =ret[1]; 
                              window.document.getElementById("txtPBDEPPDN").value =ret[0];
                              if (ret[1] == "") 
                              {
                                  $("#txtNewPBDEPPID").val("0");//没有选的话，表示是放一级部门
                              
                               }
                              else {
                                  $.ajax({
                                      type: "post",
                                      dataType: "text",
                                      url: "101010.ashx",
                                      data: "action=Get&PBDEPDC=" + ret[1],
                                      complete: function() { $("#load").hide(); },
                                      success: function(msg) {

                                          var dataarray = new Array();
                                          dataarray = msg.split("|");

                                          $("#txtNewPBDEPPID").val(dataarray[7]);
                                          //                                 $("#txtPBDEPDN").val(dataarray[1]);
                                          //                                 $("#txtPBDEPDEN").val(dataarray[2]);
                                          //                                 $("#txtPBDEPDTWN").val(dataarray[3]);
                                          //                                 
                                          //                                 
                                          //                                 $("#txtPBDEPOI").val(dataarray[5]);
                                          //                                 $("#txtPBDEPPDC").val(dataarray[6]);
                                          //                                 $("#txtPBDEPID").val(dataarray[7]);
                                          //                                 $("#txtPBDEPPID").val(dataarray[8]);
                                          //                                 $("#txtPBDEPPDN").val(dataarray[9]);
                                          //                                 $("#txtNewPBDEPPID").val(dataarray[8])
                                      }
                                  });
                              }
                              
                              
                              
                            } 
                            return false;
                        }



                        function addPopuClick() {
                            
                            var url='addItem.aspx';
                            url += '?GUID=' + Math.random() ;
                            var ret = window.showModalDialog(url, 'HI014', 'scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:480px');
                    
                            if (ret != null) {
                                $.ajax({
                                    type: "post",
                                    dataType: "text",
                                    url: "addItem.ashx",
                                    data: "addItemFlagName=PBDEP" + ret[0].toUpperCase() + "&MsgValue=" + escape(ret[1]) + "&type=" + ret[2],
                                    complete: function() { $("#load").hide(); },
                                    success: function(msg) {

                                        var div = document.getElementById("MyAddItem");
                                        // var ItemTxt = document.createTextNode(ret[1]);
                                        var ItemTxt = document.createElement("span");
                                        ItemTxt.innerHTML = ret[1];
                                        var inputTxt = document.createElement("input");
                                        inputTxt.type = "text";
                                        inputTxt.value = "";
                                        inputTxt.name = ret[0];
                                        inputTxt.setAttribute("id", "PBDEP" + ret[0].toUpperCase());
                                        var HideText = document.createElement("input");
                                        HideText.type = "hidden";
                                        HideText.value = ret[2];
                                        var br = document.createElement("<br>");
                                        div.appendChild(ItemTxt);
                                        div.appendChild(inputTxt);
                                        div.appendChild(HideText);
                                        div.appendChild(br);

                                    }
                                });
                            }
                           
                        }
	</script>  

    
</head>

<body>
    <form id="form1" runat="server">
    
<asp:scriptmanager runat="server"></asp:scriptmanager>
    <div>
        <div id="DivTreeView" style="float:left; width:30%;  ">
            
            <asp:TreeView ID="TreeView1" runat="server" OnTreeNodePopulate="TreeView1_TreeNodePopulate" EnableViewState="False"	   OnSelectedNodeChanged="SelectDeptChange" ShowLines="true">
				<Nodes>
					<asp:TreeNode PopulateOnDemand="True" SelectAction="None" Selected="false" Text="" Value="" NavigateUrl="#" ></asp:TreeNode>
				</Nodes>
				<SelectedNodeStyle BackColor="AntiqueWhite" />
	        </asp:TreeView> 
	     </div>
         <div id="DivContent" style="float:left; width:69%; margin-top:30px;">
        
             <div >
                 
              <%--  <cc2:Button ID="btnBrowseMode" runat="server" ShowPostDiv="false" ValidateForm="false" Text="Button_BrowseMode" Enabled="false"   />
               <cc2:Button ID="btnEditMode" runat="server" ShowPostDiv="false" ValidateForm="false" Text="Button_EditMode" Width="95px"  OnClick="btnBrowseMode_Click" />
               <cc2:Button ID="btnAdd" runat="server" ShowPostDiv="false" ValidateForm="false" Text="Button_Add"   />--%>
	            <%--<cc2:Button ID="btnAddDept" runat="server" ShowPostDiv="false" ValidateForm="false" Text="Button_AddDept"  Width="110px" OnClick="btnBrowseMode_Click"  />
                <cc2:Button ID="btnAddChildDept" runat="server" ShowPostDiv="false" ValidateForm="false" Text="Button_AddChildDept" Width="110px"  OnClick="btnBrowseMode_Click" />
	            <cc2:Button ID="btnSubmit" runat="server" Text="Button_DafaultSubmit"  Width="95px" OnClick="btnBrowseMode_Click" />
	            <cc2:Button ID="btnDel" runat="server" ShowPostDiv="false" ValidateForm="false" AutoPostBack="false" Text="Button_Del" ButtonImgUrl="../images/del.gif" Width="95px"   />--%>
                <%--<asp:Button ID="btnEditMode" runat="server" Text="编辑模式"  OnClick="btnBrowseMode_Click"  CssClass="ManagerButton" Width="100px" Height="25px"/>--%>
                 <%--<asp:Button ID="btnAddDept" runat="server" Text="Button"  OnClick="btnBrowseMode_Click"  CssClass="ManagerButton" Width="100px" Height="25px"/>
                 <input id="btnModify" type="button" value="更 新"  class="ManagerButton" style="width:100px; height:25px" />--%>
                 <button id="btnEditMode" type="button" class="ManagerButton" ><img src="../images/submit.gif" alt="" />编辑模式</button>
                 <button id="btnModify" type="button" class="ManagerButton" style="width:95px;"><img src="../images/submit.gif" alt=""/>更新保存</button>
                 <button id="btnAddDept" type="button" class="ManagerButton" style="width:95px;"><img src="../images/submit.gif" alt=""/>新增部门</button>
                 <button id="btnSubmit" type="button" class="ManagerButton" style="width:95px;"><img src="../images/submit.gif" alt=""/>保 存</button>
                 <button id="btnDel" type="button" class="ManagerButton" style="width:95px;"><img src="../images/submit.gif" alt=""/>删除所选</button>
              <!--   <cc2:Button ID="btnNotUsedToView" runat="server" ShowPostDiv="false" ValidateForm="false" Text="Button_DafaultSubmit" OnClick="btnNotUsedToView_Click"  ButtonImgUrl="../images/submit.gif"/> -->
             </div>
                                                
              <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server"   UpdateMode="Conditional">
                                            <ContentTemplate>       --%>                           
         
             <div id="wrapResumeContent">
                     
                         
                       <%--    <div class="conblk2" id="ProfileContainer" xmlns:fo="http://www.w3.org/1999/XSL/Format">
		                    <div class="con" id="wrapProfile"  >
					                    <div id="ProfileView" >
					                    <div class="formblk" >
					                    <div class="textblk" >--%>
                                            
                                            
                                            <div class="clear">
                                              <div class="half">
                                                 <label class="char5">
                                                    <asp:Label ID="lblPBDEPDC" runat="server" Text="">
                                                    </asp:Label>
                                                 </label>
                                                   <div class="iptblk">
                                                     <input type="text" id="txtPBDEPDC"  readonly="readonly"  onfocus="this.className='FormFocus';"  onblur="this.className='FormBase';" class="FormBase"></input>
                                                   </div>
                                              </div>
                                                  <div class="half">
                                                      <label class="char5">
                                                          <asp:Label ID="lblPBDEPDEN" runat="server" Text="">
                                                           </asp:Label>
                                                       </label>
                                                   <div class="iptblk">
                                                        <input type="text" id="txtPBDEPDEN"  readonly="readonly"  onfocus="this.className='FormFocus';"  onblur="this.className='FormBase';" class="FormBase">
                                                        </input>
                                                   </div>
                                               </div>
                                             </div>
                                             <div class="clear">
                                                <div class="half">
                                                   <label class="char5">
                                                      <asp:Label ID="lblPBDEPDN" runat="server" Text="">
                                                      </asp:Label>
                                                    </label>
                                                    <div class="iptblk">
                                                        <input type="text" id="txtPBDEPDN"  readonly="readonly"  onfocus="this.className='FormFocus';"  onblur="this.className='FormBase';" class="FormBase">
                                                        </input>
                                                     </div>
                                                  </div>
                                                 <div class="half">
                                                 <label class="char5">
                                                 <asp:Label ID="lblPBDEPDTWN" runat="server" Text="">
                                                 </asp:Label>
                                                 </label>
                                                 <div class="iptblk">
                                                 <input type="text" id="txtPBDEPDTWN"  readonly="readonly"  onfocus="this.className='FormFocus';"  onblur="this.className='FormBase';" class="FormBase"></input></div></div>
                                             </div>
                                             
                                             <div class="clear"><div class="half"><label class="char5"><asp:Label ID="lblPBDEPUS" runat="server" Text=""></asp:Label></label><div class="iptblk"><input type="checkbox" id="chkPBDEPUS"   checked="checked" disabled="disabled"></div></div>
                                                                <div class="half"><label class="char5"><asp:Label ID="lblPBDEPOI" runat="server" Text=""></asp:Label></label><div class="iptblk"><input type="text" id="txtPBDEPOI"  readonly="readonly"  onfocus="this.className='FormFocus';"  onblur="this.className='FormBase';" class="FormBase"></input></div></div>
                                             </div>
                                             <div  class="clear"><label class="char5"> <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></label></div>
                                             <div class="clear"><div class="half"><label class="char5"><asp:Label ID="lblPBDEPPDC" runat="server" Text=""></asp:Label></label><div class="iptblk"><input type="text" id="txtPBDEPPDC"  readonly="readonly"  onfocus="this.className='FormFocus';"  onblur="this.className='FormBase';" class="FormBase"></input>
                                             
                                             <img id="ImageqPEDECCR" onclick="btnqPEDECCRPopuClick()" src="../images/arrow_black.gif" title="搜索"  runat="server"    />   </div></div>
                                                                
                                             <div class="half">
                                                  <label class="char5">
                                                     <asp:Label ID="lblPBDEPPDN" runat="server" Text="上级部门名称"></asp:Label></label>
                                                  <div class="iptblk">
                                                      <input type="text" id="txtPBDEPPDN"  readonly="readonly"  onfocus="this.className='FormFocus';"  onblur="this.className='FormBase';" class="FormBase">
                                                      </input>
                                                  </div>
                                             </div>
                                             </div>
                                             <div class="clear">
                                               <div class="half">
                                              <!--  <a onclick="addPopuClick()" href="javascript:void(0)"> 增加新栏位   </a><div id="MyAddItem"></div>-->
                                               </div>
                                              
                                             </div>
                                             <input type="hidden"  id="txtPBDEPID" runat="server" />
                                             <input  type="hidden" id="txtPBDEPPID" runat="server" />
                                             <input  type ="hidden" id="txtNewPBDEPPID" runat="server" />
                                             <input  type ="hidden" id="txtOldPBDEPPDC" runat="server" />
                                             
                                             
                                             
<%--                                           
                                            
      				                    </div>
					                    </div>
					                    </div>

	                           </div>
	                        </div>--%>
	                           

            
             </div>
         
         
         </div>
         <div id="load">
		     <div style="width:100%; height:100%; background-position:center; background-image: url(../images/ajax-loading.gif); background-repeat: no-repeat;"></div>
		</div>
		
          <%--</ContentTemplate>
         <Triggers>
           <asp:AsyncPostBackTrigger ControlID="btnEditMode" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnAddDept" EventName="Click" />
           <asp:AsyncPostBackTrigger ControlID="btnAddChildDept" EventName="Click" />
           <asp:AsyncPostBackTrigger ControlID="TreeView1" EventName="SelectedNodeChanged" />
         </Triggers>
        </asp:UpdatePanel>--%>
    </div>
         
   
    </form>
</body>
</html>
