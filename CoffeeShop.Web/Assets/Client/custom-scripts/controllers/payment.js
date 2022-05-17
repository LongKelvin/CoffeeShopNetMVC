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
    },

    createOrder: function () {

        if (payment.isPaymentMethodHasSelected == false) {
            alert("Please select payment method to continue")
            return;
        }

        var order = {
            CustomerName: $('#txtFirstName').val(),
            CustomerAddress: $('#txtAddress').val(),
            CustomerEmail: $('#txtEmail').val(),
            CustomerMobile: $('#txtPhone').val(),
            PaymentMethodCode: $('input[name="paymentMethodRadioBtn"]:checked').val(),
            PaymentStatus: '0',
            Status: false
        }

        $.ajax({
            url: 'Payment/CreateOrder',
            dataType: 'json',
            type: 'POST',
            data: {
                orderVM: JSON.stringify(order)
            },
            success: function (response) {
                if (response.status == true) {
                    console.log("create order OK")
                    $('#paymentTitle').html(response.successMsg);
                }
                else {
                    console.log("create order FAILED")
                    $('#paymentTitle').text("Something went wrong, Please try agin later");
                }
            }
        })
    },

    isPaymentMethodHasSelected: function () {
        console.log($('input[name="paymentMethodRadioBtn"]').is(':checked'))
        if ($('input[name="paymentMethodRadioBtn"]').is(':checked')) {
            alert("CHECKED")
            return true;
        }
            
        return false;
    }
}

payment.init();