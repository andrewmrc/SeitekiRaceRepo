using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playbackVideoHandHeld : MonoBehaviour
{
    private string movie = "Fractal Unity.mp4";


    void Start()
    {
        StartCoroutine(streamVideo(movie));
    }

    private IEnumerator streamVideo(string video)
    {
        Handheld.PlayFullScreenMovie(video, Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFill);
        yield return new WaitForEndOfFrame();
        Debug.Log("The Video playback is now completed.");
    }
}
