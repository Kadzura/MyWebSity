document.addEventListener('DOMContentLoaded', function () {
    const addEducationButton = document.getElementById('add-education-btn');
    const educationList = document.getElementById('education-list');
    const educationTemplate = document.getElementById('education-item-template').innerHTML;

    // Обработчик для добавления секции образования
    addEducationButton.addEventListener('click', addEducationItem);

    // Функция для добавления секции образования
    function addEducationItem() {
        const newEducationItem = document.createElement('div');
        newEducationItem.innerHTML = educationTemplate;
        educationList.appendChild(newEducationItem);
        setupEducationItemEvents(newEducationItem);
    }
    // Функция для настройки событий для секции образования
    function setupEducationItemEvents(educationItem) {
        const deleteButton = educationItem.querySelector('.delete-education-btn');
        const form = educationItem.querySelector('.education-form');
        const saveButton = educationItem.querySelector('.save-education-btn');
        const universityNameInput = educationItem.querySelector('.university-name-input');
        const degreeInput = educationItem.querySelector('.degree-input');
        const periodInput = educationItem.querySelector('.period-input');

        deleteButton.addEventListener('click', () => {
            educationItem.remove();
        });

        // Обработка формы для сохранения данных
        form.addEventListener('submit', (event) => {
            event.preventDefault();

            // Важная часть: проверьте корректность ввода!
            if (!universityNameInput.value || !degreeInput.value || !periodInput.value) {
                alert('Пожалуйста, заполните все поля.');
                return; // Прекращаем выполнение, если данные некорректные
            }

            console.log('University Name:', universityNameInput.value);
            console.log('Degree:', degreeInput.value);
            console.log('Period:', periodInput.value);
            // Добавьте здесь код для отправки данных на сервер
            // Например:
            // fetch('/saveEducationData', { // Запрос к контроллеру на сервере
            //   method: 'POST',
            //   body: JSON.stringify({ universityName: universityNameInput.value, ... }),
            // })
            //   .then(response => response.json())
            //   .then(data => {
            //     console.log('Данные успешно сохранены:', data);
            //   })
            //   .catch(error => console.error('Ошибка сохранения данных:', error));

            form.reset(); // Очищаем форму после успешного сохранения
        });
    }
    // Добавляем начальный элемент для примера.
    addEducationItem();
});