using System;
using CurrCalc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CurrCalc.Controllers
{
    /// <inheritdoc />
    public class BaseController : ControllerBase
    {
        private readonly ILogger<BaseController> _logger;

        /// <inheritdoc />
        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected IActionResult LogAndError500Response(string name , Exception e)
        {
            _logger.LogError(e.Message, e);
            return new ObjectResult(new ApiResponse(name)
            {
                ErrorMsg = "Something bad happen",
                Status = StatusCodes.Status500InternalServerError
            });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected IActionResult NotFoundObjectResult(string name ,string msg)
        {
            return NotFound(new ApiResponse(name)
            {
                ErrorMsg = msg,
                Status = StatusCodes.Status404NotFound
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected IActionResult NotUpdateObjectResult(string name ,string msg)
        {
            return NotFound(new ApiResponse(name)
            {
                ErrorMsg = msg,
                Status = StatusCodes.Status422UnprocessableEntity
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected IActionResult OkObjectResult(object data)
        {
            return Ok(new ApiResponse
            {
                Data = data,
                Status = StatusCodes.Status200OK
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected IActionResult CreateObjectResult(object data)
        {
            return Ok(new ApiResponse
            {
                Data = data,
                Status = StatusCodes.Status201Created
            });
        }
    }
}