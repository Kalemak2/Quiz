using System.Reflection.Metadata;
using System.Xml.Linq;

namespace Quiz
{

    public class Program
    {
        static int highestScore = 0;
        static string rekord_path = "rekord.txt";
        static string quiz_path = "quiz.txt";
        public static void Main(string[] args)
        {
            Menu();
        }

       /*******************************
        klasa: Quiz
        opis: Klasa ta jest odpowiedzialna za przeprowadzanie quizu. Wybiera losowe pytania, wyświetla je użytkownikowi, zbiera odpowiedzi i zapisuje wyniki.
        pola: 
        - int score: przechowuje aktualny wynik quizu.
        - int[] questionsNumber: przechowuje numery wylosowanych pytań.
        - string[] answersNumber: przechowuje odpowiedzi użytkownika.
        - string[] line: przechowuje linie pliku z pytaniami.
        - string quiz_path: ścieżka do pliku z pytaniami.
        - string rekord_path: ścieżka do pliku, w którym zapisywane są wyniki.
        - int highestScore: przechowuje najwyższy dotychczasowy wynik.
        autor: Kornel Pakulski
        ********************************/
        static void Quiz()
        {
            int score = 0;
            int[] questionsNumber = new int[10];
            string[] answersNumber = new string[10];

            string[] line = File.ReadAllLines(quiz_path);

            for (int i = 0; i < 10; i++)
            {
                Console.Clear();
                Random rnd = new Random();
                int random = rnd.Next(1, 31);
                questionsNumber[i] = random;

                string[] words = line[random].Split(";");
                DisplayQuestion(words);
                Console.Write("Twoja odpowiedź: ");
                int answer;
                if (int.TryParse(Console.ReadLine(), out answer))
                {
                    if (answer == Convert.ToInt32(words[2]))
                    {
                        score++;
                    }
                }

                answersNumber[i] = answer.ToString();
            }

            if (score > highestScore)
            {
                highestScore = score;
            }

            SaveScore(score, rekord_path);

            Console.Clear();
            Console.Write($"Koniec Quizu! Twój wynik to: {score} na 10 pkt.\nChcesz powtórzyć?\n1. Tak\n2. Nie\n3. Pokaż odpowiedzi\n\nTwój wybór:");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                ProcessUserChoice(choice, questionsNumber, answersNumber, line);
            }
        }
        /********************************
         metoda: DisplayQuestion
         opis: Ta metoda jest odpowiedzialna za wyświetlanie pytania quizu wraz z dostępnymi odpowiedziami. Pytanie i odpowiedzi są przekazywane jako tablica stringów, gdzie pierwszy element to pytanie, a kolejne to odpowiedzi.
         parametry: 
         - string[] words: tablica stringów, gdzie pierwszy element to pytanie, a kolejne to odpowiedzi.
         autor: Kornel Pakulsk
         ********************************/
        static void DisplayQuestion(string[] words)
        {
            Console.WriteLine($"{words[1]}\n1.{words[3]}\n2.{words[4]}\n3.{words[5]}\n4.{words[6]}");
        }

        /********************************
         metoda: SaveScore
         opis: Ta metoda jest odpowiedzialna za zapisywanie wyniku quizu do pliku. Prosi użytkownika o podanie swojego imienia, a następnie zapisuje imię i wynik do pliku.
         parametry: 
         - int score: wynik quizu do zapisania.
         - string imie: imie użytkownika do zapisania.
         - string rekord_path: ścieżka do pliku, w którym zapisywane są wyniki.
         autor: Kornel Pakulski
         ********************************/
        static void SaveScore(int score, string rekord_path)
        {
            Console.Write("Podaj swoje imię: ");
            string name = Console.ReadLine();
            File.AppendAllTextAsync(rekord_path, $"{name};{score}\n");
        }
        /********************************
        metoda: ProcessUserChoice
        opis: Ta metoda jest odpowiedzialna za przetwarzanie wyboru użytkownika po zakończeniu quizu.Użytkownik ma możliwość powtórzenia quizu, zakończenia działania programu lub wyświetlenia swoich odpowiedzi.
        parametry: 
        - int choice: wybór użytkownika.
        - int[] questionsNumber: tablica z numerami wybranych pytań.
        - string[] answersNumber: tablica z odpowiedziami użytkownika.
        - string[] line: tablica z liniami pliku z pytaniami.
        autor: Kornel Pakulski
        ********************************/
        static void ProcessUserChoice(int choice, int[] questionsNumber, string[] answersNumber, string[] line)
        {
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Menu();
                    break;
                case 2:
                    break;
                case 3:
                    ShowAnswers(questionsNumber, answersNumber, line);
                    break;
                default:
                    Console.Write("Nieprawidłowa opcja!");
                    break;
            }
        }

       /********************************
        metoda: ShowAnswers
        opis: Ta metoda jest odpowiedzialna za wyświetlanie odpowiedzi użytkownika i poprawnych odpowiedzi po zakończeniu quizu.Użytkownik ma możliwość powrotu do menu lub zakończenia działania programu.
        parametry: 
        - int[] questionsNumber: tablica z numerami wybranych pytań.
        - string[] answersNumber: tablica z odpowiedziami użytkownika.
        - string[] line: tablica z liniami pliku z pytaniami.
        autor: Kornel Pakulski
        ********************************/
        static void ShowAnswers(int[] questionsNumber, string[] answersNumber, string[] line)
        {
            Console.Clear();
            for (int i = 0; i < questionsNumber.Length; i++)
            {
                DisplayQuestion(line[questionsNumber[i]].Split(";"));
                string correctAnswer = line[questionsNumber[i]].Split(";")[Convert.ToInt32(line[questionsNumber[i]].Split(";")[2]) + 2];
                string playerAnswer = line[questionsNumber[i]].Split(";")[Convert.ToInt32(answersNumber[i]) + 2];

                Console.WriteLine($"\nPoprawna odpowiedź: {correctAnswer}\nTwoja odpowiedź: {playerAnswer}\n\n");
            }

            Console.Write("Czy chcesz wrócić do menu?\n1.Tak\n2.Nie\n\nTwój wybór: ");
            int k = Convert.ToInt32(Console.ReadLine());
            switch (k)
            {
                case 1:
                    Console.Clear();
                    Menu();
                    break;
                case 2:
                    break;
                default:
                    Console.WriteLine("Podana opcja jest nieprawidłowa!");
                    break;
            }
        }
        /********************************
        metoda: Menu
        opis: Ta metoda jest odpowiedzialna za wyświetlanie menu głównego quizu.Użytkownik ma możliwość rozpoczęcia quizu, wyjścia z programu, wyświetlenia wszystkich wyników lub zresetowania rekordów.
        autor: <Twoje imię i nazwisko>
        ********************************/

        static void Menu()
        {
            Console.Write($"Witaj w Quizie Informatycznym! Obecny najwyższy wynik to: {highestScore}\n1.Rozpocznij Quiz!\n2.Wyjdz\n3.Pokaż wszystkie wyniki\n4.Resetuj rekordy\nTwój wybór: ");
            int numbers;
            if (int.TryParse(Console.ReadLine(), out numbers))
            {
                switch (numbers)
                {
                    case 1:
                        Console.Clear();
                        Quiz();
                        break;
                    case 2:
                        break;
                    case 3:
                        Console.Clear();
                        Wyniki();
                        break;
                    case 4:
                        Console.Clear();
                        ResetScore();
                        Console.WriteLine("Rekordy zostały zresetowane!");
                        break;
                    default:
                        Console.WriteLine("Wybrano nieprawidłową opcję.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Wybrano nieprawidłową opcję.");
            }
        }
       /* *******************************
        metoda: Wyniki
        opis: Ta metoda jest odpowiedzialna za wyświetlanie wszystkich wyników quizu. Czyta wyniki z pliku i wyświetla je użytkownikowi.
        autor: Kornel Pakulski
        ********************************/

        static void Wyniki()
        {
            string[] line = File.ReadAllLines(rekord_path);
            for (int i = 0; i < line.Length; i++)
            {
                string[] rekords = line[i].Split(";");
                Console.WriteLine(rekords[0] + " " + rekords[1]);
            }
        }

        /********************************
        metoda: ResetScore
        opis: Ta metoda jest odpowiedzialna za resetowanie wszystkich wyników quizu. Czyści plik z wynikami.
        autor: Kornel Pakulski
        ********************************/

        static void ResetScore()
        {
            File.WriteAllText(rekord_path, "");

        }
    }
}