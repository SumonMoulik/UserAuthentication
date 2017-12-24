using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UserAuthhentication.Models;

namespace UserAuthhentication.DAL
{
    public static class ProductGateway
    {
        private static SqlConnection sqlConnection = null;
        private static SqlCommand sqlCommand = null;
        private static DataAccess dataAccess = new DataAccess();
        public static List<Product> GetAllProduct()
        {
            try
            {
                sqlConnection = new SqlConnection(dataAccess.ConnectionString());
                sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "Select * from tblProductInfo";

                List<Product> productsList = null;
                sqlConnection.Close();
                sqlCommand.Connection.Open();

                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    productsList = new List<Product>();

                    while (dataReader.Read())
                    {
                        Product products = new Product();
                        products.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                        products.ProductName = dataReader["ProductName"].ToString();
                        products.SalesPrice =Convert.ToDecimal(dataReader["SalesPrice"]);
                        productsList.Add(products);
                    }
                }
                dataReader.Close();
                sqlCommand.Connection.Close();
                sqlConnection.Close();
                return productsList;
            }
            catch (Exception Err)
            {
                throw new Exception(Err.Message);
            }
        }
    }
}