var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblResult').DataTable({
        "ajax": {
            "url": "/Student/Student/GetResult"
        },
        "columns": [
            { "data": "userId" },
            { "data": "maths" },
            { "data": "science" },
            { "data": "english" },
            { "data": "hindi" },
            { "data": "computer" },
            {
                "data": "userId",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                //<a href="/Teacher/Teacher/CreateResult/${data}" class=" btn btn-success text-white" style="cursor:pointer">                
                                //    Create
                                //</a>
                                //<a href="/Teacher/Teacher/EditResult/${data}" class=" btn btn-danger text-white" style="cursor:pointer">                
                                //    Edit
                                //</a>
                            </div>
                            `;
                }, "width": "40%"
            }
        ]
    });
}

