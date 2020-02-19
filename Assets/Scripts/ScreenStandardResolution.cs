using System;
using System.Collections;
using UnityEngine;
using System.Linq;

public class ScreenStandardResolution : MonoBehaviour
{
    public Action OnSwitchScreemMode;
    public string currentStats;
    public bool continueCheckFullScreen = false;
    public float checkPeriod = 30;

    void Start()
    {
        //InitResolution16x9();
        //StartCoroutine(UpdateStatsText());
        if(continueCheckFullScreen)
            StartCoroutine(RepeatChecking());
    }

    void InitResolution16x9(){
        Resolution[] resolutions = GetTwoResolution();
        if (Screen.fullScreen){
            Screen.SetResolution (resolutions[1].width, resolutions[1].height, true);
        } else {
            Screen.SetResolution (resolutions[0].width, resolutions[0].height, false);
        }

        StartCoroutine(UpdateStatsText());
    }

    IEnumerator RepeatChecking(){
        WaitForSeconds sec = new WaitForSeconds(checkPeriod);
        while(true){
            yield return sec;

            Screen.SetResolution (1920, 1080, true);
        }
    }

    IEnumerator UpdateStatsText(){
        //Set Resolution will takes 2 frame
        yield return null;
        yield return null;

        if(Screen.fullScreen) 
            currentStats = "Full Screen Mode";
        else
            currentStats = "Window Mode";
    }

    Resolution[] GetTwoResolution(){
        Resolution[] resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        int max = 0;
        int wind = 0;
        for (int i = resolutions.Length - 1; i >= 0; i--)
        {
            if(resolutions[i].width > Display.main.systemWidth)
                continue;
            if(resolutions[i].height > Display.main.systemHeight)
                continue;
            max = i;
            break;
        }

        if(max - 1 >= 0)
            wind = max - 1;

        int customFullWidth = 0;
        int customFullHeight = 0;

        if(Display.main.systemWidth / (float)Display.main.systemHeight > 1.77f){
            customFullHeight = Display.main.systemHeight;
            customFullWidth = Mathf.FloorToInt((Display.main.systemHeight/9.0f)*16);
        } else {
            customFullWidth = Display.main.systemWidth;
            customFullHeight = Mathf.FloorToInt((Display.main.systemWidth/16.0f)*9);
        }
        
        Resolution[] result = new Resolution[2];
        result[0] = resolutions[wind];
        result[1] = new Resolution(){width = customFullWidth, height = customFullHeight};

        return result;
    }

    public void SwitchDisplayMode(){

        // Resolution[] resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        // int max = resolutions.Length - 1;
        // if (Screen.fullScreen){
        //     Screen.SetResolution (resolutions[max-1].width, resolutions[max-1].height, false);
        // } else {
        //     Screen.SetResolution (resolutions[max].width, resolutions[max].height, true);
        // }

        Resolution[] resolutions = GetTwoResolution();
        if (Screen.fullScreen){
            Screen.SetResolution (resolutions[0].width, resolutions[0].height, false);
        } else {
            Screen.SetResolution (resolutions[1].width, resolutions[1].height, true);
        }

        StartCoroutine(UpdateStatsText());

        OnSwitchScreemMode?.Invoke();
    }
}
