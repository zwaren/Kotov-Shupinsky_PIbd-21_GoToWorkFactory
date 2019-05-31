using GoToWorkFactoryModel;
using GoToWorkFactoryServiceDAL.BindingModels;
using GoToWorkFactoryServiceDAL.Interfaces;
using GoToWorkFactoryServiceDAL.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace GoToWorkFactoryImplementDataBase.Implementations
{
    public class ClientMainServiceDB : IClientMainService
    {
        private FactoryDbContext context;

        public ClientMainServiceDB(FactoryDbContext context)
        {
            this.context = context;
        }

        public void CreateOrder(OrderBindingModel model)
        {
            foreach (var m in totalMaterials(model))
                if (context.Materials.First(e => e.Id == m.Key.Id).Count < m.Value && model.Reserved)
                    throw new Exception("Не достаточно матералов на складе");

            var o = context.Orders.Add(new Order
            {
                ClientId = model.ClientId,
                DateCreate = DateTime.Now,
                Reserved = model.Reserved,
                Sum = model.Sum,
                Status = OrderStatus.Принят
            });
            context.SaveChanges();

            int curOrderId = o.Id;

            foreach (var op in model.OrderProducts) {
                context.OrderProducts.Add(new OrderProduct
                {
                    OrderId = curOrderId,
                    ProductId = op.ProductId,
                    Count = op.Count
                });
            }
            context.SaveChanges();

            if (o.Reserved) decMaterials(curOrderId);
        }

        private Dictionary<Material, int> totalMaterials(OrderBindingModel order)
        {
            var ms = new Dictionary<Material, int>();
            foreach (var op in order.OrderProducts)
            {
                var p = context.Products.FirstOrDefault(x => x.Id == op.ProductId);
                foreach (var pm in p.ProductMaterials)
                {
                    var m = pm.Material;
                    if (!ms.ContainsKey(m)) ms.Add(m, 0);
                    ms[m] += pm.Count * op.Count;
                }
            }
            return ms;
        }

        private void decMaterials(int orderId)
        {
            var o = context.Orders.First(e => e.Id == orderId);
            foreach (var p in o.OrderProducts.Select(op => op.Product))
                foreach (var pm in p.ProductMaterials)
                    pm.Material.Count -= pm.Count;
            context.SaveChanges();
        }
    }
}