//using UnityEngine;

//public class MusicManager : MonoBehaviour
//{
//    public AudioSource backgroundMusic;
//    public AudioSource battleMusic;

//    void Start()
//    {
//        backgroundMusic.loop = true;
//        battleMusic.loop = true;

//        backgroundMusic.Play();
//    }

//    public void StartBattleMusic()
//    {
//        if (backgroundMusic.isPlaying)
//            backgroundMusic.Pause();

//        if (!battleMusic.isPlaying)
//            battleMusic.Play();
//    }

//    public void StopBattleMusic()
//    {
//        if (battleMusic.isPlaying)
//            battleMusic.Stop();

//        if (!backgroundMusic.isPlaying)
//            backgroundMusic.UnPause();
//    }
//}
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource backgroundMusic;
    public AudioSource battleMusic;

    private int enemigosViendoJugador = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        backgroundMusic.loop = true;
        battleMusic.loop = true;

        backgroundMusic.Play();
    }

    public void EnemySawPlayer()
    {
        enemigosViendoJugador++;

        if (enemigosViendoJugador == 1)
            StartBattleMusic();
    }

    public void EnemyLostPlayer()
    {
        enemigosViendoJugador--;

        if (enemigosViendoJugador < 0)
            enemigosViendoJugador = 0;

        if (enemigosViendoJugador == 0)
            StopBattleMusic();
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