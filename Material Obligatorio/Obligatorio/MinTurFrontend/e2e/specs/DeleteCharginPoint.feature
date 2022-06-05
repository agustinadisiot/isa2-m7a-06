Feature: Test Delete Charging Point

  Scenario: Delete Charging Point correctly
    Given I view the list on "http://localhost:4200/explore/charging-points"
    When I click on list "choosenPoint"
    When I click on the button "Eliminar"
    When I wait for the list 3000 ms
    Then the page should load a new list of Charging Points
