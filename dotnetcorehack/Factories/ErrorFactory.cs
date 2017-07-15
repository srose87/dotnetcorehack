namespace Dotnetcorehack.Factories
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class ErrorFactory
    {
        public static Dictionary<string, List<string>> GetErrorMessages(ModelStateDictionary modelState)
        {
            var errorDictionary = new Dictionary<string, List<string>>();

            foreach (KeyValuePair<string, ModelStateEntry> modelStateError in modelState)
            {
                var errorMessagesList = new List<string>();

                foreach (var modelError in modelStateError.Value.Errors)
                {
                    errorMessagesList.Add(modelError.ErrorMessage);
                }

                if (errorMessagesList.Count > 0)
                {
                    errorDictionary.Add(modelStateError.Key, errorMessagesList);
                }
            }

            return errorDictionary;
        }
    }
}