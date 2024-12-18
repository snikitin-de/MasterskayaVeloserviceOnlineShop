document.addEventListener('DOMContentLoaded', function () {
    // Открытие формы редактирования аватара пользователя
    document.querySelectorAll('.edit-role').forEach(function (link) {
        link.addEventListener('click', function (e) {
            e.preventDefault();

            var roleId = this.getAttribute('data-role-id');

            fetch('/admin/roles/edit?id=' + roleId)
                .then(response => response.text())
                .then(html => {
                    document.getElementById('editRolePartModalContent').innerHTML = html;
                    var modal = new bootstrap.Modal(document.getElementById('editRolePartModal'));
                    modal.show();
                    $.validator.unobtrusive.parse('#editRoleForm');
                });
        });
    });

    // Отправка формы редактирования аватара пользователя через AJAX
    $('#editRolePartModal').on('submit', '#editRoleForm', function (e) {
        e.preventDefault();

        var form = $(this);

        $.ajax({
            url: form.attr('action'),
            type: form.attr('method'),
            data: form.serialize(),
            success: function (result) {
                $('#editRolePartModal').modal('hide');
                $(document).ajaxStop(function () {
                    window.location.reload();
                });
            }
        });
    });
});