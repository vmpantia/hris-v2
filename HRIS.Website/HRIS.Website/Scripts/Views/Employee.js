var Popup, dataTable;
$(document).ready(function () {
    dataTable = $('#employeeTable').DataTable({
        "ajax": {
            "url": "/Employee/GetData",
            "type": "GET",
            "dataType": "json"
        },
        "columnDefs": [
            { "targets": 0, "className": "text-center", "width": "15px", "orderable": false },
            { "targets": 4, "className": "text-center", "width": "15px" }
        ],
        "order": [[1, 'asc']],
        "columns": [
            {
                "data": null,
                "defaultContent": "<a class='cstm-page-table-button edit' href='@Url.Action('EditEmployee','Home', new {internalID = 'EmployeeID' })'><div class='icon'><i class='fa fa-pencil-square-o'></i></div><label class='text'>Edit</label></a>",
                "orderable": false
            },
            { "data": "EmployeeID" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return ConvertNameAndGenderToHTML(row.FirstName, row.LastName, row.MiddleName, row.Gender);
                }
            },
            { "data": "Birthdate" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return CovertStatusToHTML(row.Status);
                }
            }
        ],
        "language": {
            "emptyTable": "No data found, Please click on <b>Add New</b> Button"
        }
    });
});

function GenerateEditButton(internalID) {
    return "<a class='cstm-page-table-button edit' href='@Url.Action('EditEmployee','Home', new {internalID = 'EmployeeID' })'><div class='icon'><i class='fa fa-pencil-square-o'></i></div><label class='text'>Edit</label></a>"
}

function ConvertNameAndGenderToHTML(firstName, lastName, middleName, gender) {
    if (firstName === '' && lastName === '' && middleName === '') {
        return 'N/A';
    }

    var fullName = ''
    if (middleName === '' || middleName === null) {
        fullName = lastName.concat(", ", firstName);
    }
    else {
        fullName = lastName.concat(", ", firstName, " ", middleName.substring(0, 1), ".");
    }

    if (gender === 'FEMALE') {
        return '<i class="fa fa-venus" style="color: #fd79a8; font-weight: bold;" aria-hidden="true"></i> &nbsp' + fullName;
    }
    return '<i class="fa fa-mars" style="color: #0984e3; font-weight: bold;" aria-hidden="true"></i> &nbsp' + fullName;
}

function CovertStatusToHTML(status) {
    if (status === 0) {
        return '<span class="badge bg-success">Enabled</span>';
    }
    if (status === 1) {
        return '<span class="badge bg-secondary">Disabled</span>';
    }
    return '<span class="badge bg-danger">For Deletion</span>';
}

function PopupForm(url) {
    var formDiv = $('<div/>');
    $.get(url)
        .done(function (response) {
            formDiv.html(response);

            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                title: 'Fill Employee Details',
                height: 500,
                width: 700,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });
        });
}

function SubmitForm(form) {
    $.validator.unobtrusive.parse(form);
    var isValid = $(form).valid()
    if (isValid) {
        $.ajax({
            type: "POST",
            url: form.action,
            data: $(form).serialize(),
            success: function (data) {
                if (data.success) {
                    Popup.dialog('close');
                    dataTable.ajax.reload();
                }
            }
        });
    }
    return false;
}