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
        /// Path to suicide_predict.csv (data for suicideScript.py)
        /// </summary>
        public static string UserPosts { get; set; }
        /// <summary>
        /// Result of suicide prediction by python script
        /// </summary>
        public static string SuicideResult { get; set; }
        /// <summary>
        /// Directory where python script searches for text input (id.csv)
        /// and leaves probobilities (id.txt).
        /// </summary>
        public static string WorkingDir { get; set; }

        static AppSettings()
        {
            ApplicationId = 6752080;
            AccessToken = "685d8d86604d89a5612fa43761c92400818eb18a6074bc1878831f0aed5a062f4a522b3ed1946cdf8db0f";
            PythonPath = @"C:\ProgramData\Anaconda3\python.exe";
            PythonScriptPath = @"C:\Users\1\Source\Repos\myrachins\Psychotype_HSE_v2\Psychotype_HSE\Util\Scripts\suicideScript.py";
            
            WorkingDir = @"C:\Users\1\Source\Repos\myrachins\Psychotype_HSE_v2\Psychotype_HSE\Files\";
        }
    }
}