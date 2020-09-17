var dataTable;

$(document).ready(function () {
    loadDataTable();
});

//Make sure casing is correct
//Make sure DataTable was imported in _Layout.cshtml
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Category/GetAll"
        },
        "columns": [
            {"data": "name", "width": "60%"}, //Inside Category.cs we had it named as "Name". Since we're camelCasing, we need to make this lowercase "name" here
            {
                "data": "id",
                "render": function (data) { //the data is the id
                    return `
                            <div class="text-center">
                                <a href="/Admin/Category/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fa fa-edit"></i>
                                </a>
                                <a href="/Admin/Category/Delete" class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fa fa-trash-alt"></i>
                                </a>
                                </div>
                            `;
                }, "width": "40%"
            }
        ]
    })
}