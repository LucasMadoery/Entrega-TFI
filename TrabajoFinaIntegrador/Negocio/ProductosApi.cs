using Dapper;
using MySql.Data.MySqlClient;
using Mysqlx.Session;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProductosApi
    {
        string connString = "Server=db4free.net;Database=lasnieves110424;Uid=lasnieves110424;Pwd=lasnieves110424;";

        public List<ModeloProductos> GetAll()
        {
            List<ModeloProductos> listaProductos = new List<ModeloProductos>();
            using (MySqlConnection connect = new MySqlConnection(connString))
            {
                connect.Open();
                string query = "SELECT * FROM products";
                listaProductos = connect.Query<ModeloProductos>(query).ToList();
            }
            return listaProductos;

        }

        public ModeloProductos GetByID(int id)
        {
            using (MySqlConnection connect = new MySqlConnection(connString))
            {
                connect.Open();
                string query = "SELECT * FROM products WHERE Id = @ID ";
                var rta = connect.QueryFirstOrDefault<ModeloProductos>(query, new { ID = id });
                return rta;

            }
        }


        public void Update(ModeloProductos producto) { }

        public int Delete (int id) {
        
            using (MySqlConnection  connect = new MySqlConnection(connString))
            {
                connect.Open();

                string query = "DELETE FROM Products WHERE Id = @Id";
                int afectedRaws = connect.Execute(query, new { ID = id });
                return afectedRaws;

            }
        }

        public ModeloProductos Put(ModeloProductos producto) {

            using (MySqlConnection connect = new MySqlConnection(connString))
            {
                connect.Open();
                string query = @"UPDATE Products SET Title = @Title, Price = @price, 
                               description = @description, category = @category WHERE Id = @Id";

                int afectedRaws = connect.Execute(query, new {
                    
                    Title = producto.title,
                    Price = producto.price,
                    Description = producto.description,
                    Category = producto.category,
                    Id = producto.id

                });

                if (afectedRaws > 0)
                {
                    return producto;
                }
                else
                {
                    return null;
                }

            }
        }

        public ModeloProductos Post(ModeloProductos producto) {

            using (MySqlConnection connect = new MySqlConnection(connString)){

                connect.Open();
                string query = @"INSERT INTO Products (Title, Price,description,category) 
                                VALUES (@Title, @Price,@description,@category) ";

                connect.Execute(query, new
                {
                    Title = producto.title,
                    Price = producto.price,
                    Description = producto.description,
                    Category = producto.category,
                    
                });

                return producto;
            };
        }

        public List<string> GetAllCategories()
        {
            List<string> lProductos = new List<string>();
            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.Open();
                string query = "SELECT Categories FROM Categories";
                lProductos = connect.Query<string>(query).ToList();

                return lProductos;
            }
        }


    }
}
