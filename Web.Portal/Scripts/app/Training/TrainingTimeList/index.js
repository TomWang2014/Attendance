var vm = new Vue({
    el: 'body',
    data: {
        trainListSearchDto: new service.trainList.dto.trainListSearchDto(),//搜索培训班dto
        list: [],
        current: 0
    },
    methods: {
        // 获得考勤日期列表
        getList: function () {
            var modelstr = JSON.stringify(this.trainListSearchDto);
            service.trainList.io.getList(modelstr).then(function (data) {
                vm.list = [];
                $(data.List).each(function (index, item) {
                    vm.list.push(item);
                });
                $("#pager").pager(data.PageIndex, data.PageCount, data.PageSize, data.RecordCount);
            });
        },
        // 排序
        sort: function (filds) {
            this.trainListSearchDto.Orderby = filds;
            this.trainListSearchDto.Desc = !this.trainListSearchDto.Desc;
            this.getList();
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
        // 添加考勤日期信息
        save: function () {
            window.location.href = "/Training/AddTrainingDate?id=" + TrainId;
        },
        //编辑考勤日期信息
        editDate: function (date) {
            window.location.href = "/Training/EditTrainingDate?id=" + TrainId + "&date=" + this.ChangeDateFormat(date);
        },
        detailDate: function (date) {
            window.location.href = "/Training/TrainDateDetail?id=" + TrainId + "&date=" + this.ChangeDateFormat(date);
        },
        deleteDate: function (date) {
            var r = window.confirm("确定要删除吗？");
            if (r == true) {
                service.trainList.io.DeleteTrainDate(TrainId, this.ChangeDateFormat(date)).then(function (data) {
                    if (data) {
                        alert("删除成功！");
                        vm.getList();
                    } else {
                        alert("删除失败，请重试！");
                    }
                })
            }
        }

    },
    ready: function () {
        this.getList();

        $("#pager").pager().bind("changed.pager", function (e, newPage) {
            vm.trainListSearchDto.PageIndex = newPage;
            vm.getList();
        });
    }
});