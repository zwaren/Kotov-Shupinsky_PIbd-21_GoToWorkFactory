using GoToWorkFactoryModel;
using GoToWorkFactoryServiceDAL.BindingModels;
using GoToWorkFactoryServiceDAL.Interfaces;
using GoToWorkFactoryServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToWorkFactoryImplementDataBase.Implementations
{
    public class OrderServiceDB : IOrderService
    {
        private FactoryDbContext context;

        public OrderServiceDB(FactoryDbContext context)
        {
            this.context = context;
        }

        public void FinishOrder(OrderBindingModel model)
        {
            Order o = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (o == null)
            {
                throw new Exception("Элемент не найден");
            }
            var ms = new Dictionary<Material, int>();
            if (!o.Reserved)
            {
                ms = totalMaterials(o);
                foreach (var m in ms)
                    if (context.Materials.First(e => e.Id == m.Key.Id).Count < m.Value)
                        throw new Exception("Не достаточно матералов на складе");
            }

            o.Status = OrderStatus.Готов;
            if (!o.Reserved) decMaterials(ms);
            context.SaveChanges();
        }

        private void decMaterials(Dictionary<Material, int> ms)
        {
            foreach (var m in ms)
                m.Key.Count -= m.Value;
        }

        private Dictionary<Material, int> totalMaterials(Order order)
        {
            var ms = new Dictionary<Material, int>();
            foreach (var op in context.OrderProducts.Where(op => op.OrderId == order.Id))
            {
                var p = context.Products.FirstOrDefault(x => x.Id == op.ProductId);
                foreach (var pm in context.ProductMaterials.Where(pm => pm.ProductId == p.Id))
                {
                    var m = context.Materials.FirstOrDefault(x => x.Id == pm.MaterialId);
                    if (!ms.ContainsKey(m)) ms.Add(m, 0);
                    ms[m] += pm.Count * op.Count;
                }
            }
            return ms;
        }

        private void decMaterials(Order o)
        {
            foreach (var op in context.OrderProducts.Where(op => op.OrderId == o.Id))
            {
                var p = context.Products.FirstOrDefault(x => x.Id == op.ProductId);
                foreach (var pm in p.ProductMaterials)
                    pm.Material.Count -= pm.Count;
            }
            context.SaveChanges();
        }

        public List<OrderViewModel> GetList()
        {
            List<OrderViewModel> result = context.Orders.Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                ClientId = rec.ClientId,
                DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                        SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                        SqlFunctions.DateName("yyyy", rec.DateCreate),
                DateImplement = rec.DateImplement == null ? "" :
                        SqlFunctions.DateName("dd", rec.DateImplement.Value) + " " +
                        SqlFunctions.DateName("mm", rec.DateImplement.Value) + " " +
                        SqlFunctions.DateName("yyyy", rec.DateImplement.Value),
                Status = rec.Status.ToString(),
                Sum = rec.Sum,
                ClientName = rec.Client.Name,
                Reserved = rec.Reserved
            })
            .ToList();
            foreach (var o in result)
            {
                o.OrderProducts = context.OrderProducts.Where(op => op.OrderId == o.Id).Select(op => new OrderProductViewModel
                {
                    Id = op.Id,
                    OrderId = op.Order.Id,
                    ProductId = op.Product.Id,
                    ProductName = op.Product.Name,
                    Count = op.Count
                }
                ).ToList();
            }

            return result;
        }

        public OrderViewModel GetElement(int id)
        {
            var o = context.Orders.Where(e => e.Id == id).Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                ClientId = rec.ClientId,
                DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                        SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                        SqlFunctions.DateName("yyyy", rec.DateCreate),
                DateImplement = rec.DateImplement == null ? "" :
                        SqlFunctions.DateName("dd", rec.DateImplement.Value) + " " +
                        SqlFunctions.DateName("mm", rec.DateImplement.Value) + " " +
                        SqlFunctions.DateName("yyyy", rec.DateImplement.Value),
                Status = rec.Status.ToString(),
                Sum = rec.Sum,
                ClientName = rec.Client.Name,
                Reserved = rec.Reserved,
                OrderProducts = context.OrderProducts
                .Where(e => e.OrderId == id)
                .Select(op => new OrderProductViewModel
                {
                    Id = op.Id,
                    OrderId = op.Order.Id,
                    ProductId = op.Product.Id,
                    ProductName = op.Product.Name,
                    Count = op.Count
                }).ToList()

            });
            return o.First();
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Order element = context.Orders.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.OrderProducts.RemoveRange(context.OrderProducts.Where(rec => rec.OrderId == id));
                        context.Orders.Remove(element);
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
