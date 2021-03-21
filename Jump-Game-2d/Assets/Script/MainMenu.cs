using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void jogar() {
        AudioController.getInstance().tocarFx(AudioController.getInstance().fxClick);
        SceneManager.LoadSceneAsync("MenuPersonagem");
    }
}
