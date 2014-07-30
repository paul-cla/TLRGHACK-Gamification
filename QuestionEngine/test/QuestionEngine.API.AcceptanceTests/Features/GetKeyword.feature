Feature: GetKeyword
	In order to know information about a keyword
	As consumer of the keyword API
	I want to be able to view keyword details

Scenario: Request keyword for a given text and country
	Given TestCity has a keyword ID of 10 and is in TestCountry which has an id of 15 with area TestArea with area id of 20
	When I request the keyword ID for TestCity in country 15
	Then keyword ID of 10 with keyword text of TestCity with country TestCountry with area of TestArea and area ID of 20 is returned

Scenario: Request keyword for a given keyword id
	Given TestCity has a keyword ID of 10 and is in TestCountry which has an id of 15 with area TestArea with area id of 20
	When I request the keyword for ID 10
	Then keyword ID of 10 with keyword text of TestCity with country TestCountry with area of TestArea and area ID of 20 is returned