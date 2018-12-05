using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class Ex
    {
        public static IEnumerable<IEnumerable<T>> DifferentCombinations<T>(this IEnumerable<T> elements, int k)
        {
            return k == 0 ? new[] { new T[0] } :
              elements.SelectMany((e, i) =>
                elements.Skip(i + 1).DifferentCombinations(k - 1).Select(c => (new[] { e }).Concat(c)));
        }
    }

    public static class MathExtensions
    {
        public static IEnumerable<IEnumerable<T>> Combinations1<T>(this IEnumerable<T> source, int n)
        {
            if (n == 0)
                yield return Enumerable.Empty<T>();

            int count = 1;
            foreach (T item in source)
            {
                foreach (var innerSequence in source.Skip(count).Combinations1(n - 1))
                {
                    yield return new T[] { item }.Concat(innerSequence);
                }
                count++;
            }
        }
    }

    class Program
    {

        // A few palindromic sentences exist in many languages.They look the same, regardless of whether they are 
        // read normally (from left to right) or backwards(from right to left). 
        // Here are some English examples:

        // was it a rat i saw
        // mr owl ate my metal worm
        // live on time emit no evil
        // 
        // Precisely speaking, a sentence is palindromic if, after removing all its spaces(and punctuation), 
        // it looks the same when read both left to right and right to left.For example, the second example sentence 
        // would become "mrowlatemymetalworm", which looks exactly the same when spelled backwards.

        // Tom just found a list of words in an old language, and he wonders if he can use them to create a palindromic sentence.Since he doesn't know the language, he will have to assume that any sequence of words from the list is a valid sentence. In order to create a palindromic sentence, he can use each word as many times as he wants. In particular, he may decide not to use some words at all.

        // Write a function:

        //       class Solution { public String solution(String S); }

        // that, given a string S of length N containing a space-separated list of all the words in the language, returns any palindromic sentence built from the words in S.All of the words in the output sentence should be written in lower case and separated by single spaces.If no such sentence exists, the function should return the word "NO". The length of the sentence should not exceed 600,000 characters.You can assume that if a palindromic sentence can be constructed using the input words, then a palindromic sentence of length not greater than 600,000 characters can be constructed as well.

        //       Examples:

        //  1. Given S = "by metal owl egg mr crow worm my ate", your function may return "mr owl ate my metal worm", "mr owl worm", or any other palindromic sentence built using the words from the list, that does not exceed 600,000 characters.

        //  2. Given S = "live on time emit no evil", your function may for example return "live on time emit no evil", "no on on no no on", "evil time emit live", or any other palindromic sentence built using the words from the list, that does not exceed 600,000 characters.

        //  3. Given S = "abcc bc ac", your function should return "NO", since no palindromic sentence may be constructed using words from the list.

        //    Write an efficient algorithm for the following assumptions:

        //  N is an integer within the range[1..500];
        //  string S consists only of lowercase letters(a−z) and spaces.


        static void Main(string[] args)
        {
            var phrase1 = "by mr owl ate my metal worm";
            // var phrase1 = "abcc bc ac";

            var result = MergePhrase(phrase1);

            Console.WriteLine($"Original Phrase : {phrase1}");
            Console.WriteLine($"{result}");
            Console.WriteLine("End.");
            Console.ReadLine();
        }

        private static string MergePhrase(string phrase)
        {
            var listWords = phrase.Split(' ').ToList();

            var resultCombinations = GetAllCombinationsOfAllSizes(listWords);

            var result = new List<string>();

            foreach (var item in resultCombinations)
            {
                var phraseJoin = string.Join(" ", item);
                var phraseWithoutWhite = phraseJoin.Replace(" ", "");

                var ok = IsPalindrome(phraseWithoutWhite);

                if (ok && 
                    !string.IsNullOrEmpty(phraseJoin) &&
                    !result.Any(x => x.Contains(phraseJoin)))
                {
                    result.Add(phraseJoin);
                }
            }

            if (result.Count == 0) return  "NO" ;
            return string.Join(", ", result.Distinct().ToList()); ;
        }

        public static List<List<string>> GetAllCombinationsOfAllSizes(List<string> ints)
        {
            var returnResult = new List<List<string>>();

            var distinctInts = ints.Distinct().ToList();
            for (int j = 0; j < distinctInts.Count(); j++)
            {
                var number = distinctInts[j];

                var newList = new List<string>
                {
                    number
                };
                returnResult.Add(newList);

                var listMinusOneObject = ints.Select(x => x).ToList();
                listMinusOneObject.Remove(listMinusOneObject.Where(x => x == number).First());

                if (listMinusOneObject.Count() > 0)
                {
                    _GetAllCombinationsOfAllSizes(listMinusOneObject, newList, ref returnResult);
                }
            }

            return returnResult;
        }
        public static void _GetAllCombinationsOfAllSizes(List<string> ints, List<string> growingList, ref List<List<string>> returnResult)
        {
            var distinctInts = ints.Distinct().ToList();
            for (int j = 0; j < distinctInts.Count(); j++)
            {
                var number = distinctInts[j];

                var newList = growingList.ToList();
                newList.Add(number);
                returnResult.Add(newList);

                var listMinusOneObject = ints.Select(x => x).ToList();
                listMinusOneObject.Remove(listMinusOneObject.Where(x => x == number).First());

                if (listMinusOneObject.Count() > 0)
                {
                    _GetAllCombinationsOfAllSizes(listMinusOneObject, newList, ref returnResult);
                }
            }

        }



        private static string CalculatePalimdrome(string phraseEmpty)
        {
            var result = new List<string>();

            for (int i = 0; i < phraseEmpty.Length; i++)
            {
                var minPhrase = string.Empty;
                var resultPhrase = string.Empty;
                for (int tam = 2; (i + tam) < phraseEmpty.Length; tam++)
                {
                    minPhrase = phraseEmpty.Substring(i, tam);

                    var minPhrasePalimdrome = Reverse(minPhrase);

                    var indexSearch = phraseEmpty.IndexOf(minPhrasePalimdrome);
                    if (indexSearch > 0)
                    {
                        resultPhrase = minPhrase;
                        tam++;
                    }
                    else { break; }
                }

                if (!string.IsNullOrEmpty(resultPhrase) &&
                    resultPhrase.Length > 2 &&
                    !result.Any(x => x.Contains(resultPhrase)))
                    result.Add(resultPhrase);
            }

            return string.Join(",", result);
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static bool IsPalindrome(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return s.CompareTo(new string(charArray))==0;
        }
    }
}
