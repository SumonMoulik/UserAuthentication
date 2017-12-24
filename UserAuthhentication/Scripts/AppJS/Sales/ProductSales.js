var salesItem = new Array();
function RemoveItem(productId) {
    $.ajax({
        url: '/Sales/GetProductsById',//?ProductId=' + productId,
        type: 'POST',
        data: JSON.stringify({
            Sales: {
                ProductId: productId,
                Products: salesItem
            }
        }),
        contentType: 'application/json;charset=utf-8',
        success: function (products) {
            $('#Total').val(parseFloat(0));
            var ptotal = 0;
            salesItem = products;
            $("#SalesDetails tr:not(:first)").remove();
            $.each(salesItem, function (key, value) {
                $("#SalesDetails").append('<tr><td>' + value.ProductName + '</td><td>' + value.Price + '</td><td>' + value.Quantity + '</td><td>' + value.ProdTotal + '</td><td><a href="#" onclick="RemoveItem(' + value.ProductId + ');"><img src=/Images/delete.gif /></a></td><td><a href="#" onclick="EditItem(' + value.ProductId + ',' + value.Price + ',' + value.Quantity + ');"><img src=/Images/edit.gif /></a></td></tr>');
                ptotal = parseFloat(ptotal) + parseFloat(value.ProdTotal)
                $('#Total').val(parseFloat(ptotal));
            });
        }
    });
}

function EditItem(productId, price, quantity) {
    $('#ProductId').val(productId);
    $('#Quantity').val(quantity);
    $('#ProdPrice').val(price);
    RemoveItem(productId);
    $('#Quantity').prop('disabled', false);
}

function SalesItem(ProductId, ProductName, Quantity, Price) {
    this.ProductId = ProductId;
    this.ProductName = ProductName;
    this.Quantity = Quantity;
    this.Price = parseFloat(Price);
    this.ProdTotal = parseFloat(Price * Quantity);
}

(function ($) {
    $(document).ready(function () {
        var userId = sessionStorage.getItem("UserId");
        if (userId == null)
            window.location = '/Home/Login'
        var total = 0;
        var prodName = "";
        $('#Quantity').prop('disabled', true);

        $("#ProductId").change(function () {
            $.ajax({
                url: '/Sales/GetProductById?ProductId=' + $("#ProductId").val(),
                type: 'Get',
                success: function (product) {
                    prodName = product.ProductName;
                    $('#ProdPrice').val(parseFloat(product.SalesPrice));
                    $('#Quantity').prop('disabled', false);
                    $('#Quantity').focus();
                }
            });
        });
        $("#Quantity").keydown(function (e) {
            if (e.which === 13) {
                $('#btnAdd').click();
            }
        });
        $("#Quantity").keypress(function (evt) {
            var charcode = (evt.which);
            if (charcode == 8) // allowing backspace
                return true;
            if (charcode == 0)
                return true;
            if (charcode == 46) { // checking decimal point(.)
                if ($(this).val().split('.').length > 1)
                    return false;
                return true;
            }
            if (charcode < 48 || charcode > 57) // enter 0-9
                return false;
            if ($(this).val().substring($(this).val().indexOf('.')).length > 2)//checking for entering 1 more decimal point(.)
                return false;
        });
        $(document).bind("keydown", function (e) {
           //alert(e.which);
            if (e.which == 46) {
                RemoveItem(salesItem[0].ProductId);
            }
            else if (e.which == 113) {
                $('#btnAdd').click();
            }
        });
        $('#btnAdd').click(function () {
            if (FnValidation()) {
                total = 0;
                $("#SalesDetails tr:not(:first)").remove();
                //Updating existing item in the list
                $.each(salesItem, function (key, value) {
                    if (parseInt($('#ProductId').val()) == value.ProductId) {
                        $.grep(salesItem, function (product) {
                            if (parseInt(product.ProductId) == parseInt($('#ProductId').val())) {
                                var quantity = value.Quantity + parseFloat($('#Quantity').val());
                                product.Quantity = quantity;
                                product.ProdTotal = quantity * parseFloat($('#ProdPrice').val());
                            }
                        });
                        ClearDetails();
                    }
                });
                //End of updating existing item in the list
                if ($('#ProductId').val() != "" && $('#ProductId').val() > 0) {
                    salesItem.push(new SalesItem(parseInt($('#ProductId').val()), prodName, parseFloat($('#Quantity').val()), $('#ProdPrice').val()));
                }
                //$("#SalesDetails").append('<tr><th>Product Id</th><th>Rate</th><th>Quantity</th><th>Prod. Total</th></tr>');
                $.each(salesItem, function (key, value) {
                    $("#SalesDetails").append('<tr><td>' + value.ProductName + '</td><td>' + value.Price + '</td><td>' + value.Quantity + '</td><td>' + value.ProdTotal + '</td><td><a href="#" onclick="RemoveItem(' + value.ProductId + ');"><img src=/Images/delete.gif /></a></td><td><a href="#" onclick="EditItem(' + value.ProductId + ',' + value.Price + ',' + value.Quantity + ');"><img src=/Images/edit.gif /></a></td></tr>');
                    total = parseFloat(total) + parseFloat(value.ProdTotal)

                    $('#Total').val(parseFloat(total));
                });
                ClearDetails();
            }
        });
        function FnValidation() {
            if ($('#ProductId').val() == "" || $('#ProductId').val() < 0) {
                alert("Select product");
                $('#ProductId').focus();
                return false;
            }
            else if ($('#Quantity').val() == "" || parseFloat($('#Quantity').val()) <= parseFloat(0)) {
                alert("Enter product quantity");
                $('#Quantity').focus();
                return false;
            }
            else return true;
        }
        function ClearDetails() {
            $('#ProductId').val("");
            $('#ProdPrice').val("");
            $('#Quantity').val("");
            $('#Quantity').prop('disabled', true);
        }
    });
})(jQuery);