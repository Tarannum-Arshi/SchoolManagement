var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblResult').DataTable({
        "ajax": {
            "url":"/Teacher/Teacher/GetAll"
        },
        "columns": [
            { "data": "subjectId" },
            { "data": "firstName"},
            { "data": "email"},
            { "data": "class"},
            { "data": "maths"},
            { "data": "science" },
            { "data": "english" },
            { "data": "hindi" },
            { "data": "computer" },
            {
                "data": "subjectId",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Teacher/Teacher/EditResult/${data}" class=" btn btn-success text-white" style="cursor:pointer">
                                    EDIT
                                </a>
                            </div>

                            `;
                }, "width": "40%"
            }
        ]
    });
}

