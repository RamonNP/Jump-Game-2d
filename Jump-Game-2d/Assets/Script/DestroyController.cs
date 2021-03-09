using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision2d)
    {
         switch (collision2d.gameObject.tag)
        {
            case "Player":
               collision2d.gameObject.SendMessage("gameOver", SendMessageOptions.DontRequireReceiver);
               break;
            case "VOAR":
               Destroy(collision2d.gameObject);
               break;
            case "PULO":
               Destroy(collision2d.gameObject);
               break;
            case "ESCUDO":
               Destroy(collision2d.gameObject);
               break;
            case "COIN":
               Destroy(collision2d.gameObject);
               break;
            case "PADS":
               Destroy(collision2d.gameObject);
               break;
            case "TRAPS":
               Destroy(collision2d.gameObject);
               break;
        }
       
    }
    void OnCollisionEnter2D(Collision2D collision2d){   
        //print("DESTROINDO - "+collision2d.gameObject.name);
        //Destroy(collision2d.gameObject);
    }
}
