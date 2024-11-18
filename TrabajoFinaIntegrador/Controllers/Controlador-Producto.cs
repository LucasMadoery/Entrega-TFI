using Microsoft.AspNetCore.Mvc;
using Negocio;
//using Negocio.Modelo_;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System.Collections.Generic;
using Dapper;
using MySql.Data.MySqlClient;

namespace TrabajoFinaIntegrador.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        ProductosApi api = new ProductosApi();

        // Get
        [HttpGet("products")]
        public IActionResult Get()
        {
            try
            {
                var productos = api.GetAll();
                return Ok(productos); 
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Ha ocurrido un error: {ex.Message}"); 
            }
        }
        // GET: api/<ValuesController>/products/6

        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            try
            {
                ProductosApi ProductosApi = new ProductosApi();
                List<string> categorias = ProductosApi.GetAllCategories();
                return Ok(categorias); 
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, " error: {ex.Message}"); 
            }
        }




        // GET api/<ValuesController>/products/5
        // GET api/<ValuesController>/products/5
        [HttpGet("products/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var product = api.GetByID(id);
                return product != null ? Ok(product) : NotFound(); 
            }
            catch
            {
                return StatusCode(500, "Ha ocurrido un error");
            }
        }



        // POST api/<ValuesController>/products

        [HttpPost("products")]

    public IActionResult Post([FromBody] ModeloProductos producto)
    {
            ModeloProductos p;
        try
        {
            p = api.Post(producto);

        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);


        }
        return StatusCode(201, p);
    }

    // PUT api/<ValuesController>/5
    [HttpPut]

    public IActionResult Put([FromBody] ModeloProductos producto)

    {
        try
        {
                ModeloProductos p = api.Put(producto);
            return StatusCode(StatusCodes.Status200OK, new String[] { "Producto actualizado exitosamente." });

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al actualizar el producto");
        }
    }

    // DELETE api/<ValuesController>/5
    [HttpDelete("{id}")]

    public IActionResult Delete(int id)

    {
        if (api.Delete(id) == 0)
        {
            return NotFound();

        }
        else
        {
            return StatusCode(200);
        }
    }




}

 

}