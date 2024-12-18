// Текстовый поиск
document.getElementById('searchLink').addEventListener('click', function (e) {
    e.preventDefault();
    const searchValue = document.getElementById('searchInput').value;
    const controller = document.getElementById('controller').getAttribute('name');;
    const searchUrl = `/${controller.toLowerCase()}/search?text=${encodeURIComponent(searchValue)}`;

    window.location.href = searchUrl;
});