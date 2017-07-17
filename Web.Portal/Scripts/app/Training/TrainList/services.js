var service = service || {};

(function () {
    var adminService = function (serviceName) {
        this.name = serviceName || "UnnamedService";
        this.pageSzie = 10;
    };

    adminService.prototype.dto = {
        // dto 
        trainListDto: function () {
            this.Id = 0;
            this.Name = '';//培训班名称
            this.StartTime = '';//开课时间
            this.EndTime = '';//结课时间
            this.SignUpStart = '';//报名开始时间
            this.SignUpEnd = '';//报名截止时间
            this.Descriptions = '';//描述
        },

        trainListSearchDto: function () {
            this.PageIndex = 1;
            this.PageSize = 10;
            this.Name = '';
            this.Start = '';
            this.End = '';
            this.Orderby = "CreateTime";
            this.Desc = true;
        }
    };

    adminService.prototype.io = {

        // 获得培训班集合
        getList: function (model) {
            return $.ajax({
                url: '/Training/GetTrainingList',
                data: { model: model },
                type: "get",
                cache: false
            });
        },
        modifyTrainInfo: function (model) {
            return $.ajax({
                url: '/Training/ModifyTrainingInfo',
                data: { model: model },
                type: "post",
                cache: false
            });
        }
    };

    service.trainList = service.trainList || new adminService("admin-service");
})();