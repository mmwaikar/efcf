using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using Entities.Common;

namespace Common.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        // todo: needs a logger to log errors
        public UnitOfWork(CommonContext commonContext)
        {
            CommonContext = commonContext;
        }

        public CommonContext CommonContext { get; private set; }

        internal DbSet<T> GetDbSet<T>()
            where T : Entity
        {
            return CommonContext.Set<T>();
        }

        public void Commit()
        {
            try
            {
                CommonContext.SaveChanges();
            }
            catch (DbEntityValidationException dbex)
            {
                foreach (var eve in dbex.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            CommonContext.Dispose();
        }
    }
}