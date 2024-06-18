using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace belous25._1
{
    internal class Program
    {
        class MilitaryPersonnel
        {
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string Patronymic { get; set; }
            public string Address { get; set; }
            public string Nationality { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Position { get; set; }
            public string Rank { get; set; }
        }

        static void Main(string[] args)
        {
        c:
            Console.Write("Введите номер задания:");
            int n = int.Parse(Console.ReadLine());
            switch (n)
            {
                case 1:
                    {
                        n1();
                    }
                    goto c;
                case 2:
                    {
                        n2();
                    }
                    goto c;
                case 3:
                    {
                        n3();
                    }
                    goto c;
                default:
                    {
                        Console.WriteLine("Нет такого номер задания.");
                    }
                    goto c;
            }

            void n1()
            {
                string filePath = "soldiers.txt"; // путь к файлу с данными

                try
                {
                    // Чтение всех строк из файла
                    string[] lines = File.ReadAllLines(filePath);

                    // Создание нового файла для записи результатов
                    string outputFilePath = "lieutenants.txt";
                    StreamWriter writer = new StreamWriter(outputFilePath);

                    // Проход по каждой строке и обработка данных
                    foreach (string line in lines)
                    {
                        string[] data = line.Split(';');
                        string rank = data[data.Length - 1].Trim(); // последний элемент - звание

                        if (rank.ToLower() == "лейтенант")
                        {
                            // Запись строки с данными в новый файл
                            writer.WriteLine(line);
                        }
                    }

                    writer.Close();
                    Console.WriteLine("Данные о лейтенантах были успешно записаны в файл: " + outputFilePath);
                }
                catch (IOException e)
                {
                    Console.WriteLine("Ошибка чтения файла: " + e.Message);
                }
                Console.ReadKey();
            }

            void n2()
            {
                string filePath = "numbers.txt"; // путь к файлу с данными

                try
                {
                    // Чтение всех строк из файла
                    string[] lines = File.ReadAllLines(filePath);

                    if (lines.Length == 0)
                    {
                        Console.WriteLine("Файл пуст.");
                        return;
                    }

                    // Преобразование строк в массив целых чисел
                    int[] numbers = new int[lines.Length];
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (!int.TryParse(lines[i], out numbers[i]))
                        {
                            Console.WriteLine($"Ошибка преобразования строки {i + 1} в число.");
                            return;
                        }
                    }

                    // Нахождение количества чётных чисел
                    int evenCount = 0;
                    foreach (int num in numbers)
                    {
                        if (num % 2 == 0)
                        {
                            evenCount++;
                        }
                    }

                    // Нахождение разности первой и последней компоненты
                    int firstNumber = numbers[0];
                    int lastNumber = numbers[numbers.Length - 1];
                    int difference = lastNumber - firstNumber;

                    // Вывод результатов
                    Console.WriteLine($"Количество чётных чисел: {evenCount}");
                    Console.WriteLine($"Разность первой и последней компоненты: {difference}");

                    // Запись результатов в новый файл
                    string outputFilePath = "results.txt";
                    using (StreamWriter writer = new StreamWriter(outputFilePath))
                    {
                        writer.WriteLine($"Количество чётных чисел: {evenCount}");
                        writer.WriteLine($"Разность первой и последней компоненты: {difference}");
                    }

                    Console.WriteLine($"Результаты были сохранены в файл: {outputFilePath}");
                }
                catch (IOException e)
                {
                    Console.WriteLine($"Ошибка чтения файла: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Произошла ошибка: {e.Message}");
                }
            }

            void n3()
            {
                string filePath1 = "file1.txt";
                string filePath2 = "file2.txt";

                // Чтение матриц из файлов
                List<int[,]> matrices1 = ReadMatricesFromFile(filePath1);
                List<int[,]> matrices2 = ReadMatricesFromFile(filePath2);

                // Обмен нечетными матрицами
                int count = Math.Min((matrices1.Count + 1) / 2, (matrices2.Count + 1) / 2);
                for (int i = 0; i < count; i++)
                {
                    int index = 2 * i;
                    int[,] temp = matrices1[index];
                    matrices1[index] = matrices2[index];
                    matrices2[index] = temp;
                }

                // Запись измененных матриц обратно в файлы
                WriteMatricesToFile(filePath1, matrices1);
                WriteMatricesToFile(filePath2, matrices2);

                // Вывод содержимого файлов
                Console.WriteLine("Содержимое первого файла:");
                DisplayMatrices(matrices1);

                Console.WriteLine("\nСодержимое второго файла:");
                DisplayMatrices(matrices2);
            }

            // Функция для чтения матриц из файла
            List<int[,]> ReadMatricesFromFile(string filePath)
            {
                List<int[,]> matrices = new List<int[,]>();
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    int m = 0, s = 0;
                    List<string[]> currentMatrix = new List<string[]>();

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            if (currentMatrix.Count > 0)
                            {
                                m = currentMatrix.Count;
                                s = currentMatrix[0].Length;
                                int[,] matrix = new int[m, s];

                                for (int i = 0; i < m; i++)
                                {
                                    for (int j = 0; j < s; j++)
                                    {
                                        matrix[i, j] = int.Parse(currentMatrix[i][j]);
                                    }
                                }

                                matrices.Add(matrix);
                                currentMatrix.Clear();
                            }
                        }
                        else
                        {
                            currentMatrix.Add(line.Split(' '));
                        }
                    }

                    if (currentMatrix.Count > 0)
                    {
                        m = currentMatrix.Count;
                        s = currentMatrix[0].Length;
                        int[,] matrix = new int[m, s];

                        for (int i = 0; i < m; i++)
                        {
                            for (int j = 0; j < s; j++)
                            {
                                matrix[i, j] = int.Parse(currentMatrix[i][j]);
                            }
                        }

                        matrices.Add(matrix);
                    }
                }

                return matrices;
            }

            // Функция для записи матриц в файл
            void WriteMatricesToFile(string filePath, List<int[,]> matrices)
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var matrix in matrices)
                    {
                        int m = matrix.GetLength(0);
                        int s = matrix.GetLength(1);

                        for (int i = 0; i < m; i++)
                        {
                            for (int j = 0; j < s; j++)
                            {
                                writer.Write(matrix[i, j]);
                                if (j < s - 1) writer.Write(' ');
                            }
                            writer.WriteLine();
                        }
                        writer.WriteLine();
                    }
                }
            }

            // Функция для отображения матриц на экране
            void DisplayMatrices(List<int[,]> matrices)
            {
                foreach (var matrix in matrices)
                {
                    int m = matrix.GetLength(0);
                    int s = matrix.GetLength(1);

                    for (int i = 0; i < m; i++)
                    {
                        for (int j = 0; j < s; j++)
                        {
                            Console.Write(matrix[i, j] + " ");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
