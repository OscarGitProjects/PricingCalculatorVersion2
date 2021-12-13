using System;
using System.Runtime.Serialization;

namespace PricingCalculator.Exceptions
{
    /// <summary>
    /// Exception som skall kastas när man skall lägga till en customer till repository och en customer med customerId redan finns
    /// </summary>
    [Serializable]
    public class CustomerAlreadyExistsException : Exception
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public CustomerAlreadyExistsException() 
        { 
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="message">Text som skall visas</param>
        public CustomerAlreadyExistsException(string message) : base(message)
        { 
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="message">Text som skall visas</param>
        /// <param name="innerException">Exception</param>
        public CustomerAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        { 
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected CustomerAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}