using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KPO_LAB_3
{
    // Класс Word
    public class Word
    {
        public string Text { get; set; } // get set для экономии времени

        public Word() { } // Пустой конструктор для сериализации

        public Word(string text) // Конструктор
        {
            Text = text;
        }

        public int GetLength()
        {
            return Text.Length;
        }

        public bool StartsWithCapital()
        {
            if (string.IsNullOrEmpty(Text)) return false;
            return char.IsUpper(Text[0]);
        }

        public bool ContainsSubstring(string substring)
        {
            if (string.IsNullOrEmpty(substring)) return false;
            return Text.Contains(substring);
        }

        public string ToUpperCase()
        {
            return Text.ToUpper();
        }

        public string ToLowerCase()
        {
            return Text.ToLower();
        }

        public bool IsNumber()
        {
            return double.TryParse(Text, out _);
        }

        public override string ToString()
        {
            return Text;
        }
    }

    // Класс Punctuation
    public class Punctuation
    {
        public char Symbol { get; set; }

        public Punctuation() { } // Пустой конструктор для сериализации

        public Punctuation(char symbol)
        {
            Symbol = symbol;
        }

        public bool IsPunctuationMark()
        {
            return char.IsPunctuation(Symbol);
        }

        public override string ToString()
        {
            return Symbol.ToString();
        }
    }

    // Класс Sentence
    public class Sentence
    {
        public List<Word> Words { get; set; } // Свойство должно иметь публичный сеттер
        public List<Punctuation> Punctuations { get; set; } // Свойство должно иметь публичный сеттер

        public Sentence()
        {
            Words = new List<Word>();
            Punctuations = new List<Punctuation>();
        }

        public void AddWord(Word word)
        {
            Words.Add(word);
        }

        public void AddPunctuation(Punctuation punctuation)
        {
            Punctuations.Add(punctuation);
        }

        public override string ToString()
        {
            var sentence = string.Join(" ", Words);
            if (Punctuations.Count > 0)
            {
                sentence += " " + string.Join("", Punctuations);
            }
            return sentence.Trim();
        }
    }

    // Класс Text
    [XmlRoot("Text")] // Укажите, что это корневой элемент XML
    public class Text
    {
        [XmlElement("Sentence")] // Укажите, что это элемент XML
        public List<Sentence> Sentences { get; set; } // Свойство должно иметь публичный сеттер

        public Text()
        {
            Sentences = new List<Sentence>();
        }

        public void AddSentence(Sentence sentence)
        {
            Sentences.Add(sentence);
        }

        public void Tokenize(string inputText)
        {
            string[] sentenceStrings = Regex.Split(inputText, @"(?<=[.!?])\s+");

            foreach (var sentenceString in sentenceStrings)
            {
                Sentence sentence = new Sentence();

                string[] tokens = Regex.Split(sentenceString, @"(\W+)");
                foreach (var token in tokens)
                {
                    string trimmedToken = token.Trim();
                    if (string.IsNullOrEmpty(trimmedToken)) continue;

                    if (char.IsLetter(trimmedToken[0]))
                    {
                        sentence.AddWord(new Word(trimmedToken));
                    }
                    else if (trimmedToken.Length == 1)
                    {
                        sentence.AddPunctuation(new Punctuation(trimmedToken[0]));
                    }
                }

                AddSentence(sentence);
            }
        }

        public override string ToString()
        {
            return string.Join(" ", Sentences);
        }
    }
}
