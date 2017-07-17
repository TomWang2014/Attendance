var vm = new Vue({
    el: 'body',
    data: {
        studentListSearchDto: new service.studentList.dto.studentListSearchDto(),//搜索培训班dto
        dto: new service.studentList.dto.signUpDto(),    // 编辑培训班dto
        list: [],
        current: 0
    },
    methods: {
        // 获得学员列表
        getStudentList: function () {
            var modelstr = JSON.stringify(this.studentListSearchDto);
            service.studentList.io.getStudentList(modelstr).then(function (data) {
                vm.list = [];
                $(data.List).each(function (index, item) {
                    vm.list.push(item);
                });
                $("#pager").pager(data.PageIndex, data.PageCount, data.PageSize, data.RecordCount);
            });
        },
        // 排序
        sort: function (filds) {
            this.studentListSearchDto.Orderby = filds;
            this.studentListSearchDto.Desc = !this.studentListSearchDto.Desc;
            this.getStudentList();
        },
        EditSignUp: function (signinfo) {
            this.dto = JSON.parse(JSON.stringify(signinfo));
            $("#myModal").modal("show");
        },
        // 添加报名信息信息
        save: function () {
            var model = JSON.stringify(this.dto);

            // 表单是否通过前端验证
            if (Attendance.Validator.isValid("#signupForm")) {
                service.studentList.io.modifySignUpInfo(model).then(function (data) {
                    if (data.status) {
                        $("#myModal").modal("hide");
                        Attendance.Msg.showSuccess(data.msg);
                        vm.getStudentList();
                    } else {
                        Attendance.Msg.showSuccess(data.msg);
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
        }

    },
    ready: function () {

        this.getStudentList();
        modelHidden();
        $("#pager").pager().bind("changed.pager", function (e, newPage) {
            vm.studentListSearchDto.PageIndex = newPage;
            vm.getStudentList();
        });
    }
});


function modelHidden() {

    // 绑定窗口关闭事件
    $('#myModal').on('hide.bs.modal', function () {
        // 清空vm
        vm.dto = new service.studentList.dto.signUpDto();
        $("#signupForm").data('bootstrapValidator').resetForm();
    });
}
