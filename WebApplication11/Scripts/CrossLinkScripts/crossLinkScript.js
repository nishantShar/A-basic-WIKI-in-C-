$(document).ready(function () {
    function openpage(id) {
        alert('here');
        $.get('/Home/ViewPage/1', { pageId: id }, function (data) {
            $("#rData").html(data);
        });
    }
});


$(document).ready(function () {
    function abc() { }

    $('#a').on('click', abc);
});

