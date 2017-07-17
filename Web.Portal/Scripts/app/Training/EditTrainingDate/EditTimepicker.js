"use strict";

var app = new Vue({
    el: "body",
    data: function data() {
        return {
            timeList: [],
            TrainingDate: TrainDate
        };
    },

    methods: {
        addList: function addList() {
            this.addTime();
            var index = this.timeList.length - 1;
            var Class;
            var i;
            if (index >= 0) {
                Class = this.timeList[index].class;
                i = Class.slice(10) - 0 + 1;
            } else {
                Class = "timepicker0";
                i = 0;
            }

            console.log(i);
            var ClassName = "timepicker" + i;
            this.timeList.push({
                start: "",
                end: "",
                class: ClassName

            });
            setTimeout(function () {
                $(".timepicker" + i).datetimepicker({
                    format: "yyyy-mm-dd hh:ii",
                    showMeridian: true,
                    autoclose: true,
                    todayBtn: true,
                    language: 'zh-CN'
                }).on("changeDate", function () {
                    if (app.TrainingDate == "") {
                        alert("请先选择查询日期！");
                    } else {
                        if (app.ToDate($(this).val()) > app.ToDate(app.TrainingDate) || app.ToDate($(this).val()) < app.ToDate(app.TrainingDate)) {
                            alert("时间段必须和查询日期为同一天！");
                        }
                    }

                });
            }, 100);
        },
        addTime: function addTime() {
            var self = this;
            $(".datastart").each(function (index, ele) {
                self.timeList[index].start = $(this).val();
            });
            $(".dataend").each(function (index, ele) {
                self.timeList[index].end = $(this).val();
            });
        },
        minTime: function minTime(index) {
            this.addTime();
            this.timeList.splice(index, 1);
        },
        ToDate: function (dateStr, separator) {
            if (!separator) {
                separator = "-";
            };
            var dateArr = dateStr.split(separator);
            var year = parseInt(dateArr[0]);
            var month;
            if (dateArr[1].indexOf("0") == 0) {
                month = parseInt(dateArr[1].substring(1));
            } else {
                month = parseInt(dateArr[1]);
            }
            var day = parseInt(dateArr[2]);
            var date = new Date(year, month - 1, day);
            return date;
        },
        save: function save() {
            // 验证时间格式：时间段是否为空、时间段是否和日期是同一天、结束时间是否大于开始时间          
            if (this.timeList.length <= 0) {
                alert("请添加时间段！");
                return false;
            }
            for (var i in this.timeList) {
                if (this.timeList[i].start == "" || this.timeList[i].end == "") {
                    alert("查询时间段不能为空");
                    return false;
                }
                if (this.timeList[i].start > this.timeList[i].end) {
                    alert("开始时间不能大于结束时间");
                    return false;
                }
                if (this.ToDate(this.timeList[i].start) > this.ToDate(this.TrainingDate) || this.ToDate(this.timeList[i].start) < this.ToDate(this.TrainingDate) || this.ToDate(this.timeList[i].end) > this.ToDate(this.TrainingDate) || this.ToDate(this.timeList[i].end) < this.ToDate(this.TrainingDate)) {
                    alert("时间段必须和查询日期为同一天！");
                    return false;
                }
            }
            //提交
            if (confirm("您确认修改查询时间段么？")) {
                $.ajax({
                    url: '/Training/EditTrainingDate',
                    data: { date: this.TrainingDate, list: this.timeList, trainId: TrainId },
                    type: "post",
                    cache: false,
                    success: function (data) {
                        if (data) {
                            alert("修改成功！");
                            window.location.href = "/Training/TrainingTimeList?id=" + TrainId;
                        } else {
                            alert("修改失败，请重试！");
                        }
                    }
                });
            }



        },
        ChangeDateFormat: function ChangeDateFormat(jsondate) {
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
        },
        InitialTimeList: function () {
            $.ajax({
                url: '/Training/GetTrainTimes',
                data: { date: this.TrainingDate, trainId: TrainId },
                type: "get",
                cache: false,
                success: function (data) {
                    if (data.length > 0) {
                        for (var i = 0; i < data.length; i++) {
                            app.timeList.push({
                                start: app.ChangeDateFormat(data[i].StartTime),
                                end: app.ChangeDateFormat(data[i].EndTime),
                                class: "timepicker" + i
                            });
                        }
                    }
                }
            });
        },
        back: function () {
            window.location.href = "/Training/TrainingTimeList?id=" + TrainId;
        }
    },
    ready: function ready() {
        this.InitialTimeList();
    }
});