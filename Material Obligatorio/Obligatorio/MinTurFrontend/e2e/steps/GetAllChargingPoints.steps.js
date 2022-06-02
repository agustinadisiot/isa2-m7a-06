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

Given(/^I view the "([^"]*)"$/, function (url, callback) {
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

Then(/^the page should load a list of Charging Points$/, function () {
  let chargingPoints = element.all(by.css('chargingPoints'))
  expect(chargingPoints.count()).to.eventually.notEmpty().and.notify(callback);
});

Then(/^the page should show a message saying "([^"]*)"$/, function () {
  let chargingPoints = element.all(by.repeater('chargingPoints'))
  expect(chargingPoints.count()).to.eventually.equal(not (null)).and.notify(callback);
  expect(chargingPoints.get(1).getItem().getId()).to.eventually.equal(1).and.notify(callback);
});
