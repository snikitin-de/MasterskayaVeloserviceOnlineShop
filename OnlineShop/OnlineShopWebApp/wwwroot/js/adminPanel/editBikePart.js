document.addEventListener('DOMContentLoaded', function () {
    // Открытие формы редактирования товара
    document.querySelectorAll('.edit-bike-part').forEach(function (link) {
        link.addEventListener('click', function (e) {
            e.preventDefault();
            var bikePartId = this.getAttribute('data-bike-part-id');
            fetch('/admin/products/editBikePart?id=' + bikePartId)
                .then(response => response.text())
                .then(html => {
                    document.getElementById('editBikePartModalContent').innerHTML = html;
                    var modal = new bootstrap.Modal(document.getElementById('editBikePartModal'));
                    modal.show();
                    $.validator.unobtrusive.parse('#editBikePartForm');
                });
        });
    });

    // Отправка формы редактирования услуги через AJAX
    $('#editBikePartModal').on('submit', '#editBikePartForm', function (e) {
        e.preventDefault();

        var form = e.target;
        var formData = new FormData(form);

        fetch(form.action, {
            method: form.method,
            body: formData,
        })
            .then(result => {
                $('#editBikePartModal').modal('hide');
                window.location.reload();
            })
    });
});