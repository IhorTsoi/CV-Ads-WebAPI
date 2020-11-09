using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CV_Ads_WebAPI.Services
{
    public class LocalizationService : IStringLocalizer
    {
        private readonly Dictionary<string, Dictionary<string, string>> _resources;

        public LocalizationService()
        {
            Dictionary<string, string> enDictionary = GetEnglishLocalizationDictionary();
            Dictionary<string, string> uaDictionary = GetUkrainianLocalizationDictionary();
            
            CheckInitializationCorrectness(enDictionary, uaDictionary);

            _resources = new Dictionary<string, Dictionary<string, string>>
            {
                {"en", enDictionary },
                {"ua", uaDictionary }
            };
        }

        private void CheckInitializationCorrectness(
            Dictionary<string, string> enDictionary, Dictionary<string, string> uaDictionary)
        {
            if (enDictionary.Count != uaDictionary.Count)
            {
                throw new Exception(
                    "The initialization of localization service failed. The dictionaries have different number of strings.");
            }

            foreach (var pair in enDictionary)
            {
                if (!uaDictionary.ContainsKey(pair.Key))
                {
                    throw new Exception(
                        $"The initialization of localization service failed. Key in en dicitonary: {pair.Key}.");
                }
            }
        }

        private Dictionary<string, string> GetUkrainianLocalizationDictionary()
        {
            var dictionary = new Dictionary<string, string>();

            AddUkrainianValidationMessages(dictionary);

            return dictionary;
        }
        
        private Dictionary<string, string> GetEnglishLocalizationDictionary()
        {
            var dictionary = new Dictionary<string, string>();

            AddEnglishValidationMessages(dictionary);

            return dictionary;
        }

        private void AddUkrainianValidationMessages(Dictionary<string, string> dictionary)
        {
            dictionary.Add("The email is incorrect.", "Адреса електронної пошти введена некоректно.");
            dictionary.Add("The password field must not be empty.", "Пароль є обов'язковим для заповнення.");
            dictionary.Add("The first name field must not be empty.", "Ім'я є обов'язковим для заповнення.");
            dictionary.Add("The last name field must not be empty.", "Прізвище є обов'язковим для заповнення.");
            dictionary.Add("The serial number field must not be empty.", "Серійний номер є обов'язковим для заповнення.");
            dictionary.Add("The login field must not be empty.", "Логін є обов'язковим для заповнення.");
            dictionary.Add("The new password field must not be empty.", "Новий пароль є обов'язковим для заповнення.");
        }

        private void AddEnglishValidationMessages(Dictionary<string, string> dictionary)
        {
            dictionary.Add("The email is incorrect.", "The email is incorrect.");
            dictionary.Add("The password field must not be empty.", "The password field must not be empty.");
            dictionary.Add("The first name field must not be empty.", "The first name field must not be empty.");
            dictionary.Add("The last name field must not be empty.", "The last name field must not be empty.");
            dictionary.Add("The serial number field must not be empty.", "The serial number field must not be empty.");
            dictionary.Add("The login field must not be empty.", "The login field must not be empty.");
            dictionary.Add("The new password field must not be empty.", "The new password field must not be empty.");
        }

        public LocalizedString this[string name]
        {
            get
            {
                var currentCulture = CultureInfo.CurrentCulture.Name;
                if (_resources.ContainsKey(currentCulture))
                {
                    if (_resources[currentCulture].ContainsKey(name))
                    {
                        return new LocalizedString(name, _resources[currentCulture][name]);
                    }
                    else
                    {
                        throw new Exception($"Localization failed. The localization string is not defined: '{name}'.");
                    }
                }
                else
                {
                    throw new Exception($"Localization failed. The culture is not supported: '{currentCulture}'.");
                }
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;
            return _resources[currentCulture].Select((pair) => new LocalizedString(pair.Key, pair.Value));
        }

        public LocalizedString this[string name, params object[] arguments] => this[name];

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return this;
        }
    }
}
