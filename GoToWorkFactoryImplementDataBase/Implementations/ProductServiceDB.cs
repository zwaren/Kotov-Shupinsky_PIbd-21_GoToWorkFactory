using GoToWorkFactoryModel;
using GoToWorkFactoryServiceDAL.BindingModels;
using GoToWorkFactoryServiceDAL.Interfaces;
using GoToWorkFactoryServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoToWorkFactoryImplementDataBase.Implementations
{
    public class ProductServiceDB : IProductService
    {
        private FactoryDbContext context;

        public ProductServiceDB(FactoryDbContext context)
        {
            this.context = context;
        }
        public List<ProductViewModel> GetList()
        {
            List<ProductViewModel> result = context.Products.Select(rec => new ProductViewModel
            {
                Id = rec.Id,
                Name = rec.Name,
                Price = rec.Price,
                ProductMaterials = context.ProductMaterials
                    .Where(recPC => recPC.ProductId == rec.Id)
                    .Select(recPC => new ProductMaterialViewModel
                    {
                        Id = recPC.Id,
                        ProductId = recPC.ProductId,
                        MaterialId = recPC.MaterialId,
                        MaterialName = recPC.Material.Name,
                        Count = recPC.Count
                    })
                    .ToList()
            })
            .ToList();
            return result;
        }

        public List<ProductViewModel> GetFilteredList()
        {
            var result = context.Products
                .Where(productAllowed)
                .Select(rec => new ProductViewModel
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    Price = rec.Price
                })
                .ToList();
            foreach (var x in result)
                x.ProductMaterials = context.ProductMaterials
                        .Where(recPC => recPC.ProductId == x.Id)
                        .Select(recPC => new ProductMaterialViewModel
                        {
                            Id = recPC.Id,
                            ProductId = recPC.ProductId,
                            MaterialId = recPC.MaterialId,
                            MaterialName = recPC.Material.Name,
                            Count = recPC.Count
                        })
                        .ToList();
            return result;
        }

        private bool productAllowed(Product p)
        {
            foreach (var pm in context.ProductMaterials.Where(x => x.ProductId == p.Id))
                if (context.Materials.First(m => m.Id == pm.MaterialId).Count < pm.Count) return false;
            return true;
        }

        public ProductViewModel GetElement(int id)
        {
            Product element = context.Products.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ProductViewModel
                {
                    Id = element.Id,
                    Name = element.Name,
                    Price = element.Price,
                    ProductMaterials = context.ProductMaterials
                        .Where(recPC => recPC.ProductId == element.Id)
                        .Select(recPC => new ProductMaterialViewModel
                        {
                            Id = recPC.Id,
                            ProductId = recPC.ProductId,
                            MaterialId = recPC.MaterialId,
                            MaterialName = recPC.Material.Name,
                            Count = recPC.Count
                        })
                        .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ProductBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Product element = context.Products.FirstOrDefault(rec =>
                        rec.Name == model.Name);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.Products.Add(new Product
                    {
                        Name = model.Name,
                        Price = model.Price
                    });
                    context.SaveChanges();
                    // убираем дубли по компонентам
                    var groupMaterials = model.ProductMaterials
                        .GroupBy(rec => rec.MaterialId)
                        .Select(rec => new
                        {
                            MaterialId = rec.Key,
                            Count = rec.Sum(r => r.Count)
                        });
                    // добавляем компоненты
                    foreach (var groupMaterial in groupMaterials)
                    {
                        context.ProductMaterials.Add(new ProductMaterial
                        {
                            ProductId = element.Id,
                            MaterialId = groupMaterial.MaterialId,
                            Count = groupMaterial.Count
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void UpdElement(ProductBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Product element = context.Products.FirstOrDefault(rec =>
                        rec.Name == model.Name && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.Products.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.Name = model.Name;
                    element.Price = model.Price;
                    context.SaveChanges();
                    // обновляем существуюущие компоненты
                    var compIds = model.ProductMaterials.Select(rec =>
                        rec.MaterialId).Distinct();
                    var updateMaterials = context.ProductMaterials.Where(rec =>
                        rec.ProductId == model.Id && compIds.Contains(rec.MaterialId));
                    foreach (var updateMaterial in updateMaterials)
                    {
                        updateMaterial.Count =
                        model.ProductMaterials.FirstOrDefault(rec => rec.Id == updateMaterial.Id).Count;
                    }
                    context.SaveChanges();
                    context.ProductMaterials.RemoveRange(context.ProductMaterials.Where(rec =>
                        rec.ProductId == model.Id && !compIds.Contains(rec.MaterialId)));
                    context.SaveChanges();
                    // новые записи
                    var groupMaterials = model.ProductMaterials
                        .Where(rec => rec.Id == 0)
                        .GroupBy(rec => rec.MaterialId)
                        .Select(rec => new
                        {
                            MaterialId = rec.Key,
                            Count = rec.Sum(r => r.Count)
                        });
                    foreach (var groupMaterial in groupMaterials)
                    {
                        ProductMaterial elementPC = context.ProductMaterials
                            .FirstOrDefault(rec => rec.ProductId == model.Id 
                                            && rec.MaterialId == groupMaterial.MaterialId);
                        if (elementPC != null)
                        {
                            elementPC.Count += groupMaterial.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.ProductMaterials.Add(new ProductMaterial
                            {
                                ProductId = model.Id,
                                MaterialId = groupMaterial.MaterialId,
                                Count = groupMaterial.Count
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Product element = context.Products.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.ProductMaterials.RemoveRange(context.ProductMaterials.Where(rec =>
                            rec.ProductId == id));
                        context.Products.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}