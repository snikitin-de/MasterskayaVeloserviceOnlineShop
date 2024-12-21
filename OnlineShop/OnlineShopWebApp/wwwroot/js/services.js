﻿// Добавление услуги в корзину
function AddProduct(productId) {
    $.ajax({
        url: `/cart/add/${productId}`,
        complete: function (response) {
            if (response.status === 200) {
                Swal.fire({
                    icon: 'success',
                    text: 'Услуга добавлена в корзину!',
                    showConfirmButton: false,
                    allowOutsideClick: true,
                    allowEscapeKey: true,
                    timer: 1500
                }).then((result) => {
                    if (result.isDismissed) {
                        window.location.reload();
                    }
                });
            } else if(response.status === 401) {
                Swal.fire({
                    icon: 'error',
                    text: 'Для добавления услуги в корзину необходимо авторизоваться!',
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
                    text: 'Возникла ошибка при добавлении услуги в корзину!',
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

// Добавление услуги в избранное
function AddProductToFavorites(productId) {
    $.ajax({
        url: `/services/addProductToFavorites/${productId}`,
        complete: function (response) {
            if (response.status === 200) {
                Swal.fire({
                    icon: 'success',
                    text: 'Услуга добавлена в избранное!',
                    showConfirmButton: false,
                    allowOutsideClick: true,
                    allowEscapeKey: true,
                    timer: 1500
                }).then((result) => {
                    if (result.isDismissed) {
                        window.location.reload();
                    }
                });
            } else if (response.status === 401) {
                Swal.fire({
                    icon: 'error',
                    text: 'Для добавления услуги в избранное необходимо авторизоваться!',
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
                    text: 'Возникла ошибка при добавлении услуги в избранное!',
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