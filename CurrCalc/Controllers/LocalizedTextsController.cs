using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CurrCalc.Data.Entities;
using CurrCalc.Data.Repository;
using CurrCalc.Helpers;
using CurrCalc.Models.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace CurrCalc.Controllers
{
    /// <inheritdoc />
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class LocalizedTextsController : BaseController
    {

        private readonly IRepository<LocalizedText,int> _repository;

        /// <inheritdoc />
        public LocalizedTextsController(
            IRepository<LocalizedText, int> repository,
            ILogger<LocalizedTextsController> logger) : base(logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


        /// <summary>
        /// Get all localized text for specific language
        /// </summary>
        /// <param name="lang">Language code</param>
        /// <param name="token">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <response code="200">OK</response>
        /// <response code="400">NotFound</response>
        /// <response code="500">InternalServerError</response>
        /// <returns>Dictionary with all keys - values for a specific language</returns>
        [HttpGet, Route("localizeTexts/{lang}")]
        public async Task<IActionResult> GetLocalizedTexts(string lang, CancellationToken token =default)
        {
            try
            {
                var localizedText = await _repository.TableNoTracking
                                                                        .Where(x => x.Language.Equals(lang.ToUpperInvariant()))
                                                                        .ToDictionaryAsync(k=>k.Key, v =>v.Value , token)
                                                                        .ConfigureAwait(false);

                return localizedText == null 
                    ? NotFoundObjectResult(nameof(LocalizedTextsController),"language text don't exist") 
                    : OkObjectResult(localizedText);
            }
            catch (Exception e)
            {
                return LogAndError500Response(nameof(LocalizedTextsController), e);
            }
        }

        /// <summary>
        /// Get all languages
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="400">NotFound</response>
        /// <response code="500">InternalServerError</response>
        /// <returns>Select list of languages</returns>
        [HttpGet, Route("languages")]
        public IActionResult GetLanguages()
        {
            try
            {
                var listOfEnums = Enum.GetValues(typeof(Language))
                    .Cast<Enum>()
                    .Select(e => new SelectListItem( e.GetDescription(),e.ToString()));

                return !listOfEnums.Any() 
                    ? NotFoundObjectResult(nameof(LocalizedTextsController),"language don't exist") 
                    : OkObjectResult(listOfEnums);
            }
            catch (Exception e)
            {
                return LogAndError500Response(nameof(LocalizedTextsController), e);
            }
        }
    }
}
