using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utilerias.WebApi.Models.Responses.Base
{
    public class ListResponse<T> : GenericResponse
    {
        public ICollection<T> Items { get; set; }
    }
}
