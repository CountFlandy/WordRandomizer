using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;

namespace RandomizedWord
{
    public class DictionaryCheck
    {
        public string EngDictionary(string engWordDic)
        {
            string[] engDic = null; //Make the array
            string folderLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string filePath = Path.Combine(folderLoc, "EnglishDic.txt");
            engDic = File.ReadAllLines(filePath);
            int dicLength = engDic.Count();
            string correctWord = "";

            for (int i = 0; i <= dicLength;)
            {
                if (i == dicLength)
                {
                    engWordDic = null;
                    return engWordDic;
                }
                if (engWordDic == engDic[i])
                {
                    correctWord = engDic[i];
                    //i = dicLength;

                    engWordDic = correctWord;
                    return engWordDic;
                }
                //else if (i == dicLength)
                //{
                //    return "";
                //}
                else
                {
                    i++;
                }
            }

            engWordDic = correctWord;
            return engWordDic;
        }
    }
}
