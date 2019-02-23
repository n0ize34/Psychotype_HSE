﻿using System;

namespace Psychotype_HSE.Models
{
    /// <summary>
    /// All app settings which is required 
    /// </summary>
    public static class AppSettings
    {
        /// <summary>
        /// Id of registered VK application (VkApi works only beyond such applications)
        /// </summary>
        public static ulong ApplicationId { get; set; }
        /// <summary>
        /// Access token for VK API
        /// </summary>
        public static string AccessToken { get; set; }
        /// <summary>
        /// Path to python model script 
        /// </summary>
        public static string PythonScriptPath { get; set; }
        /// <summary>
        /// Path to python interpreter (in a way...)
        /// </summary>
        public static string PythonPath { get; set; }
        /// <summary>
        /// Path to suicide_predict.csv (data for suicide_predict.py)
        /// </summary>
        public static string SuicidePredictCSV { get; set; }
        /// <summary>
        /// Result of suicide prediction by python script
        /// </summary>
        public static string SuicideResult { get; set; }

        static AppSettings()
        { 
            ApplicationId = 6752080;
            AccessToken = "INPUT_YOUR_TOKEN";          
            PythonPath = "INPUT_YOUR_PATH";
            PythonScriptPath = "INPUT_YOUR_PATH";
            SuicidePredictCSV = @"../../Files/suicide_predict.csv";
            SuicideResult = @"../../Files/suicide_result.txt";
        }
    }
}