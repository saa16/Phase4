using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueManager : MonoBehaviour {

    public static List<string> AvailablePhonemes = new List<string>();
    public static List<string> SortedPhonemes = new List<string>();

    public static List<string> AvailableWords = new List<string>();
    public static List<string> SortedWords = new List<string>();

    public static int iteration = 0;

    public static System.Random rng = new System.Random();

    public static string CurrentWord;
    public static int CurrentWordPhonemeNum;

    public static List<string> SetA = new List<string>();
    public static List<string> SetB = new List<string>();
    public static List<string> SetC = new List<string>();
    public static List<string> SetD = new List<string>();

    public static List<string> FinalTestList = new List<string>();
	public static List<string> FinalTestList5 = new List<string>();
	public static List<string> FinalTestList6 = new List<string>();
	public static List<string> FinalTestList7 = new List<string>();
	public static List<string> FinalTestList8 = new List<string>();

	public static string SelectedPhoneme;
    public static string SelectedWord;
    public static AudioSource SelectedAudio;

    public static string CorrectPhoneme;
    public static AudioSource CorrectAudio;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    static public string GetRandomPhoneme()
    {
        if (iteration % AvailablePhonemes.Count == 0)
        {
            Shuffle(AvailablePhonemes);
        }
        string RandomPhoneme = AvailablePhonemes[iteration % AvailablePhonemes.Count];
        if (!StateManager.demo)
        {
            iteration++;
        }
        return RandomPhoneme;
    }

    //static public string GetNextPhonemeInWord()
    //{
        
    //}

    static public string GetRandomWord()
    {
        if (iteration % AvailableWords.Count == 0)
        {
			string temp1 = "";
			for (int i = 0; i < FinalTestList6.Count; i++) {
				temp1 += FinalTestList5[i] + "\n";
			}
			//Debug.Log(temp1);
			Shuffle(AvailableWords);
            Shuffle(FinalTestList);
			Shuffle(FinalTestList5);
			Shuffle(FinalTestList6);
			Shuffle(FinalTestList7);
			Shuffle(FinalTestList8);
			string temp = "";
			for (int i = 0; i < FinalTestList6.Count; i++) {
				temp += FinalTestList5[i] + "\n";
			}
			//Debug.Log(temp);
		}
        string RandomWord;
        if (StateManager.state != "FinalTest" && StateManager.state != "FinalTestFillIn")
        {
            RandomWord = AvailableWords[iteration % AvailableWords.Count];
        }
        else
        {
			if (StateManager.Day == 5) {
				RandomWord = FinalTestList5[iteration % FinalTestList5.Count];
			}
			else if (StateManager.Day == 6) {
				RandomWord = FinalTestList6[iteration % FinalTestList6.Count];
			}
			else if (StateManager.Day == 7) {
				RandomWord = FinalTestList7[iteration % FinalTestList7.Count];
			}
			else if (StateManager.Day >= 8) {
				RandomWord = FinalTestList8[iteration % FinalTestList8.Count];
			}
			else {
				RandomWord = FinalTestList[iteration % FinalTestList.Count];
			}
		}
        
        if (!StateManager.demo)
        {

        }
        iteration++;
        //Debug.Log(RandomWord);
        return RandomWord;
    }

    public void ClearAllSets()
    {
        AvailablePhonemes.Clear();
        StateManager.ActiveSets.Clear();
    }

    public void AddSetA()
    {
        AvailablePhonemes.AddRange(SetA);
        StateManager.ActiveSets.Add("A");
    }

    public void AddSetB()
    {
        AvailablePhonemes.AddRange(SetB);
        StateManager.ActiveSets.Add("B");
    }

    public void AddSetC()
    {
        AvailablePhonemes.AddRange(SetC);
        StateManager.ActiveSets.Add("C");
    }

    public void AddSetD()
    {
        AvailablePhonemes.AddRange(SetD);
        StateManager.ActiveSets.Add("D");
    }

    public void AddWordsA()
    {
        AvailableWords.Clear();
        foreach (KeyValuePair<string, DictManager.WordData> word in DictManager.Word)
        {
            string sets = word.Key;
            foreach (string item in word.Value.SetsReq)
            {
                sets += item;
            }
            //Debug.Log(sets);
            if (!word.Value.SetsReq.Contains("B") && !word.Value.SetsReq.Contains("C") && !word.Value.SetsReq.Contains("D"))
            {
                AvailableWords.Add(word.Key);
            }
        }
    }

    public void AddWordsB()
    {
        AvailableWords.Clear();
        foreach (KeyValuePair<string, DictManager.WordData> word in DictManager.Word)
        {
            if (!word.Value.SetsReq.Contains("A") && !word.Value.SetsReq.Contains("C") && !word.Value.SetsReq.Contains("D"))
            {
                AvailableWords.Add(word.Key);
            }
        }
    }

    public void AddWordsC()
    {
        AvailableWords.Clear();
        foreach (KeyValuePair<string, DictManager.WordData> word in DictManager.Word)
        {
            if (!word.Value.SetsReq.Contains("A") && !word.Value.SetsReq.Contains("B") && !word.Value.SetsReq.Contains("D"))
            {
                AvailableWords.Add(word.Key);
            }
        }
    }

    public void AddWordsD()
    {
        AvailableWords.Clear();
        foreach (KeyValuePair<string, DictManager.WordData> word in DictManager.Word)
        {
            if (!word.Value.SetsReq.Contains("A") && !word.Value.SetsReq.Contains("B") && !word.Value.SetsReq.Contains("C"))
            {
                AvailableWords.Add(word.Key);
            }
        }
    }

    public void AddWordsBandC()
    {
        AvailableWords.Clear();
        foreach (KeyValuePair<string, DictManager.WordData> word in DictManager.Word)
        {
            if (!word.Value.SetsReq.Contains("A") && !word.Value.SetsReq.Contains("D"))
            {
                AvailableWords.Add(word.Key);
            }
        }
    }

    public void AddAllWordsToB()
    {
        AvailableWords.Clear();
        foreach (KeyValuePair<string, DictManager.WordData> word in DictManager.Word)
        {
            if (!word.Value.SetsReq.Contains("C") && !word.Value.SetsReq.Contains("D"))
            {
                AvailableWords.Add(word.Key);
            }
        }
    }

    public void AddAllWordsToC()
    {
        AvailableWords.Clear();
        foreach (KeyValuePair<string, DictManager.WordData> word in DictManager.Word)
        {
            if (!word.Value.SetsReq.Contains("D"))
            {
                AvailableWords.Add(word.Key);
            }
        }
    }

    public void AddAllWordsToD()
    {
        AvailableWords.Clear();
        foreach (KeyValuePair<string, DictManager.WordData> word in DictManager.Word)
        {
            AvailableWords.Add(word.Key);
        }
    }

    private void AddWordToSet()
    {

    }

    public static List<string> Shuffle(List<string> array)
    {
        int n = array.Count;
        while (n > 1)
        {
            int k = rng.Next(n--);
            string temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
        return array;
    }
}
