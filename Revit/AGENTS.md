# 🤖 Project Agents & Architecture Roles

Данный файл описывает архитектурные роли и зоны ответственности агентов (модулей) в проекте расширения для Revit.

---

### 🏗️ Архитектурная матрица

| Агент         | Технологический стек | Зона ответственности |
|:--------------| :--- | :--- |
| **Plugins**   | `.NET 4.8`, `RevitAPI.dll` | Прямое взаимодействие с БД Revit, транзакции, фильтры элементов, Geometry. |
| **WebBridge** | `WebView2`, `Newtonsoft.Json` | Маршалинг данных между C# и JS, обработка `ExternalEvents`, Dispatching. |
| **Core**      | `.NET Standard 2.0` | Бизнес-логика, расчеты, DTO-модели, общие интерфейсы и утилиты. |

---

### 🧬 Описание ролей

#### 1. Plugins (Revit Engine)
* **Context**: Работает строго в основном потоке Revit.
* **Tasks**: Создание, изменение и удаление элементов (`TransactionContext`).
* **Rules**: Запрещено использование UI-логики. Все тяжелые операции должны вызываться через `IExternalEventHandler`.

#### 2. Core (Shared)
* **Context**: Платформонезависимый слой.
* **Tasks**: Валидация данных, маппинг Revit-параметров в понятные UI-структуры.
* **Rules**: Никаких ссылок на `RevitAPI.dll`. Должен быть покрыт Unit-тестами на 100%.

#### 3. WebBridge (Communication)
* **Context**: Прослойка внутри `WebView2`.
* **Tasks**: Инъекция `HostObject`, регистрация `ScriptToMessageReceived` событий.
* **Rules**: Обеспечение асинхронности (Async/Await). Обязательная сериализация сложных объектов в JSON.

---

### 🔄 Поток взаимодействия (Data Flow)

1.  **UI** -> Отправляет JSON команду через `postMessage`.
2.  **Bridge** -> Десериализует команду и инициирует `ExternalEvent`.
3.  **BIM Core** -> Открывает `Transaction`, вносит изменения в модель Revit.
4.  **Logic** -> Формирует результат (Success/Error DTO).
5.  **Bridge** -> Возвращает ответ в **UI** через `ExecuteScriptAsync`.

---

### ⚠️ Ограничения (Guardrails)

* **Thread Safety**: Revit API не является потокобезопасным. Любой вызов из WebView2 должен проходить через очередь событий.
* **Resources**: Всегда вызывать `.Dispose()` для WebView2 при закрытии Dockable Pane или диалогового окна.
* **Version Control**: Слой Logic (.NET Standard 2.0) должен оставаться совместимым со всеми версиями Revit в рамках проекта.

---
*Generated for Revit Plugin Infrastructure v1.0*