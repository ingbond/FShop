﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using FShop.Core.Models;


namespace FShop.DataAccess.InMemory
{
    public class InMemoryRepository<T>  where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;
            if (items == null)
            {
                items = new List<T>();

            }
        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            T tToUpdate = items.Find(v => v.Id == t.Id);

            if (tToUpdate != null)
            {
                tToUpdate = t;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }

        public T Find(string Id)
        {
            T t = items.Find(v => v.Id == Id);


            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string Id)
        {
            T t = items.Find(v => v.Id == Id);

            if (t != null)
            {
                items.Remove(t);
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }

    }
}