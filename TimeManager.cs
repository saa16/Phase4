using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public static float SectionTimer = -1;

    public static bool SevMinFlag = false;

    public Text TimerText;
    public Text SectionTimerText;

    public static float timer = 0;
    public string phase = "1 Cue";

    public Toggle Demo;
    public GameObject StatusBar;

    private string MinutesString;
    private string SecondsString;

    private string SectionMinutesString;
    private string SectionSecondsString;

    private float minutes;
    private float seconds;

    private float SectionMinutes;
    private float SectionSeconds;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckDemo();
        UpdateTime();
        TimerText.text = "Current Time: " + MinutesString + ":" + SecondsString;
        if (SectionTimer >= 0)
        {
            SectionTimerText.text = "Section Time Remaining: " + SectionMinutesString + ":" + SectionSecondsString;
        }
        else
        {
            SectionTimerText.text = "";
        }
    }

    public void SevMinFlagOn()
    {
        SevMinFlag = true;
    }

    private void CheckDemo()
    {
        if (Demo.isOn)
        {
            StateManager.demo = true;
            StatusBar.GetComponent<Image>().color = new Color(1f, 0.4f, 0f);
        }
        else
        {
            StateManager.demo = false;
            StatusBar.GetComponent<Image>().color = new Color(.25f, .25f, .25f);
        }
    }

    private void UpdateTime()
    {
        if (!StateManager.demo && StateManager.state != "Menu")
        {
            timer += Time.deltaTime;
        }

        minutes = Mathf.Floor(timer / 60);
        seconds = (int)timer % 60;

        if (minutes < 10)
        {
            MinutesString = "0" + minutes.ToString();
        }
        else
        {
            MinutesString = minutes.ToString();
        }
        if (seconds < 10)
        {
            SecondsString = "0" + seconds.ToString();
        }
        else
        {
            SecondsString = seconds.ToString();
        }

        // FOR SECTION TIMER

        if (!StateManager.demo && StateManager.state != "Menu")
        {
            SectionTimer -= Time.deltaTime;
        }

        SectionMinutes = Mathf.Floor(SectionTimer / 60);
        SectionSeconds = (int)SectionTimer % 60;

        if (SectionMinutes < 10)
        {
            SectionMinutesString = "0" + SectionMinutes.ToString();
        }
        else
        {
            SectionMinutesString = SectionMinutes.ToString();
        }
        if (SectionSeconds < 10)
        {
            SectionSecondsString = "0" + SectionSeconds.ToString();
        }
        else
        {
            SectionSecondsString = SectionSeconds.ToString();
        }
    }

    public void RecordRenderTime()
    {
        HapticManager.RenderTime = timer;
    }

    public void SetSectionTimer()
    {
        if (StateManager.state == "CueLearning")
        {
            SectionTimer = 10 * 60;
        }
        else if (StateManager.state == "PhonemeLearning")
        {
            SectionTimer = 5 * 60;
        }
        else if (StateManager.state == "IntroWordAssess" && StateManager.Day == 1)
        {
            SectionTimer = 3 * 60;
        }
        else if (StateManager.state == "IntroWordAssess" && StateManager.Day != 1)
        {
            SectionTimer = 5 * 60;
        }
        else if (StateManager.state == "WordAssess" && StateManager.Day != 3)
        {
            SectionTimer = 5 * 60;
        }
        else if (StateManager.state == "WordAssess" && StateManager.Day == 3)
        {
            SectionTimer = 10 * 60;
        }
		else if (StateManager.state == "WordAssessFillIn") {
			SectionTimer = 10 * 60;
		}
		else if (StateManager.state == "Review" && StateManager.Day == 2 && SevMinFlag)
        {
            SectionTimer = 7 * 60;
        }
        else if (StateManager.state == "Review" && StateManager.Day == 2 && !SevMinFlag)
        {
            SectionTimer = 5 * 60;
        }
        else if (StateManager.state == "Review" && (StateManager.Day == 3 || StateManager.Day == 4))
        {
            SectionTimer = 10 * 60;
        }
		else if (StateManager.state == "Review" && StateManager.Day >= 5) {
			SectionTimer = 5 * 60;
		}
		else if (StateManager.state == "PostTest")
        {
            SectionTimer = 5 * 60;
        }
        else
        {
            SectionTimer = 0;
        }
    }
}
