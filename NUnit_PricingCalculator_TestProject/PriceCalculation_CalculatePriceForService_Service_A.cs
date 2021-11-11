﻿using NUnit.Framework;
using PricingCalculator.Handlers;
using PricingCalculator.Models;
using PricingCalculator.Services;
using System;

namespace NUnit_PricingCalculator_TestProject
{
    public class PriceCalculation_CalculatePriceForService_Service_A : PricingCalculator_TestBase
    {        
        [OneTimeSetUp]
        public void TestSetup()
        {
            var test = this.BuildConfiguration(TestContext.CurrentContext.TestDirectory);
            this.m_PriceCalculateService = new PriceCalculateService(this.BuildConfiguration(TestContext.CurrentContext.TestDirectory), new CustomerHandler());
        }


        [SetUp]
        public void Setup()
        {
        }


        #region Test av Exceptions för metoden CalculatePriceForService

        /// <summary>
        /// Test kontrollerar att ArgumentNullException kastas om referensen till customer objektet är null
        /// </summary>
        [Test]
        public void PriceCalculateService_CalculatePriceForService_Customer_Referense_null_Test()
        {
            // Arrange
            // expected
            Customer customer = null;
            DateTime dtStartDate = DateTime.Now.AddDays(-2);
            DateTime dtEndDate = DateTime.Now;

            // Act
            // actual
            Assert.Throws<ArgumentNullException>(() => this.m_PriceCalculateService.CalculatePriceForService(CallingService.SERVICE_A, customer, dtStartDate, dtEndDate));

            // Assert
        }


        /// <summary>
        /// Test kontrollerar att ArgumentException kastas om Startdatum är efter slutdatum
        /// </summary>
        [Test]
        public void PriceCalculateService_CalculatePriceForService_StartDate_After_EndDate_Test()
        {
            // Arrange
            // expected
            Customer customer = new Customer(1, "Test 1");
            DateTime dtStartDate = DateTime.Now.AddDays(1);
            DateTime dtEndDate = DateTime.Now;

            // Act
            // actual
            Assert.Throws<ArgumentException>(() => this.m_PriceCalculateService.CalculatePriceForService(CallingService.SERVICE_A, customer, dtStartDate, dtEndDate));

            // Assert
        }

        #endregion  // End of region Test av Exceptions för metoden CalculatePriceForService

        #region Test av metoden CalculatePriceForService med eget satt pris utan rabatter

        /// <summary>
        /// Test kontrollerar att kostnaden blir riktig när man inte har några rabatter och använder egen satt kostnad i customer objektet
        /// </summary>
        [Test]
        public void PriceCalculateService_CalculatePriceForService_Customer_HasItsOwnCostForService_Test()
        {
            // Arrange
            // expected
            double dblExpectedCost = 123.0;

            Customer customer = new Customer(1, "Test 1");
            
            PriceCalculatorServiceInformation priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_A;
            priceInformation.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceA";
            priceInformation.OnlyWorkingDays = true;

            CostForService costForService = new CostForService { Cost = dblExpectedCost, HasItsOwnCostForService = true };
            priceInformation.CostForService = costForService;
            customer.AddPriceCalculatorServiceInformation(priceInformation);

            DateTime dtStartDate = new DateTime(2021, 11, 08);
            DateTime dtEndDate = dtStartDate.AddDays(2);

            // Act
            // actual
            double dblActualCost = this.m_PriceCalculateService.CalculatePriceForService(CallingService.SERVICE_A, customer, dtStartDate, dtEndDate);

            // Assert
            Assert.AreEqual(3 * dblExpectedCost, dblActualCost);
        }


        /// <summary>
        /// Test kontrollerar att kostnaden blir riktig när man inte har några rabatter har en gratisn dag och använder egen satt kostnad i customer objektet
        /// </summary>
        [Test]
        public void PriceCalculateService_CalculatePriceForService_Customer_HasItsOwnCostForService_And_1_Free_Day_Test()
        {
            // Arrange
            // expected
            double dblExpectedCost = 123.0;

            Customer customer = new Customer(1, "Test 1");
            customer.NumberOfFreeDays = 1;

            PriceCalculatorServiceInformation priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_A;
            priceInformation.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceA";
            priceInformation.OnlyWorkingDays = true;

            CostForService costForService = new CostForService { Cost = dblExpectedCost, HasItsOwnCostForService = true };
            priceInformation.CostForService = costForService;
            customer.AddPriceCalculatorServiceInformation(priceInformation);

            DateTime dtStartDate = new DateTime(2021, 11, 08);
            DateTime dtEndDate = dtStartDate.AddDays(2);

            // Act
            // actual
            double dblActualCost = this.m_PriceCalculateService.CalculatePriceForService(CallingService.SERVICE_A, customer, dtStartDate, dtEndDate);

            // Assert
            Assert.AreEqual(2 * dblExpectedCost, dblActualCost);
        }

        #endregion  // End of region Test av metoden CalculatePriceForService med eget satt pris utan rabatter

        #region Test av metoden CalculatePriceForService med pris från appsettings.json filen utan rabatter

        /// <summary>
        /// Test kontrollerar att kostnaden blir riktig när man inte har några rabatter och använder bas kostnader från config
        [Test]
        public void PriceCalculateService_CalculatePriceForServiceC_Customer_Use_base_cost_from_config_Test()
        {
            // Arrange
            // expected
            Customer customer = new Customer(1, "Test 1");
            double dblExpectedCost = 0.2;

            PriceCalculatorServiceInformation priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_A;
            priceInformation.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceA";
            priceInformation.OnlyWorkingDays = true;

            customer.AddPriceCalculatorServiceInformation(priceInformation);

            DateTime dtStartDate = new DateTime(2021, 11, 08);
            DateTime dtEndDate = dtStartDate.AddDays(2);

            // Act
            // actual
            double dblActualCost = this.m_PriceCalculateService.CalculatePriceForService(CallingService.SERVICE_A, customer, dtStartDate, dtEndDate);

            // Assert
            Assert.AreEqual(3 * dblExpectedCost, dblActualCost);
        }


        /// <summary>
        /// Test kontrollerar att kostnaden blir riktig när man inte har några rabatter har en gratis dag och använder bas kostnader från config
        /// </summary>
        [Test]
        public void PriceCalculateService_CalculatePriceForService_Customer_Use_base_cost_from_config_And_1_Free_Day_Test()
        {
            // Arrange
            // expected
            Customer customer = new Customer(1, "Test 1");
            double dblExpectedCost = 0.2;
            customer.NumberOfFreeDays = 1;

            PriceCalculatorServiceInformation priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_A;
            priceInformation.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceA";
            priceInformation.OnlyWorkingDays = true;

            customer.AddPriceCalculatorServiceInformation(priceInformation);

            DateTime dtStartDate = new DateTime(2021, 11, 08);
            DateTime dtEndDate = dtStartDate.AddDays(2);

            // Act
            // actual
            double dblActualCost = this.m_PriceCalculateService.CalculatePriceForService(CallingService.SERVICE_A, customer, dtStartDate, dtEndDate);

            // Assert
            Assert.AreEqual(2 * dblExpectedCost, dblActualCost);
        }

        #endregion  // End of region Test av metoden CalculatePriceForService med pris från appsettings.json filen utan rabatter

        #region Test av metoden CalculatePriceForService med pris från appsettings.json filen med rabatt

        /// <summary>
        /// Test kontrollerar att kostnaden blir korrekt med 1 dags rabatt
        /// </summary>
        [Test]
        public void PriceCalculateService_CalculatePriceForService_Customer_Use_base_cost_from_config_HasDiscount_1_day_Test()
        {
            // Arrange
            // expected
            double dblExpectedCost = 0.2;
            double dblExpectedDiscountInPercent = 10.0;

            Customer customer = new Customer(1, "Test 1");

            PriceCalculatorServiceInformation priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_A;
            priceInformation.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceA";
            priceInformation.OnlyWorkingDays = true;

            Discount discount = new Discount();
            discount.HasDiscount = true;
            discount.DiscountInPercent = dblExpectedDiscountInPercent;
            priceInformation.Discount = discount;

            customer.AddPriceCalculatorServiceInformation(priceInformation);

            //customer.DiscountForServiceA.HasDiscount = true;
            //customer.DiscountForServiceA.DiscountInPercent = 10.0;
            

            DateTime dtStartDate = new DateTime(2021, 11, 08);
            DateTime dtEndDate = dtStartDate;

            // Act
            // actual
            double dblActualCost = this.m_PriceCalculateService.CalculatePriceForService(CallingService.SERVICE_A, customer, dtStartDate, dtEndDate);

            // Assert
            double dblExpectedCost1 = dblExpectedCost * (1 - (double)(dblExpectedDiscountInPercent / Double.Parse("100,0")));
            Assert.AreEqual(dblExpectedCost1, dblActualCost);
        }


        /// <summary>
        /// Test kontrollerar att kostnaden blir korrekt med 2 dagars rabatt
        /// </summary>
        [Test]
        public void PriceCalculateService_CalculatePriceForService_Customer_Use_base_cost_from_config_HasDiscount_2_day_Test()
        {
            // Arrange
            // expected
            double dblExpectedCost = 0.2;
            double dblExpectedDiscountInPercent = 10.0;

            Customer customer = new Customer(1, "Test 1");

            PriceCalculatorServiceInformation priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_A;
            priceInformation.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceA";
            priceInformation.OnlyWorkingDays = true;

            Discount discount = new Discount();
            discount.HasDiscount = true;
            discount.DiscountInPercent = dblExpectedDiscountInPercent;
            priceInformation.Discount = discount;

            customer.AddPriceCalculatorServiceInformation(priceInformation);

            //customer.DiscountForServiceA.HasDiscount = true;
            //customer.DiscountForServiceA.DiscountInPercent = 10.0;

            DateTime dtStartDate = new DateTime(2021, 11, 08);
            DateTime dtEndDate = dtStartDate.AddDays(1);

            // Act
            // actual
            double dblActualCost = this.m_PriceCalculateService.CalculatePriceForService(CallingService.SERVICE_A, customer, dtStartDate, dtEndDate);

            // Assert
            double dblExpectedCost1 = dblExpectedCost * (double)(1 - (double)(dblExpectedDiscountInPercent / Double.Parse("100,0")));
            Assert.AreEqual(2 * dblExpectedCost1, dblActualCost);
        }


        /// <summary>
        /// Test kontrollerar att kostnaden blir korrekt med ett antal dagar som skall ha rabatt plus några dagar utan rabatt
        /// </summary>
        [Test]
        public void PriceCalculateService_CalculatePriceForService_Customer_Use_base_cost_from_config_HasDiscount_In_Period_Test()
        {
            // Arrange
            // expected
            double dblExpectedCost = 0.2;
            double dblExpectedDiscountInPercent = 10.0;

            Customer customer = new Customer(1, "Test 1");

            PriceCalculatorServiceInformation priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_A;
            priceInformation.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceA";
            priceInformation.OnlyWorkingDays = true;

            Discount discount = new Discount();
            discount.HasDiscount = true;
            discount.DiscountInPercent = dblExpectedDiscountInPercent;

            discount.HasDiscountForAPeriod = true;
            discount.StartDate = new DateTime(2021, 11, 06);
            discount.EndDate = new DateTime(2021, 11, 08);

            priceInformation.Discount = discount;

            customer.AddPriceCalculatorServiceInformation(priceInformation);

            DateTime dtStartDate = new DateTime(2021, 11, 06);
            DateTime dtEndDate = new DateTime(2021, 11, 11);

            // Act
            // actual
            double dblActualCost = this.m_PriceCalculateService.CalculatePriceForService(CallingService.SERVICE_A, customer, dtStartDate, dtEndDate);

            // Assert
            double dblExpectedCost1 = dblExpectedCost * (double)(1 - (double)(dblExpectedDiscountInPercent / Double.Parse("100,0")));

            Assert.AreEqual(dblExpectedCost1 + (3 * dblExpectedCost), dblActualCost);
        }

        #endregion  // End region Test av metoden CalculatePriceForService med pris från appsettings.json filen med rabatt
    }
}
