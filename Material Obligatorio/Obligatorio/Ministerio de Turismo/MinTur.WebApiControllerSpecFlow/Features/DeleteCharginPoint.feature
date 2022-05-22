Feature: DeleteChargingPoint

A short summary of the feature

@tag1
Scenario: Delete Charging Point Correctly
	Given the charging point id is 1
	And the user is admin
	When the user selects button to delete a charging point
	Then the result should be 200
