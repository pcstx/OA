扩展DropDownList控件和ListBox控件


控件使用
1、支持分组功能(optgroup标签)。通过DropDownList控件和ListBox控件的.Items.Add(ListItem item)方法，来为其添加optgroup标签，从而实现分组功能 
http://www.cnblogs.com/webabcd/archive/2008/01/03/1024977.html
使用方法
1)设置属性： 
OptionGroupValue - 用于添加DropDownList(ListBox)控件的分组项的ListItem的Value值（默认为optgroup） 
2)使用DropDownList(ListBox)控件的.Items.Add(ListItem item)方法： 
OptionGroupValue为默认值时：SmartDropDownList.Items.Add(new ListItem("中国", "optgroup"));


OK