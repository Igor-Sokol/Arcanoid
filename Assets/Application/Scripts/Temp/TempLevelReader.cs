using Application.Scripts.Application.Scenes.Game.Units.Levels;
using Application.Scripts.Application.Scenes.Game.Units.Levels.Services.Readers.Contracts;
using UnityEngine;

namespace Application.Scripts.Temp
{
    public class TempLevelReader : LevelReader
    {
        [SerializeField] private string[] row1;
        [SerializeField] private string[] row2;
        [SerializeField] private string[] row3;
        
        public override string[][] ReadPack(LevelInfo level)
        {
            return new[] 
            {
                row1,
                row2, 
                row3 
            };
        }
    }
}