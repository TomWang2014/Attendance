/*
 *------------------------------------------------------------------
 模块描述说明：
 用户管理功能模块对应的services文件
 作者：李天赐
 时间：2016-11-11 09:42:53
 ----------------------------------------------------------------- */
var service = service || {};

(function () {
    var adminService = function (serviceName) {
        this.name = serviceName || "UnnamedService";
        this.pageSzie = 10;
    };

    adminService.prototype.dto = {
        // dto 
        sysUserDto: function () {
            this.Id = 0;
            this.Name = '';
            this.Account = '';
            this.SysRoleId = 0;
        },

        sysSearchDto: function () {
            this.PageIndex = 1;
            this.PageSize = 10;
            this.Name = '';
            this.Number = '';
            this.Account = '';
            this.Orderby = "CreationTime";
            this.Desc = true;
        }
    };

    adminService.prototype.io = {

        // 修改用户信息
        modifyUserInfo: function (model) {
            return $.ajax({
                url: '/system/modifyUserInfo',
                data: { "model": model },
                type: "post",
                cache: false
            });
        },

        // 获得用户集合
        getList: function (model) {
            return $.ajax({
                url: '/System/GetALlUserList',
                data: { model: model },
                type: "get",
                cache: false
            });
        },

        // 删除用户
        deleteuser: function (id) {
            return $.ajax({
                url: '/System/DeleteUser',
                data: { "id": id },
                type: "post",
                cache: false
            });
        },

        // 删除用户
        deleteBatch: function (ids) {
            return $.ajax({
                url: '/System/DeleteBatch',
                data: { "ids": ids },
                type: "post",
                cache: false
            });
        },

    };

    service.sysUser = service.sysUser || new adminService("admin-service");
})();