using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utilerias.WebApi.Models.Responses.Base
{
    public class GenericResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
