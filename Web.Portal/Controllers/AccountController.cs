// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="zjzx">
//   ©2016 中教在线 版权所有
// </copyright>
// <summary>
//   Defines the HomeController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Web.Portal.Controllers
{
    using System;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Attendance.Application.SystemMgtServices;
    using Attendance.Infrastructure;
    using Attendance.Infrastructure.Entity;
    using Attendance.Infrastructure.Toolkit;
    using Attendance.Web.Portal.Toolkits;
    using Application.SystemMgtServices.Dtos;


    /// <summary>
    /// 账户相关控制器
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// The system services.
        /// </summary>
        private readonly SystemServices systemServices;

        /// <summary>
        /// 登陆日志
        /// </summary>
        private readonly SysLogServices logServices;

        private readonly SysSendMessageServices sysSendMessageServices;

        /// <summary>
        /// 系统服务
        /// </summary>
        /// <param name="systemServices">系统管理访问</param>
        /// <param name="logServices">登陆日志</param>
        public AccountController(SystemServices systemServices, SysLogServices logServices, SysSendMessageServices _sysSendMessageServices)
        {
            this.systemServices = systemServices;
            this.logServices = logServices;
            this.sysSendMessageServices = _sysSendMessageServices;
        }

        /// <summary>
        /// 系统用户登录
        /// </summary>
        /// <returns>返回系统用户登录页面</returns>
        public ActionResult Login()
        {
            ToolkitsHelper.ClientRouteInit(HttpContext, RouteData);
            return this.View();
        }

        /// <summary>
        /// 处理用户登录提交的数据
        /// </summary>
        /// <param name="account"> 用户账号</param>
        /// <param name="password"> 用户密码 </param>
        /// <param name="verCode">验证码</param>
        /// <returns> 返回登录成功后的页面 </returns>
        [HttpPost]
        public ActionResult Login(string account, string password, string verCode)
        {
            string msg;

            if (string.IsNullOrWhiteSpace(verCode))
            {
                this.ViewBag.msg = "请输入正确的验证码！";
                return this.View();
            }

            var code = this.TempData["vcode"] as string;
            if (!verCode.Equals(code))
            {
                this.ViewBag.msg = "请输入正确的验证码！";
                return this.View();
            }

            var user = this.systemServices.UserLogin(account, password, out msg);
            this.ViewBag.msg = msg;
            if (user == null)
            {
                return this.View();
            }


            try
            {
                var ip = HttpContext.Request.UserHostAddress;
                var browser = HttpContext.Request.Browser.Browser;

                // 新增登录日志
                this.logServices.AddLoginLog(new LoginLogDto() { AddressId = ip, BrowserInfo = browser, UserId = user.Id, LoginTime = DateTime.Now });
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex);
            }


            // 计算可访问的权限
            foreach (var sysFuncItem in user.FuncItems)
            {
                var roleName = ToolkitsHelper.InitAllFunc().FirstOrDefault(a => a.RoleName == sysFuncItem.ToString());
                if (roleName != null)
                {
                    user.AuthenticationUrl.AddRange(roleName.IncludeUrl);
                }

                foreach (var funcSmall in sysFuncItem.SubSysFunc)
                {
                    var smallRoleName = ToolkitsHelper.InitAllFunc().FirstOrDefault(a => a.RoleName == funcSmall.ToString());
                    if (smallRoleName != null)
                    {
                        user.AuthenticationUrl.AddRange(smallRoleName.IncludeUrl);
                    }
                }
            }

            UserIdentity.CurrentUser = user;
            return this.Redirect("/home/index");
        }

        /// <summary>
        /// 用户退出登录
        /// </summary>
        /// <returns>返回登录页面</returns>
        public ActionResult LoginOut()
        {
            var limit = Request.Cookies.Count;
            for (var i = 0; i < limit; i++)
            {
                var httpCookie = this.Request.Cookies[i];
                if (httpCookie == null)
                {
                    continue;
                }

                var cookieName = httpCookie.Name;
                var aCookie = new HttpCookie(cookieName) { Expires = DateTime.Now.AddDays(-1) };
                this.Response.Cookies.Add(aCookie);
            }

            Session.Abandon();
            return this.Redirect("/home/index");
        }

        /// <summary>
        /// 代理登录页面
        /// <remarks>为即将iframe嵌套登录bug，将需要登录的请求转发到此</remarks>
        /// </summary>
        /// <returns>返回登录页</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查看当前系统配置文件中的权限列表
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult GetFileResult()
        {
            return Json(ToolkitsHelper.InitAllFunc(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获得图片验证码
        /// </summary>
        public void GetImgVerificationCode()
        {
            // 生成随机验证码图
            string vcode;
            var basemap = RandomNumberHelper.GetValidateCodeMap(out vcode, 4);

            // 存储在tempdata并且返回图片格式
            this.TempData["vcode"] = vcode;
            basemap.Save(this.Response.OutputStream, ImageFormat.Gif);
            this.Response.End();
        }


        #region 考勤相关
        /// <summary>
        /// 报名培训班
        /// </summary>
        /// <param name="id">培训班id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SignUpTraining(int id)
        {
            ViewBag.TrainId = 0;
            ViewBag.TrainName = "";
            ViewBag.Start = "";
            ViewBag.End = "";
            if (id > 0)
            {
                ViewBag.TrainId = id;
                var model = this.systemServices.GetTrainById(id);
                if (model != null)
                {
                    ViewBag.TrainName = model.Name;
                    ViewBag.Start = model.StartTimeStr;
                    ViewBag.End = model.EndTimeStr;
                }
            }
            return View();
        }

        /// <summary>
        /// 报名培训班
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SignUpTraining(string CardNumber, string Password, string StudentName, string StudentPhone, string verCode, int TrainId = 0)
        {
            //参数验证
            if (string.IsNullOrEmpty(CardNumber))
            {
                return this.Json(new { msg = "学员卡号不能为空！", status = false }, JsonRequestBehavior.AllowGet);
            }
            if (TrainId <= 0)
            {
                return this.Json(new { msg = "该培训班不存在！", status = false }, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(Password))
            {
                return this.Json(new { msg = "培训班密码不能为空！", status = false }, JsonRequestBehavior.AllowGet);
            }
            if (RegularHelp.IsContainZhCn(Password))
            {
                return this.Json(new { msg = "密码不能包含汉字！", status = false }, JsonRequestBehavior.AllowGet);
            }
            if (Password.Length < 6 || Password.Length > 16)
            {
                return this.Json(new { msg = "密码长度为6到16个字符！", status = false }, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(StudentPhone))
            {
                return this.Json(new { msg = "手机号码不能为空！", status = false }, JsonRequestBehavior.AllowGet);
            }
            if (!RegularHelp.IsMobilePhone(StudentPhone))
            {
                return this.Json(new { msg = "手机号不合法！", status = false }, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(verCode))
            {
                return this.Json(new { msg = "验证码不能为空！", status = false }, JsonRequestBehavior.AllowGet);
            }

            var train = this.systemServices.GetTrainById(TrainId);
            if (train == null)
            {
                return this.Json(new { msg = "该培训班不存在！", status = false }, JsonRequestBehavior.AllowGet);
            }
            if (train.Password.Trim() != Password.Trim())
            {
                return this.Json(new { msg = "密码错误！", status = false }, JsonRequestBehavior.AllowGet);
            }
            if (DateTime.Now < train.SignUpStart)
            {
                return this.Json(new { msg = "未到报名时间！", status = false }, JsonRequestBehavior.AllowGet);
            }
            if (DateTime.Now > train.SignUpEnd)
            {
                return this.Json(new { msg = "报名时间已过！", status = false }, JsonRequestBehavior.AllowGet);
            }
            //通过cardNumber获取学员卡id  判断此学员卡是否存在
            int cardid = this.systemServices.GetStudentCardId(CardNumber);
            if (cardid <= 0)
            {
                return this.Json(new { msg = "学员卡号有误！", status = false }, JsonRequestBehavior.AllowGet);
            }
            //判断学员卡是否已经报名该培训班
            if (this.systemServices.CheckCardExsit(cardid, TrainId))
            {
                return this.Json(new { msg = "您已注册过该培训班！", status = false }, JsonRequestBehavior.AllowGet);
            }

            // 验证手机号码是否已经注册过该培训班
            if (this.systemServices.CheckPhoneIsExits(StudentPhone, TrainId))
            {
                return this.Json(new { msg = "该手机号已经被注册！", status = false }, JsonRequestBehavior.AllowGet);
            }

            var VCode = this.systemServices.GetOneVerificationCode(PhoneHandle(StudentPhone), DateTime.Now.AddHours(-1));
            if (VCode == null)
            {
                return this.Json(new { msg = "验证码错误！", status = false }, JsonRequestBehavior.AllowGet);
            }
            if (verCode != VCode.VerificationCode)
            {
                return this.Json(new { msg = "验证码错误！", status = false }, JsonRequestBehavior.AllowGet);
            }
            VCode.IsVerification = 1;
            this.systemServices.UpdateVertifyStatus(VCode);//更新验证码状态


            //添加报名
            TrainingSignUpDto signup = new TrainingSignUpDto()
            {
                TrainingId = TrainId,
                CardId = cardid,
                StudentName = StudentName,
                StudentPhone = StudentPhone,
                CreateTime = DateTime.Now
            };
            if (this.systemServices.AddSignUpInfo(signup))
            {
                return this.Json(new { msg = "报名成功！", status = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new { msg = "报名失败！", status = true }, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// 发送手机验证码
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="id">培训班id</param>
        /// <returns>是否发送成功</returns>
        [HttpPost]
        public JsonResult GetVerificationCode(string phone, int id = 0)
        {
            try
            {
                if (string.IsNullOrEmpty(phone))
                {
                    return this.Json(new { msg = "手机号不能为空！", status = false }, JsonRequestBehavior.AllowGet);
                }
                if (!RegularHelp.IsMobilePhone(phone))
                {
                    return this.Json(new { msg = "手机号不合法！", status = false }, JsonRequestBehavior.AllowGet);
                }
                // 验证手机号码是否已经注册过该培训班
                if (this.systemServices.CheckPhoneIsExits(phone, id))
                {
                    return this.Json(new { msg = "该手机号已经被注册！", status = false }, JsonRequestBehavior.AllowGet);
                }

                var VCode = this.systemServices.GetOneVerificationCode(PhoneHandle(phone), DateTime.Now.AddMinutes(-1));
                if (VCode != null)
                {
                    return this.Json(new { msg = "验证码已发送，请不要重复获取！", status = false }, JsonRequestBehavior.AllowGet);
                }
                var code = new Tab_VerificationCodeDto();
                code.IsVerification = 0;
                code.StudentPhone = PhoneHandle(phone);
                var random = new Random();
                var codeNum = random.Next(9999, 100000).ToString();
                code.VerificationCode = codeNum;
                code.VerificationTime = DateTime.Now;
                if (this.systemServices.AddVerificationCode(code) > 0)
                {
                    this.sysSendMessageServices.SendMessageSystem(phone, codeNum, 1);
                    return this.Json(new { msg = "发送成功！", status = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return this.Json(new { msg = "获取手机验证码失败！", status = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return this.Json(new { msg = "获取手机验证码失败！", status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        private int PhoneHandle(string Phone)
        {
            return int.Parse(Phone.Substring(0, 4) + Phone.Substring(Phone.Length - 4, 4));
        }

        /// <summary>
        /// 注册成功
        /// </summary>
        /// <returns></returns>
        public ActionResult SignUpSuccess(int id = 0)
        {
            ViewBag.TrainName = "";
            ViewBag.Start = "";
            ViewBag.End = "";
            if (id > 0)
            {
                var model = this.systemServices.GetTrainById(id);
                if (model != null)
                {
                    ViewBag.TrainName = model.Name;
                    ViewBag.Start = model.StartTimeStr;
                    ViewBag.End = model.EndTimeStr;
                }
                
                
            }
            return View();
        }
        /// <summary>
        /// 重复注册
        /// </summary>
        /// <returns></returns>
        public ActionResult SignUpFailed()
        {
            return View();
        }

        #endregion
    }
}