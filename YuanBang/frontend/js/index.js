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