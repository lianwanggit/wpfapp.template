using System.Data.Entity;
using WpfApp.Template.Data;
using WpfApp.Template.Domain;
using WpfApp.Template.Test.Common;

namespace WpfApp.Template.Core.Test
{
	public class WpfAppTestDbContext : InMemoryDbContext, IWpfAppDbContext
	{
		public IDbSet<Person> Persons { get; set; }
	}
}
