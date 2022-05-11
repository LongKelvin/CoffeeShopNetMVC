var cart = {
    init: function () {
        cart.loadData();
        cart.registerEvent();
    },

    registerEvent: function () {
        var shippingFee = 20000;
        
        $('#btnAddItem').off('click').on('click', function (e) {
            e.preventDefault();
            var productID = parseInt($(this).data('id'));
            var quantity = $('#itemDetailQuantityInput').val();
            cart.addItem(productID, quantity);
        });

        $('#btnDeleteItem').off('click').on('click', function (e) {
            e.preventDefault();
            var productID = parseInt($(this).data('id'));
            cart.deleteItem(productID);
        });

        $('.inputItemQuantity').on('input', function (e) {
            var quantity = parseInt($(this).val());
            var productID = parseInt($(this).data('id'));
            var itemUnitPrice = parseFloat($(this).data('price'));

            if (isNaN(quantity) == false) {
                if (quantity == 0) {
                    quantity = 1;
                    $(this).val(quantity)
                }
                var totalPrice = quantity * itemUnitPrice;
                $('#itemTotalPrice_' + productID).text(numeral(totalPrice).format('0,0'));
            }
            else {
                $(this).val(1);
                var totalPrice = 1 * itemUnitPrice;
                $('#itemTotalPrice_' + productID).text(numeral(totalPrice).format('0,0'));
            }

            var totalAmount = cart.getTotalPriceOrder() + shippingFee;

            $('#orderTotalPrice').text(numeral(cart.getTotalPriceOrder()).format('0,0'));
            $('#totalAmount').text(numeral(totalAmount).format('0,0'));
   
        });
    },

    getTotalPriceOrder: function () {
        var listInputPrice = $('.inputItemQuantity');
        var total = 0;

        $.each(listInputPrice, function (index, item) {
            var tempPrice = parseInt($(item).val()) * parseFloat($(this).data('price'))
            total += tempPrice;
        });

        return total;
    },

    loadData: function () {
        var shippingFee = 20000;
        var cartTemplateHtml = $('#cartTemplate').html();
        if (cartTemplateHtml == null || cartTemplateHtml == 'undefined') {
            cart.getTotalQuantity();
            return;
        }
            
        Mustache.parse(cartTemplateHtml, ['{{', '}}']);

        $.ajax({
            url: '/ShoppingCart/GetAll',
            type: 'GET',
            dataType: 'json',
            success: function (result) {
                if (result.status == true) {
                    var dataAsHtml = '';
                    console.log('load data: total count: ', result.count);
                    if (result.count <= 0) {
                        $('#cartDetailBody').html('<p class="mt-5 ml-1">Your cart is currently empty.</p>')
                        $('#totalQuantity').text(0);
                    }
                    else {
                        var resData = result.data;
                        $.each(resData, function (index, item) {
                            var totalPrice = item.Quantity * item.Product.Price;

                            dataAsHtml += Mustache.render(cartTemplateHtml, {
                                itemImage: item.Product.Images,
                                itemName: item.Product.Name,
                                itemUnitPrice: item.Product.Price,
                                itemProductID: item.Product.ID,
                                itemQuantity: item.Quantity,
                                itemTotalPrice: totalPrice,
                                itemTotalPriceF: numeral(totalPrice).format('0,0'),
                                itemUnitPriceF: numeral(totalPrice).format('0,0'),
                            });
                        });
                    
                        $('#cartDetailBody').html(dataAsHtml);
                        $('#shippingFee').text(numeral(shippingFee).format('0,0'));

                        var totalAmount = cart.getTotalPriceOrder() + shippingFee;

                        $('#orderTotalPrice').text(numeral(cart.getTotalPriceOrder()).format('0,0'));
                        $('#totalAmount').text(numeral(totalAmount).format('0,0'));

                        cart.getTotalQuantity();
                        cart.registerEvent();
                    }
                }
            }
        })
    },

    addItem: function (productID, quantity) {
        $.ajax({
            url: '/ShoppingCart/AddItem',
            data: {
                productID: productID,
                quantity: quantity
            },
            type: 'POST',
            dataType: 'json',
            success: function (result) {
                if (result.status == true) {
                    cart.getTotalQuantity();
                }
            }
        })
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
                    cart.loadData();
                    cart.getTotalQuantity();
                }
            }
        })
    },
    getTotalQuantity: function () {
        $.ajax({
            url: "/ShoppingCart/GetCartTotalQuantity",
            dataType: 'json',
            type: 'GET',
            success: function (data) {
                $('#totalQuantity').text(data.data);
            }
        });
    }
}

cart.init();

function addItemToCart(productID) {
    var productId = parseInt(productID);
    cart.addItem(productId,1); 
}
