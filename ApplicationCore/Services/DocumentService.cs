using ApplicationCore.Interfaces;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ApplicationCore.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IAppLogger<DocumentService> _logger;

        private readonly IAsyncRepository<Document> _documentRepository;
        private readonly IQueryRepository _queryRepository;

        private IUnitOfWork _uow;

        public DocumentService(
            IUnitOfWork uow,
            IAsyncRepository<Document> documentRepository,
            IQueryRepository queryRepository,
            IAppLogger<DocumentService> logger)
        {
            _uow = uow;
            _documentRepository = documentRepository;
            _logger = logger;
            _queryRepository = queryRepository;
        }

        public async Task SaveDocument(Document documentModel)
        {
            try
            {
                _uow.BeginTransaction();
                if(documentModel.Id == 0)
                {
                    //Create
                    await _documentRepository.AddAsync(documentModel);
                }
                else
                {
                    //Update
                    await _documentRepository.UpdateAsync(documentModel);
                }
                
                _uow.CommitTransaction();
            }
            catch (System.Exception e)
            {
                _uow.RollbackTransaction();
                throw;
            }
        }

        public async Task<Document> GetByIdAsync(int Id)
        {
            return await _documentRepository.GetByIdAsync(Id);
        }

        public async Task<IList<Document>> GetListDocument()
        {
            return await _documentRepository.ListAllAsync();
        }

        public async Task<IList<Document>> GetNodeDocument()
        {
            IList<Document> rootNode = new List<Document>();
            IList<Document> documentList = await _documentRepository.ListAllAsync();
            rootNode = documentList.Where(o => o.ParentId == 0).ToList();
            manageTree(rootNode, documentList);
            return rootNode;
        }

        private IList<Document> manageTree(IList<Document> rootNode, IList<Document> documentList)
        {
            foreach (var node in rootNode)
            {
                var tempNode = documentList.Where(o => o.ParentId == node.Id);
                if(tempNode != null)
                {
                    node.DocumentChild = tempNode.ToList();
                    manageTree(node.DocumentChild, documentList);
                }
            }
            return null;
        }

        public async Task DeleteDocument(int id)
        {
            try
            {
                _uow.BeginTransaction();

                await _documentRepository.DeleteAsync(await _documentRepository.GetByIdAsync(id));

                _uow.CommitTransaction();
            }
            catch (System.Exception e)
            {
                _uow.RollbackTransaction();
                throw;
            }
        }

    }
}
