using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace upRead
{

    class Program
    {
        static readonly char[] base64Table = {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O',
                                              'P','Q','R','S','T','U','V','W','X','Y','Z','a','b','c','d',
                                              'e','f','g','h','i','j','k','l','m','n','o','p','q','r','s',
                                              't','u','v','w','x','y','z','0','1','2','3','4','5','6','7',
                                              '8','9','+','/','=' };
        static void Base64Convert(string path)
        {
            string text;
            using (StreamReader sr = new StreamReader(path))
            {
                text = sr.ReadToEnd();
            }
            byte[] textBytes = Encoding.UTF8.GetBytes(text);

            //string binaryTextBytes1 = textBytes.Select(x => Convert.ToString(x, 2).PadLeft(8, '0'));
            string binaryTextBytes = string.Join("", textBytes.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));
            int countOfBytes = binaryTextBytes.Count();
            string append = countOfBytes % 3 == 2 ? "==" : countOfBytes % 3 == 1 ? "=" : "";

            int remOfDivision = countOfBytes % 6;
            if (remOfDivision != 0)
                for (int i = 0; i < 6 - remOfDivision; i++)
                {
                    binaryTextBytes = binaryTextBytes.Insert(countOfBytes, "0");
                    countOfBytes++;
                }

            List<string> newList = Enumerable.Range(0, countOfBytes / 6).Select(x => binaryTextBytes.Substring(x * 6, 6)).ToList();
            string myEnText = string.Join("", newList.Select(x => base64Table[Convert.ToByte(x, 2)])) + append;
            Console.WriteLine("Текст закодований методом:\n" + myEnText + "\n");
        }
        static void Base64(string path)
        {
            string text;
            using (StreamReader sr = new StreamReader(path))
            {
                text = sr.ReadToEnd();
            }
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            string enText = Convert.ToBase64String(textBytes);
            using (StreamWriter sw = new StreamWriter(path.Replace(".tar.bz2", "_base64.tar.bz2"), false))
            {
                sw.WriteLine(enText);
            }
            Console.WriteLine("Текст закодований задопомогою встроєної бібліотеки:\n" + enText + "\n\n\n");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Перший файл");
            Base64Convert(@"/Users/oleksandrkurta/Университет/3.2/Комп'ютерні системи/Лабораторки/Лабораторна№1/File1/file1.tar.bz2");
            Base64(@"/Users/oleksandrkurta/Университет/3.2/Комп'ютерні системи/Лабораторки/Лабораторна№1/File1/file1.tar.bz2");
            Console.WriteLine("Другий файл");
            Base64Convert(@"/Users/oleksandrkurta/Университет/3.2/Комп'ютерні системи/Лабораторки/Лабораторна№1/File2/file2.tar.bz2");
            Base64(@"/Users/oleksandrkurta/Университет/3.2/Комп'ютерні системи/Лабораторки/Лабораторна№1/File2/file2.tar.bz2");
            Console.WriteLine("Третій файл");
            Base64Convert(@"/Users/oleksandrkurta/Университет/3.2/Комп'ютерні системи/Лабораторки/Лабораторна№1/File3/file3.tar.bz2");
            Base64(@"/Users/oleksandrkurta/Университет/3.2/Комп'ютерні системи/Лабораторки/Лабораторна№1/File3/file3.tar.bz2");
        }
    }
}