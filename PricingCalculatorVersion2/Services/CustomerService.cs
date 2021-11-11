using PricingCalculator.Models;
using PricingCalculator.Repository;
using System;

namespace PricingCalculator.Services
{
    /// <summary>
    /// Service för att hämta customer
    /// </summary>
    public class CustomerService : ICustomerService
    {
        /// <summary>
        /// Referens till en service där man hämtar information om en customer från repository
        /// </summary>
        private readonly ICustomerRepository m_CustomerRepository;


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="CustomerRepository">Referens till en service där man hämtar information om en customer från repository</param>
        public CustomerService(ICustomerRepository CustomerRepository)
        {
            this.m_CustomerRepository = CustomerRepository;
        }

        /// <summary>
        /// Metoden skapar två kunder. Customer X och Customer Y
        /// </summary>
        public void CreateCustomers()
        {
            if (this.m_CustomerRepository != null && this.m_CustomerRepository.NumberOfCustomers > 0)
                return; 

            Customer newCustomer = null;
            // "ServiceBaseCost:ServiceA"
            // "ServiceBaseCost:ServiceB"
            // "ServiceBaseCost:ServiceA"

            /* 
                Customer X started using Service A and Service C 2019-09-20. 
                Customer X also had an discount of 20% between 2019-09-22 and 2019-09-24 for Service C.
                What is the total price for Customer X up until 2019-10-01?
             */
            newCustomer = new Customer(1, "CustomerX " + 1);

            // Ny service
            PriceCalculatorServiceInformation serviceInformation = new PriceCalculatorServiceInformation();
            serviceInformation.CallingService = CallingService.SERVICE_A;
            serviceInformation.StartDate = new DateTime(2019, 09, 20);
            serviceInformation.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceA";

            Discount discount = new Discount();
            discount.HasDiscount = false;
            discount.HasDiscountForAPeriod = false;
            serviceInformation.Discount = discount;

            newCustomer.AddPriceCalculatorServiceInformation(serviceInformation);

            // Ny service
            serviceInformation = new PriceCalculatorServiceInformation();
            serviceInformation.CallingService = CallingService.SERVICE_C;
            serviceInformation.StartDate = new DateTime(2019, 09, 20);
            serviceInformation.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceC";

            discount = new Discount();
            discount.DiscountInPercent = 20.0;
            discount.StartDate = new DateTime(2019, 09, 22);
            discount.EndDate = new DateTime(2019, 09, 24);
            discount.HasDiscount = true;
            discount.HasDiscountForAPeriod = true;
            serviceInformation.Discount = discount;

            newCustomer.AddPriceCalculatorServiceInformation(serviceInformation);

            this.m_CustomerRepository.AddCustomer(newCustomer);

            /* 
                Customer Y started using Service B and Service C 2018-01-01. 
                Customer Y had 200 free days and a discount of 30% for the rest of the time.
                What is the total price for Customer Y up until 2019-10-01?
            */
            newCustomer = new Customer(2, "CustomerY " + 2);
            newCustomer.NumberOfFreeDays = 200;

            // Ny service
            serviceInformation = new PriceCalculatorServiceInformation();
            serviceInformation.CallingService = CallingService.SERVICE_C;
            serviceInformation.StartDate = new DateTime(2018, 01, 01);
            serviceInformation.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceC";

            discount = new Discount();
            discount.DiscountInPercent = 30.0;
            discount.HasDiscount = true;
            discount.HasDiscountForAPeriod = false;
            serviceInformation.Discount = discount;

            newCustomer.AddPriceCalculatorServiceInformation(serviceInformation);

            // Ny service
            serviceInformation = new PriceCalculatorServiceInformation();
            serviceInformation.CallingService = CallingService.SERVICE_B;
            serviceInformation.StartDate = new DateTime(2018, 01, 01);
            serviceInformation.ConfigValueStringBaseCost = "ServiceBaseCost:ServiceB";

            discount = new Discount();
            discount.HasDiscount = false;
            discount.HasDiscountForAPeriod = false;
            serviceInformation.Discount = discount;

            newCustomer.AddPriceCalculatorServiceInformation(serviceInformation);

            this.m_CustomerRepository.AddCustomer(newCustomer);
        }


        /// <summary>
        /// Metoden hämtar sökt customer från repository
        /// </summary>
        /// <param name="iCustomerId">Id för sökt customer</param>
        /// <returns>Sökt customer eller null</returns>
        public Customer GetCustomer(int iCustomerId)
        {
            return this.m_CustomerRepository.GetCustomer(iCustomerId);
        }
    }
}