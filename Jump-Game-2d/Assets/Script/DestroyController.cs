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
        //print("DESTROINDO - "+collision2d.gameObject.name);
        Destroy(collision2d.gameObject);
        switch (collision2d.gameObject.tag)
        {
            case "BackGround":
            break;
        }
    }
    void OnCollisionEnter2D(Collision2D collision2d){   
        //print("DESTROINDO - "+collision2d.gameObject.name);
        Destroy(collision2d.gameObject);
    }
}
