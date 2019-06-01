using GoToWorkFactoryServiceDAL.BindingModels;
using GoToWorkFactoryServiceDAL.Interfaces;
using System;
using System.Web.Mvc;

namespace GoToWorkFactoryAdminView.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _service;
        private readonly IBackUpService _backUpService;

        public ReportController(IReportService service, IBackUpService backUpService)
        {
            _service = service;
            _backUpService = backUpService;
        }

        // GET: Report
        public ActionResult AdminOrderList()
        {
            _service.getAdminOrderList(new ReportBindingModel
            {
                Email = "deviler.san@yandex.ru",
                FileName = @"D:\test.pdf",
                DateFrom = new DateTime(2018, 1, 1),
                DateTo = DateTime.Now
            });
            return RedirectToAction("Index", "Order");
        }

        public ActionResult MaterialRequest()
        {
            _service.createMaterialRequest(new ReportBindingModel
            {
                Email = "deviler.san@yandex.ru",
                FileName = @"D:\test.docx",
                DateFrom = new DateTime(2018, 1, 1),
                DateTo = DateTime.Now
            });
            return RedirectToAction("Index", "Order");
        }

        public ActionResult BackUpAdmin()
        {
            _backUpService.BackUpAdmin();
            return RedirectToAction("Index", "Order");
        }
    }
}