using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    public int animation=-1;
    private Vector3 t;
    public GameObject target;
    public bool swing=false;
    public float offset=0;
    
    private Vector3 targetPos;
    private Vector3 thisPos;
    private float angle;

    private bool touchingEnemy=false;
    private GameObject enemyTouching;
    private bool reset=true;
    public GameObject bullet;
    private bool coolDown=false;
    private int fireRate;
    // Start is called before the first frame update
    void Start()
    {
        fireRate=4+Manager.wave*1;
        GameObject[] respawns=GameObject.FindGameObjectsWithTag("Player");
        target=respawns[0];
    }

    // Update is called once per frame
    void Update()
    {
        
        
        //rotation
        
        if(animation==-1){
            targetPos = target.transform.position;
            thisPos = transform.position;
            targetPos.x = targetPos.x - thisPos.x;
            targetPos.y = targetPos.y - thisPos.y;
            angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        }
        

        //animation
         if(swing && animation==-1){
            animation=0;
            reset=true;
            //Debug.Log("HELLO");
            swing=false;
            t=transform.up;
         }
         if(touchingEnemy){
            //Player touching enemy while swinging, do nothing  
            //Debug.Log("HEROWHTGIUHRWOHFROIHUSTPOIJEORHEOUPRH OFJEHIT L VE");
            //Debug.Log(enemyTouching.GetComponent<Player>().gameObject.transform.GetChild(1).GetComponent<Sword>().animation);
            /*
            if(enemyTouching.GetComponent<Player>().gameObject.transform.GetChild(1).GetComponent<Sword>().animation==-1 && enemyTouching.GetComponent<Player>().knocking==false && animation>=0 && transform.parent.gameObject.GetComponent<Enemy>().knocking==false){
                enemyTouching.GetComponent<Player>().hello(transform.parent.gameObject);
            }
            */
            
            if(animation>=0 && transform.parent.gameObject.GetComponent<Enemy>().knockBackStage==0 && reset && enemyTouching.GetComponent<Player>().iFrame==false){
                enemyTouching.GetComponent<Player>().moreKnockBack(transform.parent.gameObject);
                //Debug.Log("PLAYE KNOC BOACK !!!!!!!!!!!!!!!!!!!!!!");
                reset=false;
            }
            
            /*
            if(enemyTouching.GetComponent<Player>().gameObject.transform.GetChild(1).GetComponent<Sword>().animation!=-1 && enemyTouching.GetComponent<Player>().knocking==false && animation>=0){
                enemyTouching.GetComponent<Player>().hello2(transform.parent.gameObject);
            }
            */
         }
         
         

         
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
        if(c.tag=="Player"){
            touchingEnemy=true;
            enemyTouching=c.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D c){
        if(c.tag=="Player"){
            touchingEnemy=false;
        }
    }
}
