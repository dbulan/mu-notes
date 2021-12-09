# 1. Начальная настройка сервера с помощью Ubuntu 20.04
https://www.digitalocean.com/community/tutorials/initial-server-setup-with-ubuntu-20-04-ru

# 2. Установка Nginx в Ubuntu 20.04
https://www.digitalocean.com/community/tutorials/how-to-install-nginx-on-ubuntu-20-04-ru
 
# 3. Установка Linux, Nginx, MySQL, PHP (стека LEMP) в Ubuntu 20.04
https://www.digitalocean.com/community/tutorials/how-to-install-linux-nginx-mysql-php-lemp-stack-on-ubuntu-20-04-ru

# ?. UFW Essentials: Common Firewall Rules and Commands
https://www.digitalocean.com/community/tutorials/ufw-essentials-common-firewall-rules-and-commands

# ?. Настройка ключей SSH в Ubuntu 20.04
https://www.digitalocean.com/community/tutorials/how-to-set-up-ssh-keys-on-ubuntu-20-04-ru


# ------------- 1 ------------- #

// Hеобходимо настроить на сервере обычного пользователя без прав root с привилегиями sudo.

# Шаг 1 — Вход с привилегиями root

$ ssh root@your_server_ip

# Шаг 2 — Создание нового пользователя

$ adduser sammy

# Шаг 3 — Предоставление административных прав

$ usermod -aG sudo sammy

# Шаг 4 — Настройка базового брандмауэра

$ ufw app list
/**
Available applications:
	OpenSSH
*/

$ ufw allow OpenSSH // брандмауэр разрешает подключения SSH
$ ufw enable		// активировать брандмауэр, введите y и нажмите ENTER, чтобы продолжить.
$ ufw status		// Чтобы увидеть, что подключения SSH разрешены
/**
Status: active

To                         Action      From
--                         ------      ----
OpenSSH                    ALLOW       Anywhere
OpenSSH (v6)               ALLOW       Anywhere (v6)
*/

# ------------- 1 ------------- #

# ------------- 2 ------------- #

# Шаг 1 — Установка Nginx

$ sudo apt update // Первое взаимодействие с системой пакетов apt в этом сеансе
$ sudo apt install nginx

# Шаг 2 — Настройка брандмауэра

$ sudo sudo ufw app list // Для вывода списка конфигураций приложений, которые известны ufw
/**
Available applications:
	Nginx Full				> этот профиль открывает порт 80 (обычный веб-трафик без шифрования) и порт 443 (трафик с шифрованием TLS/SSL)
	Nginx HTTP				> этот профиль открывает только порт 80 (обычный веб-трафик без шифрования)
	Nginx HTTPS				> этот профиль открывает только порт 443 (трафик с шифрованием TLS/SSL)
	OpenSSH
*/

// Рекомендуется применять самый ограничивающий профиль, который будет разрешать заданный трафик. 
// Сейчас нам нужно будет разрешить трафик на порту 80.

$ sudo ufw allow 'Nginx HTTP'
$ sudo ufw status

# Шаг 3 — Проверка веб-сервера

$ systemctl status nginx // проверить работу службы

$ curl -4 icanhazip.com // узнать IP сервера

http://ip

# Шаг 4 — Управление процессом Nginx

$ sudo systemctl stop nginx
$ sudo systemctl start nginx
$ sudo systemctl restart nginx // Чтобы остановить и снова запустить службу, введите:
$ sudo systemctl reload nginx  // Если вы просто вносите изменения в конфигурацию, во многих случаях Nginx может перезагружаться без отключения соединений.

$ Шаг 5 — Настройка блоков сервера (рекомендуется)

$ sudo mkdir -p /var/www/your_domain 					// используя флаг -p для создания необходимых родительских директорий
$ sudo chown -R $USER:$USER /var/www/your_domain		// $USER eto ja sejcas -> sammy, esli nuzen kto-to drugo to togda sami vpisivaem v mesto $USER
$ sudo chmod -R 755 /var/www/your_domain

$ nano /var/www/your_domain/index.html

/**
	Для обслуживания этого контента Nginx необходимо создать серверный блок с правильными директивами.
	Вместо того чтобы изменять файл конфигурации по умолчанию напрямую, мы создадим новый файл в директории:
*/

$ sudo nano /etc/nginx/sites-available/your_domain
/**
server {
	listen 80;
	listen [::]:80;

	root /var/www/your_domain;
	index index.html index.htm index.nginx-debian.html;

	server_name your_domain www.your_domain;

	location / {
			try_files $uri $uri/ =404;
	}
}
*/

/**
	Мы обновили конфигурацию root с указанием новой директории и заменили server_name на имя нашего домена.
	Теперь мы активируем файл, создав ссылку в директории sites-enabled, который Nginx считывает при запуске:
*/

$ sudo ln -s /etc/nginx/sites-available/your_domain /etc/nginx/sites-enabled/


// Чтобы избежать возможной проблемы с хэшированием памяти при добавлении дополнительных имен серверов, необходимо изменить одно значение в файле /etc/nginx/nginx.conf
$ sudo nano /etc/nginx/nginx.conf
// Найдите директиву server_names_hash_bucket_size и удалите символ #
// Если вы используете nano, вы можете быстро найти слова в файле, нажав CTRL и w
/**
...
http {
    ...
    server_names_hash_bucket_size 64;
    ...
}
...
*/

$ sudo nginx -t
$ sudo systemctl restart nginx

# Шаг 6 — Знакомство с важными файлами и директориями Nginx

/etc/nginx/sites-available/: директория, где могут храниться серверные блоки для каждого сайта. 
Nginx не будет использовать файлы конфигурации из этой директории, если они не будут связаны с директорией sites-enabled. 
Обычно конфигурации серверных блоков записываются в эту директорию и активируются посредством ссылки на другую директорию.

/etc/nginx/sites-enabled/: директория, где хранятся активные серверные блоки каждого узла. 
Они созданы посредством ссылки на файлы конфигурации в директории sites-available.

// Журналы сервера

/var/log/nginx/access.log: каждый запрос к вашему веб-серверу регистрируется в этом файле журнала, если Nginx не настроен иначе.

/var/log/nginx/error.log: любые ошибки Nginx будут регистрироваться в этом журнале.


# ------------- 2 ------------- #

# ------------- 3 ------------- #

# Шаг 3 — Установка PHP
$ sudo apt install php-fpm php-mysql

# Шаг 4 — Настройка Nginx для использования процессора PHP
$ sudo nano /etc/nginx/sites-available/your_domain
/**
server {
	
	listen 80;
	listen [::]:80;

	root /var/www/your_domain;
	index index.html index.htm index.php;

	server_name your_domain www.your_domain;

	location / {
		try_files $uri $uri/ =404;
	}
	
	location ~ \.php$ {
        include snippets/fastcgi-php.conf;
        fastcgi_pass unix:/var/run/php/php7.4-fpm.sock;
	}

    location ~ /\.ht {
        deny all;
    }
}
*/

$ sudo nginx -t
$ sudo systemctl reload nginx

$ sudo rm /var/www/your_domain/info.php // remove

# Шаг 6 — Тестирование подключения к базе данных для PHP

# ------------- 3 ------------- #