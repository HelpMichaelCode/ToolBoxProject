using System;
using System.Collections.Generic;
using Toolbox.Contracts;

namespace Toolbox.Logic
{

    public class RandomDataGenerator : IPlotValidationStorage
    {
        public static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        Random randNum = new Random();
        Guid uuid = Guid.NewGuid();

        public long IMEI { get; set; }
        public int thingType { get; set; }
        public int TotalPlotsReviewed { get; set; }
        public float missing { get; set; }
        public DateTime lastUpdateTime { get; set; }
        public string payload { get; set; }
        // fp -> Feed Provider 
        public string[] fpOption = { "Direct Reveal EU", "Direct Reveal US", "Automation" };
        public string feedProvider { get; set; }
        // lmr - > Last Message Received
        public string[] lmrOption = { "Message Processed", "Message Discarded coming from the past", "Message validation failed" };
        public string LastMessagedReceived { get; set; }
        DateTime now = DateTime.Now;
        DateTime startD = DateTime.Parse("01/03/2020");
        // Default constructor

        public RandomDataGenerator()
        {
            // Generating Values for variables
            IMEI = randNum.Next(1000, 10000) + 1;

            thingType = randNum.Next(1, 10) + 1;

            TotalPlotsReviewed = randNum.Next(1, 20000) + 1;

            missing = (float)GetRandomNumber(0.00, 100.00);

            List<DateTime> allDates = new List<DateTime>();
            for (DateTime date = startD; date <= now; date = date.AddDays(1))
            {
                allDates.Add(date);
            }
            allDates.ToArray();
            lastUpdateTime = allDates[randNum.Next(0, allDates.ToArray().Length)].Date.AddSeconds(randNum.Next(60 * 60 * 24));

            payload = uuid.ToString();

            feedProvider = fpOption[randNum.Next(0, 3)];

            LastMessagedReceived = lmrOption[randNum.Next(0, 3)];
        }

        // Check ranges on date, missing, totalPlotsReviewed, IMEI
        public bool checkIMEIRange()
        {
            if (this.IMEI >= 1000 && this.IMEI <= 10000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkDateRange()
        {
            if(this.lastUpdateTime >= startD && this.lastUpdateTime <= now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkMissingRange()
        {
            if (this.missing >= 0.00 && this.missing <= 100.00)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkTotalPlotsReviewedRange()
        {
            if (this.TotalPlotsReviewed > 1 && this.TotalPlotsReviewed <= 20000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
        

