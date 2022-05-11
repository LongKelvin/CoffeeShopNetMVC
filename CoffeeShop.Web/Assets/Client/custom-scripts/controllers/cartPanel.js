var cartPanel = {
    init: function () {
        cartPanel.loadData();
        cartPanel.registerEvent();
    },

    registerEvent: function () {
        //$('#btnDeleteItemCartPanel').off('click').on('click', function (e) {
        //    e.preventDefault();
        //    var productID = parseInt($(this).data('id'));
        //    cartPanel.deleteItem(productID);

        //    alert("Button click")
        //});
    },

    loadData: function () {
        var cartPanelTemplateHtml = $('#cartPanelTemplate').html();

        if (cartPanelTemplateHtml == null) {
            return;
        }

        Mustache.parse(cartPanelTemplateHtml, ['{{', '}}']);
        $.ajax({
            url: '/ShoppingCart/GetAll',
            type: 'GET',
            dataType: 'json',
            success: function (result) {
                if (result.status == true) {
                    var dataAsHtml = '';
                    if (result.count <= 0) {
                        $('#cartBody').html('<p class="mt-5 ml-1">Your Cart is currently empty.</p>')
                        $('#totalPriceCartPanel').text(numeral(0).format('0,0'));
                    }
                    else {
                        var resData = result.data;
                        $.each(resData, function (index, item) {
                            dataAsHtml += Mustache.render(cartPanelTemplateHtml, {
                                itemImage: item.Product.Images,
                                itemName: item.Product.Name,
                                itemUnitPrice: item.Product.Price,
                                itemProductID: item.Product.ID,
                                itemQuantity: item.Quantity,
                                itemUnitPriceF: numeral(item.Product.Price).format('0,0'),
                            });
                        });

                        $('#cartBody').html(dataAsHtml);
                        var totalPriceCartPanel = cartPanel.getTotalPriceOrder();

                        $('#totalPriceCartPanel').text(numeral(totalPriceCartPanel).format('0,0'));
                        cartPanel.registerEvent();
                    }
                }
            }
        })
    },

    getTotalPriceOrder: function () {
        var listInputPrice = $('.itemQty');
        if (listInputPrice == null) {
            return 0;
        }

        var total = 0;

        $.each(listInputPrice, function (index, item) {
            var tempPrice = parseInt($(item).text()) * parseFloat($(this).data('price'))
            total += tempPrice;
        });

        return total;
    },

    deleteItem: function (productID) {
        $.ajax({
            url: '/ShoppingCart/DeleteItem',
            data: {
                productID: productID
            },
            type: 'POST',
            dataType: 'json',
            success: function (result) {
                if (result.status == true) {
                    cartPanel.loadData();
                    cart.loadData();

                    var totalAmount = cartPanel.getTotalPriceOrder();
                    $('#totalPriceCartPanel').text(numeral(totalAmount).format('0,0'));

                    console.log("IN DELETE BLOCK")
                }
            }
        })
    },
}

cartPanel.init();

function deleteItemInCartPanel(id) {
    cartPanel.deleteItem(id);
}