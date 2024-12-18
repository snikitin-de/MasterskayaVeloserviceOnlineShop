async function loadPartialView(target, login) {
    const response = await fetch(`/account/${target}?login=${login}`);

    if (response.ok) {
        const html = await response.text();
        document.getElementById('content-container').innerHTML = html;

        initializeModal();
    }
}

function initializeModal() {
    document.querySelectorAll('.edit-avatar').forEach(function (link) {
        link.addEventListener('click', function (e) {
            e.preventDefault();
            var userLogin = this.getAttribute('data-user-login');

            fetch('/account/editAvatar?login=' + userLogin)
                .then(response => response.text())
                .then(html => {
                    document.getElementById('editAvatarModalContent').innerHTML = html;
                    var modal = new bootstrap.Modal(document.getElementById('editAvatarPartModal'));
                    modal.show();
                });
        });
    });
}

document.querySelectorAll('.account-menu .nav-link').forEach(tab => {
    tab.addEventListener('click', function(event) {
        event.preventDefault();

        document.querySelectorAll('.account-menu .nav-link').forEach(link => {
            link.classList.remove('active');
        });

        this.classList.add('active');

        const target = this.getAttribute('data-target');
        const login = this.getAttribute('data-user-login');

        loadPartialView(target, login);
    });
});

document.querySelector('.account-menu .nav-link.active').click();