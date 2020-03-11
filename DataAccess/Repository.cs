using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccess
{
    public class Repository
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True";
        public static List<Order> orders = new List<Order>();
        public static List<OrderDetail> orderDetails = new List<OrderDetail>();

        public static List<Order> GetAllOrders()
        {
            string sql = "SELECT * FROM Orders";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                int orderID = (int)reader["OrderID"];
                DateTime orderDate = (DateTime)reader["OrderDate"];
                DateTime requiredDate = (DateTime)reader["RequiredDate"];
                DateTime shippedDate = Convert.IsDBNull(reader["ShippedDate"]) ? DateTime.MinValue : (DateTime)reader["ShippedDate"];
                int shipVia = (int)reader["ShipVia"];
                decimal freight = (decimal)reader["Freight"];
                string shipName = (string)reader["ShipName"];
                string shipAddress = (string)reader["ShipAddress"];
                string shipCity = (string)reader["ShipCity"];
                string shipRegion = Convert.IsDBNull(reader["ShipRegion"]) ? null : (string)reader["ShipRegion"];
                string shipPostalCode = Convert.IsDBNull(reader["ShipPostalCode"]) ? null : (string)reader["ShipPostalCode"];
                string shipCountry = Convert.IsDBNull(reader["ShipCountry"]) ? null : (string)reader["ShipCountry"];

                Order order = new Order(orderID, orderDate, requiredDate, shippedDate,
                    shipVia, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode,
                    shipCountry);
                orders.Add(order);
            }
            connection.Close();
            return orders;
        }
        public static List<Order> GetAllOrderDetails()
        {
            string sql = "SELECT * FROM [Order Details]";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                int orderID = (int)reader["OrderID"];
                decimal unitPrice = (decimal)reader["UnitPrice"];
                short quantity = (short)reader["Quantity"];
                float discount = (float)reader["Discount"];

                OrderDetail detail = new OrderDetail(orderID, unitPrice, quantity, discount);
                orderDetails.Add(detail);
            }
            connection.Close();
            return orders;
        }


    }
}