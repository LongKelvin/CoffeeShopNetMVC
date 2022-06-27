/// <reference path="../paymentcode.js" />
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
                    number: true
                }

            },
            messages: {
                LastName: "Customer Name is required",
                StreetAddress: "Customer Address is required",
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
                $('#txtAddressStreet').val(userData.Address);
            }
        })
    },

    removeUserAutoFillInformation() {
        $('#txtFullName').val('');
        $('#txtTelephone').val('');
        $('#txtEmail').val('');
        $('#txtAddressStreet').val('');
    },

    createOrder: function () {
        var isValid = $('#frmPayment').valid();
        if (!isValid)
            return;

        var order = {
            CustomerName: $('#txtFullName').val(),
            CustomerAddress: $('#txtAddressStreet').val(),
            CustomerEmail: $('#txtEmail').val(),
            CustomerMobile: $('#txtTelephone').val(),
            PaymentMethodCode: $('input[name="paymentMethodRadioBtn"]:checked').val(),
            CustomerMessage: $('#txtNote').val(),
            PaymentStatus: false,
            Status: true,
            TotalAmount: $('#rawTotalAmount').val(),
            TotalItemPrice: $('#rawTotalPrice').val(),
            ShippingFee: $('#rawShippingFee').val()
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
                    //console.log(response);
                    //console.log("create order OK")
                    //$('#paymentTitle').html(response.successMsg);
                    var paymentCodeResponse = response.paymentCode;

                    switch (paymentCodeResponse) {
                        case paymentCode.ShipCod: {
                            $('#paymentTitle').html(response.successMsg);
                        }
                            break;

                        case paymentCode.MoMo: {
                            var returnUrl = response.payUrl;
                            window.location.href = returnUrl;
                        }
                            break;
                        default: {
                            $('#paymentTitle').text("Something went wrong, Please try again later");
                        }
                    }

                }
                else {
                    //console.log("create order FAILED")
                    $('#paymentTitle').text("Something went wrong, Please try again later");
                }
            }
        })
    },
}

payment.init();