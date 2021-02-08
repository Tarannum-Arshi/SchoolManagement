var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblTeacher').DataTable({
        "ajax": {
            "url":"/Admin/Edit/GetAll1"
        },
        "columns": [
            { "data": "userId" },
            { "data": "firstName"},
            { "data": "lastName"},
            { "data": "gender"},
            { "data": "dob"},
            { "data": "email" },
            {"data":"imageUrl"},
            {"data":"salary"},
            {
                "data": "userId",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/Edit/EditTeacher/${data}" class=" btn btn-success text-white" style="cursor:pointer">
                                    EDIT
                                </a>
                            </div>

                            `;
                }, "width": "40%"
            }
        ]
    });
}

