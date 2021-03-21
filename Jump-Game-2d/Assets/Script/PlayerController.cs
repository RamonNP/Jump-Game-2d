using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
   private int coins;
   public int altitude;
   private Animator animator;
   PlataformaController plataformaController;
   private PersonagemController personagemController;
   //public Transform groundCheck;
   public float speed = 4;
   public float jumpForce = 200;
   private float timeJumpForce = 0.5f;
 
   private Rigidbody2D rb2d;
   public bool isGrounded = true;
   private bool jump = false;
   public GameObject efeitoMoedas;

   private bool poderPulo = false;
   private bool poderEscudo = false;
   private bool poderVoar = false;
   public GameObject objPoderEscudo;
   public GameObject objPoderEscudoVoar;
 
   private bool gameOverBoll;
   public float horizontalForceButton ;
   public Vector2 velocity;
   public float smoothTimeX;
 
   void Start () {
      //AdMobController.getInstance().RequestBanner();
      AudioController.getInstance().trocarMusica(AudioController.getInstance().musicaFase1);
      gameOverBoll = false;
      coins = AppDAO.getInstance().loadInt(AppDAO.COINS);
      animator = GetComponent<Animator>();
      plataformaController = FindObjectOfType(typeof(PlataformaController)) as PlataformaController;
      personagemController = FindObjectOfType(typeof(PersonagemController)) as PersonagemController;
      rb2d = GetComponent<Rigidbody2D>();
      //Inicializando hud modedas
      addCoins(0);
      smoothTimeX = 0.2f;
      print("Reiniciando");
   }
 
   void Update () {
      pontuacaoAltitude ();
      if(!gameOverBoll) {
         //move ();
      }

      Touch touch = simulatess();
      if (Application.platform == RuntimePlatform.Android) {
            try
            {
               touch = Input.GetTouch(0);//simulatess();//Input.GetTouch(0); SEM SIMULADOR
            }
            catch (System.Exception)
            {
               
               //throw;
            }
      } else if (Application.platform == RuntimePlatform.OSXEditor) {
            Input.GetTouch(0); 
      }
      switch (touch.phase)
         {
               case TouchPhase.Began:
               MoveTouch(GetCurrentMousePosition(touch.position).GetValueOrDefault());
               break;

               case TouchPhase.Moved:
               MoveTouch(GetCurrentMousePosition(touch.position).GetValueOrDefault());
               break;

               case TouchPhase.Ended:
               velocity.y = 0f;
               break;

         }
   }
   private Touch simulatess()
    {
        Touch touch = new Touch();
        if (Input.GetMouseButtonDown(0))
        {
            touch = new Touch();
            touch.phase = TouchPhase.Began;
            touch.position = Input.mousePosition;
        } else if (Input.GetMouseButton(0))
        {
            touch = new Touch();
            touch.phase = TouchPhase.Moved;
            touch.position = Input.mousePosition;
        } else if (Input.GetMouseButtonUp(0))
        {
            touch = new Touch();
            touch.phase = TouchPhase.Ended;
            touch.position = Input.mousePosition;
        } else {
            touch = new Touch();
            touch.phase = TouchPhase.Canceled;
        }
        return touch;
    }
   public void MoveTouch(Vector3 releasePosition) {
      float posx = Mathf.SmoothDamp(transform.position.x, releasePosition.x, ref velocity.y, smoothTimeX);
      transform.position = new Vector3(posx, transform.position.y, transform.position.z);
   }
 
   void pontuacaoAltitude (){
      if(altitude < this.gameObject.transform.position.y){
         altitude = (int) this.gameObject.transform.position.y;
         if(altitude < 0){
            altitude = 0;
         }
      }
      personagemController.txtAltitude.text = altitude.ToString().PadLeft(5, '0');
   }
 
   public void btnMove(float diracao){
      print(diracao);
      //float horizontalForceButton = Input.acceleration.x;
      horizontalForceButton = diracao/2;
   }

   
   void move(){
      //float horizontalForceButton = Input.acceleration.x;
      //horizontalForceButton = Input.GetAxis ("Horizontal");
      rb2d.velocity = new Vector2 (horizontalForceButton * speed, rb2d.velocity.y);
      //isGrounded = Physics2D.OverlapCircle (groundCheck.position, 0.15f, whatIsGround);
 

 
      if (jump) {
         rb2d.AddForce(new Vector2(0, jumpForce));
         jump = false;
      }
   }
    void OnCollisionEnter2D(Collision2D other){
            
       switch (other.gameObject.tag)
       {
            case "PADS":
               //correção para não dar super pulo, chamando a corotina que aguar 0.6 segundo
               if(isGrounded && !poderVoar){
                  pular();
               } 
               break;
            case "TRAPS":
               if(isGrounded && !poderVoar){
                 pular();
               } 
               if(!poderEscudo){
                  gameOver();
               }
            break;

       }
    }
    private void pular() {
       if(!gameOverBoll) {
         AudioController.getInstance().tocarFx(AudioController.getInstance().fxJump);
         animator.SetTrigger("pulo");
         StartCoroutine("jumpPlataform");
         rb2d.AddForce(new Vector2(0, jumpForce));
       }
         
    }
    private void OnTriggerEnter2D(Collider2D collision2d)
    {
         switch (collision2d.gameObject.tag)
        {
            case "BackGround":
               //print(collision2d.gameObject.tag);
               collision2d.gameObject.GetComponent<BoxCollider2D>().enabled = false;
               plataformaController.criarProximoBackGround();
               break;
            case "COIN":
               addCoins(1);
               Instantiate (efeitoMoedas, collision2d.gameObject.transform.position, this.gameObject.transform.rotation);
               Destroy(collision2d.gameObject);
               break;
            case "ESCUDO":
               if(!poderVoar && !poderPulo && !poderEscudo){
                  StartCoroutine("PoderEscudo");
                  Destroy(collision2d.gameObject);
               }
               break;
            case "PULO":
               if(!poderVoar && !poderPulo && !poderEscudo){
                  StartCoroutine("PoderPulo");
                  Destroy(collision2d.gameObject);
               }
               break;
            case "VOAR":
               if(!poderVoar && !poderPulo && !poderEscudo){
                  StartCoroutine("PoderVoar");
                  Destroy(collision2d.gameObject);
               }
               break;
        }
    }

    public void addCoins(int qtd) {
       AudioController.getInstance().tocarFx(AudioController.getInstance().fxCoin);
       coins+=qtd;
       personagemController.txtMoedas.text = coins.ToString().PadLeft(5, '0');
       AppDAO.getInstance().saveInt(AppDAO.COINS, coins);
    }
   IEnumerator PoderEscudo() {
      AudioController.getInstance().tocarFx(AudioController.getInstance().fxShield);
      personagemController.imgPoderEscudo.SetActive(true);
      objPoderEscudo.SetActive(true);
      poderEscudo = true;
      yield return new WaitForSeconds(20f);
      objPoderEscudo.SetActive(false);
      poderEscudo = false;
      personagemController.imgPoderEscudo.SetActive(false);
    }
   IEnumerator PoderVoar() {
      AudioController.getInstance().tocarFx(AudioController.getInstance().fxFly);
      AudioController.getInstance().tocarFx(AudioController.getInstance().fxShield);
      personagemController.imgPoderVoar.SetActive(true);
      objPoderEscudoVoar.SetActive(true);
      poderEscudo = true;
      rb2d.gravityScale = -0.2f;
      animator.SetBool("voar", true);
      poderVoar = true;
      isGrounded = false;
      yield return new WaitForSeconds(8f);
      rb2d.gravityScale = 1f;
      yield return new WaitForSeconds(2f);
      objPoderEscudoVoar.SetActive(false);
      poderEscudo = false;
      isGrounded = true;
      animator.SetBool("voar", false);
      poderVoar = false;
      personagemController.imgPoderVoar.SetActive(false);
    }
   IEnumerator PoderPulo() {
         personagemController.imgPoderPulor.SetActive(true);
         poderPulo = true;
         //força do pulo 380, pula 2 plataformas.
         jumpForce = 380;
         //tempo de espera para adicionar o jump force, se for muito baixo o personagem voa
         timeJumpForce = 0.8f;
         yield return new WaitForSeconds(20f);
         timeJumpForce = 0.5f;
         jumpForce = 300;
         poderPulo = false;
         personagemController.imgPoderPulor.SetActive(false);
    }
   IEnumerator jumpPlataform() {
        isGrounded = false;
        yield return new WaitForSeconds(timeJumpForce);
        isGrounded = true;
    }
    public void gameOver() {
       //AdMobController.getInstance().ShowInterstitial();
       AudioController.getInstance().tocarFx(AudioController.getInstance().fxLose);
       int record = AppDAO.getInstance().loadInt(AppDAO.SCORE_TOTAL);
       if(record < altitude) {
          AppDAO.getInstance().saveInt(AppDAO.SCORE_TOTAL, altitude);
       }
       personagemController.painelGameOver.SetActive(true);
       gameOverBoll = true;

    }

    private Vector3? GetCurrentMousePosition(Vector3 pos)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(Vector3.forward, Vector3.zero);

        float rayDistance;
        if (plane.Raycast(ray, out rayDistance))
        {
            return ray.GetPoint(rayDistance);
            
        }

        return null;
    }
    
}
