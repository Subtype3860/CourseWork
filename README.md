# CourseWork
- Создать базу данных (SQLite)
- Создать таблицу для сущности «Пользователь»
- Создать таблицу для сущности «Статья»
- Создать таблицу для сущности «Тег»
- Создать таблицу для сущности «Комментарий»
- Создать таблицу связей для сущностей «Статья» и её «Теги»
- Создать проект ASP.NET MVC
- Выбрать архитектуру проекта
- При помощи папок или отдельных проектов разделить проект на слои, исходя из выбранной архитектуры
- Создать бизнес-модель «Пользователь»
- Создать бизнес-модель «Статья»
- Создать бизнес-модель «Тег»
- Создать бизнес-модель «Комментарий»
- Подключить ORM Entity Framework и настроить контекст данных для бизнес-моделей
- Создать контроллер для бизнес-модели «Пользователь»
- Создать контроллер для бизнес-модели «Статья»
- Создать контроллер для бизнес-модели «Тег»
- Создать контроллер для бизнес-модели «Комментарий»
- В контроллере пользователей реализовать логику регистрации, редактирования, удаления пользователя, а также логику получения всех пользователей и логику получения только одного пользователя по его идентификатору
- В контроллере статей реализовать логику создания, редактирования, удаления статьи, а также логику получения всех статей и всех статей определённого автора по его идентификатору
- В контроллере комментариев реализовать логику создания, редактирования, удаления комментария, а также логику получения всех комментариев и только одного комментария по его идентификатору
- В контроллере тегов реализовать логику создания, редактирования, удаления тега, а также логику получения всех тегов и только одного тега по его идентификатору
- При регистрации пользователю присваивается базовая роль — Пользователь
- Добавить в базу данных новую таблицу для сущности «Роль»
- Добавить в базу данных таблицу связей для сущностей «Пользователь» и его «Роль»
- Добавить в таблицу ролей роль администратора, роль модератора и роль пользователя
- Добавить в таблицу трех пользователей
- Для одного пользователя добавить роль администратора, для другого — роль модератора, для третьего — роль пользователя
- Добавить в проект новую бизнес-модель «Роль»
- Добавить в бизнес-модель пользователя список его ролей
- В контроллере аутентификации реализовать логику для аутентификации пользователей. Роли пользователя, успешно прошедшего аутентификацию, должны сохраниться в клаймах
- Заблокировать любой метод в любом контроллере атрибутом авторизации (только для администратора) и протестировать работоспособность
- Подключить инструмент Bootstrap
- Сверстать меню
- Сверстать футер
- В меню добавить ссылки для возможности перехода на основные страницы приложения
- Добавить представление для просмотра, редактирования, добавления и удаления пользователя
- Добавить представление для просмотра, редактирования, добавления и удаления роли пользователя
- Добавить представление для просмотра, редактирования, добавления и удаления статьи
- Добавить представление для просмотра, редактирования, добавления и удаления комментария
- Добавить представление для просмотра всех пользователей
- Добавить представление для просмотра всех ролей
- Добавить представление для просмотра всех статей
- Добавить представление для просмотра всех комментариев
- Добавить представление для входа в приложение
- Создать представление с ошибкой «Доступ запрещен»
- Создать представление с ошибкой «Ресурс не найден»
- Создать представление с текстом «Что-то пошло не так»
- Добавить условия в представления для отображения контента в зависимости от того, вошел ли пользователь в систему и владеет ли он повышенными ролями
- Во всех представлениях, требующих ввод данных от пользователя, добавить валидацию введенных значений
- Добавить логирование действий пользователя
- Добавить логирование ошибок в приложении
- Добавить глобальный обработчик исключений
- В случае непредвиденной ошибки добавить перенаправление на страницу с текстом «Что-то пошло не так»
- Добавить новый проект с шаблоном веб-API ASP.NET CORE в решение
- Добавить контроллеры для бизнес моделей (пользователь, статья и т.д.)
- В контроллеры добавить основные операции с бизнес-моделями
- При помощи DI внедрить в контроллеры соответствующие и уже реализованные ранее сервисы для работы с бизнес моделями (UserService, PostService и т.д.)
- Добавить документацию API при помощи Swagger
