Feature: StatusCheck
	In order to check the service is responding
	As consumer of the keyword API
	I want to be able to check it is responding

Scenario: Call status check address for working service
	Given the service is responding
	When I request the status of the service
	Then the service reports it is responding

Scenario: Call status check address for broken service
	Given the service is not responding
	When I request the status of the service
	Then the service reports it is not responding