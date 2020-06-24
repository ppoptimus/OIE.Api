using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Exceptions;
using System;
using System.Linq;
using ApplicationCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using IdentityServer4.AccessTokenValidation;

namespace OIE.Api.Controllers
{
    [Route("api/[controller]")]
    public class DocumentController : Controller
    {
        private readonly IDocumentService _documentService;
        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        //[Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpGet("{Id}")]
        public async Task<Document> GetDocumentById(int Id)
        {
            return await _documentService.GetByIdAsync(Id);
        }

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpGet]
        public async Task<IList<Document>> GetListDocument()
        {
            IList<Document> documentList = await _documentService.GetListDocument();
            foreach (Document document in documentList)
            {
                if(document.ParentId != 0)
                {
                    var tempDocument = documentList.Where(o => o.Id == document.ParentId);
                    if (tempDocument.Count() > 0)
                    {
                        document.ParentDocumentName = tempDocument.FirstOrDefault().DocumentName;
                    }
                }
            }

            IList<Document> result = new List<Document>();
            foreach (var l1 in documentList.Where(d => d.ParentId == 0).OrderBy(p => p.ParentId == 0 ? p.Id : p.ParentId).ThenBy(p => p.Id).ToList())
            {
                l1.Level = 1;
                result.Add(l1);
                foreach (var l2 in documentList.Where(d => d.ParentId == l1.Id).OrderBy(p => p.ParentId == 0 ? p.Id : p.ParentId).ThenBy(p => p.Id).ToList())
                {
                    l2.Level = 2;
                    l2.DocumentName = "&nbsp;&nbsp;" + l2.DocumentName;
                    result.Add(l2);
                    foreach (var l3 in documentList.Where(d => d.ParentId == l2.Id).OrderBy(p => p.ParentId == 0 ? p.Id : p.ParentId).ThenBy(p => p.Id).ToList())
                    {
                        l3.Level = 3;
                        l3.DocumentName = "&nbsp;&nbsp;&nbsp;&nbsp;" + l3.DocumentName;
                        result.Add(l3);
                    }
                }
            }

            return result;
        }

        [HttpGet("HomePage")]
        public async Task<IList<Document>> GetListDocumentHomePage()
        {
            IList<Document> documentList = await _documentService.GetListDocument();
            foreach (Document document in documentList)
            {
                if (document.ParentId != 0)
                {
                    var tempDocument = documentList.Where(o => o.Id == document.ParentId);
                    if (tempDocument.Count() > 0)
                    {
                        document.ParentDocumentName = tempDocument.FirstOrDefault().DocumentName;
                    }
                }
            }

            return documentList.Where(o => o.ShowHomePage == true).OrderBy(p=>p.ParentId == 0 ?p.Id : p.ParentId).ThenBy(p=>p.Id).ToList();
        }

        [HttpGet("Download")]
        public async Task<IList<Document>> GetListDocumentDownload()
        {
            return await _documentService.GetNodeDocument();
        }

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpPost]
        public async Task<IActionResult> SaveDocument([FromBody]Document Document)
        {
            try
            {
                await _documentService.SaveDocument(Document);
                return Ok();
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            try
            {
                await _documentService.DeleteDocument(id);
                return Ok();
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}