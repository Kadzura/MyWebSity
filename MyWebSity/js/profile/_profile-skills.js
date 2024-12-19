document.addEventListener('DOMContentLoaded', function () {
    const addSkillButton = document.getElementById('add-skill-btn');
    const skillsList = document.getElementById('skills-list');
    const skillTemplate = document.getElementById('skill-item-template').innerHTML;

    // Обработчик для добавления секции навыка
    addSkillButton.addEventListener('click', addSkillItem);

    // Функция для добавления секции навыка
    function addSkillItem() {
        const newSkillItem = document.createElement('div');
        newSkillItem.innerHTML = skillTemplate;
        skillsList.appendChild(newSkillItem);
        setupSkillItemEvents(newSkillItem);
    }

    // Функция для настройки событий для секции навыка
    function setupSkillItemEvents(skillItem) {
        const deleteButton = skillItem.querySelector('.delete-skill-btn');
        const form = skillItem.querySelector('.skill-form');
        const skillNameInput = skillItem.querySelector('.skill-name-input');

        deleteButton.addEventListener('click', () => {
            skillItem.remove();
        });

        // Обработка формы для сохранения данных
        form.addEventListener('submit', (event) => {
            event.preventDefault();

            // Важная часть: проверьте корректность ввода!
            if (!skillNameInput.value) {
                alert('Пожалуйста, заполните все поля.');
                return; // Прекращаем выполнение, если данные некорректные
            }

            console.log('Skill Name:', skillNameInput.value);
            // Добавьте здесь код для отправки данных на сервер
            // Например:
            // fetch('/saveSkillData', { // Запрос к контроллеру на сервере
            //   method: 'POST',
            //   body: JSON.stringify({ skillName: skillNameInput.value, ... }),
            // })
            //   .then(response => response.json())
            //   .then(data => {
            //     console.log('Данные успешно сохранены:', data);
            //   })
            //   .catch(error => console.error('Ошибка сохранения данных:', error));
            form.reset();
        });

    }

    // Добавляем начальный элемент для примера.
    addSkillItem();
});