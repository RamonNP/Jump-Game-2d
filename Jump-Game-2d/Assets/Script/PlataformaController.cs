using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaController : MonoBehaviour
{
    public List<GameObject> plataformas;
    // Start is called before the first frame update
    void Start()
    {
        gerarPlataformas();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gerarPlataformas() {
        float y = 0;
        for (int i = 0; i < 15; i++)
        {
            Instantiate (plataformaAleatoria(), new Vector3(0,y,0), this.gameObject.transform.rotation);
            ;
            y+=1.5f;
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
}
