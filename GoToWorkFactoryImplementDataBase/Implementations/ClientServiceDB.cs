using GoToWorkFactoryModel;
using GoToWorkFactoryServiceDAL.BindingModels;
using GoToWorkFactoryServiceDAL.Interfaces;
using GoToWorkFactoryServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
namespace GoToWorkFactoryImplementDataBase.Implementations
{
    public class ClientServiceDB : IClientService
    {
        private FactoryDbContext context;
        public ClientServiceDB(FactoryDbContext context)
        {
            this.context = context;
        }
        public List<ClientViewModel> GetList()
        {
            List<ClientViewModel> result = context.Clients.Select(rec => new
            ClientViewModel
            {
                Id = rec.Id,
                Name = rec.Name,
                Email = rec.Email,
                IsAdmin = rec.IsAdmin
            })
            .ToList();
            return result;
        }
        public ClientViewModel GetElement(int id)
        {
            Client element = context.Clients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ClientViewModel
                {
                    Id = element.Id,
                    Name = element.Name,
                    Email = element.Email,
                    IsAdmin = element.IsAdmin
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(ClientBindingModel model)
        {
            Client element = context.Clients.FirstOrDefault(rec => rec.Name ==
            model.Name);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            context.Clients.Add(new Client
            {
                Name = model.Name,
                Email = model.Email,
                IsAdmin = model.IsAdmin
            });
            context.SaveChanges();
        }
        public void UpdElement(ClientBindingModel model)
        {
            Client element = context.Clients.FirstOrDefault(rec => rec.Name ==
            model.Name && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Name = model.Name;
            context.SaveChanges();
        }
        public void DelElement(int id)
        {
            Client element = context.Clients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Clients.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}