﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSLab_1
{
    static class myCountClass
    {
        static Dictionary<char, int> symbolsList = new Dictionary<char, int>();
        static char Symbol;
        public static void SymbolsCount(string filetext)
        {
            symbolsList.Clear();
            foreach (var n in filetext)
            {
                Symbol = char.ToLower(n);
                if (symbolsList.ContainsKey(Symbol))
                    symbolsList[Symbol]++;
                else
                    symbolsList.Add(Symbol, 1);
            }
        }
        public static Dictionary<char, double> Probability(Dictionary<char, double> symbols, int size)
        {
            foreach (var item in symbolsList)
            {
                symbols.Add(item.Key, ((double)item.Value / size));
            }
            return symbols;
        }
        public static double Entrophy(Dictionary<char, double> symbols)
        {
            double entrophy = 0;
            foreach (var item in symbols)
            {
                entrophy += (item.Value * Math.Log(item.Value, 2));
            }
            return (entrophy * -1);
        }

    }
    class Program
    {
        static string Read(string path) //Метод зчитує текст з файлу

        {
            string filetext;
            using (StreamReader sr = new StreamReader(path))
            {
                filetext = sr.ReadToEnd();
               
            }
            return filetext;
        }


        static double AmountOfInformation(int amount, double entropy) //Метод підраховує значення кількості інформації в тексті
        {
            return amount * entropy; ;
        }

        static void DictionaryOut(Dictionary<char, double> symbols)
        {
            foreach (var item in symbols)
            {
                Console.WriteLine(item.Key + "\t" + item.Value);
            }
        }
        static void AmountOut(double amount, string path)
        {
            FileInfo file = new FileInfo(path);
            double size = file.Length;
            Console.WriteLine("Кількість інформації в тексті - {0} біт\nЗагальний розмір файлу - {1} біт\nРозмір файлу в - {2} рази більший за кількість інформації", amount, size * 8, size * 8 / amount);
        }
      
        static void Main(string[] args)
        {
            Dictionary<char, double> first = new Dictionary<char, double>();
            Dictionary<char, double> second = new Dictionary<char, double>();
            Dictionary<char, double> third = new Dictionary<char, double>();
            string path1file = "/Users/oleksandrkurta/Университет/3.2/Комп'ютерні системи/Лабораторки/Лабораторна№1/File1/file1.txt";
            string path2file = "/Users/oleksandrkurta/Университет/3.2/Комп'ютерні системи/Лабораторки/Лабораторна№1/File2/file2.txt";
            string path3file = "/Users/oleksandrkurta/Университет/3.2/Комп'ютерні системи/Лабораторки/Лабораторна№1/File3/file3.txt";
            Console.WriteLine("Перший файл");
            myCountClass.SymbolsCount(Read(path1file));
            myCountClass.Probability(first, Read(path1file).Length);
            DictionaryOut(first);
            Console.WriteLine($"Ентропія - {myCountClass.Entrophy(first)},");
            AmountOut(AmountOfInformation(Read(path1file).Length, myCountClass.Entrophy(first)), path1file);
            Console.WriteLine("Другий файл");
            myCountClass.SymbolsCount(Read(path2file));
            myCountClass.Probability(second, Read(path2file).Length);
            DictionaryOut(second);
            Console.WriteLine($"Ентропія - {myCountClass.Entrophy(second)}");
            AmountOut(AmountOfInformation(Read(path2file).Length, myCountClass.Entrophy(second)), path2file);
            Console.WriteLine("Третій файл");
            myCountClass.SymbolsCount(Read(path3file));
            myCountClass.Probability(third, Read(path3file).Length);
            DictionaryOut(third);
            Console.WriteLine($"Ентропія - {myCountClass.Entrophy(third)}");
            AmountOut(AmountOfInformation(Read(path3file).Length, myCountClass.Entrophy(third)), path3file);







        }
    }
}
