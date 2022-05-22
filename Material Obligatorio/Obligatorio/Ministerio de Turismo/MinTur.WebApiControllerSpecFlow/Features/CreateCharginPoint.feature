Feature: CreateChargingPoint

A short summary of the feature

@tag1
Scenario: Create Charging Point Correctly
	Given the name is "Ancap Maldonado5"
	And the description is "First charging point of Uruguay"
	And the address is "San Martin 2334"
	And the regionId exists and is 1
	And the user is admin
	When the user selects button to create a charging point
	Then the result should be 201
