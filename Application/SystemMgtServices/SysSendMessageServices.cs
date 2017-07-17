

namespace Attendance.Application.SystemMgtServices
{
    using Attendance.Infrastructure.Data;
    using Attendance.Infrastructure.Unity.Aop;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    /// <summary>
    /// 短信发送  邮件发送相关
    /// </summary>
    public class SysSendMessageServices : InterceptiveObject
    {
        /// <summary>
        /// 数据库工作单元
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="SysSendMessageServices"/> class.
        /// </summary>
        /// <param name="_dbBase">
        /// The _db base.
        /// </param>
        public SysSendMessageServices(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        #region 发送基本方法

        /// <summary>
        /// 系统发送 站内信、邮件、短信 
        /// </summary>
        /// <param name="title">
        /// 标题
        /// </param>
        /// <param name="content">
        /// 内容
        /// </param>
        /// <param name="recUserId">
        /// 收件人
        /// </param>
        /// <param name="tenantId">
        /// </param>
        /// <param name="sendFlag">
        /// </param>
        /// 发送方式 
        /// <param>
        /// 0：站内信；1 邮件
        /// </param>
        //public void SendMessage(string title, string content, int[] recUserId, int tenantId, int sendFlag = 0)
        //{
        //    if (recUserId.Length == 0 || string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content)
        //        || sendFlag != 0)
        //    {
        //        return;
        //    }

        //    var messageContent = new SysUserMessageContent()
        //    {
        //        Title = title,
        //        MessageContent = content,
        //        CreateTime = DateTime.Now,
        //        TenantId = tenantId
        //    };
        //    var id = this.dbBase.AddEntity(messageContent);
        //    foreach (
        //        var model in
        //            recUserId.Select(
        //                t =>
        //                new SysUserMessage
        //                {
        //                    MessageId = id,
        //                    SendStatus = 0,
        //                    RecStatus = 0,
        //                    SendUserId = 0,
        //                    RecUserId = t,
        //                    TenantId = tenantId
        //                }))
        //    {
        //        this.dbBase.AddEntity(model);
        //    }
        //}

        #endregion

        #region 国培短信专用

        /**
        * 服务http地址
        */

        /// <summary>
        /// The bas e_ uri.
        /// </summary>
        private static string BASE_URI = "http://yunpian.com";

        /**
        * 服务版本号
        */

        /// <summary>
        /// The version.
        /// </summary>
        private static string VERSION = "v1";

        /**
        * 查账户信息的http地址
        */

        /// <summary>
        /// The ur i_ ge t_ use r_ info.
        /// </summary>
        private static string URI_GET_USER_INFO = BASE_URI + "/" + VERSION + "/user/get.json";

        /**
        * 智能匹配模版接口发短信的http地址
        */

        /// <summary>
        /// The ur i_ sen d_ sms.
        /// </summary>
        private static string URI_SEND_SMS = BASE_URI + "/" + VERSION + "/sms/send.json";

        /**
        * 模板接口短信接口的http地址
        */

        /// <summary>
        /// The ur i_ tp l_ sen d_ sms.
        /// </summary>
        private static string URI_TPL_SEND_SMS = BASE_URI + "/" + VERSION + "/sms/tpl_send.json";

        /**
        * 取账户信息
        * @return json格式字符串
        */

        /// <summary>
        /// The get user info.
        /// </summary>
        /// <param name="apikey">
        /// The apikey.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string getUserInfo(string apikey)
        {
            var req = WebRequest.Create(URI_GET_USER_INFO + "?apikey=" + apikey);
            var resp = req.GetResponse();
            var sr = new StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

        /**
        * 智能匹配模版接口发短信
        * @param text　短信内容　
        * @param mobile　接受的手机号
        * @return json格式字符串
        */

        /// <summary>
        /// The send sms.
        /// </summary>
        /// <param name="apikey">
        /// The apikey.
        /// </param>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="mobile">
        /// The mobile.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string sendSms(string apikey, string text, string mobile)
        {
            // 注意：参数必须进行Uri.EscapeDataString编码。以免&#%=等特殊符号无法正常提交
            var parameter = "apikey=" + apikey + "&text=" + Uri.EscapeDataString(text) + "&mobile=" + mobile;
            var req = WebRequest.Create(URI_SEND_SMS);
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            var bytes = Encoding.UTF8.GetBytes(parameter); // 这里编码设置为utf8
            req.ContentLength = bytes.Length;
            var os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length);
            os.Close();
            var resp = req.GetResponse();
            if (resp == null)
            {
                return null;
            }

            var sr = new StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

        /**
        * 模板接口发短信
        * @param tpl_id 模板id
        * @param tpl_value 模板变量值
        * @param mobile　接受的手机号
        * @return json格式字符串
        */

        /// <summary>
        /// The tpl send sms.
        /// </summary>
        /// <param name="apikey">
        /// The apikey.
        /// </param>
        /// <param name="tpl_id">
        /// The tpl_id.
        /// </param>
        /// <param name="tpl_value">
        /// The tpl_value.
        /// </param>
        /// <param name="mobile">
        /// The mobile.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string tplSendSms(string apikey, long tpl_id, string tpl_value, string mobile)
        {
            apikey = "c35ee9d78aab145b8609c07aa8ca12aa";
            var encodedTplValue = Uri.EscapeDataString(tpl_value);
            var parameter = "apikey=" + apikey + "&tpl_id=" + tpl_id + "&tpl_value=" + encodedTplValue + "&mobile="
                            + mobile;
            var req = WebRequest.Create(URI_TPL_SEND_SMS);
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            var bytes = Encoding.UTF8.GetBytes(parameter); // 这里编码设置为utf8
            req.ContentLength = bytes.Length;
            var os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length);
            os.Close();
            var resp = req.GetResponse();
            if (resp == null)
            {
                return null;
            }

            var sr = new StreamReader(resp.GetResponseStream());
            var result = sr.ReadToEnd().Trim();
            return result;
        }

        /// <summary>
        /// The send message system.
        /// </summary>
        /// <param name="mobile">
        /// The mobile.
        /// </param>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <param name="hour">
        /// The hour.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool SendMessageSystem(string mobile, string code, int hour)
        {
            try
            {
                // 修改为您的apikey
                var apikey = ConfigurationManager.AppSettings["MobileMessage"];
                long tpl_id = 1227055; // 使用模板ID

                // 注意：参数必须进行Uri.EscapeDataString编码。以免&#%=等特殊符号无法正常提交
                var tpl_value = "#code#=" + Uri.EscapeDataString(code) + "&#hour#="
                                + Uri.EscapeDataString(hour.ToString());
                Console.WriteLine(this.tplSendSms(apikey, tpl_id, tpl_value, mobile));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}
