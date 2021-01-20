using Microsoft.AspNetCore.Mvc;
using dotnet_core_api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace dotnet_core_api.Controllers {

    // Ruta base para todos los endpoints /api/category/*
    [ApiController]
    [Route("api/category/")]
    public class CategoryController : ControllerBase {
        
        private DB_PAMYSContext db = new DB_PAMYSContext();

        // Todos los endpoints son funciones asincronas, para mejorar
        // el performance, aun falta ver una manera de retornar los estados http
        // y validar errores.
        [Route("all")]
        [HttpGet]
        public async Task<IEnumerable<Category>> getAll() {
            return await Task.Run(() => this.db.Categories.ToList());
        }

        // Recibe el parametro id
        [HttpGet("{id}")]
        public async Task<Category> getById(int id) {
            return await Task.Run(() => this.db.Categories.Find(id));
        }

        [HttpPost]
        public async Task add(Category category) {
            await Task.Run(() =>
            {
                try
                {
                    this.db.Add(category);
                    this.db.SaveChangesAsync();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }
            });            
        }

        [HttpPut]
        public async Task update(Category category) {
            await Task.Run(() =>
            {
                try
                {
                    this.db.Categories.Update(category);
                    this.db.SaveChangesAsync();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }
            });
        }

        [HttpDelete]
        public async Task delete(Category category) {
            await Task.Run(() =>
            {
                try
                {
                    this.db.Categories.Remove(category);
                    this.db.SaveChangesAsync();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }
            });
        }

    }
}