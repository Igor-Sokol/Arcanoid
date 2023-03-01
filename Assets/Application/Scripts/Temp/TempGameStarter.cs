using Application.Scripts.Application.Scenes.Game.GameInstallers.LevelInstallers;
using Application.Scripts.Library.InitializeManager.Contracts;
using UnityEngine;

namespace Application.Scripts.Temp
{
    public class TempGameStarter : MonoBehaviour, IInitializing
    {
        [SerializeField] private LevelInstaller levelInstaller;
        
        public void Initialize()
        {
            levelInstaller.LoadLevel(default);
        }
    }
}