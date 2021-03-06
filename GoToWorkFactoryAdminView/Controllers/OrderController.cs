﻿using GoToWorkFactoryServiceDAL.BindingModels;
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
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
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

        public ActionResult Delete(int id)
        {
            _service.DelElement(id);
            return RedirectToAction("Index");
        }

        public ActionResult Finish(int id)
        {
            _service.FinishOrder(new OrderBindingModel { Id = id });
            return RedirectToAction("Index");
        }
    }
}