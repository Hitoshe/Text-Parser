using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace KPO_LAB_3
{
    public class TextOperations
    {
        private Text _text; // Объект класса Text, содержащий коллекцию предложений
        private List<string> stopWords; // Список стоп-слов

        // Конструктор, инициализирующий объект Text и список стоп-слов
        public TextOperations(Text text)
        {
            _text = text;
            stopWords = new List<string> { "и", "в", "не", "на", "с" }; // Пример стоп-слов
        }

        // Метод для вывода предложений в порядке возрастания количества слов
        public void PrintSentencesByWordCount()
        {
            var sortedSentences = _text.Sentences.OrderBy(sentence => sentence.Words.Count).ToList();
            foreach (var sentence in sortedSentences)
            {
                Console.WriteLine(sentence.ToString());
            }
        }

        // Метод для вывода предложений в порядке возрастания длины предложения
        public void PrintSentencesByLength()
        {
            var sortedSentences = _text.Sentences.OrderBy(sentence => sentence.ToString().Length).ToList();
            foreach (var sentence in sortedSentences)
            {
                Console.WriteLine(sentence.ToString());
            }
        }

        // Метод для нахождения уникальных слов заданной длины в вопросительных предложениях
        public IEnumerable<string> FindUniqueWordsInQuestions(int length)
        {
            var uniqueWords = new HashSet<string>();
            foreach (var sentence in _text.Sentences)
            {
                // Проверяем, является ли предложение вопросительным
                if (sentence.ToString().EndsWith("?"))
                {
                    foreach (var word in sentence.Words)
                    {
                        if (word.GetLength() == length)
                        {
                            uniqueWords.Add(word.Text);
                        }
                    }
                }
            }
            return uniqueWords;
        }

        // Метод для удаления слов заданной длины, начинающихся с согласной буквы
        public void RemoveWordsStartingWithConsonant(int length)
        {
            foreach (var sentence in _text.Sentences)
            {
                sentence.Words.RemoveAll(word => word.GetLength() == length &&
                                                  !"aeiouAEIOU".Contains(word.Text[0]));
            }
        }

        // Метод для замены слов заданной длины на указанную подстроку в заданном предложении
        public void ReplaceWordsInSentence(int length, string replacement, int sentenceIndex)
        {
            if (sentenceIndex < 0 || sentenceIndex >= _text.Sentences.Count) return;

            var sentence = _text.Sentences[sentenceIndex];
            for (int i = 0; i < sentence.Words.Count; i++)
            {
                if (sentence.Words[i].GetLength() == length)
                {
                    sentence.Words[i] = new Word(replacement);
                }
            }
        }

        // Метод для удаления стоп-слов из текста
        public void RemoveStopWords()
        {
            foreach (var sentence in _text.Sentences)
            {
                sentence.Words.RemoveAll(word => stopWords.Contains(word.Text.ToLower()));
            }
        }

        // Метод для экспорта текста в XML
        public void ExportToXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Text));
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fs, _text);
            }
        }
    }
}
