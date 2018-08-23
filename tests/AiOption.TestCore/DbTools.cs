using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace AiOption.TestCore {

    public static class DbTools {

        public static async Task CreateNewDatabaseAsync(DbContext context) {
            await context.Database.EnsureCreatedAsync();
            await context.Database.MigrateAsync();
            

        }

        public static Task CleanDatabase(DbContext context) {
            return new DbCleaner(context.Database.GetDbConnection()).DeleteAllData();
        }
        
    }
    


}
