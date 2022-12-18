var watchedTable;

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
        url: "/Watched/Import",
        type: "POST",
        data: formData,
        contentType: false,
        processData: false,
        success: function (data) {
            if (data.success) {
                watchedTable.clear().rows.add(data.data).draw();
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
    watchedTable = $("#watched-table").DataTable({
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
                            startImport("shows");
                        }
                    },
                    {
                        text: "Movies",
                        action: function () {
                            startImport("movies");
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
                data: "title",
                width: "50%"
            },
            {
                data: {
                    "_": "playDate",
                    display: "playDateDisplay"
                },
                width: "50%",
                class: "text-end"
            }
        ],
        order: [
            [1, "desc"]
        ]
    });

    watchedTable.on("draw", function () {
        formatTime();
    });
});
