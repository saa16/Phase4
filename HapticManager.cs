using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class HapticManager : MonoBehaviour {

    private byte ArduinoSend;
    private byte VibrotactorSend;

    private byte[] buff = new byte[2];

    public static float RenderTime;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayRandomPhoneme()
    {
        CueManager.CorrectPhoneme = CueManager.GetRandomPhoneme();
        CommandTactorPulse(DictManager.Phoneme[CueManager.CorrectPhoneme].Cue);
        CommandRockerSqueezePulse(DictManager.Phoneme[CueManager.CorrectPhoneme].Cue);
        RenderTime = TimeManager.timer;
    }

    public void PlayCorrectPhoneme()
    {
        CommandTactorPulse(DictManager.Phoneme[CueManager.CorrectPhoneme].Cue);
        CommandRockerSqueezePulse(DictManager.Phoneme[CueManager.CorrectPhoneme].Cue);
    }

    public void PlaySelectedPhoneme()
    {
        try
        {
            CommandTactorPulse(DictManager.Phoneme[CueManager.SelectedPhoneme].Cue);
            CommandRockerSqueezePulse(DictManager.Phoneme[CueManager.SelectedPhoneme].Cue);
            PlayAudio(CueManager.SelectedAudio);
        }
        catch (ArgumentNullException)
        {
            Debug.Log("Bro, you gotta choose a phoneme.");
            throw;
        }
        
    }

    private void PlayAudio(AudioSource AudioFile)
    {
        AudioFile.Play();
    }

    private void CommandTactorPulse(string cue)
    {
        string TactorString = CueToTactorString(cue);
        byte[] data = Encoding.ASCII.GetBytes(TactorString);
        DeviceManager.sending_socket.SendTo(data, data.Length, SocketFlags.None, DeviceManager.sending_end_point);
    }

    public void CommandRockerSqueezePulse(string cue)
    {
        buff[0] = CueToStretchSqueezeByte(cue.Substring(2, 1)); // byte stretch
        buff[1] = CueToStretchSqueezeByte(cue.Substring(3, 1)); // byte squeeze
        try
        {
            DeviceManager.serial1.Write(buff, 0, 2);
        }
        catch (InvalidOperationException)
        {
            
        }
    }

    public void PlayWordPhoneme()
    {
        //if (CueManager.CurrentWordPhonemeNum == 0)
        //{
        //    CueManager.CurrentWord = CueManager.GetRandomWord();
        //}
        if (CueManager.CurrentWordPhonemeNum == 0)
        {
            string ActiveSets = DataManager.AddFamilies(StateManager.ActiveSets);

            CueManager.CurrentWord = CueManager.GetRandomWord();
            if (StateManager.state == "IntroWordAssess")
            {
                int count = 0;
                while (!DictManager.Word[CueManager.CurrentWord].SetsReq.Contains(ActiveSets[ActiveSets.Length - 1].ToString()))
                {
                    count++;
                    CueManager.CurrentWord = CueManager.GetRandomWord();
                    if(count > 1000){
                        break;
                    }
                }
            }
        }
		Debug.Log(CueManager.CurrentWord);
		Debug.Log(CueManager.CurrentWordPhonemeNum);
		CueManager.CorrectPhoneme = DictManager.Word[CueManager.CurrentWord].Phonemes[CueManager.CurrentWordPhonemeNum];
        string cue = DictManager.Phoneme[CueManager.CorrectPhoneme].Cue;
        CommandRockerSqueezePulse(cue);
        CommandTactorPulse(cue);
        RenderTime = TimeManager.timer;
        CueManager.CurrentWordPhonemeNum += 1;
    }

    public void RePlayWordPhoneme()
    {
        if (CueManager.CurrentWordPhonemeNum != 0)
        {
            CueManager.CorrectPhoneme = DictManager.Word[CueManager.CurrentWord].Phonemes[CueManager.CurrentWordPhonemeNum - 1];
        }
        else
        {
            CueManager.CorrectPhoneme = DictManager.Word[CueManager.CurrentWord].Phonemes[DictManager.Word[CueManager.CurrentWord].Length-1];
        }
        string cue = DictManager.Phoneme[CueManager.CorrectPhoneme].Cue;
        CommandRockerSqueezePulse(cue);
        CommandTactorPulse(cue);
        //CueManager.CorrectAudio.Play();
    }

    private string CueToTactorString(string cue)
    {
        string TactorString;
        switch (cue.Substring(0,1))
        {
            case "T":
                TactorString = "1";
                break;
            case "R":
                TactorString = "2";
                break;
            case "B":
                TactorString = "3";
                break;
            case "L":
                TactorString = "4";
                break;
            default:
                TactorString = "ERROR";
                break;
        }
        switch (cue.Substring(1, 1))
        {
            case "L":
                TactorString += "0";
                break;
            case "H":
                TactorString += "1";
                break;
            case "D":
                TactorString += "2";
                break;
            default:
                TactorString = "ERROR";
                break;
        }
        return TactorString;
    }

    private byte CueToStretchSqueezeByte(string StretchSqueeze)
    {
        byte StretchSqueezeByte;
        switch (StretchSqueeze)
        {
            case "N":
                StretchSqueezeByte = Convert.ToByte("0"[0]);
                break;
            case "S":
                StretchSqueezeByte = Convert.ToByte("1"[0]);
                break;
            default:
                StretchSqueezeByte = Convert.ToByte("0"[0]);
                break;
        }
        return StretchSqueezeByte;
    }
}
