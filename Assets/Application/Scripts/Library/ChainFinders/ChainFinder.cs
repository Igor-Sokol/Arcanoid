using System;
using System.Collections.Generic;
using UnityEngine;

namespace Application.Scripts.Library.ChainFinders
{
    public static class ChainFinder
    {
        public static IEnumerable<Vector2Int> GetLongestChain<T,TKey>(T[][] table, Func<T, TKey> key, Func<T, bool> valid,
            Vector2Int start, Vector2Int[] moveRules, Comparer<TKey> comparer = null)
        {
            comparer ??= Comparer<TKey>.Default;

            Queue<Vector2Int> iterations = new Queue<Vector2Int>();
            Dictionary<TKey, List<Vector2Int>> ways = new Dictionary<TKey, List<Vector2Int>>();

            foreach (var moveRule in moveRules)
            {
                Vector2Int index = start + moveRule;
                if (!ValidIndex(table, index) || !valid(table[index.y][index.x])) continue;
                TKey currentKey = key(table[index.y][index.x]);
                    
                if (ways.TryGetValue(currentKey, out var list))
                {
                    list.Add(index);
                }
                else
                {
                    ways.Add(currentKey, new List<Vector2Int>() { start, index });
                }
                iterations.Enqueue(index);
            }
            
            while (iterations.Count > 0)
            {
                Vector2Int current = iterations.Dequeue();

                foreach (var rule in moveRules)
                {
                    Vector2Int index = current + rule;
                    if (!ValidIndex(table, index) || !valid(table[index.y][index.x])) continue;
                    TKey moveKey = key(table[index.y][index.x]);
                    TKey currentKey = key(table[current.y][current.x]);

                    if (!ways.ContainsKey(moveKey) ||
                        comparer.Compare(currentKey, moveKey) != 0 || ways[moveKey].Contains(index)) continue;
                    
                    ways[moveKey].Add(index);
                    iterations.Enqueue(index);
                }
            }
            
            
            List<Vector2Int> longestWay = null;
            foreach (var way in ways)
            {
                if (way.Value.Count > (longestWay?.Count ?? -1))
                {
                    longestWay = way.Value;
                }
            }

            return longestWay;

        }
        
        public static IEnumerable<Vector2Int> GetChain<T>(T[][] table,
            Vector2Int start, Vector2Int[] moves, int iterations, Predicate<T> predicate = null)
        {
            List<Vector2Int> way = new List<Vector2Int>() { start };
            
            Vector2Int current = start;

            for (int i = 0; i < iterations || iterations < 0; i++)
            {
                bool hasValid = false;
                foreach (var move in moves)
                {
                    current += move;
                    if (!ValidIndex(table, current)) continue;
                    if (predicate?.Invoke(table[current.y][current.x]) ?? true)
                    {
                        way.Add(current);
                    }
                        
                    hasValid = true;
                }

                if (!hasValid) break;
            }

            return way;
        }
        
        private static bool ValidIndex<T>(T[][] table, Vector2Int index)
        {
            return index.y >= 0 && index.y < table.Length && index.x >= 0 &&
                   index.x < table[index.y].Length;
        }
    }
}