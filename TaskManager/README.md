# Инструкция по запуску проекта TaskManager

## Требования
- .NET 8.0 SDK
- PostgreSQL
- Доступ к базе данных с правами на создание таблиц
- NuGet-пакеты проекта (восстанавливаются через `dotnet restore`)

---

## 1. Клонирование репозитория
```bash
git clone https://github.com/npoleva/TaskManager
cd TaskManager/TaskManager
```

## 2. Настройка переменных окружения
- Создать файл private.env в корне проекта (TaskManager/TaskManager/):
```ini
DB_HOST=localhost
DB_PORT=5432
DB_NAME=task_manager_db
DB_USER=<пользователь_postgres>
DB_PASSWORD=<пароль_postgres>

```

## 3. Настройка базы данных PostgreSQL
```sql
CREATE DATABASE task_manager_db;
```

## 4. Применение существующих миграций
- Миграции находятся в папке Migrations
```bash
dotnet ef database update --project TaskManager.csproj
```

## 5. Восстановление зависимостей
```bash
dotnet restore
```

## 6. Запуск проекта
```bash
dotnet run --project TaskManager.csproj
```
- Приложение будет доступно на
```arduino
http://localhost:5122
```

## 7. Проверка работы API
- Получить список всех задач:
```bash
curl -X GET http://localhost:5122/api/tasks
```
- Получить задачу по ID:
```bash
curl -X GET http://localhost:5122/api/tasks/1
```
- Создать задачу:
```bash
curl -X POST http://localhost:5122/api/tasks \
-H "Content-Type: application/json" \
-d '{"title":"Новая задача","description":"Описание","isCompleted":false}'
```
- Обновить задачу:
```bash
curl -X PUT http://localhost:5122/api/tasks/1 \
-H "Content-Type: application/json" \
-d '{"id":1,"title":"Обновлённая задача","description":"Описание обновлено","isCompleted":true}'
```
- Удалить задачу:
```bash
curl -X DELETE http://localhost:5122/api/tasks/1
```
## 8. Swagger
- Swagger доступен по адресу:
```arduino
http://localhost:5122/swagger
```
