using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaController : MonoBehaviour
{
    public  float PROXIMO_ESPACO = 16f;
    public float espacoBackgGround = 16f;
    public float espacoPlataforma = 1.5f;
    public List<GameObject> plataformas;
    float yPlataforma = 0;
    // Start is called before the first frame update
    void Start()
    {
        gerarPlataformas(12);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gerarPlataformas(int qtd) {
        
        for (int i = 0; i < qtd; i++)
        {
            Instantiate (plataformaAleatoria(), new Vector3(0,yPlataforma,0), this.gameObject.transform.rotation);
            ;
            yPlataforma+=espacoPlataforma;
        }
    }

    private GameObject plataformaAleatoria() {
        int i = Random.Range(0,5);
        //caso o i for 5 pegou a TRAP mais difficil, ai tem a chance de não pegar novamente, Melhorar isso
        if(i == 5) {
            i = Random.Range(0,5);
        }
        return plataformas[i];
    }

    public void criarProximoBackGround() {
        espacoBackgGround += PROXIMO_ESPACO;
        GameObject gobj =  Instantiate (GameObject.Find("BackGroundProximo"), new Vector3(0,espacoBackgGround,0), this.gameObject.transform.rotation);
        gobj.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gerarPlataformas(13);
    }
}
