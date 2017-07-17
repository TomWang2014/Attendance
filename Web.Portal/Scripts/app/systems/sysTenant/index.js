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
        dto: new service.sysTenant.dto.sysTenantDto(),
        sysSearchDto: new service.sysTenant.dto.sysSearchDto(),
        logoMsg: "机构logo是显示该机构的标志之一，请选择图片后点击编辑并上传即可。",
        setLogoType: 0,
        list: [],
        roleList: []
    },
    methods: {

        // 编辑用户信息
        save: function () {

            console.log(this.dto);

            var model = JSON.stringify(this.dto);

            if (Attendance.Validator.isValid("#signupForm")) {
                service.sysTenant.io.modifyInfo(model).then(function () {
                    $("#avatar-modal").modal("hide");
                    Attendance.Msg.showSuccess("编辑机构信息成功");
                    vm.getList();
                });
            }
        },

        // 编辑用户
        edituser: function (el) {
            var dto = JSON.parse(JSON.stringify(el));
            dto.FrontImage = dto.FrontImage || this.dto.FrontImage;
            dto.Logo = dto.Logo || this.dto.Logo;
            dto.Account = dto.NetSysUserAccount;
            dto.Name = dto.NetSysUserName;

            this.dto = dto;
            $("#avatar-modal").modal("show");
        },

        // 获得租户列表
        getList: function () {
            var modelstr = JSON.stringify(this.sysSearchDto);
            service.sysTenant.io.getList(modelstr).then(function (data) {
                vm.list = data.List;
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
        deleteInfo: function (id) {
            service.sysTenant.io.deleteInfo(id).then(function (data) {
                Attendance.Msg.showSuccess();
                vm.getList();
            });
        },
        setLogo: function (type) {
            this.setLogoType = type;
            if (type == 1) {
                // 设置logo
                this.logoMsg = "机构logo是显示该机构的标志之一，请选择图片后点击编辑并上传即可。";
            } else {
                // 设置图标
                this.logoMsg = "请选择用于设置机构图标的图片，调整大小后点击编辑并上传即可。";
            }
        }

    }, ready: function () {

        this.getList();

        $("#pager").pager().bind("changed.pager", function (e, newPage) {
            vm.sysSearchDto.PageIndex = newPage;
            vm.getList();
        });
    }
});


$("#btnSelectFile").click(function () {
    $("#avatarInput").click();
});

 
$(document).ready(function () {
    $('#tab1,#tab2').on('click', function (e) {
        if (vm.dto.Id == 0) {
            Attendance.Msg.showInfo("请先保存基本信息后，再设置机构图标");
            return false;
        }
    });


    // 绑定窗口关闭事件
    $('#avatar-modal').on('hide.bs.modal', function () {

        // 清空vm
        vm.dto = new service.sysTenant.dto.sysTenantDto();

        // 重置选项卡
        Attendance.Toolkit.resetting();

        $("#signupForm").data('bootstrapValidator').resetForm();

    });

});