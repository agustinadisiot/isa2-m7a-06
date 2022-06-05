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

Given(/^I view the form "([^"]*)"$/, function (url, callback) {
  browser.get(url).then(function () {
    callback();
  });
});

When(
  /^i type name "([^"]*)" in field "([^"]*)"$/,
  function (inputTextEntry, inputName) {   
    return browser.driver
      .findElement(by.css('[formControlName="' + inputName + '"]'))
      .sendKeys(inputTextEntry);
    }
  );

  
When(
  /^i type desc "([^"]*)" in field "([^"]*)"$/,
  function (inputTextEntry, inputName) {   
    return browser.driver
      .findElement(by.css('[formControlName="' + inputName + '"]'))
      .sendKeys(inputTextEntry);
    }
  );
  
When(
  /^i type address "([^"]*)" in field "([^"]*)"$/,
  function (inputTextEntry, inputName) {   
    return browser.driver
      .findElement(by.css('[name="' + inputName + '"]'))
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
  

When(/^I click on button "([^"]*)"$/, function (buttonName) {
  return browser.driver
    .findElement(by.css('[name="' + buttonName + '"]'))
    .click();
});

When(/^I wait for (\d+) miliseconds$/, function (timeToWait, callback) {
  setTimeout(callback, timeToWait);
});

Then(/^the page should show "([^"]*)"$/, function (text) {
  browser.driver.sleep(1000);
  browser.waitForAngular().then(function () {
    expect(
      element(by.css('[name="successMessage"]')).getText()
    ).to.eventually.equal(text);
  });
});

Then(/^the page should show a message saying "([^"]*)"$/, function (text) {
  browser.driver.sleep(1000);
  browser.waitForAngular().then(function () {
    expect(
      element(by.css('[name="alertMessage"]')).getText()
    ).to.eventually.equal(text);
  });
});
