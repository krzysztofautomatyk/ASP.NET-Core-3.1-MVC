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
            "url": "/Admin/Kategoria/GetAll"
        },
        "columns": [
            // Tu wklejamy nasze dane z modelu który został stowrzony, pola z małych liter!
            { "data": "nazwa", "width": "60%" },
            {
                "data": "id",
                // Tworzę teraz kolumne gdzie są przyciski
                "render": function (data) {
                    return `
                           <div class="text-center">
                                <a href="/Admin/Kategoria/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i>
                                </a>
                                <a class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>
                            `
                }, "width": "60%"
            }
        ]
    });
}