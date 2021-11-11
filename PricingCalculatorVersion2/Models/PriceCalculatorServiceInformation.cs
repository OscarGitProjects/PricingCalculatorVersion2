using PricingCalculator.Services;
using System;
using System.Text;

namespace PricingCalculator.Models
{
    /// <summary>
    /// Klass med information om en service
    /// Startdatum för användning av service, Rabatter och kostnaden för att använda servicen. Används när man inte skall använda kostnaden som finns i appsettings.json
    /// </summary>
    public class PriceCalculatorServiceInformation
    {
        /// <summary>
        /// Servicen som dessa uppgifter gäller för
        /// </summary>
        public CallingService CallingService { get; set; } = CallingService.NA;

        /// <summary>
        /// Datum när man kan starta att använda servicen
        /// </summary>
        public DateTime StartDate { get; set; } = DateTime.Now.AddYears(100);

        /// <summary>
        /// Eventuella rabatter
        /// </summary>
        public Discount Discount { get; set; }

        /// <summary>
        /// Kostnaden för att använda servicen. Sätts om vi inte skall använda baskostnaden som finns i appsettings.json
        /// </summary>
        public CostForService CostForService { get; set; }

        /// <summary>
        /// true för om vi bara skall räkna arbetsdagar. Annars false
        /// </summary>
        public bool OnlyWorkingDays { get; set; } = false;

        /// <summary>
        /// Config string för att hämta baskostnaden från appsettings.json
        /// </summary>
        public string ConfigValueStringBaseCost { get; set; } = String.Empty;


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
        public PriceCalculatorServiceInformation()
        {
        }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="CallingService">Anropande service</param>
        /// <param name="StartDate">Startdatum när man kan börja använda servicen</param>
        /// <param name="Discount">Rabatter</param>
        /// <param name="CostForService">Costnaden för servicen</param>
        /// <param name="OnlyWorkingDays">true för om vi bara skall räkna arbetsdagar. Annars false</param>
        /// <param name="ConfigValueStringBaseCost">Config string för att hämta baskostnaden från appsettings.json</param>
        public PriceCalculatorServiceInformation(CallingService CallingService, DateTime StartDate, Discount Discount, CostForService CostForService, bool OnlyWorkingDays, string ConfigValueStringBaseCost)
        {
            this.CallingService = CallingService;
            this.StartDate = StartDate;
            this.Discount = Discount;
            this.CostForService = CostForService;
            this.OnlyWorkingDays = OnlyWorkingDays;
            this.ConfigValueStringBaseCost = ConfigValueStringBaseCost;
        }


        public override string ToString()
        {
            StringBuilder strBuild = new StringBuilder();

            strBuild.AppendLine("PriceCalculatorServiceInformation");
            strBuild.Append("Service: " + CallingService);
            strBuild.Append(" StartDate: " + this.StartDate.ToShortDateString());
            strBuild.Append(" OnlyWorkingDays: " + this.OnlyWorkingDays);
            strBuild.AppendLine(" ConfigBaseCost: " + this.ConfigValueStringBaseCost);

            if (this.Discount != null)
                strBuild.AppendLine(this.Discount.ToString());

            if (this.CostForService != null)
                strBuild.AppendLine(this.CostForService.ToString());

            return strBuild.ToString();
        }
    }
}