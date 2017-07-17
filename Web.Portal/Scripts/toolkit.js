/*!
 * 网络学院工具类文档注释
 *Include
 *jquery 、layer
 * waterfall - v1.0.0 (2013-03-15T14:55:51+0800)
 * http://jraiser.org/ | Released under MIT license
 */

var Attendance = window.Attendance || {};

/*响应操作消息相关功能
@info：操作的详细描述
@title：标题描述
 */
Attendance.Msg = new function () {

    var self = this;
    toastr.options = {
        "closeButton": true,
        "debug": true,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "showDuration": "400",
        "hideDuration": "1000",
        "timeOut": "3000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    //操作成功的提示
    self.showSuccess = function (info, title) {
        toastr.success(info || "您的操作已经成功完成", title || "操作成功！");
    };

    //操作成功的提示
    self.showError = function (info, title) {
        toastr.error(info || "抱歉，您的操作失败！", title || "操作失败！");
    };

    //操作成功的提示
    self.showInfo = function (info, title) {
        toastr.info(info || "显示信息详细信息！", title || "信息标题！");
    };

    //操作成功的提示
    self.showWarning = function (info, title) {
        toastr.warning(info || "显示信息详细信息！", title || "信息标题！");
    };
};

Attendance.Path = new function () {

    var self = this;

    self.url = function (controllerName, actionName, areaName) {

        actionName = actionName || "";
        controllerName = controllerName || "";
        areaName = areaName || "";

        var url = "";
        $(allPaths).each(function (index, item) {
            if (item.Area == areaName.toLowerCase() && item.Controller == controllerName.toLowerCase() && item.Action == actionName.toLowerCase()) {
                url = item.Url;
            }
        });

        if (url == "") {
            Attendance.Msg.showError("没有找到路径:area:" + areaName + ",controller:" + controllerName, "action:" + actionName);
        }
        return url;
    }
};
/*
  其他工具辅助类
 */
Attendance.Toolkit = new function () {

    var self = this;

    self.resetting = function () {

        // 重置boostrop的tabs选项卡
        $('.nav-tabs li').each(function (i, li) {
            if (i === 0) {
                $(li).addClass("active");
            } else {
                $(li).removeClass("active");
            }
        });

        $('.tab-pane').each(function (i, tab) {
            if (i === 0) {
                $(tab).addClass("active");
            } else {
                $(tab).removeClass("active");
            }
        });
    }
};

Attendance.Validator = new function () {
    var self = this;
    // 表单是否符合验证规则
    self.isValid = function (fromId) {
        $(fromId).data('bootstrapValidator').validate();
        return $(fromId).data('bootstrapValidator').isValid();
    }

}