// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var current = 1;
var height = jQuery(".ticker").height();
var numberDivs = jQuery(".ticker").children().length;
var first = jQuery(".ticker h1:nth-child(1)");
setInterval(function() {
        var number = current * -height;
        first.css("margin-top", number + "px");
        if (current === numberDivs) {
            first.css("margin-top", "0px");
            current = 1;
        } else current++;
    },
    2500);