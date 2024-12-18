document.addEventListener('DOMContentLoaded', function () {
    // Отправка формы редактирования аватара пользователя через AJAX
    $('#editAvatarModal').on('submit', '#editAvatarForm', function (e) {
        e.preventDefault();

        var form = $(this);

        $.ajax({
            url: form.attr('action'),
            type: form.attr('method'),
            data: form.serialize(),
            success: function (result) {
                $('#editAvatarModal').modal('hide');
                $(document).ajaxStop(function () {
                    window.location.reload();
                });
            }
        });
    });
});