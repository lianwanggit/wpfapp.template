Feature: InMemoryDb
	In order to avoid silly mistakes
	As an EF idiot
	I want to be told that In-memory DB works

Background:
	Given The following persons in database
		| FirstName | LastName | Gender | DateOfBirth |
		| James     | Bond     | 1      | 1953-06-17  |

Scenario: 1. Create a person
	Given I have entered person data
		| FirstName | LastName | Gender | DateOfBirth |
		| John      | Smith    | 0      | 1990-06-18  |
	When I press add
	Then the result should be saved to database
		| FirstName | LastName | Gender | DateOfBirth |
		| James     | Bond     | 1      | 1953-06-17  |
		| John      | Smith    | 0      | 1990-06-18  |

Scenario: 2. Delete a person
	Given I selected a person record
		| FirstName | LastName |
		| James     | Bond     |
	When I press delete
	Then the record should be deleted from database