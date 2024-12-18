// Открытие формы товара
document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.show-bike-part').forEach(function (link) {
        link.addEventListener('click', function (e) {
            e.preventDefault();
            var bikePartId = this.getAttribute('data-bike-part-id');
            fetch('/admin/products/showBikePart?id=' + bikePartId)
                .then(response => response.text())
                .then(html => {
                    document.getElementById('showBikePartModalContent').innerHTML = html;
                    var modal = new bootstrap.Modal(document.getElementById('showBikePartModal'));
                    modal.show();
                });
        });
    });
});