using System.Data.Entity;
using WpfApp.Template.Domain;

namespace WpfApp.Template.Data
{
	public class WpfAppDbContext : DbContext
	{
		public DbSet<Person> Persons { get; set; }
	}
}
