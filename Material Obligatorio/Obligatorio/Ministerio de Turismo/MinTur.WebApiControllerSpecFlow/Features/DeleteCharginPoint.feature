Feature: DeleteChargingPoint

A short summary of the feature

@tag2
Scenario: Delete Charging Point Correctly
	Given the charging point id is 2
	When the user selects button to delete a charging point
	Then the result code should be 200
