document.addEventListener('DOMContentLoaded', function () {
    const container = document.getElementById('authorizationPartModal'); 

    container.addEventListener('click', function (event) {
        let target = event.target;

        if (target.classList.contains('register')) {
            event.preventDefault();

            // Закрытие формы авторизации
            $('#authorizationPartModal').modal('hide');

            // Открытие формы регистрации
            fetch('/account/registration/register')
                .then(response => response.text())
                .then(html => {
                    document.getElementById('registrationPartModalContent').innerHTML = html;
                    var modal = new bootstrap.Modal(document.getElementById('registrationPartModal'));
                    modal.show();
                    document.getElementById('returnUrl').value = window.location.pathname + window.location.search;
                    $.validator.unobtrusive.parse('#registrationForm');
                })

            // Отправка формы регистрации через AJAX
            $('#registrationPartModal').on('submit', '#registrationForm', function (e) {
                e.preventDefault();

                var form = $(this);

                $.ajax({
                    url: form.attr('action'),
                    type: form.attr('method'),
                    data: form.serialize(),
                    success: function (result) {
                        if ($(result).find('.text-danger').length > 0) {
                            $('#registrationPartModalContent').html(result);
                            $.validator.unobtrusive.parse('#registrationForm');
                        } else {
                            $('#registrationPartModal').modal('hide');
                            window.location.href = document.getElementById('returnUrl').value;
                        }
                    }
                });
            });
        }
    });
});
