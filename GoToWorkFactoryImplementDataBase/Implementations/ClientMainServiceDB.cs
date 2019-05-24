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

            foreach (var op in model.OrderProducts) {
                context.OrderProducts.Add(new OrderProduct
                {
                    OrderId = context.Orders.Last().Id,
                    ProductId = op.ProductId,
                    Count = op.Count
                });
            }
            context.SaveChanges();
        }

        public void FinishOrder(OrderBindingModel model)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = OrderStatus.Готов;
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
                ClientName = rec.Client.Name
            })
            .ToList();
            return result;
        }

        public void PayOrder(OrderBindingModel model)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = OrderStatus.Оплачен;
            context.SaveChanges();
        }
        
        public List<OrderViewModel> GetFreeOrders()
        {
            List<OrderViewModel> result = context.Orders
            .Where(x => x.Status == OrderStatus.Принят || x.Status == OrderStatus.НедостаточноРесурсов)
            .Select(rec => new OrderViewModel
            {
                Id = rec.Id
            })
            .ToList();
            return result;
        }

        public void TakeOrderInWork(OrderBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.Status != OrderStatus.Принят)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    element.DateImplement = DateTime.Now;
                    element.Status = OrderStatus.Выполняется;
                    context.SaveChanges();
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