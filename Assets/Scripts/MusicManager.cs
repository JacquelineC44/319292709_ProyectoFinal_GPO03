using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource battleMusic;

    void Start()
    {
        backgroundMusic.loop = true;
        battleMusic.loop = true;

        backgroundMusic.Play();
    }

    public void StartBattleMusic()
    {
        if (backgroundMusic.isPlaying)
            backgroundMusic.Pause();

        if (!battleMusic.isPlaying)
            battleMusic.Play();
    }

    public void StopBattleMusic()
    {
        if (battleMusic.isPlaying)
            battleMusic.Stop();

        if (!backgroundMusic.isPlaying)
            backgroundMusic.UnPause();
    }
}