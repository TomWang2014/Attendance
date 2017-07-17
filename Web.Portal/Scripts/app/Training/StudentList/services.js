var service = service || {};

(function () {
    var adminService = function (serviceName) {
        this.name = serviceName || "UnnamedService";
        this.pageSzie = 10;
    };

    adminService.prototype.dto = {
        // dto 
        signUpDto: function () {
            this.Id = 0;
            this.Name = trainName;//培训班名称
            this.TrainingCardCardNumber = '';//学员卡号
            this.StudentPhone = '';//学员手机号
            this.StudentName = '';//学员姓名
            this.TrainingId = trainId;
        },
        studentListSearchDto: function () {
            this.PageIndex = 1;
            this.PageSize = 10;
            this.TrainId = trainId;
            this.StudentPhone = '';
            this.CardNumber = '';
            this.StartTime = '';
            this.EndTime = '';
            this.Orderby = "CreateTime";
            this.Desc = true;
        }
    };

    adminService.prototype.io = {

        // 获得学员的集合
        getStudentList: function (model) {
            return $.ajax({
                url: '/Training/GetStudentList',
                data: { model: model },
                type: "get",
                cache: false
            });
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

    service.studentList = service.studentList || new adminService("admin-service");
})();