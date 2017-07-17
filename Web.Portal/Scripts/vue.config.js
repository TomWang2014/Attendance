/*!
 * vueJs配置文件
 * waterfall - v1.0.0 (2013-03-15T14:55:51+0800)
 * http://cn.vuejs.org/guide/instance.html| Released under MIT license
 */
(function (vue) {

    //在调试模式中，Vue 会：1、为所有的警告打印栈追踪。2、把所有的锚节点以注释节点显示在 DOM 中，更易于检查渲染结果的结构。
    Vue.config.debug = true;

    Vue.config.silent = true;

    // 配置是否允许 vue-devtools 检查代码。开发版默认为 true， 生产版默认为 false。 生产版设为 true 可以启用检查。
    Vue.config.devtools = true;

    // 截取字符串指定长度的过滤器
    vue.filter('substring', function (value, length) {
        if (value.length > length) {
            return value.substring(0, length) + "...";
        } else {
            return value.substring(0, length);
        }
    });

    // 证书类型
    vue.filter('toCertificateType', function (value) {
        if (value) {
            return '自动';
        } else {
            return '手动';
        }
    });

    // 交易状态过滤器
    vue.filter('orderStatusStr', function (value) {
        // 订单状态0 草稿中 1 待审批 2 审批通过 3 审批拒绝 4 待付款 5 已付款 6 已发货 7 已完成
        switch (value) {
            case 0:
                return '草稿中';
            case 1:
                return '待审批';
            case 2:
                return '审批通过';
            case 3:
                return '审批拒绝';
            case 4:
                return '待付款';
            case 5:
                return '已付款';
            case 6:
                return '已发货';
            case 7:
                return '已完成';
            default:
                return '状态异常';
        }
    });
})(Vue);


