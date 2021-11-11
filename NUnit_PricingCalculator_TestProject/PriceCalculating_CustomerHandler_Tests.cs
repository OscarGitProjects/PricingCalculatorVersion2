using NUnit.Framework;
using PricingCalculator.Handlers;
using PricingCalculator.Models;
using PricingCalculator.Services;
using System;

namespace NUnit_PricingCalculator_TestProject
{
    public class PriceCalculating_CustomerHandler_Tests
    {
        private ICustomerHandler m_CustomerHandler;

        [OneTimeSetUp]
        public void TestSetup()
        {
            m_CustomerHandler = new CustomerHandler();
        }


        [SetUp]
        public void Setup()
        {
        }


        #region Test av GetDiscount

        /// <summary>
        /// Test kontrollerar att ArgumentNullException kastas när referensen till customer är null
        /// </summary>
        [Test]
        public void PriceCalculating_CustomerHandler_GetDiscount_Customer_null_Test()
        {
            // Arrange
            // expected
            Customer customer = null;

            // Act
            // actual
            Assert.Throws<ArgumentNullException>(() => this.m_CustomerHandler.GetDiscount(CallingService.SERVICE_A, customer));

            // Assert
        }


        /// <summary>
        /// Test kontrollerar att om jag försöker hämta discount från en customer som inte har discount för en service.
        /// Så skall null returneras
        /// </summary>
        [Test]
        public void PriceCalculating_CustomerHandler_GetDiscount_Customer_Wrong_Service_Test()
        {
            // Arrange
            // expected
            double dblExpectedDiscount = 10.0;
            Customer customer = new Customer(1, "Test costumer 1");
            customer.NumberOfFreeDays = 0;

            PriceCalculatorServiceInformation priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_B;
            priceInformation.OnlyWorkingDays = false;

            Discount discount = new Discount();
            discount.DiscountInPercent = dblExpectedDiscount;
            discount.HasDiscount = true;
            discount.HasDiscountForAPeriod = true;
            discount.StartDate = new DateTime(2021, 11, 08);
            discount.EndDate = new DateTime(2021, 11, 11);

            priceInformation.Discount = discount;

            customer.AddPriceCalculatorServiceInformation(priceInformation);


            // Act
            // actual
            Discount actualDiscount = this.m_CustomerHandler.GetDiscount(CallingService.SERVICE_A, customer);

            // Assert
            Assert.IsNull(actualDiscount);
        }

        /// <summary>
        /// Test kontrollerar att vi får tillbaka korrekta discont data från ett customer objekt
        /// </summary>
        [Test]
        public void PriceCalculating_CustomerHandler_GetDiscount_From_Customer_Test()
        {
            // Arrange
            // expected
            double dblExpectedDiscount = 10.0;
            Customer customer = new Customer(1, "Test costumer 1");
            customer.NumberOfFreeDays = 0;

            PriceCalculatorServiceInformation priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_A;
            priceInformation.OnlyWorkingDays = false;

            Discount discount = new Discount();
            discount.DiscountInPercent = dblExpectedDiscount;
            discount.HasDiscount = true;
            discount.HasDiscountForAPeriod = true;
            discount.StartDate = new DateTime(2021, 11, 08);
            discount.EndDate = new DateTime(2021, 11, 11);

            priceInformation.Discount = discount;

            customer.AddPriceCalculatorServiceInformation(priceInformation);


            // Act
            // actual
            Discount actualDiscount = this.m_CustomerHandler.GetDiscount(CallingService.SERVICE_A, customer);

            // Assert
            Assert.IsNotNull(actualDiscount);
            Assert.AreEqual(dblExpectedDiscount, actualDiscount.DiscountInPercent);
            Assert.IsTrue(actualDiscount.HasDiscount);
            Assert.IsTrue(actualDiscount.HasDiscountForAPeriod);
            Assert.AreEqual(new DateTime(2021, 11, 08), actualDiscount.StartDate);
            Assert.AreEqual(new DateTime(2021, 11, 11), actualDiscount.EndDate);
        }

        #endregion  // End of region Test av GetDiscount

        #region Test av GetCostForService

        /// <summary>
        /// Test kontrollerar att ArgumentNullException kastas när referensen till customer är null
        /// </summary>
        [Test]
        public void PriceCalculating_CustomerHandler_GetCostForService_Customer_null_Test()
        {
            // Arrange
            // expected
            Customer customer = null;

            // Act
            // actual
            Assert.Throws<ArgumentNullException>(() => this.m_CustomerHandler.GetCostForService(CallingService.SERVICE_A, customer));

            // Assert
        }


        /// <summary>
        /// Test kontrollerar att om jag försöker hämta CostForService från en customer som inte har CostForService för en service.
        /// Så skall null returneras
        /// </summary>
        [Test]
        public void PriceCalculating_CustomerHandler_GetCostForService_Customer_Wrong_Service_Test()
        {
            // Arrange
            // expected
            double dblExpectedCost = 10.0;
            Customer customer = new Customer(1, "Test costumer 1");
            customer.NumberOfFreeDays = 0;

            PriceCalculatorServiceInformation priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_B;
            priceInformation.OnlyWorkingDays = false;

            CostForService costForService = new CostForService();
            costForService.Cost = dblExpectedCost;
            costForService.HasItsOwnCostForService = true;

            priceInformation.CostForService = costForService;

            customer.AddPriceCalculatorServiceInformation(priceInformation);


            // Act
            // actual
            CostForService actualCostForService = this.m_CustomerHandler.GetCostForService(CallingService.SERVICE_A, customer);

            // Assert
            Assert.IsNull(actualCostForService);
        }

        /// <summary>
        /// Test kontrollerar att vi får tillbaka korrekta CostForService data från ett customer objekt
        /// </summary>
        [Test]
        public void PriceCalculating_CustomerHandler_GetCostForService_From_Customer_Test()
        {
            // Arrange
            // expected
            double dblExpectedCost = 10.0;
            Customer customer = new Customer(1, "Test costumer 1");
            customer.NumberOfFreeDays = 0;

            PriceCalculatorServiceInformation priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_A;
            priceInformation.OnlyWorkingDays = false;

            CostForService costForService = new CostForService();
            costForService.Cost = dblExpectedCost;
            costForService.HasItsOwnCostForService = true;

            priceInformation.CostForService = costForService;

            customer.AddPriceCalculatorServiceInformation(priceInformation);


            // Act
            // actual
            CostForService actualCostForService = this.m_CustomerHandler.GetCostForService(CallingService.SERVICE_A, customer);

            // Assert
            Assert.IsNotNull(actualCostForService);
            Assert.AreEqual(dblExpectedCost, actualCostForService.Cost);
            Assert.IsTrue(actualCostForService.HasItsOwnCostForService);
        }

        #endregion  // End of region test av GetCostForService

        #region Test av GetConfigValueStringBaseCost

        // "ServiceBaseCost:ServiceA"
        // "ServiceBaseCost:ServiceB"
        // "ServiceBaseCost:ServiceA"

        /// <summary>
        /// Test kontrollerar att ArgumentNullException kastas när referensen till customer är null
        /// </summary>
        [Test]
        public void PriceCalculating_CustomerHandler_GetConfigValueStringBaseCoste_Customer_null_Test()
        {
            // Arrange
            // expected
            Customer customer = null;

            // Act
            // actual
            Assert.Throws<ArgumentNullException>(() => this.m_CustomerHandler.GetConfigValueStringBaseCost(CallingService.SERVICE_A, customer));

            // Assert
        }


        /// <summary>
        /// Test kontrollerar att om jag försöker hämta CostForService från en customer som inte har CostForService för en service.
        /// Så skall enpty string returneras
        /// </summary>
        [Test]
        public void PriceCalculating_CustomerHandler_GetConfigValueStringBaseCost_Customer_Wrong_Service_Test()
        {
            // Arrange
            // expected
            string strExpectedString = "ServiceBaseCost:ServiceB";
            Customer customer = new Customer(1, "Test costumer 1");
            customer.NumberOfFreeDays = 0;

            PriceCalculatorServiceInformation priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_B;
            priceInformation.OnlyWorkingDays = false;
            priceInformation.ConfigValueStringBaseCost = strExpectedString;

            customer.AddPriceCalculatorServiceInformation(priceInformation);


            // Act
            // actual
            string strActualString = this.m_CustomerHandler.GetConfigValueStringBaseCost(CallingService.SERVICE_A, customer);

            // Assert
            Assert.IsEmpty(strActualString);
        }

        /// <summary>
        /// Test kontrollerar att vi får tillbaka korrekta CostForService data från ett customer objekt
        /// </summary>
        [Test]
        public void PriceCalculating_CustomerHandler_GetConfigValueStringBaseCost_From_Customer_Test()
        {
            // Arrange
            // expected
            string strExpectedString = "ServiceBaseCost:ServiceB";
            Customer customer = new Customer(1, "Test costumer 1");
            customer.NumberOfFreeDays = 0;

            PriceCalculatorServiceInformation priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_B;
            priceInformation.OnlyWorkingDays = false;
            priceInformation.ConfigValueStringBaseCost = strExpectedString;

            customer.AddPriceCalculatorServiceInformation(priceInformation);


            // Act
            // actual
            string strActualString = this.m_CustomerHandler.GetConfigValueStringBaseCost(CallingService.SERVICE_B, customer);

            // Assert
            Assert.AreEqual(strExpectedString, strActualString);
        }

        #endregion  // End of region test av GetConfigValueStringBaseCost

        #region Test av OnlyWorkingDays

        /// <summary>
        /// Test kontrollerar att ArgumentNullException kastas när referensen till customer är null
        /// </summary>
        [Test]
        public void PriceCalculating_CustomerHandler_OnlyWorkingDays_Customer_null_Test()
        {
            // Arrange
            // expected
            Customer customer = null;

            // Act
            // actual
            Assert.Throws<ArgumentNullException>(() => this.m_CustomerHandler.OnlyWorkingDays(CallingService.SERVICE_A, customer));

            // Assert
        }


        /// <summary>
        /// Test kontrollerar att om jag försöker hämta OnlyWorkingDays från en customer som inte har OnlyWorkingDays för en service.
        /// Så skall false returneras
        /// </summary>
        [Test]
        public void PriceCalculating_CustomerHandler_OnlyWorkingDays_Customer_Wrong_Service_Test()
        {
            // Arrange
            // expected
            Customer customer = new Customer(1, "Test costumer 1");
            customer.NumberOfFreeDays = 0;

            PriceCalculatorServiceInformation priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_B;
            priceInformation.OnlyWorkingDays = false;
            priceInformation.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceB";

            customer.AddPriceCalculatorServiceInformation(priceInformation);

            // Act
            // actual
            bool bActualOnlyWorkingDays = this.m_CustomerHandler.OnlyWorkingDays(CallingService.SERVICE_A, customer);

            // Assert
            Assert.IsFalse(bActualOnlyWorkingDays);
        }

        /// <summary>
        /// Test kontrollerar att vi får tillbaka korrekta OnlyWorkingDays data från ett customer objekt
        /// </summary>
        [Test]
        public void PriceCalculating_CustomerHandler_OnlyWorkingDays_From_Customer_Test()
        {
            // Arrange
            // expected
            string strExpectedString = "ServiceBaseCost:ServiceB";
            Customer customer = new Customer(1, "Test costumer 1");
            customer.NumberOfFreeDays = 0;

            PriceCalculatorServiceInformation priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_B;
            priceInformation.OnlyWorkingDays = true;
            priceInformation.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceB";

            customer.AddPriceCalculatorServiceInformation(priceInformation);

            // Act
            // actual
            bool bActualOnlyWorkingDays = this.m_CustomerHandler.OnlyWorkingDays(CallingService.SERVICE_B, customer);

            // Assert
            Assert.IsTrue(bActualOnlyWorkingDays);
        }

        #endregion  // End of region test av OnlyWorkingDays

        #region Test av CanUseService

        /// <summary>
        /// Test kontrollerar att ArgumentNullException kastas när referensen till customer är null
        /// </summary>
        [Test]
        public void PriceCalculating_CustomerHandler_CanUseService_Customer_null_Test()
        {
            // Arrange
            // expected
            Customer customer = null;

            // Act
            // actual
            Assert.Throws<ArgumentNullException>(() => this.m_CustomerHandler.CanUseService(CallingService.SERVICE_A, customer));

            // Assert
        }


        /// <summary>
        /// Test kontrollerar att om jag försöker hämta CanUseService från en customer som inte har CanUseService för en service.
        /// Så skall false returneras
        /// </summary>
        [Test]
        public void PriceCalculating_CustomerHandler_CanUseService_Customer_Wrong_Service_Test()
        {
            // Arrange
            // expected
            Customer customer = new Customer(1, "Test costumer 1");
            customer.NumberOfFreeDays = 0;

            PriceCalculatorServiceInformation priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_B;
            priceInformation.OnlyWorkingDays = false;
            priceInformation.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceB";
            priceInformation.StartDate = DateTime.Now.AddDays(-1);

            customer.AddPriceCalculatorServiceInformation(priceInformation);

            // Act
            // actual
            bool bActualCanUseService = this.m_CustomerHandler.CanUseService(CallingService.SERVICE_A, customer);

            // Assert
            Assert.IsFalse(bActualCanUseService);
        }

        /// <summary>
        /// Test kontrollerar att vi får tillbaka korrekta CanUseService data från ett customer objekt
        /// Testar när startdatum för servicen är i dag
        /// </summary>
        [Test]
        public void PriceCalculating_CustomerHandler_CanUseService_From_Customer_Test()
        {
            // Arrange
            // expected
            string strExpectedString = "ServiceBaseCost:ServiceB";
            Customer customer = new Customer(1, "Test costumer 1");
            customer.NumberOfFreeDays = 0;

            PriceCalculatorServiceInformation priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_B;
            priceInformation.OnlyWorkingDays = true;
            priceInformation.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceB";
            priceInformation.StartDate = DateTime.Now;

            customer.AddPriceCalculatorServiceInformation(priceInformation);

            // Act
            // actual
            bool bActualCanUseService = this.m_CustomerHandler.CanUseService(CallingService.SERVICE_B, customer);

            // Assert
            Assert.IsTrue(bActualCanUseService);
        }


        /// <summary>
        /// Test kontrollerar att vi får tillbaka korrekta CanUseService data från ett customer objekt
        /// Testar när startdatum för servicen är om en dag
        /// </summary>
        [Test]
        public void PriceCalculating_CustomerHandler_CanUseService_From_Customer_When_StartDate_Is_In_The_Future_Test()
        {
            // Arrange
            // expected
            string strExpectedString = "ServiceBaseCost:ServiceB";
            Customer customer = new Customer(1, "Test costumer 1");
            customer.NumberOfFreeDays = 0;

            PriceCalculatorServiceInformation priceInformation = new PriceCalculatorServiceInformation();
            priceInformation.CallingService = CallingService.SERVICE_B;
            priceInformation.OnlyWorkingDays = true;
            priceInformation.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceB";
            priceInformation.StartDate = DateTime.Now.AddDays(1);

            customer.AddPriceCalculatorServiceInformation(priceInformation);

            // Act
            // actual
            bool bActualCanUseService = this.m_CustomerHandler.CanUseService(CallingService.SERVICE_B, customer);

            // Assert
            Assert.IsFalse(bActualCanUseService);
        }

        #endregion  // End of region test av CanUseService

        #region Test kontrollerar att AddPriceCalculatorServiceInformationt fungera

        /// <summary>
        /// Test kontrollerar att AddPriceCalculatorServiceInformationt fungera
        /// Kontroll av att vi fårkorrekt antal PriceCalculatorServiceInformationt från customer objektet
        /// </summary>
        [Test]
        public void PriceCalculating_CustomerHandler_AddPriceCalculatorServiceInformation_Add_Objects_Test()
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

        #endregion  // End of region Test kontrollerar att AddPriceCalculatorServiceInformationt fungera

    }
}
