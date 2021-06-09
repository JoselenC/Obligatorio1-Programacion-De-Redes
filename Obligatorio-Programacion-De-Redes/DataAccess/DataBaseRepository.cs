using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic;
using DataAccess.Mappers;
using Microsoft.EntityFrameworkCore;
using MSP.BetterCalm.DataAccess;

namespace DataAccess
{
    public class DataBaseRepository<D, T> : IRepository<D> where T : class
    {
          private IMapper<D,T> mapper;
        public DataBaseRepository(IMapper<D,T> mapper)
        {
            this.mapper = mapper;
        }

        public D Add(D objectToAdd)
        {
            using (ContextDb context = new ContextDb())
            {
                DbSet<T> entity = context.Set<T>();
                var dto = mapper.DomainToDto(objectToAdd, context);
                if (context.Entry(dto).State == (EntityState) EntityState.Detached)
                    entity.Add(dto);
                context.SaveChanges();
                var domainObj = mapper.DtoToDomain(dto, context);
                return domainObj;
            }
        }

        private T FindDto(Predicate<D> condition, ContextDb context)
        {
            DbSet<T> entity = context.Set<T>();
            List<T> TDtos = entity.ToList();
            foreach (var TDto in TDtos)
            {
                var DDto = mapper.DtoToDomain(TDto, context);
                var condResult = condition(DDto);
                if (condResult)
                    return TDto;
            };
            throw new KeyNotFoundException();
        }

        public void Delete(D objectToDelete)
        {
            using (ContextDb context = new ContextDb())
            {
                var entity = context.Set<T>();
                var ObjectToDeleteDto = FindDto(x => x.Equals(objectToDelete), context);
                entity.Remove(ObjectToDeleteDto);
                context.SaveChanges();
            }
        }

        public D Find(Predicate<D> condition)
        {
            using (ContextDb context = new ContextDb())
            {
                DbSet<T> entity = context.Set<T>();
                List<T> TDtos = entity.ToList();
                foreach (var TDto in TDtos)
                {
                    var DDto = mapper.DtoToDomain(TDto, context);
                    var condResult = condition(DDto);
                    if (condResult)
                        return DDto;
                };
                throw new KeyNotFoundException();
            }
        }

        public List<D> Get()
        {
            using (ContextDb context = new ContextDb())
            {
                DbSet<T> entity = context.Set<T>();
                List<D> Dlist = new List<D>();
                foreach (T item in entity.ToList())
                {
                    var x = mapper.DtoToDomain(item, context);
                    Dlist.Add(x);
                }
                return Dlist;
            }
        }

        public D Update(D OldObject, D UpdatedObject)
        {
            using (ContextDb context = new ContextDb())
            {
                DbSet<T> entity = context.Set<T>();
                T objToUpdate = FindDto(x => x.Equals(OldObject), context);
                mapper.UpdateDtoObject(objToUpdate, UpdatedObject, context);
                context.SaveChanges();
                return UpdatedObject;
            }
        }
    }
}