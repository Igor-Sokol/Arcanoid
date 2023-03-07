using System;
using System.Collections.Generic;
using System.Linq;
using Application.Scripts.Library.PopUpManagers.PopUpContracts;
using UnityEngine;

namespace Application.Scripts.Library.PopUpManagers
{
    public class PopUpManager : MonoBehaviour, IPopUpManager
    {
        private readonly Dictionary<Type, List<IPopUp>> _loadedPopUps = new Dictionary<Type, List<IPopUp>>();

        [SerializeField] private PopUp[] popUpPrefabs;
        [SerializeField] private Transform container;

        public IEnumerable<IPopUp> ActivePopUps =>
            GetActivePopups().OrderBy(p => ((MonoBehaviour)p).transform.GetSiblingIndex());

        public T Show<T>() 
            where T : MonoBehaviour, IPopUp
        {
            T popUp = null;
            if (_loadedPopUps.TryGetValue(typeof(T), out List<IPopUp> popUpList))
            {
                popUp = popUpList.FirstOrDefault(p => !p.Active) as T;
            }

            if (!popUp)
            {
                popUp = CreatePopUp<T>();
            }
            
            popUp.transform.SetSiblingIndex(0);
            popUp.Show();
            
            return popUp;
        }

        private T CreatePopUp<T>()
            where T : MonoBehaviour, IPopUp
        {
            T prefab = popUpPrefabs.FirstOrDefault(p => p.GetType() == typeof(T)) as T;

            if (prefab)
            {
                var instance = Instantiate(prefab, container);
                instance.gameObject.SetActive(false);
                instance.transform.SetSiblingIndex(0);

                if (_loadedPopUps.TryGetValue(typeof(T), out var popUps))
                {
                    popUps.Add(instance);
                }
                else
                {
                    _loadedPopUps.Add(typeof(T), new List<IPopUp>() { instance });
                }

                return instance;
            }

            return default;
        }

        private IEnumerable<IPopUp> GetActivePopups()
        {
            foreach (var loadedPopUp in _loadedPopUps)
            {
                foreach (var popUp in loadedPopUp.Value.Where(popUp => popUp.Active))
                {
                    yield return popUp;
                }
            }
        }
    }
}