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
        // 编辑用户dto
        dto: new service.sysUser.dto.sysUserDto(),
        sysSearchDto: new service.sysUser.dto.sysSearchDto(),
        list: [],
        current: 0,
        roleList: [],
        userSelected: []
    },
    methods: {

        // 编辑用户信息
        save: function () {

            console.log(this.dto);

            var model = JSON.stringify(this.dto);
            // 表单是否通过前端验证
            if (Attendance.Validator.isValid("#signupForm")) {
                service.sysUser.io.modifyUserInfo(model).then(function() {
                    $("#myModal").modal("hide");
                    Attendance.Msg.showSuccess("编辑用户成功");
                    vm.getList();
                });
            }
        },

        // 编辑用户
        edituser: function (el) {
            console.log(this.dto);
            this.dto = JSON.parse(JSON.stringify(el));
            $("#myModal").modal("show");
        },

        // 获得用户列表
        getList: function () {

            var modelstr = JSON.stringify(this.sysSearchDto);
            service.sysUser.io.getList(modelstr).then(function (data) {

                vm.list = [];
                vm.userSelected = [];
                $(data.List).each(function (index, item) {
                    item.seleced = false;
                    vm.list.push(item);
                });
                $("#pager").pager(data.PageIndex, data.PageCount, data.PageSize, data.RecordCount);
            });
        },

        // 获得角色列表
        getRoleList: function () {
            service.sysRole.io.getRoleItemList().then(function (data) {
                vm.roleList = data;
            });
        },

        // 删除用户
        deleteuser: function (id) {
            service.sysUser.io.deleteuser(id).then(function () {
                Attendance.Msg.showSuccess();
                vm.getList();
            });
        },

        // 批量删除
        deleteBatch: function () {
            var ids = JSON.stringify(this.userSelected);
            service.sysUser.io.deleteBatch(ids).then(function () {
                Attendance.Msg.showSuccess();
                vm.getList();
            });
        },

        // 排序
        sort: function (filds) {
            this.sysSearchDto.Orderby = filds;
            this.sysSearchDto.Desc = !this.sysSearchDto.Desc;
            this.getList();
        },

        // checkbox复选
        change: function (that, id) {
            if (that) {
                this.userSelected.push(id);
            } else {
                this.userSelected.splice(this.userSelected.indexOf(id), 1);
            }
        },
    },
    computed: {
        checkAllBool: {
            get: function () {
                return this.list.reduce(function (prev, curr) {
                    return prev && curr.seleced;
                }, true);
            },
            set: function (newValue) {
                $(this.list).each(function (index, item) {
                    if (newValue) {
                        if (vm.userSelected.indexOf(item.Id) < 0) {
                            vm.userSelected.push(item.Id);
                        }
                    } else {
                        vm.userSelected = [];
                    }
                    item.seleced = newValue;
                });
            }
        }
    },
    ready: function () {

        this.getList();
        this.getRoleList();
        modelHidden();

        $("#pager").pager().bind("changed.pager", function (e, newPage) {
            vm.sysSearchDto.PageIndex = newPage;
            vm.getList();
        });
    }
});



function modelHidden() {

    // 绑定窗口关闭事件
    $('#myModal').on('hide.bs.modal', function () {
        // 清空vm
        vm.dto = new service.sysUser.dto.sysUserDto();
        $("#signupForm").data('bootstrapValidator').resetForm();
    });
}