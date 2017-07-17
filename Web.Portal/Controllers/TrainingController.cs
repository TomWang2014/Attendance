using Attendance.Application.AttendanceMgtServices;
using Attendance.Application.AttendanceMgtServices.Dtos;
using Attendance.Application.SystemMgtServices;
using Attendance.Infrastructure.Mvc.ModelBinder;
using Attendance.Infrastructure.Toolkit;
using Attendance.Web.Portal.Toolkits;
using Infrastructure.Toolkit;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Attendance.Web.Portal.Controllers
{
    public class TrainingController : AppAuthorizeController
    {
        private readonly AttendanceServices attendanceServices;

        private readonly SystemServices systemServices;
        public TrainingController(AttendanceServices _attendanceServices, SystemServices _systemServices)
        {
            this.attendanceServices = _attendanceServices;
            this.systemServices = _systemServices;
        }

        /// <summary>
        /// 培训班列表-首页
        /// </summary>
        /// <returns></returns>
        public ActionResult TrainingList()
        {
            return View();
        }
        /// <summary>
        /// 获取培训班列表
        /// </summary>
        /// <param name="model">搜索</param>
        /// <returns></returns>
        public JsonResult GetTrainingList([FromJson] TrainListSearch model)
        {
            var list = this.attendanceServices.GetTrainingList(model);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 编辑培训班信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ModifyTrainingInfo([FromJson] TrainListItem model)
        {
            bool result = false;
            if (model != null)
            {
                try
                {
                    if (model.Id > 0)
                    {
                        //修改
                        this.attendanceServices.ModifyTraining(model);
                    }
                    else
                    {
                        //添加
                        model.CreateTime = DateTime.Now;
                        model.Password = RandomNumberHelper.GetRandomNumber(6);
                        this.attendanceServices.AddTraining(model);
                    }
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 编辑报名信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ModifySignUpInfo([FromJson]TrainingSignUpItem model)
        {
            //参数验证
            if (string.IsNullOrEmpty(model.TrainingCardCardNumber))
            {
                return this.Json(new { msg = "学员卡号不能为空！", status = false }, JsonRequestBehavior.AllowGet);
            }
            if (model.TrainingId <= 0)
            {
                return this.Json(new { msg = "该培训班不存在！", status = false }, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(model.StudentPhone))
            {
                return this.Json(new { msg = "手机号码不能为空！", status = false }, JsonRequestBehavior.AllowGet);
            }
            if (!RegularHelp.IsMobilePhone(model.StudentPhone))
            {
                return this.Json(new { msg = "手机号不合法！", status = false }, JsonRequestBehavior.AllowGet);
            }


            var train = this.systemServices.GetTrainById(model.TrainingId);
            if (train == null)
            {
                return this.Json(new { msg = "该培训班不存在！", status = false }, JsonRequestBehavior.AllowGet);
            }

            //通过cardNumber获取学员卡id  判断此学员卡是否存在
            int cardid = this.systemServices.GetStudentCardId(model.TrainingCardCardNumber);
            if (cardid <= 0)
            {
                return this.Json(new { msg = "学员卡号有误！", status = false }, JsonRequestBehavior.AllowGet);
            }

            if (model.Id > 0)
            {
                var newModel = this.attendanceServices.GetTrainingSignUp(cardid, model.TrainingId);
                if (newModel != null)
                {

                    //已经存在，验证一下是不是同一个对象
                    if (newModel.Id == model.Id)
                    {
                        //验证手机号是否修改 如果修改 需要判断是否已经绑定其它培训班
                        if (model.StudentPhone != newModel.StudentPhone)
                        {
                            if (this.systemServices.CheckPhoneIsExits(model.StudentPhone, model.TrainingId))
                            {
                                return this.Json(new { msg = "该手机号已经被注册！", status = false }, JsonRequestBehavior.AllowGet);
                            }
                        }

                        //同一个对象 直接修改
                        if (this.attendanceServices.modifySignUp(model, cardid))
                        {
                            return this.Json(new { msg = "编辑报名信息成功！", status = true }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return this.Json(new { msg = "编辑报名信息失败！", status = false }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        //该学员卡已经注册过了
                        return this.Json(new { msg = "该卡号已注册过该培训班！", status = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    //不是同一个对象 直接修改  
                    if (this.systemServices.CheckPhoneIsExits(model.StudentPhone, model.TrainingId))
                    {
                        return this.Json(new { msg = "该手机号已经被注册！", status = false }, JsonRequestBehavior.AllowGet);
                    }

                    if (this.attendanceServices.modifySignUp(model, cardid))
                    {
                        return this.Json(new { msg = "编辑报名信息成功！", status = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return this.Json(new { msg = "编辑报名信息失败！", status = false }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {

                // 验证手机号码是否已经注册过该培训班
                if (this.systemServices.CheckPhoneIsExits(model.StudentPhone, model.TrainingId))
                {
                    return this.Json(new { msg = "该手机号已经被注册！", status = false }, JsonRequestBehavior.AllowGet);
                }
                //判断学员卡是否已经报名该培训班
                if (this.systemServices.CheckCardExsit(cardid, model.TrainingId))
                {
                    return this.Json(new { msg = "该卡号已注册过该培训班！", status = false }, JsonRequestBehavior.AllowGet);
                }
                //添加  
                if (this.attendanceServices.AddSignUp(model, cardid))
                {
                    return this.Json(new { msg = "添加报名信息成功！", status = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return this.Json(new { msg = "添加报名信息失败！", status = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        /// <summary>
        /// 培训班详情
        /// </summary>
        /// <param name="id">培训班id</param>
        /// <returns></returns>
        public ActionResult TrainingDetail(int id = 0)
        {
            var model = this.systemServices.GetTrainById(id);
            if (model != null)
            {
                ViewBag.model = model;
            }
            List<TrainingTimeDto> list = new List<TrainingTimeDto>();
            var trainingTimes = this.attendanceServices.GetTraingTimes(id);
            if (trainingTimes.Count > 0)
            {
                TrainingTimeDto time = new TrainingTimeDto();
                foreach (var item in trainingTimes)
                {
                    time.TrainingDate = item;
                    time.Times = this.attendanceServices.GetTimeDetails(item, id);
                    list.Add(time);
                }
            }
            return View(list);
        }

        /// <summary>
        /// 培训班学员列表
        /// </summary>
        /// <param name="id">培训班id</param>
        /// <returns></returns>
        public ActionResult StudentList(int id = 0)
        {
            ViewBag.TrainId = id;
            var model = this.systemServices.GetTrainById(id);
            ViewBag.TrainName = "";
            if (model != null)
            {
                ViewBag.TrainName = model.Name;
            }
            return View();
        }

        public JsonResult GetStudentList([FromJson] StudentListSearch model)
        {
            var list = this.attendanceServices.GetStudentList(model);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 课时安排
        /// </summary>
        /// <param name="id">培训班id</param>
        /// <returns></returns>
        public ActionResult TrainingTimeList(int id = 0)
        {
            ViewBag.TrainId = id;
            return View();
        }
        /// <summary>
        /// 获取考勤查询日期列表
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        public JsonResult GetTrainingTimeList([FromJson]TimeListSearch model)
        {
            var list = this.attendanceServices.GetTraingDateList(model);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除考勤日期
        /// </summary>
        /// <param name="id">培训班id</param>
        /// <param name="date">考勤日期</param>
        [HttpPost]
        public JsonResult DeleteTrainDate(int id, string date)
        {
            var result = this.attendanceServices.DeleteTrainTimes(id, date);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加考勤日期
        /// </summary>
        /// <param name="id">培训班id</param>
        /// <returns></returns>
        public ActionResult AddTrainingDate(int id = 0)
        {
            ViewBag.TrainId = id;
            return View();
        }

        /// <summary>
        /// 导入考勤记录
        /// </summary>
        /// <returns></returns>
        public ActionResult ImportRecords()
        {
            var TrainingList = this.attendanceServices.GetTrainings();
            return View(TrainingList);
        }

        /// <summary>
        /// 上传考勤记录
        /// </summary>
        /// <param name="TrainId">培训班id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult TrainUpLoad(int TrainId = -1)
        {
            //验证培训班信息是否正确
            if (TrainId <= 0)
            {
                return Json(new { status = 0, msg = "导入失败，培训班为空" }, JsonRequestBehavior.AllowGet);
            }
            var train = this.attendanceServices.GetTrainById(TrainId);
            if (train == null)
            {
                return Json(new { status = 0, msg = "导入失败，培训班为空" }, JsonRequestBehavior.AllowGet);
            }
            //验证上传文件信息
            HttpPostedFileBase file = Request.Files["trainingFile"];
            if (file == null)
            {
                return Json(new { status = 0, msg = "导入失败，文件为空" }, JsonRequestBehavior.AllowGet);
            }
            var fileExtension = Path.GetExtension(file.FileName);
            if (!(fileExtension == ".xlsx" || fileExtension == ".xls"))
            {
                return Json(new { status = 0, msg = "导入失败，文件格式不正确" }, JsonRequestBehavior.AllowGet);
            }
            StringBuilder sb = new StringBuilder();
            try
            {
                //将硬盘路径转化为服务器路径的文件流
                var serverPath = this.Server.MapPath("~/SaveFile");
                if (!Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }
                string fileName = Path.Combine(Request.MapPath("~/SaveFile"), Path.GetFileName(file.FileName));
                //NPOI得到EXCEL的第一种方法              
                file.SaveAs(fileName);
                DataSet ds = ExcelHelper.Import(fileName);
                if (ds.Tables.Count > 0)
                {
                    for (int x = 0; x < ds.Tables.Count; x++)
                    {
                        DataTable dtData = ds.Tables[x];
                        if (dtData != null && dtData.Rows.Count > 0)
                        {
                            if (dtData.Columns[0].ColumnName != "卡号" || dtData.Columns[1].ColumnName != "签到时间")
                            {
                                sb.Append("第" + (x + 1).ToString() + "张Sheet表" + dtData.TableName + "导入失败，表头不正确！|");
                                continue;
                            }
                            var cardNumber = "";
                            var checkinTime = "";
                            for (int i = 0; i < dtData.Rows.Count; i++)
                            {
                                cardNumber = dtData.Rows[i][0].ToString();
                                checkinTime = dtData.Rows[i][1].ToString();
                                //验证学员卡是否报名该培训班
                                var cardid = this.systemServices.GetStudentCardId(cardNumber);
                                if (cardid > 0)
                                {
                                    if (this.systemServices.CheckCardExsit(cardid, TrainId))
                                    {

                                        try
                                        {
                                            if (!this.attendanceServices.IsExsistCheckIn(cardid, TrainId, checkinTime))
                                            {
                                                //导入成功
                                                this.attendanceServices.TrainingUpload(cardid, TrainId, checkinTime);
                                            }
                                            else
                                            {
                                                sb.Append("第" + (x + 1).ToString() + "张Sheet表" + dtData.TableName + "，第" + (i + 1).ToString() + "行导入失败,记录重复导入！|");
                                                continue;
                                            }

                                        }
                                        catch (Exception)
                                        {
                                            //导入失败
                                            sb.Append("第" + (x + 1).ToString() + "张Sheet表" + dtData.TableName + "，第" + (i + 1).ToString() + "行导入失败！|");
                                            continue;
                                        }

                                    }
                                    else
                                    {
                                        //未报名该培训班
                                        sb.Append("第" + (x + 1).ToString() + "张Sheet表" + dtData.TableName + "，第" + (i + 1).ToString() + "行导入失败，学员未报名该培训班！|");
                                        continue;
                                    }
                                }
                                else
                                {
                                    //学员卡不存在
                                    sb.Append("第" + (x + 1).ToString() + "张Sheet表" + dtData.TableName + "，第" + (i + 1).ToString() + "行导入失败，学员卡不存在！|");
                                    continue;
                                }
                                //验证通过，导入
                            }

                        }
                    }
                }

                sb = sb.Remove(sb.Length - 1, 1);
                return Json(new { status = 1, msg = "导入成功", result = sb.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                sb = sb.Remove(sb.Length - 1, 1);
                return Json(new { status = 2, msg = "导入失败", result = sb.ToString() }, JsonRequestBehavior.AllowGet);

            }



        }


        /// <summary>
        /// 考勤统计
        /// </summary>
        /// <returns></returns>
        public ActionResult AttendanceStatistics()
        {
            return View();
        }

        public JsonResult GetTrainingAjax()
        {
            var list = this.attendanceServices.GetTrainings();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取培训班最小日期
        /// </summary>
        /// <param name="trainId">培训班id</param>
        /// <returns></returns>
        public JsonResult GetMinDate(int trainId = 0)
        {
            var model = this.attendanceServices.GetTrainMinDate(trainId);
            if (model != null)
            {
                return Json(model.TrainingDateStr, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(string.Empty, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetMaxDate(int trainId = 0)
        {
            var model = this.attendanceServices.GetTrainMaxDate(trainId);
            if (model != null)
            {
                return Json(model.TrainingDateStr, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(string.Empty, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 判断查询日期是否存在
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public JsonResult IsExistsTrainDate(string date, int trainId = 0)
        {
            var result = this.attendanceServices.IsExistTrainingDate(date, trainId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddTrainingDate(string date, List<TrainingDateDto> list, int trainId = 0)
        {
            var result = false;
            if (!string.IsNullOrEmpty(date) && list.Count > 0 && trainId > 0)
            {
                result = this.attendanceServices.AddTrainingDate(date, list, trainId);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 编辑考勤日期
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public ActionResult EditTrainingDate(int id, string date)
        {
            ViewBag.TrainId = id;
            ViewBag.TrainDate = date.Substring(0, 10);
            return View();
        }

        /// <summary>
        /// 获取考勤时间段
        /// </summary>
        /// <param name="date"></param>
        /// <param name="trainId"></param>
        /// <returns></returns>
        public JsonResult GetTrainTimes(string date, int trainId)
        {
            var list = this.attendanceServices.GetTrainingTimes(date, trainId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 编辑查询时间段
        /// </summary>
        /// <param name="date">查询日期</param>
        /// <param name="list">时间段</param>
        /// <param name="trainId">培训班id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EditTrainingDate(string date, List<TrainingDateDto> list, int trainId = 0)
        {
            var result = false;
            if (!string.IsNullOrEmpty(date) && list.Count > 0 && trainId > 0)
            {
                result = this.attendanceServices.EditTrainingDate(date, list, trainId);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 考勤时间段详情
        /// </summary>
        /// <param name="id">培训班id</param>
        /// <param name="date">查询时间段</param>
        /// <returns></returns>
        public ActionResult TrainDateDetail(int id, string date)
        {
            ViewBag.TrainId = id;
            ViewBag.TrainDate = date.Substring(0, 10);
            var list = this.attendanceServices.GetTrainingTimes(date, id);
            return View(list);
        }

        /// <summary>
        /// 考勤统计查询
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public JsonResult AttendanceStaticSearch([FromJson]AttendanceStaticDto model)
        {
            var list = this.attendanceServices.GetAttendanceStaticData(model);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public FileResult AttendanceStaticDown([FromJson]AttendanceStaticDto model)
        {
            var list = this.attendanceServices.GetAttendanceStaticData(model);
            var sbHtml = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                sbHtml.Append("<table border='1' cellspacing='0' cellpadding='0'>");
                sbHtml.Append("<tr><td></td >");
                foreach (var i in list)
                {
                    sbHtml.Append("<td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25' colspan='" + i.list.Count + "'>" + i.TrainingDate.ToString("yyyy-MM-dd") + "</td>");
                }

                sbHtml.Append(" </tr><tr><td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>统计项</td>");
                foreach (var j in list)
                {
                    if (j.list != null && j.list.Count > 0)
                    {
                        foreach (var k in j.list)
                        {
                            sbHtml.Append("<td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>时间段" + (j.list.IndexOf(k) + 1) + "</td>");
                        }
                    }

                }
                sbHtml.Append(" </tr><tr><td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>应到人数</td>");

                foreach (var j in list)
                {
                    if (j.list != null && j.list.Count > 0)
                    {
                        foreach (var k in j.list)
                        {
                            sbHtml.Append("<td>" + k.DueNumber + "</td>");
                        }
                    }

                }

                sbHtml.Append(" </tr><tr><td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>实到人数</td>");
                foreach (var j in list)
                {
                    if (j.list != null && j.list.Count > 0)
                    {
                        foreach (var k in j.list)
                        {
                            sbHtml.Append("<td>" + k.AttendanceNumber + "</td>");
                        }
                    }
                }
                sbHtml.Append(" </tr><tr><td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>实到人数比例（%）</td>");
                foreach (var j in list)
                {
                    if (j.list != null && j.list.Count > 0)
                    {
                        foreach (var k in j.list)
                        {
                            sbHtml.Append("<td>" + k.AttendancePro + "</td>");
                        }
                    }
                }

                sbHtml.Append(" </tr><tr><td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>缺席人数</td>");
                foreach (var j in list)
                {
                    if (j.list != null && j.list.Count > 0)
                    {
                        foreach (var k in j.list)
                        {
                            sbHtml.Append("<td>" + k.MissingNumber + "</td>");
                        }
                    }
                }

                sbHtml.Append(" </tr><tr><td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>缺席人数比例（%）</td>");
                foreach (var j in list)
                {
                    if (j.list != null && j.list.Count > 0)
                    {
                        foreach (var k in j.list)
                        {
                            sbHtml.Append("<td>" + k.MissingPro + "</td>");
                        }
                    }
                }
                sbHtml.Append("</tr>");
            }
            byte[] fileContents = Encoding.Default.GetBytes(sbHtml.ToString());

            var fileStream = new MemoryStream(fileContents);
            return File(fileStream, "application/ms-excel", "fileStream.xls");
        }

        public ActionResult StudentAttendance()
        {
            return View();
        }

        /// <summary>
        /// 学员考勤统计查询
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public JsonResult StudentStaticSearch([FromJson]StudentStaticDto model)
        {
            var list = this.attendanceServices.GetStudentStaticData(model);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public FileResult StudentStaticDown([FromJson]StudentStaticDto model)
        {
            var list = this.attendanceServices.GetStudentStaticData(model);
            var sbHtml = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                sbHtml.Append("<table border='1' cellspacing='0' cellpadding='0'>");
                sbHtml.Append("<tr><td colspan='2'></td >");
                foreach (var m in list)
                {
                    if (m.list != null && m.list.Count > 0)
                    {
                        foreach (var n in m.list)
                        {
                            sbHtml.Append("<td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25' colspan='" + n.list.Count + "'>" + n.TrainingDate.ToString("yyyy-MM-dd") + "</td>");
                        }
                    }
                }


                sbHtml.Append("<td></td></tr><tr><td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>学员姓名</td><td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>学员卡号</td>  ");


                foreach (var x in list)
                {
                    if (x.list != null && x.list.Count > 0)
                    {
                        foreach (var y in x.list)
                        {
                            if (y.list != null && y.list.Count > 0)
                            {
                                foreach (var z in y.list)
                                {
                                    sbHtml.Append("<td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>时间段" + (y.list.IndexOf(z) + 1) + "</td>");
                                }
                            }
                        }
                    }
                }
                sbHtml.Append("<td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>考勤率</td></tr>");
                foreach (var x in list)
                {
                    sbHtml.Append("<td>" + x.StudentName + "</td><td>" + x.CardNumber + "</td>");
                    if (x.list != null && x.list.Count > 0)
                    {
                        foreach (var y in x.list)
                        {
                            if (y.list != null && y.list.Count > 0)
                            {
                                foreach (var z in y.list)
                                {
                                    sbHtml.Append("<td>" + z.IsCheckStr + "</td>");
                                }
                            }
                        }
                    }
                    sbHtml.Append("<td>" + x.Rate + "</td>");
                }

                sbHtml.Append("</tr>");
            }
            byte[] fileContents = Encoding.Default.GetBytes(sbHtml.ToString());

            var fileStream = new MemoryStream(fileContents);
            return File(fileStream, "application/ms-excel", "fileStream.xls");
        }

        /// <summary>
        /// 判断是否报名了该培训班
        /// </summary>
        /// <param name="trainId"></param>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public JsonResult IsSignUpTraining(int trainId, string cardNumber)
        {
            var cardid = this.systemServices.GetStudentCardId(cardNumber);
            if (this.systemServices.CheckCardExsit(cardid, trainId))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 获取时间段
        /// </summary>
        /// <param name="trainId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public JsonResult GetDateTimes(int trainId, string start, string end)
        {
            //先获取时间段内的所有日期
            List<TrainingTimeDto> list = null;
            var result = this.attendanceServices.GetTraingDateList(new TimeListSearch() { TrainId = trainId, StartTime = start, EndTime = end, Orderby = "CreateTime", Desc = true, PageIndex = 1, PageSize = 9999 });
            if (result != null)
            {
                if (result.List != null && result.List.Count() > 0)
                {
                    foreach (var item in result.List)
                    {
                        item.Times = this.attendanceServices.GetTrainingTimes(item.TrainingDate.ToString(), trainId);
                    }
                }
                list = result.List.ToList();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 生成二维码页面
        /// </summary>
        /// <param name="id">培训班id</param>
        /// <returns></returns>
        public ActionResult TrainingGenerate(int id = 0)
        {
            var model = this.systemServices.GetTrainById(id);
            if (model != null)
            {
                ViewBag.model = model;


                string filename = "peixunban" + id.ToString();

                string path = Server.MapPath("~/QRCode/") + filename + ".png";
                if (!Directory.Exists(Server.MapPath("~/QRCode/")))//判断文件夹是否存在 
                {
                    Directory.CreateDirectory(Server.MapPath("~/QRCode/"));//不存在则创建文件夹 
                }

                if (!System.IO.File.Exists(path))
                {
                    string content =Request.Url.Scheme+"://"+Request.Url.Authority + Url.Action("SignUpTraining", "Account", new { id = id });// Request.Url.Scheme+ Request.Url.Host + ":" + Request.Url.Port
                    MemoryStream ms = QRCodeHelper.GetQRCode(content);
                    Image img = Image.FromStream(ms);
                    img.Save(path);
                }
                ViewBag.Src = "/QRCode/" + filename + ".png";
            }
            return View(model);
        }

        /// <summary>
        /// 无考勤学员记录列表
        /// </summary>
        /// <param name="id">培训班id</param>
        /// <returns></returns>
        public ActionResult TrainingAbsentList(int id = 0)
        {
            ViewBag.TrainId = id;
            return View();
        }

        public JsonResult GetAbsentStudentList([FromJson]AbsentListSearch model)
        {
            var list = this.attendanceServices.GetAbsentStudentList(model);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


    }
}