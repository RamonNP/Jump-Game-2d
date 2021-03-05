using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   //public Transform groundCheck;
   public float speed = 4;
   public float jumpForce = 200;
   public LayerMask whatIsGround;
 
   [HideInInspector]
   public bool lookingRight = true;
 
   private Rigidbody2D rb2d;
   public bool isGrounded = true;
   private bool jump = false;
 
   void Start () {
      rb2d = GetComponent<Rigidbody2D>();
   }
 
   void Update () {
      inputCheck ();
      move ();
   }
 
   void inputCheck (){
     
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
        if (other.gameObject.CompareTag("PADS"))
        {
           if(isGrounded){
              print("PULO");
               StartCoroutine("jumpPlataform");
               rb2d.AddForce(new Vector2(0, jumpForce));
           } else {
              print("NÂO PULO");
           }
        }else if (other.gameObject.CompareTag("TRAPS"))
        {
          print("MORREU");
        }
    }
   IEnumerator jumpPlataform() {
        isGrounded = false;
        yield return new WaitForSeconds(0.5f);
        isGrounded = true;
    }
  
}
