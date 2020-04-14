// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function SearchFunc() {
    var input, filter, a, tbody, a, i;
    input = document.getElementById("mySearch");
    filter = input.value.toUpperCase();
    Filter(filter);
}

function myFunction() {
    var input = document.getElementById("mySelect");
    var i = input.selectedIndex;
    var selectedOption = input.options[i].text;
    Filter(selectedOption);
}

function Filter(filter) {
    var a = document.getElementById("myMenu");
    var b = a.getElementsByTagName("tr")
    for (i = 1; i < b.length; i++) {
        a = b[i].children[3].firstChild.data;

        if (a.toUpperCase().indexOf(filter.toUpperCase()) > -1) {
            b[i].style.display = "";
        } else {
            b[i].style.display = "none";
        }
    }
}

$(document).ready(function () {
    $("#news").click(function () {
        $("#content").toggle();
    });
});