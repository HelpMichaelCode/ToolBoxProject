using System;
using System.Data;
// Connector to the DB 
using MySql.Data.MySqlClient;
using System.Collections.Generic;
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

        
        
        private long IMEI;
        private int thingType;
        private int TotalPlotsReviewed;
        private float missing;
        DateTime lastUpdateTime;
        private string payload;
        // fp -> Feed Provider
        string[] fpOption = { "Automation", "Direct Reveal EU", "Direct Reveal US" };
        private string feedProvider;
        // lmr - > Last Message Received
        string[] lmrOption = { "Message validation failed", "Message Processed", "Message Discarded coming from the past"};
        private string LastMessagedReceived;

        Random rand = new Random();
        Guid uuid = Guid.NewGuid();
        // Method to generate random double values between 0.00 - 100.00
        public static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        // truncate PlotValidation; -> To delete all the data from the table
        public void fillTable()
        {
            
            string connstring = string.Format("Server=localhost; database={0}; UID=root; password=Michaelsantos1234; Allow User Variables=True", dbName);
            connection = new MySqlConnection(connstring);
            connection.Open();

            // Generating Values for variables
            IMEI = rand.Next(1000, 10000) + 1;
            thingType = rand.Next(1, 10) + 1;
            TotalPlotsReviewed = rand.Next(1, 20000) + 1;
            missing = (float)GetRandomNumber(0.00, 100.00);
            string startDate = "01/03/2020"; 
            DateTime now = DateTime.Now;
            DateTime startD = DateTime.Parse(startDate);
            // Creating a list of time stamp
            List<DateTime> allDates = new List<DateTime>();

            // Loops from the specified until the date now, and it iterates
            // based on the day adding one.
            for (DateTime date = startD; date <= now; date = date.AddDays(1))
            {
                // Adding it to the allDates lists
                allDates.Add(date);

            }
            // Converting allDates to an array
            allDates.ToArray();

            lastUpdateTime = allDates[rand.Next(0, allDates.ToArray().Length)].Date.AddSeconds(rand.Next(60 * 60 * 24)); 
            
            payload = uuid.ToString();
            feedProvider = fpOption[rand.Next(0, 3)];
            LastMessagedReceived = lmrOption[rand.Next(0, 3)];

            try
            {
                    string query = "INSERT INTO PlotValidation VALUES (DEFAULT, ?IMEI,?ThingType,?TotalPlotsReviewed,?Missing, " +
                    "?LastUpdateTime, ?PayloadId, ?FeedProvider, ?LastMessagedReceived);";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.Add("?IMEI", MySqlDbType.Int64).Value = IMEI;
                        cmd.Parameters.Add("?ThingType", MySqlDbType.Int32).Value = thingType;
                        cmd.Parameters.Add("?TotalPlotsReviewed", MySqlDbType.Int32).Value = TotalPlotsReviewed;
                        cmd.Parameters.Add("?Missing", MySqlDbType.Decimal).Value = missing;
                        cmd.Parameters.Add("?LastUpdateTime",MySqlDbType.Timestamp).Value = lastUpdateTime;
                        cmd.Parameters.Add("?PayloadId", MySqlDbType.VarChar).Value = payload;
                        cmd.Parameters.Add("?FeedProvider", MySqlDbType.VarChar).Value = feedProvider;
                        cmd.Parameters.Add("?LastMessagedReceived", MySqlDbType.VarChar).Value = LastMessagedReceived;
                        cmd.ExecuteNonQuery();
                   
                }

                Close();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error in adding mysql row. Error: " + ex.Message);
                    Console.WriteLine("Value ->  " + lastUpdateTime);
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

