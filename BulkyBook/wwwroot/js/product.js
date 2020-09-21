var dataTable;

$(document).ready(function () {
    loadDataTable();
});

//Make sure casing is correct
//Make sure DataTable was imported in _Layout.cshtml
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "width": "15%"},
            { "data": "isbn", "width": "15%"},
            { "data": "price", "width": "15%"},
            { "data": "author", "width": "15%"},
            //in ProductController.cs, we have this line of code, so that's why we can access the below information
            //            var allObj = _unitOfWork.Category.GetAll(includeProperties:"Category,CoverType");
            { "data": "category.name", "width": "15%"},
            {
                "data": "id",
                "render": function (data) { //the data is the id
                    return `
                            <div class="text-center">
                                <a href="/Admin/Product/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fa fa-edit"></i>
                                </a>
                                <a onclick=Delete("/Admin/Product/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fa fa-trash-alt"></i>
                                </a>
                                </div>
                            `;
                }, "width": "40%"
            }
        ]
    })
}

function Delete(url) {
    swal({ //using Sweet Alert
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data.",
        icon: "warning",
        buttons: true, //plural "buttons"
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}