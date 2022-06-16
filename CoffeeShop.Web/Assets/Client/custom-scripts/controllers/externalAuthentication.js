var externalAuthentication =
{
    init: function () {
       
    },

    registerEvent: function () {
    },

    login: function (returnUrl) {
        $.ajax({
            url: 'Account/ExternalLogin',
            dataType: 'json',
            type: 'POST',
            data: returnUrl,
            success: function (response) {
               
            }
        })
    }
}