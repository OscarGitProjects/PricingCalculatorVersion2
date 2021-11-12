using NUnit.Framework;
using PricingCalculator.Models;
using PricingCalculator.Services;
using System;
using System.Linq;

namespace NUnit_PricingCalculator_TestProject
{
    public class PriceCalculation_Customer_Tests
    {

        [OneTimeSetUp]
        public void TestSetup()
        {
        }


        [SetUp]
        public void Setup()
        {
        }

        #region Test kontrollerar att AddPriceCalculatorServiceInformation fungera

        /// <summary>
        /// Test kontrollerar att AddPriceCalculatorServiceInformationt fungera
        /// Kontroll av att vi fårkorrekt antal PriceCalculatorServiceInformationt från customer objektet
        /// </summary>
        [Test]
        public void PriceCalculating_Customer_AddPriceCalculatorServiceInformation_Add_Objects_Test()
        {
            // Arrange
            // expected
            string strExpectedString = "ServiceBaseCost:ServiceB";
            Customer customer = new Customer(1, "Test costumer 1");
            customer.NumberOfFreeDays = 0;

            PriceCalculatorServiceInformation priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_A;
            priceInformation.OnlyWorkingDays = true;
            priceInformation.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceA";
            priceInformation.StartDate = DateTime.Now.AddDays(1);

            customer.AddPriceCalculatorServiceInformation(priceInformation);


            priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_B;
            priceInformation.OnlyWorkingDays = true;
            priceInformation.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceB";
            priceInformation.StartDate = DateTime.Now.AddDays(1);

            customer.AddPriceCalculatorServiceInformation(priceInformation);


            // Act
            // actual
            int iActualNumberOfPriceCalculatorServiceInformationObjects = customer.PriceCalculatorServiceInformation.Count;

            // Assert
            Assert.AreEqual(2, iActualNumberOfPriceCalculatorServiceInformationObjects);
        }


        /// <summary>
        /// Test kontrollerar att när man föröker lägga till PriceCalculatorServiceInformation för samma service. 
        /// Då raderas först det gamla objektet och sen läggs det nya objektet till i listan
        /// </summary>
        [Test]
        public void PriceCalculating_Customer_AddPriceCalculatorServiceInformation_Add_Objects_For_Same_Servis_Test()
        {
            // Arrange
            // expected
            string strExpectedString = "ServiceBaseCost:ServiceB";
            Customer customer = new Customer(1, "Test costumer 1");
            customer.NumberOfFreeDays = 0;

            PriceCalculatorServiceInformation priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_A;
            priceInformation.OnlyWorkingDays = true;
            priceInformation.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceA";
            priceInformation.StartDate = DateTime.Now.AddDays(1);

            customer.AddPriceCalculatorServiceInformation(priceInformation);


            priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_A;
            priceInformation.OnlyWorkingDays = true;
            priceInformation.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceA";
            priceInformation.StartDate = DateTime.Now.AddDays(1);

            customer.AddPriceCalculatorServiceInformation(priceInformation);


            // Act
            // actual
            int iActualNumberOfPriceCalculatorServiceInformationObjects = customer.PriceCalculatorServiceInformation.Count;

            // Assert
            Assert.AreEqual(1, iActualNumberOfPriceCalculatorServiceInformationObjects);
        }

        #endregion  // End of region Test kontrollerar att AddPriceCalculatorServiceInformation fungera

        #region Test kontrollerar att RemovePriceCalculatorServiceInformation fungera

        [Test]
        public void PriceCalculating_Customer_RemovePriceCalculatorServiceInformation_Remove_Object_Test()
        {
            // Arrange
            // expected
            string strExpectedString = "ServiceBaseCost:ServiceB";
            Customer customer = new Customer(1, "Test costumer 1");
            customer.NumberOfFreeDays = 0;

            PriceCalculatorServiceInformation priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_A;
            priceInformation.OnlyWorkingDays = true;
            priceInformation.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceA";
            priceInformation.StartDate = DateTime.Now.AddDays(1);

            customer.AddPriceCalculatorServiceInformation(priceInformation);


            PriceCalculatorServiceInformation priceInformation2 = new PriceCalculatorServiceInformation();
            priceInformation2.CallingService = CallingService.SERVICE_B;
            priceInformation2.OnlyWorkingDays = true;
            priceInformation2.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceB";
            priceInformation2.StartDate = DateTime.Now.AddDays(1);

            customer.AddPriceCalculatorServiceInformation(priceInformation2);


            // Act
            // actual
            int iActualNumberOfPriceCalculatorServiceInformationObjectsBeforeRemoving = customer.PriceCalculatorServiceInformation.Count;

            // Assert
            Assert.AreEqual(2, iActualNumberOfPriceCalculatorServiceInformationObjectsBeforeRemoving);

            // Radera ett PriceCalculatorServiceInformation från listan
            customer.RemovePriceCalculatorServiceInformation(priceInformation);

            // Act
            // actual
            int iActualNumberOfPriceCalculatorServiceInformationObjectsAfterRemoving = customer.PriceCalculatorServiceInformation.Count;
            var actualPriceInformationElement = customer.PriceCalculatorServiceInformation.ElementAt(0);

            // Assert
            Assert.AreEqual(1, iActualNumberOfPriceCalculatorServiceInformationObjectsAfterRemoving);

            // Kontrollera att det är rätt objelt som finns kvar i listan med PriceCalculatorServiceInformation
            Assert.AreEqual(priceInformation2.CallingService, actualPriceInformationElement.CallingService);
            Assert.AreEqual(priceInformation2.OnlyWorkingDays, actualPriceInformationElement.OnlyWorkingDays);
            Assert.AreEqual(priceInformation2.ConfigValueStringBaseCost, actualPriceInformationElement.ConfigValueStringBaseCost);
            Assert.AreEqual(priceInformation2.StartDate, actualPriceInformationElement.StartDate);
        }

        #endregion  // End of region Test kontrollerar att RemovePriceCalculatorServiceInformation fungera

    }
}
