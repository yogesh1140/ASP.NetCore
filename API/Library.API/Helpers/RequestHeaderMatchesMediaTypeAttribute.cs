using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.Helpers
{
    [AttributeUsage(AttributeTargets.All, Inherited =true, AllowMultiple =true)]
    public class RequestHeaderMatchesMediaTypeAttribute : Attribute, IActionConstraint
    {
        public readonly string _requestHeaderToMatch;
        private readonly string[] _mediaTypes;

        public RequestHeaderMatchesMediaTypeAttribute(string requestHeaderToMatch, string[] mediaType)
        {
            _requestHeaderToMatch = requestHeaderToMatch;
            _mediaTypes = mediaType;
        }

        public int Order
        {
            get { return 0; }
        }
        public bool Accept(ActionConstraintContext context)
        {
            var requestHeaders = context.RouteContext.HttpContext.Request.Headers;
            if (!requestHeaders.ContainsKey(_requestHeaderToMatch)) {
                return false;
            } else
            {
                foreach (var mediaType in _mediaTypes) {
                    var mediaTypeMatches = string.Equals(requestHeaders[_requestHeaderToMatch].ToString(), mediaType, StringComparison.OrdinalIgnoreCase);
                    if(mediaTypeMatches)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
