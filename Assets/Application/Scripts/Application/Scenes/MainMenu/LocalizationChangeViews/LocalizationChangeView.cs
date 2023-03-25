using System.Collections.Generic;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.Localization.Configs;
using Application.Scripts.Library.Localization.LocalizationManagers;
using TMPro;
using UnityEngine;
using Zenject;
using ProjectContext = Application.Scripts.Library.DependencyInjection.ProjectContext;

namespace Application.Scripts.Application.Scenes.MainMenu.LocalizationChangeViews
{
    public class LocalizationChangeView : MonoBehaviour, IInitializing
    {
        private readonly List<LanguageInfo> _languages = new List<LanguageInfo>();
        private ILocalizationManager _localizationManager;

        [SerializeField] private TMP_Dropdown tmpDropdown;

        [Inject]
        public void Construct(ILocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
        }
        
        public void Initialize()
        {
            GenerateOptions();
            tmpDropdown.value = _languages.IndexOf(_localizationManager.CurrentLanguage.LanguageInfo);
        }
        
        private void OnEnable()
        {
            tmpDropdown.onValueChanged.AddListener(OnLanguageChanged);
        }

        private void OnDisable()
        {
            tmpDropdown.onValueChanged.RemoveListener(OnLanguageChanged);
        }

        private void GenerateOptions()
        {
            tmpDropdown.options.Clear();
            foreach (var languageConfig in _localizationManager.SupportedLanguages)
            {
                var option =
                    new TMP_Dropdown.OptionData(_localizationManager.GetString(languageConfig.LanguageInfo.LanguageKey),
                        languageConfig.LanguageInfo.Flag);
                
                tmpDropdown.options.Add(option);
                _languages.Add(languageConfig.LanguageInfo);
            }
            
            tmpDropdown.RefreshShownValue();
        }
        
        private void OnLanguageChanged(int value)
        {
            _localizationManager.LoadLanguage(_languages[value].Language);
            GenerateOptions();
        }
    }
}