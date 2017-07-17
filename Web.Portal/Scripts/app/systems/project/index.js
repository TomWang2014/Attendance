/*
 *------------------------------------------------------------------
 模块描述说明：
 用户管理功能模块对应的js文件
 作者：李天赐
 时间：2016-11-11 09:42:53
 ----------------------------------------------------------------- */
var vm = new Vue({
    el: 'body',
    data: {
        sysSearchDto: new service.project.dto.sysSearchDto(),
        content: '',
        types: [],
        project: [],
        settingId: 0,
        list: []
    },
    methods: {
        // 获得用户列表
        getList: function () {
            var modelstr = JSON.stringify(this.sysSearchDto);
            service.project.io.getList(modelstr).then(function (data) {
                vm.list = data.List;
                $("#pager").pager(data.PageIndex, data.PageCount, data.PageSize, data.RecordCount);
            });
        },

        // 获得为设置为推荐项目的列表
        getProjectItemList: function () {
            service.project.io.getProjectItemList().then(function (data) {
                vm.project = data;
            });
        },

        // 取消推荐
        cancel: function (id) {
            service.project.io.cancel(id).then(function (data) {
                Attendance.Msg.showSuccess();
                vm.getList();
            });
        },
        setting: function (id) {
            this.settingId = id;
            $("#myModal").modal("show");
        }
    },
    ready: function () {
        this.getList();
        this.getProjectItemList();

        // 分页
        $("#pager").pager().bind("changed.pager", function (e, newPage) {
            vm.sysSearchDto.PageIndex = newPage;
            vm.getList();
        });

        // 表单提交
        $("#signupForm").submit(function () {
            $(this).ajaxSubmit({
                url: '/system/SetProjetHost',
                type: "post",
                success: function () {
                    Attendance.Msg.showSuccess();
                    $("#myModal").modal("hide");
                    vm.getList();
                    vm.getProjectItemList();
                }
            });
            return false;
        });
    }
});





