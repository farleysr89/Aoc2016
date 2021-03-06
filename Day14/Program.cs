﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Day14
{
    internal class Program
    {
        private static readonly Dictionary<string, string> Hashes = new Dictionary<string, string>();
        private static void Main()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var input = File.ReadAllText("Input.txt");
            var salt = input.Split('\n')[0];
            var index = 0;
            var count = 0;
            var goal = 64;
            var md5 = MD5.Create();
            while (count < goal)
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(salt + index));
                var hex = BitConverter.ToString(hash).Replace("-", "");
                for (var i = 0; i < hex.Length - 2; i++)
                {
                    var found = false;
                    if (hex[i] != hex[i + 1] || hex[i] != hex[i + 2]) continue;
                    var tmp = index + 1;
                    var tmpMax = index + 1001;
                    while (tmp < tmpMax)
                    {
                        var tmpHash = md5.ComputeHash(Encoding.UTF8.GetBytes(salt + tmp));
                        var tmpHex = BitConverter.ToString(tmpHash).Replace("-", "");
                        if (tmpHex.Contains(hex[i].ToString() + hex[i] + hex[i] + hex[i] + hex[i]))
                        {
                            found = true;
                            break;
                        }
                        tmp++;
                    }

                    if (found) count++;
                    break;
                }

                index++;
            }
            Console.WriteLine("64th key index = " + (index - 1));
        }

        private static void SolvePart2()
        {
            var input = File.ReadAllText("Input.txt");
            var salt = input.Split('\n')[0];
            var index = 0;
            var count = 0;
            var goal = 64;
            while (count < goal)
            {
                string hex;
                if (Hashes.ContainsKey(salt + index))
                {
                    hex = Hashes[salt + index];
                }
                else
                {
                    hex = GetHash(salt + index);
                }

                for (var i = 0; i < hex.Length - 2; i++)
                {
                    var found = false;
                    if (hex[i] != hex[i + 1] || hex[i] != hex[i + 2]) continue;
                    var tmp = index + 1;
                    var tmpMax = index + 1001;
                    while (tmp < tmpMax)
                    {
                        string tmpHex;
                        if (Hashes.ContainsKey(salt + tmp))
                        {
                            tmpHex = Hashes[salt + tmp];
                        }
                        else
                        {
                            tmpHex = GetHash(salt + tmp);
                        }

                        if (tmpHex.Contains(hex[i].ToString() + hex[i] + hex[i] + hex[i] + hex[i]))
                        {
                            found = true;
                            break;
                        }
                        tmp++;
                    }

                    if (found) count++;
                    break;
                }

                index++;
            }
            Console.WriteLine("64th key index = " + (index - 1));
        }

        public static string GetHash(string s)
        {
            var md5 = MD5.Create();
            var ss = s;
            for (var i = 0; i < 2017; i++)
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(ss));
                ss = BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
            Hashes.Add(s, ss);
            return ss;
        }
    }
}
