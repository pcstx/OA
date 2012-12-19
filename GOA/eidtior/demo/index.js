if (document.body && (!document.readyState || document.readyState == 'complete')) {
    var script = document.createElement('script');
    script.type = 'text/javascript';
    script.src = "../jquery/jquery-1.5.2.min.js";
    document.body.appendChild(script);
}
else {
    document.write(
			'<script type="text/javascript" src="' + "../jquery/jquery-1.5.2.min.js" + '"></script>');
}