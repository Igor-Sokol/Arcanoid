using System;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels.Readers.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels
{
    [Serializable]
    public struct LevelInfo
    {
        [SerializeField] private string path;
        [SerializeField] private LevelReader levelReader;

        public string Path => path;
        public LevelReader LevelReader => levelReader;
    }
}