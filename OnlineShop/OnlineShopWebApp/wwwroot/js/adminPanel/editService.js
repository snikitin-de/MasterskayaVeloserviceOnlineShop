document.addEventListener('DOMContentLoaded', function () {
    // Открытие формы редактирования услуги
    document.querySelectorAll('.edit-service').forEach(function (link) {
        link.addEventListener('click', function (e) {
            e.preventDefault();
            var serviceId = this.getAttribute('data-service-id');
            fetch('/admin/products/editService?id=' + serviceId)
                .then(response => response.text())
                .then(html => {
                    document.getElementById('editServicePartModalContent').innerHTML = html;
                    var modal = new bootstrap.Modal(document.getElementById('editServicePartModal'));
                    modal.show();
                    $.validator.unobtrusive.parse('#editServiceForm');
                });
        });
    });

    // Отправка формы редактирования услуги через AJAX
    $('#editServicePartModal').on('submit', '#editServiceForm', function (e) {
        e.preventDefault();

        var form = e.target;
        var formData = new FormData(form); 

        fetch(form.action, {
            method: form.method,
            body: formData,
        })
            .then(result => {
                $('#editServicePartModal').modal('hide');
                window.location.reload();
            })
    });
});