using System;

namespace Entities.Common
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}