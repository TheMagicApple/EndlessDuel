using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy2 : MonoBehaviour
{
    public bool movingforward=false;
    public bool movingright=false;
    public bool movingbackward=false;
    public bool movingleft=false;
    private float movespeed=3f;

    public static bool swinging=false;
    public bool knocking=false;
    private float knockForce=4f;
     public GameObject player;
    private Vector2 direction;

    public int knockBackStage=0;
    public bool iFrame=false;

    private float health, maxHealth;

    public RawImage hBar;
    //public HealthBar healthBar;
    
    /*
    public void TakeDamage(){
        // Use your own damage handling code, or this example one.
        health -= Mathf.Min(Random.value, health / 4f );            
        healthBar.UpdateHealthBar();
    }
    */


    // Start is called before the first frame update
    void Start()
    {
        maxHealth=100+(Manager.wave*15);
        health=maxHealth;
        GameObject[] respawns=GameObject.FindGameObjectsWithTag("Player");
        player=respawns[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
                int offset=1;
                Vector3 targetPos = player.transform.position;
                 Vector3 thisPos = transform.position;
                 targetPos.x = targetPos.x - thisPos.x;
                 targetPos.y = targetPos.y - thisPos.y;
                 float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
                 if(angle>-90 && angle<90){
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                 }else{
                    transform.rotation = Quaternion.Euler(new Vector3(180, 0, -angle));
                 }
                 
         if(angle>-90 && angle<90){
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
         }else{
            transform.rotation = Quaternion.Euler(new Vector3(180, 0, -angle));
         }
         transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.01f);
            transform.position=new Vector3(transform.position.x,transform.position.y,-4);

         
       
       
       
       
    }

private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("TRIGGERGRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");
        //Debug.Log(iFrame);
        if(other.tag=="Bullet" && !iFrame){
            health-=20;
            //Debug.Log("HEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
            if(health<=60){
                hBar.color=Color.yellow;
            }
            if(health<=30){
                hBar.color=Color.red;
            }
            if(health<=0){
                Manager.score++;
                Destroy(gameObject);
            }
            hBar.GetComponent<RectTransform>().sizeDelta=new Vector2(0.5f*(health/maxHealth),0.1f);
            Destroy(other.gameObject);
            StartCoroutine(iFrames());
            if(health<=0){
                Destroy(gameObject);
            }
            
        }
        
    }
    private void OnTriggerExit2D(Collider2D c) {
        if(c.tag == "Platform"){
            Destroy(gameObject);
        }
}
     private IEnumerator iFrames(){
        iFrame=true;
        yield return new WaitForSeconds(0.1f);
        iFrame=false;
    }
public void moreKnockBack(GameObject sender){
        health-=10;
        if(health<=60){
            hBar.color=Color.yellow;
        }
        if(health<=30){
            hBar.color=Color.red;
        }
        hBar.GetComponent<RectTransform>().sizeDelta=new Vector2(0.5f*(health/maxHealth),0.1f);
        if(health<=0){
            //Debug.Log("LOSE");
        }
        StartCoroutine(stopstop());
        /*
        
        knockBackStage++;
        Debug.Log("KNOCBKACK: "+knockBackStage);
        transform.GetChild(1).gameObject.GetComponent<Sword>().animation=-1;
        if(knockBackStage>=5){
            StopAllCoroutines();
            direction=(transform.position-sender.transform.position);
            knockBackStage=0;
            GetComponent<Rigidbody2D>().AddForce(direction*knockForce*2f,ForceMode2D.Impulse);
            //transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color=new Color(0,0,0,1);
            StartCoroutine(stopstop());
        }else{
            StopAllCoroutines();
            StartCoroutine(combo(sender));
        }
        */
    }
    private IEnumerator combo(GameObject sender){
        direction=(transform.position-sender.transform.position);
        GetComponent<Rigidbody2D>().AddForce(direction*knockForce*knockBackStage*0.3f,ForceMode2D.Impulse);
        //transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color=new Color(0,0,0,1);
        StartCoroutine(stopstopstop());
        yield return new WaitForSeconds(0.4f-knockBackStage*0.02f);
        direction=(transform.position-sender.transform.position);
        GetComponent<Rigidbody2D>().AddForce(direction*knockForce*knockBackStage*0.8f,ForceMode2D.Impulse);
        //transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color=new Color(0,0,0,1);
        StartCoroutine(stopstop());
        knockBackStage=0;

    }
private IEnumerator stopstopstop(){
        yield return new WaitForSeconds(0.08f);
//transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color=new Color(1,0,0,1);
        GetComponent<Rigidbody2D>().velocity=Vector3.zero;
    }
    private IEnumerator stopstop(){
        iFrame=true;
        yield return new WaitForSeconds(0.3f);
        iFrame=false;
        //transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color=new Color(1,0,0,1);
        //GetComponent<Rigidbody2D>().velocity=Vector3.zero;
    }


















    public void hello(GameObject sender){
        knocking=true;
        direction=(transform.position-sender.transform.position);
        //Debug.Log("HEY");
        //Debug.Log("PLAYER BIG KNOCKBACK");
        StartCoroutine(stop());
    }
    public void hello2(GameObject sender){
        knocking=true;
        direction=(transform.position-sender.transform.position);
        //Debug.Log("HEY");
        //Debug.Log("PLAYER SMALL KNOCKBACK");
        StartCoroutine(stop2());
    }
    
    
    
    
    
    
    
    
    private IEnumerator stop2(){
       
        
        GetComponent<Rigidbody2D>().AddForce(direction*knockForce*0.6f,ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
        GetComponent<Rigidbody2D>().velocity=Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        knocking=false;

        
    }
    private IEnumerator stop(){
        yield return new WaitForSeconds(0.1f);
        
        GetComponent<Rigidbody2D>().AddForce(direction*knockForce,ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
        GetComponent<Rigidbody2D>().velocity=Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        knocking=false;
       
    }
}



