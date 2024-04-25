const modal = document.getElementById('confirmModal');
const openModalBtn = document.getElementById('openModalBtn');
const confirmBtn = document.getElementById('confirmBtn');
const cancelBtn = document.getElementById('cancelBtn');
var form;
var buttons = document.querySelectorAll('.btnDelete');


buttons.forEach(function(button) {
    button.addEventListener('click', function(event) {
        var clickedButton = event.target;
        form = clickedButton.closest('form');
        modal.style.display = 'block';
    });
});
confirmBtn.addEventListener('click', function() {
    if(form) form.submit();
    closeModal();

});

// Обработка нажатия на кнопку "Отмена" или клика вне модального окна
cancelBtn.addEventListener('click', closeModal);
window.addEventListener('click', function(event) {
    if (event.target === modal) {
        closeModal();
    }
});

// Функция для закрытия модального окна
function closeModal() {
    modal.style.display = 'none';
}