
$(document).ready(function () {

    var arrayCart = [];
    var totalitem = 0;

    console.log(arrayCart.length);

    $(".mzs-atc").on('click', function (e) {

        //var arrayCart = [];

        console.log($(this).parents().parents().parents().attr('id'));
        var parentDivId = (e.target).parentNode.parentNode.parentNode.id;
        console.log(parentDivId);

        var selectedsize = $("#" + parentDivId).find('option:selected').text();
        console.log(selectedsize);
        if (selectedsize == "Size") {
            alert("Select a Size");
        } else {
            var pid = $("#" + parentDivId + " > input[name='pid']").val();
            var pname = $("#" + parentDivId + " > input[name='pname']").val();
            var category = $("#" + parentDivId + " > input[name='category']").val();
            var pfor = $("#" + parentDivId + " > input[name='pfor']").val();
            var size = $("#" + parentDivId + " > input[name='size']").val();
            var available = $("#" + parentDivId + " > input[name='available']").val();
            var price = $("#" + parentDivId + " > input[name='price']").val();
            var currency = $("#" + parentDivId + " > input[name='currency']").val();
            var cost = $("#" + parentDivId + " > input[name='cost']").val();
            var offer = $("#" + parentDivId + " > input[name='offer']").val();
            var quantity = $("#" + parentDivId).find("input[name='quantity']").val();
            var quantityFor = $("#" + parentDivId).find('option:selected').val();
            //var avmax = $("#" + parentDivId).find("input[name='quantity']").attr("max");

            //console.log(" Avmax : " + avmax);
            console.log(quantity);
            console.log("QFor : " + quantityFor);

            var sub = Number(available);
            sub -= Number(quantity);
            mini = 1;

            if (sub < 1) {
                sub = 0;
                mini = 0;
            }

            $("#" + parentDivId).find("input[name='quantity']").attr({
                "max": sub, // substitute your own
                "min": mini // values (or variables) here
            });

            $("#" + parentDivId + " > input[name='available']").val(sub);

            totalitem += Number(quantity);
            $('#cartitem').html(totalitem);
            //console.log(cost);
            //console.log(available);

            var cartObj = {};
            cartObj.pid = pid;
            cartObj.pname = pname;
            cartObj.pfor = pfor;
            cartObj.category = category;
            //cartObj.size = quantityFor;
            cartObj.available = available;
            cartObj.quantity = Number(quantity);
            cartObj.quantityFor = quantityFor;
            cartObj.price = price;
            cartObj.cost = cost;
            cartObj.currency = currency;
            cartObj.offer = offer;

            if (sub < Number(quantity)) {
                $("#" + parentDivId).find("input[name='quantity']").val(sub);
            }

            console.log("AVAILABLE:" + sub);

            var numb = Number(cartObj.quantity);
            var prc = Number(cartObj.price);
            var totalprc = numb * prc;

            cartObj.total = totalprc;

            console.log(cartObj);
            qntty = Number(quantity);

            if (arrayCart.length == 0) {
                arrayCart.push(cartObj);
                //$.cookie("products", JSON.stringify(arrayCart));

            } else {

                var found = false;

                for (var i = 0; i < arrayCart.length; i++) {
                    if (cartObj.pid == arrayCart[i].pid) {
                        if (cartObj.quantityFor == arrayCart[i].quantityFor) {
                            arrayCart[i].quantity += Number(cartObj.quantity);
                            numb = Number(arrayCart[i].quantity);
                            prc = Number(arrayCart[i].price);
                            totalprc = numb * prc;
                            arrayCart[i].total = totalprc;
                            //$.cookie("products", JSON.stringify(arrayCart));
                            found = true;
                            break;
                        }
                    }

                }

                if (found == false) {
                    arrayCart.push(cartObj);
                }

            }
            
            console.log("arrayCart:");
            console.log(arrayCart);

            $.ajax({
                url: "/User/AddToCart",
                method: "get",
                data: {

                    cartitems: arrayCart
                },
                success: function (res) {

                    console.log("res cart session:");
                    console.log(res);
                    //window.location = "http://google.com";
                    //window.location = "/cartlist";	
                }

            });
        }
    });
});