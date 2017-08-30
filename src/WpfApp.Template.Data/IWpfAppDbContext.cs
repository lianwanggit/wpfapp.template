using System.Data.Entity;
using WpfApp.Template.Domain;

namespace WpfApp.Template.Data
{
	public interface IWpfAppDbContext
	{
		IDbSet<Person> Persons { get; set; }
	}
}