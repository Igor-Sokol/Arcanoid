using System;
using System.Collections.Generic;
using System.Linq;
using Application.Scripts.Library.PopUpManagers.PopUpContracts;
using UnityEngine;

namespace Application.Scripts.Library.PopUpManagers
{
    public class PopUpManager : MonoBehaviour
    {
        private readonly Dictionary<Type, PopUp> _loadedPopUps = new Dictionary<Type, PopUp>();

        [SerializeField] private PopUp[] popUpPrefabs;
        [SerializeField] private Transform container;

        public T Show<T>() 
            where T : PopUp
        {
            if (_loadedPopUps.TryGetValue(typeof(T), out PopUp popUp))
            {
                if (!popUp.Active)
                {
                    popUp.Show();
                    return (T)popUp;
                }
            }
            else
            {
                T prefab = popUpPrefabs.FirstOrDefault(p => p.GetType() == typeof(T)) as T;

                if (prefab)
                {
                    var instance = Instantiate(prefab, container);
                    _loadedPopUps.Add(typeof(T), instance);
                    popUp = instance;
                    popUp.Show();
                    return (T)popUp;
                }
            }

            return null;
        }

        public void Hide<T>()
        {
            if (_loadedPopUps.TryGetValue(typeof(T), out PopUp popUp))
            {
                popUp.Hide();
            }
        }
    }
}