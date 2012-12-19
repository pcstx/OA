var News = Class.create();

News.prototype = {

    initialize: function() {
        var wc = this;
        wc.parent = null;
        wc.ajax = new CDrag.Ajax;
    },

    edit: function(o) {
        //alert("�������չ���һ�����������Դ�News.js���Լ����ã��������ڸı�����Ϊ����");
        //	o.content.innerHTML = o.title.innerHTML;
        var wc = this, ajax = wc.ajax;
        wc.parent.content.innerHTML = "loading";
        ajax.send("News.ashx?", wc.load.bind(wc));   //��ȡ��̨������Ϣ
    },

    load: function(json) {
        var wc = this;
        wc.parent.content.innerHTML = json; 
    },

    open: function() {
        var wc = this, ajax = wc.ajax;
        wc.parent.content.innerHTML = "loading";

       // ajax.send("http://www.baidu.com", wc.load.bind(wc)); 
        ajax.send("News.ashx?", wc.load.bind(wc));   //��ȡ��̨������Ϣ
    }

};

News.loaded = true;