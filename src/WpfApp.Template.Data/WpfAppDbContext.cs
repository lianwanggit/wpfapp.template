using System.Data.Entity;
using WpfApp.Template.Domain;

namespace WpfApp.Template.Data
{
	public class WpfAppDbContext : DbContext, IWpfAppDbContext
	{
		public IDbSet<Person> Persons { get; set; }
	}
}
