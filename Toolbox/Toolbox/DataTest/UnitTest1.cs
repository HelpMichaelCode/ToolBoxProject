using System;
using NUnit.Framework;
using Toolbox.Logic;

namespace DataTest
{
    public class Tests
    {
        RandomDataGenerator obj = new RandomDataGenerator();
        [Test]
        public void TestDataType()
        {
            // Arrange
            var expectedIMEI_Type = typeof(long);
            var expectedThingType = typeof(int);
            var expectedTotalPlotsReviewed = typeof(int);
            var expectedMissing = typeof(float);
            var expectedLastUpdateTime = typeof(DateTime);
            var expectedPayload = typeof(string);
            var expectedFeedProvider = typeof(string);
            var expectedLastMessagedReceived = typeof(string);
            // Act
           
            // Assert

            Assert.AreEqual(expectedIMEI_Type, obj.IMEI.GetType());
            Assert.AreEqual(expectedThingType, obj.thingType.GetType());
            Assert.AreEqual(expectedTotalPlotsReviewed, obj.TotalPlotsReviewed.GetType());
            Assert.AreEqual(expectedMissing, obj.missing.GetType());
            Assert.AreEqual(expectedLastUpdateTime, obj.lastUpdateTime.GetType());
            Assert.AreEqual(expectedPayload, obj.payload.GetType());
            Assert.AreEqual(expectedFeedProvider, obj.feedProvider.GetType());
            Assert.AreEqual(expectedLastMessagedReceived, obj.LastMessagedReceived.GetType());
        }

        [Test]
        public void TestDataRange()
        {
            Assert.True(obj.checkIMEIRange());
            Assert.True(obj.checkDateRange());
            Assert.True(obj.checkMissingRange());
            Assert.True(obj.checkTotalPlotsReviewedRange());
        }
    }
}