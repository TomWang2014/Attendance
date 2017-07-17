"use strict";

var APP = new Vue({
    el: ".sign_inner",
    data: function data() {
        return {
            number: "",
            password: "",
            phone: "",
            code: "",
            flag: 1,
            time: "获取验证码",
            interval: null,
            name: ""
        };
    },

    methods: {

        /**
         * [注册方法]
         * @method sign
         * @return {[type]} [description]
         * @description 注册前检查必须项是否为空，为空则提示并且返回，
         * ajax请求数据
         */
        sign: function sign() {
            //验证学员卡是否为空
            if (this.number.trim() === "") {
                alert("学员卡号不能为空");
                return false;
            }
            //验证学员卡格式(数字)
            if (!/^[0-9]*$/.test(this.number.trim())) {
                alert("学员卡号不正确！");
                return false;
            }
            //验证学员卡长度
            if (this.number.trim().length > 16) {
                alert("学员卡号不正确！");
                return false;
            }
            //验证培训班密码是否为空
            if (this.password.trim() == "") {
                alert("培训班密码不能为空！");
                return false;
            }
            //验证培训班密码长度
            if (this.password.trim().length > 6) {
                alert("培训班密码不正确！");
                return false;
            }

            //验证手机号是否为空
            if (this.phone.trim() == "") {
                alert("手机号不能为空！");
                return false;
            }
            //验证手机号格式
            if (!/^1\d{10}$/.test(this.phone.trim())) {
                alert("手机号格式不正确！");
                return false;
            }
            //验证手机验证码是否为空
            if (this.code.trim() == "") {
                alert("验证码不能为空！");
                return false;
            }
            //验证码格式是否正确（数字）
            if (!/^[0-9]*$/.test(this.code.trim()) || this.code.trim().length > 5) {
                alert("验证码不正确！");
                return false;
            }
            $.ajax({
                url: '/Account/SignUpTraining',
                type: 'Post',
                data: { CardNumber: this.number.trim(), Password: this.password.trim(), StudentName: this.name.trim(), StudentPhone: this.phone.trim(), verCode: this.code.trim(), TrainId: trainId },
                success: function (data) {
                    if (data.status) {
                        //报名成功
                        alert(data.msg);
                        //跳转到报名成功页面
                        window.location.href = '/Account/SignUpSuccess?id=' +trainId;
                    } else {
                        //报名失败
                        alert(data.msg);
                        //如果是重复报名跳转到报名失败页面
                        if (data.msg == "您已注册过该培训班！")
                        window.location.href = '/Account/SignUpFailed';
                    }
                }
            });

        },

        /**``
         * [获取二维码]
         * @method getCode
         * @return {[type]} [descriptio]
         * @description 检查必须项是否为空值，空值返回，
         * 点击成功后立刻置为不可点击状态，使用flag变量控制，
         * 启动定时器，60秒后清除并且重置状态
         */
        getCode: function getCode() {
            var _this = this;

            if (this.flag === 1) {
                //验证手机号是否为空
                if (this.phone.trim() === "") {
                    alert("请输入您的手机号码");
                    return false;
                }
                //验证手机号格式
                if (!/^1\d{10}$/.test(this.phone.trim())) {
                    alert("手机号格式不正确！");
                    return false;
                }

               

                this.flag = 0;
                this.time = 60;
                this.interval = setInterval(function () {
                    _this.time--;
                    if (_this.time === 1) {
                        clearInterval(_this.interval);
                        _this.flag = 1;
                        _this.time = "获取验证码";
                    }
                }, 1000);

                $.ajax({
                    url: '/Account/GetVerificationCode',
                    type: 'Post',
                    data: { phone: this.phone.trim(), id: trainId },
                    success: function (data) {
                        if (!data.status) {
                            clearInterval(_this.interval);
                            _this.flag = 1;
                            _this.time = "获取验证码";
                            //展示提示信息
                            alert(data.msg);
                           
                        } else {
                            alert("验证码已发送，请注意接收！");
                        }
                    }
                });


            }
        }
    },
    mounted: function mounted() { }
});