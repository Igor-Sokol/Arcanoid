using System;
using System.Collections.Generic;
using UnityEngine;

namespace Application.Scripts.Library.ChainFinders
{
    public static class ChainFinder
    {
        public static IEnumerable<Vector2> GetLongestChain<T>(T[][] table, Func<T, T, bool> predicate, Func<T, bool> valid,
            Vector2 start, Vector2[] moveRules)
        {
            List<List<Vector2>> ways = new List<List<Vector2>>();
            Queue<List<Vector2>> iterations = new Queue<List<Vector2>>();

            ways.Add(new List<Vector2>() { start });
            iterations.Enqueue(ways[0]);
            
            while (iterations.Count > 0)
            {
                List<Vector2> currentWay = iterations.Dequeue();
                Vector2 current = currentWay[currentWay.Count - 1];

                for (int i = 0; i < moveRules.Length; i++)
                {
                    Vector2 rule = moveRules[i];
                    
                    Vector2 index = current + rule;
                    if (ValidIndex(table, index) && valid(table[(int)index.y][(int)index.x]) && !currentWay.Contains(index)
                        && (current == start || predicate(table[(int)current.y][(int)current.x], table[(int)index.y][(int)index.x])))
                    {
                        if (i == moveRules.Length - 1)
                        {
                            currentWay.Add(index);
                            iterations.Enqueue(currentWay);
                        }
                        else
                        {
                            var newWay = new List<Vector2>(currentWay) { index };
                            ways.Add(newWay);
                            iterations.Enqueue(newWay);
                        }
                    }
                }
            }

            List<Vector2> longestWay = null;
            foreach (var way in ways)
            {
                if (way.Count > (longestWay?.Count ?? -1))
                {
                    longestWay = way;
                }
            }

            return longestWay;
        }
        
        public static IEnumerable<Vector2> GetChain<T>(T[][] table,
            Vector2 start, Vector2[] moves, int iterations, Predicate<T> predicate = null)
        {
            List<Vector2> way = new List<Vector2>() { start };
            
            Vector2 current = start;

            for (int i = 0; i < iterations || iterations < 0; i++)
            {
                bool hasValid = false;
                foreach (var move in moves)
                {
                    current += move;
                    if (ValidIndex(table, current))
                    {
                        if (predicate?.Invoke(table[(int)current.y][(int)current.x]) ?? true)
                        {
                            way.Add(current);
                        }
                        
                        hasValid = true;
                    }
                }

                if (!hasValid) break;
            }

            return way;
        }
        
        private static bool ValidIndex<T>(T[][] table, Vector2 index)
        {
            if (index.x >= 0 && index.x < table.Length 
             && index.y >= 0 && index.y < table[(int)index.x].Length)
            {
                return true;
            }

            return false;
        }
    }
}