"use strict";

/*时间格式过滤器*/
Vue.filter("timeVer", function (value) {

    value = value.split(" ")[1];

    return value;
});
var timeVer = Vue.filter('timeVer');
var App = new Vue({
    el: "body",
    data: function data() {
        return {
            timestart: null, //日期控件起始日期对象
            timeend: null, //日期控件结束日期对象
            minDate: "",
            maxDate: "",
            romId: 123, //培训机构id
            start: "2017", //机构时间表初始时间
            end: "2017", //机构时间表结束时间
            allCheck: 0, //顶层全选是否处于全选状态            
            CardNumber: "",
            MinRate: 0,
            MaxRate: 100,
            //培训班数据集合
            romList: [],
            //时间段数据集合
            timeList: []

        };
    },

    methods: {
        /**
         * [最终结果查询]
         * @method lookFor
         * @return {[type]} [description]
         */
        lookFor: function lookFor() {
            var verList = JSON.parse(JSON.stringify(this.timeList));
            var selectList = [];
            for (var k = 0; k < verList.length; k++) {
                if (verList[k].value == 1) {
                    selectList.push(verList[k]);
                } else {
                    for (var m = 0; m < verList[k].list.length; m++) {
                        if (verList[k].list[m].value == 1) {
                            selectList.push(verList[k]);
                            break;
                        }
                    }
                }
            }

            for (var i = 0, List = selectList; i < selectList.length; i++) {
                delete List[i].value;
                for (var j = 0, Lists = List[i].list; j < Lists.length; j++) {
                    if (Lists[j].value == 0) {
                        List[i].list.splice($.inArray(Lists[j].value, List[i].list), 1);
                    } else {
                        delete Lists[j].value;
                    }
                }
            }
            if (App.MinRate > App.MaxRate || App.MinRate < 0 || App.MinRate > 100 || App.MaxRate < 0 || App.MaxRate > 100 || App.MaxRate.toString() == "" || App.MinRate.toString()=="") {
                alert("考勤率输入有误");
                return false;
            }
            if (App.romId <= 0) {
                alert("请选择培训班");
                return false;
            }
            if (selectList.length <= 0) {
                alert("请选择时间段");
                return false;
            }

            var ajaxDate = {
                TrainId: this.romId,
                CardNumber: this.CardNumber,
                MinRate: this.MinRate,
                MaxRate: this.MaxRate,
                TimeList: selectList
            };
            $.ajax({
                url: '/Training/StudentStaticSearch',
                data: { model: JSON.stringify(ajaxDate) },
                type: "get",
                dataType: "json",
                cache: false,
                async: false,
                success: function (data) {
                    $("#tablelist").html("");
                    if (data.length > 0) {
                        $("#divtablelist").css("display", "block");
                        var tableStr = "";
                        tableStr += "<tr><td colspan='2'></td >";
                        for (var i in data) {
                            if (data[i].list.length > 0) {
                                for (var j in data[i].list) {
                                    tableStr += '<td style="font-weight:bold" colspan="' + data[i].list[j].list.length + '">' + App.ChangeDateFormat(data[i].list[j].TrainingDate) + '</td>';
                                }
                            }
                        }


                        tableStr += "  <td></td></tr><tr><td style='font-weight:bold'>学员姓名</td><td style='font-weight:bold'>学员卡号</td>  ";
                        for (var m in data) {
                            if (data[m].list.length > 0) {
                                for (var k in data[m].list) {
                                    if (data[m].list[k].list.length > 0) {
                                        for (var n in data[m].list[k].list) {
                                            tableStr += ' <td style="font-weight:bold">时间段' + (parseInt(n) + 1) + '</td>';
                                        }
                                    }

                                }
                            }
                        }
                        tableStr += "  <td style='font-weight:bold'>考勤率</td></tr>";
                        for (var x in data) {
                            tableStr += ' <td>' + data[x].StudentName + '</td><td>' + data[x].CardNumber + '</td>';
                            if (data[x].list.length > 0) {
                                for (var y in data[x].list) {
                                    if (data[x].list[y].list.length > 0) {
                                        for (var z in data[x].list[y].list) {
                                            tableStr += ' <td>' + data[x].list[y].list[z].IsCheckStr + '</td>';
                                        }
                                    }

                                }
                            }
                            tableStr += ' <td>' + data[x].Rate + '</td>';
                        }
                        tableStr += "</tr>";

                        $("#tablelist").html(tableStr);
                    } else {
                        $("#divtablelist").css("display", "none");
                    }
                }
            });
        },
        //查询并导出
        DownLoad: function () {
            var verList = JSON.parse(JSON.stringify(this.timeList));
            var selectList = [];
            for (var k = 0; k < verList.length; k++) {
                if (verList[k].value == 1) {
                    selectList.push(verList[k]);
                }
            }

            for (var i = 0, List = selectList; i < selectList.length; i++) {
                delete List[i].value;
                for (var j = 0, Lists = List[i].list; j < Lists.length; j++) {
                    delete Lists[j].value;
                }
            }
            if (App.MinRate > App.MaxRate || App.MinRate < 0 || App.MinRate > 100 || App.MaxRate < 0 || App.MaxRate > 100) {
                alert("考勤率输入有误");
                return false;
            }
            if (App.romId <= 0) {
                alert("请选择培训班");
                return false;
            }
            if (selectList.length <= 0) {
                alert("请选择时间段");
                return false;
            }

            var ajaxDate = {
                TrainId: this.romId,
                CardNumber: this.CardNumber,
                MinRate: this.MinRate,
                MaxRate: this.MaxRate,
                TimeList: selectList
            };
            window.location.href = "/Training/StudentStaticDown?model=" + JSON.stringify(ajaxDate);

        },


        /**
         * [list数据格式化]
         * @method verData
         * @param  {[array]} List [list列表]
         * @return {[type]}      [处理好的list列表]
         */
        verData: function verData(List) {
            for (var i = 0; i < List.length; i++) {
                List[i].value = 0;
                for (var _i = 0, Lists = List[_i].list; _i < Lists.length; _i++) {
                    Lists[_i].value = 0;
                }
            }
        },

        /**
         * [培训周期变化时触发数据更新函数]
         * @method timechange
         * @return {[type]}   [description]
         */
        timechange: function timechange() {
            /*
              培训班上课周期变更后触发
              进行ajax请求，更新timeList数据,
              每次更新timelist都必须先进行数据格式化操作
             */
            // this.verData(List);
            //清空timeList
            this.timeList = [];
            //根据时间选中的日期获取时间段
            $.ajax({
                url: '/Training/GetDateTimes',
                data: { trainId: this.romId, start: $(".timestart").val(), end: $(".timeend").val() },
                type: "get",
                cache: false,
                async: false,
                success: function (data) {
                    if (data.length > 0) {
                        for (var i in data) {
                            var trainDate = App.ChangeDateFormat(data[i].TrainingDate);
                            var times = data[i].Times;
                            var list = [];
                            for (var j in times) {
                                list.push({ start: App.ChangeDateTimeFormat(times[j].StartTime), end: App.ChangeDateTimeFormat(times[j].EndTime), value: 0 });
                            }
                            App.timeList.push({
                                date: trainDate,
                                list: list,
                                value: 0
                            });

                        }
                    }
                }
            });

        },

        /**
         * [最高等级全选函数]
         * @method topAllCheck
         * @return {[type]}    [description]
         */
        topAllCheck: function topAllCheck() {
            /**
            @description 当全选为false，遍历所有天数时间段，执行选中清空
             */
            if (!this.allCheck) {
                for (var i = 0, List = this.timeList; i < List.length; i++) {
                    List[i].value = 0;
                    this.secondCheck(List[i]);
                }
            } else {
                for (var _i2 = 0, _List = this.timeList; _i2 < _List.length; _i2++) {
                    _List[_i2].value = 1;
                    this.secondCheck(_List[_i2]);
                }
            }
        },

        /**
         * [具体天数时间段全选函数]
         * @method secondCheck
         * @param  {[obj]}    item [天时间段对象]
         * @return {[type]}         [description]
         */
        secondCheck: function secondCheck(item) {
            /**
             * @description 如果为全部清空状态，顶层全选则也应变为false状态，
             */
            if (item.value) {
                var flag = 0;
                /*选中状态时，遍历所有天数，假若所有天数都是全选状态，顶级也应为全选*/
                for (var i = 0, List = this.timeList; i < List.length; i++) {
                    if (!List[i].value) {
                        flag = 1;
                    }
                }
                if (flag === 0) {
                    this.allCheck = true;
                }
                /*遍历子级时间段对象，全部设置为全选状态*/
                for (var _i3 = 0, _List2 = item.list; _i3 < _List2.length; _i3++) {
                    _List2[_i3].value = 1;
                }
            } else {
                this.allCheck = false;
                for (var _i4 = 0, _List3 = item.list; _i4 < _List3.length; _i4++) {
                    _List3[_i4].value = 0;
                }
            }
        },

        /*
        timelIST单项时间选择按钮
         */
        /**
         * [单项时间点选择]
         * @method lastCheck
         * @param  {[obj]}  item [日期时间段]
         * @param  {[obj]}  tem  [时间点时间段]
         * @return {[type]}       [description]
         */
        lastCheck: function lastCheck(item, tem) {
            /*时间段为不选中状态，则上层天数时间段和顶级全选都应该为不选中状态，此时直接终止程序*/
            if (!tem.value) {
                item.value = 0;
                this.allCheck = false;
                return false;
            }
            var flag = 0;
            /*遍历所有同级元素，说过所有子元素中存在不被选中状态，终止循环，父级元素和顶级全选都为不被全选状态*/
            for (var i = 0, List = item.list; i < List.length; i++) {
                if (!List[i].value) {
                    flag = 1;
                    break;
                }
            }
            if (flag === 1) {
                item.value = 0;
                this.allCheck = false;
                return false;
            } else {
                item.value = 1;
            }
            /*已知当前父元素为全选状态，遍历父元素统计元素,均为全选则变为全选状态*/
            var flags = 0;
            for (var _i5 = 0, _List4 = this.timeList; _i5 < _List4.length; _i5++) {
                if (!_List4[_i5].value) {
                    flags = 1;
                    break;
                }
            }
            if (flags === 0) {
                this.allCheck = true;
            } else {
                this.allCheck = false;
            }
        },
        InitialTraining: function () {
            $.ajax({
                url: '/Training/GetTrainingAjax',
                data: {},
                type: "get",
                cache: false,
                success: function (data) {
                    if (data.length > 0) {
                        for (var i in data) {
                            App.romList.push({
                                title: data[i].Name,
                                id: data[i].Id
                            });
                        }

                    }
                }
            });
        },
        ChangeDateFormat: function ChangeDateFormat(cellval) {

            var date = new Date(parseInt(cellval.replace("/Date(", "").replace(")/", ""), 10));

            var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;

            var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();

            return date.getFullYear() + "-" + month + "-" + currentDate;

        },
        ChangeDateTimeFormat: function ChangeDateFormat(jsondate) {
            jsondate = jsondate.replace("/Date(", "").replace(")/", "");
            if (jsondate.indexOf("+") > 0) {
                jsondate = jsondate.substring(0, jsondate.indexOf("+"));
            }
            else if (jsondate.indexOf("-") > 0) {
                jsondate = jsondate.substring(0, jsondate.indexOf("-"));
            }
            var date = new Date(parseInt(jsondate, 10));
            var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
            var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
            var hours = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
            var minutes = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
            var second = date.getMilliseconds() / 1000 < 10 ? "0" + parseInt(date.getMilliseconds() / 1000) : parseInt(date.getMilliseconds() / 1000);
            return date.getFullYear() + "-" + month + "-" + currentDate + " " + hours + ":" + minutes + ":" + second;
        }
    },
    watch: {

        /**
         * [培训机构变化监听]
         * @method romId
         * @return {[type]} [description]
         * @description 监听培训机构的变化，每次变化产生新的培训限定日期，获取对应romid的约定时间段
          */
        romId: function romId() {
            /*ajax获取数据，succe回调中更新数据*/
            //清空查询日期
            $(".timestart,.timeend").val("");
            if (this.romId > 0) {
                $(".timestart").removeAttr("disabled");
                $(".timeend").removeAttr("disabled");

                //获取培训班的查询日期时间段
                $.ajax({
                    url: '/Training/GetMinDate',
                    data: { trainId: this.romId },
                    type: "get",
                    cache: false,
                    async: false,
                    success: function (data) {
                        App.minDate = data;
                    }
                });
                $.ajax({
                    url: '/Training/GetMaxDate',
                    data: { trainId: this.romId },
                    type: "get",
                    cache: false,
                    async: false,
                    success: function (data) {
                        App.maxDate = data;
                    }
                });
                //设置日历控件范围
                this.timestart.datetimepicker('setStartDate', App.minDate);
                this.timestart.datetimepicker('setEndDate', App.maxDate);
                this.timeend.datetimepicker('setStartDate', App.minDate);
                this.timeend.datetimepicker('setEndDate', App.maxDate);

            } else {
                $(".timestart").attr("disabled", "disabled");
                $(".timeend").attr("disabled", "disabled");
            }


        }
    },
    ready: function mounted() {
        //初始化培训班
        this.InitialTraining();

        this.verData(this.timeList);
        var self = this;
        /*时间变化后ajax请求新的时间段函数*/
        this.timestart = $(".timestart").datetimepicker({
            language: 'zh-CN',
            format: "yyyy-mm-dd",
            autoclose: 1,
            minView: 2,
        }).on('changeDate', function (ev) {
            //回调
            $(".timestart").datetimepicker("setStartDate", App.minDate)
            $(".timestart").datetimepicker("setEndDate", App.maxDate)
            var start = $(".timestart").val();
            var end = $(".timeend").val();
            if (start != "" && end != "") {
                if (start > end) {
                    alert("开始日期不能大于结束日期！");
                    $(".timestart").val("");
                    return false;
                }
                self.timechange();
            }


        });
        this.timeend = $(".timeend").datetimepicker({
            language: 'zh-CN',
            format: "yyyy-mm-dd",
            autoclose: 1,
            minView: 2,
        }).on('changeDate', function (ev) {
            //回调
            $(".timeend").datetimepicker("setStartDate", App.minDate);
            $(".timeend").datetimepicker("setEndDate", App.maxDate);
            if (start != "" && end != "") {
                var start = $(".timestart").val();
                var end = $(".timeend").val();
                if (start > end) {
                    alert("结束日期不能小于开始日期！");
                    $(".timeend").val("");
                    return false;
                }



                self.timechange();
            }

        });

    }
});