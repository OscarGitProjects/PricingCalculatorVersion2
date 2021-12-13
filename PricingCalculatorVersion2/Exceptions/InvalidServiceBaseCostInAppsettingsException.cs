using System;
using System.Runtime.Serialization;

namespace PricingCalculator.Exceptions
{
    /// <summary>
    /// Exception som skall kastas när ServiceBaseCost data i Appsettings.json inte är korrekt
    /// </summary>
    [Serializable]
    public class InvalidServiceBaseCostInAppsettingsException : Exception
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public InvalidServiceBaseCostInAppsettingsException() 
        { 
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="message">Text som skall visas</param>
        public InvalidServiceBaseCostInAppsettingsException(string message) : base(message)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="message">Text som skall visas</param>
        /// <param name="innerException">Exception</param>
        public InvalidServiceBaseCostInAppsettingsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected InvalidServiceBaseCostInAppsettingsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}