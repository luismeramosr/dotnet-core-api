using Microsoft.AspNetCore.Mvc;
using dotnet_core_api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace dotnet_core_api.Controllers
{

    // Ruta base para todos los endpoints /api/document_type/*
    [Authorize]
    [ApiController]
    [Route("api/document_type/")]
    public class DocumentTypeController : ControllerBase
    {

        private DB_PAMYSContext db = new DB_PAMYSContext();

        // Todos los endpoints son funciones asincronas, para mejorar
        // el performance, aun falta ver una manera de retornar los estados http
        // y validar errores.
        [Route("all")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<DocumentType>> getAll()
        {
            return await Task.Run<IEnumerable<DocumentType>>(() =>
            {
                return this.db.DocumentTypes.ToList();
            });
        }

        // Recibe el parametro id
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DocumentType>> getById(int id)
        {
            return await Task.Run<ActionResult<DocumentType>>(() =>
            {
                var doctype = this.db.DocumentTypes.Find(id);
                if (doctype != null)
                    return Ok(doctype);
                else
                    return NotFound();
            });
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DocumentType>> save(DocumentType doctype)
        {
            return await Task.Run<ActionResult<DocumentType>>(() =>
            {
                if (doctype == null)
                    return BadRequest();
                this.db.DocumentTypes.Add(doctype);
                this.db.SaveChanges();
                return Ok(doctype);
            });
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DocumentType>> update(DocumentType doctype)
        {
            return await Task.Run<ActionResult<DocumentType>>(() =>
            {
                try
                {
                    var updateTask = this.db.DocumentTypes.Update(doctype);
                    if (updateTask.State == EntityState.Modified)
                        this.db.SaveChanges();
                    return Ok(doctype);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            });
        }

        [HttpDelete("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> delete(int id)
        {
            var doctype = this.db.DocumentTypes.Find(id);
            return await Task.Run<IActionResult>(() =>
            {
                try
                {
                    var deleteTask = this.db.DocumentTypes.Remove(doctype);
                    if (deleteTask.State == EntityState.Deleted)
                        this.db.SaveChanges();
                    return Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            });
        }

    }
}
