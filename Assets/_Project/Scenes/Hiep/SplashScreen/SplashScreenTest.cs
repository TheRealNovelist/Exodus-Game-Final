using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SplashScreenTest : MonoBehaviour
{
    public RawImage rawImg; //reference to raw image
    public VideoPlayer videoPlayer; //reference to video player
    public string screenToLoad; //reference to name of screen to load string
    [SerializeField] private ASyncLoader _aSyncLoader;

    //Call PlayVideo() below to play video
    //Activate LoadScene() below after done plaing video 
    // Start is called before the first frame update
    void Start()
    {
        //Call PlayVideo() below to play video
        StartCoroutine(PlayVideo());

        //Activate LoadScene() below after done plaing video 
        videoPlayer.loopPointReached += LoadScene;
    }

    //Prepare and play video
    //Called in Start()
    private IEnumerator PlayVideo()
    {
        videoPlayer.Prepare(); //prepare video

        //while the video is not completely prepared
        while (!videoPlayer.isPrepared)
        {
            //yied 0.5 sec
            yield return new WaitForSeconds(0.5f);
            break;
        }

        //draw texture of video to raw img
        rawImg.texture = videoPlayer.texture;

        //play video
        videoPlayer.Play();
    }

    //Load screen
    //Called in Start
    private void LoadScene(VideoPlayer vp)
    {
        //SceneManager.LoadScene("Loading Screen");

        //Call ScenesManager.LoadLoadGameSceneInTime() to load screen after time
        //SceneManager.LoadScene(screenToLoad);
        
        _aSyncLoader.LoadSceneASync(screenToLoad);
    }
}