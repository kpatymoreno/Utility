using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilerias.Infraestructure.DbContext;
using Utilerias.Infraestructure.DbContext.Interface;

namespace Utilerias.Infraestructure
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services)
        {
            services.AddTransient<IAppDbContext, AppDbContext>();
            
        }
    }
}
