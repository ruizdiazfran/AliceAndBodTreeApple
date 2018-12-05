using System;
using System.Collections.Generic;
using System.Linq;

namespace AppleTree
{
    class Program
    {
        //  Alice and Bob work in a beautiful orchard.There are N apple trees in the orchard.The apple trees are arranged in a row and they are numbered from 1 to N. 
        //  Alice is planning to collect all the apples from K consecutive trees and Bob is planning to collect all the apples from L consecutive trees. 
        //  They want to choose two disjoint segments (one consisting of K trees for Alice and the other consisting of L trees for Bob) so as not to disturb each other.
        //  What is the maximum number of apples that they can collect? 
        //  Write a function that given an array A consisting of N integers denating the number of apples on each apple tree in the row,
        //  and integers K and L denoting, respectively, thenumber of trees that Alice and Bob can choose when collecting, returns the maximum number of apples that can be collected by them, 
        //  or -1 if there are no such intervals.For example, given A =[6, 1,4,6,3,2,7,4], K=3, L=2, your function should return 24,
        //  because Alice can choose trees 3 to 5 and collect 4 + 6 + 3 = 13 apples, 
        //  and Bob can choose trees 7 to 8 and collect 7 + 4 = 11 apples.
        //  Thus, they will collect 13 + 11 = 24 apples in total, and that is the maximum number that can be achieved.
        //  Given A = [10, 19, 15], K = 2, L = 2, your function should return -1, because it is not possible for Alice and Bob to choose two disjoint intervals.

        //  Assume that: N is an integer within the range[2..600];
        //  K and L are integers within the range[1..N - 1]; each element of array A is an integer within the range[1..500] In your solution focus on correctness.
        //  The performance of your solution will not be the focus of the assessment.


        //  Alice y Bob trabajan en un hermoso huerto.Hay n manzanos en el huerto.Los manzanos están dispuestos en una fila y están numerados del 1 al N. 
        //  Alice planea recolectar todas las manzanas de K árboles consecutivos y Bob planea recolectar todas las manzanas de L árboles consecutivos.
        //  Quieren elegir dos segmentos separados (uno que consiste en K árboles para Alice y el otro que consiste en L árboles para Bob) para no molestarse unos a otros.
        //  ¿Cuál es el número máximo de manzanas que pueden recolectar? Escriba una función que, dada una matriz A que consta de N enteros que deniegan 
        //  el número de manzanas en cada manzano en la fila, y los enteros K y L denoten, respectivamente, el número de árboles que Alicia y Bob pueden elegir al recolectar,
        //  devuelve el número máximo de manzanas que pueden ser recolectadas por ellos, o -1 si no hay tales intervalos.
        //  Por ejemplo, dado A = [6, 1, 4, 6, 3, 2, 7, 4], K = 3, L = 2, su función debe devolver 24, porque Alicia puede elegir árboles de 3 a 5 y recoger 4 6 3 = 13 manzanas,
        //  y Bob puede elegir árboles 7 a 8 y recolectar 7 4 = 11 manzanas.Por lo tanto, recolectarán 13 11 = 24 manzanas en total, y ese es el número máximo que se puede lograr.
        //  Dado A = [10, 19, 15], K = 2, L = 2, su función debe devolver -1, porque Alice y Bob no pueden elegir dos intervalos separados.

        //  Suponga que: N es un número entero dentro del rango [2..600]; K y L son números enteros dentro del rango[1..N - 1]; 
        //  cada elemento de la matriz A es un número entero dentro del rango[1..500] En su solución, enfóquese en la corrección.
        //  El desempeño de su solución no será el enfoque de la evaluación.


        static void Main(string[] args)
        {
            var list = new List<int>() { 6, 1, 4, 6, 3, 2, 7, 4 };
            var k = 3;
            var l = 2;

            var result = Calculate(list, k, l);
            Console.Write("A:");
            foreach (var item in list)
            {
                Console.Write($"{item},");
            }
            Console.WriteLine();
            Console.WriteLine($"K: {k}");
            Console.WriteLine($"L: {l}");
            Console.WriteLine($"Result: {result}");
            Console.ReadLine();
        }

        private static int Calculate(List<int> a, int x, int y)
        {
            if ((x + y) > a.Count) return -1;
            var searchSet = (x >= y) ? x : y;

            var maxAndStartingIndexBigInterval = GetMaxInterval(a, searchSet);

            var totalOfKAndL = maxAndStartingIndexBigInterval.Item1;

            var afterItems = a.GetRange(0, maxAndStartingIndexBigInterval.Item2);
            var indexEnd = maxAndStartingIndexBigInterval.Item2 + searchSet;
            var otherItems = a.GetRange(indexEnd, a.Count - indexEnd);
            
            var searchSetSmallInterval = (x < y) ? x : y;
            var maxAndStartinIndexSmallInterval = GetMaxInterval(afterItems.Union(otherItems).ToList(), searchSetSmallInterval);

            totalOfKAndL += maxAndStartinIndexSmallInterval.Item1;

            return totalOfKAndL;
        }

        private static Tuple<int, int> GetMaxInterval(List<int> a, int numberOrElements)
        {
            int sumForFirst = 0;
            int sumForFirstPrev = 0;
            int startingIndexForK = 0;

            for (int i = 0; i < a.Count; i++)
            {
                if (i <= a.Count - numberOrElements)
                {
                    for (int j = i; j < i + numberOrElements; j++)
                    {
                        sumForFirst += a[j];
                    }
                }
                if (sumForFirst > sumForFirstPrev)
                {
                    sumForFirstPrev = sumForFirst;
                    startingIndexForK = i;
                }
                sumForFirst = 0;
            }

            return new Tuple<int, int>(sumForFirstPrev, startingIndexForK);
        }
    }
}
