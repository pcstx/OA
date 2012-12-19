function importJS(src) {
    if (src.lastIndexOf(".js") != (src.length - 2) && src.lastIndexOf(".JS") != (src.length - 2))
        jpath = src + '.js';
    else
        jpath = src;
    var headerDom = document.getElementsByTagName('head').item(0);
    var jsDom = document.createElement('script');
    jsDom.type = 'text/javascript';
    jsDom.src = jpath; headerDom.appendChild(jsDom);
} 