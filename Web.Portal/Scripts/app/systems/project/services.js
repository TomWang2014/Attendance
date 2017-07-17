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
        sysSearchDto: function () {
            this.PageIndex = 1;
            this.PageSize = 10;
            this.Name = '';
        }
    };

    adminService.prototype.io = {

        // 获得集合
        getList: function (model) {
            return $.ajax({
                url: '/System/GetAllProject',
                data: { model: model },
                type: "get",
                cache: false
            });
        },
        cancel: function (id) {
            return $.ajax({
                url: '/System/cancel',
                data: { id: id },
                type: "get",
                cache: false
            });
        },
        getProjectItemList: function () {
            return $.ajax({
                url: '/org/GetProjectItem',
                type: "get",
                cache: false
            });
        }

    };

    service.project = service.project || new adminService("admin-service");
})();