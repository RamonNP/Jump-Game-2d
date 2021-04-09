using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    private const int KOALA_PRECO = 300;
    private const int RAPOZA_PRECO = 2000;
    public Text txtMoedasShop;
    public Text txtUPAShop;
    public GameObject coelhoJoy;
    public GameObject coelhoAberto;
    public GameObject BtnCoelhoFechado;
    public GameObject txtCoelhoPreco;
    public GameObject KoalaJoy;
    public GameObject koalaAberto;
    public GameObject BtnKoalaFechado;
    public GameObject txtKoalaPreco;
    public GameObject raposaJoy;
    public GameObject raposaAberto;
    public GameObject BtnRaposaFechado;
    public GameObject txtRaposaPreco;
    // Start is called before the first frame update
    void Start()
    {
        AppDAO.getInstance().saveInt(AppDAO.PERSONAGEM_ATUAL, 1);
        txtKoalaPreco.GetComponent<Text>().text = KOALA_PRECO.ToString().PadLeft(4, '0');
        txtRaposaPreco.GetComponent<Text>().text = RAPOZA_PRECO.ToString().PadLeft(4, '0');
        txtMoedasShop.text = AppDAO.getInstance().loadInt(AppDAO.COINS).ToString().PadLeft(5, '0');
        txtUPAShop.text = AppDAO.getInstance().loadInt(AppDAO.SCORE_TOTAL).ToString().PadLeft(5, '0');
        //AppDAO.getInstance().saveInt(AppDAO.COINS, 10000);
        AppDAO.getInstance().saveInt(AppDAO.COELHO, 1);
        //AppDAO.getInstance().saveInt(AppDAO.KOALA, 0);
        //AppDAO.getInstance().saveInt(AppDAO.RAPOZA, 0);
        verificaCompras();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void comprar(int personagem) {
        int moedas = AppDAO.getInstance().loadInt(AppDAO.COINS);
        if(personagem == 2){
            if(moedas > KOALA_PRECO) {
                AudioController.getInstance().tocarFx(AudioController.getInstance().fxSell);
                moedas = moedas - KOALA_PRECO;
                AppDAO.getInstance().saveInt(AppDAO.COINS, moedas);
                AppDAO.getInstance().saveInt(AppDAO.KOALA, 1);
                verificaCompras();
                txtMoedasShop.text = AppDAO.getInstance().loadInt(AppDAO.COINS).ToString().PadLeft(5, '0');
            } else {
                AudioController.getInstance().tocarFx(AudioController.getInstance().fxError);
            }

        }
        if(personagem == 3){
            if(moedas > RAPOZA_PRECO) {
                AudioController.getInstance().tocarFx(AudioController.getInstance().fxSell);
                moedas = moedas - RAPOZA_PRECO;
                AppDAO.getInstance().saveInt(AppDAO.COINS, moedas);
                AppDAO.getInstance().saveInt(AppDAO.RAPOZA, 1);
                verificaCompras();
                txtMoedasShop.text = AppDAO.getInstance().loadInt(AppDAO.COINS).ToString().PadLeft(5, '0');
            } else {
                AudioController.getInstance().tocarFx(AudioController.getInstance().fxError);
            }

        }
    }

    public void verificaCompras() {
        if(AppDAO.getInstance().loadInt(AppDAO.COELHO) == 1){
            coelhoAberto.SetActive(true);
            BtnCoelhoFechado.SetActive(false);
            txtCoelhoPreco.SetActive(false);
        } else {
            coelhoAberto.SetActive(false);
            BtnCoelhoFechado.SetActive(true);
            txtCoelhoPreco.SetActive(true);
        }
        if(AppDAO.getInstance().loadInt(AppDAO.KOALA) == 1){
            koalaAberto.SetActive(true);
            BtnKoalaFechado.SetActive(false);
            txtKoalaPreco.SetActive(false);
        } else {
            koalaAberto.SetActive(false);
            BtnKoalaFechado.SetActive(true);
            txtKoalaPreco.SetActive(true);
        }
        if(AppDAO.getInstance().loadInt(AppDAO.RAPOZA) == 1){
            raposaAberto.SetActive(true);
            BtnRaposaFechado.SetActive(false);
            txtRaposaPreco.SetActive(false);
        } else {
            raposaAberto.SetActive(false);
            BtnRaposaFechado.SetActive(true);
            txtRaposaPreco.SetActive(true);
        }
    }

    public void trocarPersonagem(int personagem){
        //1 Coelho 2 Koala
        if(personagem == 1){
            AppDAO.getInstance().saveInt(AppDAO.PERSONAGEM_ATUAL, 1);
            coelhoJoy.SetActive(true);
            KoalaJoy.SetActive(false);
            raposaJoy.SetActive(false);
        }
        if(personagem == 2){
            AppDAO.getInstance().saveInt(AppDAO.PERSONAGEM_ATUAL, 2);
            coelhoJoy.SetActive(false);
            raposaJoy.SetActive(false);
            KoalaJoy.SetActive(true);
        }
        if(personagem == 3){
            AppDAO.getInstance().saveInt(AppDAO.PERSONAGEM_ATUAL, 3);
            coelhoJoy.SetActive(false);
            KoalaJoy.SetActive(false);
            raposaJoy.SetActive(true);
        }
    }
    public void jogar() {
        AudioController.getInstance().tocarFx(AudioController.getInstance().fxClick);
        SceneManager.LoadSceneAsync("Fase1");
    }
}
