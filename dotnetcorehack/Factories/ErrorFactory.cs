using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace dotnetcorehack.Factories {

    public class ErrorFactory {
        public static Dictionary<String, List<String>> getErrorMessages(ModelStateDictionary modelState) {
            var errorDictionary = new Dictionary<String, List<String>>();

            foreach (KeyValuePair<String, ModelStateEntry> modelStateError in modelState) {
               var errorMessagesList = new List<String>();

               foreach (var modelError in modelStateError.Value.Errors) {
                    errorMessagesList.Add(modelError.ErrorMessage);
               }

               errorDictionary.Add(modelStateError.Key, errorMessagesList);
            }
            return errorDictionary;
        }

    }
}