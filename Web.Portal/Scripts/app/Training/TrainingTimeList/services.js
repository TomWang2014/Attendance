var service = service || {};

(function () {
    var adminService = function (serviceName) {
        this.name = serviceName || "UnnamedService";
        this.pageSzie = 10;
    };

    adminService.prototype.dto = {
        trainListSearchDto: function () {
            this.PageIndex = 1;
            this.PageSize = 10;
            this.StartTime = '';
            this.EndTime = '';
            this.TrainId = TrainId;
            this.Orderby = "CreateTime";
            this.Desc = true;
        }
    };

    adminService.prototype.io = {

        // 获得考勤查询时间段集合
        getList: function (model) {
            return $.ajax({
                url: '/Training/GetTrainingTimeList',
                data: { model: model },
                type: "get",
                cache: false
            });
        },
        modifyTimeInfo: function (model) {
            return $.ajax({
                url: '/Training/ModifyTimeInfo',
                data: { model: model },
                type: "post",
                cache: false
            });
        },
        DeleteTrainDate: function (id, date) {
            return $.ajax({
                url: '/Training/DeleteTrainDate',
                data: { id: id, date: date },
                type: "post",
                cache: false
            });
        }
    };

    service.trainList = service.trainList || new adminService("admin-service");
})();