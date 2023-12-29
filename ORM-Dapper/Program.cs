using System;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);

            #region Department section.
            //var departmentRepo = new DapperDepartmentRepository(conn);

            //departmentRepo.InsertDepartment("John's new Department.");

            //var departments = departmentRepo.GetAllDepartments();

            //foreach (var department in departments)
            //{
            //Console.WriteLine(department.DepartmentID);
            //Console.WriteLine(department.Name);
            //Console.WriteLine();
            //}
            #endregion

            var ProductRepository = new DapperProductRepository(conn);

            /*var productToUpdate = ProductRepository.GetProduct(942);

            productToUpdate.Name = "UPDATED!";
            productToUpdate.Price = 12.99;
            productToUpdate.CategoryID = 1;
            productToUpdate.OnSale = false;
            productToUpdate.StockLevel = 1000;

            ProductRepository.UpdateProduct(productToUpdate);*/

            ProductRepository.DeleteProduct(942);

            var Products = ProductRepository.GetAllProduct();
            foreach (var Product in Products)
            {
                Console.WriteLine(Product.ProductID);
                Console.WriteLine(Product.Name);
                Console.WriteLine(Product.Price);
                Console.WriteLine(Product.OnSale);
                Console.WriteLine(Product.CategoryID);
                Console.WriteLine(Product.StockLevel);
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
