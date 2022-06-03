Feature: Test Get All Charging Points

  Scenario: Get All Charging Points
    Given I view the "http://localhost:4200/explore/regions"
    When I click on button Charging Points
    When I wait for 3000 ms
    Then the page should load a list of Charging Points

#
#    Scenario: Get All Charging Points empty
#      Given I view the "http://localhost:4200/explore/regions"
#      When I click on button Charging Points
#      When I wait for 3000 ms
#      Then the page should show a message saying "No hay puntos de carga por el momento."
