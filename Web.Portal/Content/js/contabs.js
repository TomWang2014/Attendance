$(function() {
 
  //左侧table点击添加新的iframe层
  function menuItem() {
    // 获取标识数据，获取ifrmae对象的url值；
    var dataUrl = $(this).attr('href'),
      //取出对应的data-index值；
      // dataIndex = $(this).data('index'),
      //获取text值，并且取出前后的空格和制表符
      menuName = $.trim($(this).text());
    // 标签无值，或者url为空，结束程序并且返回
    if (dataUrl === undefined || $.trim(dataUrl).length === 0) {
      return false;
    }
    $('.J_mainContent .J_iframe').each(function() {
      // 还是以id值来进行判断，改成以数据驱动的方式应该会大量的减少代码的数量
      if ($(this).data('src') !== dataUrl) {
        $(this).attr('src', dataUrl);
        $(this)[0].src=dataUrl;
        var maodian = window.location.href.split("#")[0];
        window.location.href = maodian + "#" + dataUrl;
        return false;
      }
    });
   
    return false;
  }
  //左侧导航点击函数
  $('.J_menuItem').on('click', menuItem);

  
  });
