using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour {

    public static List<string> Day              = new List<string>();
    public static List<string> State            = new List<string>();
    public static List<string> Page             = new List<string>();
    public static List<string> Families         = new List<string>();
    public static List<string> RenderingType    = new List<string>();
    public static List<string> ActualRendering  = new List<string>();
    public static List<string> TimeRendered     = new List<string>();
    public static List<string> UserResponse     = new List<string>();
    public static List<string> TimeUserResponse = new List<string>();
    public static List<string> IsCorrect        = new List<string>();

    private string[] headers = { "Day", "State", "Page", "Families", "RenderingType", "ActualRendering", "TimeRendered", "UserResponse", "TimeUserResponse", "IsCorrect" };

    public static List<int> PreTestResults;
    public static List<int> PreTestCount;
    public GameObject PreTestResultsText;

    public static int CurrentCorrect = 0;
    public static int CurrentTotal = 0;

    string delimiter = ",";

    public static string DateAndTime;
    
    public static string filePath = @"C:\data\NWSE";
	public static string DirectoryPath;

	// For final test
	public InputField WordFinalTestFillIn;
	public Button RecordAnswerFillIn;

	// For practice
	public InputField PracticeFillIn;
	public Button CheckAnswerFillIn;

    // Use this for initialization
    void Start () {
        DateAndTime = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
    }
	
	// Update is called once per frame
	void Update () {
		if (WordFinalTestFillIn.text == "") {
			RecordAnswerFillIn.enabled = false;
		}
		else {
			RecordAnswerFillIn.enabled = true;
		}
		if (PracticeFillIn.text == "") {
			CheckAnswerFillIn.enabled = false;
		}
		else {
			CheckAnswerFillIn.enabled = true;
		}
	}

    public void RecordData()
    {
        if (!StateManager.demo)
        {
            Day.Add(StateManager.Day.ToString()); // DAY
            State.Add(StateManager.state); // STATE
            Page.Add(StateManager.CurrentPage); // PAGE
            Families.Add(AddFamilies(StateManager.ActiveSets)); // FAMILES
            RenderingType.Add(GetType(StateManager.CurrentPage)); // RENDERING TYPE
            ActualRendering.Add(GetRendering(RenderingType[RenderingType.Count - 1])); // ACTUAL RENDERING
            TimeRendered.Add(HapticManager.RenderTime.ToString("0.00")); // TIME RENDERED
            if (StateManager.CurrentPage == "ChoosePhoneme" && (StateManager.state == "PostTest" || StateManager.state == "FinalTest" || StateManager.state == "FinalTestFillIn" || StateManager.state == "WordAssess" || StateManager.state == "WordAssessFillIn"))
            {
                UserResponse.Add("NA"); // USER RESPONSE
                TimeUserResponse.Add("NA"); // TIME USER RESPONSE
                IsCorrect.Add("NA"); // IS CORRECT
            }
            else
            {
				if (StateManager.state == "FinalTestFillIn") 
				{
					UserResponse.Add(WordFinalTestFillIn.text); // USER RESPONSE
				}
				else if (StateManager.state == "WordAssessFillIn") {
					UserResponse.Add(PracticeFillIn.text); // USER RESPONSE
				}
				else {
					UserResponse.Add(GetResponse(RenderingType[RenderingType.Count - 1])); // USER RESPONSE
				}
				TimeUserResponse.Add(TimeManager.timer.ToString("0.00")); // TIME USER RESPONSE
				IsCorrect.Add(GetIsCorrect(ActualRendering[ActualRendering.Count - 1], UserResponse[UserResponse.Count - 1])); // IS CORRECT
			}
            try
            {
                AppendDataToFile();
            }
            catch (UnauthorizedAccessException)
            {

            }
        }
    }

    private void AppendDataToFile()
    {
        StringBuilder data = new StringBuilder();
        if (!File.Exists(filePath))
        {
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }
            data.AppendLine(string.Join(delimiter, headers));
        }
        string[] NewData = { Day[Day.Count - 1],
                             State[State.Count - 1],
                             Page[Page.Count - 1],
                             Families[Families.Count - 1],
                             RenderingType[RenderingType.Count - 1],
                             ActualRendering[ActualRendering.Count - 1],
                             TimeRendered[TimeRendered.Count - 1],
                             UserResponse[UserResponse.Count - 1],
                             TimeUserResponse[TimeUserResponse.Count - 1],
                             IsCorrect[IsCorrect.Count - 1]};
        data.AppendLine(string.Join(delimiter, NewData));
        File.AppendAllText(filePath, data.ToString());
    }

    static public string AddFamilies(List<string> SetsList)
    {
        string sets = "";
        foreach (string set in SetsList)
        {
            sets += set;
        }
        return SortString(sets);
    }

    static public string SortString(string input)
    {
        char[] characters = input.ToArray();
        Array.Sort(characters);
        return new string(characters);
    }

    private string GetIsCorrect(string actual, string user)
    {
        CurrentTotal += 1;
        if (actual == user)
        {
            CurrentCorrect += 1;
            return "true";
        }
        else
        {
            return "false";
        }
    }

    private string GetRendering(string RenderingType)
    {
        if (RenderingType == "Phoneme")
        {
            return CueManager.CorrectPhoneme;
        }
        else if (RenderingType == "Word")
        {
            return CueManager.CurrentWord;
        }
        else
        {
            return "ERROR";
        }
    }

    private string GetResponse(string RenderingType)
    {
        if (RenderingType == "Phoneme")
        {
            return CueManager.SelectedPhoneme;
        }
        else if (RenderingType == "Word")
        {
            return CueManager.SelectedWord;
        }
        else
        {
            return "ERROR";
        }
    }

    private string GetType(string Page)
    {
        if (TextManager.PhonemePages.Contains(Page))
        {
            return "Phoneme";
        }
        else if (TextManager.WordPages.Contains(Page))
        {
            return "Word";
        }
        else
        {
            return "ERROR";
        }
    }

    public void UpdatePretestResults()
    {
        int index = CueManager.AvailablePhonemes.IndexOf(CueManager.CorrectPhoneme);
        if (CueManager.SelectedPhoneme == CueManager.CorrectPhoneme)
        {
            PreTestResults[index] += 1;   
        }
        PreTestCount[index] += 1;
        WritePretestResults();
    }

    private void WritePretestResults()
    {
        PreTestResultsText.GetComponent<Text>().text = "Incorrect Phonemes:\n";
        for (int i = 0; i < CueManager.AvailablePhonemes.Count; i++)
        {
            if (PreTestResults[i] != PreTestCount[i])
            {
                PreTestResultsText.GetComponent<Text>().text += DictManager.Phoneme[CueManager.AvailablePhonemes[i]].Text + ": " + PreTestResults[i].ToString() + "/" + PreTestCount[i].ToString() + "\n";
            }
        } 
    }
}
