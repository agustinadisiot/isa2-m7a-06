Feature: Test Get All Charging Points

  Scenario: Get All Charging Points empty
    Given there are no charging points when I view the "http://localhost:4200/explore/charging-points"
    When I wait for 3000 ms
    Then the page should show a specific message saying "No hay puntos de carga por el momento."

  Scenario: Create a valid charging point
    Given I view the form "http://localhost:4200/explore/create-charging-point"
    When i type name "Ancap" in the field "name"
    When i type desc "Desc" in the field "description"
    When i type address "Maldonado" in the field "address"
    When i choose "Región Metropolitana" in the combo "region"
    When I click on button "Crear"
    When I wait for 3000 miliseconds
    Then the page should show "Creado Correctamente"

  Scenario: Get All Charging Points
    Given I view the "http://localhost:4200/explore/regions"
    When I click on button Charging Points
    When I wait for 3000 ms
    Then the page should load a list of Charging Points with the name "Ancap" we just created


