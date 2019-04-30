using GoToWorkFactoryModel;
using GoToWorkFactoryServiceDAL.BindingModels;
using GoToWorkFactoryServiceDAL.Interfaces;
using GoToWorkFactoryServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoToWorkFactoryImplementDataBase.Implementations
{
    public class MaterialServiceDB : IMaterialService
    {

        private FactoryDbContext context;

        public MaterialServiceDB(FactoryDbContext context)
        {
            this.context = context;
        }

        public void AddElement(MaterialBindingModel model)
        {
            Material element = context.Materials.FirstOrDefault(rec => rec.Name == model.Name);
            if (element != null)
            {
                throw new Exception("Уже есть материал с таким названием");
            }
            context.Materials.Add(new Material
            {
                Name = model.Name,
                Count = model.Count
            });
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Material element = context.Materials.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Materials.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public MaterialViewModel GetElement(int id)
        {
            Material element = context.Materials.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new MaterialViewModel
                {
                    Id = element.Id,
                    Name = element.Name,
                    Count = element.Count
                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<MaterialViewModel> GetList()
        {
            List<MaterialViewModel> result = context.Materials.Select(rec => new MaterialViewModel
            {
                Id = rec.Id,
                Name = rec.Name,
                Count = rec.Count
            })
            .ToList();
            return result;
        }

        public void UpdElement(MaterialBindingModel model)
        {
            Material element = context.Materials.FirstOrDefault(rec => rec.Name == model.Name && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть материал с таким названием");
            }
            element = context.Materials.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Name = model.Name;
            element.Count = model.Count;
            context.SaveChanges();
        }
    }
}