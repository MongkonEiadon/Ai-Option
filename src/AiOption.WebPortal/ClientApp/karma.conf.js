// karma.conf.js
module.exports = function(config) {
    config.set({
      // normal config stuffs
  
      reporters: ['nyan'],

      browser: ['Chrome'],
  
      // reporter options
      nyanReporter: {
        // suppress the error report at the end of the test run
        suppressErrorReport: true, // default is false
  
        // suppress the red background on errors in the error
        // report at the end of the test run
        suppressErrorHighlighting: true, // default is false
  
        // increase the number of rainbow lines displayed
        // enforced min = 4, enforced max = terminal height - 1
        numberOfRainbowLines: 100, // default is 4
  
        // only render the graphic after all tests have finished.
        // This is ideal for using this reporter in a continuous
        // integration environment.
        renderOnRunCompleteOnly: true // default is false
      }
    });
  };