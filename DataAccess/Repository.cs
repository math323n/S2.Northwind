using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DataAccess
{
    public class Repository
    {

            #region Fields and constants
            const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=5;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            #endregion


            #region Constructors
            /// <summary>
            /// Initializes a new instance of Repository. Attempts to establish a connection, and will throw an exception on connection error.
            /// </summary>
            public Repository()
            {
                try
                {
                    SqlConnection connection = GetConnection(connectionString) as SqlConnection;
                    (bool, Exception) connectionAttemptResult = TryConnectUsing(connection);
                }
                catch(Exception e)
                {
                    throw new Exception("Data access error. See inner exception for details", e);
                }
            }
            #endregion


            #region Helper Methods
            /// <summary>
            /// Executes the provided SQL statement and returns data wrapped in a data set, if any.
            /// </summary>
            /// <param name="sql">The SQL statement to execute.</param>
            /// <returns>A <see cref="DataSet"/> wrapping any returned data.</returns>
            /// <exception cref="ArgumentException"/>
            /// <exception cref=""
            public DataSet Execute(string query)
            {
                if(string.IsNullOrWhiteSpace(query))
                {
                    throw new ArgumentException("Null or whitespace.");
                }
                DataSet resultSet = new DataSet();
                try
                {
                    SqlConnection connection = GetConnection(connectionString) as SqlConnection;
                    using(SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(query, connection)))
                    {
                        adapter.Fill(resultSet);
                    }
                    return resultSet;
                }
                catch(Exception e)
                {
                    throw new Exception("Data access error. See inner exception for details", e);
                }
            }
            /// <summary>
            /// Creates a connection based on the name of the input parameter connection string.
            /// </summary>
            /// <param name="connectionString">The name of the connection string.</param>
            /// <returns>A new connection.</returns>
            private static DbConnection GetConnection(string connectionString)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                return connection;
            }

            /// <summary>
            /// Attempts to connect to a data source using the provided connection.
            /// </summary>
            /// <param name="connection">The connection to use.</param>
            /// <returns>True, if the connection could be established, false otherwise.</returns>
            public (bool, Exception) TryConnectUsing(DbConnection connection)
            {
                try
                {
                    using(connection)
                    {
                        connection.Open();
                        connection.Close();
                    }
                    return (true, null);
                }
                catch(Exception e)
                {
                    return (false, e);
                }
            }

            /// <summary>
            /// Extract all data relevant to an order from a dat row object, and return an order object.
            /// </summary>
            /// <param name="dataRow"></param>
            /// <returns></returns>
            private static Order ExtractOrderFrom(DataRow dataRow)
            {
                int orderID = (int)dataRow["OrderID"];         
                DateTime orderDate = (DateTime)dataRow["OrderDate"];
                DateTime requiredDate = (DateTime)dataRow["RequiredDate"];
                DateTime shippedDate = Convert.IsDBNull(dataRow["ShippedDate"]) ? DateTime.MinValue : (DateTime)dataRow["ShippedDate"];
                int shipVia = (int)dataRow["ShipVia"];
                decimal freight = (decimal)dataRow["Freight"];
                string shipName = Convert.IsDBNull(dataRow["ShipName"]) ? null : (string)dataRow["ShipName"];
                string shipAddress = Convert.IsDBNull(dataRow["ShipAddress"]) ? null : (string)dataRow["ShipAddress"];
                string shipCity = Convert.IsDBNull(dataRow["ShipCity"]) ? null : (string)dataRow["ShipCity"];
                string shipRegion = Convert.IsDBNull(dataRow["ShipRegion"]) ? null : (string)dataRow["ShipRegion"];
                string shipPostalCode = Convert.IsDBNull(dataRow["ShipPostalCode"]) ? null : (string)dataRow["ShipPostalCode"];
                string shipCountry = Convert.IsDBNull(dataRow["ShipCountry"]) ? null : (string)dataRow["ShipCountry"];


                string query = $"SELECT * FROM [Order Details] WHERE OrderID = {orderID}";
                Repository repository = new Repository();
                DataSet orderDetails = repository.Execute(query);
                List<OrderDetail> orderDetailList = new List<OrderDetail>();
                if(orderDetails.Tables.Count > 0 && orderDetails.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow orderDetailsDataRow in orderDetails.Tables[0].Rows)
                    {
                        OrderDetail orderDetail = ExtractOrderDetailsFrom(orderDetailsDataRow);
                        orderDetailList.Add(orderDetail);
                    }
                }
                Order order = new Order(orderID, orderDate, requiredDate, shippedDate, shipVia, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, orderDetailList);

                return order;
            }
            /// <summary>
            /// Extract all data relevant to an order detail from a dat row object, and return an order detail object.
            /// </summary>
            /// <param name="dataRow"></param>
            /// <returns></returns>
            private static OrderDetail ExtractOrderDetailsFrom(DataRow dataRow)
            {
                int orderID = (int)dataRow["OrderID"];
                int productID = (int)dataRow["ProductID"];
                decimal unitPrice = (decimal)dataRow["UnitPrice"];
                short quantity = (short)dataRow["Quantity"];
                float discount = (float)dataRow["Discount"];

                OrderDetail orderDetails = new OrderDetail(orderID, productID, unitPrice, quantity, discount);

                return orderDetails;
            }
            #endregion


            #region Repository Methods
            /// <summary>
            /// Gets all orders.
            /// </summary>
            /// <returns>A list of all orders</returns>
            public List<Order> GetAllOrders()
            {
                List<Order> orders = new List<Order>();
                string query = "SELECT * FROM Orders";
                DataSet resultSet;
                try
                {
                    resultSet = Execute(query);
                }
                catch(Exception)
                {
                    throw;
                }

                if(resultSet.Tables.Count > 0 && resultSet.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dataRow in resultSet.Tables[0].Rows)
                    {
                        Order order = ExtractOrderFrom(dataRow);
                        orders.Add(order);
                    }
                }
                return orders;
            }
            #endregion


























            /*
            private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True";
            public static List<Order> orders = new List<Order>();
            public static List<OrderDetail> orderDetails = new List<OrderDetail>();


            public Repository()
            {
                try
                {
                    SqlConnection connection = GetConnection(connectionString) as SqlConnection;
                    (bool, Exception) connectionAttemptResult = TryConnectUsing(connection);
                }
                catch(Exception e)
                {
                    throw new Exception("Data access error. See inner exception for details", e);
                }
            }

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


                    string query = $"SELECT * FROM [Order Details] WHERE OrderID = {orderID}";
                    Repository repository = new Repository();
                    DataSet orderDetails = repository.Execute(query);
                    List<OrderDetails> orderDetailList = new List<OrderDetails>();
                    if(orderDetails.Tables.Count > 0 && orderDetails.Tables[0].Rows.Count > 0)
                    {
                        foreach(DataRow orderDetailsDataRow in orderDetails.Tables[0].Rows)
                        {
                            OrderDetails orderDetail = ExtractOrderDetailsFrom(orderDetailsDataRow);
                            orderDetailList.Add(orderDetail);
                        }
                    }

                    Order order = new Order(orderID, orderDate, requiredDate, shippedDate,
                        shipVia, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode,
                        shipCountry, orderDetails);
                    orders.Add(order);
                }

                connection.Close();
                return orders;
            }

            private static OrderDetail ExtractOrderDetailsFrom(DataRow dataRow)
            {
                int orderID = (int)dataRow["OrderID"];
                int productID = (int)dataRow["ProductID"];
                decimal unitPrice = (decimal)dataRow["UnitPrice"];
                short quantity = (short)dataRow["Quantity"];
                float discount = (float)dataRow["Discount"];

                OrderDetail orderDetails = new OrderDetail(orderID, productID, unitPrice, quantity, discount);

                return orderDetails;
            }
            private static DbConnection GetConnection(string name)
            {
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
                DbProviderFactory factory = DbProviderFactories.GetFactory(settings.ProviderName);
                DbConnection connection = factory.CreateConnection();
                connection.ConnectionString = settings.ConnectionString;
                return connection;
            }
            public (bool, Exception) TryConnectUsing(DbConnection connection)
            {
                try
                {
                    using(connection)
                    {
                        connection.Open();
                        connection.Close();
                    }
                    return (true, null);
                }
                catch(Exception e)
                {
                    return (false, e);
                }
            }

            public static List<OrderDetail> GetAllOrderDetails()
            {
                string sql = "SELECT * FROM [Order Details]";
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    int orderID = (int)reader["OrderID"];
                    int productID = (int)reader["ProductID"];
                    decimal unitPrice = (decimal)reader["UnitPrice"];
                    short quantity = (short)reader["Quantity"];
                    float discount = (float)reader["Discount"];

                    OrderDetail detail = new OrderDetail(orderID, productID, unitPrice, quantity, discount);
                    orderDetails.Add(detail);
                }
                connection.Close();
                return orderDetails;
            }   
            */

        }
    }