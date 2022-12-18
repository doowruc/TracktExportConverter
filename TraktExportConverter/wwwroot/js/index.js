var traktType = {
    Movie: 1,
    Show: 2,
    Watchlist: 3
}

var traktTable;

var clearForm = () => {
    $("#import-type").val("");
    $("#import-file").val("");
}

$("#import-file").change(function (e) {
    const files = e.target.files;
    if (files.length === 0) {
        clearForm();
        return;
    }
    if (window.FormData === undefined) {
        errorAlert("This browser doesn't support HTML5 file uploads");
        clearForm();
        return;
    }
    $("#import-form").submit();
});

$("#import-form").submit(function (e) {
    e.preventDefault();
    const formData = new FormData($(this)[0]);
    $.ajax({
        url: "/Import",
        type: "POST",
        data: formData,
        contentType: false,
        processData: false,
        success: function (data) {
            if (data.success) {
                traktTable.clear().rows.add(data.data).draw();
            }
            else {
                errorAlert(data.error);
            }
        }
    });
});

var startImport = type => {
    $("#import-type").val(type);
    $("#import-file").trigger("click");
}

$(function () {
    traktTable = $("#trakt-table").DataTable({
        dom: "<'row mb-3'<'col-12 text-end'B>>" +
            "<'row'<'col-6'l><'col-6'f>>" +
            "<'row'<'col-12'tr>>" +
            "<'row'<'col-5'i><'col-7'p>>",
        buttons: [
            {
                extend: "collection",
                text: "Import",
                className: "page-link",
                align: "button-right",
                buttons: [
                    {
                        text: "Shows",
                        action: function () {
                            startImport(traktType.Show);
                        }
                    },
                    {
                        text: "Movies",
                        action: function () {
                            startImport(traktType.Movie);
                        }
                    },
                    {
                        text: "Watchlist",
                        action: function () {
                            startImport(traktType.Watchlist);
                        }
                    }
                ]
            },
            {
                extend: "collection",
                text: "Export",
                className: "page-link",
                align: "button-right",
                buttons: [
                    "excel",
                    "csv",
                    "spacer",
                    {
                        extend: "copy",
                        text: "Copy to clipboard"
                    }
                ]
            }
        ],
        columns: [
            {
                data: "type",
                width: "15%"
            },
            {
                data: "title",
                width: "45%"
            },
            {
                data: {
                    "_": "date",
                    display: "dateDisplay"
                },
                width: "40%",
                class: "text-end"
            }
        ],
        order: [[2, "desc"]]
    });

    traktTable.on("draw", function () {
        formatTime();
    });
});
