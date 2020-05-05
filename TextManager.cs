using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    public GameObject CorrectCue;
    public GameObject YourCue;
    public GameObject PhonemeCheckPA;

    public Text ScoreText;
    public Text PageText;
    public Text StateText;
    public Text InstructionsText;

	public InputField FinalFillIn;
	public InputField PractFillIn;

	private float InstructionsHeightPhonemes = 3.8f;
    private float InstructionsHeightRest = 1.75f;

    public static List<string> PhonemePages = new List<string>() {"ChoosePhoneme", "PhonemeFam", "CueFam", "PhonemeCheck", "CueAssess"};
    public static List<string> WordPages = new List<string>() { "ChooseWord" };

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (PhonemePages.Contains(StateManager.CurrentPage))
        {
            WriteYourCue();
        }
        else if (WordPages.Contains(StateManager.CurrentPage))
        {
            WriteYourWord();
        }
        else
        {
            UnWriteYourCue();
            UnWriteCorrectCue();
        }
        WriteScore();
        WritePage();
        WriteState();
        WriteInstructions();
	}

    private void WriteScore()
    {
        if (StateManager.state != "PreTest" && StateManager.state != "PostTest" && StateManager.state != "FinalTest" && StateManager.state != "FinalTestFillIn")
        {
            ScoreText.text = "Score: " + DataManager.CurrentCorrect + "/" + DataManager.CurrentTotal;
            if (DataManager.CurrentTotal != 0)
            {
                ScoreText.text += " (" + (DataManager.CurrentCorrect / (float)DataManager.CurrentTotal * 100).ToString("0.00") + "%)";
            }
        }
        else
        {
            ScoreText.text = "Iteration: " + DataManager.CurrentTotal;
        }
    }

    private void WritePage()
    {
        //PageText.text = "Current Page: " + StateManager.CurrentPage;
    }

    private void WriteState()
    {
        StateText.text = "Current State: " + StateManager.state;
    }

    private void WriteInstructions()
    {
        if (StateManager.state == "CueLearning")
        {
            InstructionsText.enabled = true;
            InstructionsText.transform.position = new Vector3(InstructionsText.transform.position.x, InstructionsHeightPhonemes, InstructionsText.transform.position.z);
            InstructionsText.text = "Select one of the white or yellow boxes and then click [play selected cue] to render the haptic cue.You have 10 minutes to freely move between this familiarization screen and the self - assessment exercise.";
        }
        else if (StateManager.state == "CueAssess")
        {
            InstructionsText.enabled = true;
            InstructionsText.transform.position = new Vector3(InstructionsText.transform.position.x, InstructionsHeightPhonemes, InstructionsText.transform.position.z);
            InstructionsText.text = "Click [play random cue] to render a cue. Select the corresponding box on the diagram and then [check answer] to see the correct answer. You have 10 minutes to freely move between this self-assessment exercise and the previous familiarization screen.";
        }
        else if (StateManager.state == "PhonemeLearning" || StateManager.state == "Review")
        {
            InstructionsText.enabled = true;
            InstructionsText.transform.position = new Vector3(InstructionsText.transform.position.x, InstructionsHeightPhonemes, InstructionsText.transform.position.z);
            InstructionsText.text = "Select one of the white or yellow boxes and then click [play] to render the haptic cue and hear the phoneme. You have 5 minutes to freely move between this familiarization screen and the self-assessment exercise.";
        }
        else if (StateManager.state == "PhonemeAssess")
        {
            InstructionsText.enabled = true;
            if (PhonemeCheckPA.activeSelf)
            {
                InstructionsText.transform.position = new Vector3(InstructionsText.transform.position.x, InstructionsHeightPhonemes, InstructionsText.transform.position.z);
            }
            else
            {
                InstructionsText.transform.position = new Vector3(InstructionsText.transform.position.x, InstructionsHeightRest, InstructionsText.transform.position.z);
            }
            InstructionsText.text = "Click [next phoneme] to render a random haptic cue. Select the corresponding phoneme on the diagram and then [check answer] to see the correct answer. You have 5 minutes to freely move between this self-assessment exercise and the previous familiarization screen.";
        }
        else if (StateManager.state == "IntroWordAssess")
        {
            InstructionsText.enabled = true;
            InstructionsText.transform.position = new Vector3(InstructionsText.transform.position.x, InstructionsHeightRest, InstructionsText.transform.position.z);
            InstructionsText.text = "A word will be rendered on the haptic device as a series of phonemes, presented one at a time. Click [next phoneme] to feel the first cue, then select the corresponding phoneme from the grid and click [check answer]. Continue until all phonemes in the word have been presented, and then identify the word. Remember that phonemes are based on pronunciation, not spelling.";
        }
        else if (StateManager.state == "WordAssess" || StateManager.state == "WordAssessFillIn" || StateManager.state == "FinalTest" || StateManager.state == "FinalTestFillIn")
        {
            InstructionsText.enabled = true;
            InstructionsText.transform.position = new Vector3(InstructionsText.transform.position.x, InstructionsHeightRest, InstructionsText.transform.position.z);
            InstructionsText.text = "A word will be rendered on the haptic device as a series of phonemes, presented one at a time. Click [next phoneme] to feel each cue(you will not be asked to identify the phoneme). Continue until all phonemes in the word have been presented, and then identify the word. Remember that phonemes are based on pronunciation, not spelling.";
        }
        else if (StateManager.state == "PostTest")
        {
            InstructionsText.enabled = true;
            InstructionsText.transform.position = new Vector3(InstructionsText.transform.position.x, InstructionsHeightRest, InstructionsText.transform.position.z);
            InstructionsText.text = "A word will be rendered on the haptic device as a series of phonemes, presented one at a time.Click[next phoneme] to feel each cue(you will not be asked to identify the phoneme). Continue until all phonemes in the word have been presented, and then identify the word. You will not receive correct answer feedback.";
        }
        else if (StateManager.state == "PreTest")
        {
            InstructionsText.enabled = true;
            InstructionsText.transform.position = new Vector3(InstructionsText.transform.position.x, InstructionsHeightRest, InstructionsText.transform.position.z);
            InstructionsText.text = "Click [next phoneme] to render a random haptic cue. Select the corresponding phoneme on the diagram and then [record answer] to record your answer.";
        }
        else
        {
            InstructionsText.text = "";
            InstructionsText.enabled = false;
        }
    }

    public void WriteYourCue()
    {
        if (CueManager.SelectedPhoneme != null)
        {
            YourCue.GetComponent<Text>().text = DictManager.Phoneme[CueManager.SelectedPhoneme].Text;
        }
        else
        {
            YourCue.GetComponent<Text>().text = "";
        }
    }

    public void UnWriteYourCue()
    {
        YourCue.GetComponent<Text>().text = "";

		PractFillIn.text = "";
		FinalFillIn.text = "";
	}

    public void WriteYourWord()
    {
        if (CueManager.SelectedWord != null)
        {
            YourCue.GetComponent<Text>().text = CueManager.SelectedWord;
        }
        else
        {
            YourCue.GetComponent<Text>().text = "";
        }
    }

    public void WriteCorrectCue()
    {
        CorrectCue.GetComponent<Text>().text = DictManager.Phoneme[CueManager.CorrectPhoneme].Text;
        PlayCorrectAudio();
    }

    public void WriteCorrectWord()
    {
        CorrectCue.GetComponent<Text>().text = CueManager.CurrentWord;
    }

    public void UnWriteCorrectCue()
    {
        CorrectCue.GetComponent<Text>().text = "";
        CueManager.SelectedPhoneme = null;
        CueManager.SelectedWord = null;
    }

    private void PlayCorrectAudio()
    {
        AudioClip myClip = Resources.Load<AudioClip>(DictManager.Phoneme[CueManager.CorrectPhoneme].Audio);
        AudioSource aud = gameObject.GetComponent<AudioSource>();
        aud.clip = myClip;
        aud.Play();
    }
}
