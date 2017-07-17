var vm = new Vue({
    el: 'body',
    data: {
        dto: new service.trainList.dto.trainListDto(),    // 编辑培训班dto
        trainListSearchDto: new service.trainList.dto.trainListSearchDto(),//搜索培训班dto
        list: [],
        current: 0
    },
    methods: {
        // 获得培训班列表
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
        // 添加培训班信息
        save: function () {
            var model = JSON.stringify(this.dto);

            // 表单是否通过前端验证
            if (Attendance.Validator.isValid("#signupForm")) {
                service.trainList.io.modifyTrainInfo(model).then(function (data) {
                    if (data) {
                        $("#myModal").modal("hide");
                        Attendance.Msg.showSuccess("编辑培训班成功！");
                        vm.getList();
                    } else {
                        Attendance.Msg.showSuccess("编辑培训班失败，请重试！");
                    }

                });
            }
        },
        //编辑培训班信息        
        editTrain: function (el) {
            this.dto = JSON.parse(JSON.stringify(el));
            this.dto.StartTime = this.ChangeDateFormat(this.dto.StartTime);
            this.dto.EndTime = this.ChangeDateFormat(this.dto.EndTime);
            this.dto.SignUpStart = this.ChangeDateFormat(this.dto.SignUpStart);
            this.dto.SignUpEnd = this.ChangeDateFormat(this.dto.SignUpEnd);
            this.dto.CreateTime = this.ChangeDateFormat(this.dto.CreateTime);
            $("#myModal").modal("show");
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
        toDetail: function (id) {
            window.location.href = "/Training/TrainingDetail?id=" + id;
        },
        ToStudentList: function (id) {
            window.location.href = "/Training/StudentList?id=" + id;
        },
        ToTimeList: function (id) {
            window.location.href = "/Training/TrainingTimeList?id=" + id;
        },
        ToGenerate: function (id) {
            window.location.href = "/Training/TrainingGenerate?id=" + id;
        },
        ToAbsentStudentList: function (id) {
            window.location.href = "/Training/TrainingAbsentList?id=" + id;
        }
    },
    ready: function () {

        this.getList();
        modelHidden();

        $("#pager").pager().bind("changed.pager", function (e, newPage) {
            vm.trainListSearchDto.PageIndex = newPage;
            vm.getList();
        });
    }
});



function modelHidden() {

    // 绑定窗口关闭事件
    $('#myModal').on('hide.bs.modal', function () {
        // 清空vm
        vm.dto = new service.trainList.dto.trainListDto();
        $("#signupForm").data('bootstrapValidator').resetForm();
    });
}