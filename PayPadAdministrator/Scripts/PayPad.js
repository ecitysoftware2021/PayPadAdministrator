function Dateformat(date) {
    var year = date.split(',')[1];
    var mes = date.split(',')[0].split(' ')[1];
    var day = date.split(',')[0].split(' ')[0];
    return year + "-" + GetMonth(mes) + "-" + day;
}

function GetDateFormat(date) {
    var datereturn = new Date(date);
    return datereturn.getDate() + '/' +
        (datereturn.getMonth() + 1) + '/' +
        datereturn.getFullYear();
}

function ConvertDate(date) {
    var myDate = new Date(date.match(/\d+/)[0] * 1);
    var newdate = GetDateFormat(myDate);
    return newdate;
}

function ConvertDateV2(date) {
    var myDate = new Date(date.match(/\d+/)[0] * 1);
    var newdate = GetDateFormatHour(myDate);
    return newdate;
}


function GetDateFormatHour(date) {
    var datereturn = new Date(date);
    return datereturn.getDate() + '/' +
        (datereturn.getMonth() + 1) + '/' +
        datereturn.getFullYear() + ' ' + datereturn.getHours() + ':' + datereturn.getMinutes();
}


$(window).load(function () {
    var $container = $('.portfolioContainer');
    $container.isotope({
        filter: '*',
        animationOptions: {
            duration: 750,
            easing: 'linear',
            queue: false
        }
    });

    $('.portfolioFilter a').click(function(){
        $('.portfolioFilter .current').removeClass('current');
        $(this).addClass('current');

        var selector = $(this).attr('data-filter');
        $container.isotope({
            filter: selector,
            animationOptions: {
                duration: 750,
                easing: 'linear',
                queue: false
            }
        });
        return false;
    });
});
$(document).ready(function() {
    $('.image-popup').magnificPopup({
        type: 'image',
        closeOnContentClick: true,
        mainClass: 'mfp-fade',
        gallery: {
            enabled: true,
            navigateByImgClick: true,
            preload: [0,1] // Will preload 0 - before current, and 1 after the current image
        }
    });
});

