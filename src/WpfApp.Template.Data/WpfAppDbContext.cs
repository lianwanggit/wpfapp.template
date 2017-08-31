using System.Data.Entity;
using System.Data.Entity.Migrations;
using WpfApp.Template.Data.Migrations;
using WpfApp.Template.Domain;

namespace WpfApp.Template.Data
{
	public class WpfAppDbContext : DbContext, IWpfAppDbContext
	{
		public IDbSet<Person> Persons { get; set; }

		public void Migrate()
		{
			Database.CreateIfNotExists();

			var configuration = new Configuration();
			var migrator = new DbMigrator(configuration);

			migrator.Update();
		}
	}
}
