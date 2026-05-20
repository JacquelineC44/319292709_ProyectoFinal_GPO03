using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string siguienteEscena = "319292709_ProyectoFinal_GP03ver5";

    void Start()
    {
        videoPlayer.loopPointReached += FinVideo;
    }

    void FinVideo(VideoPlayer vp)
    {
        SceneManager.LoadScene(siguienteEscena);
    }
}
