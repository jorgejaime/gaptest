using System.Collections.Generic;

namespace Jorge.ClinicaApp.Infrastructure.Messaging
{
    public class ContractResponse<T>   where T : class
    {
        /// <summary>
        /// Result set of information to return
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        /// Error handling from the database
        /// </summary>
       // public EnterpriseException Error { get; set; }

        /// <summary>
        /// IF true, this means the response is valid
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// The count of result elements
        /// </summary>
        public int DataCount { get; set; }
       
        /// <summary>
        /// The login result message
        /// </summary>
        public string[] ErrorMessages { get; set; }

    }
}
