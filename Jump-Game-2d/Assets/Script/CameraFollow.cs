using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   private Vector2 velocity;
 
   public float smoothTimeY;
   public float smoothTimeX;
   public float deltaY;
 
   private PlayerController player;
   public GameObject destroyObjetos;
   private float limiteY;
 
   void Start(){
      player = FindObjectOfType(typeof(PlayerController)) as PlayerController;
      smoothTimeY = 0.2f;
      smoothTimeX = 0.2f;
      deltaY = 0.4f;
   }
 
   void FixedUpdate(){
      /*if(player == null){
         player = FindObjectOfType(typeof(PlayerController)) as PlayerController;
      } */
      //float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
      float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + deltaY, ref velocity.y, smoothTimeY);
      if(posY > limiteY) {
        transform.position = new Vector3(0, posY, transform.position.z);
        limiteY = posY;
      }
      //print(transform.position.y);
      float ny = (transform.position.y - 16f);
      destroyObjetos.transform.position = new Vector3(0, ny, transform.position.z);
   }
}
