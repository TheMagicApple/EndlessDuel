using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private float speed=0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position-=transform.right*speed*Time.deltaTime;
        if(transform.position.x<-9){
            transform.position=new Vector3(9,transform.position.y,transform.position.z);
        }
    }
}
