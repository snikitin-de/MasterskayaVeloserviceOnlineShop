document.addEventListener('DOMContentLoaded', function () {
    var orderStatusSelect = document.getElementById('orderStatus');

    orderStatusSelect.addEventListener('change', function (e) {
        e.preventDefault();

        var orderId = orderStatusSelect.getAttribute('data-order-id');
        var status = e.target.value;

        fetch(`/admin/orders/changeStatus?id=${orderId}&status=${status}`);
    });
});