# 💻 MasterskayaVeloserviceOnlineShop

💻 :bike: Проект интернет-магазина веломастерской на *ASP.NET Core*.

Проект реализован в виде *MVC* приложения и так же имеет своё *WebApi*. Проект обёрнут в *Docker* (см. раздел *Docker*).

![Главная страница сайта](https://github.com/user-attachments/assets/fc4ba245-c387-41bc-ab1f-630737b3b1c4)

![Swagger](https://github.com/user-attachments/assets/8654de37-69ba-4e1a-9d5f-040800c8f0d8)

## 📄 Описание

### Основной функционал

- Приложения *WebApi* и *MVC* используют общую БД и могут работать независимо друг от друга.
- **Авторизация и регистрация**: пользователи магазина могут авторизоваться и регистрироваться через *ASP.NET Core Identity*.
- **Пользователи**: авторизованные пользователи могут изменить личную информацию, просмотреть историю своих заказов.
- **Администратор**: администратору доступны функции по работе с заказами, пользователями, ролями, товарами и услугами.
- **Товары**: пользователям доступен просмотр товаров и услуг, подробной информации о товаре, добавление товаров в корзину, избранное и сравнение.
- **Заказы**: пользователи могу оформить свой заказ и просмотреть его статус в личном кабинете.

### Установка и настройка

В данном разделе рассмотрим установку и настройку приложения с *Docker*.

#### Docker

Перед билдом и запуском docker-compose файла необходимо:

1. Сконфигурировать *Nginx*.
2. Настроить переменные окружения в файле *docker.env*.
3. Настроить переменные окружения в файле *app.env*.

##### Конфигурирование Nginx

В папке решения `OnlineShop` необходимо создать папку `nginx` и под папку `ssl`:

![Nginx](https://github.com/user-attachments/assets/bfbe4964-8a8e-477f-8c54-0637ec76e780)

В папке `ssl` расположить сертификат с ключом: `*.crt` и `*.key`.

Также в папке `nginx` необходимо создать файл `nginx.conf`:

```
user nginx;
worker_processes auto;

events {
    worker_connections 1024;
}

http {
    include       mime.types;
    default_type  application/octet-stream;

    # SSL настройки
    ssl_protocols TLSv1.2 TLSv1.3;
    ssl_ciphers HIGH:!aNULL:!MD5;
    ssl_prefer_server_ciphers on;
    ssl_session_cache shared:SSL:10m;
    ssl_session_timeout 1d;
    ssl_session_tickets off;

    add_header Strict-Transport-Security "max-age=31536000; includeSubDomains" always;
    add_header X-Content-Type-Options nosniff;
    add_header X-Frame-Options DENY;
    add_header X-XSS-Protection "1; mode=block";

    server {
        listen 443 ssl;
        server_name localhost;

        ssl_certificate /etc/nginx/ssl/cert.crt;
        ssl_certificate_key /etc/nginx/ssl/cert.key;

        location / {
            proxy_pass http://masterskaya_veloservice_web_app:8080;
            proxy_http_version 1.1;
            proxy_set_header Upgrade $http_upgrade;
            proxy_set_header Connection keep-alive;
            proxy_set_header Host $host;
            proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
            proxy_set_header X-Forwarded-Proto $scheme;
        }

        location /swagger/ {
            proxy_pass http://masterskaya_veloservice_web_api:8080;
            proxy_http_version 1.1;
            proxy_set_header Upgrade $http_upgrade;
            proxy_set_header Connection keep-alive;
            proxy_set_header Host $host;
            proxy_cache_bypass $http_upgrade;
            proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
            proxy_set_header X-Forwarded-Proto $scheme;
        }
    }

    # Redirect HTTP to HTTPS
    server {
        listen 80;
        server_name localhost;

        return 301 https://localhost$request_uri;
    }
}
```

##### Настройка переменных окружения в файле docker.env

Файл *docker.env* со следующими переменными окружения необходимо создать в решении `OnlineShop`:

```env
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://+:8080
POSTGRES_DB=MasterskayaVeloserviceOnlineShop
POSTGRES_USER=user
POSTGRES_PASSWORD=12345678
```

> [!IMPORTANT]
> Не забудьте изменить пароль на безопасный!

##### Настройка переменных окружения в файле app.env

Файл *app.env* со следующими переменными окружения необходимо создать в решении `OnlineShop`ю

В файле *app.env* указывается `KeyId` и `SecretId` для доступа к API, UUID секрета и его версия, версию можно указывать как конкретную версию, так и `latest`.

```env
CLOUD_RU_KEY_ID=xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
CLOUD_RU_SECRET=xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
DATABASE_SETTINGS_SECRET_ID=46e4044f-4a00-4cf4-b5e0-04df25f5ee8d
DATABASE_SETTINGS_SECRET_VERSION=4
WEBAPI_SECRET_ID=0d656041-22c5-4e24-8c45-6ec636311804
WEBAPI_SECRET_VERSION=latest
APP_SECRET_ID=7b6d7fde-fda7-4dcc-9dfd-a9726ec55eb9
APP_SECRET_VERSION=latest
```

Для работы приложения необходимо зарегистрироваться на [cloud.ru](https://cloud.ru/), создать сервисный аккаунт, получить `KeyId` и `SecretId`, которые необходимо указать в файле *app.env* и создать секреты.

![Secret Manager](https://github.com/user-attachments/assets/b9f3d455-dc51-4e7a-b802-4efc15aaeeeb)

Для каждой среды `DEV`, `PROD` и т.п. необходимо создать три секрета.

Рассмотрим на примере создания секретов для DEV-среды:

1. Секрет БД `DATABASE_DEV`:

В секрете указывается строка подключения к БД:

```json
{
  "ConnectionString": "Host=postgres;Username=user;Password=12345678;Database=MasterskayaVeloserviceOnlineShop"
}
```

> [!IMPORTANT]
> Данные в строке подключения к БД должны совпадать с теми данными, которые были указаны в файле `docker.env`.

2. Секрет WebApi `WEBAPI_DEV`:

В секрете указываются настройки JWT:

```json
{
  "JwtKey": "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
  "JwtIssuer": "https://localhost:44341",
  "JwtAudience": "http://localhost:4200"
}
```

3. Секрет MVC `APP_DEV`:

В секрете указываются дефолтные почта и пароль администратора магазина:

```json
{
  "DefaultAdminEmail": "admin@gmail.com",
  "DefaultAdminPassword": "12345678"
}
```

> [!IMPORTANT]
> Не забудьте изменить email и пароль на безопасные!

##### Сборка приложения

В репозитории находятся два docker-compose файла:

- docker-compose.yml: для разработки.
- docker-compose-prod.yml: для production без *WebAPI*.

Для сборки приложения используете файл в зависимости от окружения.

Ниже рассмотрим `docker-compose.yml` для среды разработки с включённым Swagger.

Путь до Swagger по умолчанию `{Scheme}://{ServiceHost}:{ServicePort}/swagger`.

```yaml
version: '3.9'

services:
  postgres:
    container_name: masterskaya_veloservice_db
    image: postgres:17.2
    env_file:
      - docker.env
    volumes:
      - postgresql_data:/var/lib/postgresql/data
    restart: unless-stopped
    logging:
      driver: "json-file"
      options:
        max-size: "10m"
        max-file: "10"


  nginx:
    image: nginx:1.27.3
    container_name: nginx
    ports:
      - "80:80"
      - "443:443"
    volumes:
      - ./nginx/nginx.conf:/etc/nginx/nginx.conf
      - ./nginx/ssl:/etc/nginx/ssl
    depends_on:
      - onlineshopwebapp
      - onlineshopwebapi 
    restart: unless-stopped
    logging:
      driver: "json-file"
      options:
        max-size: "10m"
        max-file: "10"


  onlineshopwebapp:
    container_name: masterskaya_veloservice_web_app
    image: ${DOCKER_REGISTRY-}onlineshopwebapp
    env_file:
      - docker.env
    build:
      context: .
      dockerfile: OnlineShopWebApp/Dockerfile
    volumes:
      - images:/app/wwwroot/images
      - data-protection-keys:/root/.aspnet/DataProtection-Keys
    depends_on:
      - postgres
    restart: unless-stopped
    logging:
      driver: "json-file"
      options:
        max-size: "10m"
        max-file: "10"


  onlineshopwebapi:
    container_name: masterskaya_veloservice_web_api
    image: ${DOCKER_REGISTRY-}onlineshopwebapi
    env_file:
      - docker.env
    build:
      context: .
      dockerfile: OnlineShopWebApi/Dockerfile
    depends_on:
      - postgres
    restart: unless-stopped
    logging:
      driver: "json-file"
      options:
        max-size: "10m"
        max-file: "10"


networks:
  default:
    driver: bridge
    driver_opts:
      com.docker.network.driver.mtu: 1450
      
volumes:
  postgresql_data:
  data-protection-keys:
  images:
```

## 💻 Работа программы

Демонстрация работы программы:

https://github.com/user-attachments/assets/6dcd230e-624e-4eab-9cb1-118a8e1715b3

## 🔧 Техническая часть

* ASP.NET CORE MVC/WebApi
* Entity Framework Core
* Linq
* ASP.NET Core Identity
* JWT bearer-based authentication
* Postman/Swagger
* Dependency Injection
* PostgreSQL
* Serilog
* Docker
* Secret Manager (cloud.ru)
* Nginx

### 🧩 Архитектура

![Solution](https://github.com/user-attachments/assets/03ef4b76-2b9d-43fe-9c6c-98502564dd2e)
![WebApp](https://github.com/user-attachments/assets/c91d1997-42fa-4bcf-9974-f093444ec6db)
