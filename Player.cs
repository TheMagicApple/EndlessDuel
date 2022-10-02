using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{
    public bool movingforward=false;
    public bool movingright=false;
    public bool movingbackward=false;
    public bool movingleft=false;
    private float movespeed=3f;

    public static bool swinging=false;
    public bool knocking=false;
    private float knockForce=4f;
    
    private Vector2 direction;

    public int knockBackStage=0;
    public bool iFrame=false;

    private float health, maxHealth;

    public RawImage hBar;
    public GameObject deathPanel;
    public TMP_Text scoreText;
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
        maxHealth=100;
        health=maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 mousePos = Input.mousePosition;
         mousePos.z = 0f;
         Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
         mousePos.x = mousePos.x - objectPos.x;
         mousePos.y = mousePos.y - objectPos.y;
 
         float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
         if(angle>-90 && angle<90){
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
         }else{
            transform.rotation = Quaternion.Euler(new Vector3(180, 0, -angle));
         }
         
            
        
        
       
        if(knockBackStage==0){
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

            transform.position+=new Vector3(0,1,0)*movespeed*Time.deltaTime;
        }
        if(movingleft==true){
            transform.position-=new Vector3(1,0,0)*movespeed*Time.deltaTime;
        }
        if(movingbackward==true){

            transform.position-=new Vector3(0,1,0)*movespeed*Time.deltaTime;
        }
        if(movingright==true){
            transform.position+=new Vector3(1,0,0)*movespeed*Time.deltaTime;
        }
        }
       
    }
private void OnTriggerEnter2D(Collider2D c){
    if(c.tag=="Enemy" && !iFrame){
         health-=10;
        CManager.shakeState=true;
        if(health<=60){
            hBar.color=Color.yellow;
        }
        if(health<=30){
            hBar.color=Color.red;
        }
        StartCoroutine(regeneration());
        hBar.GetComponent<RectTransform>().sizeDelta=new Vector2(0.5f*(health/maxHealth),0.1f);
        if(health<=0){
            Manager.timerIsRunning=false;
            deathPanel.SetActive(true);
            scoreText.text="Score: "+(Manager.score*10);
            int highScore=PlayerPrefs.GetInt("HighScore",0);
            if(Manager.score>highScore){
                PlayerPrefs.SetInt("HighScore",Manager.score);
            }
            Destroy(gameObject);
        }
        
        StartCoroutine(stopstop());
    }

}

private void OnTriggerExit2D(Collider2D c) {
        if(c.tag == "Platform"){
            //Debug.Log("you are dead");
            deathPanel.SetActive(true);
            scoreText.text="Score: "+(Manager.score*10);
            int highScore=PlayerPrefs.GetInt("HighScore",0);
            if(Manager.score>highScore){
                PlayerPrefs.SetInt("HighScore",Manager.score);
            }
            Manager.timerIsRunning=false;
            Destroy(gameObject);
        }
}

public IEnumerator regeneration(){
        health+=1;
        //Debug.Log("REGEN: " + health);
        hBar.GetComponent<RectTransform>().sizeDelta=new Vector2(0.5f*(health/maxHealth),0.1f);
        if(health>60){
            hBar.color=Color.green;
        }
        if(health<=60){
            hBar.color=Color.yellow;
        }
        if(health<=30){
            hBar.color=Color.red;
        }
        yield return new WaitForSeconds(1.4f);
    if(health<maxHealth){
        StartCoroutine(regeneration());
    }
}

public void moreKnockBack(GameObject sender){
        health-=10;
        CManager.shakeState=true;
        if(health<=60){
            hBar.color=Color.yellow;
        }
        if(health<=30){
            hBar.color=Color.red;
        }
        StartCoroutine(regeneration());
        hBar.GetComponent<RectTransform>().sizeDelta=new Vector2(0.5f*(health/maxHealth),0.1f);
        if(health<=0){
            Manager.timerIsRunning=false;
            deathPanel.SetActive(true);
            scoreText.text="Score: "+Manager.score;
            Destroy(gameObject);
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
        Debug.Log("PLAYER BIG KNOCKBACK");
        StartCoroutine(stop());
    }
    public void hello2(GameObject sender){
        knocking=true;
        direction=(transform.position-sender.transform.position);
        //Debug.Log("HEY");
        Debug.Log("PLAYER SMALL KNOCKBACK");
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



