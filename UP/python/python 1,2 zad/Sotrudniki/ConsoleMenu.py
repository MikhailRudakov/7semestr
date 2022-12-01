from MethodsCode import file_open, insert, drop_by_arg, find, avg_age, save, show_csv
from builtins import print

FILENAME = "users2.csv"

MENU = {
    '1': 'Открыть файл',
    '2': 'Добавить новго сотрудника',
    '3': 'Удалить',
    '4': 'Найти по значению',
    '5': 'Вывести средний возраст',
    '6': 'Сохранить в файл',
    '7': 'Вывести записи',
    '0': '<-Меню',
    'exit': 'Выход'
}

for key, val in MENU.items():
    print(key, '-', val)        # написать "номер действия" - "описание действия"

while True:
    action = input('>_ ')
    if action == '1':  # если - действие открыть файл
        file_open()
    elif action == '2':  # если - действие добавить файл
        print(insert(input('ФИО: '), int(input('Возраст: ')), input('Телефон: '), input('Отдел: ')))
    elif action == '3':  # если - действие удалить файл
        col = input('Колонка: ')
        val = input('Значение: ')
        print(drop_by_arg(val, col_name=col))
    elif action == '4':  # если - действие найти по значению
        col = input('Колонка: ')
        val = input('Значение: ')
        find(val, col_name=col)
    elif action == '5':  # если - действие ввести средний возраст
        avg_age()
    elif action == '6':     # если - действие сохранить файл
        save()
    elif action == '0':     # если - действие меню
        for key, val in MENU.items():
            print(key, '-', val)
    elif action == '7':     # если - ввывести записи
        show_csv()
    elif action == 'exit':  # если - действие выйти
        break