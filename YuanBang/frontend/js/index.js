const initUserAgent = function () {
    var sUserAgent = navigator.userAgent.toLowerCase();
    var bIsIpad = sUserAgent.match(/ipad/i) == "ipad";
    var bIsIphoneOs = sUserAgent.match(/iphone os/i) == "iphone os";
    var bIsMidp = sUserAgent.match(/midp/i) == "midp";
    var bIsUc7 = sUserAgent.match(/rv:1.2.3.4/i) == "rv:1.2.3.4";
    var bIsUc = sUserAgent.match(/ucweb/i) == "ucweb";
    var bIsAndroid = sUserAgent.match(/android/i) == "android";
    var bIsCE = sUserAgent.match(/windows ce/i) == "windows ce";
    var bIsWM = sUserAgent.match(/windows mobile/i) == "windows mobile";
    if (bIsIpad || bIsIphoneOs || bIsMidp || bIsUc7 || bIsUc || bIsAndroid || bIsCE || bIsWM) {
        window.location.href = "/Mobile/Index";
    }
};
const initSlide = function () {
    jQuery(".app-slide").slide({
        mainCell: ".bd ul",
        autoPlay: true,
        effect: 'fold',
        interTime: 5000,
        easing: 'swing'
    });
    jQuery(".app-profile-slide").slide({
        mainCell: ".bd ul",
        autoPlay: true,
        effect: 'fold',
        interTime: 5000,
        easing: 'swing'
    });
};
const initNotices = function () {
    $.ajax({
        url: '/Admin/GetNotices',
        data: {
            queryInfo: {
                Type: null,
            },
            pageInfo: {
                PageIndex: 1,
                pageSize: 6
            }
        },
        type: 'post',
        dataType: 'json',
        success: function (notices) {
            let template = $.templates('#newsItem');
            let list = notices.list;
            list.forEach(function (notice) {
                $('#news').append(template.render(notice));
            });
        }
    });
};
const initQuery = function () {
    $('#app-order-query-btn').bind('click', function () {
        const order = $('#app-order-num').val();
        window.location.href = "/Home/Query/?order=" + order;
    });

    $(document).keydown(function (event) {
        if (event.keyCode === 13) {
            const order = $('#app-order-num').val();
            window.location.href = "/Home/Query/?order=" + order;
        }
    });
};