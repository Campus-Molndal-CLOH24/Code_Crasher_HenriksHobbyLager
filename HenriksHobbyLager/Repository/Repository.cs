using System;
using System.Collections.Generic;
using System.Linq;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Repository

{
    public class Repository<T> : IRepository<T> where T : class
{
    private readonly List<T> _entities;
    private int _nextId;

    public Repository()
    {
        _entities = new List<T>();
        _nextId = 1;
    }

    public IEnumerable<T> GetAll()
    {
        return _entities;
    }

        public T? GetById(int id)
        {
            var property = typeof(T).GetProperty("Id");

            if (property == null)
                throw new InvalidOperationException("T måste ha en Id-egenskap.");

            foreach (var entity in _entities)
            {
                var value = property.GetValue(entity);
                if (value is int intValue && intValue == id)
                {
                    return entity;
                }
            }

            return null;
        }

        public void Add(T entity)
    {
        var property = typeof(T).GetProperty("Id");
        if (property == null)
            throw new InvalidOperationException("T måste ha en Id-egenskap.");

        property.SetValue(entity, _nextId++);
        _entities.Add(entity);
    }

    public void Update(T entity)
    {
        var property = typeof(T).GetProperty("Id");
        if (property == null)
            throw new InvalidOperationException("T måste ha en Id-egenskap.");

        int id = (int)property.GetValue(entity);
        var existingEntity = GetById(id);
        if (existingEntity != null)
        {
            var index = _entities.IndexOf(existingEntity);
            _entities[index] = entity;
        }
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity != null)
        {
            _entities.Remove(entity);
        }
    }

    public IEnumerable<T> Search(Func<T, bool> predicate)
    {
        return _entities.Where(predicate);
    }
}
}
