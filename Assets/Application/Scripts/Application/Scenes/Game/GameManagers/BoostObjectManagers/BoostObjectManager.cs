using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.Pools.BoostObjectProviders;
using Application.Scripts.Application.Scenes.Game.Units.Boosts.Objects;
using Application.Scripts.Library.Reusable;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BoostObjectManagers
{
    public class BoostObjectManager : MonoBehaviour, IBoostObjectManager, IReusable
    {
        private readonly List<BoostObject> _activeBoostsView = new List<BoostObject>();

        [SerializeField] private BoostViewProvider boostViewProvider;

        public void PrepareReuse()
        {
            foreach (var boostView in _activeBoostsView)
            {
                boostViewProvider.Return(boostView);
            }
            _activeBoostsView.Clear();
        }
        
        public BoostObject GetBoostView(string key)
        {
            var boostView = boostViewProvider.GetBoostView(key);
            _activeBoostsView.Add(boostView);
            boostView.PrepareReuse();

            return boostView;
        }

        public void ReturnBoostView(BoostObject boostObject)
        {
            _activeBoostsView.Remove(boostObject);
            boostViewProvider.Return(boostObject);
        }
    }
}