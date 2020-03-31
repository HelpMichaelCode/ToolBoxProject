using System;
using Toolbox.Database;
namespace Toolbox
{
    class Program
   {
        static void Main(string[] args)
        {
            var dbCob = DBConnection.Instance();
            dbCob.DatabaseName = "sys";
            try
            {
                if (dbCob.IsConnect())
                {
                    Console.WriteLine("Connection Success!");
                    dbCob.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);

            }



            // Inserts 200 rows into table
            //for (int i = 0; i < 2000; i++)
            //{
            //    dbCob.fillTable();
            //}


            //int startPoint = 0;
            //dbCob.selectTable(startPoint);
            //Console.WriteLine("\nPress 'Spacebar' to load more data...");

            //while (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
            //{ 
            //    startPoint += 20;
            //    dbCob.selectTable(startPoint);

            //}



        }
    }
}
