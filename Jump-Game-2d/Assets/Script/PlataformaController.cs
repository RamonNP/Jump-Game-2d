using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaController : MonoBehaviour
{
    public  float PROXIMO_ESPACO = 16f;
    public float espacoBackgGround = 16f;
    public float espacoPlataforma = 1.5f;
    public List<GameObject> coletaveis;
    public List<GameObject> plataformas;
    public GameObject objMoeda;
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
            //bus e instancia as plataformas prefabs
            Instantiate (plataformaAleatoria(), new Vector3(0,yPlataforma,0), this.gameObject.transform.rotation);
            //gera as moedas baseado no yPlataforma;
            gerarMoedas();
            yPlataforma+=espacoPlataforma;
        }
    }
    public float posicaoPlataformaMDE(int i) {
        //os ifs, para posicionamento das moedas, moeio ou nos cantos
        float x = 0f;
        if(i == 0 ){
            x = -1.7f;
        } else
        if(i == 1){
            x = 0f;
        } else 
        if(i == 2){
            x = 1.7f;
        } 
        return x;
    }

    public void gerarMoedas() {
        //randomico para porcentagem das moedas, 
        int i = Random.Range(0,15);
        if(i < 14){
            i = Random.Range(0,3);
            Instantiate (objMoeda, new Vector3( posicaoPlataformaMDE(i),yPlataforma,0), this.gameObject.transform.rotation);
        } else {
            gerarBonus();
        }
    }
    public void gerarBonus() {
        //randomico para porcentagem das moedas, 
        int i = Random.Range(0,15);
        if(i < 5){
            i = Random.Range(0,3);
            GameObject obj =  coletaveis[i];
            i = Random.Range(0,3);
            Instantiate (obj, new Vector3( posicaoPlataformaMDE(i),yPlataforma,0), this.gameObject.transform.rotation);
        }
    }

    private GameObject plataformaAleatoria() {
        int i = Random.Range(0,5);
        return plataformas[i];
    }

    public void criarProximoBackGround() {
        espacoBackgGround += PROXIMO_ESPACO;
        GameObject gobj =  Instantiate (GameObject.Find("BackGroundProximo"), new Vector3(0,espacoBackgGround,0), this.gameObject.transform.rotation);
        gobj.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gerarPlataformas(13);
    }
}
