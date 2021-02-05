var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Leave/GetAll"

        },
        "columns": [
            { "data": "name"},
            { "data": "leaveDays" },
            { "data": "startDate" },
            { "data": "teacherId", "render": function (data)
                {
                    return `
                            <div class="text-center">
                                <a href="/Admin/Leave/Approved/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    Approve
                                </a>
                            </div>  
                            `;
                 },
                "width": "40%"
            }
        ]
    });
}

