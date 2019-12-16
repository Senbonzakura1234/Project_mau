$(document).ready(function() {
    //if ($("#goto").length) {
    //    $("html, body").delay(1000).animate({ scrollTop: $("#goto").offset().top }, 1000);
    //}

    // ReSharper disable once CoercedEqualsUsing
    $("#advanceCheckValue").val(0);

    $(`.search-option-select`).on(`change`,
        function() {
            const optionSelected = $(`option:selected`, this);
            const valueSelected = this.value;
            console.log(valueSelected);
            console.log(optionSelected);
            // ReSharper disable once CoercedEqualsUsing
            if (valueSelected == 0) {
                $(`.search-form`).attr(`action`, `/Customers`);
                // ReSharper disable once CoercedEqualsUsing
            } else if (valueSelected == 1) {
                $(`.search-form`).attr(`action`, `/Products`);
            }
        });
    var searchForm = document.getElementById("search-form");
    document.getElementById("search-form-submit").addEventListener("click",
        function() {
            searchForm.submit();
        });
    $("form").attr("novalidate", "novalidate");
    const parentRemove = document.querySelector(`#remove-html`);
    parentRemove.removeChild(document.querySelector(`.remove-inner`));
    parentRemove.removeAttribute(`id`);
    $("select.form-selectpicker").attr("data-style", "select-option-btn");
    $("#advanceCheck").click(function() {
        if ($(this).prop("checked") === true) {
            $("#advanceCheckValue").val(1);
        } else if ($(this).prop("checked") === false) {
            $("#advanceCheckValue").val(0);
        }
    });


    $("#clearFilterCustomer").click(function() {
        $("#advanceCheck").prop("checked", false);
        $("#advanceCheckValue").val(0);
        $("input[name = 'advanceFullname']").val(null);
        $("input[name = 'advanceEmail']").val(null);
        $("input[name = 'advanceAge']").val(null);
    });
    //$("#clearFilterProduct").click(function () {
    //    $("input[name = 'advanceName']").val("");
    //    $("select[name = 'advancePriceFrom']").val(0);
    //    $("select[name = 'advancePriceTo']").val(0);
    //    $("select[name = 'advanceBrand']").val(0);
    //    $("select[name = 'advanceCategory']").val(0);
    //    $("#form-submit-btn").click();
    //});


    $("#AdvanceSearchFormCustomer").on("submit",
        function(e) {
            e.preventDefault();
            const advanceFullname = $("input[name = 'advanceFullname']").val();
            const advanceEmail = $("input[name = 'advanceEmail']").val();
            const advanceAge = $("input[name = 'advanceAge']").val();
            const advanceCheckValue = $("#advanceCheckValue").val();
            $.ajax({
                url: "/Customers/AjaxCustomers",
                type: "GET",
                data: {
                    advanceFullname: advanceFullname,
                    advanceEmail: advanceEmail,
                    advanceAge: advanceAge,
                    advanceCheckValue: advanceCheckValue
                },
                success: function(res) {
                    $("#update-customer").html(res);
                },
                error: function(xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
        });

    $("#AdvanceSearchFormProduct").on("submit",
        function(e) {
            e.preventDefault();
            const advanceName = $("input[name = 'advanceName']").val();
            const advanceBrand = $("select[name = 'advanceBrand']").val();
            const advanceCategory = $("select[name = 'advanceCategory']").val();
            const advancePriceFrom = $("select[name = 'advancePriceFrom']").val();
            const advancePriceTo = $("select[name = 'advancePriceTo']").val();
            $.ajax({
                url: "/Products/AjaxProducts",
                type: "GET",
                data: {
                    advanceName: advanceName,
                    advanceBrand: advanceBrand,
                    advanceCategory: advanceCategory,
                    advancePriceFrom: advancePriceFrom,
                    advancePriceTo: advancePriceTo
                },
                success: function(res) {
                    $("#update-product").html(res);
                },
                error: function(xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
            $("html, body").animate({ scrollTop: $("#goto").offset().top }, 1000);

        });


    // ************************************************
    // Shopping Cart API
    // ************************************************

    var shoppingCart = (function() {
        // =============================
        // Private methods and propeties
        // =============================
        var cart = [];

        // Constructor
        function Item(id, name, price, count) {
            this.id = id;
            this.name = name;
            this.price = price;
            this.count = count;
        }

        // Save cart
        function saveCart() {
            sessionStorage.setItem("shoppingCart", JSON.stringify(cart));
        }

        // Load cart
        function loadCart() {
            cart = JSON.parse(sessionStorage.getItem("shoppingCart"));
        }

        if (sessionStorage.getItem("shoppingCart") != null) {
            loadCart();
        }


        // =============================
        // Public methods and propeties
        // =============================
        const obj = {};

        // Add to cart
        obj.addItemToCart = function(id, name, price, count) {
            var item;
            for (item in cart) {
                if (cart.hasOwnProperty(item)) {
                    if (cart[item].id === id) {
                        cart[item].count++;
                        saveCart();
                        return;
                    }
                }
            }
            item = new Item(id, name, price, count);
            console.log(item);
            cart.push(item);
            saveCart();
        };
        // Set count from item
        obj.setCountForItem = function(id, count) {
            for (let i in cart) {
                if (cart.hasOwnProperty(i)) {
                    if (cart[i].id === id) {
                        cart[i].count = count;
                        break;
                    }
                }
            }
        };
        // Remove item from cart
        obj.removeItemFromCart = function(id) {
            for (let item in cart) {
                if (cart.hasOwnProperty(item)) {
                    if (cart[item].id === id) {
                        cart[item].count--;
                        if (cart[item].count === 0) {
                            cart.splice(item, 1);
                        }
                        break;
                    }
                }
            }
            saveCart();
        };

        // Remove all items from cart
        obj.removeItemFromCartAll = function(id) {
            for (let item in cart) {
                if (cart.hasOwnProperty(item)) {
                    if (cart[item].id === id) {
                        cart.splice(item, 1);
                        break;
                    }
                }
            }
            saveCart();
        };

        // Clear cart
        obj.clearCart = function() {
            cart = [];
            saveCart();
        };

        // Count cart 
        obj.totalCount = function() {
            var totalCount = 0;
            for (let item in cart) {
                if (cart.hasOwnProperty(item)) {
                    totalCount += cart[item].count;
                }
            }
            return totalCount;
        };

        // Total cart
        obj.totalCart = function() {
            var totalCart = 0;
            for (let item in cart) {
                if (cart.hasOwnProperty(item)) {
                    totalCart += cart[item].price * cart[item].count;
                }
            }
            return Number(totalCart.toFixed(2));
        };

        // List cart
        obj.listCart = function() {
            const cartCopy = [];
            var i;
            for (i in cart) {
                if (cart.hasOwnProperty(i)) {
                    const item = cart[i];
                    const itemCopy = {};
                    let p;
                    for (p in item) {
                        if (item.hasOwnProperty(p)) {
                            itemCopy[p] = item[p];

                        }
                    }
                    itemCopy.total = Number(item.price * item.count).toFixed(2);
                    cartCopy.push(itemCopy);
                }
            }
            return cartCopy;
        };

        // cart : Array
        // Item : Object/Class
        // addItemToCart : Function
        // removeItemFromCart : Function
        // removeItemFromCartAll : Function
        // clearCart : Function
        // countCart : Function
        // totalCart : Function
        // listCart : Function
        // saveCart : Function
        // loadCart : Function
        return obj;
    })();


    // *****************************************
    // Triggers / Events
    // ***************************************** 
    // Add item
    $(".add-to-cart").click(function(event) {
        event.preventDefault();

        const data = parseInt(this.id);
        $.ajax({
            url: "/Home/GetProductInfo",
            type: "GET",
            data: {
                id: data
            },
            success: function(res) {
                if (res != null) {
                    console.log(res);
                    const id = res.id;
                    const name = res.name;
                    const price = res.price;
                    shoppingCart.addItemToCart(id, name, price, 1);
                    displayCart();
                }
            },
            error: function(xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });
    });


    // Clear items
    $(".clear-cart").click(function() {
        shoppingCart.clearCart();
        displayCart();
    });
    $(".createOrder").click(function () {
        const cartArray = shoppingCart.listCart();
        console.log(cartArray);

        if (cartArray != null && cartArray.count !== 0 && shoppingCart.totalCount() !== 0) {
            shoppingCart.clearCart();
            displayCart();
            $.ajax({
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                type: "POST",
                url: "/ShoppingCart/CreateOrder",
                data: JSON.stringify({
                    'cartArrays': cartArray
                }),
                success: function (res) {
                    alert(res.message);
                    window.location.href = res.url;
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
        }
    });


    function displayCart() {
        const cartArray = shoppingCart.listCart();
        var data = "";

        for (let i in cartArray) {
            if (cartArray.hasOwnProperty(i)) {
                //output += `<tr><td>${cartArray[i].name}</td><td>(${cartArray[i].price
                //    })</td><td><div class="form-group"><div class='input-group'><input type="button" class='minus-item input-group-prepend btn btn-primary' value="-" data-name=${cartArray[i].name
                //    }><input type='number' class='item-count form-control' data-name='${cartArray[i].name}' value='${cartArray[i].count
                //    }'><input  type="button" class='plus-item btn btn-primary input-group-append' value="+" data-name=${cartArray[i].name
                //    }/></div></div></td><td><button class='delete-item btn btn-danger btn-sm' data-name=${cartArray[i].name}>X</button></td> = <td>${cartArray[i].total
                //    }</td></tr>`;

                data += `<tr>
                            <td>
                               ${cartArray[i].id}
                            </td>
                            <td>
                               ${cartArray[i].name} ($ ${cartArray[i].price})
                            </td>
                            <td class="text-center">
                                <div class="form-group mb-0">
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <button class="minus-item btn btn-primary btn-sm input-group-addon" data-id='${
                    cartArray[i].id}'>-</button>
                                        </div>
                                        <input type='number' class='item-count form-control' data-id='${cartArray[i].id
                    }' value='${cartArray[i].count}'>
                                        <div class="input-group-append">
                                            <button class="plus-item btn btn-primary btn-sm input-group-addon" data-id='${
                    cartArray[i].id}'>+</button>
                                        </div>
                                    </div>
                                </div>
                            </td>
                            <td class="text-center">
                                <button class='delete-item btn btn-danger btn-sm' data-id=${cartArray[i].id}>X</button>
                            </td>
                            <td class="text-center">
                                $ ${cartArray[i].total}
                            </td>
                        </tr>`;
            }
        }
        const output = ` <table class="show-cart table">
                            <thead class="text-primary">
                                <tr>
                                    <th>
                                        #
                                    </th>
                                    <th>
                                        Product
                                    </th>
                                    <th class="text-center">
                                        Quantity
                                    </th>
                                    <th class="text-center">
                                        Total
                                    </th>
                                    <th class="text-center">

                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                ${data}
                            </tbody>
                        </table>`;
        $(".show-cart").html(output);
        $(".total-cart").html(shoppingCart.totalCart());
        $(".total-count").html(shoppingCart.totalCount());
    }

    // Delete item button

// ReSharper disable once UnusedParameter
    $(".show-cart").on("click",
        ".delete-item",
        function(event) {
            const id = $(this).data("id");
            shoppingCart.removeItemFromCartAll(id);
            displayCart();
        });


    // -1
// ReSharper disable once UnusedParameter
    $(".show-cart").on("click",
        ".minus-item",
        function(event) {
            const id = $(this).data("id");
            shoppingCart.removeItemFromCart(id);
            displayCart();
        });
    // +1
// ReSharper disable once UnusedParameter
    $(".show-cart").on("click",
        ".plus-item",
        function(event) {
            const id = $(this).data("id");
            shoppingCart.addItemToCart(id);
            displayCart();
        });

    // Item count input
// ReSharper disable once UnusedParameter
    $(".show-cart").on("change",
        ".item-count",
        function(event) {
            const id = $(this).data("id");
            const count = Number($(this).val());
            shoppingCart.setCountForItem(id, count);
            displayCart();
        });

    displayCart();


    //$("img").css("opacity", 0);

});