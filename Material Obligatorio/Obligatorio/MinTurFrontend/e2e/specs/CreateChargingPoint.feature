Feature: Create Charging Point

  Scenario: Create a valid charging point
    Given I view the "http://localhost:4200/explore/create-charging-point"
    When I type "Ancap" in the field "name"
    When I type "Desc" in the field "description"
    When I type "maldonado" in the field "address"
    When I choose "Región Metropolitana" in the combo "region"
    When I click on button Crear
    When I wait for 3000 ms
    Then the page should show a message saying "Exito!"

  Scenario: try create charging point invalid name
    Given I view the "http://localhost:4200/explore/create-charging-point"
    When I type "**" in the field "name"
    When I type "Desc" in the field "description"
    When I type "maldonado" in the field "address"
    When I choose "Región Metropolitana" in the combo "region"
    When I click on button Crear
    When I wait for 3000 ms
    Then the page should show a message saying "El nombre no es válido"


  Scenario: try create charging point invalid description
    Given I view the "http://localhost:4200/explore/create-charging-point"
    When I type "Ancap" in the field "name"
    When I type "" in the field "description"
    When I type "maldonado" in the field "address"
    When I choose "Región Metropolitana" in the combo "region"
    When I click on button Crear
    When I wait for 3000 ms
    Then the page should show a message saying "La descripcion no es válida"

  Scenario: try create charging point invalid description
    Given I view the "http://localhost:4200/explore/create-charging-point"
    When I type "Ancap" in the field "name"
    When I type "Desc" in the field "description"
    When I type "mal****" in the field "address"
    When I choose "Región Metropolitana" in the combo "region"
    When I click on button Crear
    When I wait for 3000 ms
    Then the page should show a message saying "La dirección no es válida"

  Scenario: try create charging point invalid region
    Given I view the "http://localhost:4200/explore/create-charging-point"
    When I type "Ancap" in the field "name"
    When I type "Desc" in the field "description"
    When I type "Maldonado" in the field "address"
    When I choose "" in the combo "region"
    When I click on button Crear
    When I wait for 3000 ms
    Then the page should show a message saying "La región no es válida"
