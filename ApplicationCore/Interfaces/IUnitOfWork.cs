using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IUnitOfWork
    {
        //Microsoft.EntityFrameworkCore.DbContext DbContext { get; set; }

        void SaveChanges();

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
        Task CompleteAsync();

    }
}
