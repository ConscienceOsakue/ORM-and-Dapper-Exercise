using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace ORM_Dapper
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;

        public DapperProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        

        public IEnumerable<Product> GetAllProduct()
        {
            return _conn.Query<Product>("SELECT * FROM Products;");
        }

        public Product GetProduct(int id)
        {
            return _conn.QuerySingle<Product>("SELECT * FROM products WHERE ProductID = @id;", new {id = id});
        }

        public void UpdateProduct(Product product)
        {
            _conn.Execute("UPDATE products " +
                          "SET Name = @name, " +
                          "Price = @price, " +
                          "OnSale = @onSale, " +
                          "CategoryID = @catID, " +
                          "StockLevel = @stock " +  // Add a space here
                          "WHERE ProductID = @id;",
                          new
                          {
                              id = product.ProductID,
                              name = product.Name,
                              price = product.Price,
                              onSale = product.OnSale,
                              catID = product.CategoryID,
                              stock = product.StockLevel
                          });
        }

        public void DeleteProduct(int id)
        {
            _conn.Execute("DELETE FROM sales WHERE ProductID = @id;", new { id = id });
            _conn.Execute("DELETE FROM reviews WHERE ProductID = @id;", new { id = id });
            _conn.Execute("DELETE FROM products WHERE ProductID = @id;", new { id = id });
        }
    }
}
