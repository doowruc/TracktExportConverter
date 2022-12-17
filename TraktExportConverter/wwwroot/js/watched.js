$("#import-file").change(function (e) {
    const files = e.target.files;
    if (files.length === 0) {
        $("#import-file").val("");
        return;
    }
    if (window.FormData === undefined) {
        errorAlert("This browser doesn't support HTML5 file uploads");
        $("#import-file").val("");
        return;
    }
    $("#import-form").submit();
});

var initializeTable = (table, columns, sort, importUrl) => {
    var dataTable = table.DataTable({
        dom: "<'row mb-3'<'col-12 text-end'B>>" +
            "<'row'<'col-6'l><'col-6'f>>" +
            "<'row'<'col-12'tr>>" +
            "<'row'<'col-5'i><'col-7'p>>",
        buttons: [
            {
                text: "Import",
                className: "page-link",
                action: function () {
                    $("#import-file").trigger("click");
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
        columns: columns,
        order: [
            [sort, "desc"]
        ]
    });

    dataTable.on("draw", function () {
        formatTime();
    });

    $("#import-form").on("submit", function (e) {
        e.preventDefault();
        const formData = new FormData($(this)[0]);
        $.ajax({
            url: importUrl,
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.success) {
                    dataTable.clear().rows.add(data.data).draw();
                }
                else {
                    errorAlert(data.error);
                }
            }
        });
    });

    return dataTable;
};
