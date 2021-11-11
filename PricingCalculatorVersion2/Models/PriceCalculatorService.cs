using PricingCalculator.Services;
using System;

namespace PricingCalculator.Models
{
    /// <summary>
    /// Klass med information om en service
    /// Startdatum för användning av service, Rabatter och kostnaden för att använda servicen. Används när man inte skall använda kostnaden som finns i appsettings.json
    /// </summary>
    public class PriceCalculatorService
    {
        public CallingService CallingService { get; set; } = CallingService.NA;
        public DateTime StartDate { get; set; } = DateTime.Now.AddYears(100);
        public Discount Discount { get; set; }
        public CostForService CostForService { get; set; }


        /// <summary>
        /// Property som returnerar true om användaren kan använda service
        /// Annars returneras false
        /// </summary>
        public bool CanUseService
        {
            get
            {
                if (StartDate.Date <= DateTime.Now.Date)
                    return true;

                return false;
            }
        }


        /// <summary>
        /// Konstruktor
        /// </summary>
        public PriceCalculatorService()
        {
        }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="CallingService">Anropande service</param>
        /// <param name="StartDate">Startdatum när man kan börja använda servicen</param>
        /// <param name="Discount">Rabatter</param>
        /// <param name="CostForService">Costnaden för servicen</param>
        public PriceCalculatorService(CallingService CallingService, DateTime StartDate, Discount Discount, CostForService CostForService)
        {
            this.CallingService = CallingService;
            this.StartDate = StartDate;
            this.Discount = Discount;
            this.CostForService = CostForService;
        }
    }
}