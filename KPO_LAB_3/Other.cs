using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace KPO_LAB_3
{
    public class TextOperations
    {
        private Text _text; // ������ ������ Text, ���������� ��������� �����������
        private List<string> stopWords; // ������ ����-����

        // �����������, ���������������� ������ Text � ������ ����-����
        public TextOperations(Text text)
        {
            _text = text;
            stopWords = new List<string> { "�", "�", "��", "��", "�" }; // ������ ����-����
        }

        // ����� ��� ������ ����������� � ������� ����������� ���������� ����
        public void PrintSentencesByWordCount()
        {
            var sortedSentences = _text.Sentences.OrderBy(sentence => sentence.Words.Count).ToList();
            foreach (var sentence in sortedSentences)
            {
                Console.WriteLine(sentence.ToString());
            }
        }

        // ����� ��� ������ ����������� � ������� ����������� ����� �����������
        public void PrintSentencesByLength()
        {
            var sortedSentences = _text.Sentences.OrderBy(sentence => sentence.ToString().Length).ToList();
            foreach (var sentence in sortedSentences)
            {
                Console.WriteLine(sentence.ToString());
            }
        }

        // ����� ��� ���������� ���������� ���� �������� ����� � �������������� ������������
        public IEnumerable<string> FindUniqueWordsInQuestions(int length)
        {
            var uniqueWords = new HashSet<string>();
            foreach (var sentence in _text.Sentences)
            {
                // ���������, �������� �� ����������� ��������������
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

        // ����� ��� �������� ���� �������� �����, ������������ � ��������� �����
        public void RemoveWordsStartingWithConsonant(int length)
        {
            foreach (var sentence in _text.Sentences)
            {
                sentence.Words.RemoveAll(word => word.GetLength() == length &&
                                                  !"aeiouAEIOU".Contains(word.Text[0]));
            }
        }

        // ����� ��� ������ ���� �������� ����� �� ��������� ��������� � �������� �����������
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

        // ����� ��� �������� ����-���� �� ������
        public void RemoveStopWords()
        {
            foreach (var sentence in _text.Sentences)
            {
                sentence.Words.RemoveAll(word => stopWords.Contains(word.Text.ToLower()));
            }
        }

        // ����� ��� �������� ������ � XML
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
