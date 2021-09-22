using System;
using Heap;

namespace MyExampleProject
{
    class Program
    {
        static void Main()
        {
            var heap = new Heap<string>();

            var testData = new string[]
            {
                "cat",
                "dog",
                "apple",
                "bear",
                "zebra"
            };

            heap.AddRange(testData);

            for (int i = 0; i < testData.Length; i++)
            {
                Console.WriteLine(heap.Extract());
            }

            /*
             Outputs:
             - apple
             - bear
             - cat
             - dog
             - zebra
             */
        }
    }
}
