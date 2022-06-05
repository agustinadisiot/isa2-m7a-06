'use strict';

var { Given } = require('cucumber');
var { When } = require('cucumber');
var { Then } = require('cucumber');

// Use the external Chai As Promised to deal with resolving promises in
// expectations
const chai = require('chai');
const chaiAsPromised = require('chai-as-promised');
const {loadavg} = require("os");
chai.use(chaiAsPromised);
const expect = chai.expect;


Given(/^I view the list on "([^"]*)"$/, function (url, callback) {
  browser.get(url).then(function () {
    callback();
  });
});

When(/^I click on list "([^"]*)"$/, function (listName) {
  return browser.driver
    .findElement(by.css('[name="' + listName + '"]'))
    .click();
});

When(/^I click on button "([^"]*)"$/, function (buttonName) {
  return browser.driver
    .findElement(by.css('[name="' + buttonName + '"]'))
    .click();
});

When(/^I wait for the list (\d+) ms$/, function (timeToWait, callback) {
  setTimeout(callback, timeToWait);
});


Then(/^the page should load a new list of Charging Points$/, function (callback) {
  let loadedCorrectly = element(by.id('loadCorrectly'))
  expect(loadedCorrectly.getText()).to.eventually.equal("Ancap Maldonado3").and.notify(callback);
});
