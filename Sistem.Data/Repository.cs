using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
namespace Sistem.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbContext _context;
        private IDbSet<T> _entities;
        public Repository(IDbContext context)
        {
            this._context = context;
        }
        public virtual IEnumerable<T> Get(
              Expression<Func<T, bool>> filter = null,
              Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
              string includeProperties = "")
        {
            try
            {
                IQueryable<T> query = this.Entities;

                if (filter != null)
                {
                    query = query.Where(filter);
                }
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        public virtual IQueryable<T> Table
        {
            //artık bu fonk. yerine üstteki;"  public virtual IEnumerable<T> Get(" metodu kullanılacak...
            get
            {
                try
                {
                    return this.Entities;
                }
                catch (Exception exp)
                {
                    throw exp;
                }
            }
        }

        public T GetById(object id)
        {
            try
            {
                return this.Entities.Find(id);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        public void Insert(T entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException("entity");
                this.Entities.Add(entity);
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = String.Empty;
                foreach (var validationerrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationerror in validationerrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + String.Format("property: {0} error: {1}",
                        validationerror.PropertyName, validationerror.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
            catch (Exception otherExp)
            {
                throw otherExp;
            }
        }
        public void Update(T entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException("entity");
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = String.Empty;
                foreach (var validationerrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationerror in validationerrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + String.Format("property: {0} error: {1}",
                        validationerror.PropertyName, validationerror.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
            catch (Exception otherExp)
            {
                throw otherExp;
            }
        }
        public void Delete(T entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException("entity");
                this.Entities.Remove(entity);
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = String.Empty;
                foreach (var validationerrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationerror in validationerrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + String.Format("property: {0} error: {1}",
                        validationerror.PropertyName, validationerror.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
            catch (Exception otherExp)
            {
                throw otherExp;
            }
        }
        private IDbSet<T> Entities
        {
            get
            {
                try
                {
                    if (_entities == null)
                    {
                        _entities = _context.Set<T>();
                    }
                    return _entities;
                }
                catch (Exception exp)
                {
                    throw exp;
                }
            }
        }
    }
}