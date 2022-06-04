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

When(/^I click on button Delete Charging Points$/, function (callback) {
  element(by.id('deleteChargingPoint')).click();
  callback();
});

When(/^I wait for the list (\d+) ms$/, function (timeToWait, callback) {
  setTimeout(callback, timeToWait);
});


Then(/^the page should load a message saying "([^"]*)"$/, function (message) {
  let errorLoading = element(by.id('deletedCorrectly'))
  expect(errorLoading.getText()).to.eventually.equal(message);
});
