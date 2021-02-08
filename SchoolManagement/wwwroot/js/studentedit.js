var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblStudent').DataTable({
        "ajax": {
            "url":"/Admin/Edit/GetAll"
        },
        "columns": [
            { "data": "userId" },
            { "data": "firstName" },
            { "data": "lastName" },
            { "data": "gender" },
            { "data": "dob" },
            { "data": "email" },
            {"data": "imageUrl"},
            { "data": "class" },
            {
                "data": "userId",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/Edit/EditStudent/${data}" class=" btn btn-success text-white" style="cursor:pointer">                
                                    EDIT
                                </a>
                            </div>

                            `;
                }, "width": "40%"
            }
        ]
    });
}

