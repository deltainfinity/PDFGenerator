using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace PDFGenerator.Controllers.Base
{
    /// <summary>
    /// Base API controller for defining shared functions. This must be backwards compatible and safe across all versions.
    /// </summary>
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Memory cache
        /// </summary>
        protected IMemoryCache Cache { get; set; }

        /// <summary>
        /// DI Constructor
        /// </summary>
        /// <param name="cache">Instance of memory cache to load</param>
        protected BaseApiController(IMemoryCache cache)
        {
            Cache = cache;
        }
    }
}