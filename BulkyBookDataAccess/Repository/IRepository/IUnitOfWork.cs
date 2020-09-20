using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable 
    {
        ICategoryRepository Category { get; }

        ICoverTypeRepository CoverType { get; } //Step 5. - Configure IUnitOfWork. Next step is to configure UnitOfWork.

        ISP_Call SP_Call { get; }

        void Save();
    }
}
