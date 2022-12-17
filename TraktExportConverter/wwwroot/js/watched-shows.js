$(function () {
    const columns = [
        {
            data: "title",
            width: "34%"
        },
        {
            data: "episode",
            width: "33%",
            class: "text-center"
        },
        {
            data: {
                "_": "watched",
                display: "watchedDisplay"
            },
            width: "33%",
            class: "text-end"
        }
    ];

    initializeTable($("#watched-shows-table"), columns, 2, "/WatchedShows/Import");
});
