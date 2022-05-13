using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;

namespace RandomizedWord
{
    class ThreeLetterWord
    {
        static void Main(string[] args)
        {
            string userDifferentWord = "yes";
            while (userDifferentWord == "yes")
            {
                string userWord;
                int realWordCount = 0; //To later print the correct value of real words to user
                string optionFakeWords;
                bool fakeWords = true;
                bool goAgain = false;
                string inputFilePath = "null";

                Console.WriteLine("Enter a 5 letter word and all possible 3 letter combinations will be retrieved.");
                userWord = Console.ReadLine();
                Console.WriteLine("");
                userWord.Trim();
                userWord.ToLower();

                //Loop to check for answers smaller than size
                if (userWord.Length < 5)
                {
                    for (int i = 0; i < 1;)
                    {
                        if (userWord.Length < 6)
                        {
                            i++;
                        }
                        Console.WriteLine("Error, word length too small. Try again.");
                        Console.WriteLine("Enter a 5 letter word and all possible 3 letter combinations will be retrieved.");
                        userWord = Console.ReadLine();
                        Console.WriteLine("");
                    }
                }

                Console.WriteLine("Do you want to display only real words?");
                Console.WriteLine("Example: (Word is: House) uohse would be filtered out.");
                Console.WriteLine("");
                optionFakeWords = Console.ReadLine();
                optionFakeWords.Trim();
                optionFakeWords.ToLower();
                if (optionFakeWords == "yes" || optionFakeWords == "Yes" || optionFakeWords == "ye" || optionFakeWords == "y" || optionFakeWords == "Y")
                {
                    fakeWords = true;
                }
                else if (optionFakeWords == "no" || optionFakeWords == "No" || optionFakeWords == "n" || optionFakeWords == "N")
                {
                    fakeWords = false;
                }
                else
                {
                    Console.WriteLine("Error: Neither yes or no were detected. Defaulting to fake word filtering ON. No fake words will be sent to user.");
                    Console.WriteLine("");
                }

                Console.WriteLine("Would you like to write the results into a text file for later reading?");
                Console.WriteLine("Yes/y or No/n");
                Console.WriteLine();
                string saveFile = Console.ReadLine();
                saveFile.Trim();
                saveFile.ToLower();
                if(saveFile == "yes" || saveFile == "y" || saveFile == "ye")
                {
                    if (File.Exists(inputFilePath + ".txt"))
                    {
                        File.Delete(inputFilePath);
                    }
                    inputFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                }
                else if (saveFile == "no" || saveFile == "n" || saveFile == "noo")
                {
                    Console.WriteLine("Nothing will be written to a text file.");
                }
                StreamWriter writeFile = new StreamWriter(userWord + ".txt");

                int wordLength = userWord.Length; //Get length of word above
                int minWordLength = 3;
                List<string> wordList = new List<string>(); //Create string we'll use below

                for (int i = 0; i < wordLength; i++) //Add the user generated word to the list
                {
                    wordList.Add(userWord.Substring(i, 1));
                }

                List<String> shuffleWordList = new List<String>();
                List<String> compareWordList = new List<String>();
                compareWordList.Add(userWord);
                string compareString = "";
                Random rnd = new Random();
                int k = (rnd.Next(0, 100));
                int wL = wordLength;
                int mWL = minWordLength;
                bool suddenlyBreak = false;
                int timer = 0;
                int maxPassesEqu = ((userWord.Length - mWL + 1) *  userWord.Length * 26);
                shuffleWordList = wordList;

                for (int i = 0; i <= maxPassesEqu;)
                {
                    if (wordList.Count != wordLength)
                    {
                        wordList.Clear();
                        for (int r = 0; r < wordLength; r++) //Add the user generated word to the list
                        {
                            wordList.Add(userWord.Substring(r, 1));
                        }
                    }
                    if (shuffleWordList.Count != wordLength)
                    {
                        shuffleWordList.Clear();
                        for (int e = 0; e < wordLength; e++) //Add the user generated word to the list
                        {
                            shuffleWordList.Add(userWord.Substring(e, 1));
                        }
                    }
                    for (int q = 0; q < 1;) //This part finds the "words"
                    {
                        if (shuffleWordList.Count != wordLength)
                        {
                            shuffleWordList.Clear();
                            for (int e = 0; e < wordLength; e++) //Add the user generated word to the list
                            {
                                shuffleWordList.Add(userWord.Substring(e, 1));
                            }
                        }

                        wL = wordLength;
                        mWL = minWordLength;
                        timer = timer + 1;
                        if (timer > maxPassesEqu)
                        { //just in case my math is wrong this is a safeway to abort it.
                            suddenlyBreak = true;
                            i = maxPassesEqu + 1;
                            q = 2;
                        }

                        while (wL > 1)
                        {
                            k = (rnd.Next(0, wL));
                            wL--;
                            string value = shuffleWordList[k];
                            shuffleWordList[k] = shuffleWordList[wL];
                            shuffleWordList[wL] = value;
                        }
                        wL = wordLength;
                        int r = rnd.Next(mWL, wL);
                        while (wL > r)
                        {
                            if (shuffleWordList.Count < mWL)
                            {
                                q = 2;
                                suddenlyBreak = true;
                            }
                            else
                            {
                                shuffleWordList.RemoveAt(r);
                                wL--;
                            }
                        }
                        if (!compareWordList.Contains(shuffleWordList.ToString()))
                        {
                            q++;
                        }
                    }
                    if (suddenlyBreak == false)
                    {
                        //Creates a string that is used when compared against compareWordList
                        foreach (var item in shuffleWordList)
                        {
                            compareString = compareString + item;
                        }

                        if (compareString.Length <= wordLength && compareString.Length >= minWordLength && !compareWordList.Contains(compareString))
                        {
                            string dupeWord = "";
                            dupeWord = compareString;
                            DictionaryCheck localDict = new DictionaryCheck();
                            dupeWord = localDict.EngDictionary(dupeWord);
                            if (dupeWord == compareString)
                            {
                                Console.WriteLine(compareString);
                                writeFile.WriteLine("REAL WORD: " + compareString.ToString());
                                realWordCount++;
                            }
                            else
                            {
                                if (fakeWords == false)
                                {
                                    writeFile.WriteLine("FAKE WORD: " + compareString.ToString());
                                    Console.WriteLine(compareString);
                                }
                            }
                            //Add the string that passed the above comparision to the list so that it may no longer be taken.
                            compareWordList.Add(compareString);
                            i++;
                        }
                        //Clear string so we get a fresh start above
                        compareString = "";
                    }
                }

                if (realWordCount == 0)
                {
                    Console.WriteLine(compareWordList.Count + " entries were found. None of them were real words");
                }
                if (realWordCount > 0)
                {
                    Console.WriteLine(compareWordList.Count + " entries were found. Out of these " + realWordCount + " entries were real words.");
                }

                Console.WriteLine("");
                Console.WriteLine("Would you like try a different word?");
                Console.WriteLine("Yes/No");

                userDifferentWord = Console.ReadLine();
                userDifferentWord = userDifferentWord.Trim();
                userDifferentWord = userDifferentWord.ToLower();

                if (userDifferentWord == "yes" || userDifferentWord == "ye" || userDifferentWord == "y")
                {
                    //return to default, then force a loop of program
                    userWord = "null";
                    compareWordList.Clear();
                    realWordCount = 0;
                    compareString = "null";
                    fakeWords = true;
                    //dupeWord = "null";
                    //inputFilePath = "null";
                    suddenlyBreak = false;
                    shuffleWordList.Clear();
                    wL = 0;
                    wordList.Clear();
                    mWL = 0;
                    wordLength = 0;
                    optionFakeWords = "null";
                    userDifferentWord = "yes";
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Goodbye");
                    Environment.Exit(0);
                }
            }
        }
    }
}
