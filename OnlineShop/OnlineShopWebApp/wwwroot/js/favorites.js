// Добавление товара в корзину
function AddProduct(productId) {
    $.ajax({
        url: `/cart/add/${productId}`,
        complete: function (jqxhr) {
            if (jqxhr.status === 200) {
                Swal.fire({
                    icon: 'success',
                    text: 'Товар или услуга добавлены в корзину!',
                    showConfirmButton: false,
                    allowOutsideClick: true,
                    allowEscapeKey: true,
                    timer: 1500
                }).then((result) => {
                    if (result.isDismissed) {
                        window.location.reload();
                    }
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    text: 'Возникла ошибка при добавлении товара или услуги в корзину!',
                    showConfirmButton: false,
                    allowOutsideClick: true,
                    allowEscapeKey: true,
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
                url: `/bikeParts/addProductToComparison?id=${bikePartId}`
            })

            $(document).ajaxStop(function () {
                window.location.reload();
            });
        }
    }
});

// Удаление товара/услуги из избранного
function RemoveFromFavorites(productId) {
    $.ajax({
        url: `/favorites/remove/${productId}`,
        complete: function (jqxhr) {
            if (jqxhr.status === 200) {
                if (jqxhr.status === 200) {
                    Swal.fire({
                        icon: 'success',
                        text: 'Товар или услуга удалены из избранного!',
                        showConfirmButton: false,
                        allowOutsideClick: true,
                        allowEscapeKey: true,
                        timer: 1500
                    }).then((result) => {
                        if (result.isDismissed) {
                            window.location.reload();
                        }
                    });
                } else {
                    Swal.fire({
                        icon: 'error',
                        text: 'Возникла ошибка при удалении товара или услуги из избранного!',
                        showConfirmButton: false,
                        allowOutsideClick: true,
                        allowEscapeKey: true,
                        timer: 1500
                    }).then((result) => {
                        if (result.isDismissed) {
                            window.location.reload();
                        }
                    });
                }
            }
        }
    })
};

// Очистка избранного
function ClearFavorites(userId) {
    $.ajax({
        url: `/favorites/clear`
    })
    $(document).ajaxStop(function () {
        window.location.reload();
    });
};