using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PersonagemController : MonoBehaviour
{
    public GameObject imgPoderVoar;
    public GameObject imgPoderEscudo;
    public GameObject imgPoderPulor;
    public GameObject painelGameOver;
    public Text txtAltitude;
    public Text txtMoedas;
    public GameObject coelho;
    public GameObject koala;
    public GameObject rapoza;
    private PlayerController player;
    // Start is called before the first frame update
    void Awake() {
        print(AppDAO.getInstance().loadInt(AppDAO.PERSONAGEM_ATUAL) );
        if(AppDAO.getInstance().loadInt(AppDAO.PERSONAGEM_ATUAL) == 1) {
            Instantiate (coelho, new Vector3(0,0,0), this.gameObject.transform.rotation);
        } else if(AppDAO.getInstance().loadInt(AppDAO.PERSONAGEM_ATUAL) == 2) {
            Instantiate (koala, new Vector3(0,0,0), this.gameObject.transform.rotation);
        } else if(AppDAO.getInstance().loadInt(AppDAO.PERSONAGEM_ATUAL) == 3) {
            Instantiate (rapoza, new Vector3(0,0,0), this.gameObject.transform.rotation);
        }
        player = FindObjectOfType(typeof(PlayerController)) as PlayerController;
    }
    void Start()
    {
    }

    public void btnMove(int diracao){
      player.btnMove(diracao);
   }
   public void restartCurrentScene(){
         Scene scene = SceneManager.GetActiveScene(); 
         SceneManager.LoadScene(scene.name);
     }
    
   public void jmenu() {
        //AdMobController.getInstance().DestroyBanner();
        SceneManager.LoadSceneAsync("MenuPersonagem");
    }

}
