using System;

namespace WpfApp.Template.Domain
{
	public class Person
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public bool Gender { get; set; }
		public DateTimeOffset DateOfBirth { get; set; }
	}
}
