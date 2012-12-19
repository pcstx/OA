<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowFiles.aspx.cs" Inherits="GOA.Basic.ShowFiles" %>

<%@ Register Assembly="Brettle.Web.NeatUpload" Namespace="Brettle.Web.NeatUpload"
    TagPrefix="Upload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>

<head runat="server">
    <title>文件显示</title>
    <script language="javascript" type="text/javascript">
       var  FileFullName ;
       var  FileTailName ;
       var  FileES ;
       document.onkeydown = function()
       {  
           
            var   k = window.event.keyCode; 
            if(window.event.ctrlKey   &&   k   ==   84)      // Ctrl   +  S
                window.event.returnValue = false; 
       }
    
        function EditOffice(fullname, tailName) {
            
            var top = screen.height - 70;
            var left = screen.width - 10;
            var src = encodeURI("Office.aspx?ImgPath=" + escape(fullname) + "&tailName=" + escape(tailName));
            var returnValue = window.showModalDialog(src, "", "scroll:1;status:0;help:0;resizable:1;dialogWidth:850px;dialogHeight:585px");

        }

        function csMenu(_object, _menu) {
            this.IEventHander = null;
            this.IFrameHander = null;
            this.IContextMenuHander = null;
            document.getElementById("Menu1").style.display = "none";
            this.Show = function(_menu) {
                var e = window.event || event;
                if (e.button == 2) {
                    if (window.document.all) {
                        this.IContextMenuHander = function() { return false; };
                        document.attachEvent("oncontextmenu", this.IContextMenuHander);
                    }
                    else {
                        this.IContextMenuHander = document.oncontextmenu;
                        document.oncontextmenu = function() { return false; };
                    }

                    window.csMenu$Object = this;
                    this.IEventHander = function() { window.csMenu$Object.Hide(_menu); };

                    if (window.document.all)
                        document.attachEvent("onmousedown", this.IEventHander);
                    else
                        document.addEventListener("mousedown", this.IEventHander, false);

                    _menu.style.left = e.clientX;
                    _menu.style.top = e.clientY;
                    _menu.style.display = "";

                    if (this.IFrameHander) {
                        var _iframe = document.getElementById(this.IFrameHander);
                        _iframe.style.left = e.clientX;
                        _iframe.style.top = e.clientY;
                        _iframe.style.height = _menu.offsetHeight;
                        _iframe.style.width = _menu.offsetWidth;
                        _iframe.style.display = "none";
                    }
                }
            };

            this.Hide = function(_menu) {
                var e = window.event || event;
                var _element = e.srcElement;
                do {
                    if (_element == _menu) {
                        return false;
                    }
                }
                while ((_element = _element.offsetParent));

                if (window.document.all)
                    document.detachEvent("on" + e.type, this.IEventHander);
                else
                    document.removeEventListener(e.type, this.IEventHander, false);

                if (this.IFrameHander) {
                    var _iframe = document.getElementById(this.IFrameHander);
                    _iframe.style.display = "none";
                }

                _menu.style.display = "none";

                if (window.document.all)
                    document.detachEvent("oncontextmenu", this.IContextMenuHander);
                else
                    document.oncontextmenu = this.IContextMenuHander;
            };

            this.initialize = function(_object, _menu) {
                window._csMenu$Object = this;
                var _eventHander = function() { window._csMenu$Object.Show(_menu); };

                _menu.style.position = "absolute";
                _menu.style.display = "none";
                _menu.style.zIndex = "1000000";

                if (window.document.all) {
                    var _iframe = document.createElement('iframe');
                    document.body.insertBefore(_iframe, document.body.firstChild);
                    _iframe.id = _menu.id + "_iframe";
                    this.IFrameHander = _iframe.id;

                    _iframe.style.position = "absolute";
                    _iframe.style.display = "none";
                    _iframe.style.zIndex = "999999";
                    _iframe.style.border = "0px";
                    _iframe.style.height = "0px";
                    _iframe.style.width = "0px";

                    _object.attachEvent("onmouseup", _eventHander);
                }
                else {
                    _object.addEventListener("mouseup", _eventHander, false);
                }
            };

            this.initialize(_object, _menu);
        }

        function Get(fullname, tailName, id, fileES) {

            document.getElementById("Menu1").style.display = "none";
//            if (fileES == 0)
//                document.getElementById("biaoqian").innerHTML = "签出";
//            else if (fileES == 1)
//                document.getElementById("biaoqian").innerHTML = "签入";
            var MM = new csMenu(document.getElementById(id), document.getElementById("Menu1"));
            FileFullName = fullname;
            FileTailName = tailName;
            FileES = fileES;
            window.document.getElementById("hiddenFileName").value = fullname;
            window.document.getElementById("hiddenFileES").value = fileES;
 
        }

        function Open() {
            document.getElementById("Menu1").style.display = "none";
            var top = screen.height - 70;
            var left = screen.width - 10;
            var src = encodeURI("Office.aspx?ImgPath=" + escape(FileFullName) + "&tailName=" + escape(FileTailName));
            var returnValue = window.open(src, "", "scroll:1;status:0;help:0;resizable:1;dialogWidth:850px;dialogHeight:585px");

        }

        function CheckInOut(FullName,TailName,ES) {

                document.getElementById("Menu1").style.display = "none";
                var top = screen.height - 70;
                var left = screen.width - 10;
                var src = encodeURI("OfficeCheckInOut.aspx?ImgPath=" + escape(FullName) + "&tailName=" + escape(TailName)) + "&fileES=" + escape(ES);
                var returnValue = window.open(src, "", "scroll:1;status:0;help:0;resizable:1;dialogWidth:850px;dialogHeight:585px");
           
        }


        function GetAllEdtion() {
            document.getElementById("Menu1").style.display = "none";
            var top = screen.height - 70;
            var left = screen.width - 10;
            var src = encodeURI("GetAllEdtion.aspx?ImgPath=" + escape(FileFullName) + "&tailName=" + escape(FileTailName));
            //  var returnValue = window.showModalDialog(src, "", "scroll:1;status:0;help:0;resizable:1;dialogWidth:850px;dialogHeight:585px");
            var returnValue = window.open(src, "", "scroll:1;status:0;help:0;resizable:1;dialogWidth:850px;dialogHeight:585px");
        }


        function Edition() {
            document.getElementById("Menu1").style.display = "none";
            var top = screen.height - 70;
            var left = screen.width - 10;
            var src = encodeURI("Edition.aspx?ImgPath=" + escape(FileFullName) + "&tailName=" + escape(FileTailName));
            var returnValue = window.open(src, "", "scroll:1;status:0;help:0;resizable:1;dialogWidth:850px;dialogHeight:585px");
         
        }
   
   
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" EnablePartialRendering="false">
     </cc1:ToolkitScriptManager>

    <div>
 
        <div class="conblk2" >
            <div class="clear">
            <input type="hidden" id="hiddenFileName" runat="server" />
            <input type="hidden" id="hiddenFileES" runat="server" />
                              
                <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                    <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="文件目录">
                        <ContentTemplate>
                        
                            <asp:Panel runat="server" ID="PanelUserContrl" Height="400px">
                            <div>搜索文件
                             <input type="text" id="fileKey" runat="server" />
                             <input type="button" id="findFile" runat="server"   onserverclick="SearchFile"  value="搜 索"/>
                           
         
                             </div>
                            <div id="showfiles" runat="server" >
                            </div>
                             
                            </asp:Panel>
                            
                            
                           
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    
                    <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="文件上传">
                        <ContentTemplate>
                            <asp:Panel runat="server" ID="Panel2" Height="300px" Width="400px">
                                <asp:FileUpload ID="FileUpLoad1" runat="server" />  
                                 <br />  
                                   <asp:Button ID="btnFileUpload" runat="server"   OnClick="btnFileUpload_Click" Text="文件上传"   />  
                                   <asp:Label ID="lblMessage" runat="server"></asp:Label>  
                     

                                       <%-- <Upload:ProgressBar ID="ProgressBar2" runat='server' Inline="True" Triggers="">
                                        </Upload:ProgressBar>
      <br />
    <Upload:InputFile ID="AttachFile" runat="server" />
    <br />
    <br />
<asp:Button ID="BtnUP" runat="server" onclick="btnFileUpload_Click" Text="上 传" />--%>
<br />
                                文件有限期至:
          <input type="text" name="EndTime" id="EndTime" onfocus="new WdatePicker(this,'%Y-%M-%D %h:%m:%s',true)" runat="server"/>
                            </asp:Panel>
                           
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    
                    
                </ajaxToolkit:TabContainer>
                
                <div id="Menu1" style="background-color:White; border:1px solid #cccccc; padding:10px; display:none">
                <ul>
                 <li><a href="javascript:void()" onclick="Open()" id="OpenBtn">打开</a></li>
                 <li><a href="javascript:void()" onclick="Edition()" id="EditionBtn" runat="server">版本控制</a></li>
                <li><a href="javascript:void()" runat="server" onserverclick="DeleteClick"  id="DeleteBtn" >删除</a></li>
<%--                <li><a href="javascript:void()"  runat="server"  onserverclick="UpdateESClick" id="UpdateBtn"> <div id="biaoqian" runat="server"></div></a></li>--%>
                      <li><a href="javascript:void()"  runat="server"  onserverclick="CheckInClick" id="CheckInBtn">签入</a></li>
                              <li><a href="javascript:void()"  runat="server"  onserverclick="CheckOutClick" id="CheckOutBtn">签出</a></li>
                    <li><a href="javascript:void()" runat="server" onserverclick="CancelClick"  id="CancelBtn" >作废</a></li>
                 <li><a href="javascript:void()" onclick="GetAllEdtion()" id="ShowEdtion">查看所有版本</a></li>
                 </ul>
               </div>

            </div>
         </div>
      </div>
    </form>
</body>
</html>
