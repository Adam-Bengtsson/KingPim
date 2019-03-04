$(document).ready(function () {
    var links = $('.navbar ul li a');
    $.each(links, function (key, a) {
        var b = a.href.split('/');
        var c = b[3];
        var d = window.location.pathname.split('/');
        var e = d[1];
        if (c === e) {
            $(this).addClass('active');
        }
    });
});