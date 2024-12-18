document.addEventListener('DOMContentLoaded', function () {
    // Открытие формы авторизации
    document.querySelector('.authorize').addEventListener('click', function (e) {
        e.preventDefault();

        fetch('/account/authorization/login?isModal=true')
            .then(response => response.text())
            .then(html => {
                document.getElementById('authorizationPartModalContent').innerHTML = html;
                var modal = new bootstrap.Modal(document.getElementById('authorizationPartModal'));
                modal.show();
                document.getElementById('authReturnUrl').value = window.location.pathname + window.location.search;
                $.validator.unobtrusive.parse('#authorizationForm');
            })
    });

    // Отправка формы авторизации через AJAX
    $('#authorizationPartModal').on('submit', '#authorizationForm', function (e) {
        e.preventDefault();

        var form = $(this);

        $.ajax({
            url: form.attr('action'),
            type: form.attr('method'),
            data: form.serialize(),
            success: function (result) {
                if ($(result).find('.text-danger').length > 0) {
                    $('#authorizationPartModalContent').html(result);
                    $.validator.unobtrusive.parse('#authorizationForm');
                } else {
                    $('#authorizationPartModal').modal('hide');
                    window.location.href = document.getElementById('authReturnUrl').value;
                }
            }
        });
    });
});
