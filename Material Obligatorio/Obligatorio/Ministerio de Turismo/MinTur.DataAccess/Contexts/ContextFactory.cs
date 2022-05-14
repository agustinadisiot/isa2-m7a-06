using  Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace MinTur.DataAccess.Contexts
{
    public  enum  ContextType { Memory, SQL }

	[ExcludeFromCodeCoverage]
	public class ContextFactory: IDesignTimeDbContextFactory<NaturalUruguayContext>
	{
		public  NaturalUruguayContext  CreateDbContext(string[] args) {
			return  GetNewContext();
		}
		
		public static NaturalUruguayContext GetNewContext(ContextType type = ContextType.SQL)
		{
			var builder = new  DbContextOptionsBuilder<NaturalUruguayContext>();
			DbContextOptions  options = null;
			if (type == ContextType.Memory) {
				options = GetMemoryConfig(builder);
			} else {
				options = GetSqlConfig(builder);
			}
			
			return new NaturalUruguayContext(options);
		}

		private static DbContextOptions GetMemoryConfig(DbContextOptionsBuilder builder) 
		{
			builder.UseInMemoryDatabase("NaturalUruguayDB");
			
			return  builder.Options;
		}

		private static DbContextOptions GetSqlConfig(DbContextOptionsBuilder  builder)
		 {
			string directory = Directory.GetCurrentDirectory();

			IConfigurationRoot configuration = new ConfigurationBuilder()
			.SetBasePath(directory)
			.AddJsonFile("appsettings.json")
			.Build();

			var connectionString = configuration.GetConnectionString(@"NaturalUruguayDB");
			builder.UseSqlServer(connectionString);
			return  builder.Options;
		}
        
    }
}