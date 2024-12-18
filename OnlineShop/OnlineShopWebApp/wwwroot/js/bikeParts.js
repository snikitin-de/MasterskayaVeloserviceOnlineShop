// Добавление товара в корзину
function AddProduct(productId) {
    $.ajax({
        url: `/cart/add/${productId}`,
        complete: function (jqxhr) {
            if (jqxhr.status === 200) {
                Swal.fire({
                    icon: 'success',
                    text: 'Товар добавлен в корзину!',
                    showConfirmButton: false,
                    timer: 1500
                }).then((result) => {
                    if (result.isDismissed) {
                        window.location.reload();
                    }
                });
            } else if (jqxhr.status === 401) {
                Swal.fire({
                    icon: 'error',
                    text: 'Для добавления товара в корзину необходимо авторизоваться!',
                    showConfirmButton: false,
                    timer: 1500
                }).then((result) => {
                    if (result.isDismissed) {
                        window.location.reload();
                    }
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    text: 'Возникла ошибка при добавлении товара в корзину!',
                    showConfirmButton: false,
                    timer: 1500
                }).then((result) => {
                    if (result.isDismissed) {
                        window.location.reload();
                    }
                });
            }
        }
    })
};

// Добавление товара в избранное
function AddProductToFavorites(productId) {
    $.ajax({
        url: `/bikeParts/addProductToFavorites/${productId}`,
        complete: function (jqxhr) {
            if (jqxhr.status === 200) {
                Swal.fire({
                    icon: 'success',
                    text: 'Товар добавлен в корзину!',
                    showConfirmButton: false,
                    timer: 1500
                }).then((result) => {
                    if (result.isDismissed) {
                        window.location.reload();
                    }
                });
            } else if (jqxhr.status === 401) {
                Swal.fire({
                    icon: 'error',
                    text: 'Для добавления товара в избранное необходимо авторизоваться!',
                    showConfirmButton: false,
                    timer: 1500
                }).then((result) => {
                    if (result.isDismissed) {
                        window.location.reload();
                    }
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    text: 'Возникла ошибка при добавлении товара в избранное!',
                    showConfirmButton: false,
                    timer: 1500
                }).then((result) => {
                    if (result.isDismissed) {
                        window.location.reload();
                    }
                });
            }
        }
    })
};

// Добавление товара в сравнение
const checkboxesCompare = document.querySelectorAll('input[type="checkbox"][id*="checkBoxCompare"]');

checkboxesCompare.forEach((checkbox) => {
    checkbox.onclick = function () {

        if (checkbox.checked) {
            const bikePartId = checkbox.getAttribute('data-bikepart');

            $.ajax({
                url: `/bikeParts/addProductToComparison?id=${bikePartId}`,
                complete: function (jqxhr) {
                    if (jqxhr.status === 200) {
                        Swal.fire({
                            icon: 'success',
                            text: 'Товар добавлен в сравнение!',
                            showConfirmButton: false,
                            timer: 1500
                        }).then((result) => {
                            if (result.isDismissed) {
                                window.location.reload();
                            }
                        });
                    } else if (jqxhr.status === 401) {
                        Swal.fire({
                            icon: 'error',
                            text: 'Для добавления товара в сравнение необходимо авторизоваться!',
                            showConfirmButton: false,
                            timer: 1500
                        }).then((result) => {
                            if (result.isDismissed) {
                                document.getElementById(`checkBoxCompare-${bikePartId}`).checked = false;
                                window.location.reload();
                            }
                        });
                    } else {
                        Swal.fire({
                            icon: 'error',
                            text: 'Возникла ошибка при добавлении товара в сравнение!',
                            showConfirmButton: false,
                            timer: 1500
                        }).then((result) => {
                            if (result.isDismissed) {
                                document.getElementById(`checkBoxCompare-${bikePartId}`).checked = false;
                                window.location.reload();
                            }
                        });
                    }
                }
            })
        }
    }
});