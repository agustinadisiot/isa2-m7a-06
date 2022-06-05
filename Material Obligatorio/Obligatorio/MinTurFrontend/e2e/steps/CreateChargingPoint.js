'use strict';

var { Given } = require('cucumber');
var { When } = require('cucumber');
var { Then } = require('cucumber');

// Use the external Chai As Promised to deal with resolving promises in
// expectations
const chai = require('chai');
const chaiAsPromised = require('chai-as-promised');
chai.use(chaiAsPromised);
const expect = chai.expect;

// Given(/^I view the "([^"]*)"$/, function (url, callback) {
//   browser.get(url).then(function () {
//     callback();
//   });
// });

When(
  /^i type "([^"]*)" in field "([^"]*)"$/,
  function (inputTextEntry, inputName) {
    return browser.driver
      .findElement(by.css('[formControlName="' + inputName + '"]'))
      .sendKeys(inputTextEntry);
    }
  );

When(
  /^i choose "([^"]*)" in the combo "([^"]*)"$/,
  function (inputTextEntry, inputName) {
    return browser.driver
      .findElement(by.css('[formControlName="' + inputName + '"]'))
      .sendKeys(inputTextEntry);
    }
  );


When(/^I click on button Create Charging Point$/, function (callback) {
  element(by.id('createChargingPoint')).click();
  callback();
});

// When(/^I wait for (\d+) ms$/, function (timeToWait, callback) {
//   setTimeout(callback, timeToWait);
// });

Then(/^the page should Create Charging Point$/, function (callback) {
  let chargingPoints = element(by.css('chargingPoints'))
  expect(chargingPoints.count()).to.eventually.equal("").and.notify(callback);
});

// Then(/^the page should show a message saying "([^"]*)"$/, function (text, callback) {
//   let chargingPoints = element(by.css('errorCreatingChargingPoint'))
//   expect(element(by.css('[name="alert"]')).getText()).to.eventually.equal(text).and.notify(callback);
// });
