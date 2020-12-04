# Tag Cloud

## Консольное приложение, создающее облако тегов
Аргументы консольной строки можно посмотреть с помощью команды --help

## Реализованы следующие пункты на перспективу:

### Предобработка слов:
Есть возможность влиять на список скучных слов
Есть возможность выбирать часть речи

### Формат результата:
Есть возможность выбирать формат изображений
Есть возможность задавать несколько цветов, или не задавать вовсе (в таком случае будут использованы случайные цвета)



## Примеры раскраски облака тегов

![RandomExample](https://github.com/CaptainBelyash/di/blob/master/TagCloud/examples/random_color_example.png)

![TwoColorExample](https://github.com/CaptainBelyash/di/blob/master/TagCloud/examples/two_color_example.png)

## Примеры облаков тегов, построенных по определенным частям речи

### Все части речи (за исключением скучных) попали в облако

--file c:/shpora/cake.txt --ex jpg --outname cake_all_words --minfont 30 --maxfont 40 --colors "48 213 200"

![AllWords](https://github.com/CaptainBelyash/di/blob/master/TagCloud/examples/cake_all_words.png)

### Только существительные

--file c:/shpora/cake.txt --ex jpg --outname cake_nouns --minfont 30 --maxfont 40 --colors "48 213 200" --gramparts Noun

![NounWords](https://github.com/CaptainBelyash/di/blob/master/TagCloud/examples/cake_nouns.png)

### Cуществительные и глаголы

--file c:/shpora/cake.txt --ex jpg --outname cake_nouns_verbs --minfont 30 --maxfont 40 --colors "48 213 200" --gramparts Noun,Verb

![NounVerbsWords](https://github.com/CaptainBelyash/di/blob/master/TagCloud/examples/cake_nouns_verbs.png)


## Шутка длиною в титулы последнего императора (сделано черновиком, не разделяющим строки по пробелам)

![Nikolai](https://github.com/CaptainBelyash/di/blob/master/TagCloud/examples/nikolai.png)
