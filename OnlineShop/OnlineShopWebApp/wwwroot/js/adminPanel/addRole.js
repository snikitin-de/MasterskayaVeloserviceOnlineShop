document.addEventListener('DOMContentLoaded', function () {
    // Открытие формы добавления роли
    document.querySelector('.add-role').addEventListener('click', function (e) {
        e.preventDefault();

        fetch('/admin/roles/add')
            .then(response => response.text())
            .then(html =>
            {
                document.getElementById('addRolePartModalContent').innerHTML = html;
                var modal = new bootstrap.Modal(document.getElementById('addRolePartModal'));
                modal.show();
                $.validator.unobtrusive.parse('#addRoleForm');
            })
    });

    // Отправка формы добавления роли через AJAX
    $('#addRolePartModal').on('submit', '#addRoleForm', function (e) {
        e.preventDefault();

        var form = $(this);

        $.ajax({
            url: form.attr('action'),
            type: form.attr('method'),
            data: form.serialize(),
            success: function (result) {
                if ($(result).find('.text-danger').length > 0) {
                    $('#addRolePartModalContent').html(result);
                    $.validator.unobtrusive.parse('#addRoleForm');
                } else {
                    $('#addRolePartModal').modal('hide');
                    $(document).ajaxStop(function () {
                        window.location.reload();
                    });
                }
            }
        });
    });
});