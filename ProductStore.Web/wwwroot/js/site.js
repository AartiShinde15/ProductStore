// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('.navbar-nav li a').click(function (e) {

        $('.navbar-nav li.active').removeClass('active');

        var $parent = $(this).parent();
        $parent.addClass('active');
    });
});

$(document).ready(function () {
    $('#basic-text1').click(function () {
        $('Form').submit();
    });
});



var currentValue = 0;
function handleClick1(ref, cnt) {
    currentValue = ref.value;

    if (cnt == 1) {
        $('#Rating1').value = currentValue
    }
    else if (cnt == 2) {
        $('#Rating2').value = currentValue
    }
    else if (cnt == 3) {
        $('#Rating3').value = currentValue
    }
    else if (cnt == 4) {
        $('#Rating4').value = currentValue
    }
    else {
        $('#Rating5').value = currentValue
    }
}


$(document).ready(function () {
    $('#tblList').DataTable();
});