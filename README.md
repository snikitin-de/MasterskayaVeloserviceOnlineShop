# 💻 MasterskayaVeloserviceOnlineShop

💻 :bike: Проект интернет-магазина веломастерской на ASP.NET Core.

Проект реализован в виде MVC приложения и так же имеет своё WebApi. Проект обёрнут в Docker (см. раздел Docker).

![Главная страница сайта](https://github.com/user-attachments/assets/fc4ba245-c387-41bc-ab1f-630737b3b1c4)

![Swagger](https://github.com/user-attachments/assets/8654de37-69ba-4e1a-9d5f-040800c8f0d8)

## 📄 Описание

### Основной функционал

- Приложения *WebApi* и *MVC* используют общую БД и могут работать независимо друг от друга.
- **Авторизация и регистрация**: пользователи магазина могут авторизоваться и регистрироваться через ASP.NET Core Identity.
- **Пользователи**: авторизованные пользователи могут изменить личную информацию, просмотреть историю своих заказов.
- **Администратор**: администратору доступны функции по работе с заказами, пользователями, ролями, товарами и услугами.
- **Товары**: пользователям доступен просмотр товаров и услуг, подробной информации о товаре, добавление товаров в корзину, избранное и сравнение.
- **Заказы**: пользователи могу оформить свой заказ и просмотреть его статус в личном кабинете.

### Docker

В репозитории находятся два docker-compose файла:

- docker-compose.yml: для разработки.
- docker-compose-prod.yml: для production без *WebAPI*.

Для сборки приложения используете файл в зависимости от окружения.

Ниже рассмотрим `docker-compose.yml` для среды разработки с включённым Swagger.

Путь до Swagger по умолчанию {Scheme}://{ServiceHost}:{ServicePort}/swagger

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

### Установка и настройка

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
