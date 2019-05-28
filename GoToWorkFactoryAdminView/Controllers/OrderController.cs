using GoToWorkFactoryServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoToWorkFactoryAdminView.Controllers
{
    public class OrderController : Controller
    {
        private readonly IClientMainService _service;

        public OrderController(IClientMainService service)
        {
            _service = service;
        }

        // GET: Order
        public ActionResult Index()
        {
            return View(_service.GetList());
        }

        public ActionResult Details(int id)
        {
            return View(_service.GetElement(id));
        }
    }
}