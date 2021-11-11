using PricingCalculator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PricingCalculator.Models
{
    /// <summary>
    /// Information om en customer
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Customer id
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Customer namn
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Antal dagar som customer inte faktureras för
        /// </summary>
        public int NumberOfFreeDays { get; set; }

        /// <summary>
        /// Lista med information som gäller för olika service som kan användas
        /// </summary>
        public List<PriceCalculatorServiceInformation> PriceCalculatorServiceInformation;


        /// <summary>
        /// Metoden returnerar objekt med eventuell rabatt för användning av vald Service
        /// </summary>
        /// <param name="CallingService">Anropande service</param>
        /// <returns>Discount objekt. Om service inte är vald returneras null</returns>
        public Discount GetDiscount(CallingService CallingService)
        {
            var service = PriceCalculatorServiceInformation.Where(cs => cs.CallingService == CallingService).FirstOrDefault();
            return service?.Discount;
        }


        /// <summary>
        /// Metoden returnera objekt med kostnaden för att använda en vald service
        /// </summary>
        /// <param name="CallingService">Vilken tjänst är det som anropas</param>
        /// <returns>CostForService objekt. Om service inte är vald returneras null</returns>
        public CostForService GetCostForService(CallingService CallingService)
        {
            var service = PriceCalculatorServiceInformation.Where(cs => cs.CallingService == CallingService).FirstOrDefault();
            return service?.CostForService;
        }


        /// <summary>
        /// Metoden returnera den string med vilket json element man skall hämta från appsettings.json filen
        /// </summary>
        /// <param name="CallingService">Vilken tjänst är det som anropas</param>
        /// <returns>json element som skall hämtas från appsettings.json. Om service inte är vald returneras en tom sträng</returns>
        public string GetConfigValueStringBaseCost(CallingService CallingService)
        {
            var service = PriceCalculatorServiceInformation.Where(cs => cs.CallingService == CallingService).FirstOrDefault();
            return service != null ? service.ConfigValueStringBaseCost : String.Empty;
        }


        /// <summary>
        /// true om vi bara skall ta betalt för arbetsdagar. Annars retruneras false
        /// </summary>
        /// <param name="CallingService">Vilken tjänst är det som anropas</param>
        /// <returns>true om vi bara skall ta betalt för arbetsdagar. Annars retruneras false</returns>
        public bool OnlyWorkingDays(CallingService CallingService)
        {
            var service = PriceCalculatorServiceInformation.Where(cs => cs.CallingService == CallingService).FirstOrDefault();
            return service != null ? service.OnlyWorkingDays : false;
        }


        /// <summary>
        /// Metoden kontrollera om customer får använda service
        /// </summary>
        /// <param name="CallingService">Anropande service</param>
        /// <returns>true om customer får använda servicen. Annars returneras false</returns>
        public bool CanUseService(CallingService CallingService)
        {
            var service = PriceCalculatorServiceInformation.Where(cs => cs.CallingService == CallingService).FirstOrDefault();
            return service != null ? service.CanUseService : false;
        }


        /// <summary>
        /// Metoden lähher till ett nytt PriceCalculatorServiceInformation objekt till list med objekt
        /// </summary>
        /// <param name="PriceCalculatorServiceInformation">Nytt PriceCalculatorServiceInformation objekt</param>
        public void AddPriceCalculatorServiceInformation(PriceCalculatorServiceInformation PriceCalculatorServiceInformation)
        {
            if (this.PriceCalculatorServiceInformation == null)
                this.PriceCalculatorServiceInformation = new List<PriceCalculatorServiceInformation>();

            this.PriceCalculatorServiceInformation.Add(PriceCalculatorServiceInformation);
        }        


        /// <summary>
        /// Property som returnerar true om användaren har några gratis dagar
        /// Annars returneras false
        /// </summary>
        public bool HasFreeDays {
            get {
                if (NumberOfFreeDays > 0)
                    return true;

                return false;
            }
        }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="iCustomerId">CustomerId</param>
        /// <param name="strCustomerName">CustomerName</param>
        public Customer(int iCustomerId, string strCustomerName)
        {
            CustomerId = iCustomerId;
            CustomerName = strCustomerName;

            PriceCalculatorServiceInformation = new List<PriceCalculatorServiceInformation>();

            NumberOfFreeDays = 0;
        }


        public override string ToString()
        {
            StringBuilder strBuild = new StringBuilder();
            strBuild.AppendLine($"CustomerId: {CustomerId}, CustomerName: {CustomerName}, NumberOfFreeDays: {NumberOfFreeDays}");

            foreach(var serviceInformation in PriceCalculatorServiceInformation)
            {
                strBuild.AppendLine(serviceInformation.ToString());
            }
            return strBuild.ToString();
        }
    }
}
