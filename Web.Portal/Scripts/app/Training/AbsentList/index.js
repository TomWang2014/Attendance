var vm = new Vue({
    el: 'body',
    data: {
        studentListSearchDto: new service.studentList.dto.studentListSearchDto(),//搜索学员dto       
        list: [],
        current: 0
    },
    methods: {
        // 获得学员列表
        getStudentList: function () {
            var modelstr = JSON.stringify(this.studentListSearchDto);
            service.studentList.io.getStudentList(modelstr).then(function (data) {
                vm.list = [];
                $(data.List).each(function (index, item) {
                    vm.list.push(item);
                });
                $("#pager").pager(data.PageIndex, data.PageCount, data.PageSize, data.RecordCount);
            });
        },
        // 排序
        sort: function (filds) {
            this.studentListSearchDto.Orderby = filds;
            this.studentListSearchDto.Desc = !this.studentListSearchDto.Desc;
            this.getStudentList();
        }
    },
    ready: function () {

        this.getStudentList();
        $("#pager").pager().bind("changed.pager", function (e, newPage) {
            vm.studentListSearchDto.PageIndex = newPage;
            vm.getStudentList();
        });
    }
});
