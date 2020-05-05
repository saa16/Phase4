using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SetCueScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeActivePhoneme()
    {
        CueManager.SelectedAudio = gameObject.GetComponent<AudioSource>();
        string ButtonText = gameObject.GetComponentInChildren<Text>().text;
        string[] PhonemeText = ButtonText.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        string Phoneme = DictManager.Phoneme.FirstOrDefault(x => x.Value.Text == PhonemeText[0]).Key;
        CueManager.SelectedPhoneme = Phoneme;
    }

    public void ChangeActiveWord()
    {
        //CueManager.SelectedAudio = gameObject.GetComponent<AudioSource>();
        string WordText = gameObject.GetComponentInChildren<Text>().text;
        CueManager.SelectedWord = WordText;
    }
}
