//----------------------------
// http://webabcd.cnblogs.com/
//----------------------------

function yy_sl_copyTextToHiddenField(source, destination)
{
/// <summary>将Label控件的的值赋给隐藏控件</summary>

    document.getElementById(destination).value = document.getElementById(source).innerHTML;
}