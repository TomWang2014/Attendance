/*!
 * 前端全局控制
 *Include
 *jquery 、layer
 * waterfall - v1.0.0 (2013-03-15T14:55:51+0800)
 * http://jraiser.org/ | Released under MIT license
 */

//ajax请求全局处理异常
(function ($, layer) {
    // ajax事件开始
    $(document).ajaxStart(function () {
        layer.load(2);
    });

    $(document).ajaxStop(function () {
        //此处演示关闭
        layer.closeAll('loading');
    });

    $(document).ajaxError(function (event, xhr, options, exc) {
        layer.closeAll('loading');
        console.log(xhr);
        console.log(event);
        console.log(options);
        console.log(exc);

        if (xhr.status == 401) {
            // ajax返回请求没有通过登录身份验证
            Attendance.Msg.showError("抱歉，您的登录验证失效，3秒后刷新页面！");
            setTimeout("window.location.reload()", 3000);
            return;
        }

        // 用户自定义异常
        if (xhr.responseText) {
            try {
                var obj = JSON.parse(xhr.responseText);
                if (obj) {
                    Attendance.Msg.showError(obj.errorMessage);
                } else {
                    Attendance.Msg.showError("抱歉，当前请求发生错误!");
                }

            } catch (e) {
                Attendance.Msg.showError("抱歉，当前请求发生错误!");
            }
        }
    });

})(jQuery, layer);

///**
// js全局错误处理
// * @param {String}  errorMessage   错误信息
// * @param {String}  scriptURI      出错的文件
// * @param {Long}    lineNumber     出错代码的行号
// * @param {Long}    columnNumber   出错代码的列号
// * @param {Object}  errorObj       错误的详细信息，Anything
// */
//window.onerror = function (errorMessage, scriptURI, lineNumber, columnNumber, errorObj) {
//    setTimeout(function () {
//        //此处演示关闭
//        layer.closeAll('loading');
//    }, 2000);

//    noty({ text: "抱歉，当前页面脚本执行有错误，请联系管理员!", layout: 'top', type: 'error', timeout: 1500 });
//    console.log("错误信息：", errorMessage);
//    console.log("出错文件：", scriptURI);
//    console.log("出错行号：", lineNumber);
//    console.log("出错列号：", columnNumber);
//    console.log("错误详情：", errorObj);
//}


