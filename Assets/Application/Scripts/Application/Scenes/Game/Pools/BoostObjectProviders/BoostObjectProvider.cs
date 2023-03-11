using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.Pools.BoostObjectProviders.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Boosts.Objects;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.ObjectPools;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Pools.BoostObjectProviders
{
    public class BoostObjectProvider : MonoBehaviour, IBoostObjectProvider, IInitializing
    {
        private Dictionary<string, ObjectPoolMono<BoostObject>> _blockPool;

        [SerializeField] private Transform container;
        [SerializeField] private BoostObject[] prefabs;
        public void Initialize()
        {
            _blockPool = new Dictionary<string, ObjectPoolMono<BoostObject>>();

            foreach (var boostView in prefabs)
            {
                _blockPool.Add(boostView.Key, new ObjectPoolMono<BoostObject>(() =>
                {
                    var view = Instantiate(boostView);
                    view.Initialize();
                    return view;
                }, container));
            }
        }
        
        public BoostObject GetBoostObject(string key)
        {
            if (_blockPool.TryGetValue(key, out var pool))
            {
                var view = pool.Get();
                return view;
            }

            return null;
        }

        public void Return(BoostObject boostObject)
        {
            if (_blockPool.TryGetValue(boostObject.Key, out var pool))
            {
                pool.Return(boostObject);
            }
        }
    }
}