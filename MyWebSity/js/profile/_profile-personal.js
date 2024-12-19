document.addEventListener('DOMContentLoaded', function () {
    const profilePictureInput = document.getElementById('ProfilePicture');
    const profileImagePreview = document.getElementById('profile-image-preview');
    const uploadButton = document.querySelector('.upload-button');
    const profileForm = document.getElementById('profile-form');

    // Обработчик изменения для загрузки изображения профиля
    profilePictureInput.addEventListener('change', function () {
        if (this.files && this.files[0]) {
            const reader = new FileReader();

            reader.onload = function (e) {
                profileImagePreview.src = e.target.result;
            };

            reader.readAsDataURL(this.files[0]);
        }
    });

    // Обработчик клика по кнопке "Загрузить"
    uploadButton.addEventListener('click', () => {
        profilePictureInput.click(); // Имитируем клик по скрытому input
    });

    // Обработчик отправки формы с персональными данными
    profileForm.addEventListener('submit', (event) => {
        event.preventDefault();
        const formData = new FormData(profileForm);
        // Собираем данные и выводим в консоль. Здесь вы можете добавить код для отправки данных на сервер
        for (const [name, value] of formData) {
            console.log(`${name}: ${value}`);
        }
        // Сброс формы после отправки (если требуется)
        profileForm.reset();
    });
});