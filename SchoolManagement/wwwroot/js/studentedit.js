var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Compounder/Patient/GetAll"
        },
        "columns": [
            { "data": "firstName"},
            { "data": "lastName"},
            { "data": "gender"},
            { "data": "dob"},
            { "data": "email" },
            {"data":"class"},
                
           
            {
                "data": "studentDetailsId",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Compounder/Patient/Upsert/${data}" class=" btn btn-success text-white" style="cursor:pointer">
                                   
                                    EDIT
                                </a>
                            </div>

                            `;
                }, "width": "40%"
            }
        ]
    });
}

