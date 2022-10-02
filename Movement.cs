using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool movingforward=false;
    public bool movingright=false;
    public bool movingbackward=false;
    public bool movingleft=false;
    private float movespeed=3f;
    public GameObject c;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("w")){
            movingforward=true;
            
        }
        if(Input.GetKeyUp("w")){
            movingforward=false;
        }
        if(Input.GetKeyDown("a")){
            movingleft=true;
            
        }
        if(Input.GetKeyUp("a")){
            movingleft=false;
        }
        if(Input.GetKeyDown("s")){
            movingbackward=true;
        }
        if(Input.GetKeyUp("s")){
            movingbackward=false;
        }
        if(Input.GetKeyDown("d")){
            movingright=true;
            
        }
        if(Input.GetKeyUp("d")){
            movingright=false;
        }
        if(movingforward==true){

            transform.position+=transform.up*movespeed*Time.deltaTime;
        }
        if(movingleft==true){
            transform.position-=transform.right*movespeed*Time.deltaTime;
        }
        if(movingbackward==true){

            transform.position-=transform.up*movespeed*Time.deltaTime;
        }
        if(movingright==true){
            transform.position+=transform.right*movespeed*Time.deltaTime;
        }
    }
}
