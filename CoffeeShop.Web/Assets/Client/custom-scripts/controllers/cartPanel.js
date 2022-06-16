var cartPanel = {
    init: function () {
        cartPanel.loadData();
        //cartPanel.registerEvent();
    },

    registerEvent: function () {
    },

    loadData: async function () {
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
                        return;
                    }

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
        })
    },

    appendItem: function (data) {
        var cartPanelTemplateHtml = $('#cartPanelTemplate').html();

        if (cartPanelTemplateHtml == null) {
            return;
        }

        Mustache.parse(cartPanelTemplateHtml, ['{{', '}}']);

        var dataAsHtml = '';
        dataAsHtml += Mustache.render(cartPanelTemplateHtml, {
            itemImage: data.Product.Images,
            itemName: data.Product.Name,
            itemUnitPrice: data.Product.Price,
            itemProductID: data.Product.ID,
            itemQuantity: data.Quantity,
            itemUnitPriceF: numeral(data.Product.Price).format('0,0'),
        });

        if ($('.cart-order li').length === 0) {
            $('#cartBody').html(dataAsHtml);
        }
        else {
            //check if dupplicate item
            if ($(".cart-order #item_" + data.ProductID).length) {
                $('#item_' + data.ProductID).remove();
            }

            $('#cartBody').append(dataAsHtml);
        }

        var totalPriceCartPanel = cartPanel.getTotalPriceOrder();
        $('#totalPriceCartPanel').text(numeral(totalPriceCartPanel).format('0,0'));
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

    getTotalQuantity: function () {
        var listInputQty = $('.itemQty');
        var total = 0;

        $.each(listInputQty, function (index, item) {
            var qty = parseInt($(item).text());

            total += qty;
        });

        $('#totalQuantity').text(total);
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
                    cartPanel.getTotalQuantity();
                    var totalAmount = cartPanel.getTotalPriceOrder();
                    $('#totalPriceCartPanel').text(numeral(totalAmount).format('0,0'));
                }
            }
        })
    },
}

cartPanel.init();

function deleteItemInCartPanel(id) {
    $('.remove-item').click(function () {
        $(this).closest('li').remove();
        checkCartList();
    })

    cartPanel.deleteItem(id);
}

function checkCartList() {
    if ($('.cart-order li').length === 0) {
        $('.cart-order li').html('Cart is empty')
    }
}