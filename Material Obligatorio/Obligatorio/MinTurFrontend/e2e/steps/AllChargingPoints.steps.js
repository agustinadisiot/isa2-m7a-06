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


Given(/^there are no charging points when I view the "([^"]*)"$/, function (url, callback) {
  browser.get(url).then(function () {
    callback();
  });
});

When(/^I click on button Charging Points$/, function (callback) {
  element(by.id('getChargingPoints')).click();
  callback();
});

When(/^I wait for (\d+) ms$/, function (timeToWait, callback) {
  setTimeout(callback, timeToWait);
});


Then(/^the page should show a message saying "([^"]*)"$/, function (message, callback) {
    let errorLoading = element(by.id('loadIncorrectly'))
    expect(errorLoading.getText()).to.eventually.equal(message).and.notify(callback);

});

Then(/^the page should load a list of Charging Points with the name "([^"]*)" we just created$/, function (name, callback) {
  let loadedCorrectly = element(by.id('loadCorrectly'))
  expect(loadedCorrectly.getText()).to.eventually.equal(name).and.notify(callback);
});
