using System;
using System.Collections.Generic;
using System.Linq;
using Jorge.ClinicaApp.Infrastructure.Domain;

namespace Jorge.ClinicaApp.Infrastructure.Messaging
{
    public class ContractUtil
    {


        #region Request 

        public static ContractRequest<TRequest> CreateRequest<TRequest>(TRequest dtoData )       
            where TRequest : class 
        {
            var toReturn = new ContractRequest<TRequest>
            {                               
                Data = dtoData,
            };

            return toReturn;
        }


        public static ContractRequest<BaseRequest> CreateBaseRequest()
        {
            var toReturn = new ContractRequest<BaseRequest>()
            {
                Data = new BaseRequest(),
            };

            return toReturn;
        }

        #endregion

        #region Response

       
        public static ContractResponse<TResponse> CreateResponse<TResponse, TRequest>(ContractRequest<TRequest> request, TResponse data)
            where TRequest : class
            where TResponse : class
        {
            if (request == null) request = new ContractRequest<TRequest>();
            return CreateResponse(request, new List<TResponse> {data}, 0, 0);
        }


        public static ContractResponse<TResponse> CreateResponse<TResponse, TRequest>(ContractRequest<TRequest> request, IEnumerable<TResponse> data)
            where TRequest : class
            where TResponse : class
        {
            if (request == null) request = new ContractRequest<TRequest>();
            return CreateResponse(request, data, 0, 0);
        }


        public static ContractResponse<TResponse> CreateResponse<TResponse, TRequest>(ContractRequest<TRequest> request, IEnumerable<TResponse> data, long count, long countFilter)
            where TRequest : class
            where TResponse : class
        {

            return new ContractResponse<TResponse>
            {
                Data = data.ToList(),                
                ErrorMessages = new string[] { } ,
                DataCount = data.Count(),
                IsValid = true,
            };
        }

        #endregion

        #region InvalidReponse

               

        public static ContractResponse<TResponse> CreateInvalidResponse<TResponse>(TResponse data, string errorMessage)
            where TResponse : class
        {

            return new ContractResponse<TResponse>
            {
                Data = data == null ? new List<TResponse>() : new List<TResponse> { data },                
                ErrorMessages = new[] { errorMessage } ,
                DataCount = data == null ? 0 : 1,
                IsValid = false,
            };
        }

        public static ContractResponse<TResponse> CreateInvalidResponse<TResponse>(IEnumerable<TResponse> data, string errorMessage)             
             where TResponse : class
        {

            return new ContractResponse<TResponse>
            {
                Data =  data == null ? new List<TResponse>()  : (List<TResponse>) data ,                
                ErrorMessages = new[] { errorMessage },
                DataCount = data?.Count() ?? 0,
                IsValid = false,
            };
        }

        public static ContractResponse<TResponse> CreateInvalidResponse<TResponse>(Exception ex, IEnumerable<TResponse> data)
            where TResponse : class
        {

            return new ContractResponse<TResponse>
            {
                Data = data == null ? new List<TResponse>() : (List<TResponse>)data,                               
                ErrorMessages =  new[] { ex.Message },
                DataCount = data?.Count() ?? 0,
                IsValid = false,
            };
        }

        public static ContractResponse<TResponse> CreateInvalidResponse<TResponse>(Exception ex, TResponse data)
            where TResponse : class
        {

            return new ContractResponse<TResponse>
            {
                Data = data == null ? new List<TResponse>() : new List<TResponse> { data },                                
                ErrorMessages = new[] { ex.Message },
                DataCount = data == null ? 0 : 1,
                IsValid = false,
            };
        }


        public static ContractResponse<TResponse> CreateInvalidResponse<TResponse>(Exception ex)
            where TResponse : class 
        {

            return new ContractResponse<TResponse>
            {
                Data =  new List<TResponse>(),                
                ErrorMessages = new[] { ex.Message },
                DataCount = 0,
                IsValid = false,
            };
        }

        public static ContractResponse<TResponse> CreateInvalidResponse<TResponse>(IEnumerable<BusinessRule> brokenRules)
            where TResponse : class
        {

            return new ContractResponse<TResponse>
            {
                Data = new List<TResponse>(),                
                ErrorMessages = brokenRules.Select(b=> b.Rule).ToArray(),
                DataCount = 0,
                IsValid = false,
            };
        }

        #endregion
    }
}
