document.addEventListener('DOMContentLoaded', function () {
    // Открытие формы добавления пользователя
    document.querySelector('.add-user').addEventListener('click', function (e) {
        e.preventDefault();

        fetch('/admin/users/add')
            .then(response => response.text())
            .then(html => {
                document.getElementById('addUserPartModalContent').innerHTML = html;
                var modal = new bootstrap.Modal(document.getElementById('addUserPartModal'));
                modal.show();
                $.validator.unobtrusive.parse('#addUserForm');
            })
    });

    // Отправка формы добавления пользователя через AJAX
    $('#addUserPartModal').on('submit', '#addUserForm', function (e) {
        e.preventDefault();

        var form = $(this);

        $.ajax({
            url: form.attr('action'),
            type: form.attr('method'),
            data: form.serialize(),
            success: function (result) {
                if ($(result).find('.text-danger').length > 0) {
                    $('#addUserPartModalContent').html(result);
                    $.validator.unobtrusive.parse('#addUserForm');
                } else {
                    $('#addUserPartModal').modal('hide');
                    $(document).ajaxStop(function () {
                        window.location.reload();
                    });
                }
            }
        });
    });
});