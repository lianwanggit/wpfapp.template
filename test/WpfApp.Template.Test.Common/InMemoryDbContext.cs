using System;
using System.Data.Entity;
using System.Linq;

namespace WpfApp.Template.Test.Common
{
	public abstract class InMemoryDbContext
	{
		protected InMemoryDbContext()
		{
			var t = GetType();
			var dbSets = from p in t.GetProperties()
							 where p.PropertyType.IsGenericType
							 where p.PropertyType.GetGenericTypeDefinition() == typeof(IDbSet<>)
							 select p;

			foreach (var dbSet in dbSets)
			{
				var e = dbSet.PropertyType.GetGenericArguments()[0];
				var j = typeof(InMemoryDbSet<>).MakeGenericType(e);
				var fakeDbSet = Activator.CreateInstance(j);

				dbSet.SetValue(this, fakeDbSet);
			}
		}
	}
}
