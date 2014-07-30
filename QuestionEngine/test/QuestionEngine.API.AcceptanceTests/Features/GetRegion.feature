Feature: GetRegion
	In order to know information about a region
	As consumer of the keyword API
	I want to be able to view region details

Scenario: Request region for a given text
	Given TestCity has a region ID of 10
	When I request the region ID for TestCity in country 1
	Then region ID of 10 with region text of TestCity is returned

Scenario: Request region for a id
	Given TestCity has a region ID of 10
	When I request the region for ID 10
	Then region ID of 10 with region text of TestCity is returned