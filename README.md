# MyPrintHelper
Простой помошник для работы с принтерами.

Написан под свои нужды, для работы в СЦ.

Кнопка FindPrinters выводит список устройств в системе (Только устройства онлайн. Кроме устройств FAX и XPS.)
(Некоторые принеры фирмы НР даже после отключения остаются в системе с флагами Local = True, WorkOffline = true, поэтому попадают в список (пока).)

Кнопка PrinterSettings вызывает системное окно настроек принтера.

Кнопка PrintDialog позволяет отправить на печать страницу А4, полностью заполненную сеткой.

Кнопка GetProperties позволяет получить список свойств принтера (Строки без начений не попадают в список).



