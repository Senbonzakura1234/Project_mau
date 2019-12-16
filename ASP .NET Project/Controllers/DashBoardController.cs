// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.NET_Project.Models;

namespace ASP.NET_Project.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly MyDbContext _db = new MyDbContext();

        // GET: DashBoard
        [HandleError]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetBannerData()
        {
            var now = DateTime.Now;
            var firstDayCurrentMonth = new DateTime(now.Year, now.Month, 1);

            var startLastMonth = new DateTime(now.Year, now.Month - 1, 1);
            var endLastMonth = firstDayCurrentMonth.AddDays(-1);
            var startEarlier = new DateTime(now.Year, now.Month - 2, 1);
            var endEarlier = startLastMonth.AddDays(-1);

            var data = _db.Orders.Where(s => s.OrderStatus != Order.OrderStatusEnum.Deleted && 
                                             s.OrderStatus != Order.OrderStatusEnum.Cancel &&
                                             s.CreatedAt >= startLastMonth && s.CreatedAt <= endLastMonth);
            var dataEarlier = _db.Orders.Where(s => s.OrderStatus != Order.OrderStatusEnum.Deleted && 
                                             s.OrderStatus != Order.OrderStatusEnum.Cancel &&
                                             s.CreatedAt >= startEarlier && s.CreatedAt <= endEarlier);
            var customer = _db.Customers.Count();
            double lastMonth = 0;
            foreach (var item in data)
            {
                lastMonth += item.TotalPrice;
            }
            double earlierMonth = 0;
            foreach (var item in dataEarlier)
            {
                earlierMonth +=  item.TotalPrice;
            }

            double percentage;
            string detailPercent;
            if (lastMonth >= earlierMonth && earlierMonth > 0)
            {
                percentage = lastMonth * 100 / earlierMonth;
                detailPercent = "Increased";
            }
            else if(lastMonth < earlierMonth && lastMonth > 0)
            {
                percentage = earlierMonth * 100 / lastMonth;
                detailPercent = "Decreased";

            }
            else
            {
                percentage = 0;
                detailPercent = "No data";
            }
            lastMonth = (int)lastMonth;
            earlierMonth = (int)earlierMonth;
            var percentageInt = (int)percentage;
            return Json(new
            {
                lastMonth,
                earlierMonth,
                detailPercent,
                percentage,
                percentageInt, customer
            }, JsonRequestBehavior.AllowGet);
        }

        // > $40000, <= $40000 && > $10000, <= $10000
        public ActionResult GetColumnChartData()
        {
            var now = DateTime.Now;
            var firstDayCurrentMonth = new DateTime(now.Year, now.Month, 1);
            var endDay = firstDayCurrentMonth.AddDays(-1);
            var lastYear = now.AddYears(-1).Year;
            var thisYear = now.Year;
            DateTime startDay;
            if (!DateTime.IsLeapYear(thisYear) && DateTime.IsLeapYear(lastYear))
            {
                var leapDayCheck = new DateTime(thisYear, 03, 1);
                startDay = endDay < leapDayCheck ? endDay.AddDays(-366) : endDay.AddDays(-365);
            }else if (DateTime.IsLeapYear(thisYear) && !DateTime.IsLeapYear(lastYear))
            {
                var leapDayCheck = new DateTime(thisYear, 02, 29);
                startDay = endDay >= leapDayCheck ? endDay.AddDays(-366) : endDay.AddDays(-365);
            }
            else
            {
                startDay = endDay.AddDays(-365);
            }
            
            var data1 = _db.Orders.Where(s => s.OrderStatus != Order.OrderStatusEnum.Deleted &&
                                              s.OrderStatus != Order.OrderStatusEnum.Cancel && s.TotalPrice > 200 &&
                                              s.CreatedAt > startDay && s.CreatedAt <= endDay)
                .GroupBy(
                    s => new
                    {
                        s.CreatedAt.Month
                    }
                ).Select(s => new
                {
                    Date = s.FirstOrDefault().CreatedAt,
                    MonthData = s.FirstOrDefault().CreatedAt.Month,
                    Count = s.Count()
                }).OrderBy(s => s.Date).ToList();
            var data2 = _db.Orders.Where(s => s.OrderStatus != Order.OrderStatusEnum.Deleted &&
                                              s.OrderStatus != Order.OrderStatusEnum.Cancel && s.TotalPrice <= 200 && s.TotalPrice >= 50 &&
                                              s.CreatedAt > startDay && s.CreatedAt <= endDay)
                .GroupBy(
                    s => new
                    {
                        s.CreatedAt.Month
                    }
                ).Select(s => new
                {
                    Date = s.FirstOrDefault().CreatedAt,
                    MonthData = s.FirstOrDefault().CreatedAt.Month,
                    Count = s.Count()
                }).OrderBy(s => s.Date).ToList();
            var data3 = _db.Orders.Where(s => s.OrderStatus != Order.OrderStatusEnum.Deleted &&
                                              s.OrderStatus != Order.OrderStatusEnum.Cancel && s.TotalPrice < 50 &&
                                              s.CreatedAt > startDay && s.CreatedAt <= endDay)
                .GroupBy(
                    s => new
                    {
                        s.CreatedAt.Month
                    }
                ).Select(s => new
                {
                    Date = s.FirstOrDefault().CreatedAt,
                    MonthData = s.FirstOrDefault().CreatedAt.Month,
                    Count = s.Count()
                }).OrderBy(s => s.Date).ToList();
            return Json(new
            {
                data1,
                data2,
                data3
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPieChartData()
        {
            var total = _db.Orders.Count(s => s.OrderStatus != Order.OrderStatusEnum.Deleted &&
                                              s.OrderStatus != Order.OrderStatusEnum.Cancel);
            var data = _db.Orders.Where(s => s.OrderStatus != Order.OrderStatusEnum.Deleted &&
                                             s.OrderStatus != Order.OrderStatusEnum.Cancel)
                .GroupBy(
                    s => new
                    {
                        s.PaymentTypeId
                    }
                ).Select(s => new
                {
                    PaymentTypeData = s.FirstOrDefault().PaymentTypeId.ToString(),
                    Percent = s.Count() * 100 / total
                }).ToList();
            return Json(new
            {
                data
            }, JsonRequestBehavior.AllowGet);
        }
    }
}