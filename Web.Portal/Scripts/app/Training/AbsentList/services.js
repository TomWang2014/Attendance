var service = service || {};

(function () {
    var adminService = function (serviceName) {
        this.name = serviceName || "UnnamedService";
        this.pageSzie = 10;
    };

    adminService.prototype.dto = {      
        studentListSearchDto: function () {
            this.PageIndex = 1;
            this.PageSize = 10;
            this.TrainId = trainId;
            this.StudentPhone = '';
            this.CardNumber = '';
            this.Orderby = "CreateTime";
            this.Desc = true;
        },
        modifySignUpInfo: function (model) {
            return $.ajax({
                url: '/Training/ModifySignUpInfo',
                data: { model: model },
                type: "post",
                cache: false
            });
        }
    };

    adminService.prototype.io = {

        // 获得学员的集合
        getStudentList: function (model) {
            return $.ajax({
                url: '/Training/GetAbsentStudentList',
                data: { model: model },
                type: "get",
                cache: false
            });
        }
    };

    service.studentList = service.studentList || new adminService("admin-service");
})();