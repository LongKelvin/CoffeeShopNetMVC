var payment = {
    init: function () {
        payment.registerEvent();
    },

    registerEvent: function () {
        $('#btnAutoFillUserInfo').change(function () {
            if (this.checked) {
                payment.getAuthenticatedUser();
            }
            else {
                if ($('#txtFirstName').val() != null) {
                    payment.removeUserAutoFillInformation();
                }
            }
        });
    },

    getAuthenticatedUser: function () {
        $.ajax({
            url: 'Account/GetAuthenticatedUser',
            dataType: 'json',
            type: 'POST',
            success: function (response) {
                var userData = response.data;

                $('#txtFirstName').prop("disabled", true);
                $('#txtFirstName').val(userData.FullName);
                $('#txtTelephone').val(userData.PhoneNumber);
                $('#txtEmail').val(userData.Email);
                $('#txtAddress').val(userData.Address);
                $('#txtZipCode').val('70000');
            }
        })
    },

    removeUserAutoFillInformation() {
        $('#txtFirstName').prop("disabled", false);
        $('#txtFirstName').val('');
        $('#txtTelephone').val('');
        $('#txtEmail').val('');
        $('#txtAddress').val('');
        $('#txtZipCode').val('');
    }
}

payment.init();