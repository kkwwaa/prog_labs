using System;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;

//this

namespace lab4
{
    internal class Program
    {
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

        static void Main(string[] args)
        {
            int[] array = [];
            bool exit = false;
            int n = 0;

            while (!exit)
            {
                #region menu
                Console.WriteLine("Главное меню:");
                Console.WriteLine("1. Сформировать массив");
                Console.WriteLine("2. Распечатать массив");
                Console.WriteLine("3. Удалить минимальный элемент из массива");
                Console.WriteLine("4. Добавить К элементов в конец массива");
                Console.WriteLine("5. Сдвинуть циклически на M элементов вправо");
                Console.WriteLine("6. Поиск первого отрицательного элемента в массиве");
                Console.WriteLine("7. Выполнить сортировку простым выбором для массива");
                Console.WriteLine("8. Бинарный поиск в массиве с использованием быстрой сортировки");
                Console.WriteLine("9. Выход");
                Console.Write("Выберите пункт: ");
                #endregion 

                int choice = Convert(Console.ReadLine(), 1, 9);

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Выберите способ формирования массива:");
                        Console.WriteLine("1. Случайные числа");
                        Console.WriteLine("2. Ввод с клавиатуры");
                        int subChoice = int.Parse(Console.ReadLine());

                        switch (subChoice)
                        {
                            case 1:
                                Console.Write("Введите размер массива: ");
                                n = Convert(Console.ReadLine(), 0, int.MaxValue);
                                array = new int[n];
                                Random rand = new Random();
                                for (int i = 0; i < n; i++)
                                    array[i] = rand.Next(-100, 100);
                                break;

                            case 2:
                                Console.Write("Введите размер массива: ");
                                n = Convert(Console.ReadLine(), 0, int.MaxValue);
                                array = new int[n];
                                for (int i = 0; i < n; i++)
                                {
                                    Console.WriteLine($"Введите {i+1} элемент массива:");
                                    array[i] = Convert(Console.ReadLine(), int.MinValue, int.MaxValue);
                                }
                                break;

                            default:
                                Console.WriteLine("Неверный выбор");
                                break;
                        }
                        break;

                    case 2:
                        if (array.Length != 0)
                        {
                            Console.WriteLine("Элементы массива:");
                            foreach (var item in array)
                                Console.Write(item + " ");
                            Console.WriteLine();
                        }
                        else
                            Console.WriteLine("Массив не задан.");
                        break;

                    case 3:
                        if (array.Length!=0)
                        {
                            int mn = int.MaxValue, ind_min = 0;
                            n = array.Length;
                            for (int i = 0; i < n; i++)
                            {
                                if (array[i] <= mn)
                                {
                                    mn = array[i];
                                    ind_min = i;
                                }
                            }

                            int[] array_del = new int[n - 1];
                            for (int i = 0; i < n; i++)
                            {
                                if (i < ind_min) array_del[i] = array[i];
                                else if (i > ind_min) array_del[i - 1] = array[i];
                            }
                            
                            array = array_del;
                            Console.WriteLine("Удаление прошло успешно");
                        }
                        else
                            Console.WriteLine("Массив не задан.");
                        break;

                    case 4:
                        if (true)
                        {
                            n = array.Length;
                            Console.WriteLine("Введите число K");
                            int k = Convert(Console.ReadLine(), 0, int.MaxValue);
                            int[] arr_add = new int[n + k];

                            Console.WriteLine("1. Случайные числа");
                            Console.WriteLine("2. Ввод с клавиатуры");
                            subChoice = int.Parse(Console.ReadLine());

                            switch (subChoice)
                            {
                                case 2:
                                    for (int i = 0; i < n + k; i++)
                                    {
                                        if (i < n) arr_add[i] = array[i];
                                        else
                                        {
                                            Console.WriteLine($"Введите элемент для добавления в массив:");
                                            arr_add[i] = Convert(Console.ReadLine(), int.MinValue, int.MaxValue);
                                        }
                                    }
                                    array = arr_add;
                                    Console.WriteLine("Элементы массива были добавлены");
                                    break;

                                case 1:
                                    Random rand = new Random();
                                    for (int i = 0; i < n+k; i++)
                                    {
                                        if (i < n) arr_add[i] = array[i];
                                        else arr_add[i] = rand.Next(-100, 100);
                                    }
                                    array = arr_add;
                                    Console.WriteLine("Элементы массива были добавлены");
                                    break;
                            }
                        }
                        else
                            Console.WriteLine("Массив не задан.");
                        break;

                    case 5:
                        if (array.Length != 0)
                        {
                            n = array.Length;
                            Console.WriteLine("Введите число M");
                            int m = Convert(Console.ReadLine(), 0, int.MaxValue);
                            m %= n;
                            int[] arr_sd = new int[n];

                            for (int i = 0; i < n; i++)
                            {
                                arr_sd[i] = array[(n+i-m)%n];
                            }
                            
                            array = arr_sd;
                            Console.WriteLine("Элементы массива сдвинуты");
                        }
                        else
                            Console.WriteLine("Массив не задан.");
                        break;

                    case 6:
                        int cnt = 0;
                        if (array.Length != 0)
                        {
                            n = array.Length;
                            for (int i = 0; i < n; i++) {
                                if (array[i] < 0)
                                {
                                    Console.WriteLine($"Первый отрицательный элемент в массиве {array[i]} находится на позиции {i + 1}, было произведено {i+1} сравнений");
                                    cnt++;
                                    break;
                                }
                            }
                            if (cnt==0) Console.WriteLine("Отрицательных элементов не обнаружено");
                        }
                        else Console.WriteLine("Массив не задан.");
                        break;

                    case 7:
                        if (array.Length != 0)
                        {
                            n = array.Length;

                            for (int i = 0; i < n - 1; i++)
                            {
                                int minIndex = i;
                                for (int j = i + 1; j < n; j++)
                                {
                                    if (array[j] < array[minIndex])
                                    {
                                        minIndex = j;
                                    }
                                }

                                if (minIndex != i)
                                {
                                    int temp = array[i];
                                    array[i] = array[minIndex];
                                    array[minIndex] = temp;
                                }
                            }
                            Console.WriteLine("Массив отсортирован");
                        }
                        else
                            Console.WriteLine("Массив не задан.");
                        break;

                    case 8:
                        if (array.Length != 0)
                        {
                            n = array.Length;
                            bool IsSorted = true;

                            for (int i = 0; i < n-1; i++)
                            {
                                if (array[i + 1] < array[i])
                                {
                                    IsSorted = false;
                                    break;
                                }
                            }

                            if (IsSorted)
                            {
                                Console.WriteLine("Введите число X");
                                int X = Convert(Console.ReadLine(), int.MinValue, int.MaxValue);

                                int left = 0;
                                int right = n;
                                int mid = (left + right) / 2;
                                int ComparisonCount = 0;
                                bool IsFound = false;
                                while (left <= right)
                                {
                                    mid = (left + right) / 2;
                                    if (array[mid] == X)
                                    {
                                        ComparisonCount++;
                                        IsFound = true;
                                        break;
                                    }
                                    else if (array[mid] < X) left = mid + 1;
                                    else right = mid - 1;
                                    ComparisonCount += 2;
                                }
                                if (IsFound) Console.WriteLine($"Элемент {X} найден, было проведено {ComparisonCount} сравнений");
                                else Console.WriteLine($"Элемент {X} в массиве не найден");

                            }
                            else Console.WriteLine("Массив не отсортирован, пожалуйста, выполните сначала 7 функцию");
                        }
                        else
                            Console.WriteLine("Массив не задан");
                            break;

                    case 9:
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Неверный выбор");
                        break;

                }
                Console.WriteLine();
            }
        }
    }
}
