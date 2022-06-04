
exports.config = {

  seleniumAddress: 'http://localhost:4444/wd/hub', // This is targetting your local running instance of the selenium webdriver
  //directConnect: true,
  specs: [
    './specs/*.feature'
  ],

  capabilities: {
    browserName: 'firefox' // You can use any browser you want.
  },

  framework: 'custom', //We need this line to use the cucumber framework

  frameworkPath: require.resolve('protractor-cucumber-framework'), // Here it is

  cucumberOpts: {
    require: './steps/*.js', // This is where we'll be writing our actual tests
  },

  baseURL: 'http://localhost:4200/',
};


