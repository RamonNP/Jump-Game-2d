using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   private Vector2 velocity;
 
   public float smoothTimeY;
   public float smoothTimeX;
   public float deltaY;
 
   public GameObject player;
   private float limiteY;
 
   void Start(){
      player = GameObject.Find("Player");
      smoothTimeY = 0.2f;
      smoothTimeX = 0.2f;
      deltaY = 0.4f;
   }
 
   void FixedUpdate(){
      //float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
      float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + deltaY, ref velocity.y, smoothTimeY);
      if(posY > limiteY) {
        transform.position = new Vector3(0, posY, transform.position.z);
        limiteY = posY;
      }
   }
}
