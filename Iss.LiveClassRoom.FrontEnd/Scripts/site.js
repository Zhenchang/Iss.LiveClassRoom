
var Iss= {};

Iss.IndexGridFunction = function () {
    for (var key in pageGrids) {
        var grid = pageGrids[key];
        grid.onRowSelect(function (e) {
            if (e.row.Href != null && e.row.Href != '') window.location.href = e.row.Href;
        });
    }

    if ($('ul.nav.nav-tabs > li.active').length == 0) {
        $('ul.nav.nav-tabs > li').first().addClass('active');
    }
    if ($('.tab-content > div.active').length == 0) {
        $('.tab-content > div').first().addClass('active');
    }
    options = { container: 'body' };
    $('.btn').tooltip(options)

    $('#searchBtn').click(function () {
        $('#searchForm').submit();
    });
    $(".delete-button").click(function (event) {
        var deleteButton = $(this);
        var modalId = deleteButton.attr('data-delete-modal');
        var name = deleteButton.attr('data-delete-name');
        var deleteHref = deleteButton.attr('data-delete-href');
        $("#deleteModal-" + modalId + " .modal-title").html('<span class="glyphicon glyphicon-remove-sign"></span> ' + "Delete " + name);
        $("#deleteDescription-" + modalId).html(name);
        $("#deleteModal-" + modalId).modal('show');
        $("#deleteModal-" + modalId).on('hidden.bs.modal', function () {
            deleteButton.attr('data-delete-current', 'off');
        })
        deleteButton.attr('data-delete-current', 'on');
        event.stopPropagation();
    });

    $(".deleteBtn").click(function () {
        $("a[data-delete-current='on']").each(function () {
            var currentButton = $(this);
            var deleteHref = currentButton.attr('data-delete-href');
            $.post(deleteHref, function () {
                currentButton.closest('tr').remove();
            })
            .fail(function () {
                currentButton.closest('tr').addClass('danger');
            })
            .always(function () {
                currentButton.attr('data-delete-current', 'off');
                $("div[id^='deleteModal']").modal('hide');
            });
        });
    });

    $('.modal').bind('hidden.bs.modal', function () {
        $("html").css("margin-right", "0px");
    });
    $('.modal').bind('show.bs.modal', function () {
        $("html").css("margin-right", "-15px");
    });

}
function go(id, controller) {
    window.location.href = window.location.protocol + "//" + window.location.host + "/" + controller + "/Details/" + id;
}



Iss.ChatFunction = function (feedId) {

    var chatHub = $.connection.chatHub;


    $.connection.hub.start().done(function () {
        $("#commentForm button").click(function () {
            if ($("textarea[name=commentTxt]").val() == '') return;
            chatHub.server.send(feedId, $("textarea[name=commentTxt]").val());
            $("textarea[name=commentTxt]").val('');
            $("textarea[name=commentTxt]").text('');
        });
    });

    // Create a function that the hub can call to broadcast messages.
    chatHub.client.broadcastMessage = function (name, message, time) {
        Iss.AddMessageToView(name, message, time);
        Iss.RefreshView();
    }


}


Iss.AddMessageToView = function (name, message, time) {
    var comments = $("#comments");
    var dummyMedia = $("#dummyMedia").html();
    dummyMedia = dummyMedia.replace("{0}", name);
    dummyMedia = dummyMedia.replace("{1}", message);
    dummyMedia = dummyMedia.replace("{2}", time);
    comments.append(dummyMedia);
};

Iss.RefreshView = function (name, message, time) {
    var comments = $("#comments");
    var commentDiv = comments.closest('div');
    commentDiv.animate({ scrollTop: commentDiv.prop("scrollHeight") - commentDiv.height() }, 500);
};