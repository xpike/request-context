using System;
using Example.Library;
using Microsoft.AspNetCore.Mvc;
using XPike.Logging;

namespace XPikeRequestContext.Controllers
{
    [ApiController]
    [Route("")]
    public class TestController
        : ControllerBase
    {
        private readonly ILog<TestController> _logger;
        private readonly TestClass _testClass;

        public TestController(ILog<TestController> logger, TestClass testClass)
        {
            _logger = logger;
            _testClass = testClass;
        }

        [HttpGet("test/{header}")]
        public IActionResult Get([FromRoute] string header)
        {
            string headerValue = null;

            try
            {
                headerValue = _testClass.GetHeader(header);
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed to retrieve header value: {header}: {ex.Message} ({ex.GetType()})", ex);
            }

            return string.IsNullOrWhiteSpace(headerValue) ? (IActionResult) NotFound() : Ok(headerValue);
        }
    }
}