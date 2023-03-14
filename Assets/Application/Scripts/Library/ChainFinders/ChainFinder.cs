using System;
using System.Collections.Generic;
using UnityEngine;

namespace Application.Scripts.Library.ChainFinders
{
    public static class ChainFinder
    {
        public static IEnumerable<Vector2> GetLongestChain<T,TKey>(T[][] table, Func<T, TKey> key, Func<T, bool> valid,
            Vector2 start, Vector2[] moveRules, Comparer<TKey> comparer = null)
        {
            comparer ??= Comparer<TKey>.Default;

            Queue<Vector2> iterations = new Queue<Vector2>();
            Dictionary<TKey, List<Vector2>> ways = new Dictionary<TKey, List<Vector2>>();

            foreach (var moveRule in moveRules)
            {
                Vector2 index = start + moveRule;
                if (ValidIndex(table, index) && valid(table[(int)index.y][(int)index.x]))
                {
                    TKey currentKey = key(table[(int)index.y][(int)index.x]);
                    
                    if (ways.TryGetValue(currentKey, out var list))
                    {
                        list.Add(index);
                    }
                    else
                    {
                        ways.Add(currentKey, new List<Vector2>() { start, index });
                    }
                    iterations.Enqueue(index);
                }
            }
            
            while (iterations.Count > 0)
            {
                Vector2 current = iterations.Dequeue();

                for (int i = 0; i < moveRules.Length; i++)
                {
                    Vector2 rule = moveRules[i];

                    Vector2 index = current + rule;
                    if (ValidIndex(table, index) && valid(table[(int)index.y][(int)index.x]))
                    {
                        TKey moveKey = key(table[(int)index.y][(int)index.x]);
                        TKey currentKey = key(table[(int)current.y][(int)current.x]);

                        if (ways.ContainsKey(moveKey) &&
                            comparer.Compare(currentKey, moveKey) == 0 && !ways[moveKey].Contains(index))
                        {
                            ways[moveKey].Add(index);
                            iterations.Enqueue(index);
                        }
                    }
                }
            }
            
            
            List<Vector2> longestWay = null;
            foreach (var way in ways)
            {
                if (way.Value.Count > (longestWay?.Count ?? -1))
                {
                    longestWay = way.Value;
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
            if (index.y >= 0 && index.y < table.Length 
             && index.x >= 0 && index.x < table[(int)index.x].Length)
            {
                return true;
            }

            return false;
        }
    }
}