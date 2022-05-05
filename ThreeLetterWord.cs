using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;

namespace RandomizedWord
{

    //TODO: Allow for items randomized to be of any # of letters instead of only the length. This can improve found words. Will need to improve loop however.
    //TODO: Add an option for displaying of all "Fake" words as well as all "Real" words. Likely will require changing how they are displayed for user?
    //TODO: Remove user caps, and trim white space
    //TODO: Capitolize first letter of the "Fake" word

    class ThreeLetterWord
    {
        static void Main(string[] args)
        {
            string word;
            //To later print the correct value of real words to user
            int realWordCount = 0;
            bool dupecheck = true;

            string wordRead;
            Console.WriteLine("Enter a 5 letter word and all possible 3 letter combinations will be retrieved.");
            word = Console.ReadLine();
            Console.WriteLine("");

            //Loop to check for answers smaller than size
            if (word.Length < 5)
            {
                for (int i = 0; i < 1;)
                {
                    if (word.Length < 6)
                    {
                        i++;
                    }
                    Console.WriteLine("Error, word length too small. Try again.");
                    Console.WriteLine("Enter a 5 letter word and all possible 3 letter combinations will be retrieved.");
                    word = Console.ReadLine();
                    Console.WriteLine("");
                }
            }

            //Check for if user wants duplicate filtering on or off.
            Console.WriteLine("Do you want to filter out duplicates? (Yes/No)");
            Console.WriteLine("Examples: aaa, aab, abb");
            Console.WriteLine("");
            wordRead = Console.ReadLine();
            if (wordRead == "yes" || wordRead == "Yes" || wordRead == "ye" || wordRead == "y" || wordRead == "Y")
            {
                dupecheck = true;
            }
            else if (wordRead == "no" || wordRead == "No" || wordRead == "n" || wordRead == "N")
            {
                dupecheck = false;
            }
            else
            {
                Console.WriteLine("Error: Neither yes or no were detected. Defaulting to duplicate filtering ON. No Duplicates will be sent to user.");
                Console.WriteLine("");
            }
            //End checking for user duplicate filtering opinion.


            int wordLength = word.Length; //Get length of word above
            List<string> wordList = new List<string>(); //Create string we'll use below

            for (int i = 0; i < wordLength; i++) //Add the user generated word to the list
            {
                wordList.Add(word.Substring(i, 1));
            }


            List<String> shuffleWordList = new List<String>();
            List<String> compareWordList = new List<String>();
            string compareString = "";
            Random rnd = new Random();

            //This whole loop might need some improvement but it works
            for (int i = 0; i <= word.Length * word.Length;)
            {
                shuffleWordList = wordList.OrderBy(a => rnd.Next()).ToList();

                //Creates a string that is used when compared against compareWordList
                foreach (var item in shuffleWordList)
                {
                    compareString = compareString + item;
                }

                if (!compareWordList.Contains(compareString))
                {
                    string WordListString = "";
                    foreach (var item in shuffleWordList)
                    {
                        WordListString = WordListString + item;
                    }

                    string dupeWord = "";
                    dupeWord = WordListString;
                    DictionaryCheck localDict = new DictionaryCheck();
                    dupeWord = localDict.EngDictionary(dupeWord);

                    if (dupeWord == WordListString)
                    {
                        Console.WriteLine(WordListString);
                        realWordCount++;
                    }
                    else { }

                    //Add the string that passed the above comparision to the list so that it may no longer be taken.
                    compareWordList.Add(WordListString);
                    i++;
                }

                //Clear string so we get a fresh start above
                compareString = "";
            }
            if (realWordCount == 0)
            {
                Console.WriteLine(compareWordList.Count + " entries were found. None of them were real words");
            }

            if (realWordCount > 0)
            {
                Console.WriteLine(compareWordList.Count + " entries were found. Out of these " + realWordCount + " entries were real words.");
            }
            Console.ReadLine();
        }
    }
}
