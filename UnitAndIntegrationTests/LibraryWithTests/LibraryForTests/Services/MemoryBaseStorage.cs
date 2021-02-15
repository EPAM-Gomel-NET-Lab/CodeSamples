using System;
using System.Collections.Generic;
using System.Linq;
using LibraryForTests.Core;
using LibraryForTests.Domain;

namespace LibraryForTests.Services
{
    public class MemoryBaseStorage<TEntity>
        where TEntity : IHasBasicId, IArchivable
    {
        private readonly List<TEntity> _entities;

        protected MemoryBaseStorage(List<TEntity> entities)
        {
            _entities = new List<TEntity>(entities ?? new List<TEntity>());
        }

        public void Add(TEntity item)
        {
            // I think Cast<IHasBasicId>() is redundant 
            if(item == null)
            {
                throw new InvalidOperationException("Can not add null object!");
            }
            var calculatedId = _entities.Cast<IHasBasicId>().GetIncrementedId();
            item.Id = calculatedId;
            _entities.Add(item);
        }

        public void Delete(TEntity item)
        {
            if (item == null)
            {
                throw new InvalidOperationException("Can not delete null object!");
            }

            var itemToRemove = _entities.FirstOrDefault(e => e.Id == item.Id);

            if(itemToRemove == null)
            {
                throw new InvalidOperationException("There is not this item in the Item Storage!");
            }
            _entities.Remove(itemToRemove);
        }

        public void Update(TEntity item)
        {
            if (item == null)
            {
                throw new InvalidOperationException("Can not update null object!");
            }

            var itemToUpdate = _entities.FirstOrDefault(e => e.Id == item.Id);

            if (itemToUpdate == null)
            {
                throw new InvalidOperationException("There is not this item in the Item Storage!");
            }
            _entities.Remove(itemToUpdate);
            _entities.Add(item);
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
        }

        public List<TEntity> GetAll()
        {
            return new List<TEntity>(_entities);
        }
    }
}
