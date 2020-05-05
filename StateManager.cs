using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    public InputField DayInput;
    public InputField SubjectIDInput;

    public Text LetterText;
    public Text PhonemeText;

    public GameObject CueFamLetterLabel;
    public GameObject CueFamPhonemeLabel;
    public GameObject PhonemeFamLetterLabel;
    public GameObject PhonemeFamPhonemeLabel;
    public GameObject PCPALetterLabel;
    public GameObject PCPAPhonemeLabel;



    public Button CollectInfoButton;
    public Button StartButton;
   
    static public int Day = 0;
    static public string SubjectID = "";

    public static List<string> ActiveSets = new List<string>();

    static public string state = "Menu";
    static public string nextstate = "CueLearning";
    static public string CurrentPage = "Menu";

    static public bool demo = false;

    // Settings
    static public bool CAF = false;
    static public bool AudioOn = false;
   
    static public bool letters = false;

    static public string DataType = "Phoneme";
    
    // Use this for initialization
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Day == 0 || SubjectID == "")
        {
            StartButton.enabled = false;
        }
        else
        {
            StartButton.enabled = true;
        }
    }
    
    public void ChangeMode()
    {
        if (letters == true)
        {
            letters = false;
            LetterText.enabled = true;
            PhonemeText.enabled = false;
            CueFamLetterLabel.SetActive(true);
            CueFamPhonemeLabel.SetActive(false);
            PhonemeFamLetterLabel.SetActive(true);
            PhonemeFamPhonemeLabel.SetActive(false);
            PCPALetterLabel.SetActive(true);
            PCPAPhonemeLabel.SetActive(false);

        } 
        else
        { 
            letters = true;
            LetterText.enabled = false;
            PhonemeText.enabled = true;
            CueFamLetterLabel.SetActive(true);
            CueFamPhonemeLabel.SetActive(false);
            PhonemeFamLetterLabel.SetActive(true);
            PhonemeFamPhonemeLabel.SetActive(false);
            PCPALetterLabel.SetActive(true);
            PCPAPhonemeLabel.SetActive(false);
        }
        
        
    }

    public void CollectInfo()
    {
        Day = Int32.Parse(DayInput.text);
        SubjectID = SubjectIDInput.text;
        DataManager.filePath += "/Subject" + SubjectID + "/Day" + Day + "/S" + SubjectID + "D" + Day + "_" + DataManager.DateAndTime + ".csv";
        DataManager.DirectoryPath = @"C:\data\NWSE" + "/Subject" + SubjectID + "/Day" + Day;
        CollectInfoButton.enabled = false;
    }

    public void EnterMenu()
    {
        CurrentPage = "Menu";
        nextstate = "Menu";
        CueManager.iteration = 0;
        CueManager.CurrentWordPhonemeNum = 0;
        DataManager.CurrentCorrect = 0;
        DataManager.CurrentTotal = 0;
        TimeManager.SectionTimer = -1;
        TimeManager.SevMinFlag = false;
        //CueManager.SelectedPhoneme = null;
        //CueManager.SelectedWord = null;
    }

    public void EnterCueLearning()
    {
        nextstate = "CueLearning";

        // Settings
        CAF = true;
        AudioOn = true;
    }

    public void EnterCueAssess()
    {
        nextstate = "CueAssess";
        CAF = true;
        AudioOn = true;
    }

    public void EnterPhonemeLearning()
    {
        nextstate = "PhonemeLearning";
        CAF = true;
        AudioOn = true;
    }

    public void EnterPhonemeAssess()
    {
        nextstate = "PhonemeAssess";
        CAF = true;
        AudioOn = true;
    }

    public void EnterIntroWordAssess()
    {
        nextstate = "IntroWordAssess";
        CAF = true;
        AudioOn = true;
    }

    public void EnterWordAssess()
    {
        nextstate = "WordAssess";
        CAF = true;
        AudioOn = true;
    }

	public void EnterWordAssessFillIn() {
		nextstate = "WordAssessFillIn";
		CAF = true;
		AudioOn = true;
	}

	public void EnterReview()
    {
        nextstate = "Review";
        CAF = true;
        AudioOn = true;
    }

    public void EnterPreTest()
    {
        nextstate = "PreTest";
        CAF = false;
        AudioOn = false;
        DataManager.PreTestResults = new List<int>(new int[CueManager.AvailablePhonemes.Count]);
        DataManager.PreTestCount = new List<int>(new int[CueManager.AvailablePhonemes.Count]);
    }

    public void EnterPostTest()
    {
        nextstate = "PostTest";
        CAF = false;
        AudioOn = false;
    }

    public void EnterFinalTest()
    {
        nextstate = "FinalTest";
        CAF = false;
        AudioOn = false;
    }

	public void EnterFinalTestFillIn() {
		nextstate = "FinalTestFillIn";
		CAF = false;
		AudioOn = false;
	}
}