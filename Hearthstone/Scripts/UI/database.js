$(function () {

    // Tag頁籤功能
    // 預設顯示第一個 Tab
    var _showTab = 0;
    var $defaultLi = $('ul.tabs li a').eq(_showTab).addClass('selected');
    $($defaultLi.find('a').attr('href')).siblings().hide();

    // 當 li 頁籤被點擊時...
    // 若要改成滑鼠移到 li 頁籤就切換時, 把 click 改成 mouseover
    $('ul.tabs li a').click(function () {
        // 找出 li 中的超連結 href(#id)
        var $this = $(this);
		var	_clickTab = $this.attr('href').substring(1);
        // 把目前點擊到的 li 頁籤加上selected &移除 other class
        $('ul.tabs li a').removeClass('selected');
        $this.addClass('selected');

        // 切換Table
        if (_clickTab == "pic") {
            $("#tab-list").hide();
            $("#tab-pic").show();
        } else if (_clickTab == "list") {
            $("#tab-pic").hide();
            $("#tab-list").show();
        }

        return false;
    });
});


    //設定tooltip彈出圖片
    $(document).on("mouseenter", ".listview-mode-default > tbody > tr > td > .iconmedium > a", function () {
        var a = $(this).data("id"),
            b = $(this).offset(),
            c = $(this).width();
        SetToolTip(a, b, c);
    });
$(document).on("mouseenter", ".listview-mode-default > tbody > tr > td > #title > a", function () {
    var a = $(this).data("id"),
        b = $(this).offset(),
        c = $(this).width();
    SetToolTip(a, b, c);
});

$(document).on("mouseleave", ".listview-mode-default > tbody > tr > td > .iconmedium > a", function () {
    $(".hs-tooltip").hide()
})
$(document).on("mouseleave", ".listview-mode-default > tbody > tr > td > #title > a", function () {
    $(".hs-tooltip").hide()
})

function SetToolTip(a, b, c) {
    $(".hs-tooltip").css({
        top: b.top,
        left: b.left + c + 5
    }).css("visibility", "visible");

    $(".hs-tooltip .hearthhead-tooltip-image").attr("src", "http://wow.zamimg.com/images/hearthstone/cards/enus/original/" + a + ".png")
    $(".hs-tooltip").css({
        top: b.top - $(window).scrollTop() < $(".hs-tooltip").height() ? b.top : b.top - $(".hs-tooltip").height(),
        left: b.left + c + 20
    }),
    $(".hs-tooltip").show()
}

//Table 全部撈的 JS分頁製作
var pageSize = 30;    //每頁顯示的記錄條數
var curPage = 0;        //當前頁
var lastPage;        //最後頁
var direct = 0;        //方向
var len;            //總行數
var page;            //總頁數
var begin;
var end;

$(document).ready(function display() {
    len = $("#cardList tr").length;    // 求這個表的總行數
    page = len % pageSize == 0 ? len / pageSize : Math.floor(len / pageSize) + 1;//根據記錄條數，計算頁數
    // alert("page==="+page);
    curPage = 1;    // 設置當前為第一頁
    displayPage(1);//顯示第一頁
    setPageInfo(curPage, page);

    $(".act_first").click(function firstPage() {    // 首頁
        curPage = 1;
        direct = 0;
        displayPage();
    });
    $(".act_previous").click(function frontPage() {    // 上一頁
        direct = -1;
        displayPage();
    });
    $(".act_next").click(function nextPage() {    // 下一頁
        direct = 1;
        displayPage();
    });
    $(".act_last").click(function lastPage() {    // 尾頁
        curPage = page;
        direct = 0;
        displayPage();
    });
});

function setPageInfo(curPage, page) {
    $(".page_info").text(curPage + "/" + page + "頁");
    //document.getElementById("btn0").innerHTML = "當前 " + curPage + "/" + page + " 頁    每頁 ";    // 顯示當前多少頁

    //調整頁面選單
    if (curPage == 1 && page == 1) {
        $(".act_first").attr("data-visible", "no")
        $(".act_previous").attr("data-visible", "no")
        $(".act_next").attr("data-visible", "no")
        $(".act_last").attr("data-visible", "no")
    }else
    if (curPage == 1) {
        $(".act_first").attr("data-visible", "no")
        $(".act_previous").attr("data-visible", "no")
        $(".act_next").attr("data-visible", "yes")
        $(".act_last").attr("data-visible", "yes")
    }
    else if (curPage == page) {
        $(".act_first").attr("data-visible", "yes")
        $(".act_previous").attr("data-visible", "yes")
        $(".act_next").attr("data-visible", "no")
        $(".act_last").attr("data-visible", "no")
    }
    else {
        $(".act_first").attr("data-visible", "yes")
        $(".act_previous").attr("data-visible", "yes")
        $(".act_next").attr("data-visible", "yes")
        $(".act_last").attr("data-visible", "yes")
    }
}

function displayPage() {
    if (curPage <= 1 && direct == -1) {
        direct = 0;
        alert("已經是第一頁了");
        return;
    } else if (curPage >= page && direct == 1) {
        direct = 0;
        alert("已經是最後一頁了");
        return;
    }

    lastPage = curPage;

    // 修復當len=1時，curPage計算得0的bug
    if (len > pageSize) {
        curPage = ((curPage + direct + len) % len);
    } else {
        curPage = 1;
    }

    //document.getElementById("btn0").innerHTML = "當前 " + curPage + "/" + page + " 頁    每頁 ";        // 顯示當前多少頁

    begin = (curPage - 1) * pageSize ;// 起始記錄號
    end = begin + 1 * pageSize - 1;    // 末尾記錄號

    if (end > len) end = len;
    $("#cardList tr").hide();    // 首先隱藏
    $("#cardList tr").each(function (i) {    // 然後，通過條件判斷決定本行是否恢復顯示
        if ((i >= begin && i <= end))//顯示begin<=x<=end的記錄
            $(this).show();
    });

    setPageInfo(curPage, page);
}

//排序
$('th').click(function () {
    var table = $(this).parents('table').eq(0)
    var rows = table.find('tr:gt(0)').toArray().sort(comparer($(this).index()+1))
    this.asc = !this.asc
    if (!this.asc) { rows = rows.reverse() }
    for (var i = 0; i < rows.length; i++) { table.append(rows[i]) }

    displayPage();
})
function comparer(index) {
    return function (a, b) {
        var valA = getCellValue(a, index), valB = getCellValue(b, index)
        return $.isNumeric(valA) && $.isNumeric(valB) ? valA - valB : valA.localeCompare(valB)
    }
}
function getCellValue(row, index) { return $(row).children('td').eq(index).html() }
