var dt = window.luxon.DateTime;

var getLocalTime = value => {
    var local = dt.fromISO(value, { zone: "utc" }).toLocal();
    return `${local.toLocaleString(dt.DATE_SHORT)} ${local.toLocaleString(dt.TIME_24_WITH_SECONDS)}`;
}

var formatTime = () => {
    $("time").each(function () {
        $(this).text(getLocalTime($(this).data("value")));
    });
};

var errorAlert = message => {
    bootbox.alert({
        title: `<span class="text-danger"><i class="fa-solid fa-circle-exclamation fa-lg"></i> Error</span>`,
        message: message,
        closeButton: false,
        buttons: {
            ok: {
                className: "btn btn-primary btn-sm"
            }
        }
    });
};
