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
                if ($('#txtFullName').val() != null) {
                    payment.removeUserAutoFillInformation();
                }
            }
        });

        $('#frmPayment').validate({
            rules: {
                FullName: "required",
                StreetAddress: "required",
                Email: {
                    required: true,
                    email: true
                },
                Telephone: {
                    required: true,
                    number:true
                }

            },
            messages: {
                LastName: "Customer Name is required",
                StreetAddress: "Customer Name is required",
                Email: {
                    required: "Email is required",
                    email: "Email is not valid, please try again"
                },
                Telephone: {
                    required: "Phone number is required",
                    number: "Phone number is not valid"
                }

            }
        })
    },

    getAuthenticatedUser: function () {
        $.ajax({
            url: 'Account/GetAuthenticatedUser',
            dataType: 'json',
            type: 'POST',
            success: function (response) {
                var userData = response.data;
                $('#txtFullName').val(userData.FullName);
                $('#txtTelephone').val(userData.PhoneNumber);
                $('#txtEmail').val(userData.Email);
                $('#txtAddress').val(userData.Address);
            }
        })
    },

    removeUserAutoFillInformation() {
        $('#txtFullName').val('');
        $('#txtTelephone').val('');
        $('#txtEmail').val('');
        $('#txtAddress').val('');
    },

    createOrder: function () {
        var isValid = $('#frmPayment').valid();
        if (!isValid)
            return;

        var order = {
            CustomerName: $('#txtFullName').val(),
            CustomerAddress: $('#txtAddress').val(),
            CustomerEmail: $('#txtEmail').val(),
            CustomerMobile: $('#txtTelephone').val(),
            PaymentMethodCode: $('input[name="paymentMethodRadioBtn"]:checked').val(),
            CustomerMessage: $('#txtNote').val(),
            PaymentStatus: false,
            Status: true
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
}

payment.init();