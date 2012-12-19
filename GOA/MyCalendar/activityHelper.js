/*----------------------------------日历-活动插件js，相关说明-------------------------------------------
|	功能：生成相关的活动界面，提供相应的活动的增删改查方法
|   作者：mayonghua
|   时间：2011年5月
|	使用方法：在页面引入jquery.js、activityHelper.js、activityHelper.css
|	注意：1、renderTo是必须要填写的
|		  2、在body 中添加以下两个属性防止和页面的划选冲突，onselectstart="return false"(解决IE的划选) style="-moz-user-select: none;"(解决火狐等的划选)
|		  3、通过修改loadDate属性，改变操作的时间	
|	几个重要的对象说明：
|	1、dayOfWeek（返回一周内的各天的数据）
|	-today//今天
|   ------.year//今天所在的年
|	------.month//今天所在的月
|	------.day//今天所在的月
|	------.dayOfThisWeek//今天是所在周的第几天
|	-firstDayOfThisWeek//本周的第一天
|	-------.year
|	-------.month
|	-------.day
|	-lastDayOfThisWeek//本周的最后一天
|	-------.year
|	-------.month
|	-------.day
|	-thisMonthDays//本周所在的月一共有几天
|	-items[]//数组，存放了本周的每一天
|	-------items.year
|	-------items.month
|	-------items.day
|	-------items.weekDay//每天是周几：周一、周二……
|	2、dateOfMonth (返回某个月份的相关数据)
|	-year//年
|	-month//月
|	-day//日
|	-days//该月的天数
|	-firstDayOfWeek//该月的第一天是周几
|	-lastDayOfWeek//该月的最后一天是周几
|	3、strToTime（将字符串类型的时间，转换为时间点）
|	-year
|	-month
|	-day
|	-hour
|	-minute//带格式的分钟数
|	-minutes//分钟
|	-trId//距离该时间最近的时间线的ID
|	-px//时间的最小偏移量
|	-dayOfThisWeek//本周的第几天
|
|init 初始化
|initWeekView  初始化周视图
|initMonthView 初始化月视图
|dayOfWeek 得到一周的各天
|dateOfMonth 对月日期进行相关的计算
|setTodayBackground 改变当天的背景颜色
|selectItem 创建一个鼠标选择框
|popItem 创建一个弹出窗口
|selectItemOfMonth  月视图中的时间点选择
|setPopItemPosition 设置弹出窗口的位置
|isNull 判断字符串是否为空
|idToTime 根据传入的ID，计算离该ID最近的整数时间(整点或半点)
|strToTime 根据传入的时间字符串转换为可以用于绘制TD的相关属性
|getTdInfo  根据传入的TD对象，获得该元素的绝对位置以及宽和高等属性
|activityAddItem  在周视图中，创建一个活动添加窗口
|activityAddItemOfMonth  在月视图中，创建一个活动添加窗口
|toSaveParam 组织将要保存的数据
|toLoadParam 组织将要载入的数据
|saveActivity  保存活动数据
|loadWeekActivity 载入周活动数据
|loadMonthActivity  载入月活动数据 
|drawMonth 将月活动数据绘制到相应位置
|drawMonthActivity 绘制月视图中的活动
|drawActivityItems 绘制月视图中的活动Items
|activitySort  对activity数组进行排序，按照持续时间长短进行排序，持续时间长的在最前面
|refreshItems  获得需要刷新的活动数据
|activityDateFormat  对activity的时间格式进行预处理
|millisecondsOfTwoMonth  得到两个时间之间的毫秒数
|timeParamToActivity  将timeParam转换为activity对象
|drawWeekActivity  将周活动数据绘制到相应位置
|viewActivity  查看活动
|activityViewItem  活动查看数据
|clear 清除系统中的临时DIV
|nowDate  获得当前的时间
|random 用于系统中随机ID的生成
|	
-------------------------------------------------------------------------------------------------------*/
 var ActivityHelper = function(config) {
    this.renderTo = config.renderTo; //该控件将要添加到的元素的ID
    this.savePath = "CalendarEvent.ashx?action=save";  //保存活动的后台地址
    this.loadByWeekPath = "CalendarEvent.ashx?action=loadweek"; //加载周活动的后台地址
    this.viewPath = "CalendarEvent.ashx?action=show"; //查看活动的后台地址
    this.deletePath = "CalendarEvent.ashx?action=delete";  //删除活动的后台地址
    this.loadDate = config.loadDate == "" ?this.nowDate() : config.loadDate; //需要载入的活动的所属日期，如果为空则系统会自动载入本天数据
    this.moreInfoPath = "AddEvent.aspx"; //跳转到活动的详细编辑界面的地址
    this.updatePath = "AddEvent.aspx?action=show";  //活动更新的后台地址
    this.loadByMonthPath = "CalendarEvent.ashx?action=loadmonth";  //月活动加载地址
    this.UserID = config.UserID == "" ? "0" : config.UserID;
    this.PermissionIndex = config.PermissionIndex;
    this.init();
}

ActivityHelper.prototype = {
    init: function() {

        var entity = this;
        if (entity.renderTo == "" || entity.renderTo == null) {
            alert('请配置renderTo属性!!');
            return;
        }
        //  alert(entity.PermissionIndex);
        //初始化周视图
        entity.initWeekView(entity.UserID, entity.PermissionIndex);
        //载入周视图数据
        entity.loadWeekActivity(entity.UserID, entity.PermissionIndex);
    },

    //初始化周视图
    initWeekView: function(UserID, Index) {
        var entity = this;
        entity.clear();
        $(".headDiv").remove();
        $(".monthTable").remove();
        //最顶部的head栏
        var headDiv = '<div id="headDiv" class="headDiv"><div class="toWeek changeBackground">周</div><div class="toMonth normalBackground">月</div></div>';
        $(headDiv).appendTo($("#" + entity.renderTo));
        $(".toWeek").click(function() {
            entity.initWeekView(UserID, Index);
            entity.loadWeekActivity(UserID, Index);
        });
        $(".toMonth").click(function() {
            entity.initMonthView(UserID, Index);
            entity.loadMonthActivity(UserID, Index);
        });

        //开始绘制周视图
        var id = entity.random();
        var timeTable = '<table id="' + id + '" class="timeTable"></table>';
        $(timeTable).appendTo($("#" + entity.renderTo));
        var table_weeks = [];
        table_weeks.push('<tr class="table_weeks" id="table_weeks">');
        table_weeks.push('<td rowspan="2" class="week-title"></td>');
        for (var i = 0; i < 7; i++) {
            table_weeks.push('<td class="week-day"></td>');
        }
        table_weeks.push('</tr>');
        table_weeks.push('<tr class="allDay" id="allDay">');
        for (var i = 0; i < 7; i++) {
            table_weeks.push('<td class="oneDay"></td>');
        }
        table_weeks.push('</tr>');
        $(table_weeks.join('')).appendTo($("#" + id));
        $('<tr class="headBottomLine" id="headBottomLine"><td colspan="8"></td></tr>').appendTo($("#" + id));
        //得到父窗口的宽度，然后计算每个td的宽度
        var parentWidth = $("#" + entity.renderTo).width();
        $("td.week-day").each(function() {
            var width = (parentWidth - $("td.week-title").width()) / 7;
            $(this).width(width);
        });
        //计算生成时间点，共24*2行
        var table_time = [];
        for (var i = 0; i < 24 * 2; i++) {
            var time_day = [];
            time_day.push('<tr id="' + i + '" class="time-row">');
            if (i % 2 == 0) {
                time_day.push('<td rowspan="2" class="time-title"></td>');
            }
            for (var j = 0; j < 7; j++) {
                time_day.push('<td class="timeItem"></td>');
            }
            time_day.push('</tr>');
            table_time.push(time_day.join(''));
        }
        $(table_time.join('')).appendTo($("#" + id));
        //设置时间线的显示样式
        $("table.timeTable").find("tr").each(function() {
            var id = $(this).attr("id");
            if (id >= 0) {
                //如果是整点则采用实线
                if (id % 2 == 0) {
                    $(this).addClass("solid");
                }
                //如果是半点，则采用虚线
                if (id % 2 == 1 && !$(this).is(".solid")) {
                    $(this).addClass("dotted");
                }
            }
        });

        //设置时间显示的相关信息
        var activityDate = entity.dayOfWeek(entity.loadDate);
        //设置起止日期
        var startDate = activityDate.firstDayOfThisWeek.year + '年' + activityDate.firstDayOfThisWeek.month + '月' + activityDate.firstDayOfThisWeek.day + '日';
        var endDate = activityDate.lastDayOfThisWeek.year + '年' + activityDate.lastDayOfThisWeek.month + '月' + activityDate.lastDayOfThisWeek.day + '日';
        $('<span>' + startDate + '-' + endDate + '</span>').appendTo($("#headDiv"));

        //设置载入的时间点
        var timeParam = {};
        timeParam.startDate = activityDate.firstDayOfThisWeek.year + "-" + activityDate.firstDayOfThisWeek.month + "-" + activityDate.firstDayOfThisWeek.day;
        timeParam.endDate = activityDate.lastDayOfThisWeek.year + "-" + activityDate.lastDayOfThisWeek.month + "-" + activityDate.lastDayOfThisWeek.day;
        entity.timeParam = timeParam;

        //设置一周的各个时间
        var items = activityDate.items;
        var i = 0;
        $("td.week-day").each(function() {
            if (i <= 6) {
                var day = items[i];
                var title = day.month + '/' + day.day + '(' + day.weekDay + ')';
                $(this).text(title);
                i++;
            }
        });
        //设置时间段
        var i = 0;
        $("td.time-title").each(function() {
            var time;
            if (i < 12) {
                time = '上午' + i + '点';
            } else if (i == 12) {
                time = '中午' + i + '点';
            } else {
                time = '下午' + i + '点';
            }
            $(this).text(time);
            i++;
        });
        //将本天的背景颜色进行设置
        var today = activityDate.today;
        var index = today.dayOfThisWeek;
        entity.setTodayBackground(index);
        //为每个时间单元格添加点击事件
        if (UserID == "0" || index != 0) {
            $("td.timeItem").click(function() {
                entity.clear();
                var id = parseInt($(this).parent().attr("id"));
                var startTime = entity.idToTime(id);
                var endTime = entity.idToTime(id + 1);
                //创建一个selectItem
                var selectItemId = entity.selectItem();
                var tdInfo = entity.getTdInfo($(this));
                if (tdInfo == null) {
                    alert("获得时间信息失败");
                    return;
                }
                //设置位置及大小
                $("#" + selectItemId).css({ left: tdInfo.left + 'px', top: tdInfo.top + 'px' });
                $("#" + selectItemId).width(tdInfo.width + 1);
                $("#" + selectItemId + " .content").height(tdInfo.height * 2 - 3 - 15);
                $("#" + selectItemId + " .head").text(startTime.fullTime + "-" + endTime.fullTime);
                //组织要保存的时间信息
                var today = items[tdInfo.index];
                var timeParam = {};
                timeParam.startDate = today;
                timeParam.endDate = today;
                timeParam.startTime = startTime;
                timeParam.endTime = endTime;
                entity.timeParam = timeParam;

                //创建一个弹出窗口
                var popItemId = entity.popItem();
                //设置弹出框的位置
                var position = {};
                position.left = tdInfo.left;
                position.top = tdInfo.top;
                entity.setPopItemPosition(position, popItemId);
                //为弹出框添加内容
                var timeTile = today.month + "月" + today.day + "日(" + today.weekDay + ")," + " " + startTime.fullTime + "-" + endTime.fullTime;
                entity.activityAddItem(popItemId, timeTile, selectItemId, UserID, Index);
            });

            //添加鼠标划选
            var isPress = false;
            var flag = 0;
            var timeParam = {};
            var selectItemId;
            var startTdInfo = null;
            $('body').mousedown(function(e) {
                if (e.which != 1) {
                    return false;
                }
                isPress = true;
                flag = 0;
            }).mouseup(function() {
                isPress = false;
                if (startTdInfo == null) {
                    return false;
                }
                //创建一个弹出窗口
                var popItemId = entity.popItem();
                //设置弹出框的位置
                var position = {};
                position.left = startTdInfo.left;
                position.top = startTdInfo.top;
                entity.setPopItemPosition(position, popItemId);
                var timeTile = timeParam.startDate.month + "月" + timeParam.startDate.day + "日(" + timeParam.startDate.weekDay + ")," + " " + timeParam.startTime.fullTime + "-" + timeParam.endTime.fullTime;
                entity.activityAddItem(popItemId, timeTile, selectItemId, UserID, Index);
                timeParam = {};
                startTdInfo = null;
            });
            $("td.timeItem").mousemove(function() {
                if (isPress) {
                    if (flag == 0) {
                        //创建一个div
                        entity.clear();
                        var id = parseInt($(this).parent().attr("id"));
                        var startTime = entity.idToTime(id);
                        var endTime = entity.idToTime(id + 1);
                        //创建一个selectItem
                        selectItemId = entity.selectItem();
                        var tdInfo = entity.getTdInfo($(this));
                        startTdInfo = tdInfo;
                        if (tdInfo == null) {
                            alert("获得时间信息失败");
                            return;
                        }
                        startTdInfo.top = tdInfo.top;
                        //设置位置及大小
                        $("#" + selectItemId).css({ left: tdInfo.left + 'px', top: tdInfo.top + 'px' });
                        $("#" + selectItemId).width(tdInfo.width + 1);
                        $("#" + selectItemId + " .content").height(tdInfo.height - 3 - 15);
                        $("#" + selectItemId + " .head").text(startTime.fullTime + "-" + endTime.fullTime);
                        var today = items[tdInfo.index];
                        timeParam.startDate = today;
                        timeParam.endDate = today;
                        timeParam.startTime = startTime;
                        timeParam.endTime = endTime;
                        entity.timeParam = timeParam;
                    } else if (flag == 1) {
                        var id = parseInt($(this).parent().attr("id"));
                        var endTime = entity.idToTime(id + 1);
                        var tdInfo = entity.getTdInfo($(this));
                        var height = tdInfo.top - startTdInfo.top + tdInfo.height + 1 - 3 - 3 - 15;
                        $("#" + selectItemId + " .content").height(height);
                        timeParam.endTime = endTime;
                        entity.timeParam = timeParam;
                        //设置显示时间
                        var time = entity.timeParam.startTime.fullTime + "-" + entity.timeParam.endTime.fullTime;
                        if (entity.timeParam.startTime.fullTime == entity.timeParam.endTime.fullTime) {
                            time = entity.timeParam.startTime.fullTime;
                        }
                        $("#" + selectItemId + " .head").text(time);
                    }
                    flag = 1;
                }
            });
        }
    },

    //初始化月视图
    initMonthView: function(UserID, Index) {
        var entity = this;
        entity.clear();
        $(".headDiv").remove();
        $(".timeTable").remove();
        $(".selectItem").remove();
        //时间格式为yyyy-MM-dd
        if (entity.isNull(entity.loadDate)) {
            alert("时间错误");
            return;
        }
        //最顶部的head栏
        var headDiv = '<div id="headDiv" class="headDiv"><div class="toWeek normalBackground">周</div><div class="toMonth changeBackground">月</div></div>';
        $(headDiv).appendTo($("#" + entity.renderTo));
        $(".toWeek").click(function() {
            entity.initWeekView(UserID);
            entity.loadWeekActivity(UserID);
        });
        $(".toMonth").click(function() {
            entity.initMonthView(UserID);
            entity.loadMonthActivity(UserID);
        });
        //开始绘制周视图
        var id = entity.random();
        var monthTable = '<table id="' + id + '" class="monthTable"></table>';
        $(monthTable).appendTo($("#" + entity.renderTo));
        var table_weeks = [];
        table_weeks.push('<tr class="table_weeks" id="table_weeks">');
        for (var i = 0; i < 7; i++) {
            table_weeks.push('<td class="week-day"></td>');
        }
        table_weeks.push('</tr>');
        $(table_weeks.join('')).appendTo($("#" + id));

        var weeks = ['周日', '周一', '周二', '周三', '周四', '周五', '周六'];
        //将周数据添加到顶部视图中
        var i = 0;
        $("td.week-day").each(function() {
            $(this).text(weeks[i]);
            i++;
        });
        //得到父窗口的宽度，然后计算每个td的宽度[将单元格的宽度设置为固定宽度，防止文字过长，影响界面效果]
        var parentWidth = $("#" + entity.renderTo).width();
        $("td.week-day").each(function() {
            var width = parentWidth / 7;
            $(this).width(width);
        });
        //计算该月的各天
        var dateInfo = entity.dateOfMonth(entity.loadDate);
        entity.days = dateInfo.days;
        var monthTd = [];
        var colnums = 0;
        //首先绘制该月第一周的空白时间
        for (var i = 0; i < dateInfo.firstDayOfWeek; i++) {
            if (colnums == 0) {
                monthTd.push('<tr class="dayTr">');
            }
            monthTd.push('<td class="noday"></td>');
            colnums++;
        }
        //绘制该月的各个时间点
        for (var i = 1; i <= dateInfo.days; i++) {
            if (colnums == 0) {
                monthTd.push('<tr class="dayTr">');
            }
            monthTd.push('<td class="dayTd" id="' + i + '"><div class="dayTime">' + i + '</div></td>');
            colnums++;
            if (colnums == 7) {
                monthTd.push("</tr>");
                colnums = 0;
            }
        }
        //绘制该月最后一周的空白时间
        for (var i = 0; i < 6 - dateInfo.lastDayOfWeek; i++) {
            monthTd.push('<td class="noday"></td>');
        }
        monthTd.push("</tr>");
        $(monthTd.join("")).appendTo($("#" + id));
        //为每一天添加一个table
        var dayTimeHeight = $('td.dayTd').find(".dayTime").height();
        var tdHeight = $('td.dayTd').height();
        var tableHeight = tdHeight - dayTimeHeight;
        var counts = tableHeight / 16;
        var table = [];
        table.push('<table class="itemTable">');
        for (var i = 1; i <= counts; i++) {
            table.push('<tr class="itemTr"><td class="itemTd"></td></tr>');
        }
        table.push('</table>');
        //将table添加到每一天里面		
        $("td.dayTd").each(function() {
            $(table.join('')).appendTo($(this));
        });
        $('<span>' + dateInfo.year + '年' + dateInfo.month + '月</span>').appendTo($("#headDiv"));
        //为每一天添加点击事件
        //   if (UserID == "0" || Index != 0) {
        $("td.dayTd").click(function() {
            entity.clear();
            $("td.dayTd").removeClass("tempSelect");
            var id = $(this).attr("id");
            var popItemId = entity.popItem();
            var tdInfo = entity.getTdInfo($(this));
            $(this).addClass("tempSelect");
            var position = {};
            position.left = tdInfo.left;
            position.top = tdInfo.top + 15;
            entity.setPopItemPosition(position, popItemId);
            //为弹出框添加内容
            var timeTile = dateInfo.month + "月 " + id + "日";
            entity.activityAddItemOfMonth(popItemId, timeTile, id, UserID, Index);
            //组织将要提交的数据
            dateInfo.day = id;
            var timeParam = {};
            timeParam.startDate = dateInfo;
            timeParam.endDate = dateInfo;
            var startTime = {};
            startTime.hour = 0;
            startTime.second = 0;
            timeParam.startTime = startTime;
            timeParam.endTime = startTime;
            entity.timeParam = timeParam;

        });
        //添加鼠标移动事件
        var sId;
        var eId;
        var selectTd = null;
        var isPress = false;
        var flag = 0;
        $("body").mousedown(function(e) {
            if (e.which != 1) {
                return false;
            }
            isPress = true;
            flag = 0;
        }).mouseup(function() {
            isPress = false;
            flag = 1;
            if (selectTd == null) {
                return;
            }
            var popItemId = entity.popItem();
            var tdInfo = entity.getTdInfo(selectTd);
            var position = {};
            position.left = tdInfo.left;
            position.top = tdInfo.top + 15;
            entity.setPopItemPosition(position, popItemId);
            //为弹出框添加内容
            var time;
            if (sId == eId) {
                time = sId + "日";
            } else if (sId < eId) {
                time = sId + "日-" + eId + "日";
            } else {
                var tempId;
                tempId = eId;
                eId = sId;
                sId = tempId;
                time = sId + "日-" + eId + "日";
            }
            var timeTile = dateInfo.month + "月 " + time;
            //组织需要提交的时间对象
            var timeParam = {};
            //开始日期
            var startDate = {};
            startDate.day = sId;
            startDate.year = dateInfo.year;
            startDate.month = dateInfo.month;
            timeParam.startDate = startDate;
            //结束日期
            var endDate = {};
            endDate.day = eId;
            endDate.year = dateInfo.year;
            endDate.month = dateInfo.month;
            timeParam.endDate = endDate;
            //开始时间
            var startTime = {};
            startTime.hour = 0;
            startTime.second = 0;
            timeParam.startTime = startTime;
            //结束时间
            timeParam.endTime = startTime;
            entity.timeParam = timeParam;
            entity.activityAddItemOfMonth(popItemId, timeTile, eId, UserID, Index);
            selectTd = null;
        });
        $("td.dayTd").mousemove(function() {
            if (isPress) {
                entity.clear();
                if (flag == 0) {
                    //获得开始的ID
                    sId = parseInt($(this).attr("id"));
                    eId = sId;
                    selectTd = $(this);
                    $(this).addClass("tempSelect");
                } else if (flag == 1) {
                    selectTd = $(this);
                    eId = parseInt($(this).attr("id"));
                    if (sId < eId) {
                        $("td.dayTd").removeClass("tempSelect");
                        for (var i = sId; i <= eId; i++) {
                            $("#" + i).addClass("tempSelect");
                        }
                    } else {
                        $("td.dayTd").removeClass("tempSelect");
                        for (var i = eId; i <= sId; i++) {
                            $("#" + i).addClass("tempSelect");
                        }
                    }
                }
                flag = 1;
            }
        });
        //   }
        //将今天进行背景颜色设置
        $("#" + dateInfo.day).addClass("thisDay");
        $("#" + dateInfo.day + " .dayTime").css("background", "#FAD163");
    },


    //得到一周的各天
    dayOfWeek: function(date) {
        var entity = this;
        //支持的格式为YY-MM-DD
        if (entity.isNull(date)) {
            alert("日期不合法");
            return;
        }
        var weeks = ['周日', '周一', '周二', '周三', '周四', '周五', '周六'];
        var activityDate = {}; //声明活动日期对象
        var dateArray = date.split("-");
        var year = dateArray[0];
        var month = dateArray[1].replace("0", ""); //去掉日期中的0，比如将"05"->5
        var day = dateArray[2];
        var newDate = new Date(year, parseInt(month) - 1, day);
        var newDay = newDate.getDate(); //得到天
        var onWeekDay = newDate.getDay(); //得到该天是所在周的第几天
        var today = {};
        today.year = year;
        today.month = month;
        today.day = day;
        today.dayOfThisWeek = onWeekDay;
        activityDate.today = today;
        //计算该周的第一天
        newDate.setDate(newDay - onWeekDay);
        var firstDayOfThisWeek = {};
        firstDayOfThisWeek.year = newDate.getFullYear();
        firstDayOfThisWeek.month = parseInt(newDate.getMonth()) + 1;
        firstDayOfThisWeek.day = newDate.getDate();
        activityDate.firstDayOfThisWeek = firstDayOfThisWeek;
        //计算该周的最后一天    
        newDate = new Date(year, parseInt(month) - 1, day);
        newDate.setDate(newDay - onWeekDay + 6);
        var lastDayOfThisWeek = {};
        lastDayOfThisWeek.year = newDate.getFullYear();
        lastDayOfThisWeek.month = parseInt(newDate.getMonth()) + 1;
        lastDayOfThisWeek.day = newDate.getDate();
        activityDate.lastDayOfThisWeek = lastDayOfThisWeek;
        //计算该月有几天
        var thisMonthDays = new Date(year, parseInt(month) - 1, 0);
        activityDate.thisMonthDays = thisMonthDays.getDate();
        //计算该周的每一天
        var items = [];
        for (var i = 0; i < 7; i++) {
            var itemDate = new Date(year, parseInt(month) - 1, day);
            var itemDay = itemDate.getDate();
            var itemOnWeekDay = itemDate.getDay();
            itemDate.setDate(itemDay - itemOnWeekDay + i);
            var dayItem = {};
            dayItem.year = itemDate.getFullYear();
            dayItem.month = parseInt(itemDate.getMonth()) + 1;
            dayItem.day = itemDate.getDate();
            dayItem.weekDay = weeks[i];
            items[i] = dayItem;
        }
        activityDate.items = items;
        return activityDate;
    },


    //对月日期进行相关的计算
    dateOfMonth: function(date) {
        var entity = this;
        if (entity.isNull(date)) {
            alert("日期不合法");
            return;
        }
        date = date.replace(/-0/g, "-");
        var dateArray = date.split("-");
        var year = dateArray[0];
        var month = dateArray[1].replace("0", ""); //去掉日期中的0，比如将"05"->5
        var day = dateArray[2];
        //计算该月有几天
        var date = new Date(year, month, 0);
        var days = date.getDate(); //计算该月有几天
        date.setDate(1);
        var firstDayOfWeek = date.getDay(); //计算该月的一号是周几
        date.setDate(days);
        var lastDayOfWeek = date.getDay();
        var thisMonth = {};
        thisMonth.year = year;
        thisMonth.month = month;
        thisMonth.day = day;
        thisMonth.days = days;
        thisMonth.firstDayOfWeek = firstDayOfWeek;
        thisMonth.lastDayOfWeek = lastDayOfWeek;
        return thisMonth;
    },
    //改变当天的背景颜色
    setTodayBackground: function(index) {
        $("td.week-day").eq(index).addClass("thisDayHead");
        $("td.oneDay").eq(index).addClass("thisDay");
        $("tr.time-row").each(function() {
            var trInfo = $(this);
            var tdIndex;
            if (trInfo.is(".solid"))
                tdIndex = index + 1;
            else
                tdIndex = index;
            trInfo.find("td").eq(tdIndex).addClass("thisDay");
        });
    },


    //创建一个鼠标选择框
    selectItem: function() {
        var entity = this;
        var id = this.random();
        var div = [];
        div.push('<div id="' + id + '" class="selectItem temp">');
        div.push('<table><tr><td class="TL"></td><td class="TC"></td><td class="TR"></td></tr></table>');
        div.push('<div id="head" class="head"></div>');
        div.push('<div id="content" class="content"><table><tr><td></td></tr></table></div>');
        div.push('<table><tr><td class="BL"></td><td class="BC"></td><td class="BR"></td></tr></table>');
        div.push('</div>');
        $(div.join('')).appendTo($("#" + entity.renderTo));
        return id;
    },


    //创建一个弹出窗口
    popItem: function() {
        var entity = this;
        var id = this.random();
        var div = '<div id="' + id + '" class="popItem temp"><div class="close"></div>'
			 + '<table><tr><td class="TL"></td><td class="TC"></td><td class="TR"></td></tr>'
			 + '<tr><td class="ML"></td><td class="MC"></td><td class="MR"></td></tr>'
			 + '<tr><td class="BL"></td><td class="BC"></td><td class="BR"></td></tr>'
			 + '</table>'
			 + '</div>';
        $(div).appendTo($("#" + entity.renderTo));
        $('<div id="vPic" class="vPic temp"></div>').appendTo($("#" + entity.renderTo));
        //添加一个关闭事件
        $("#" + id + " .close").click(function() {
            entity.clear();
        });
        return id;
    },


    //月视图中的时间点选择
    selectItemOfMonth: function(flag, id) {
        var entity = this;
        if (flag == 'all') {
            //完整部分，类似于：《======》
            var item_all = '<table class="dayItem_all" id="' + id + '">'
	  				+ '<tr><td class="TL"></td><td class="TM"></td><td class="TR"></td></tr>'
	  				+ '<tr><td class="ML"></td><td class="MM"></td><td class="MR"></td></tr>'
	  				+ '<tr><td class="BL"></td><td class="BM"></td><td class="BR"></td></tr>'
	  				+ '</table>';
            return item_all;
        } else if (flag == 'head') {
            //只有开始，类似于：《==
            var item_head = '<table class="dayItem_head" id="' + id + '">'
	  				+ '<tr><td class="TL"></td><td class="TM"></td></tr>'
	  				+ '<tr><td class="ML"></td><td class="MM"></td></tr>'
	  				+ '<tr><td class="BL"></td><td class="BM"></td></tr>'
	  				+ '</table>';
            return item_head;
        } else if (flag == 'tail') {
            //只有结束，类似于：==》
            var item_tail = '<table class="dayItem_tail" id="' + id + '">'
	  				+ '<tr><td class="TM"></td><td class="TR"></td></tr>'
	  				+ '<tr><td class="MM"></td><td class="MR"></td></tr>'
	  				+ '<tr><td class="BM"></td><td class="BR"></td></tr>'
	  				+ '</table>';
            return item_tail;
        } else if (flag == 'body') {
            //只有中间，类似于： ===
            var item_body = '<table class="dayItem_body" id="' + id + '">'
	  				+ '<tr><td class="TM"></td></tr>'
	  				+ '<tr><td class="MM"></td></tr>'
	  				+ '<tr><td class="BM"></td></tr>'
	  				+ '</table>';
            return item_body;
        }

    },


    //设置弹出窗口的位置
    setPopItemPosition: function(position, popItemId) {
        var left = position.left - 50;
        var top = position.top - 230;
        if (top <= 0) {
            top = 0;
            $("#vPic").css("display", "none");
        }
        if ((left + 410) > $("body").width()) {
            left = $("body").width() - 410;
            $("#vPic").css("display", "none");
        }
        if (left <= 0) {
            left = 0;
            $("#vPic").css("display", "none");
        }
        $("#" + popItemId).css({ left: left + 'px', top: top + 'px' });
        $("#vPic").css({ left: (left + 100) + 'px', top: (top + 135) + 'px' });
    },


    //判断字符串是否为空
    isNull: function(data) {
        if (data == "" || data == null) {
            return true;
        } else {
            return false;
        }
    },



    //根据传入的ID，计算离该ID最近的整数时间(整点或半点)
    idToTime: function(id) {
        var entity = this;
        if (entity.isNull(id)) {
            alert('id错误');
            return;
        }
        var hour = parseInt(id) / 2;
        var second = parseInt(id) % 2;
        //将计算结果组织成time对象
        var time = {};
        time.timeId = id;
        time.hour = parseInt(hour);
        time.second = second;
        if (second == 0) {
            time.fullTime = parseInt(hour) + ":00";
        } else if (second == 1) {
            time.fullTime = parseInt(hour) + ":30";
        }
        return time;
    },


    //根据传入的时间字符串转换为可以用于绘制TD的相关属性
    strToTime: function(time) {
        var entity = this;
        if (time == "" || time == null) {
            alert('时间错误');
            return;
        }
        //格式一般为yyyy-MM-dd HH:mm:ss
        time = time.replace("-0", "-");
        var dateStr = time.replace(/[-:\s.]/g, ","); //将字符串在中的- ： .字符进行替换，便于分割
        var dateArray = dateStr.split(",");
        var date;
        if (dateArray.length == 3) {
            date = new Date(dateArray[0], parseInt(dateArray[1]) - 1, dateArray[2]);
        } else {
            date = new Date(dateArray[0], parseInt(dateArray[1]) - 1, dateArray[2], dateArray[3], dateArray[4]);
        }
        var year = date.getFullYear();
        var month = parseInt(date.getMonth()) + 1;
        var day = date.getDate();
        var hour = date.getHours();
        var minute = date.getMinutes();
        var dayOfThisWeek = date.getDay();
        var hour_id = parseInt(hour) * 2; //根据小时计算出的ID
        var minute_id = parseInt(parseInt(minute) / 30); //根据分钟计算出的ID
        var minute_px = parseInt(minute) % 30; //根据分钟计算出偏移的像素值如：43计算出的偏移值为13。
        var timeTd = {};
        timeTd.year = year;
        timeTd.month = month;
        timeTd.day = day;
        timeTd.hour = hour;
        timeTd.minute = minute == 0 ? '00' : minute;
        timeTd.minutes = minute;
        timeTd.trId = hour_id + minute_id;
        timeTd.px = minute_px;
        timeTd.dayOfThisWeek = dayOfThisWeek;
        return timeTd;
    },



    //根据传入的TD对象，获得该元素的绝对位置以及宽和高等属性
    getTdInfo: function(item) {
        var tdInfo = {};
        tdInfo.width = item.width();
        tdInfo.height = item.height();
        tdInfo.left = item.offset().left;
        tdInfo.top = item.offset().top;
        if (item.parent().is(".solid")) {
            tdInfo.index = item.index() - 1;
        } else {
            tdInfo.index = item.index();
        }
        return tdInfo;
    },



    //在周视图中，创建一个活动添加窗口
    activityAddItem: function(popItemId, time, selectItemId, UserID, Index) {
        var entity = this;
        var item = [];
        item.push('<div>');
        item.push('<table class="contentTable"><tr><td class="label">时间：</td><td class="time">' + time + '</td></tr><tr><td class="label">内容：</td><td><input type="text" class="text" /></td></tr></table>');

        item.push('<div class="operate">')
        if (UserID == "0" || Index == 3) {
            item.push('<div id="createButton" class="createButton">创建活动</div>');
            item.push('<a href="#" class="moreInfo">编辑活动详细信息</a>');
        }
        else {
            item.push('您没权限创建活动！');
        }
        item.push('</div>');
        item.push('</div>');
        $(item.join('')).appendTo($("#" + popItemId + " .MC"));
        //为创建活动按钮添加点击事件
        if (UserID == "0" || Index == 3) {
            $("#createButton").click(function() {
                var val = $("#" + popItemId + " .text").val();
                var param = "activity.title=" + val;
                var id = entity.saveActivity(param);
                if (id != "") {
                    $("#" + selectItemId).removeClass("temp");
                    $("#" + selectItemId).width($("#" + selectItemId).width() - 8);
                    //设置title
                    $("#" + selectItemId + " .content td").text(val);
                    //设置一个隐含域，用于存储活动的ID，便于查看，修改，删除等操作
                    var hiddenField = '<input type="hidden" class="activityId" value="' + id + '">';
                    $(hiddenField).appendTo($("#" + selectItemId));
                    //添加点击事件
                    $("#" + selectItemId).click(function() {
                        entity.clear();
                        var position = {};
                        position.left = $(this).offset().left;
                        position.top = $(this).offset().top + 15;
                        var id = $(this).find(".activityId").val();
                        entity.viewActivity(position, id);
                    });
                    entity.clear();
                }
            });

            //为编辑详细信息添加点击事件
            $("#" + popItemId + " .moreInfo").click(function() {
                window.parent.location = entity.moreInfoPath; //'&loadDate='+entity.loadDate;
            });
        }
    },



    //在月视图中，创建一个活动添加窗口
    activityAddItemOfMonth: function(popItemId, time, selectItemId, UserID, Index) {
        var entity = this;
        var item = [];
        item.push('<div>');
        item.push('<table class="contentTable">' +
			'<tr><td class="label">时间：</td><td class="time">' + time + '</td></tr>' +
			'<tr><td class="label">内容：</td><td><input type="text" class="text" /></td></tr>' +
			'</table>');
        item.push('<div class="operate">');
        if (UserID == "0" || Index == 3) {
            item.push('<div id="createButton" class="createButton">创建活动</div>');
            item.push('<a href="#" class="moreInfo">编辑活动详细信息</a>');
        } else
            item.push('您没权限创建活动！');
        item.push('</div>');
        item.push('</div>');
        $(item.join('')).appendTo($("#" + popItemId + " .MC"));
        //添加相应的事件
        if (UserID == "0" || Index == 3) {
            $("#createButton").click(function() {
                var val = $("#" + popItemId + " .text").val();
                var param = "activity.title=" + val;
                var timeParam = entity.timeParam;
                var activity = {};
                activity = entity.timeParamToActivity(activity, timeParam);
                var startId = parseInt(timeParam.startDate.day);
                var endId = parseInt(timeParam.endDate.day);
                var days = entity.days;
                var id = entity.saveActivity(param);
                activity.title = val;
                if (id != "") {
                    activity.id = id;
                    for (var i = startId; i <= endId; i++) {
                        $("#" + i).removeClass("tempSelect");
                    }
                    //从该起始日期开始重画界面------该方法不好，每次添加完，都要重画界面-----需要重构
                    var activityArray = new Array();
                    //收集当前页面的所有活动数据
                    for (var j = 1; j <= days; j++) {
                        activityArray[j] = entity.refreshItems(j);
                    }
                    //将刚添加的活动追加到数组中
                    var tempArray = activityArray[startId];
                    tempArray.push(activity);
                    //删除所有的活动数据
                    for (var j = 1; j <= days; j++) {
                        $("#" + j + " .itemTr").find("table").remove();
                    }
                    //将活动数据进行重新绘制
                    for (var j = 1; j <= days; j++) {
                        var array = activityArray[j];
                        entity.drawMonth(array);
                    }
                    entity.clear();
                }
            });
            //为编辑详细信息添加点击事件
            $("#" + popItemId + " .moreInfo").click(function() {
                window.parent.mainFrame.location = entity.moreInfoPath + '?loadDate=' + entity.loadDate;
            });
        }
    },




    //组织将要保存的数据
    toSaveParam: function() {
        var entity = this;
        var timeParam = entity.timeParam;
        var startDate = timeParam.startDate;
        var endDate = timeParam.endDate;
        var startTime = timeParam.startTime;
        var endTime = timeParam.endTime;
        var st = startDate.year + "-" + startDate.month + "-" + startDate.day + " " + startTime.hour + ":" + startTime.second * 30 + ":00";
        var et = endDate.year + "-" + endDate.month + "-" + endDate.day + " " + endTime.hour + ":" + endTime.second * 30 + ":00";
        entity.timeParam = {};
        return 'activity.startTime=' + st + "&activity.endTime=" + et;
    },



    //组织将要载入的数据
    toLoadParam: function() {
        var entity = this;
        var timeParam = entity.timeParam;
        var startDate = timeParam.startDate;
        var endDate = timeParam.endDate;
        //将时间清空
        entity.timeParam = {};
        return 'activity.startTime=' + startDate + "&activity.endTime=" + endDate;
    },



    //保存活动数据
    saveActivity: function(param) {
        var entity = this;
        var timeParam = entity.toSaveParam();
        var data = timeParam + "&" + param;
        var id;
        $.ajax({
            url: entity.savePath,
            data: data,
            type: 'post',
            async: false,
            dataType: 'json',
            success: function(data) {
                if (data.returnStr == "success") {
                    //如果保存成功则返回活动的ID
                    id = data.id;

                }
            },
            error: function() {
            }
        });
        return id;
    },



    //载入周活动数据
    loadWeekActivity: function(UserID, Index) {
        var entity = this
        var data = entity.toLoadParam();
        $.ajax({
            url: entity.loadByWeekPath + "&PerssionUserID=" + UserID,
            data: data,
            type: 'post',
            async: false,
            dataType: 'json',
            success: function(activityArray, index, UserID) {
                entity.drawWeekActivity(activityArray, index, UserID);
            },
            error: function() {
            }
        });
    },



    //载入月活动数据
    loadMonthActivity: function(UserID, Index) {
        var entity = this;
        var date = entity.dateOfMonth(entity.loadDate);
        for (var i = 1; i <= date.days; i++) {
            //设置载入时间点
            var timeParam = {};
            timeParam.startDate = date.year + "-" + date.month + "-" + i + " 00:00:00";
            timeParam.endDate = date.year + "-" + date.month + "-" + i + " 23:59:59";
            entity.timeParam = timeParam;
            //组织查询数据
            var data = entity.toLoadParam();
            $.ajax({
                url: entity.loadByMonthPath + "&PerssionUserID=" + UserID,
                data: data,
                type: 'post',
                async: false,
                dataType: 'json',
                success: function(activityArray) {
                    //得到所有符合条件的数据
                    entity.drawMonth(activityArray);
                },
                error: function() {
                }
            });
        }
    },



    //将月活动数据绘制到相应位置
    drawMonth: function(activityArray) {
        var entity = this;
        if (activityArray == null || activityArray.length == 0) {
            return;
        }
        //对活动时间进行格式化
        for (var i = 0; i < $(activityArray).length; i++) {
            activityArray.Table[i] = entity.activityDateFormat(activityArray.Table[i]);
        }
        //对将要绘制的活动数据进行排序，持续时间长的在前面
        var array = entity.activitySort(activityArray);
        for (var i = 0; i < $(array).length; i++) {
            entity.drawMonthActivity(array.Table[i]);
        }
    },



    //绘制月视图中的活动
    drawMonthActivity: function(activity) {
        var entity = this;
        if (activity == null) {
            alert("对象为空");
            return;
        }
        var sId = activity.startTime.day; //获得该活动的开始时间
        var eId = activity.endTime.day; //获得活动的结束时间
        var startDate = activity.startTime.year + "-" + activity.startTime.month + "-" + activity.startTime.day;
        var endDate = activity.endTime.year + "-" + activity.endTime.month + "-" + activity.endTime.day;
        if (eId == sId) {
            //活动只在一天中
            var id = entity.random();
            var item = entity.selectItemOfMonth('all', id);
            var index = entity.drawActivityItems(sId, item);
            //设置绝对对定位
            var td = $("#" + sId).find(".itemTr").eq(index).find(".itemTd");
            var tdInfo = entity.getTdInfo(td);
            $("#" + id).css({ 'left': tdInfo.left, 'top': tdInfo.top });
            $("#" + id).width(tdInfo.width);
            var hiddenField = [];
            hiddenField.push('<input type="hidden" class="gapMills" value=' + activity.gapMills + '>');
            hiddenField.push('<input type="hidden" class="activityId"  id ="myactivityId" value=' + activity.CalendarEventUserial + '>');
            hiddenField.push('<input type="hidden" class="startDate" value=' + startDate + '>');
            hiddenField.push('<input type="hidden" class="endDate" value=' + endDate + '>');
            $(hiddenField.join('')).appendTo($("#" + id));
            $("#" + id + " .MM").text("事件:" + activity.CalendarEventTitle + "\r\n" + startDate + "--" + endDate);

            $("#" + id).click(function(event) {
                entity.clear();
                var activityId = document.getElementById("myactivityId").value; // $("#myactivityId").val(); //$(this).find(".activityId").val();
                var position = {};
                position.left = $("#" + sId).offset().left;
                position.top = $("#" + sId).offset().top;
                entity.viewActivity(position, activityId);
                //阻止事件的冒泡
                event.stopPropagation();
            });
        } else {
            //活动跨越多天
            var index;
            var classId = entity.random();
            for (var j = sId; j <= eId; j++) {
                var id = entity.random();
                if (j == sId) {
                    var item = entity.selectItemOfMonth('head', id);
                    index = entity.drawActivityItems(sId, $(item));
                } else if (j == eId) {
                    var item = entity.selectItemOfMonth('tail', id);
                    var tr = $("#" + j).find(".itemTr").eq(index);
                    var td = tr.find('.itemTd');
                    td.append($(item));
                } else {
                    var item = entity.selectItemOfMonth('body', id);
                    var tr = $("#" + j).find(".itemTr").eq(index);
                    var td = tr.find('.itemTd');
                    td.append($(item));
                }
                //为该Item添加相应的信息
                var hiddenField = [];
                hiddenField.push('<input type="hidden" class="gapMills" value=' + activity.gapMills + '>');
                hiddenField.push('<input type="hidden" class="activityId" value=' + activity.id + '>');
                hiddenField.push('<input type="hidden" class="startDate" value=' + startDate + '>');
                hiddenField.push('<input type="hidden" class="endDate" value=' + endDate + '>');
                $(hiddenField.join('')).appendTo($("#" + id));
                var td = $("#" + j).find(".itemTr").eq(index).find(".itemTd");
                var tdInfo = entity.getTdInfo(td);
                $("#" + id).css({ 'left': tdInfo.left, 'top': tdInfo.top });
                if (j == sId) {
                    $("#" + id + " .MM").text("事件:" + activity.title);
                    $("#" + id).width(tdInfo.width + 1);
                } else if (j == eId) {
                    $("#" + id).width(tdInfo.width);
                } else {
                    $("#" + id).width(tdInfo.width + 1);
                }
                $("#" + id).click(function(event) {
                    entity.clear();
                    var activityId = $(this).find(".activityId").val();
                    var position = {};
                    position.left = $("#" + sId).offset().left;
                    position.top = $("#" + sId).offset().top;
                    entity.viewActivity(position, activityId);
                    //阻止事件的冒泡
                    event.stopPropagation();
                });
            }
        }
    },



    //绘制月视图中的活动Items
    drawActivityItems: function(id, item) {
        var entity = this;
        var i = 0;
        var index;
        $("#" + id + " .itemTr").each(function() {
            var size = $(this).find("table").size();
            if (size > 0) {
                return true;
            } else {
                $(this).find(".itemTd").append($(item));
                index = $(this).index();
                return false;
            }
        });
        return index;
    },



    //对activity数组进行排序，按照持续时间长短进行排序，持续时间长的在最前面
    activitySort: function(array) {
        var entity = this;
        if (array == null || array.length == 0) {
            return false;
        }
        var len = $(array).length;
        for (var i = 0; i < len - 1; i++) {
            for (var j = 0; j < len - i - 1; j++) {
                var activity = array[j];
                var activity1 = array[j + 1];
                if (activity1.gapMills > activity.gapMills) {
                    var activity = {};
                    activity = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = activity;
                }
            }
        }

        return array;
    },




    //获得需要刷新的活动数据
    refreshItems: function(id) {
        var entity = this;
        //得到从该天开始的活动数据
        var returnArray = new Array();
        var i = 0;
        $("#" + id + " .itemTr").each(function() {
            var items = $(this).find("table.dayItem_all,table.dayItem_head");
            items.each(function() {
                var thisItem = $(this);
                var array = new Array();
                if (thisItem.size() == 0) {
                    return false;
                }
                var activity = {};
                var startTime = {};
                var endTime = {};
                array["gapMills"] = parseInt(thisItem.find(".gapMills").val());
                array["item"] = thisItem.parent().html();
                startTime.day = entity.strToTime(thisItem.find(".startDate").val()).day;
                endTime.day = entity.strToTime(thisItem.find(".endDate").val()).day;
                array["startTime"] = thisItem.find(".startDate").val();
                array["endTime"] = thisItem.find(".endDate").val();
                array["title"] = thisItem.find(".MM").text();
                thisItem.remove();
                returnArray[i] = array;
                i++;
            });
        });
        return returnArray;
    },




    //对activity的时间格式进行预处理
    activityDateFormat: function(activity) {
        var entity = this;
        if (activity.startTime == "" || activity.endTime == "") {
            alert('格式化时间错误');
            return;
        }
        var startTimeInfo = entity.strToTime(activity.CalendarEventStartTime);
        var endTimeInfo = entity.strToTime(activity.CalendarEventEndTime);
        //计算开始时间和结束时间之间的毫秒数
        var startDate = new Date(startTimeInfo.year, startTimeInfo.month, startTimeInfo.day);
        var endDate = new Date(endTimeInfo.year, endTimeInfo.month, endTimeInfo.day);
        var mills = entity.millisecondsOfTwoMonth(startDate, endDate);
        //重新为activity的属性赋值
        activity.startTime = startTimeInfo;
        activity.endTime = endTimeInfo;
        activity.gapMills = mills;
        return activity;
    },




    //得到两个时间之间的毫秒数
    millisecondsOfTwoMonth: function(startTime, endTime) {
        var startMilli = startTime.getTime();
        var endMilli = endTime.getTime();
        return endMilli - startMilli;
    },



    //将timeParam转换为activity对象
    timeParamToActivity: function(activity, timeParam) {
        var entity = this;
        var startDate = timeParam.startDate;
        var endDate = timeParam.endDate;
        activity.startTime = startDate.year + "-" + startDate.month + "-" + startDate.day;
        activity.endTime = endDate.year + "-" + endDate.month + "-" + endDate.day;
        return activity;
    },





    //将周活动数据绘制到相应位置
    drawWeekActivity: function(activityArray, index, UserID) {
        var entity = this;
        if (activityArray == null)
            return;

        //        $(activityArray).each(function(i, n) {
        //            activityArray.Table[i].CalendarEventStartTime;
        //          
        //        });

        for (var i = 0; i < $(activityArray).length; i++) {
            var start = entity.strToTime(activityArray.Table[i].CalendarEventStartTime);
            var end = entity.strToTime(activityArray.Table[i].CalendarEventEndTime);
            //计算开始的tr和td
            var trId = start.trId; //得到该td所属TR的id
            var startTr = $("#" + trId);
            var startIndex;
            if (startTr.is(".solid")) {
                startIndex = start.dayOfThisWeek + 1;
            } else {
                startIndex = start.dayOfThisWeek;
            }
            var startTd = startTr.find("td").eq(startIndex);
            var startTdInfo = entity.getTdInfo(startTd);
            //计算结束的tr和td
            var endTrId = end.trId; //得到结束的TR的Id
            var endTr = $("#" + endTrId);
            var endIndex;
            if (startTr.is(".solid")) {
                endIndex = end.dayOfThisWeek + 1;
            } else {
                endIndex = end.dayOfThisWeek;
            }
            var endTd = endTr.find("td").eq(endIndex);
            var endTdInfo = entity.getTdInfo(endTd);
            //创建一个selectItem
            var selectItemId = entity.selectItem();
            //设置位置及大小
            var top = startTdInfo.top + start.px;
            $("#" + selectItemId).css({ left: startTdInfo.left + 'px', top: top + 'px' });
            $("#" + selectItemId).width(startTdInfo.width - 7);
            $("#" + selectItemId + " .content").height(endTdInfo.top - top + end.px - 5 - 15);
            $("#" + selectItemId + " .head").text(start.hour + ":" + start.minute + "-" + end.hour + ":" + end.minute);
            $("#" + selectItemId).removeClass("temp");
            $("#" + selectItemId + " .content td").text(activity.title);
            //设置一个隐含域，用于存储活动的ID，便于查看，修改，删除等操作
            var hiddenField = '<input type="hidden" class="activityId" id="myactivityId" value="' + activityArray.Table[i].CalendarEventUserial + '">';
            $(hiddenField).appendTo($("#" + selectItemId));
            //添加点击事件
            if (UserID == "0" || index == "3")  //
                $("#" + selectItemId).click(function() {
                    entity.clear();
                    var position = {};
                    position.left = $(this).offset().left;
                    position.top = $(this).offset().top + 15;
                    var id = document.getElementById("myactivityId").value; // $(this).find(".activityId").val();
                    entity.viewActivity(position, id, UserID, index);
                });
        }
    },



    //查看活动
    viewActivity: function(position, activityId, UserID, index) {
        var entity = this;
        $.ajax({
            url: entity.viewPath,
            data: 'activity.id=' + activityId,
            type: 'post',
            async: false,
            dataType: 'json',
            success: function(activity) {
                var popItemId = entity.popItem(); //创建一个弹出窗口，并返回其ID
                entity.setPopItemPosition(position, popItemId); //设置弹出窗口的位置
                entity.activityViewItem(popItemId, activity, UserID, index);
            },
            error: function() {
                alert("获取数据失败，请检查服务器状态");
            }
        });
    },




    //活动查看数据
    activityViewItem: function(popItemId, activity, UserID, index) {
        var entity = this;
        var start = entity.strToTime(activity.Table[0].CalendarEventStartTime);
        var end = entity.strToTime(activity.Table[0].CalendarEventEndTime);
        var startDate = start.year + "-" + start.month + "-" + start.day;
        var endDate = end.year + "-" + end.month + "-" + end.day;
        var startTime = start.hour + ":" + start.minute;
        var endTime = end.hour + ":" + end.minute;
        var time;
        if (startDate == endDate) {
            time = startDate + "," + startTime + "-" + endTime;
        } else {
            time = startDate + "," + startTime + "   " + endDate + "," + endTime;
        }
        //组织活动查看的显示数据，将其添加到弹出窗口上面
        var item = [];
        item.push('<div>');
        item.push('<table class="contentTable"><tr><td class="title">' + activity.Table[0].CalendarEventTitle + '</td></tr><tr><td class="time">' + time + '</td></tr></table>');
        item.push('<div class="line"><div>');
        item.push('<div class="viewOperate">');
        if (UserID == "0" || index == "2") {
            item.push('<a href="#" class="delete">删除</a>');
            item.push('<a href="#" class="moreInfo">编辑活动详细信息</a></div>');
        } else
            itemm.push('您没权限进行删除与修改');
        item.push('</div>');
        $(item.join('')).appendTo($("#" + popItemId + " .MC"));
        //添加相应的事件
        if (UserID == "0" || index == "2") {
            $(".delete").click(function() {
                //删除一条记录
                var param = "idList[0]=" + activity.Table[0].CalendarEventUserial;
                $.ajax({
                    url: entity.deletePath,
                    data: param,
                    type: 'post',
                    dataType: 'json',
                    success: function(result) {
                        //清除弹出窗口
                        entity.clear();
                        //清除selectItem
                        $("input[value=" + activity.Table[0].CalendarEventUserial + "]").parent().remove();
                    },
                    error: function() {
                        alert("删除数据失败，请检查服务器状态");
                    }
                });
            });
        }
        //为编辑详细信息添加点击事件
        $("#" + popItemId + " .moreInfo").click(function() {
            window.parent.location = entity.updatePath + '&activity.id=' + activity.Table[0].CalendarEventUserial;
        });
    },



    //清除系统中的临时DIV
    clear: function() {
        $(".temp").remove();
    },



    //获得当前的时间
    nowDate: function() {
        var date = new Date();
        var year = date.getFullYear();
        var month = parseInt(date.getMonth()) + 1;
        var day = date.getDate();
        return year + "-" + month + "-" + day;
    },



    //用于系统中随机ID的生成
    random: function() {
        //首先产生一个1000以内的随机数
        var r = Math.round(Math.random() * 1000);
        //获得当前的日期
        var date = new Date();
        var year = date.getYear();
        var month = date.getMonth();
        var day = date.getDate();
        var hours = date.getHours();
        var minutes = date.getMinutes();
        var seconds = date.getSeconds();
        //根据日期+随机数生成一个随机ID
        return 'id_' + year + month + day + minutes + seconds + r;
    }
}