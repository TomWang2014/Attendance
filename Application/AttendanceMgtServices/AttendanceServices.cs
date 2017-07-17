using Attendance.Application.AttendanceMgtServices.Dtos;
using Attendance.Domain.Trainings;
using Attendance.Infrastructure.Unity.Aop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Attendance.Domain;
using Attendance.Infrastructure.Data;
using Attendance.Infrastructure.Data.Specification;
using Attendance.Infrastructure.Entity;
using Attendance.Infrastructure.AutoMapper;
using System.Security.Cryptography;

namespace Attendance.Application.AttendanceMgtServices
{

    /// <summary>
    /// 考勤系统后台管理服务
    /// </summary>
    public class AttendanceServices : InterceptiveObject
    {
        /// <summary>
        /// 培训班仓储
        /// </summary>
        private readonly ITrainingRepository trainingRepository;
        private readonly ITrainingSignUpRepository trainingSignUpRepository;

        private readonly ITrainingTimesRepository trainingTimesRepository;

        private readonly ITrainingCheckinRepository trainingCheckinRepository;

        private readonly ITrainingCardRepository trainingCardRepository;
        /// <summary>
        /// 数据库工作单元
        /// </summary>
        private readonly IUnitOfWork unitOfWork;
        public AttendanceServices(IUnitOfWork _unitOfWork, ITrainingRepository _trainingRepository, ITrainingSignUpRepository _trainingSignUpRepository, ITrainingTimesRepository _trainingTimesRepository, ITrainingCheckinRepository _trainingCheckinRepository, ITrainingCardRepository _trainingCardRepository)
        {
            this.unitOfWork = _unitOfWork;
            this.trainingRepository = _trainingRepository;
            this.trainingSignUpRepository = _trainingSignUpRepository;
            this.trainingTimesRepository = _trainingTimesRepository;
            this.trainingCheckinRepository = _trainingCheckinRepository;
            this.trainingCardRepository = _trainingCardRepository;
        }



        /// <summary>
        /// 获取结合列表
        /// </summary>
        /// <param name="model">搜索实体</param>
        /// <returns></returns>

        public PagedResult<TrainListItem> GetTrainingList(TrainListSearch model)
        {
            Expression<Func<Training, bool>> filter = p => 1 == 1;

            if (!string.IsNullOrEmpty(model.Name))
            {
                filter = filter.And(p => p.Name.Contains(model.Name.Trim()));
            }

            if (!string.IsNullOrEmpty(model.Start))
            {
                var start = Convert.ToDateTime(model.Start);
                filter = filter.And(p => p.CreateTime >= start);
            }
            if (!string.IsNullOrEmpty(model.End))
            {
                var end = Convert.ToDateTime(model.End);
                filter = filter.And(p => p.CreateTime <= end);
            }

            var list = this.trainingRepository.GetFilteredPageResult<Training>(
                filter,
                model.PageIndex,
                model.PageSize,
                model.Orderby,
                model.Desc);

            return list.MapTo<PagedResult<TrainListItem>>();
        }

        /// <summary>
        /// 添加培训班
        /// </summary>
        /// <param name="model"></param>
        public void AddTraining(TrainListItem model)
        {
            var train = model.MapTo<Training>();
            this.trainingRepository.Add(train);
            this.unitOfWork.Commit();
        }

        /// <summary>
        /// 修改培训班
        /// </summary>
        /// <param name="model"></param>
        public void ModifyTraining(TrainListItem model)
        {
            var train = model.MapTo<Training>();
            this.trainingRepository.Modify(train);
            this.unitOfWork.Commit();
        }

        /// <summary>
        /// 获取学员列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedResult<StudentListItem> GetStudentList(StudentListSearch model)
        {
            Expression<Func<TrainingSignUp, bool>> filter = p => p.TrainingId == model.TrainId;

            if (!string.IsNullOrEmpty(model.CardNumber))
            {
                filter = filter.And(p => p.TrainingCard.CardNumber.Contains(model.CardNumber));
            }
            if (!string.IsNullOrEmpty(model.StudentPhone))
            {
                filter = filter.And(p => p.StudentPhone.Contains(model.StudentPhone));
            }
            if (!string.IsNullOrEmpty(model.StartTime))
            {
                var start = Convert.ToDateTime(model.StartTime);
                filter = filter.And(p => p.CreateTime >= start);
            }
            if (!string.IsNullOrEmpty(model.EndTime))
            {
                var end = Convert.ToDateTime(model.EndTime);
                filter = filter.And(p => p.CreateTime <= end);
            }
            var list = this.trainingSignUpRepository.GetFilteredPageResult<TrainingSignUp>(
                filter,
                model.PageIndex,
                model.PageSize,
                model.Orderby,
                model.Desc);

            return list.MapTo<PagedResult<StudentListItem>>();
        }

        /// <summary>
        /// 修改报名信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool modifySignUp(TrainingSignUpItem model, int cardid)
        {
            bool result = false;
            try
            {
                var signUp = this.trainingSignUpRepository.GetModel(model.Id);
                if (signUp != null)
                {
                    signUp.CardId = cardid;
                    signUp.StudentName = model.StudentName;
                    signUp.StudentPhone = model.StudentPhone;
                    this.trainingSignUpRepository.Modify(signUp);
                    this.unitOfWork.Commit();
                    result = true;
                }

            }
            catch (Exception)
            {

                result = false;
            }
            return result;
        }

        /// <summary>
        /// 获取报名实体
        /// </summary>
        /// <param name="cardid"></param>
        /// <param name="trainId"></param>
        /// <returns></returns>
        public TrainingSignUpDto GetTrainingSignUp(int cardid, int trainId)
        {
            var model = this.trainingSignUpRepository.GetFilteredElements(p => p.CardId == cardid && p.TrainingId == trainId).FirstOrDefault();
            if (model != null)
            {
                return model.MapTo<TrainingSignUpDto>();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 添加报名
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trainId"></param>
        public bool AddSignUp(TrainingSignUpItem model, int cardid)
        {
            bool result = false;
            try
            {
                var signUp = new TrainingSignUp();
                signUp.TrainingId = model.TrainingId;
                signUp.CardId = cardid;
                signUp.StudentName = model.StudentName;
                signUp.StudentPhone = model.StudentPhone;
                signUp.CreateTime = DateTime.Now;
                this.trainingSignUpRepository.Add(signUp);
                this.unitOfWork.Commit();
                result = true;

            }
            catch (Exception)
            {

                result = false;
            }
            return result;
        }

        public PagedResult<TrainingTimeDto> GetTraingDateList(TimeListSearch search)
        {
            Expression<Func<TrainingTimes, bool>> filter = p => 1 == 1;

            if (search.TrainId > 0)
            {
                filter = filter.And(p => p.TrainingId == search.TrainId);
            }
            if (!string.IsNullOrEmpty(search.StartTime))
            {
                var start = Convert.ToDateTime(search.StartTime);
                filter = filter.And(p => p.TrainingDate >= start);
            }
            if (!string.IsNullOrEmpty(search.EndTime))
            {
                var end = Convert.ToDateTime(search.EndTime);
                filter = filter.And(p => p.TrainingDate <= end);
            }

            var list = this.trainingTimesRepository.GetFilteredElements(filter).GroupBy(p => new { p.TrainingDate }).Select(g => new TrainingTimes { TrainingDate = g.Key.TrainingDate });
            var pagelist = list.Skip((search.PageIndex - 1) * search.PageSize).Take(search.PageSize).ToList();


            var pagedResultList = new PagedResult<TrainingTimes>(search.PageIndex, search.PageSize, list.Count(), pagelist);
            return pagedResultList.MapTo<PagedResult<TrainingTimeDto>>();
        }

        public List<TrainListItem> GetTrainings()
        {
            var list = this.trainingRepository.GetAll();
            if (list != null && list.Count() > 0)
            {
                return list.MapTo<List<TrainListItem>>();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取培训班实体
        /// </summary>
        /// <param name="trainId"></param>
        /// <returns></returns>
        public TrainListItem GetTrainById(int trainId)
        {
            var model = this.trainingRepository.GetModel(trainId);
            if (model != null)
            {
                return model.MapTo<TrainListItem>();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除考勤日期
        /// </summary>
        /// <param name="id">培训班id</param>
        /// <param name="date">考勤日期</param>
        public bool DeleteTrainTimes(int id, string date)
        {
            bool result = false;
            try
            {
                var trainDate = Convert.ToDateTime(date);
                var list = this.trainingTimesRepository.GetFilteredElements(p => p.TrainingId == id && p.TrainingDate == trainDate);
                if (list != null && list.Count() > 0)
                {
                    foreach (var item in list)
                    {
                        this.trainingTimesRepository.Delete(item);
                    }
                    this.unitOfWork.Commit();
                    result = true;
                }
            }
            catch (Exception)
            {
                return result;
            }
            return result;

        }

        /// <summary>
        /// 获取培训班的课时安排日期
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<DateTime> GetTraingTimes(int id)
        {
            var list = this.trainingTimesRepository.GetFilteredElements(p => p.TrainingId == id).GroupBy(p => new { p.TrainingDate }).Select(g => g.Key.TrainingDate);
            return list.MapTo<List<DateTime>>();
        }

        /// <summary>
        /// 导入签到记录
        /// </summary>
        /// <param name="cardid">学员卡id</param>
        /// <param name="trainId">培训班id</param>
        /// <param name="checkinTime">签到时间</param>
        public void TrainingUpload(int cardid, int trainId, string checkinTime)
        {
            TrainingCheckin model = new TrainingCheckin()
            {
                CardId = cardid,
                TrainingId = trainId,
                CheckInTime = Convert.ToDateTime(checkinTime),
                CreateTime = DateTime.Now,
                CheckCode = GetCheckCode(cardid.ToString() + trainId.ToString() + checkinTime)
            };
            this.trainingCheckinRepository.Add(model);
            this.unitOfWork.Commit();
        }

        /// <summary>
        /// 判断是不是已经导入数据库
        /// </summary>
        /// <param name="cardid"></param>
        /// <param name="trainId"></param>
        /// <param name="checkinTime"></param>
        /// <returns></returns>
        public bool IsExsistCheckIn(int cardid, int trainId, string checkinTime)
        {
            var checkCode = GetCheckCode(cardid.ToString() + trainId.ToString() + checkinTime);
            var model = this.trainingCheckinRepository.GetFilteredElements(p => p.CheckCode == checkCode).FirstOrDefault();
            if (model == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="checkStr"></param>
        /// <returns></returns>
        private string GetCheckCode(string checkStr)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(checkStr));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public List<TrainingTimeDetails> GetTimeDetails(DateTime dateTime, int id)
        {
            var list = this.trainingTimesRepository.GetFilteredElements(p => p.TrainingDate == dateTime && p.TrainingId == id);
            return list.MapTo<List<TrainingTimeDetails>>();
        }

        /// <summary>
        /// 获取培训班考勤查询时间最小日期
        /// </summary>
        /// <param name="trainId">培训班id</param>
        /// <returns></returns>
        public TrainingTimeDto GetTrainMinDate(int trainId)
        {
            if (trainId > 0)
            {
                var model = this.trainingTimesRepository.GetFilteredElements(p => p.TrainingId == trainId).OrderBy(p => p.TrainingDate).FirstOrDefault();
                if (model != null)
                {
                    return model.MapTo<TrainingTimeDto>();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public TrainingTimeDto GetTrainMaxDate(int trainId)
        {
            if (trainId > 0)
            {
                var model = this.trainingTimesRepository.GetFilteredElements(p => p.TrainingId == trainId).OrderByDescending(p => p.TrainingDate).FirstOrDefault();
                if (model != null)
                {
                    return model.MapTo<TrainingTimeDto>();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 判断是否已经存在考勤日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool IsExistTrainingDate(string date, int trainId)
        {
            var trainDate = Convert.ToDateTime(date);
            var model = this.trainingTimesRepository.GetFilteredElements(p => p.TrainingId == trainId && p.TrainingDate == trainDate).FirstOrDefault();
            if (model != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 添加考勤时间段
        /// </summary>
        /// <param name="date"></param>
        /// <param name="list"></param>
        /// <param name="trainId"></param>
        /// <returns></returns>
        public bool AddTrainingDate(string date, List<TrainingDateDto> list, int trainId)
        {
            try
            {

                foreach (var item in list)
                {
                    var trainingTimes = new TrainingTimes();
                    trainingTimes.TrainingId = trainId;
                    trainingTimes.TrainingDate = Convert.ToDateTime(date);
                    trainingTimes.StartTime = Convert.ToDateTime(item.start);
                    trainingTimes.EndTime = Convert.ToDateTime(item.end);
                    trainingTimes.Type = 0;
                    trainingTimes.CreateTime = DateTime.Now;
                    this.trainingTimesRepository.Add(trainingTimes);
                }
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        /// <summary>
        /// 获取考勤查询日期列表
        /// </summary>
        /// <param name="date">考勤日期</param>
        /// <param name="trainId">培训班id</param>
        /// <returns></returns>
        public List<TrainingTimeDetails> GetTrainingTimes(string date, int trainId)
        {
            try
            {
                var trainDate = Convert.ToDateTime(date);
                var list = this.trainingTimesRepository.GetFilteredElements(p => p.TrainingDate == trainDate && p.TrainingId == trainId);
                if (list != null)
                {
                    return list.MapTo<List<TrainingTimeDetails>>();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }

        }

        public bool EditTrainingDate(string date, List<TrainingDateDto> list, int trainId)
        {
            try
            {
                //先删除当前查询日期的查询时间段
                var trainDate = Convert.ToDateTime(date);
                var delList = this.trainingTimesRepository.GetFilteredElements(p => p.TrainingDate == trainDate && p.TrainingId == trainId);
                if (delList != null)
                {
                    foreach (var item in delList)
                    {
                        this.trainingTimesRepository.Remove(item);
                    }
                    this.unitOfWork.Commit();
                }

                //添加新的查询时间段

                foreach (var item in list)
                {
                    var trainingTimes = new TrainingTimes();
                    trainingTimes.TrainingId = trainId;
                    trainingTimes.TrainingDate = Convert.ToDateTime(date);
                    trainingTimes.StartTime = Convert.ToDateTime(item.start);
                    trainingTimes.EndTime = Convert.ToDateTime(item.end);
                    trainingTimes.Type = 0;
                    trainingTimes.CreateTime = DateTime.Now;
                    this.trainingTimesRepository.Add(trainingTimes);
                }
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        /// <summary>
        /// 获取学员考勤查询结果
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public List<StudentResultDto> GetStudentStaticData(StudentStaticDto model)
        {
            List<StudentResultDto> list = new List<StudentResultDto>();
            try
            {
                var trainId = model.TrainId;
                var cardNumber = model.CardNumber;
                var minRate = model.MinRate;
                var maxRate = model.MaxRate;
                var timeList = model.TimeList;
                var studentName = "";
                List<StudentCheckInDto> checkinList = null;
                if (trainId > 0 && timeList.Count > 0)
                {
                    //查询条件中的卡号不为空（非模糊查询）
                    if (!string.IsNullOrEmpty(cardNumber))
                    {
                        var trainCard = this.trainingCardRepository.GetFilteredElements(p => p.CardNumber == cardNumber.Trim()).FirstOrDefault();
                        if (trainCard != null)
                        {
                            var signIn = this.trainingSignUpRepository.GetFilteredElements(p => p.TrainingId == trainId && p.CardId == trainCard.Id).FirstOrDefault();
                            if (signIn != null)
                            {
                                studentName = signIn.StudentName;
                            }
                        }
                        checkinList = GetCheckList(timeList, trainCard.Id, trainId);
                        list.Add(new StudentResultDto() { StudentName = studentName, CardNumber = cardNumber, list = checkinList });
                    }
                    else
                    {
                        //查询所有报名了该培训班的学员
                        var studentlist = this.trainingSignUpRepository.GetFilteredElements(p => p.TrainingId == trainId);
                        if (studentlist != null)
                        {
                            studentlist = studentlist.Distinct(new TrainSignUpNoComparer());//去重
                                                                                            //遍历
                            foreach (var item in studentlist)
                            {
                                cardNumber = item.TrainingCard.CardNumber;
                                studentName = item.StudentName;
                                checkinList = GetCheckList(timeList, item.CardId, trainId);
                                list.Add(new StudentResultDto() { StudentName = studentName, CardNumber = cardNumber, list = checkinList });
                            }
                        }
                    }
                }
                if (list != null && list.Count > 0)
                {
                    list = list.Where(p => p.Rate >= minRate && p.Rate <= maxRate).ToList();
                }
            }
            catch (Exception)
            {
                return list;
            }

            return list;
        }

        /// <summary>
        /// 获取签到列表
        /// </summary>
        /// <param name="timeList">查询时间段</param>
        /// <param name="cardId">卡号</param>
        /// <param name="trainId">培训班id</param>
        /// <returns></returns>
        private List<StudentCheckInDto> GetCheckList(List<TimeList> timeList, int cardId, int trainId)
        {
            List<StudentCheckInDto> list = new List<StudentCheckInDto>();
            try
            {
                if (timeList != null && timeList.Count > 0 && cardId > 0 && trainId > 0)
                {
                    foreach (var item in timeList)
                    {
                        var trainDate = item.date;//培训日期
                        var trainTimeList = item.list;//时间段s
                        if (!string.IsNullOrEmpty(trainDate) && trainTimeList != null && trainTimeList.Count > 0)
                        {
                            List<StudentStaticDetail> StaticDetailList = new List<StudentStaticDetail>();
                            foreach (var time in trainTimeList)
                            {
                                var start = Convert.ToDateTime(time.start);//时间段-开始
                                var end = Convert.ToDateTime(time.end);//时间段-结束
                                var isCheckIn = false;
                                var checkInList = this.trainingCheckinRepository.GetFilteredElements(p => p.CardId == cardId && p.TrainingId == trainId && p.CheckInTime >= start && p.CheckInTime <= end);
                                if (checkInList != null && checkInList.Count() > 0)
                                {
                                    isCheckIn = true;
                                }
                                StaticDetailList.Add(new StudentStaticDetail() { StartTime = start, EndTime = end, IsCheckIn = isCheckIn });

                            }

                            list.Add(new StudentCheckInDto() { TrainingDate = Convert.ToDateTime(trainDate), list = StaticDetailList });
                        }
                    }
                }
            }
            catch (Exception)
            {
                return list;
            }
            return list;

        }

        /// <summary>
        /// 获取考勤统计数据
        /// </summary>
        /// <param name="model">查询条件</param>
        /// <returns></returns>
        public List<AttendanceResultDto> GetAttendanceStaticData(AttendanceStaticDto model)
        {
            List<AttendanceResultDto> list = null;
            var trainId = model.TrainId;
            var timeList = model.TimeList;
            if (trainId > 0 && timeList.Count > 0)
            {
                try
                {
                    list = new List<AttendanceResultDto>();
                    //先获取该培训班的报名人数，即应到人数
                    var dueNumber = this.trainingSignUpRepository.GetFilteredElements(p => p.TrainingId == trainId).Count();
                    if (dueNumber > 0)
                    {
                        foreach (var item in timeList)
                        {
                            List<StaticDetail> StaticDetailList = new List<StaticDetail>();
                            var trainDate = item.date;//培训日期
                            var trainTimeList = item.list;//时间段s
                            if (!string.IsNullOrEmpty(trainDate) && trainTimeList != null && trainTimeList.Count > 0)
                            {
                                foreach (var time in trainTimeList)
                                {
                                    var start = Convert.ToDateTime(time.start);//时间段-开始
                                    var end = Convert.ToDateTime(time.end);//时间段-结束
                                                                           //查询该时间段的签到人数即实到人数(去除重复打卡)
                                    var attendaceNumber = this.trainingCheckinRepository.GetFilteredElements(p => p.TrainingId == trainId && p.CheckInTime >= start && p.CheckInTime <= end).Select(p => p.CardId).Distinct().Count();
                                    StaticDetail detail = new StaticDetail()
                                    {
                                        StartTime = start,
                                        EndTime = end,
                                        DueNumber = dueNumber,
                                        AttendanceNumber = attendaceNumber
                                    };
                                    StaticDetailList.Add(detail);

                                }
                                list.Add(new AttendanceResultDto() { TrainingDate = Convert.ToDateTime(trainDate), list = StaticDetailList });
                            }
                        }
                    }


                }
                catch (Exception)
                {
                    list = null;

                }
            }
            return list;

        }

        /// <summary>
        /// 获取无考勤记录学员列表
        /// </summary>
        /// <param name="id">培训班id</param>
        /// <returns></returns>
        public PagedResult<StudentListItem> GetAbsentStudentList(AbsentListSearch model)
        {

            Expression<Func<TrainingSignUp, bool>> filter = p => p.TrainingId == model.TrainId;

            if (!string.IsNullOrEmpty(model.CardNumber))
            {
                filter = filter.And(p => p.TrainingCard.CardNumber.Contains(model.CardNumber));
            }

            if (!string.IsNullOrEmpty(model.StudentPhone))
            {
                filter = filter.And(p => p.StudentPhone.Contains(model.StudentPhone));
            }

            var list = this.trainingSignUpRepository.GetFilteredElements(filter);
            var temp = new List<TrainingSignUp>();
            if (list != null && list.Count() > 0)
            {
                foreach (var item in list)
                {
                    var checklist = this.trainingCheckinRepository.GetFilteredElements(p => p.TrainingId == item.TrainingId && p.CardId == item.CardId);
                    if (checklist != null && checklist.Count() > 0)
                    {
                        continue;
                    }
                    else
                    {
                        temp.Add(item);
                    }
                }
            }
            var pagelist = temp.Skip((model.PageIndex - 1) * model.PageSize).Take(model.PageSize).ToList();

            var pagedResultList = new PagedResult<TrainingSignUp>(model.PageIndex, model.PageSize, temp.Count, pagelist);

            return pagedResultList.MapTo<PagedResult<StudentListItem>>();


        }
    }
}
