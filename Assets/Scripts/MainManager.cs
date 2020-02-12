using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MainManager : MonoBehaviour
{
    public RectTransform OptionPanel;
    public VideoPlayer mainPlayer;
    public VideoClip VideoTitle;
    public VideoClip VideoContent;

    bool isPlaying = false;

    int localPlayTime = 0;

    void Start()
    {
        SignalAdapter.OnRecieveRun += RecieveRunSignal;
        mainPlayer.loopPointReached += (vp) =>
        {
            if (vp.clip == VideoContent)
                TurnToTitleClip();
        };

        RunBatCmd.CreateBatFile();
        StartCoroutine(DelayCloseOption());
    }

    IEnumerator DelayCloseOption()
    {
        yield return new WaitForSeconds(5);
        OptionPanel.gameObject.SetActive(false);
    }

    void RecieveRunSignal()
    {
        if (isPlaying)
            return;

        mainPlayer.clip = VideoContent;
        mainPlayer.Play();
        isPlaying = true;
        localPlayTime++;
        Debug.Log(">> Playing Content");
        GooglePostTool.Post(new object[] { localPlayTime });
    }

    void TurnToTitleClip()
    {
        isPlaying = false;
        mainPlayer.clip = VideoTitle;
        mainPlayer.Play();

        Debug.Log(">> Content finished. Back to title");
    }
}
