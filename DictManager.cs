using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DictManager : MonoBehaviour {

    public static Dictionary<string, PhonemeData> Phoneme = new Dictionary<string, PhonemeData>();
    public static Dictionary<string, WordData> Word = new Dictionary<string, WordData>();
    public static Dictionary<string, string> Cue = new Dictionary<string, string>();

    public static List<string[]> lineDataCue = new List<string[]>();
    public static List<string[]> lineDataPhoneme = new List<string[]>();
    public static List<string[]> lineDataWord = new List<string[]>();
    public static List<string[]> LineDataFinalTest = new List<string[]>();
	public static List<string[]> LineDataFinalTest5 = new List<string[]>();
	public static List<string[]> LineDataFinalTest6 = new List<string[]>();
	public static List<string[]> LineDataFinalTest7 = new List<string[]>();
	public static List<string[]> LineDataFinalTest8 = new List<string[]>();

	public class PhonemeData
    {
        public string Text; //csv
        public string ExampleWord; //csv
        public string Audio; //csv
        public string Set;
        public List<string> Words;
        public Color Color;
        public string Cue;
        public bool Active = false; // based on which phonemes we are using for deliverable, not what set
        public bool CurrentSet = false; // whether or not the phoneme is in the current active sets
    }

    public class WordData
    {
        public List<string> Phonemes;
        public List<string> SetsReq;
        public int Length;
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InitializeCues() {
        CreatePhoneme();
        CreateCue();
        CreateWord();
        CreateFinalTest();
    }


    private void CreatePhoneme()
    {
        string[] PhonemeData;
        if (StateManager.letters == true)
        {
            PhonemeData = System.IO.File.ReadAllLines(@"PhonemeData\Letter.csv");
        }
        else
        {
            PhonemeData = System.IO.File.ReadAllLines(@"PhonemeData\Phoneme.csv");
        }
        foreach (string line in PhonemeData)
        {
            lineDataPhoneme.Add(line.Trim().Split(","[0]));
        }
        foreach (string[] line in lineDataPhoneme)
        {
            if (TF(line[4]))
            {
                try
                {
                    Phoneme.Add(line[0], new PhonemeData
                    {
                        Text = line[1],
                        ExampleWord = line[2],
                        Audio = line[3],
                        Set = null, // managed in CreateCue
                        Words = null, // will be managed in CreateWords **NOT DONE YET**
                        Color = Color.white,
                        Cue = null, // managed in CreateCue
                        Active = TF(line[4]),
                        CurrentSet = false
                    });
                }
                catch { }
            }
        }
    }
   
    private void CreateCue()
    {
        string[] CueData;
        if (StateManager.letters == true)
        {
            CueData = System.IO.File.ReadAllLines(@"PhonemeData\LettersCue.csv");
            Debug.Log("Letters");
        }
        else
        {
            CueData = System.IO.File.ReadAllLines(@"PhonemeData\Cue.csv");
            Debug.Log("Phonemes");
        }
            
        foreach (string line in CueData)
        {
            lineDataCue.Add(line.Trim().Split(","[0]));
        }
        foreach (string[] line in lineDataCue)
        {
            try
            {
                Cue.Add(line[0], line[1]);
                Phoneme[line[1]].Cue = line[0];
                Phoneme[line[1]].Set = ReturnSet(line[0]);

                CreateSets(line[1]);
            }
            catch { }
        }
    }

    private void CreateWord()
    {
        List<string> PhonemeList;
        string[] WordData = System.IO.File.ReadAllLines(@"PhonemeData\Word.csv");
        foreach (string line in WordData)
        {
            lineDataWord.Add(line.Trim().Split(","[0]));
        }
        foreach (string[] line in lineDataWord)
        {
            int WordLength;
            if (StateManager.letters == true)
            {                
                PhonemeList = GenerateWordListStringLetter(line[0]);
             
                WordLength = PhonemeList.Count;
                           }
            else
            {
                PhonemeList = GenerateWordListString(line);
                WordLength = Int32.Parse(line[1]);
            }


            Word.Add(line[0].Replace("\"", ""), new WordData
            {
                Phonemes = PhonemeList,
                SetsReq = GenerateSets(PhonemeList),
                Length = WordLength
            }) ;
         
        }
    }

    private void CreateFinalTest()
    {
		// Final Test for Day 4
        string[] FinalTestData = System.IO.File.ReadAllLines(@"PhonemeData\FinalTest.csv");
        foreach (string line in FinalTestData)
        {
            LineDataFinalTest.Add(line.Trim().Split(","[0]));
        }
        foreach (string[] line in LineDataFinalTest)
        {
            CueManager.FinalTestList.Add(line[0]);
        }

		// Final Test for Day 5
		string[] FinalTestData5 = System.IO.File.ReadAllLines(@"PhonemeData\FinalTest5.csv");
		foreach (string line in FinalTestData5) {
			LineDataFinalTest5.Add(line.Trim().Split(","[0]));
		}
		foreach (string[] line in LineDataFinalTest5) {
			CueManager.FinalTestList5.Add(line[0]);
		}

		// Final Test for Day 6
		string[] FinalTestData6 = System.IO.File.ReadAllLines(@"PhonemeData\FinalTest6.csv");
		foreach (string line in FinalTestData6) {
			LineDataFinalTest6.Add(line.Trim().Split(","[0]));
		}
		foreach (string[] line in LineDataFinalTest6) {
			CueManager.FinalTestList6.Add(line[0]);
		}

		// Final Test for Day 7
		string[] FinalTestData7 = System.IO.File.ReadAllLines(@"PhonemeData\FinalTest7.csv");
		foreach (string line in FinalTestData7) {
			LineDataFinalTest7.Add(line.Trim().Split(","[0]));
		}
		foreach (string[] line in LineDataFinalTest7) {
			CueManager.FinalTestList7.Add(line[0]);
		}

		// Final Test for Day 8
		string[] FinalTestData8= System.IO.File.ReadAllLines(@"PhonemeData\FinalTest8.csv");
		foreach (string line in FinalTestData8) {
			LineDataFinalTest8.Add(line.Trim().Split(","[0]));
		}
		foreach (string[] line in LineDataFinalTest8) {
			CueManager.FinalTestList8.Add(line[0]);
		}
	}

	private List<string> GenerateSets(List<string> Phonemes)
    {
        List<string> SetList = new List<string>();
        foreach (string Phoneme in Phonemes)
        {
            if (CueManager.SetA.Contains(Phoneme) && !SetList.Contains("A"))
            {
                SetList.Add("A");
            }
            if (CueManager.SetB.Contains(Phoneme) && !SetList.Contains("B"))
            {
                SetList.Add("B");
            }
            if (CueManager.SetC.Contains(Phoneme) && !SetList.Contains("C"))
            {
                SetList.Add("C");
            }
            if (CueManager.SetD.Contains(Phoneme) && !SetList.Contains("D"))
            {
                SetList.Add("D");
            }
        }
        return SetList;
    }

    private List<string> GenerateWordListString(string[] StringList)
    {
        List<string> PhonemeList = new List<string>();
        for (int i = 2; i < 2 + Int32.Parse(StringList[1]); i++)
        {
            PhonemeList.Add(StringList[i].Replace("\"",""));

        }
       
        return PhonemeList;
        
    }

    private List<string> GenerateWordListStringLetter(string word)
    {
        List<string> PhonemeList = new List<string>();
        char[] newletters = word.ToCharArray();
        for (int i = 0; i < newletters.Length; i++)
        {
            PhonemeList.Add(newletters[i].ToString().ToUpper());
            
        }
        foreach (var item in PhonemeList)
        {
            Debug.Log(item);
        }

        return PhonemeList;
    }

    private string ReturnSet(string cue)
    {
        if (cue.Substring(0,1) == "T") // Will change based on set decisions
        {
            return "A";
        }
        else if (cue.Substring(0, 1) == "L") // Will change based on set decisions
        {
            return "B";
        }
        else if (cue.Substring(0, 1) == "B") // Will change based on set decisions
        {
            return "C";
        }
        else if(cue.Substring(0, 1) == "R") // Will change based on set decisions
        {
            return "D";
        }
        else
        {
            return "ERROR";
        }
    }

    private void CreateSets(string phoneme)
    {
        if (Phoneme[phoneme].Set == "A")
        {
            CueManager.SetA.Add(phoneme);
        }
        else if (Phoneme[phoneme].Set == "B")
        {
            CueManager.SetB.Add(phoneme);
        }
        else if (Phoneme[phoneme].Set == "C")
        {
            CueManager.SetC.Add(phoneme);
        }
        else if (Phoneme[phoneme].Set == "D")
        {
            CueManager.SetD.Add(phoneme);
        }
        else
        {
            Debug.Log("ERROR CREATING SETS");
        }
    }

    private bool TF(string YN)
    {
        if (YN == "Y")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
