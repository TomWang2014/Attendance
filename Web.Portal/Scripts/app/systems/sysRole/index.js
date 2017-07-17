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
        dto: new service.sysRole.dto.sysRoleDto(),
        Name: '',
        list: []
    },
    methods: {

        // 编辑用户信息
        save: function () {

            vm.dto.SysFuncIds = [];

            // 取到选择的节点
            $.each($("#roleTree").jstree("get_selected"), function (i, n) {
                vm.dto.SysFuncIds.push(n);
                console.log(n);
            });

            // 取到选中节点的父节点
            $("#roleTree").find(".jstree-checked, .jstree-undetermined ").each(function () {
                var item = $(this).parent().parent();
                vm.dto.SysFuncIds.push(item.attr("id"));
            });

            // 表单是否通过前端验证
            if (Attendance.Validator.isValid("#signupForm")) {

                var model = JSON.stringify(vm.dto);
                service.sysRole.io.modifyRoleInfo(model).then(function () {
                    $("#myModal").modal("hide");
                    Attendance.Msg.showSuccess("编辑角色信息成功");
                    vm.getList();
                });
            }
        },

        // 编辑用户
        editRole: function (el) {
            var dto = JSON.parse(JSON.stringify(el));

            this.dto = dto;

            var treeObj = $("#roleTree").jstree();

            $(this.dto.SysFuncIds).each(function (i, item) {
                treeObj.select_node(item);
            });

            $("#myModal").modal("show");
        },

        // 获得用户列表
        getList: function () {
            service.sysRole.io.getList(this.Name).then(function (data) {
                vm.list = data;
            });
        },

        // 删除用户
        deleteRole: function (id) {
            service.sysRole.io.deleteRole(id).then(function () {
                Attendance.Msg.showSuccess();
                vm.getList();
            });
        }

    }, ready: function () {

        this.getList();
        treeLoad();
        modelHidden();
    }
});


//jsTree加载
function treeLoad() {
    $('#roleTree').jstree({
        'plugins': ["checkbox"],
        'core': {
            "check_callback": true,
            'strings': {
                'Loading ...': '加载中...'
            },
            'multiple': true,
            "themes": {
                "responsive": false,
                'icons': false
            },
            'data': {
                'cache': true,
                'url': '/System/GetAllNotTenantFuncsTree'
            }
        }
    });
}

function modelHidden() {
    // 绑定窗口关闭事件
    $('#myModal').on('hide.bs.modal', function (e) {

        // 清空vm
        vm.dto = new service.sysRole.dto.sysRoleDto();

        //重置jstree的状态
        var treeObj = $('#roleTree').jstree(false);
        treeObj.deselect_all(false);
        $("#signupForm").data('bootstrapValidator').resetForm();

    });
}

