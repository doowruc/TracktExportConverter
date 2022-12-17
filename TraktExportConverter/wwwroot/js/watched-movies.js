$(function () {
    const columns = [
        {
            data: "title",
            width: "50%"
        },
        {
            data: {
                "_": "watched",
                display: "watchedDisplay"
            },
            width: "50%",
            class: "text-end"
        }
    ];

    initializeTable($("#watched-movies-table"), columns, 1, "/WatchedMovies/Import");
});
