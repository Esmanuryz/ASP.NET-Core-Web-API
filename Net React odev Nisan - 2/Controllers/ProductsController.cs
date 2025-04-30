using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
namespace Net_React_odev_Nisan___2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        /*     // GET: api/product
             [HttpGet]
             public IActionResult GetAll()
             {
                 return Ok(fakedata.Products);
             }

             // GET: api/product/{id}
             [HttpGet("{id}")]
             public IActionResult GetById(int id)
             {
                 var product = fakedata.Products.FirstOrDefault(p => p.Id == id);
                 if (product == null)
                     return NotFound();

                 return Ok(product);
             }

             // POST: api/product
             [HttpPost]
             public IActionResult Create(Product product)
             {
                 product.Id = fakedata.Products.Max(p => p.Id) + 1;
                 fakedata.Products.Add(product);
                 return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
             }

             // PUT: api/product/{id}
             [HttpPut("{id}")]
             public IActionResult Update(int id, Product updatedProduct)
             {
                 var product = fakedata.Products.FirstOrDefault(p => p.Id == id);
                 if (product == null)
                     return NotFound();

                 product.Name = updatedProduct.Name;
                 product.Price = updatedProduct.Price;
                 product.Stock = updatedProduct.Stock;

                 return NoContent();
             }

             // DELETE: api/product/{id}
             [HttpDelete("{id}")]
             public IActionResult Delete(int id)
             {
                 var product = fakedata.Products.FirstOrDefault(p => p.Id == id);
                 if (product == null)
                     return NotFound();

                 fakedata.Products.Remove(product);
                 return NoContent();
             }
*/


        private readonly string _connectionString;

        public ProductsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/products
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> products = new();
            using SqlConnection conn = new(_connectionString);
            SqlCommand cmd = new("SELECT * FROM Product", conn);
            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                    Price = (decimal)reader["Price"],
                    Stock = (int)reader["Stock"]
                });
            }
            return Ok(products);
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            using SqlConnection conn = new(_connectionString);
            SqlCommand cmd = new("SELECT * FROM Product WHERE Id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var product = new Product
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                    Price = (decimal)reader["Price"],
                    Stock = (int)reader["Stock"]
                };
                return Ok(product);
            }
            return NotFound();
        }

        // POST: api/products
        [HttpPost]
        public IActionResult Create(Product product)
        {
            using SqlConnection conn = new(_connectionString);
            SqlCommand cmd = new("INSERT INTO Product (Name, Price, Stock) VALUES (@name, @price, @stock)", conn);
            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@stock", product.Stock);
            conn.Open();
            cmd.ExecuteNonQuery();
            return Ok("Product added.");
        }

        // PUT: api/products/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            using SqlConnection conn = new(_connectionString);
            SqlCommand cmd = new("UPDATE Product SET Name = @name, Price = @price, Stock = @stock WHERE Id = @id", conn);
            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@stock", product.Stock);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected == 0)
                return NotFound();
            return Ok("Product updated.");
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using SqlConnection conn = new(_connectionString);
            SqlCommand cmd = new("DELETE FROM Product WHERE Id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected == 0)
                return NotFound();
            return Ok("Product deleted.");
        }

    }
}
