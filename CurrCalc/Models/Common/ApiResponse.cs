using System;
using Microsoft.AspNetCore.Http;

namespace CurrCalc.Models.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiResponse
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public ApiResponse(string name = default)
        {
            Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        private Guid Id { get;} = Guid.NewGuid();

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int Status { get; set; } = StatusCodes.Status200OK;

        /// <summary>
        /// 
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorMsg { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public bool Success => string.IsNullOrWhiteSpace(this.ErrorMsg);
    }
}