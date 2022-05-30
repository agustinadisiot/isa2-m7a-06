Feature: GetAllChargingPoints

A short summary of the feature

@tag1
Scenario: Get All Charging Point Correctly
	Given existing charging points
	When the user selects button to get all charging points in the lateral menu
	Then the result should be a list of charging points

Scenario: Get All Charging Points Empty List 
	Given there are not charging points
	When the user selects button to get all charging points in the lateral menu
	Then the result should be an empty list

