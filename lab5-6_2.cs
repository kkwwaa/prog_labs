using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        int[,] dynamicArray = null; // Двумерный массив
        int[][] jaggedArray = null; // Рваный массив
        string inputString = null;  // Строка символов
        int choice;

        do
        {
            // Печать меню
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Сформировать и вывести двумерный массив");
            Console.WriteLine("2. Добавить столбец в двумерный массив");
            Console.WriteLine("3. Сформировать и вывести рваный массив");
            Console.WriteLine("4. Удалить самую короткую строку из рваного массива");
            Console.WriteLine("5. Ввести строку символов");
            Console.WriteLine("6. Обработать строку символов");
            Console.WriteLine("7. Выход");
            Console.Write("Выберите пункт меню: ");
            choice = Convert(Console.ReadLine(), 1, 7);

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Выберите способ формирования массива:");
                    Console.WriteLine("1. Случайные числа");
                    Console.WriteLine("2. Ввод с клавиатуры");
                    int subChoice = Convert(Console.ReadLine(), 1, 2);

                    dynamicArray = CreateAndPrint2DArray(subChoice);
                    break;

                case 2:
                    if (dynamicArray != null)
                    {
                        Console.WriteLine("Выберите способ формирования столбца:");
                        Console.WriteLine("1. Случайные числа");
                        Console.WriteLine("2. Ввод с клавиатуры");
                        subChoice = Convert(Console.ReadLine(), 1, 2);
                        dynamicArray = AddColumnTo2DArray(dynamicArray, subChoice);
                    }
                    else
                        Console.WriteLine("Сначала создайте двумерный массив (пункт 1).");
                    break;

                case 3:
                    Console.WriteLine("Выберите способ формирования рваного массива:");
                    Console.WriteLine("1. Случайные числа");
                    Console.WriteLine("2. Ввод с клавиатуры");
                    subChoice = Convert(Console.ReadLine(), 1, 2);

                    jaggedArray = CreateAndPrintJaggedArray(subChoice);
                    break;

                case 4:
                    if (jaggedArray != null && jaggedArray.Length!=0)
                        jaggedArray = RemoveShortestRow(jaggedArray);
                    else
                        Console.WriteLine("Сначала создайте рваный массив (пункт 3).");
                    break;

                case 5:
                    inputString = EnterString();
                    break;

                case 6:
                    if (!string.IsNullOrEmpty(inputString))
                    {
                        string processed = ProcessString(inputString);
                        Console.WriteLine("Результат обработки строки:");
                        Console.WriteLine(processed);
                    }
                    else
                        Console.WriteLine("Сначала введите строку (пункт 5).");
                    break;

                case 7:
                    Console.WriteLine("Выход из программы.");
                    break;

                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }

        } while (choice != 7);
    }

    // Проверка ввода
    public static int Convert(string input, int l, int r)
    {
        int n;
        bool isConvert = Int32.TryParse(input, out n) && r >= n && n >= l;
        while (isConvert == false)
        {
            Console.WriteLine("Попробуйте еще раз");
            input = Console.ReadLine();
            isConvert = Int32.TryParse(input, out n) && r >= n && n >= l;
        }
        return n;
    }

    // 1. Сформировать и вывести двумерный массив
    static int[,] CreateAndPrint2DArray(int method)
    {
        Console.Write("Введите количество строк: ");
        int rows = Convert(Console.ReadLine(), 1, 100);
        Console.Write("Введите количество столбцов: ");
        int cols = Convert(Console.ReadLine(), 1, 100);

        int[,] array = new int[rows, cols];
        if (method == 1) // Случайные числа
        {
            Random random = new Random();
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    array[i, j] = random.Next(1, 101);
        }
        else // Ввод с клавиатуры
        {

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    Console.WriteLine("Введите элемент массива:");
                    array[i, j] = Convert(Console.ReadLine(), -1000, 1000);
                }
        }

        Console.WriteLine("Двумерный массив:");
        PrintArray(array);
        return array;
    }

    // Печать двумерного массива
    static void PrintArray(int[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
                Console.Write(array[i, j] + " ");
            Console.WriteLine();
        }
    }

    // 2. Добавить столбец с заданным номером
    static int[,] AddColumnTo2DArray(int[,] array, int method)
    {
        Console.Write("Введите номер нового столбца (от 1 до " + (array.GetLength(1)+1) + "): ");
        int colIndex = Convert(Console.ReadLine(), 1, array.GetLength(1)+1)-1;

        int[,] newArray = new int[array.GetLength(0), array.GetLength(1) + 1];

        if (method == 1) // Случайные числа
        {
            Random random = new Random();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0, k = 0; j < array.GetLength(1) + 1; j++, k++)
                {
                    if (j == colIndex)
                    {
                        newArray[i, j] = random.Next(1, 101);
                        k--;
                    }
                    else
                        newArray[i, j] = array[i, k];
                }
            }
        }
        else
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0, k = 0; j < array.GetLength(1) + 1; j++, k++)
                {
                    if (j == colIndex)
                    {
                        Console.Write($"Введите значение для строки {i + 1}, столбца {colIndex + 1}: ");
                        newArray[i, j] = Convert(Console.ReadLine(), -1000, 1000);
                        k--;
                    }
                    else
                        newArray[i, j] = array[i, k];
                }
            }
        }

        Console.WriteLine("Обновленный массив:");
        PrintArray(newArray);
        return newArray;
    }

    // 3. Сформировать и вывести рваный массив
    static int[][] CreateAndPrintJaggedArray(int method)
    {
        Console.Write("Введите количество строк: ");
        int rows = Convert(Console.ReadLine(), 1, 100);
        int[][] jaggedArray = new int[rows][];

        for (int i = 0; i < rows; i++)
        {
            Console.Write($"Введите количество элементов в строке {i + 1}: ");
            int cols = Convert(Console.ReadLine(), 1, 100);
            jaggedArray[i] = new int[cols];

            if (method == 1) // Случайные числа
            {
                Random random = new Random();
                for (int j = 0; j < cols; j++)
                    jaggedArray[i][j] = random.Next(1, 101);
            }
            else // Ввод с клавиатуры
            {
                Console.WriteLine($"Введите элементы строки {i + 1}:");
                for (int j = 0; j < cols; j++)
                    jaggedArray[i][j] = Convert(Console.ReadLine(), -1000, 1000);
            }
        }

        Console.WriteLine("Рваный массив:");
        PrintArray(jaggedArray);
        return jaggedArray;
    }

    // Печать рваного массива
    static void PrintArray(int[][] array)
    {
        foreach (var row in array)
        {
            foreach (var val in row)
                Console.Write(val + " ");
            Console.WriteLine();
        }
    }

    // 4. Удалить самую короткую строку из рваного массива
    static int[][] RemoveShortestRow(int[][] jaggedArray)
    {
        int minLength = int.MaxValue, minIndex = -1;
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            if (jaggedArray[i].Length < minLength)
            {
                minLength = jaggedArray[i].Length;
                minIndex = i;
            }
        }

        var newJaggedArray = new int[jaggedArray.Length - 1][];
        for (int i = 0, k = 0; i < jaggedArray.Length; i++)
        {
            if (i != minIndex)
                newJaggedArray[k++] = jaggedArray[i];
        }

        Console.WriteLine("Обновленный рваный массив:");
        PrintArray(newJaggedArray);
        return newJaggedArray;
    }

    // 5. Ввести строку символов
    static string EnterString()
    {
        Console.Write("Введите строку символов: ");
        string input = Console.ReadLine();

        // Проверка: строка пуста
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Ошибка: строка пуста. Введите строку с символами.");
            return EnterString(); // Повторный ввод
        }

        string validPunctuation = ",;:.!?";
        if (input.Any(c => char.IsPunctuation(c) && !validPunctuation.Contains(c)))
        {
            Console.WriteLine("Ошибка: строка содержит недопустимые знаки препинания. Допустимые знаки: , ; : . ! ?");
            return EnterString(); // Повторный ввод
        }

        // Проверка: строка оканчивается без знаков препинания
        char lastChar = input.Trim().Last();
        if (!".!?".Contains(lastChar))
        {
            Console.WriteLine("Предупреждение: строка некорректно завершена. Добавьте завершающий знак препинания.");
            return EnterString();
        }

        //Ошибочная постановка знака препинания
        if (char.IsPunctuation(input[0]))
        {
            Console.WriteLine("Ошибка: ошибочная постановка знака препинания. Введите корректную строку.");
            return EnterString(); // Повторный ввод
        }
        for (int i = 1; i < input.Length; i++)
        {
            if (char.IsPunctuation(input[i]) && ((char.IsLetter(input[i - 1]) == false) && (char.IsNumber(input[i - 1]) == false)))
            {
                Console.WriteLine("Ошибка: ошибочная постановка знака препинания. Введите корректную строку.");
                return EnterString(); // Повторный ввод
            }
        }

        return input;
    }


    // 6. Обработать строку символов
    static string ProcessString(string inputString)
    {
        // Используем регулярные выражения для извлечения всех слов
        string pattern = @"\b\w+\b";
        MatchCollection matches = Regex.Matches(inputString, pattern);

        // Преобразуем все найденные слова в массив
        string[] words = new string[matches.Count];
        for (int i = 0; i < matches.Count; i++)
        {
            words[i] = matches[i].Value;
        }

        // Переворачиваем четные слова
        for (int i = 0; i < words.Length; i++)
        {
            if ((i + 1) % 2 == 0)
            {
                words[i] = ReverseString(words[i]);
            }
        }

        // Восстанавливаем строку
        string result = RebuildStringWithSeparators(inputString, words);

        return result;
    }

    // Метод для переворота строки
    public static string ReverseString(string word)
    {
        char[] charArray = word.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    // Метод для слияния слов и исходных разделителей
    public static string RebuildStringWithSeparators(string inputString, string[] processedWords)
    {
        StringBuilder result = new StringBuilder();
        int wordIndex = 0;
        bool inWord = false;

        for (int i = 0; i < inputString.Length; i++)
        {
            char currentChar = inputString[i];

            if (char.IsLetterOrDigit(currentChar))
            {
                if (!inWord) // Начало нового слова
                {
                    result.Append(processedWords[wordIndex]);
                    wordIndex++;
                    inWord = true;
                }
            }
            else
            {
                result.Append(currentChar); // Добавляем разделитель
                inWord = false;
            }
        }

        return result.ToString();
    }
}using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        int[,] dynamicArray = null; // Двумерный массив
        int[][] jaggedArray = null; // Рваный массив
        string inputString = null;  // Строка символов
        int choice;

        do
        {
            // Печать меню
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Сформировать и вывести двумерный массив");
            Console.WriteLine("2. Добавить столбец в двумерный массив");
            Console.WriteLine("3. Сформировать и вывести рваный массив");
            Console.WriteLine("4. Удалить самую короткую строку из рваного массива");
            Console.WriteLine("5. Ввести строку символов");
            Console.WriteLine("6. Обработать строку символов");
            Console.WriteLine("7. Выход");
            Console.Write("Выберите пункт меню: ");
            choice = Convert(Console.ReadLine(), 1, 7);

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Выберите способ формирования массива:");
                    Console.WriteLine("1. Случайные числа");
                    Console.WriteLine("2. Ввод с клавиатуры");
                    int subChoice = Convert(Console.ReadLine(), 1, 2);

                    dynamicArray = CreateAndPrint2DArray(subChoice);
                    break;

                case 2:
                    if (dynamicArray != null)
                    {
                        Console.WriteLine("Выберите способ формирования столбца:");
                        Console.WriteLine("1. Случайные числа");
                        Console.WriteLine("2. Ввод с клавиатуры");
                        subChoice = Convert(Console.ReadLine(), 1, 2);
                        dynamicArray = AddColumnTo2DArray(dynamicArray, subChoice);
                    }
                    else
                        Console.WriteLine("Сначала создайте двумерный массив (пункт 1).");
                    break;

                case 3:
                    Console.WriteLine("Выберите способ формирования рваного массива:");
                    Console.WriteLine("1. Случайные числа");
                    Console.WriteLine("2. Ввод с клавиатуры");
                    subChoice = Convert(Console.ReadLine(), 1, 2);

                    jaggedArray = CreateAndPrintJaggedArray(subChoice);
                    break;

                case 4:
                    if (jaggedArray != null && jaggedArray.Length!=0)
                        jaggedArray = RemoveShortestRow(jaggedArray);
                    else
                        Console.WriteLine("Сначала создайте рваный массив (пункт 3).");
                    break;

                case 5:
                    inputString = EnterString();
                    break;

                case 6:
                    if (!string.IsNullOrEmpty(inputString))
                    {
                        string processed = ProcessString(inputString);
                        Console.WriteLine("Результат обработки строки:");
                        Console.WriteLine(processed);
                    }
                    else
                        Console.WriteLine("Сначала введите строку (пункт 5).");
                    break;

                case 7:
                    Console.WriteLine("Выход из программы.");
                    break;

                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }

        } while (choice != 7);
    }

    // Проверка ввода
    public static int Convert(string input, int l, int r)
    {
        int n;
        bool isConvert = Int32.TryParse(input, out n) && r >= n && n >= l;
        while (isConvert == false)
        {
            Console.WriteLine("Попробуйте еще раз");
            input = Console.ReadLine();
            isConvert = Int32.TryParse(input, out n) && r >= n && n >= l;
        }
        return n;
    }

    // 1. Сформировать и вывести двумерный массив
    static int[,] CreateAndPrint2DArray(int method)
    {
        Console.Write("Введите количество строк: ");
        int rows = Convert(Console.ReadLine(), 1, 100);
        Console.Write("Введите количество столбцов: ");
        int cols = Convert(Console.ReadLine(), 1, 100);

        int[,] array = new int[rows, cols];
        if (method == 1) // Случайные числа
        {
            Random random = new Random();
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    array[i, j] = random.Next(1, 101);
        }
        else // Ввод с клавиатуры
        {

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    Console.WriteLine("Введите элемент массива:");
                    array[i, j] = Convert(Console.ReadLine(), -1000, 1000);
                }
        }

        Console.WriteLine("Двумерный массив:");
        PrintArray(array);
        return array;
    }

    // Печать двумерного массива
    static void PrintArray(int[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
                Console.Write(array[i, j] + " ");
            Console.WriteLine();
        }
    }

    // 2. Добавить столбец с заданным номером
    static int[,] AddColumnTo2DArray(int[,] array, int method)
    {
        Console.Write("Введите номер нового столбца (от 1 до " + (array.GetLength(1)+1) + "): ");
        int colIndex = Convert(Console.ReadLine(), 1, array.GetLength(1)+1)-1;

        int[,] newArray = new int[array.GetLength(0), array.GetLength(1) + 1];

        if (method == 1) // Случайные числа
        {
            Random random = new Random();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0, k = 0; j < array.GetLength(1) + 1; j++, k++)
                {
                    if (j == colIndex)
                    {
                        newArray[i, j] = random.Next(1, 101);
                        k--;
                    }
                    else
                        newArray[i, j] = array[i, k];
                }
            }
        }
        else
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0, k = 0; j < array.GetLength(1) + 1; j++, k++)
                {
                    if (j == colIndex)
                    {
                        Console.Write($"Введите значение для строки {i + 1}, столбца {colIndex + 1}: ");
                        newArray[i, j] = Convert(Console.ReadLine(), -1000, 1000);
                        k--;
                    }
                    else
                        newArray[i, j] = array[i, k];
                }
            }
        }

        Console.WriteLine("Обновленный массив:");
        PrintArray(newArray);
        return newArray;
    }

    // 3. Сформировать и вывести рваный массив
    static int[][] CreateAndPrintJaggedArray(int method)
    {
        Console.Write("Введите количество строк: ");
        int rows = Convert(Console.ReadLine(), 1, 100);
        int[][] jaggedArray = new int[rows][];

        for (int i = 0; i < rows; i++)
        {
            Console.Write($"Введите количество элементов в строке {i + 1}: ");
            int cols = Convert(Console.ReadLine(), 1, 100);
            jaggedArray[i] = new int[cols];

            if (method == 1) // Случайные числа
            {
                Random random = new Random();
                for (int j = 0; j < cols; j++)
                    jaggedArray[i][j] = random.Next(1, 101);
            }
            else // Ввод с клавиатуры
            {
                Console.WriteLine($"Введите элементы строки {i + 1}:");
                for (int j = 0; j < cols; j++)
                    jaggedArray[i][j] = Convert(Console.ReadLine(), -1000, 1000);
            }
        }

        Console.WriteLine("Рваный массив:");
        PrintArray(jaggedArray);
        return jaggedArray;
    }

    // Печать рваного массива
    static void PrintArray(int[][] array)
    {
        foreach (var row in array)
        {
            foreach (var val in row)
                Console.Write(val + " ");
            Console.WriteLine();
        }
    }

    // 4. Удалить самую короткую строку из рваного массива
    static int[][] RemoveShortestRow(int[][] jaggedArray)
    {
        int minLength = int.MaxValue, minIndex = -1;
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            if (jaggedArray[i].Length < minLength)
            {
                minLength = jaggedArray[i].Length;
                minIndex = i;
            }
        }

        var newJaggedArray = new int[jaggedArray.Length - 1][];
        for (int i = 0, k = 0; i < jaggedArray.Length; i++)
        {
            if (i != minIndex)
                newJaggedArray[k++] = jaggedArray[i];
        }

        Console.WriteLine("Обновленный рваный массив:");
        PrintArray(newJaggedArray);
        return newJaggedArray;
    }

    // 5. Ввести строку символов
    static string EnterString()
    {
        Console.Write("Введите строку символов: ");
        string input = Console.ReadLine();

        // Проверка: строка пуста
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Ошибка: строка пуста. Введите строку с символами.");
            return EnterString(); // Повторный ввод
        }

        string validPunctuation = ",;:.!?";
        if (input.Any(c => char.IsPunctuation(c) && !validPunctuation.Contains(c)))
        {
            Console.WriteLine("Ошибка: строка содержит недопустимые знаки препинания. Допустимые знаки: , ; : . ! ?");
            return EnterString(); // Повторный ввод
        }

        // Проверка: строка оканчивается без знаков препинания
        char lastChar = input.Trim().Last();
        if (!".!?".Contains(lastChar))
        {
            Console.WriteLine("Предупреждение: строка некорректно завершена. Добавьте завершающий знак препинания.");
            return EnterString();
        }

        //Ошибочная постановка знака препинания
        if (char.IsPunctuation(input[0]))
        {
            Console.WriteLine("Ошибка: ошибочная постановка знака препинания. Введите корректную строку.");
            return EnterString(); // Повторный ввод
        }
        for (int i = 1; i < input.Length; i++)
        {
            if (char.IsPunctuation(input[i]) && ((char.IsLetter(input[i - 1]) == false) && (char.IsNumber(input[i - 1]) == false)))
            {
                Console.WriteLine("Ошибка: ошибочная постановка знака препинания. Введите корректную строку.");
                return EnterString(); // Повторный ввод
            }
        }

        return input;
    }


    // 6. Обработать строку символов
    static string ProcessString(string inputString)
    {
        // Используем регулярные выражения для извлечения всех слов
        string pattern = @"\b\w+\b";
        MatchCollection matches = Regex.Matches(inputString, pattern);

        // Преобразуем все найденные слова в массив
        string[] words = new string[matches.Count];
        for (int i = 0; i < matches.Count; i++)
        {
            words[i] = matches[i].Value;
        }

        // Переворачиваем четные слова
        for (int i = 0; i < words.Length; i++)
        {
            if ((i + 1) % 2 == 0)
            {
                words[i] = ReverseString(words[i]);
            }
        }

        // Восстанавливаем строку
        string result = RebuildStringWithSeparators(inputString, words);

        return result;
    }

    // Метод для переворота строки
    public static string ReverseString(string word)
    {
        char[] charArray = word.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    // Метод для слияния слов и исходных разделителей
    public static string RebuildStringWithSeparators(string inputString, string[] processedWords)
    {
        StringBuilder result = new StringBuilder();
        int wordIndex = 0;
        bool inWord = false;

        for (int i = 0; i < inputString.Length; i++)
        {
            char currentChar = inputString[i];

            if (char.IsLetterOrDigit(currentChar))
            {
                if (!inWord) // Начало нового слова
                {
                    result.Append(processedWords[wordIndex]);
                    wordIndex++;
                    inWord = true;
                }
            }
            else
            {
                result.Append(currentChar); // Добавляем разделитель
                inWord = false;
            }
        }

        return result.ToString();
    }
}
