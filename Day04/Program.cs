﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day04
{
    internal class Program
    {
        private static void Main()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var input = File.ReadAllText("Input.txt");
            var data = input.Split('\n').ToList();
            var idSum = 0;
            foreach (var s in data.Where(s => s != ""))
            {
                var counts = new Dictionary<char, int>();
                var id = int.Parse(s[(s.LastIndexOf('-') + 1)..s.IndexOf('[')]);
                var line = s.Split('[');
                var room = line[0][0..s.LastIndexOf('-')].Replace("-", "");
                var checkSum = line[1].Trim(']');
                foreach (var c in room)
                {
                    if (counts.ContainsKey(c)) counts[c]++;
                    else counts[c] = 1;
                }

                var topFive = counts.OrderByDescending(kv => kv.Value).ThenBy(kv => kv.Key).Take(5).Select(kv => kv.Key).Aggregate("", (current, next) => current + next);
                if (topFive == checkSum) idSum += id;
            }
            Console.WriteLine("Valid rooms = " + idSum);
        }

        private static void SolvePart2()
        {
            var input = File.ReadAllText("Input.txt");
            var data = input.Split('\n').ToList();
            var rooms = new List<(string, int)>();
            foreach (var s in data.Where(s => s != ""))
            {
                var counts = new Dictionary<char, int>();
                var id = int.Parse(s[(s.LastIndexOf('-') + 1)..s.IndexOf('[')]);
                var line = s.Split('[');
                var room = line[0][0..s.LastIndexOf('-')].Replace("-", "");
                var checkSum = line[1].Trim(']');
                foreach (var c in room)
                {
                    if (counts.ContainsKey(c)) counts[c]++;
                    else counts[c] = 1;
                }

                var topFive = counts.OrderByDescending(kv => kv.Value).ThenBy(kv => kv.Key).Take(5).Select(kv => kv.Key).Aggregate("", (current, next) => current + next);
                if (topFive == checkSum) rooms.Add((s[0..s.LastIndexOf('-')], id));
            }

            var decryptedRooms = new List<(string, int)>();
            foreach (var r in rooms)
            {
                var decrypted = "";
                var inc = r.Item2 % 26;
                foreach (var c in r.Item1)
                {
                    if (c == '-') decrypted += " ";
                    else
                    {
                        decrypted += (c + inc) > 122 ? (char)((c + inc) - 26) : (char)(c + inc);
                    }
                }
                decryptedRooms.Add((decrypted, r.Item2));
            }
            Console.WriteLine("North Pole objects are in room " + decryptedRooms.FirstOrDefault(r => r.Item1.Contains("north")).Item2);
        }
    }
}
