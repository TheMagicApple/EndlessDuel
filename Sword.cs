using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int animation=-1;
    private Vector3 t;
    private bool touchingEnemy=false;
    private GameObject enemyTouching;
    private bool reset=true;
    public GameObject bullet;
    private bool coolDown=false;
    private int fireRate=6;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fireRate=6+Manager.wave*2;
        //rotation
         /*
         Vector3 mousePos = Input.mousePosition;
         mousePos.z = 0f;
 
         Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
         mousePos.x = mousePos.x - objectPos.x;
         mousePos.y = mousePos.y - objectPos.y;
 
         float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
         transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
         */
        if(Input.GetMouseButton(0) && !coolDown){
            Instantiate(bullet,transform.position,transform.rotation);
            coolDown=true;
            StartCoroutine(cool());
        }
         
         /*
         
         if(Input.GetMouseButtonDown(0) && animation==-1 && transform.parent.gameObject.GetComponent<Player>().knockBackStage==0){
            animation=0;
            reset=true;
            t=transform.up;
            Player.swinging=true;
         }else{
            Player.swinging=false;
         }
         if(touchingEnemy){
            //Player touching enemy while swinging, do nothing  Debug.Log("HEROWHTGIUHRWOHFROIHUSTPOIJEORHEOUPRH OFJEHIT L VE");
            //Debug.Log(enemyTouching.GetComponent<Enemy>().gameObject.transform.GetChild(1).GetComponent<EnemySword>().animation);
            /*
            if(enemyTouching.GetComponent<Enemy>().gameObject.transform.GetChild(1).GetComponent<EnemySword>().animation==-1 && enemyTouching.GetComponent<Enemy>().knocking==false && animation>=0 && transform.parent.gameObject.GetComponent<Player>().knocking==false){
                enemyTouching.GetComponent<Enemy>().hello(transform.parent.gameObject);
            }
            if(enemyTouching.GetComponent<Enemy>().gameObject.transform.GetChild(1).GetComponent<EnemySword>().animation!=-1 && enemyTouching.GetComponent<Enemy>().knocking==false && animation>=0 && transform.parent.gameObject.GetComponent<Player>().knocking==false){
                enemyTouching.GetComponent<Enemy>().hello2(transform.parent.gameObject);
                transform.parent.gameObject.GetComponent<Player>().hello2(enemyTouching);
                Debug.Log(transform.parent.gameObject);
            }
            
            if(animation>=0 && transform.parent.gameObject.GetComponent<Player>().knockBackStage==0 && reset && enemyTouching.GetComponent<Enemy>().iFrame==false){
                Debug.Log("HITTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT");
                enemyTouching.GetComponent<Enemy>().moreKnockBack(transform.parent.gameObject);
                reset=false;
            }

         }
         */

         
    }
    private IEnumerator cool(){
        yield return new WaitForSeconds(1.0f/fireRate);
        coolDown=false;
    }
    void FixedUpdate(){
        if(animation>=0){
            animation++;
            if(animation<6){
                transform.position+=t*0.1f;
            }else if(animation<11){
                transform.position-=t*0.1f;
            }else{
                animation=-1;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D c){
        /*
        if(c.tag=="Enemy" && c.GetComponent<Enemy>().swinging==false && c.GetComponent<Enemy>().knocking==false && animation>=0){
            //Debug.Log(180-transform.eulerAngles.z);
            //Debug.Log(animation);
            
            c.GetComponent<Enemy>().hello(transform.parent.gameObject);
        }
        */
        if(c.tag=="Enemy"){
            touchingEnemy=true;
            enemyTouching=c.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D c){
        if(c.tag=="Enemy"){
            touchingEnemy=false;
        }
    }


}
