namespace mib_map
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            int[] numbers = { 3, 7, 2, 9, 5, 10};

            var numbersN = numbers.Where(n => n > 5);
            foreach (var n in numbersN)
            {
                Console.WriteLine(n);
;           }

            int[] numbers2 = { 1, 2, 3, 4 };

            var numbers2N = numbers2.Select(n => n * n);

            foreach (var n in numbers2N)
            {
                Console.WriteLine(n);
            }

            int[] numbers3 = { 1, 2, 3, 4, 5, 6 };

        }
    }
}