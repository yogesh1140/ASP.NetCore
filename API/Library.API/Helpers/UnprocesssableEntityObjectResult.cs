using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.Helpers
{
    public class UnprocesssableEntityObjectResult : ObjectResult
    {
        public UnprocesssableEntityObjectResult(ModelStateDictionary modelState) : base(new SerializableError(modelState))
        {
            if (modelState == null) {
                throw new ArgumentException(nameof(modelState));
            }
            StatusCode = 422;
        }
    }
}
