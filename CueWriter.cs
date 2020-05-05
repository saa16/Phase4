using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CueWriter : MonoBehaviour {

    static public List<string> VisibleWordsSorted = new List<string>();
    
    private string cue;
    public GameObject PhonemeListPA;
    public GameObject PhonemeListCue;
    public GameObject PhonemeListFam;
    //public GameObject CueList;
    public GameObject SortedPhonemeListPreTest;
    public GameObject SortedPhonemeListPA;
    public GameObject SortedPhonemeListITW;
    public GameObject SortedWordListITW;
    public GameObject SortedWordListWA;
    public GameObject SortedWordListPostTest;
    public GameObject SortedWordListFinalTest;
    AudioSource aud;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

    }

    private string GetCue(Transform Phoneme)
    {
        string Mode = Phoneme.name;
        string Stretch = Phoneme.transform.parent.name;
        string Location = Phoneme.transform.parent.parent.name;
        string Squeeze = Phoneme.transform.parent.parent.parent.name;
        cue =  Location.Substring(0, 1) + Mode.Substring(0, 1) + Stretch.Substring(0, 1) + Squeeze.Substring(0, 1);
        return cue;
    }

    private void WritePhonemeSpatial(Transform Mode)
    {
        Debug.Log(Mode);
        string cue = GetCue(Mode);
        string Phoneme = DictManager.Cue[cue];
        if (DictManager.Cue[cue] != "NONE" && CueManager.AvailablePhonemes.Contains(Phoneme) && DictManager.Phoneme[Phoneme].Active)
        {
            string PhonemeText = DictManager.Phoneme[Phoneme].Text + "\n" + DictManager.Phoneme[Phoneme].ExampleWord;
            Mode.GetComponentInChildren<Text>().text = PhonemeText;
            AddAudio(Mode, Phoneme);
            if (cue.Substring(2, 1) == "S")
            {
                Mode.GetComponent<Button>().image.color = new Color(1f, 1f, 0.75f);
            }
            if (cue.Substring(3, 1) == "S")
            {
                Mode.GetComponentInChildren<Text>().color = new Color(0f, 0.25f, 0f);
            }
            Mode.GetComponent<Button>().interactable = true;
        }
        else
        {
            Mode.GetComponent<Button>().image.color = new Color(1f, 1f, 1f);
            string PhonemeText = " ";
            Mode.GetComponentInChildren<Text>().text = PhonemeText;
            Mode.GetComponent<Button>().interactable = false;
        }
    }

    private void WritePhonemesAlphabetical(Transform Number)
    {
        if (Int32.Parse(Number.name) < CueManager.SortedPhonemes.Count)
        {
            int iteration = Int32.Parse(Number.name);
            string Phoneme = CueManager.SortedPhonemes[iteration];
            if (DictManager.Cue[DictManager.Phoneme[Phoneme].Cue] != "NONE" && DictManager.Phoneme[Phoneme].Active)
            {
                string PhonemeText = DictManager.Phoneme[Phoneme].Text + "\n" + DictManager.Phoneme[Phoneme].ExampleWord;
                Number.GetComponentInChildren<Text>().text = PhonemeText;
                AddAudio(Number, Phoneme);
                Number.GetComponent<Button>().interactable = true;
            }  
        }
        else
        {
            string PhonemeText = " ";
            Number.GetComponentInChildren<Text>().text = PhonemeText;
            Number.GetComponent<Button>().interactable = false;
        }
    }

    private void WriteWordsAlphabetical(Transform Number)
    {
        if (Int32.Parse(Number.name) < CueManager.SortedWords.Count)
        {
            int iteration = Int32.Parse(Number.name);
            string Word = CueManager.SortedWords[iteration];
            

            string WordText = Word; //+ "\n" + DictManager.Phoneme[Phoneme].ExampleWord;
            Number.GetComponentInChildren<Text>().text = WordText;
            //AddAudio(Number, Phoneme);
            Number.GetComponent<Button>().interactable = true;
        }
        else
        {
            string WordText = " ";
            Number.GetComponentInChildren<Text>().text = WordText;
            Number.GetComponent<Button>().interactable = false;
        }
    }

    private void WriteVisibleWordsAlphabetical(Transform Number)
    {
        if (Int32.Parse(Number.name) < VisibleWordsSorted.Count)
        {
            int iteration = Int32.Parse(Number.name);
            string Word = VisibleWordsSorted[iteration];


            string WordText = Word; //+ "\n" + DictManager.Phoneme[Phoneme].ExampleWord;
            Number.GetComponentInChildren<Text>().text = WordText;
            //AddAudio(Number, Phoneme);
            Number.GetComponent<Button>().interactable = true;
        }
        else
        {
            string WordText = " ";
            Number.GetComponentInChildren<Text>().text = WordText;
            Number.GetComponent<Button>().interactable = false;
        }
    }

    private void AddAudio(Transform Mode, string Phoneme)
    {
        AudioClip myClip = Resources.Load(DictManager.Phoneme[Phoneme].Audio) as AudioClip;
        aud = Mode.GetComponent<AudioSource>();
        aud.clip = myClip;
    }

    public void WriteAllPhonemesPA()
    {
        foreach (Transform Squeeze in PhonemeListPA.transform)
        {
            foreach (Transform Location in Squeeze.transform)
            {
                foreach (Transform Stretch in Location.transform)
                {
                    foreach (Transform Mode in Stretch.transform)
                    {
                        WritePhonemeSpatial(Mode);
                    }
                }
            }
        }

        foreach (Transform Squeeze in PhonemeListFam.transform)
        {
            foreach (Transform Location in Squeeze.transform)
            {
                foreach (Transform Stretch in Location.transform)
                {
                    foreach (Transform Mode in Stretch.transform)
                    {
                        WritePhonemeSpatial(Mode);
                    }
                }
            }
        }
    }

    public void WriteAllPhonemesCue()
    {
        foreach (Transform Squeeze in PhonemeListCue.transform)
        {
            foreach (Transform Location in Squeeze.transform)
            {
                foreach (Transform Stretch in Location.transform)
                {
                    foreach (Transform Mode in Stretch.transform)
                    {
                        WritePhonemeSpatial(Mode);
                    }
                }
            }
        }
    }

    public void WriteAllPhonemesAlphabeticalPA()
    {
        CueManager.SortedPhonemes.Clear();
        CueManager.SortedPhonemes.AddRange(CueManager.AvailablePhonemes);
        CueManager.SortedPhonemes.Sort((x, y) => string.Compare(DictManager.Phoneme[x].Text, DictManager.Phoneme[y].Text));

        foreach (Transform Number in SortedPhonemeListPA.transform)
        {
            WritePhonemesAlphabetical(Number);
        }

        foreach (Transform Number in SortedPhonemeListPreTest.transform)
        {
            WritePhonemesAlphabetical(Number);
        }
    }

    public void WriteAllPhonemesAlphabeticalITP()
    {
        CueManager.SortedPhonemes.Clear();
        CueManager.SortedPhonemes.AddRange(CueManager.AvailablePhonemes);
        CueManager.SortedPhonemes.Sort((x, y) => string.Compare(DictManager.Phoneme[x].Text, DictManager.Phoneme[y].Text));

        foreach (Transform Number in SortedPhonemeListITW.transform)
        {
            WritePhonemesAlphabetical(Number);
        }
    }

    public void WriteAllWordsAlphabetical()
    {
        CueManager.SortedWords.Clear();
        CueManager.SortedWords.AddRange(CueManager.AvailableWords);
        CueManager.SortedWords.Sort();
        //var SortedPhonemes = CueManager.AvailablePhonemes.OrderBy(Phoneme => Phoneme.Text);
        //CueManager.SortedPhonemes.Sort();

        foreach (Transform Number in SortedWordListITW.transform)
        {
            WriteWordsAlphabetical(Number);
        }

        foreach (Transform Number in SortedWordListWA.transform)
        {
            WriteWordsAlphabetical(Number);
        }

        foreach (Transform Number in SortedWordListPostTest.transform)
        {
            WriteWordsAlphabetical(Number);
        }
    }

    public void WriteAllVisibleWordsAlphabetical()
    {
        VisibleWordsSorted.Clear();
        VisibleWordsSorted.Add(CueManager.CurrentWord);
        while (VisibleWordsSorted.Count < 12 && VisibleWordsSorted.Count < CueManager.AvailableWords.Count)
        {
            string RandomAvailableWord = CueManager.AvailableWords[CueManager.rng.Next(CueManager.AvailableWords.Count)];
            if (!VisibleWordsSorted.Contains(RandomAvailableWord))
            {
                VisibleWordsSorted.Add(RandomAvailableWord);
            }
        }
        VisibleWordsSorted.Sort();

        foreach (Transform Number in SortedWordListITW.transform)
        {
            WriteVisibleWordsAlphabetical(Number);
        }

        foreach (Transform Number in SortedWordListWA.transform)
        {
            WriteVisibleWordsAlphabetical(Number);
        }

        foreach (Transform Number in SortedWordListPostTest.transform)
        {
            WriteVisibleWordsAlphabetical(Number);
        }

        foreach (Transform Number in SortedWordListFinalTest.transform)
        {
            WriteVisibleWordsAlphabetical(Number);
        }
        
    }
}
