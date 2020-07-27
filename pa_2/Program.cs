using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace pa_2
{


    public class Square
    {
        int width;
        List<Point> points;
        public Square(int width)
        {
            this.width = width;
            this.points = new List<Point>();
        }
        public int Width { get => width; set => width = value; }
        public List<Point> Points { get => points; set => points = value; }
    }

    public class Point
    {
        double x, y;

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
            
        }
        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public bool isInCircle()
        {
            double D = Math.Sqrt(Math.Pow(0 - x, 2) + Math.Pow(0 - y, 2));
            return D <= 1.0;
        }
    }

    class Program
    {
        private static readonly Random getrandom = new Random();

        public static double GetRandomNumber(int points)
        {
            return getrandom.NextDouble();
        }

        public static void GetRandomList(Int64 countOfPoints, ref List<Int64> inCircle)
        {
            Random random = new Random();
            Int64 inCircleCount = 0;
            for (Int64 i = 0; i < countOfPoints; i++)
            {
                Point newRandom = new Point(
                    random.NextDouble(),
                    random.NextDouble()
                );
                if (newRandom.isInCircle())
                {
                    inCircleCount++;
                }
            }
            inCircle.Add(inCircleCount);
        }

        static void miniMaxSum(int[] arr)
        {
            int max = arr[0];
            int min = arr[0];

            for (int i = 1; i < arr.Length; i++)
            {
                if (max < arr[i])
                {
                    max = arr[i];
                }
                if (min > arr[i])
                {
                    min = arr[i];
                }
            }
            int sumMax = 0;
            int suMin = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != max)
                {
                    suMin += arr[i];
                }
                if (arr[i] != min)
                {
                    sumMax += arr[i];
                }
            }
            Console.WriteLine(suMin + " " + sumMax);

        }

        static void Main(string[] args)
        {
            Square square = new Square(1);

            Int64 countOfPoints = 10000000000;
            int countOfThreads = 24;
            List<Int64> inCircle = new List<Int64>();
            List<Thread> threads = new List<Thread>();
            for (Int64 i = 0; i < countOfThreads; i++)
            {
                Thread myNewThread = new Thread(() => GetRandomList(countOfPoints/countOfThreads, ref inCircle));
                myNewThread.Start();
                threads.Add(myNewThread);
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            Int64 inCircleSum = 0;
            foreach (Int64 sum in inCircle)
            {
                inCircleSum += sum;
            }

            decimal calculatedPi = ((decimal)inCircleSum / (decimal)countOfPoints) * (decimal)4; 
            Console.WriteLine("inCircle : " + inCircleSum);
            Console.WriteLine("countOfPoints : " + countOfPoints);
            Console.WriteLine("Calculated PI : " + calculatedPi);

        }
    }
}
