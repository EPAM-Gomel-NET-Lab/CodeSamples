using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibraryForTests.Core;
using LibraryForTests.Domain;
using Newtonsoft.Json;

namespace LibraryForTests.Services
{
    public class FileStorageBase<TEntity> 
        where TEntity : IHasBasicId, IArchivable
    {
        private readonly IFIleStorageSettings _settings;

        protected FileStorageBase(IFIleStorageSettings settings)
        {
            _settings = settings;
        }

        public void Add(TEntity item) 
        {
            
            if (item == null)
            {
                throw new InvalidOperationException("Can not add null object!");
            }

            // I think Cast<IHasBasicId>() is redundant 
            var entities = GetAll();
            var calculatedId = entities.Cast<IHasBasicId>().GetIncrementedId();
            item.Id = calculatedId;
            entities.Add(item);
            SaveAll(entities);
        }               

        public List<TEntity> GetAll()
        {
            var text = File.ReadAllText(_settings.FileNameData);
            var entities = JsonConvert.DeserializeObject<List<TEntity>>(text);
            return entities;
        }               

        public void Delete(TEntity item)
        {
            if (item == null)
            {
                throw new InvalidOperationException("Can not delete null object!");
            }

            var entities = GetAll();
            var itemToRemove = entities.FirstOrDefault(e => e.Id == item.Id);

            if (itemToRemove == null)
            {
                throw new InvalidOperationException("There is not this item in the Item Storage!");
            }

            entities.Remove(itemToRemove);
            SaveAll(entities);
        }

        public void Update(TEntity item)
        {
            if (item == null)
            {
                throw new InvalidOperationException("Can not update null object!");
            }

            var entities = GetAll();
            var itemToUpdate = entities.FirstOrDefault(e => e.Id == item.Id);

            if (itemToUpdate == null)
            {
                throw new InvalidOperationException("There is not this item in the Item Storage!");
            }

            entities.Remove(itemToUpdate);
            entities.Add(item);
            SaveAll(entities);
        }

        public void Archive(TEntity item)
        {
            if (item == null)
            {
                throw new InvalidOperationException("Can not archive null object!");

            }
            var entities = GetAll();
            var itemToArchive = entities.FirstOrDefault(e => e.Id == item.Id);

            if (itemToArchive == null)
            {
                throw new InvalidOperationException("There is not this item in the Item Storage!");
            }
            itemToArchive.IsArchive = true;
            SaveAll(entities);
        }

        protected void SaveAll(List<TEntity> entities)
        {
            var text = JsonConvert.SerializeObject(entities);
            File.WriteAllText(_settings.FileNameData, text);
        }
    }
}
