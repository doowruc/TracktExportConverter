var watchedShowsTable;

$("#ImportFile").change(function (e) {
    const files = e.target.files;
    if (files.length === 0) {
        $("#ImportFile").val("");
        return;
    }
    if (window.FormData === undefined) {
        errorAlert("This browser doesn't support HTML5 file uploads");
        $("#ImportFile").val("");
        return;
    }
    $("#import-form").submit();
});

$("#import-form").submit(function (e) {
    e.preventDefault();
    const formData = new FormData($(this)[0]);
    $.ajax({
        url: "/WatchedShows/Import",
        type: "POST",
        data: formData,
        contentType: false,
        processData: false,
        success: function (data) {
            if (data.success) {
                watchedShowsTable.clear().rows.add(data.data).draw();
            }
            else {
                errorAlert(data.error);
            }
        }
    });
});

$(function () {
    watchedShowsTable = $("#watched-shows-table").DataTable({
        dom: "<'row mb-3'<'col-12 text-end'B>>" +
            "<'row'<'col-6'l><'col-6'f>>" +
            "<'row'<'col-12'tr>>" +
            "<'row'<'col-5'i><'col-7'p>>",
        buttons: [
            {
                text: "Import",
                className: "page-link",
                action: function () {
                    $("#ImportFile").trigger("click");
                }
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
                data: "show",
                width: "70%"
            },
            {
                data: "episode",
                width: "10%",
                class: "text-center"
            },
            {
                data: {
                    "_": "watched",
                    display: "watchedDisplay"
                },
                width: "20%",
                class: "text-center"
            }
        ],
        order: [
            [2, "desc"]
        ]
    });

    watchedShowsTable.on("draw", function () {
        formatTime();
    });
});
