using System;
using KPO_LAB_3;

class MAIN
{
    static void Main(string[] args)
    {
        // Создаем объект Word
        Word word = new Word("Example");

        // Тестируем метод GetLength
        Console.WriteLine($"Word: {word.Text}, Length: {word.GetLength()}");

        // Тестируем StartsWithCapital
        Console.WriteLine($"Starts with capital: {word.StartsWithCapital()}");

        // Тестируем ContainsSubstring
        Console.WriteLine($"Contains 'amp': {word.ContainsSubstring("amp")}");
        Console.WriteLine($"Contains 'bxz': {word.ContainsSubstring("xyz")}");

        // Тестируем ToUpperCase и ToLowerCase
        Console.WriteLine($"Uppercase: {word.ToUpperCase()}");
        Console.WriteLine($"Lowercase: {word.ToLowerCase()}");

        // Тестируем IsNumber
        Word numberWord = new Word("12345");
        Console.WriteLine($"Is number (word '12345'): {numberWord.IsNumber()}");

        Word notNumberWord = new Word("abc123");
        Console.WriteLine($"Is number (word 'abc123'): {notNumberWord.IsNumber()}");

        // Тестируем IsPunctuationMark
        Punctuation punctuationmark = new Punctuation('.');
        Console.WriteLine($"Symbol: {punctuationmark.Symbol}, Is Punctuation: {punctuationmark.IsPunctuationMark()}");

        // Тестируем Sentence
        Sentence sentence = new Sentence();
        sentence.AddWord(new Word("Hello"));
        sentence.AddWord(new Word("world"));
        sentence.AddPunctuation(new Punctuation('!'));

        Console.WriteLine(sentence.ToString()); // Выведет: "Hello world !"

        // Тестируем Text
        Text text = new Text();

        Sentence sentence1 = new Sentence();
        sentence1.AddWord(new Word("Hello"));
        sentence1.AddWord(new Word("world"));
        sentence1.AddPunctuation(new Punctuation('!'));

        Sentence sentence2 = new Sentence();
        sentence2.AddWord(new Word("This"));
        sentence2.AddWord(new Word("is"));
        sentence2.AddWord(new Word("a"));
        sentence2.AddWord(new Word("test"));
        sentence2.AddPunctuation(new Punctuation('.'));

        text.AddSentence(sentence1);
        text.AddSentence(sentence2);

        Console.WriteLine(text.ToString()); // Ожидаемый вывод: "Hello world ! This is a test ."


        // Тестируем Tokenize

        string inputText = "Hello world! This is a test. Are you ready? Let's go.";
        text.Tokenize(inputText);
        Console.WriteLine(text.ToString());


        /*-----------------------------------------------------------------------------------------*/


        // Создаем объект TextOperations
        TextOperations textOperations = new TextOperations(text);

        // Вывод всех предложений в порядке возрастания количества слов
        Console.WriteLine("\nПредложения в порядке возрастания количества слов:");
        textOperations.PrintSentencesByWordCount();

        // Вывод всех предложений в порядке возрастания длины предложения
        Console.WriteLine("\nПредложения в порядке возрастания длины предложения:");
        textOperations.PrintSentencesByLength();

        // Нахождение уникальных слов заданной длины в вопросительных предложениях
        Console.WriteLine("\nУникальные слова длины 3 в вопросительных предложениях:");
        var uniqueWords = textOperations.FindUniqueWordsInQuestions(3);
        foreach (var uniqueWord in uniqueWords)
        {
            Console.WriteLine(uniqueWord);
        }

        // Удаление из текста всех слов заданной длины, начинающихся с согласной буквы
        Console.WriteLine("\nУдаляем слова длины 4, начинающиеся с согласной буквы:");
        textOperations.RemoveWordsStartingWithConsonant(4);
        Console.WriteLine(text.ToString());

        // Замена слов заданной длины на указанную подстроку в заданном предложении
        textOperations.ReplaceWordsInSentence(2, "XX", 0); // заменяем слова длины 2 в первом предложении
        Console.WriteLine("\nПосле замены слов длины 2 на 'XX' в первом предложении:");
        Console.WriteLine(text.ToString());

        // Удаление стоп-слов из текста
        Console.WriteLine("\nУдаляем стоп-слова:");
        textOperations.RemoveStopWords();
        Console.WriteLine(text.ToString());

        // Экспорт текста в XML-документ
        string xmlFilePath = "output.xml";
        textOperations.ExportToXml(xmlFilePath);
        Console.WriteLine($"\nТекст экспортирован в XML: {xmlFilePath}");


    }
}