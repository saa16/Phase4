using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavManager : MonoBehaviour {

    // Main Pages
    public GameObject Menu;
    public GameObject CueFam;
    public GameObject PhonemeFam;
    public GameObject ChoosePhonemePA;
    public GameObject PhonemeCheckPA;
    public GameObject ChoosePhonemeITW;
    public GameObject ChooseWordITW;
    public GameObject ChooseWordWA;
	public GameObject ChooseWordWAFillIn;
	public GameObject ChooseWordPostTest;
    public GameObject ChoosePhonemePreTest;
    public GameObject ChooseWordFinalTest;
	public GameObject ChooseWordFinalTestFillIn;
	public GameObject Navigation;

    // Navigation Buttons
    public GameObject ToMainMenu;
    public GameObject ToPrevPage;
    public GameObject ToSelfAssess;
    public GameObject Status;

    // Cue Stuff
    public GameObject PlaySelectedCue;
    public GameObject PlayRandomCue;
    public GameObject CheckAnswer;

    // Phoneme Assessment
    public GameObject PlayNextPhonemePA;
    public GameObject CheckAnswerPhonemePA;
    public GameObject ReplayCorrectPhonemePA;

    // Word Assessment
    public GameObject GoToWordWA;
    public GameObject PlayNextPhonemeWA;
    public GameObject WordAlphabeticalWA;
    public GameObject ToNextWordWA;
    public GameObject CheckAnswerWordWA;

	// Word Assessment Fill In
	public GameObject WordWAFillIn;
	public GameObject GoToWordWAFillIn;
	public GameObject PlayNextPhonemeWAFillIn;
	public GameObject ToNextWordWAFillIn;
	public GameObject CheckAnswerWordWAFillIn;

	// Pre Test
	public GameObject PhonemesAlphabeticalPreTest;
    public GameObject PlayNextPhonemePreTest;
    public GameObject RecordAnswerPreTest;

    // Intro to words
    public GameObject PlayNextPhonemeITW;
    public GameObject CheckAnswerPhonemeITW;
    public GameObject GoToWordITW;
    public GameObject CheckAnswerWordITW;

    // Post Test
    public GameObject WordAlphabeticalPostTest;
    public GameObject ToNextWordPostTest;
    public GameObject RecordAnswerPostTest;
    public GameObject GoToWordPostTest;
    public GameObject PlayNextPhonemePostTest;

    // Final Test
    public GameObject WordAlphabeticalFinalTest;
    public GameObject ToNextWordFinalTest;
    public GameObject RecordAnswerFinalTest;
    public GameObject GoToWordFinalTest;
    public GameObject PlayNextPhonemeFinalTest;
    public GameObject ToMenuTextFinalTest;

	// Final Test Fill In
	public GameObject WordFinalTestFillIn;
	public GameObject ToNextWordFinalTestFillIn;
	public GameObject RecordAnswerFinalTestFillIn;
	public GameObject GoToWordFinalTestFillIn;
	public GameObject PlayNextPhonemeFinalTestFillIn;
	public GameObject ToMenuTextFinalTestFillIn;

	// Pre Test Text
	public GameObject PreTestResults;
    public GameObject ToMenuTextPreTest;


    //public static List<string> CueFamList = new List<string>() { "CueLearning", "CueAssess" };
    //public static List<string> PhonemeFamList = new List<string>() { "PhonemeLearning" };
    public static List<string> ChoosePhonemeList = new List<string>() { "PhonemeAssess", "IntroWordAssess", "PreTest" };
    public static List<string> ChooseWordList = new List<string>() { "WordAssess", "WordAssessFillIn", "PostTest", "FinalTest", "FinalTestFillIn" };

    // Use this for initialization
    void Start () {
        AllCanvasesOff();

        ToMainMenu.SetActive(false);
        ToPrevPage.SetActive(false);
        ToSelfAssess.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (StateManager.state == "Menu")
        {
            Menu.SetActive(true);
            ToMainMenu.SetActive(false);
            ToPrevPage.SetActive(false);
            ToSelfAssess.SetActive(false);
        }
        else
        {
            ToMainMenu.SetActive(true);
            ToPrevPage.SetActive(false);
            ToSelfAssess.SetActive(true);
        }
    }

    private void AllCanvasesOff()
    {
        Menu.SetActive(false);
        CueFam.SetActive(false);
        PhonemeFam.SetActive(false);
        ChoosePhonemePA.SetActive(false);
        PhonemeCheckPA.SetActive(false);
        ChoosePhonemeITW.SetActive(false);
        ChoosePhonemePreTest.SetActive(false);
        ChooseWordITW.SetActive(false);
        ChooseWordWA.SetActive(false);
		ChooseWordWAFillIn.SetActive(false);
        ChooseWordPostTest.SetActive(false);
        PreTestResults.SetActive(false);
        ChooseWordFinalTest.SetActive(false);
		ChooseWordFinalTestFillIn.SetActive(false);
        Navigation.SetActive(true);
        Status.SetActive(true);
    }

    public void EnterNextState()
    {
        StateManager.state = StateManager.nextstate;
        AllCanvasesOff();
        if (StateManager.state == "PhonemeLearning")
        {
            PhonemeFam.SetActive(true);
            StateManager.CurrentPage = "PhonemeFam";
        }
        else if (StateManager.state == "PhonemeAssess")
        {
            ChoosePhonemePA.SetActive(true);
            StateManager.CurrentPage = "ChoosePhoneme";
        }
        else if (StateManager.state == "IntroWordAssess")
        {
            ChoosePhonemeITW.SetActive(true);
            ToPhonemeAssessITP();
            StateManager.CurrentPage = "ChoosePhoneme";
        }
        else if (StateManager.state == "WordAssess")
        {
            ChooseWordWA.SetActive(true);
            ToWordAssessWA();
            StateManager.CurrentPage = "ChoosePhoneme";
        }
		else if (StateManager.state == "WordAssessFillIn") {
			ChooseWordWAFillIn.SetActive(true);
			ToWordAssessWAFillIn();
			StateManager.CurrentPage = "ChoosePhoneme";
		}
		else if (StateManager.state == "CueLearning")
        {
            CueFam.SetActive(true);
            PlaySelectedCue.SetActive(true);
            PlayRandomCue.SetActive(false);
            CheckAnswer.SetActive(false);
            StateManager.CurrentPage = "CueFam";
        }
        else if (StateManager.state == "PreTest")
        {
            ChoosePhonemePreTest.SetActive(true);
            ToPhonemeAssessPreTest();
            StateManager.CurrentPage = "ChoosePhoneme";
        }

        else if (StateManager.state == "PostTest")
        {
            ChooseWordPostTest.SetActive(true);
            ToWordAssessPostTest();
            StateManager.CurrentPage = "ChoosePhoneme";
        }

        else if (StateManager.state == "FinalTest")
        {
            ChooseWordFinalTest.SetActive(true);
            ToWordAssessFinalTest();
            StateManager.CurrentPage = "ChoosePhoneme";
        }

		else if (StateManager.state == "FinalTestFillIn") {
			ChooseWordFinalTestFillIn.SetActive(true);
			ToWordAssessFinalTestFillIn();
			StateManager.CurrentPage = "ChoosePhoneme";
		}

		else if (StateManager.state == "Review")
        {
            if (!TimeManager.SevMinFlag)
            {
                PreTestResults.SetActive(true);
            }
            PhonemeFam.SetActive(true);
            StateManager.CurrentPage = "PhonemeFam";
        }
    }

    //////////////////////////////////////////////////////////////
    // NAVIGATION
    //////////////////////////////////////////////////////////////

    public void ToAssess()
    {
        if (StateManager.state == "CueLearning")
        {
            ToCueAssess();
        }
        else if (StateManager.state == "PhonemeLearning" || StateManager.state == "Review")
        {
            ToPhonemeAssessPA();
        }
    }

    public void ToCheckAnswer()
    {
        if (StateManager.state == "CueAssess")
        {
            PlayRandomCue.SetActive(false);
            CheckAnswer.SetActive(true);
        }
        else if (StateManager.state == "IntroWordAssess")
        {
            PlayNextPhonemeITW.SetActive(false);
            CheckAnswerPhonemeITW.SetActive(true);
        }
    }

    public void ToCueAssess()
    {
        PlaySelectedCue.SetActive(true);
        CheckAnswer.SetActive(false);
        PlayRandomCue.SetActive(true);
        StateManager.state = "CueAssess";
        StateManager.CurrentPage = "CueFam";
    }

    //////////////////////////////////////////////////////////////
    // INTRO TO WORDS
    //////////////////////////////////////////////////////////////

    public void ToPhonemeAssessITP()
    {
        StateManager.CurrentPage = "ChoosePhoneme";
        if (CueManager.CurrentWord == null)
        {
            ChoosePhonemeITW.SetActive(true);
            PlayNextPhonemeITW.SetActive(true);
            CheckAnswerPhonemeITW.SetActive(false);
            GoToWordITW.SetActive(false);
            ChooseWordITW.SetActive(false);
            StateManager.CurrentPage = "ChoosePhoneme";
        }
        else if (CueManager.CurrentWordPhonemeNum < DictManager.Word[CueManager.CurrentWord].Length)
        {
            ChoosePhonemeITW.SetActive(true);
            PlayNextPhonemeITW.SetActive(true);
            CheckAnswerPhonemeITW.SetActive(false);
            GoToWordITW.SetActive(false);
            ChooseWordITW.SetActive(false);
            StateManager.CurrentPage = "ChoosePhoneme";
        }
        else
        {
            CheckAnswerPhonemeITW.SetActive(false);
            GoToWordITW.SetActive(true);
            StateManager.CurrentPage = "ChoosePhoneme";
            CueManager.CurrentWordPhonemeNum = 0;
        }
    }

    public void ToWordAssessITP()
    {
        ChoosePhonemeITW.SetActive(false);
        PhonemeFam.SetActive(false);
        PlayNextPhonemeITW.SetActive(false);
        CheckAnswerPhonemeITW.SetActive(false);
        CheckAnswerWordITW.SetActive(true);
        ChooseWordITW.SetActive(true);
        StateManager.CurrentPage = "ChooseWord";
    }

    //////////////////////////////////////////////////////////////
    // PHONEME ASSESSMENT
    //////////////////////////////////////////////////////////////

    public void ToPhonemeAssessPA()
    {
        if (StateManager.state != "Review")
        {
            StateManager.state = "PhonemeAssess";
        }
        StateManager.CurrentPage = "ChoosePhoneme";
        ChoosePhonemePA.SetActive(true);
        PlayNextPhonemePA.SetActive(true);
        CheckAnswerPhonemePA.SetActive(false);
        PhonemeCheckPA.SetActive(false);
        PhonemeFam.SetActive(false);
        ReplayCorrectPhonemePA.SetActive(false);
    }

    public void ToPhonemeCheckPA()
    {
        StateManager.CurrentPage = "PhonemeCheck";
        ChoosePhonemePA.SetActive(false);
        PlayNextPhonemePA.SetActive(true);
        CheckAnswerPhonemePA.SetActive(false);
        PhonemeCheckPA.SetActive(true);
        PhonemeFam.SetActive(false);
        ReplayCorrectPhonemePA.SetActive(false);
    }

    //////////////////////////////////////////////////////////////
    // WORD ASSESSMENT
    //////////////////////////////////////////////////////////////

    public void ToWordAssessWA()
    {
        WordAlphabeticalWA.SetActive(false);
        ToNextWordWA.SetActive(true);
        CheckAnswerWordWA.SetActive(false);
        GoToWordWA.SetActive(false);
        PlayNextPhonemeWA.SetActive(true);
        StateManager.CurrentPage = "ChoosePhoneme";
    }

    public void ToWordWA()
    {
        if (CueManager.CurrentWordPhonemeNum >= DictManager.Word[CueManager.CurrentWord].Length)
        {
            GoToWordWA.SetActive(true);
            PlayNextPhonemeWA.SetActive(false);
            CueManager.CurrentWordPhonemeNum = 0;
            StateManager.CurrentPage = "ChooseWord";
        }
    }

	//////////////////////////////////////////////////////////////
	// WORD ASSESSMENT FILL IN
	//////////////////////////////////////////////////////////////

	public void ToWordAssessWAFillIn() {
		WordWAFillIn.SetActive(false);
		ToNextWordWAFillIn.SetActive(true);
		CheckAnswerWordWAFillIn.SetActive(false);
		GoToWordWAFillIn.SetActive(false);
		PlayNextPhonemeWAFillIn.SetActive(true);
		StateManager.CurrentPage = "ChoosePhoneme";
	}

	public void ToWordWAFillIn() {
		if (CueManager.CurrentWordPhonemeNum >= DictManager.Word[CueManager.CurrentWord].Length) {
			GoToWordWAFillIn.SetActive(true);
			PlayNextPhonemeWAFillIn.SetActive(false);
			CueManager.CurrentWordPhonemeNum = 0;
			StateManager.CurrentPage = "ChooseWord";
		}
	}

	//////////////////////////////////////////////////////////////
	// WORD ASSESSMENT POST TEST
	//////////////////////////////////////////////////////////////

	public void ToWordAssessPostTest()
    {
        WordAlphabeticalPostTest.SetActive(false);
        ToNextWordPostTest.SetActive(true);
        RecordAnswerPostTest.SetActive(false);
        GoToWordPostTest.SetActive(false);
        PlayNextPhonemePostTest.SetActive(true);
        StateManager.CurrentPage = "ChoosePhoneme";
    }

    public void ToWordPostTest()
    {
        if (CueManager.CurrentWordPhonemeNum >= DictManager.Word[CueManager.CurrentWord].Length)
        {
            GoToWordPostTest.SetActive(true);
            PlayNextPhonemePostTest.SetActive(false);
            CueManager.CurrentWordPhonemeNum = 0;
            StateManager.CurrentPage = "ChooseWord";
        }
    }

    //////////////////////////////////////////////////////////////
    // WORD ASSESSMENT FINAL TEST
    //////////////////////////////////////////////////////////////

    public void ToWordAssessFinalTest()
    {
        if (CueManager.iteration < 50)
        {
            WordAlphabeticalFinalTest.SetActive(false);
            ToNextWordFinalTest.SetActive(true);
            RecordAnswerFinalTest.SetActive(false);
            GoToWordFinalTest.SetActive(false);
            PlayNextPhonemeFinalTest.SetActive(true);
            ToMenuTextFinalTest.SetActive(false);
            StateManager.CurrentPage = "ChoosePhoneme";
        }
        else
        {
            ToMenuTextFinalTest.SetActive(true);
            WordAlphabeticalFinalTest.SetActive(false);
            ToNextWordFinalTest.SetActive(false);
            RecordAnswerFinalTest.SetActive(false);
            GoToWordFinalTest.SetActive(false);
            PlayNextPhonemeFinalTest.SetActive(false);
        }
    }

    public void ToWordFinalTest()
    {
        if (CueManager.CurrentWordPhonemeNum >= DictManager.Word[CueManager.CurrentWord].Length)
        {
            GoToWordFinalTest.SetActive(true);
            PlayNextPhonemeFinalTest.SetActive(false);
            CueManager.CurrentWordPhonemeNum = 0;
            StateManager.CurrentPage = "ChooseWord";
        }
    }

	//////////////////////////////////////////////////////////////
	// WORD ASSESSMENT FINAL TEST
	//////////////////////////////////////////////////////////////

	public void ToWordAssessFinalTestFillIn() {
		if (CueManager.iteration < 75) {
			WordFinalTestFillIn.SetActive(false);
			ToNextWordFinalTestFillIn.SetActive(true);
			RecordAnswerFinalTestFillIn.SetActive(false);
			GoToWordFinalTestFillIn.SetActive(false);
			PlayNextPhonemeFinalTestFillIn.SetActive(true);
			ToMenuTextFinalTestFillIn.SetActive(false);
			StateManager.CurrentPage = "ChoosePhoneme";
		}
		else {
			ToMenuTextFinalTestFillIn.SetActive(true);
			WordFinalTestFillIn.SetActive(false);
			ToNextWordFinalTestFillIn.SetActive(false);
			RecordAnswerFinalTestFillIn.SetActive(false);
			GoToWordFinalTestFillIn.SetActive(false);
			PlayNextPhonemeFinalTestFillIn.SetActive(false);
		}
	}

	public void ToWordFinalTestFillIn() {
		if (CueManager.CurrentWordPhonemeNum >= DictManager.Word[CueManager.CurrentWord].Length) {
			GoToWordFinalTestFillIn.SetActive(true);
			PlayNextPhonemeFinalTestFillIn.SetActive(false);
			CueManager.CurrentWordPhonemeNum = 0;
			StateManager.CurrentPage = "ChooseWord";
		}
	}

	//////////////////////////////////////////////////////////////
	// PHONEME ASSESSMENT PRETEST
	//////////////////////////////////////////////////////////////

	public void ToPhonemeAssessPreTest()
    {
        if (CueManager.iteration < CueManager.AvailablePhonemes.Count * 2)
        {
            StateManager.CurrentPage = "ChoosePhoneme";
            ChoosePhonemePreTest.SetActive(true);
            PlayNextPhonemePreTest.SetActive(true);
            RecordAnswerPreTest.SetActive(false);
            ToMenuTextPreTest.SetActive(false);
            PhonemesAlphabeticalPreTest.SetActive(true);
        }
        else
        {
            ToMenuTextPreTest.SetActive(true);
            PlayNextPhonemePreTest.SetActive(false);
            RecordAnswerPreTest.SetActive(false);
            PhonemesAlphabeticalPreTest.SetActive(false);
        }
    }
}
