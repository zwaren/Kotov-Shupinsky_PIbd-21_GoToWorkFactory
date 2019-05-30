using GoToWorkFactoryServiceDAL.BindingModels;
using GoToWorkFactoryServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GoToWorkFactoryAdminView.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly IMaterialService _matService;

        public ProductController(IProductService service, IMaterialService matService)
        {
            _service = service;
            _matService = matService;
        }

        // GET: Product
        public ActionResult Index()
        {
            return View(_service.GetList());
        }

        public ActionResult Details(int id)
        {
            return View(_service.GetElement(id));
        }

        public ActionResult Create()
        {
            if (Session["Product"] == null)
            {
                var product = new ProductBindingModel();
                product.ProductMaterials = new List<ProductMaterialBindingModel>();
                Session["Product"] = product;
            }
            return View((ProductBindingModel) Session["Product"]);
        }

        [HttpPost]
        public ActionResult CreatePost()
        {
            var product = (ProductBindingModel) Session["Product"];

            product.Name = Request["Name"];
            product.Price = Convert.ToDecimal(Request["Price"]);

            _service.AddElement(product);

            Session.Remove("Product");

            return RedirectToAction("Index");
        }

        public ActionResult AddMaterial()
        {
            var materials = new SelectList(_matService.GetList(), "Id", "Name");
            ViewBag.Materials = materials;
            return View();
        }

        [HttpPost]
        public ActionResult AddMaterialPost()
        {
            var product = (ProductBindingModel)Session["Product"];
            var material = new ProductMaterialBindingModel
            {
                ProductId = product.Id,
                MaterialId = int.Parse(Request["MaterialId"]),
                MaterialName = _matService.GetElement(int.Parse(Request["MaterialId"])).Name,
                Count = int.Parse(Request["Count"])
            };
            product.ProductMaterials.Add(material);
            Session["Product"] = product;
            return RedirectToAction("Create");
        }

        public ActionResult Edit(int id)
        {
            var viewModel = _service.GetElement(id);
            var bindingModel = new ProductBindingModel
            {
                Id = id,
                Name = viewModel.Name
            };
            return View(bindingModel);
        }

        [HttpPost]
        public ActionResult EditPost()
        {
            _service.UpdElement(new ProductBindingModel
            {
                Id = int.Parse(Request["Id"]),
                Name = Request["ProductName"]
            });
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _service.DelElement(id);
            return RedirectToAction("Index");
        }
    }
}