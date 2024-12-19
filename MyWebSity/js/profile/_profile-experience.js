document.addEventListener('DOMContentLoaded', function () {
    const addExperienceButton = document.getElementById('add-experience-btn');
    const experienceList = document.getElementById('experience-list');
    const experienceTemplate = document.getElementById('experience-item-template').innerHTML;


    // Обработчик для добавления секции опыта работы
    addExperienceButton.addEventListener('click', addExperienceItem);

    // Функция для добавления секции опыта работы
    function addExperienceItem() {
        const newExperienceItem = document.createElement('div');
        newExperienceItem.innerHTML = experienceTemplate;
        experienceList.appendChild(newExperienceItem);
        setupExperienceItemEvents(newExperienceItem);
    }
    // Функция для настройки событий для секции опыта работы
    function setupExperienceItemEvents(experienceItem) {
        const deleteButton = experienceItem.querySelector('.delete-experience-btn');
        const form = experienceItem.querySelector('.experience-form');
        const saveButton = experienceItem.querySelector('.save-experience-btn');
        const companyNameInput = experienceItem.querySelector('.company-name-input');
        const positionInput = experienceItem.querySelector('.position-input');
        const periodInput = experienceItem.querySelector('.period-input');
        const responsibilitiesInput = experienceItem.querySelector('.responsibilities-input');

        deleteButton.addEventListener('click', () => {
            experienceItem.remove();
        });

        // Обработка формы для сохранения данных
        form.addEventListener('submit', (event) => {
            event.preventDefault();

            // Важная часть: проверьте корректность ввода!
            if (!companyNameInput.value || !positionInput.value || !periodInput.value || !responsibilitiesInput.value) {
                alert('Пожалуйста, заполните все поля.');
                return; // Прекращаем выполнение, если данные некорректные
            }

            console.log('Company Name:', companyNameInput.value);
            console.log('Position:', positionInput.value);
            console.log('Period:', periodInput.value);
            console.log('Responsibilities:', responsibilitiesInput.value);
            // Добавьте здесь код для отправки данных на сервер
            // Например:
            // fetch('/saveExperienceData', { // Запрос к контроллеру на сервере
            //   method: 'POST',
            //   body: JSON.stringify({ companyName: companyNameInput.value, ... }),
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
    addExperienceItem();
});