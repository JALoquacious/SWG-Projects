$(document).ready(function () {

    let totalIn      = 0.00,
        selectedItem = 0,
        insertCoins  = new Audio('sounds/insertCoins.mp3'),
        returnCoins  = new Audio('sounds/returnCoins.mp3'),
        registerSale = new Audio('sounds/registerSale.mp3'),
        currencies   = {
            'dollar': 1.00,
            'quarter': 0.25,
            'dime': 0.10,
            'nickel': 0.05
        },
        hintOptions  = {
            track: true,
            disabled: false,
            show: 'slideDown',
            open: function (event, ui) {
                ui.tooltip.hover(
                    function () {
                        $(this).fadeTo('slow', 0.5);
                    }
                );
            }
        };


    function blinkAlert(color1, color2, number, duration) {
        let id = $('#display-item').text();

        for (let i = 1; i <= number; i++) {
            $(`#product-${id}-quantity`)
                .animate({
                    'color': color1
                }, duration)
                .animate({
                    'color': color2
                }, duration);
        }
    }


    function reset(resetTotal = true, resetSelection = true, disableControls = true,
        eraseMessages = true) {

        if (resetTotal) {
            totalIn = 0;
            $('#total-in').text('$' + totalIn.toFixed(2));
        }

        if (resetSelection) {
            selectedItem = 0;
            $('.product').removeClass('selected glow');
            $('.product-id').removeClass('lit glow');
        }

        if (disableControls) {
            $('#make-purchase')
                .addClass('disabled')
                .tooltip(hintOptions)
                .off('click');
            $('#return-change')
                .addClass('disabled')
                .tooltip(hintOptions)
                .off('click');
        }

        if (eraseMessages) {
            $('#display-messages').text('');
            $('#display-change').text('');
            $('#display-item').text('');
        }
    }


    function registerProducts(list) {
        for (let p = 0; p < list.length; p++) {

            $(document).on('mouseenter', `#product-${p+1}`, function () {

                $('.product').removeClass('product-hover');
                $(this).addClass('product-hover');
            });

            $(document).on('click', `#product-${p+1}`, function () {

                $('.product-id').removeClass('lit glow');
                $('.product').removeClass('selected glow');
                $('#make-purchase')
                    .removeClass('disabled')
                    .tooltip({ disabled: true });
                $('#return-change')
                    .removeClass('disabled')
                    .tooltip({ disabled: true });

                $(this).addClass('selected glow');
                $(`#product-${p+1}-id`).addClass('lit glow');

                selectedItem = parseInt($(`#product-${p+1}-id`).text());
                $('#display-item').text(selectedItem);
                $('#display-messages').text('');
                
                registerMainControls();
            });
        }
    }


    function registerCurrencyControls() {
        for (let c in currencies) {
            $('#currency-controls').on('click', 'button', function () {
                if (this.id === `add-${c}`) {
                    insertCoins.play();
                    totalIn += parseFloat(currencies[c]);
                }
                $('#total-in').text('$' + totalIn.toFixed(2));
            });
        }
    }


    function registerMainControls() {
        // prevent over-registering
        $('#make-purchase').off('click');
        $('#return-change').off('click');
        
        $('#return-change').on('click', function () {
            returnCoins.play();
            reset();
        });

        $('#make-purchase').on('click', attemptPurchase);
    }


    function displayChange(data) {

        function pluralize(amount, str) {
            let suffix = 's';

            if (str.endsWith('y')) {
                str = str.slice(0, str.length - 1);
                suffix = 'ies';
            };
            return (amount === 1) ? str : str + suffix;
        }

        let result = '';

        if (!data.pennies && !data.nickels && !data.dimes && !data.quarters) {
            return result;
        } else {
            let q = data.quarters,
                d = data.dimes,
                n = data.nickels,
                p = data.pennies;
            
            result += (q) ? `${q} ${pluralize(q, 'Quarter')}` : '';
            
            result += ((d) ? ', ' : '') +
                      ((d) ? `${d} ${pluralize(d, 'Dime')}` : '');
            
            result += ((n) ? ', ' : '') +
                      ((n) ? `${n} ${pluralize(n, 'Nickel')}` : '');
            
            result += ((p) ? ', ' : '') +
                      ((p) ? `${p} ${pluralize(p, 'Penny')}` : '');
        }
        return result;
    }


    function queryProducts(initialize = false) {
        $.ajax({
            type: 'GET',
            url: 'http://localhost:8080/items',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            dataType: 'json',
            success: function (data, status) {
//let product = ''
                $.each(data, function (index, item) {

// product += `<div class="col-xs-12 col-md-4 product"`;
// product += `id="product-${index+1}">\u00A0 ${index+1} \u00A0</div>`;
// product += `<span class="product-id" id="product-${index+1}-id"></span>`;
// product += `<h4 id="product-${item.name}-title"></h4>`;
// product += `<p id="product-1-price" class="strong">$${item.price.toFixed(2)}</p>`;
// product += `<p id="product-1-quantity">Quantity Left: ${item.quantity}</p>`;

                    $(`#product-${index+1}-id`)
                        .text(`\u00A0 ${index+1} \u00A0`);

                    $(`#product-${index+1}-title`)
                        .text(item.name);

                    $(`#product-${index+1}-price`)
                        .text(`$${item.price.toFixed(2)}`);

                    $(`#product-${index+1}-quantity`)
                        .text(`Quantity Left: ${item.quantity}`);
                });

//$(`#product-col`).append(product);

                if (initialize) {
                    registerCurrencyControls();
                    registerProducts(data);
                }
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText, error);
            }
        });
    }


    function attemptPurchase() {
        $.ajax({
            type: 'GET',
            url: 'http://localhost:8080/money/' +
                `${totalIn.toFixed(2)}/item/${selectedItem}`,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            dataType: 'json',
            success: function (data, status) {
                $('#display-messages')
                    .text(data.message ? data.message : 'Thank you!');

                $('#display-change').text(displayChange(data));

                registerSale.play();
                reset(resetTotal = true, resetSelection = false,
                    disableControls = false, eraseMessages = false);
                queryProducts();
                blinkAlert('white', 'black', 5, 300);
            },
            error: function (xhr, status, error) {
                $('#display-messages')
                    .text($.parseJSON(xhr.responseText).message);
            }
        });
    }

    reset();
    queryProducts(initialize = true);
});
