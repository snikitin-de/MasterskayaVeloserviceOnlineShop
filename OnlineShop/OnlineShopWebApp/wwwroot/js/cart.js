// Увеличение количества товара в корзине
function IncreaseCartItem(productId) {
    $.ajax({
        url: `/cart/increaseCartItem?productId=${productId}`
    })
    $(document).ajaxStop(function () {
        window.location.reload();
    });
};

// Уменьшение количества товара в корзине
function DecreaseCartItem(productId) {
    $.ajax({
        url: `/cart/decreaseCartItem?productId=${productId}`
    })
    $(document).ajaxStop(function () {
        window.location.reload();
    });
};

// Очистка корзины
function ClearCart(userId) {
    $.ajax({
        url: `/cart/clear`
    })
    $(document).ajaxStop(function () {
        window.location.reload();
    });
};