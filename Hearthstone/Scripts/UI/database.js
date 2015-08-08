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