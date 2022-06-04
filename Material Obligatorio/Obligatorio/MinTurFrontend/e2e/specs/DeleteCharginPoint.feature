Feature: Test Delete Charging Point

  Scenario: Delete Charging Point correctly
    Given I view the list on "http://localhost:4200/explore/charging-points"
    When I click on button Delete Charging Points
    When I wait for the list 3000 ms
    Then the page should load a message saying "Deleted successful"
