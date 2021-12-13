using PricingCalculator.Models;
using PricingCalculator.Services;
using System;

namespace PricingCalculator.Handlers
{
    public class CustomerHandler : ICustomerHandler
    {
        /// <summary>
        /// Metoden returnerar objekt med rabatt för användning av vald Service
        /// </summary>
        /// <param name="callingService">Vilken tjänst är det som anropas</param>
        /// <param name="customer">Referens till customer objekt</param>
        /// <returns>Discount objekt. Om service inte är vald returneras null</returns>
        /// <exception cref="ArgumentNullException">Kastats om referensen till CallingService objektet inte satt dvs. CallingService.NA</exception>
        /// <exception cref="ArgumentNullException">Kastats om referensen till Customer objektet är null</exception>
        public Discount GetDiscount(CallingService callingService, Customer customer)
        {
            if (callingService == CallingService.NA)
                throw new ArgumentNullException(nameof(callingService), "CustomerHandler->GetDiscount(). Referensen till CallingService objektet är inte satt dvs. CallingService.NA");

            if (customer == null)
                throw new ArgumentNullException(nameof(customer), "CustomerHandler->GetDiscount(). Referensen till Customer objektet är null"); 

            return customer.GetDiscount(callingService);
        }


        /// <summary>
        /// Metoden returnera objekt med kostnaden för att använda en vald service
        /// </summary>
        /// <param name="callingService">Vilken tjänst är det som anropas</param>
        /// <param name="customer">Referens till customer objekt</param>
        /// <returns>CostForService objekt. Om service inte är vald returneras null</returns>
        /// <exception cref="ArgumentNullException">Kastats om referensen till CallingService objektet inte satt dvs. CallingService.NA</exception>
        /// <exception cref="ArgumentNullException">Kastats om referensen till Customer objektet är null</exception>
        public CostForService GetCostForService(CallingService callingService, Customer customer)
        {
            if (callingService == CallingService.NA)
                throw new ArgumentNullException(nameof(callingService), "CustomerHandler->GetCostForService(). Referensen till CallingService objektet är inte satt dvs. CallingService.NA");

            if (customer == null)
                throw new ArgumentNullException(nameof(customer), "CustomerHandler->GetCostForService(). Referensen till Customer objektet är null");

            return customer.GetCostForService(callingService);
        }


        /// <summary>
        /// Metoden returnera den string med vilket json element man skall hämta från appsettings.json filen
        /// </summary>
        /// <param name="callingService">Vilken tjänst är det som anropas</param>
        /// <param name="customer">Referens till customer objekt</param>
        /// <returns>json element som skall hämtas från appsettings.json. Om service inte är vald returneras en tom sträng</returns>
        /// <exception cref="ArgumentNullException">Kastats om referensen till CallingService objektet inte satt dvs. CallingService.NA</exception>
        /// <exception cref="ArgumentNullException">Kastats om referensen till Customer objektet är null</exception>
        public string GetConfigValueStringBaseCost(CallingService callingService, Customer customer)
        {
            if (callingService == CallingService.NA)
                throw new ArgumentNullException(nameof(callingService), "CustomerHandler->GetConfigValueStringBaseCost(). Referensen till CallingService objektet är inte satt dvs. CallingService.NA");

            if (customer == null)
                throw new ArgumentNullException(nameof(customer), "CustomerHandler->GetConfigValueStringBaseCost(). Referensen till Customer objektet är null");

            return customer.GetConfigValueStringBaseCost(callingService);
        }


        /// <summary>
        /// true om vi bara skall ta betalt för arbetsdagar. Annars retruneras false
        /// Vilket som skall användas beror på vilken service som används
        /// </summary>
        /// <param name="callingService">Vilken tjänst är det som anropas</param>
        /// <param name="customer">Referens till customer objekt</param>
        /// <returns>true om vi bara skall ta betalt för arbetsdagar. Annars retruneras false. Om service inte är vald returneras false</returns>
        /// <exception cref="ArgumentNullException">Kastats om referensen till CallingService objektet inte satt dvs. CallingService.NA</exception>
        /// <exception cref="ArgumentNullException">Kastats om referensen till Customer objektet är null</exception>
        public bool OnlyWorkingDays(CallingService callingService, Customer customer)
        {
            if (callingService == CallingService.NA)
                throw new ArgumentNullException(nameof(callingService), "CustomerHandler->OnlyWorkingDays(). Referensen till CallingService objektet är inte satt dvs. CallingService.NA");

            if (customer == null)
                throw new ArgumentNullException(nameof(customer), "CustomerHandler->OnlyWorkingDays(). Referensen till Customer objektet är null");

            return customer.OnlyWorkingDays(callingService);
        }


        /// <summary>
        /// Anropas för att se om customer får använda vald service
        /// </summary>
        /// <param name="callingService">Vilken tjänst är det som anropas</param>
        /// <param name="customer">Referens till customer objekt</param>
        /// <returns>true om det går att använda vald service. Annars returneras false</returns>
        /// <exception cref="ArgumentNullException">Kastats om referensen till CallingService objektet inte satt dvs. CallingService.NA</exception>
        /// <exception cref="ArgumentNullException">Kastats om referensen till Customer objektet är null</exception>
        public bool CanUseService(CallingService callingService, Customer customer)
        {
            if (callingService == CallingService.NA)
                throw new ArgumentNullException(nameof(callingService), "CustomerHandler->CanUseService(). Referensen till CallingService objektet är inte satt dvs. CallingService.NA");

            if (customer == null)
                throw new ArgumentNullException(nameof(customer), "CustomerHandler->CanUseService(). Referensen till Customer objektet är null");

            return customer.CanUseService(callingService);
        }
    }
}
