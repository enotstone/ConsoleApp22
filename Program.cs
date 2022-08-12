using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp22
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Input value array:");
            int n = Convert.ToInt32(Console.ReadLine());
            //int[] Parray = GetArray(n);
            //for (int i = 0; i < n; i++)
            //{
            //    Console.Write($"{Parray[i]} ");
            //}
            //Console.WriteLine();
            //int SumA = SumArray(Parray);
            //Console.WriteLine(SumA);
            //int MV = MaxMArr(Parray);
            //Console.WriteLine(MV);

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, int> func2 = new Func<Task<int[]>, int>(SumArray);
            Task<int> task2 = task1.ContinueWith<int>(func2);

            Func<Task<int[]>, int> func3 = new Func<Task<int[]>, int>(MaxMArr);
            Task<int> task3 = task1.ContinueWith<int>(func3);

            task1.Start();
            Console.ReadKey();
        }
        static int[] GetArray(object v)
        {
            int n = (int)v;
            int[] array = new int[n];
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next(0, 100);
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();
            return array;
        }
        static int SumArray(Task<int[]> task)
        {
            int[] array = task.Result();
            int Sum = 0;
            for (int i = 0; i < array.Count(); i++)
            {
                Sum += array[i];
            }
            Console.WriteLine(Sum);
            return Sum;
        }
        static int MaxMArr(Task<int[]> task)
        {
            int[] array = task.Result();
            int MaxV = array.Max();
            Console.WriteLine(MaxV);
            return MaxV;
        }
        
    }
}
