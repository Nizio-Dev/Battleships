# Battleships

Wyjaśnienia:

Miałem pomysł na mądrzejszą strategię, w której bym śledził wykonane uderzenia i zaimplementował algorytm z nawrotami, ale myślenie nad samą ideą działania takiego algorytmu zabrałaby więcej czasu, niż implementacja. Jeżeli nie trafił nic ostatnio oraz na mapie niebyłoby statków, które nie są zatopione, to losowo wybrałby nowy punkt i w wybranym kierunu sprawdzałby ile uda mu się zbić punktów pod rząd. W wypadku, gdy nie zbije to zmienia kierunek, natomiast jeżeli trafiłby to kontynuowałby uderzanie w danym kierunku. Jeżeli natrafi na puste pole, lecz nie udało się zbić to idzie do punktu początkowego i zbija w przeciwnym kierunku.

Solucja został podzielony na 2 projekty, pierwszy zawiera:
* Program.cs - Wstępne ustawienia gry
* Game.cs - Logika gry. Zawiera instancje graczy
* Players.cs - Każdy gracz posiada imię, wykorzystaną strategię, mapę punktów uderzonych oraz własną mapę. (Mapa odzwierciedla planszę w grze statki. znajdują się tam ustawienia przeciwnika)
* Map.cs - Mapa zawiera ilość zatopionych statków, metody do zainicjaliwozania obiektów, wyświetlania, uderzenia w pole oraz listę list 10x10 obiektów Field. Klasa Field zawiera swoje współrzędne, znak do wyświetlania oraz czy istnieje statek (nullable).
* Dumb/SmarterStrategy.cs jest to klasa strategii, którą wykorzystują gracze. Każda instancja ma pole wykorzystującego ją gracza.

Drugi projekt jest projektem xUnit, który zawiera podstawowe testy niektórych metod.

Działanie:
* Program.cs - Użytkownik wybiera opcje strategii, które chce wykorzystać. W konstruktorze gry podane są nazwy graczy oraz ich wykorzystane strategie.
* Game.cs - Pętla gry, w której wykonywane są czynności oraz warunki końcowe. Gracz wykonuje ruch, nastepnie wyswietlany jest wynik jego inputu (W tym przypadku 'bota').
* Player.cs - Wykonywany ruch odwołuje się do Strategii, która atakuje.
* Dumb/SmarterStrategy - Odpowiednio manipuluje właściwościami gracza, takimi jak mapa czy mapa uderzeń, aby osiągnąć cel wygranej


W kodzie zawarłem bardziej techniczne wyjaśnienia przy pomocy komentarzy.
