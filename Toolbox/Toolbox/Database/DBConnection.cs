using System;
using System.Data;
// Connector to the DB 
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Toolbox.Logic;

namespace Toolbox.Database
{
    public class DBConnection
    {
        public DBConnection()
        { } // Default constructor

        private string dbName = string.Empty;
        // Property
        public string DatabaseName
        {
            get
            {
                return dbName;
            }
            set
            {
                dbName = value;
            }
        }

        public string Password { get; set; }
        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get { return connection; } // returns a connection
        }


        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)// Checking if there are open connections to DB
                _instance = new DBConnection();
            return _instance;// Returns a new instance connection to the DB
        }
        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(dbName))
                    return false;
                string connstring = string.Format("Server=localhost; database={0}; UID=root; password=Michaelsantos1234", dbName);
                connection = new MySqlConnection(connstring);
                connection.Open();
            }

            return true;
        }

        public void Close()
        {
            connection.Close();
        }

        // truncate PlotValidation; -> To delete all the data from the table
        public void insertValidationMessage()
        {
            string connstring = string.Format("Server=localhost; database={0}; UID=root; password=Michaelsantos1234; Allow User Variables=True", dbName);
            connection = new MySqlConnection(connstring);
            connection.Open();

            RandomDataGenerator data = new RandomDataGenerator();
            
            try
            {
                    string query = "INSERT INTO PlotValidation VALUES (DEFAULT, ?IMEI,?ThingType,?TotalPlotsReviewed,?Missing, " +
                    "?LastUpdateTime, ?PayloadId, ?FeedProvider, ?LastMessagedReceived);";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.Add("?IMEI", MySqlDbType.Int64).Value = data.IMEI;
                        cmd.Parameters.Add("?ThingType", MySqlDbType.Int32).Value = data.thingType;
                        cmd.Parameters.Add("?TotalPlotsReviewed", MySqlDbType.Int32).Value = data.TotalPlotsReviewed;
                        cmd.Parameters.Add("?Missing", MySqlDbType.Decimal).Value = data.missing;
                        cmd.Parameters.Add("?LastUpdateTime", MySqlDbType.Timestamp).Value = data.lastUpdateTime;
                        cmd.Parameters.Add("?PayloadId", MySqlDbType.VarChar).Value = data.payload;
                        cmd.Parameters.Add("?FeedProvider", MySqlDbType.VarChar).Value = data.feedProvider;
                        cmd.Parameters.Add("?LastMessagedReceived", MySqlDbType.VarChar).Value = data.LastMessagedReceived;
                        cmd.ExecuteNonQuery();
                    }

                Close();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error in adding mysql row. Error: " + ex.Message);
                    Console.WriteLine();
                }
            }


            public void selectTable(int startPoint)
            {
            string connstring = string.Format("Server=localhost; database={0}; UID=root; password=Michaelsantos1234", dbName);
            connection = new MySqlConnection(connstring);
            connection.Open();

            int counter = 1;
            try
            {
                string query = "SELECT * FROM PlotValidation ORDER BY LastUpdateTime limit ?startPoint, 20 ;"; 
             
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.Add(new MySqlParameter("?startPoint ", startPoint));
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Console.WriteLine("---------------------------");
                        Console.WriteLine($"Vehicle {counter} details");
                        for (int i = 0; i <= 8; i++)
                        {
                            
                            Console.WriteLine($"{rdr.GetValue(i)}");
                            

                        }
                        counter++;
                        Console.WriteLine("---------------------------");
                    }
                }
               
                Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error in adding mysql row. Error: " + ex.Message);
            }
        }
    }
}

