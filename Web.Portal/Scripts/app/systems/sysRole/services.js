/*
 *------------------------------------------------------------------
 模块描述说明：
 用户管理功能模块对应的services文件
 作者：李天赐
 时间：2016-11-11 09:42:53
 ----------------------------------------------------------------- */
var service = service || {};
var baserul = "";
(function () {
    var adminService = function (serviceName) {
        this.name = serviceName || "UnnamedService";
        this.pageSzie = 10;
    };

    adminService.prototype.dto = {
        // dto 
        sysRoleDto: function () {
            this.Id = 0;
            this.Name = '';
            this.SysFuncIds = [];
        }
    };

    adminService.prototype.io = {

        // 修改用户信息
        modifyRoleInfo: function (model) {
            return $.ajax({
                url: '/system/modifyRoleInfo',
                data: { "model": model },
                type: "post",
                cache: false
            });
        },

        // 获得权限集合
        getList: function (roleName) {
            return $.ajax({
                url: '/System/GetRoleList',
                data: { "roleName": roleName },
                type: "get",
                cache: false
            });
        },

        // 获得权限键值对集合
        getRoleItemList: function () {
            return $.ajax({
                url: '/System/GetRoleItemList',
                type: "get",
                cache: false
            });
        },


        // 获得用户集合
        deleteRole: function (id) {
            return $.ajax({
                url: '/System/DeleteRole',
                data: { "id": id },
                type: "post",
                cache: false
            });
        },

    };

    service.sysRole = service.sysRole || new adminService("admin-service");
})();