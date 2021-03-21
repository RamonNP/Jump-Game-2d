using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    
    public AudioSource sMusic;  // FONTE DE MUSICA
    public AudioSource sFx;     // FONTE DE EFEITOS SONOROS

    [Header("Musicas")]
    public AudioClip musicaTitulo;
    public AudioClip musicaFase1;
    public AudioClip musicaFase2;
    //public AudioClip musicaTrain;

    [Header("FX")]
    public AudioClip fxClick;
    public AudioClip fxWin;
    public AudioClip fxCoin;
    public AudioClip fxJump;
    public AudioClip fxSell;
    public AudioClip fxError;
    public AudioClip fxShield;
    public AudioClip fxLose;
    public AudioClip fxFly;
    public AudioClip fxDiamante;

    // configurações dos audios
    public float volumeMaximoMusica;
    public float volumeMaximoFx;
    private AudioClip novaMusica;
    public static AudioController instance;
    public static AudioController getInstance() {
        return instance;
    }
    void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if(instance != null) {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        novaMusica=musicaTitulo;
        StartCoroutine("changeMusic");
    }

     IEnumerator changeMusic()
    {
        for(float volume = volumeMaximoMusica; volume >= 0; volume -= 0.1f)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            sMusic.volume = volume;
        }
        sMusic.volume = 0;

        sMusic.clip = novaMusica;
        sMusic.Play();

        for (float volume = 0; volume < volumeMaximoMusica; volume += 0.1f)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            sMusic.volume = volume;
        }
        sMusic.volume = volumeMaximoMusica;

    }
    public void tocarFx(AudioClip fx)
    {
        sFx.PlayOneShot(fx);
    }
    public void trocarMusica(AudioClip musica)
    {
        novaMusica=musica;
        StartCoroutine("changeMusic");
    }
}
