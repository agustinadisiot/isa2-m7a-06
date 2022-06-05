Feature: Test Get All Charging Points

#  Scenario: Get All Charging Points empty
#    Given there are no charging points when I view the "http://localhost:4200/explore/charging-points"
#    Then the page should show a message saying "No hay puntos de carga por el momento."

#  Scenario: Create a valid charging point
#    Given I view the "http://localhost:4200/explore/create-charging-points"
#    When i type "Ancap" in field "name"
#    When i type "Desc" in the field "description"
#    When i type "maldonado" in the field "address"
#    When i choose "Mediterraneo" in the combo "region"
#    When I click on button Create Charging Point
#    When I wait for 3000 ms
#    Then the page should Create Charging Point

  Scenario: Get All Charging Points
    Given I view the "http://localhost:4200/explore/regions"
    When I click on button Charging Points
    When I wait for 3000 ms
    Then the page should load a list of Charging Points with the name "Ancap" we just created


