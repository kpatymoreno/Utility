using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilerias.Infraestructure.DbContext.Interface
{
    public interface IAppDbContext
    {
        IDbConnection GetConnection();
    }
}
