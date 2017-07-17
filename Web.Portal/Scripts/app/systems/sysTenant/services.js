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
        sysTenantDto: function () {
            this.Id = 0;
            this.Name = '';
            this.Account = '';
            this.NetSysUserNetSysRoleId = 0;
            this.Descriptions = '';
            this.FrontImage = '/Content/img/course.jpg';
            this.Logo = '/Content/img/course.jpg';
            this.AesKey = '';
            this.Gateway = '';
            this.TokenUrl = '';
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

        // 修改记录
        modifyInfo: function (model) {
            return $.ajax({
                //url: '/system/modifyTenantInfo',
                url: Attendance.Path.url('system', 'modifyTenantInfo'),
                data: { "model": model },
                type: "post",
                cache: false
            });
        },

        // 获得集合
        getList: function (model) {
            return $.ajax({
                url: Attendance.Path.url('system', 'getTenantList'),
                data: { "model": model },
                type: "get",
                cache: false
            });
        },

        // 删除记录
        deleteInfo: function (id) {
            return $.ajax({
                url: Attendance.Path.url('system', 'deleteTenantInfo'),
                data: { "id": id },
                type: "post",
                cache: false
            });
        },

    };

    service.sysTenant = service.sysTenant || new adminService("admin-service");
})();