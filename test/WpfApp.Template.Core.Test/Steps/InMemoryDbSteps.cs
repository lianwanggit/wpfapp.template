using System.Linq;
using Autofac;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using WpfApp.Template.Data;
using WpfApp.Template.Domain;
using NUnit.Framework;
using System.Collections.Generic;

namespace WpfApp.Template.Core.Test.Steps
{
	[Binding]
	public class InMemoryDbSteps
	{
		private readonly ScenarioContext _context;

		public InMemoryDbSteps(ScenarioContext context)
		{
			_context = context;
		}

		[Given(@"The following persons in database")]
		public void GivenTheFollowingPersonsInDatabase(Table table)
		{
			var container = _context.Get<IContainer>();
			var dbContext = container.Resolve<IWpfAppDbContext>();

			var persons = table.CreateSet<Person>();
			foreach (var person in persons)
			{
				dbContext.Persons.Add(person);
			}
		}

		[Given(@"I have entered person data")]
		public void GivenIHaveEnteredPersonData(Table table)
		{
			var persons = table.CreateSet<Person>().ToList();
			_context.Set(persons, "PersonsToBeAdded");
		}

		[When(@"I press add")]
		public void WhenIPressAdd()
		{
			var container = _context.Get<IContainer>();
			var dbContext = container.Resolve<IWpfAppDbContext>();

			var persons = _context.Get<List<Person>>("PersonsToBeAdded");
			foreach (var person in persons)
			{
				dbContext.Persons.Add(person);
			}
		}

		[Then(@"the result should be saved to database")]
		public void ThenTheResultShouldBeSavedToDatabase(Table table)
		{
			var container = _context.Get<IContainer>();
			var dbContext = container.Resolve<IWpfAppDbContext>();
			var persons = table.CreateSet<Person>().ToList();

			var dbPersons = dbContext.Persons.ToList();
			Assert.AreEqual(persons.Count, dbPersons.Count);

			foreach (var person in persons)
			{
				var dbPerson = dbPersons.FirstOrDefault(p => p.FirstName == person.FirstName
						&& p.LastName == person.LastName);

				Assert.IsNotNull(dbPerson);
				Assert.AreEqual(dbPerson.Gender, person.Gender);
				Assert.AreEqual(dbPerson.DateOfBirth, person.DateOfBirth);
			}
		}

		[Given(@"I selected a person record")]
		public void GivenISelectedAPersonRecord(Table table)
		{
			var persons = table.CreateSet<Person>().ToList();
			_context.Set(persons, "PersonsToBeDeleted");
		}


		[When(@"I press delete")]
		public void WhenIPressDelete()
		{
			var container = _context.Get<IContainer>();
			var dbContext = container.Resolve<IWpfAppDbContext>();

			var persons = _context.Get<List<Person>>("PersonsToBeDeleted");
			foreach (var person in persons)
			{
				var dbPerson = dbContext.Persons.FirstOrDefault(p => p.FirstName == person.FirstName
						&& p.LastName == person.LastName);

				dbContext.Persons.Remove(dbPerson);
			}
		}

		[Then(@"the record should be deleted from database")]
		public void ThenTheRecordShouldBeDeletedFromDatabase()
		{
			var container = _context.Get<IContainer>();
			var dbContext = container.Resolve<IWpfAppDbContext>();

			var persons = _context.Get<List<Person>>("PersonsToBeDeleted");
			var dbPersons = dbContext.Persons.ToList();
			foreach (var person in persons)
			{
				var dbPerson = dbPersons.FirstOrDefault(p => p.FirstName == person.FirstName
						&& p.LastName == person.LastName);

				Assert.IsNull(dbPerson);
			}
		}

	}
}
