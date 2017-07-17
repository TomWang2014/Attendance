(function ($) {
	/*分页控件构造函数
	@@page int: 当前页码
	@@pageCount int: 总页数
	@@pageSize int: 每页记录数
	@@recordCount int: 记录总数*/
	$.fn.pager = function (page, pageCount, pageSize, recordCount) {
		var self = this;

		this.bind("changed.pager", function (e, newPage){
			
		});

		self.isDefined = function(param){
			return typeof(param) != "undefined";
		};

		/*显示分页信息和链接
		@@page int: 当前页码
		@@pageCount int: 总页数
		@@pageSize int: 每页记录数
		@@recordCount int: 记录总数
		@@maxPageNumberToShow int: 可显示的页码总数*/
		self.render = function(page, pageCount, pageSize, recordCount, maxPageNumberToShow)
		{
	        var firstPageToShow = 1;
	        var lastPageToShow = pageCount;

	        if(pageCount == 0){
	        	self.hide();
	        	return;
	        }

			//self.attr("style", "position: absolute;bottom: 10px;width: 96%;margin: 15px 0;");
	        self.show();

	        if(!maxPageNumberToShow)
	        	maxPageNumberToShow =5;//default val

	        if(pageCount > maxPageNumberToShow){
	            firstPageToShow = page - parseInt(maxPageNumberToShow / 2);

	            if(firstPageToShow < 1){
	                firstPageToShow = 1;
	            }

	            lastPageToShow = firstPageToShow + maxPageNumberToShow -1;

	            if(lastPageToShow > pageCount){
	                firstPageToShow = pageCount - maxPageNumberToShow + 1;
	                lastPageToShow = pageCount;
	            }
	        }

			//清空原有内容
	        this.empty();

		     //        <div class="row">
		     //    		<div class="col-xs-7">
		     //    		 	<span></span>
		     //    		</div>
		     //    		<div class="col-xs-5">
		     //    		  	<nav>
							//   	<ul class="pagination pagination-sm">
							// 	    <li class="disabled"><a href="#">上一页</a></li>
							// 	    <li class="active"><a href="#">1 <span class="sr-only">(current)</span></a></li>

							// 	    <li><a href="#">下一页</a></li>
							//   	</ul>
							// </nav>
		     //    		</div>
		     //    	</div>
     		var rowDiv = $("<div>").attr("class","row");
     		var colDiv7 = $("<div>").attr("class","col-md-5 col-sm-12").attr("style","padding-top:10px;");
     		var colDiv5 = $("<div>").attr("class","col-md-7 col-sm-12");

     		//var span = $("<span>").attr("style","margin-top:10px;");
     		var span = $("<span>");
     		var summaryInfo = "第<span style='color:#b70c5e'> " + page + " / " + pageCount + " </span>页";
     		if(pageSize){
				summaryInfo += "，每页显示<span style='color:#b70c5e'> " + pageSize + " </span>条记录";
     		}
     		if(recordCount){
     			summaryInfo += "，共搜索出<span style='color:#b70c5e'> " + recordCount + " </span>条记录";
     		}
     		span.html(summaryInfo);
     		span.appendTo(colDiv7);

	        var navObj = $("<nav>").attr("style","float:right;");
	        var pageUlObj = $("<ul>").attr("class","pagination pagination-sm");
	        pageUlObj.appendTo(navObj);
	        navObj.appendTo(colDiv5);

	        colDiv7.appendTo(rowDiv);
	        colDiv5.appendTo(rowDiv);

	        rowDiv.appendTo(this);
	        
	        //分页信息        
	        //$("<li class='pull-left' ref='" + page + "'><a href='#'>第" + page + "页/共" +pageCount + "页</a></li>").appendTo(pageUlObj);

	        //首页
	        $("<li><a href='#'>首页</a></li>").attr("ref",1).appendTo(pageUlObj);

	        //上一页
	        $("<li><a href='#'>&laquo;</a></li>").attr("ref",page == 1? page : page -1).appendTo(pageUlObj);

	        //页码
	        for (var i = firstPageToShow; i <= lastPageToShow; i++) {
	            if(i==page){
	                $("<li class='active'><a href='#'>" + i + " </a></li>").attr("ref",i).appendTo(pageUlObj);
	            }else{
	                $("<li><a href='#'>" + i + " </a></li>").attr("ref",i).appendTo(pageUlObj);
	            }      
	        };

	        //下一页
	         $("<li><a href='#'>&raquo;</a></li>").attr("ref",page == pageCount ? pageCount : page + 1).appendTo(pageUlObj);

	        //尾页
	        $("<li><a href='#'>尾页</a></li>").attr("ref",pageCount).appendTo(pageUlObj);
	       
	        this.find("li").bind("click", function(event){
	        	event.preventDefault();
	        	var pageClicked = $(this).attr("ref");
	        	self.trigger("changed.pager", pageClicked);
	        });
		};

		if(self.isDefined(page) && self.isDefined(pageCount) &&  self.isDefined(pageSize) && self.isDefined(recordCount) ){
			self.render(page, pageCount, pageSize, recordCount);
		}
		else if(self.isDefined(page) && self.isDefined(pageCount)){
			self.render(page, pageCount);
		}

		//当前的页码数
		self.get_current_page = function(){
			return $(this).find("li:first").attr("ref");
		};

        return this;
    };

})(jQuery);