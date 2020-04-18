var dataTable;

// Jeśli dokument się załadował
$(document).ready(function () {
    loadDataTable();
})

// Wykorzystuje info z strony https://datatables.net/examples/ajax/index.html
// "tblData" -> nazwa tabeli w cshtml
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            // Ścieżka gdzie są nasze wszystkie dane z API Kontrollera
            "url": "/Admin/Company/GetAll"
        },
        "columns": [
            // Tu wklejamy nasze dane z modelu który został stowrzony, pola z małych liter!
            { "data": "name", "width": "15%" },
            { "data": "streetAddress", "width": "15%" },
            { "data": "city", "width": "10%" },
            { "data": "state", "width": "10%" },
            { "data": "phoneNumber", "width": "15%" },
            {
                "data": "isAuthorizedCompany",
                "render": function (data) {
                    if (data) {
                        return `<input type="checkbox" disabled checked />`
                    }
                    else {
                        return `<input type="checkbox" disabled/>`
                    }
                },
                "width": "10%"
            },
            {
                "data": "id",
                // Tworzę teraz kolumne gdzie są przyciski
                "render": function (data) {
                    return `
                           <div class="text-center">
                                <a href="/Admin/Company/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i>
                                </a>
                                <a onclick=Delete("/Admin/Company/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>
                            `
                }, "width": "25%"
            }
        ]
    });
}


//https://sweetalert2.github.io/#examples
function Delete(url) {
    swal({
        title: "Czy jesteś pewny/a, że chcesz usunąć to ?",
        text: "Nie będzie możliwe wrócenie do tych danych!",
        icon: "warning",
        buttons: ["Anuluj", "Usuń"],
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
                        console.error(
                            "dsasd");
                    }
                }
            });
        }
    });
}