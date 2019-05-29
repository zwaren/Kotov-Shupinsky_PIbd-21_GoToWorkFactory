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
            context.Orders.Add(new Order
            {
                ClientId = model.ClientId,
                DateCreate = DateTime.Now,
                Reserved = model.Reserved,
                Sum = model.Sum,
                Status = OrderStatus.Принят
            });
            context.SaveChanges();

            int curOrderId = context.Orders.Last().Id;

            foreach (var op in model.OrderProducts) {
                context.OrderProducts.Add(new OrderProduct
                {
                    OrderId = curOrderId,
                    ProductId = op.ProductId,
                    Count = op.Count
                });
            }
            context.SaveChanges();

            decMaterials(curOrderId);
        }

        private void decMaterials(int orderId)
        {
            var o = context.Orders.First(e => e.Id == orderId);
            foreach (var op in o.OrderProducts)
                foreach (var pm in op.Product.ProductMaterials)
                    pm.Material.Count -= pm.Count;
            context.SaveChanges();
        }
    }
}