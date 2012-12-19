var Pic = Class.create();

Pic.prototype = {
	
	initialize : function () {
		var wc = this;
		wc.parent = null;
		wc.ajax = new CDrag.Ajax;
	},
	
	edit : function (o) {  //刷新
	    var wc = this, ajax = wc.ajax;
	    wc.parent.content.innerHTML = "loading";
	    ajax.send("News.ashx?type=pic&", wc.load.bind(wc));   //返回图片的url
	},
	
	load : function (json) {
		var wc = this;
		//wc.parent.content.innerHTML = '<img width="180" height="100" alt="" src="' + json + '" \/>';
		wc.parent.content.innerHTML = '<iframe style="width:100%;height:100%;border:0px;margin:0px;padding:0px;" src="' + json + '" \/>';
        
	},
	
	open : function () {
		var wc = this, ajax = wc.ajax;
		wc.parent.content.innerHTML = "loading";
		ajax.send("News.ashx?type=pic&", wc.load.bind(wc));   //返回图片的url
	}
	
};

Pic.loaded = true;