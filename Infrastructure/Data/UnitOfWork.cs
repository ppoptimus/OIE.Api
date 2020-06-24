using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Infrastructure.Data
{

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        //public DbContext DbContext { get; set; }
        private ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this._context = dbContext;
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        public void BeginTransaction()
        {
            this._context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            this._context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            this._context.Database.RollbackTransaction();
        }


        private bool disposed = false;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //// clear repositories
                    //if (repositories != null)
                    //{
                    //    repositories.Clear();
                    //}

                    // dispose the db context.
                    _context.Dispose();
                }
            }

            disposed = true;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}