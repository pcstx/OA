<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkflowPrint.aspx.cs" Inherits="GOA.WF.WorkflowPrint" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
     <script type="text/javascript" src="../fceform/eprint/printer.js"></script>
    <script type="text/javascript"  src="../fceform/eprint/eprint.js"></script>
	<script type="text/javascript" src="../fceform/js/fcpub.js"></script>
    <script type="text/javascript" src="../fceform/js/fcopendj.js"></script>
    
       <script  type="text/javascript"  language="javascript" >

        Printer.Init = function() {
      
        Printer.inited = true;
        
         //  
            }
           


            function LoadXMLFiles(fileXLM, dataSetName, dataSetID) {
                Printer.inited = false;
                Printer.typeid = "WF";
                if (dataSetID == "DataSetMain")
                Printer.clear();
                //获得后端的数据集合
                var oM, oD;

                oM = SetDom(fileXLM);
                if (oM.documentElement == null) {
                    alert(fileXLM);
                }
                //Printer.adddataset
                if (oM.documentElement != null) {
                    Printer.AddDataset(dataSetID, dataSetName, oM.documentElement.xml);
                }

                
                //  Printer.AddRelation("POPrintMain.t_orno=POPrintDetail.t_orno and POPrintMain.rptTitle=POPrintDetail.rptTitle ");
                //  Printer.AddRelation("");
                //             
                //////               //获得XML的数据集合
                //                var oTemp = SetDomFile("../POPrintXML/send.xml");
                //                Printer.AddDataset("sendmain", "sendmain", oTemp.documentElement.xml);

                //                var oTemp = SetDomFile("../POPrintXML/senddetail.xml");
                //                Printer.AddDataset("senddetail", "sendmain", oTemp.documentElement.xml);
                //                Printer.AddRelation("sendmain.t_orno=senddetail.t_orno");


                // Printer.oBillXml = SetDom("<root><CompanyCity>昆山</CompanyCity><CompanyName>捷奥比电动车有限公司</CompanyName><CompanyAddress>江苏省昆山市陆家镇陆丰东路10号</CompanyAddress><PrintApprover>王曙光</PrintApprover><PrintVerifier>余远平</PrintVerifier></root>");


            }
	
	
	
   </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left:20px; margin-top:10px">
   <%-- <a onclick="Printer.Design('WorkflowPrintForm_7_7')" href="#">设计</a>--%>
     <cc2:Button ID="btnPrint" runat="server"  ShowPostDiv="false" ValidateForm="false" Text="Button_Print"   Disable="false"    ButtonImgUrl="../../images/printer.gif"   OnClick="btnPrint_Click" />
    </div>
    </form>
</body>
</html>
