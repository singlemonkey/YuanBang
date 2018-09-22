/**
 * 表格通用类
 * */
class Table {
    /**
     * 参数说明
     * @param {object} props - json数据，将参数整合
     * @param {object} props.pageInfo - 必填，表格分页
     * @param {object} props.url - 必填，表格数据加载地址
     * @param {object} props.callback - 选填，回调函数，数据加载完触发
     */
    constructor(props) {
        this.container = props.container;
        this.pageInfo = {
            pageSize: props.rows || 8,
            pageIndex: 1
        };
        this.queryInfo = props.queryInfo;
        this.url = props.url;
        this.callback = props.callback || function () { };
        this.template = props.template;
        this.count = 0;

        this._bindEvent();
    }

    query(queryInfo) {
        let self = this;
        if (typeof queryInfo !== 'undefined') {
            Object.assign(self.queryInfo, queryInfo);
        }

        $.ajax({
            url: this.url,
            data: {
                queryInfo: self.queryInfo,
                pageInfo: self.pageInfo
            },
            type: 'post',
            dataType: 'json',
            success: function (data) {
                self._renderRows(data.list);
                self._renderPage(data.count);
                
                //TODO:判断如果没有任何数据，显示空行提示
                //TODO:判断如果当页没有数据，则切换到前一页
            }
        });
    }

    _renderRows(list) {
        let self = this;
        $(self.container).empty();
        if (list.length !== 0) {
            list.forEach(function (currentValue) {
                $(self.container).append(self.template.render(currentValue));
            });
        }
    }

    _renderPage(count) {
        $('.app-table-pages').empty();
        //计算总共有多少页
        let pages = Math.ceil(count / this.pageInfo.pageSize);
        for (var i = 1; i < pages + 1; i++) {
            let li = $('<li></li>', {
                'data-page': i
            });
            let a = $('<a></a>', {
                herf: 'javascript:;',
                'data-page': i,
                text:i
            });
            li.append(a);
            $('.app-table-pages').append(li);
        }

        $('.app-table-pages [data-page=' + this.pageInfo.pageIndex + ']').addClass('active');
    }

    
    getSelect() {
        const IDs= $('.selectItem:checked').map(function (index, item) {
            return $(item).data('id');
        }).get();
        return IDs;
    }

    _bindEvent() {
        let self = this;
        $('.app-table-pages').on('click', 'li', function () {
            let pageIndex = $(this).data('page');
            self.pageInfo.pageIndex = pageIndex;
            self.query();
        });

        $(".selectAll").on("click", function () {
            let checked = $(this).prop("checked");
            $(".selectItem").prop("checked", checked);
        });

        $(self.container).on('click', '.selectItem', function () {
            let flag = true;
            $(".selectItem").each((i, e) => {
                let checked = $(e).prop("checked");
                if (checked) {
                    return true;
                } else {
                    flag = false;
                    return flag;
                }
            });
            $(".selectAll").prop("checked", flag);
        });
    }
}

$(function () {
    jQuery.extend({
        ///$.formatDate(new Date(parseInt(item.CreateDate.substr(6, 13))));
        formatDate: function (date) {
            var y = date.getFullYear();
            var m = date.getMonth() + 1;
            m = m < 10 ? '0' + m : m;
            var d = date.getDate();
            d = d < 10 ? '0' + d : d;
            var h = date.getHours();
            var minute = date.getMinutes();
            minute = minute < 10 ? '0' + minute : minute;
            if (h !== "00" || minute !== "00") {
                return y + '-' + m + '-' + d + ' ' + h + ':' + minute;
            } else {
                return y + '-' + m + '-' + d;
            }
        },
        HTMLDecode(text) {
            var temp = document.createElement("div");
            temp.innerHTML = text;
            var output = temp.innerText || temp.textContent;
            temp = null;
            return output;
        }
    });
});