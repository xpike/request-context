using XPike.RequestContext;

namespace Example.Library
{
    public class TestClass
    {
        private readonly IRequestContext _requestContext;

        public TestClass(IRequestContext requestContext)
        {
            _requestContext = requestContext;
        }

        public string GetHeader(string header) =>
            _requestContext.Headers[header];
    }
}