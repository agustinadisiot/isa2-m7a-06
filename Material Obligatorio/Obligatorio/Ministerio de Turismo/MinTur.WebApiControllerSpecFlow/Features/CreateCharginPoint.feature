Feature: CreateChargingPoint

A short summary of the feature

@tag1
Scenario: Create Charging Point Correctly
	Given the name is "Ancap Maldonado6"
	And the description is "First charging point of Uruguay"
	And the address is "San Martin 2334"
	And the regionId exists and is 1
	When the user selects button to create a charging point
	Then the result should be 201

Scenario: Invalid name on Creating Charging Point Correctly
	Given the name is "Inva_lid! Name**"
	And the description is "First charging point of Uruguay"
	And the address is "San Martin 2334"
	And the regionId exists and is 1
	When the user selects button to create a charging point
	Then the result should be 400

Scenario: Invalid description on Creating Charging Point Correctly
    Given the name is "Valid Name"
    And the description is "Inva_lid! description"
    And the address is "San Martin 2334"
    And the regionId exists and is 1
    When the user selects button to create a charging point
    Then the result should be 400

Scenario: Invalid address on Creating Charging Point Correctly
    Given the name is "Valid Name"
    And the description is "Valid description"
    And the address is "Inva_lid! address"
    And the regionId exists and is 1
    When the user selects button to create a charging point
    Then the result should be 400

Scenario: Invalid regionId on Creating Charging Point Correctly
    Given the name is "Valid Name"
    And the description is "Valid description"
    And the address is "Valid address"
    And the regionId exists and is -1
    When the user selects button to create a charging point
    Then the result should be 404

Scenario: Invalid  on Creating Charging Point Correctly
    Given the name is "Valid Name"
    And the description is "Valid description"
    And the address is "Valid address**"
    And the regionId exists and is -1
    When the user selects button to create a charging point
    Then the result should be 400