
     
//��ʾ
function BOX_show(e) 
{   
    if(document.getElementById(e)==null){return;}
    BOX_layout(e);
    window.onresize = function(){BOX_layout(e);} //�ı䴰�����µ���λ��
    window.onscroll = function(){BOX_layout(e);} //�����������µ���λ��
}

//�Ƴ�
function BOX_remove(e)
{   
}

//��������
function BOX_layout(e)
{

    var a = document.getElementById(e);
    
    //�ж��Ƿ��½����ڲ�

    if (document.getElementById('BOX_overlay')==null)
    { 
        var overlay = document.createElement("div");
        overlay.setAttribute('id','BOX_overlay');
        a.parentNode.appendChild(overlay);
    }
    
    //ȡ�ͻ����������꣬����
    var scrollLeft = (document.documentElement.scrollLeft ? document.documentElement.scrollLeft : document.body.scrollLeft);
    var scrollTop = (document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop);
    var clientWidth = document.documentElement.clientWidth;
    var clientHeight = document.documentElement.clientHeight;
  
    var bo = document.getElementById('BOX_overlay');
    bo.style.left = scrollLeft+'px';
    bo.style.top = scrollTop+'px';
    bo.style.width = clientWidth+'px';
    bo.style.height = clientHeight+'px';
    bo.style.display="";
    //Popup���ڶ�λ
    a.style.position = 'absolute';
    a.style.zIndex=1010;
    a.style.display="";
    a.style.left = "40%";
    a.style.top = "40%";
    
    document.getElementById('setting').style.display="block";
}

        function MaxAddFrom()
        {
            if(document.getElementById('programmaticPopup').style.width=="650px")
            {document.getElementById('programmaticPopup').style.width="100%";}
            else
            {document.getElementById('programmaticPopup').style.width="650px";}
        }
       function MaxQueryFrom()
        {
            if(document.getElementById('programmaticPopupQuery').style.width=="650px")
            {document.getElementById('programmaticPopupQuery').style.width="100%";}
            else
            {document.getElementById('programmaticPopupQuery').style.width="650px";}
        }
        function MaxImportExcelFrom()
        {
            if(document.getElementById('PanelImportExcel').style.width=="650px")
            {document.getElementById('PanelImportExcel').style.width="100%";}
            else
            {document.getElementById('PanelImportExcel').style.width="650px";}
        }


