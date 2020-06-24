using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.ViewModels;

namespace ApplicationCore.Interfaces
{
    public interface IDocumentService
    {
        Task SaveDocument(Document industryReview);
        Task DeleteDocument(int id);
        Task<Document> GetByIdAsync(int Id);
        Task<IList<Document>> GetListDocument();
        Task<IList<Document>> GetNodeDocument();
    }
}