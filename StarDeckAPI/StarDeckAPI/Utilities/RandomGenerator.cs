using System.Text;
using System;

namespace StarDeckAPI.Utilities
{
    public static class RandomGenerator
    {
        public static int[] GenerateRandomArray(int size, int min, int max)
        {
            if (size > max - min + 1)
            {
                throw new ArgumentException("El rango no es suficiente para generar un array sin repeticiones.");
            }

            Random random = new Random();
            int[] array = new int[size];
            bool[] used = new bool[max - min + 1];

            for (int i = 0; i < size; i++)
            {
                int randomNumber = random.Next(min, max + 1);

                while (used[randomNumber - min])
                {
                    randomNumber = random.Next(min, max + 1);
                }

                array[i] = randomNumber;
                used[randomNumber - min] = true;
            }

            return array;
        }

        public static void ShuffleList<T>(List<T> list)
        {
            Random random = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void OrdenarArray(int[] array)
        {
            int n = array.Length;

            for (int i = 0; i < n - 1; i++)
            {
                int maxIndex = i;

                for (int j = i + 1; j < n; j++)
                {
                    if (array[j] > array[maxIndex])
                    {
                        maxIndex = j;
                    }
                }

                int temp = array[maxIndex];
                array[maxIndex] = array[i];
                array[i] = temp;
            }
        }
    }
}
