var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Teacher/Teacher/GetStudent"
        },
        "columns": [
            { "data": "userId" },
            { "data": "firstName" },
            { "data": "email" },
            { "data": "class" },
            {
                "data": "userId",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Teacher/Teacher/CreateResult/${data}" class=" btn btn-success text-white" style="cursor:pointer">                
                                    Create
                                </a>
                                <a href="/Teacher/Teacher/EditResult/${data}" class=" btn btn-danger text-white" style="cursor:pointer">                
                                    Edit
                                </a>
                                <a href="/Teacher/Teacher/EditResult/${data}" class=" btn btn-success text-white" style="cursor:pointer">                
                                    View Result
                                </a>
                            </div>

                            `;
                }, "width": "40%"
            }
        ]
    });
}

