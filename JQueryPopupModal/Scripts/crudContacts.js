var $dialog;

$(document).ready(function () {
    // Populate Contact
    LoadContacts();

    // Open popup
    $('body').on("click", "a.popup", function (e) {
        e.preventDefault();
        var page = $(this).attr('href');
        OpenPopup(page);
    });

    $('body').on('change', '#CountryId', function () {
        var countryId = $(this).val();
        LoadStates(countryId);
    });

    // Save Contracts
    $('body').on('submit', '#saveForm', function (e) {
        e.preventDefault();
        SaveContacts();
    });

    // Delete Contact
    $('body').on('submit', '#deleteForm', function (e) {
        e.preventDefault();
        DeleteContact();
    });
});

function LoadContacts() {
    $('#update_panel').html('Loading Data...');

    $.ajax({
        url: '/Home/GetContacts',
        type: 'GET',
        dataType: 'json',
        success: function (d) {
            if (d.length > 0) {
                var $data = $('<table></table>').addClass('table table-responsive table-striped');
                var header = '<thead><tr><th>Contact Person</th><th>Contact No</th><th>Country</th><th>State</th><th></th></tr></thead>';
                $data.append(header);

                $.each(d, function (i, row) {

                    var $row = $('<tr/>');
                    $row.append($('<td/>').html(row.ContactPerson));
                    $row.append($('<td/>').html(row.ContactNo));
                    $row.append($('<td/>').html(row.CountryName));
                    $row.append($('<td/>').html(row.StateName));
                    $row.append($('<td/>').html("<a href='/Home/Save/" + row.ContactId + "' class='popup'>Edit</a>&nbsp;|&nbsp;<a href='/Home/Delete/" + row.ContactId + "' class='popup'>Delete</a>"));
                    $data.append($row);
                });

                $('#update_panel').html($data);
            }
            else {
                var $onData = $('<div/>').html('No Data Found!');
                $('#update_panel').html($onData);
            }
        },
        error: function () {
            alert('Error! Please try again');
        }
    });
}

// open popup
function OpenPopup(Page) {
    var $pageContent = $('<div />');
    $pageContent.load(Page);
    $dialog = $('<div class="popupWindow" style="overflow: hidden"></div>')
                .html($pageContent)
                .dialog({
                    draggable: false,
                    autoOpen: false,
                    resizable: false,
                    model: true,
                    height: 600,
                    width: 600,
                    close: function () {
                        $dialog.dialog('destroy').remove();
                    }
                })
    $dialog.dialog('open');
}

// Cascade dropdown - Populate states of selected country
function LoadStates(countryId) {
    var $state = $('#StateId');
    $state.empty();
    $state.append($('<option></option>').val('').html('Please Wait...'));
    if (countryId == null || countryId == "") {
        $state.empty();
        $state.append($('<option></option>').val('').html('Select State'));
        return;
    }

    $.ajax({
        url: '/Home/GetStateList',
        type: 'GET',
        data: { 'countryId': countryId },
        dataType: 'json',
        success: function (d) {
            $state.empty();
            $state.append($('<option></option>').val('').html('Select State'));
            $.each(d, function (i, val) {
                $state.append($('<option></option>').val(val.StateId).html(val.StateName));
            });
        },
        error: function () {

        }
    });
}

// Save Contact
function SaveContacts() {
    // Validation
    if ($('#ContactPerson').val().trim() == '' ||
        $('#ContactNo').val().trim() == '' ||
        $('#CountryId').val().trim() == '' ||
        $('#StateId').val().trim() == '') {
        $('#msg').html('<div class="failed">All fields are required.</div>');
        return false;
    }

    var contact = {
        ContactId: $('#ContactId').val() == '' ? '0' : $('#ContactId').val(),
        ContactPerson: $('#ContactPerson').val().trim(),
        ContactNo: $('#ContactNo').val().trim(),
        CountryId: $('#CountryId').val().trim(),
        StateId: $('#StateId').val().trim()
    };

    // Add validation token
    contact.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();

    // Save Contact
    $.ajax({
        url: '/Home/Save',
        type: 'POST',
        data: contact,
        dataType: 'json',
        success: function (data) {
            alert(data.message);
            if (data.status) {
                $('#ContactId').val('');
                $('#ContactPerson').val('');
                $('#ContactNo').val('');
                $('#CountryId').val('');
                $('#StateId').val('');
                LoadContacts();
                $dialog.dialog('close');
            }
        },
        error: function () {
            $('#msg').html('<div class="failed">Error! Please try again.</div>');
        }
    });
}

// Delete Contact
function DeleteContact() {
    $.ajax({
        url: '/Home/Delete',
        type: 'POST',
        dataType: 'json',
        data: {
            'id': $('#ContactId').val(),
            '__RequestVerificationToken': $('input[name=__RequestVerificationToken]').val()
        },
        success: function (data) {
            alert(data.message);
            if (data.status) {
                $dialog.dialog('close');
                LoadContacts();
            }
        },
        error: function () {
            $('#msg').html('<div class="failed">Error! Please try again.</div>');
        }
    });
}