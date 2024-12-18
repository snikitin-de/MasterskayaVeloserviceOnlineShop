// Очистка сравнения
function ClearComparison(userId) {
    $.ajax({
        url: `/comparison/clear`
    })
    $(document).ajaxStop(function () {
        window.location.reload();
    });
};