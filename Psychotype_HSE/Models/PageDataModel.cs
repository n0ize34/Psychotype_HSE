﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Psychotype_HSE.Models
{
    public class PageDataModel
    {
        public class PopularWordsAtributes
        {
            // префикс для получения ссылок на словарь
            public static string dictLink = "https://ru.wiktionary.org/wiki/";
            // список пар <корень, формы>
            public List<Tuple<string, List<String>>> response = new List<Tuple<string, List<string>>>();

            public PopularWordsAtributes() { }

            public PopularWordsAtributes(Components.User user, DateTime timeFrom,
                                                DateTime timeTo, int numberOfWords)
            {
                List<List<String>> popularWords = 
                    user.GetMostPopularWordsOnWall(timeFrom, timeTo, numberOfWords);

                // для всех слов создаем пары <слово, формы>
                for (int i = 0; i < numberOfWords; i++)
                    if (i + 1 <= popularWords.Count)
                    {
                        HashSet<string> set = new HashSet<string>();
                        foreach (string s in popularWords[i])
                        {
                            set.Add(s.ToLower());
                        }
                        List<String> list = set.ToList();
                        String leading = list[0];
                        list.RemoveAt(0);
                        response.Add(new Tuple<string, List<string>>(leading,list));
                    }
            }
        }

        DateTime timeFrom = new DateTime(2006, 1, 1);
        DateTime timeTo = DateTime.Now;
        int numberOfWord = 10;


        private String id = "";
        private double botProbability = 0;
        private double suicideProbability = 0;
        private bool hasWords = false;
        private PopularWordsAtributes popularWords = new PopularWordsAtributes();
        private string fullName = "Имя Фамилия";
        private string photoURL = "https://vk.com/images/camera_200.png?ava=1";
        private List<string> description = new List<string>();

        public string FullName
        {
            get { return fullName;  }
        }

        public string PhotoURL
        {
            get { return photoURL; }
        }

        public List<string> Description
        {
            get { return description; }
        }

        public double BotProbability
        {
            get { return botProbability; }
        }

        public double SuicideProbability
        {
            get { return suicideProbability; }
        }

        public bool HasWords
        {
            get { return hasWords; }
        }

        public PopularWordsAtributes PopularWords
        {
            get { return popularWords; }
        }

        public bool isLinkValid { set; get; }

        public String Id
        {
            get { return id; }
            set
            {
                string[] splitedId;

                isLinkValid = false;

                if (value != null)
                {
                    splitedId = value.Split('/');
                    id = splitedId[splitedId.Length - 1];
                }
                else id = "";

                // If something wrong inside this try block
                // further conditions won't be satisfied.
                try
                {
                    if (id == "")
                        throw new Exception("Void input");
                    user = new Components.User(id);
                    isLinkValid = true;
                }
                catch (Exception)
                {
                    id = "";
                    isLinkValid = false;
                }

                if (isLinkValid)
                {
                    // Link might be valid, meanwhile profile is private.
                    // We'll treat those as invalid links.
                    try
                    {
                        if (isLinkValid)
                            botProbability = user.IsBot();
                        else botProbability = 0;
                    }
                    catch (Exception)
                    {
                        isLinkValid = false;
                        id = "";
                        botProbability = 0;
                    }
                }

                // From this poin we can be sure if link is valid.
                if (isLinkValid)
                {
                    //string curDir = "C:\\Users\\1\\Source\\Repos\\myrachins\\Psychotype_HSE_v2\\Psychotype_HSE\\Files\\";// Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "/Files/"; //.GetCurrentDirectory();
                    //string fileData = AppSettings.WorkingDir + id + ".csv";
                    //string fileRes = AppSettings.WorkingDir + id + ".txt";

                    //"C:\Users\1\Source\Repos\myrachins\Psychotype_HSE_v2\Psychotype_HSE\Files\"

                    // вызывается отдельно
                    //Util.PythonRunner.RunScript("C:\\Users\\1\\Source\\Repos\\myrachins\\Psychotype_HSE_v2\\Psychotype_HSE\\Util\\Scripts\\suicideScript.py",
                    //    AppSettings.PythonPath, curDir);//, fileData);
                    var api = Api.Get();

                    popularWords = new PopularWordsAtributes(user, timeFrom, timeTo, numberOfWord);

                    suicideProbability = user.SuicideProbability(timeFrom, timeTo, AppSettings.WorkingDir, id);

                    VkNet.Model.User vkUser =
                     api.Users.Get(new string[] { id }, VkNet.Enums.Filters.ProfileFields.All).First();

                    try
                    {
                        fullName = vkUser.FirstName + " " + vkUser.LastName;
                        if (fullName == "")
                            throw new Exception();
                    }
                    catch (Exception)
                    {
                        fullName = "ФИО недоступно";
                    }

                    photoURL = vkUser.Photo50.AbsoluteUri;// vkUser.PhotoMax.AbsoluteUri;//Photo200.AbsoluteUri;

                    string s = "";
                    int i = 0;

                    s = vkUser.Status;
                    if (s != null & s != "")
                        description.Add("статус: " + s);
                    s = "";

                    s = "пол: ";
                    i = (int)vkUser.Sex;
                    switch (i)
                    {
                        case 0:
                            s += "не указан";
                            break;

                        case 1:
                            s += "женский";
                            break;

                        case 2:
                            s += "мужской";
                            break;
                    }
                    description.Add(s);
                    s = "";
                    
                        if (vkUser.Country != null)
                            description.Add("страна: " + vkUser.Country.Title);
                    s = "";
                    
                        if (vkUser.City != null)
                            description.Add("город: " + vkUser.City.Title);
                        s = "";

                    s = vkUser.MobilePhone;
                    if (s != null & s != "")
                        description.Add("моб. тел.: " + s);
                    s = "";

                    s = vkUser.HomePhone;
                    if (s != null & s != "")
                        description.Add("дом. тел.: " + s);

                    s = "отношения: ";
                    if (vkUser.Relation != null)
                    {
                        i = (int)vkUser.Relation;
                        switch (i)
                        {
                            case (1):
                                description.Add(s + "не женат/ замужем");
                                break;
                            case (2):
                                description.Add(s + "есть друг/ подруга");
                                break;
                            case (3):
                                description.Add(s + "помолвлен(а)");
                                break;
                            case (4):
                                description.Add(s + "женат(а)");
                                break;
                            case (5):
                                description.Add(s + "всё сложно");
                                break;
                            case (6):
                                description.Add(s + "в активном поиске");
                                break;
                            case (7):
                                description.Add(s + "влюблен(а)");
                                break;
                            case (8):
                                description.Add(s + "в гражданском браке");
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    popularWords = new PopularWordsAtributes();
                    suicideProbability = 0;
                }

                hasWords = !(popularWords.response.Count == 0);
            }
        }

        Components.User user = new Components.User("");

        //static public Components.User User { get { return user; }  }

        public PageDataModel() : this("") { }
        public PageDataModel(string raw_id) { this.Id = raw_id;  }

    }
}