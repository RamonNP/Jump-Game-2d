using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
   private int altitude;
   public Text txtAltitude;
   public Text txtMoedas;
   private Animator animator;
   PlataformaController plataformaController;
   //public Transform groundCheck;
   public float speed = 4;
   public float jumpForce = 200;
 
   [HideInInspector]
 
   private Rigidbody2D rb2d;
   public bool isGrounded = true;
   private bool jump = false;
 
   void Start () {
      animator = GetComponent<Animator>();
      plataformaController = FindObjectOfType(typeof(PlataformaController)) as PlataformaController;
      rb2d = GetComponent<Rigidbody2D>();
      //QtdFlechasIAP.text = quantidadeFlechas.ToString().PadLeft(4, '0');
   }
 
   void Update () {
      pontuacaoAltitude ();
      move ();
   }
 
   void pontuacaoAltitude (){
      if(altitude < this.gameObject.transform.position.y){
         altitude = (int) this.gameObject.transform.position.y;
         if(altitude < 0){
            altitude = 0;
         }
      }
      txtAltitude.text = altitude.ToString().PadLeft(5, '0');
   }
 
   void move(){
      float horizontalForceButton = Input.GetAxis ("Horizontal");
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
               if(isGrounded){
                  animator.SetTrigger("pulo");
                  StartCoroutine("jumpPlataform");
                  rb2d.AddForce(new Vector2(0, jumpForce));
               } 
               break;
            case "TRAPS":
            break;

       }
    }
    private void OnTriggerEnter2D(Collider2D collision2d)
    {
         switch (collision2d.gameObject.tag)
        {
            case "BackGround":
               print(collision2d.gameObject.tag);
               collision2d.gameObject.GetComponent<BoxCollider2D>().enabled = false;
               plataformaController.criarProximoBackGround();
               break;
        }
    }
   IEnumerator jumpPlataform() {
        isGrounded = false;
        yield return new WaitForSeconds(0.5f);
        isGrounded = true;
    }
    
  
}
