//自定义js
//公共配置
$(document).ready(function () {
    // 左侧table插件初始化
    $('#side-menu').metisMenu();

    // 打开右侧边栏
    $('.right-sidebar-toggle').click(function () {
        $('#right-sidebar').toggleClass('sidebar-open');
    });

    // 右侧边栏使用slimscroll，滚动条美化插件
    $('.sidebar-container').slimScroll({
        height: '100%',
        railOpacity: 0.4,
        wheelStep: 10
    });
    //左侧固定菜单栏，使用滚动条美化插件，超出已有区域时出现滚动条
    $(function () {
        $('.sidebar-collapse').slimScroll({
            height: '100%',
            railOpacity: 0.9,
            alwaysVisible: false
        });
    });
    // 左侧菜单切换（展开收缩功能）
    $('.navbar-minimalize').click(function () {
        $("body").toggleClass("mini-navbar");
        //判断当前body出于何种状态，用以张开或者收缩左侧的菜单栏
        SmoothlyMenu();
    });

    $('.full-height-scroll').slimScroll({
        height: '100%'
    });

    // 在处于关闭状态下的时候点击左侧的table按键会模拟点击展开按钮
    $('#side-menu>li').click(function () {
        if ($('body').hasClass('mini-navbar')) {
            NavToggle();
        }
    });
    $('#side-menu>li li a').click(function () {
        if ($(window).width() < 769) {
            NavToggle();
        }
    });

    //ios浏览器兼容性处理
    if (/(iPhone|iPad|iPod|iOS)/i.test(navigator.userAgent)) {
        $('#content-main').css('overflow-y', 'auto');
    }

});

// 屏幕大小自适应处理（左侧table栏是否需要关闭）
$(window).bind("load resize", function () {
    if ($(this).width() < 769) {
        $('body').addClass('mini-navbar');
        $('.navbar-static-side').fadeIn();
    }
});

//模拟点击左侧导航栏展开汗牛
function NavToggle() {
    $('.navbar-minimalize').trigger('click');
}

//处理左侧table的显示隐藏
function SmoothlyMenu() {
    if (!$('body').hasClass('mini-navbar')) {
        $('#side-menu').hide();
        setTimeout(
            function () {
                $('#side-menu').fadeIn(500);
            }, 100);
    } else if ($('body').hasClass('fixed-sidebar')) {
        $('#side-menu').hide();
        setTimeout(
            function () {
                $('#side-menu').fadeIn(500);
            }, 300);
    } else {
        $('#side-menu').removeAttr('style');
    }
}
