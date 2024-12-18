// Открытие формы услуги
document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.show-service').forEach(function (link) {
        link.addEventListener('click', function (e) {
            e.preventDefault();
            var serviceId = this.getAttribute('data-service-id');
            fetch('/admin/products/showService?id=' + serviceId)
                .then(response => response.text())
                .then(html => {
                    document.getElementById('showServiceModalContent').innerHTML = html;
                    var modal = new bootstrap.Modal(document.getElementById('showServiceModal'));
                    modal.show();
                });
        });
    });
});