using System;
using Microsoft.EntityFrameworkCore;
using UnitOfWorkAsync.Context;

namespace UnitOfWorkAsync.Repository
{
    public class UnitOfWork : IDisposable
    {
        DbContext _context;
        //
        public UnitOfWork() => _context = new Db();
        public TableRepository TableRepository () =>
                new TableRepository(_context);
        //
        //  ...
        //
        private bool disposed = false;
        //
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        //
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
