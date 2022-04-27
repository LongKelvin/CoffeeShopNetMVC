var common = {
    init: function () {
        common.registerEvents();
    },
    registerEvents: function () {
        $("#searchInput").autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/Product/GetListProductByName",
                    dataType: "json",
                    data: {
                        name: request.term
                    },
                    success: function (res) {
                        response(res.data);
                       
                    }
                });
            },
            focus: function (event, ui) {
                $("#searchInput").val(ui.item.Name);
                return false;
            },
            select: function (event, ui) {
                $("#searchInput").val(ui.item.Name);
                return false;
            }
        }).data('uiAutocomplete')._renderItem = function (ul, item) {
           
            return $("<li>")
                .append("<a>" + item.Name + "</a>")
                .appendTo(ul);
            };

        //$('.btnAddToCart').off('click').on('click', function (e) {
        //    e.preventDefault();
        //    var productId = parseInt($(this).data('id'));
        //    $.ajax({
        //        url: '/ShoppingCart/Add',
        //        data: {
        //            productId: productId
        //        },
        //        type: 'POST',
        //        dataType: 'json',
        //        success: function (response) {
        //            if (response.status) {
        //                alert('Thêm sản phẩm thành công.');
        //            }
        //            else {
        //                alert(response.message);
        //            }
        //        }
        //    });
        //});

        //$('#btnLogout').off('click').on('click', function (e) {
        //    e.preventDefault();
        //    $('#frmLogout').submit();
        //});
    }
}
common.init();