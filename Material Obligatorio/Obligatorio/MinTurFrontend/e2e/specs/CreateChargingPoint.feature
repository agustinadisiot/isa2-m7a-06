Feature: Create Charging Point

  Scenario: Create a valid charging point
    Given I view the form "http://localhost:4200/explore/create-charging-point"
    When i type name "Ancap" in the field "name"
    When i type desc "Desc" in the field "description"
    When i type address "Maldonado" in the field "address"
    When i choose "Región Metropolitana" in the combo "region"
    When I click on button "Crear"
    When I wait for 3000 miliseconds
    Then the page should show "¡Exito!"

  Scenario: try create charging point invalid name
    Given I view the form "http://localhost:4200/explore/create-charging-point"
    When i type name " " in the field "name"
    When i type desc "Desc" in the field "description"
    When i type address "maldonado" in the field "address"
    When i choose "Región Metropolitana" in the combo "region"
    When I click on button "Crear"
    When I wait for 3000 miliseconds
    Then the page should show a message saying "Es necesario especificar un nombre"


  Scenario: try create charging point invalid description
    Given I view the form "http://localhost:4200/explore/create-charging-point"
    When i type name "Ancap" in the field "name"
    When i type desc " " in the field "description"
    When i type address "maldonado" in the field "address"
    When i choose "Región Metropolitana" in the combo "region"
    When I click on button "Crear"
    When I wait for 3000 miliseconds
    Then the page should show a message saying "Es necesario especificar una descripción"

  Scenario: try create charging point invalid description
    Given I view the form "http://localhost:4200/explore/create-charging-point"
    When i type name "Ancap" in the field "name"
    When i type desc "Desc" in the field "description"
    When i type address " " in the field "address"
    When i choose "Región Metropolitana" in the combo "region"
    When I click on button "Crear"
    When I wait for 3000 miliseconds
    Then the page should show a message saying "Es necesario especificar una dirección"

  Scenario: try create charging point invalid region
    Given I view the form "http://localhost:4200/explore/create-charging-point"
    When i type name "Ancap" in the field "name"
    When i type desc "Desc" in the field "description"
    When i type address "Maldonado" in the field "address"
    When i choose " " in the combo "region"
    When I click on button "Crear"
    When I wait for 3000 miliseconds
    Then the page should show a message saying "Es necesario especificar una región"