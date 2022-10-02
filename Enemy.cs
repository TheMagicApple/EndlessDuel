using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public bool swinging=false;
    public bool knocking=false;
    private float knockForce=4f;
    public GameObject player;
    private Vector2 direction;
    public GameObject[] objects;
    public float spawnTime = 6f;
    private Vector3 spawnPosition;
    private float health=100;
    private float maxHealth=100;
    public bool iFrame=false;
    public int knockBackStage=0;
    public RawImage hBar;
    
    //public Image healthBarImage;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] respawns=GameObject.FindGameObjectsWithTag("Player");
        player=respawns[0];
        //InvokeRepeating("Spawn", spawnTime, spawnTime);
    }
    /*
    void Spawn() {
        spawnPosition.x = Random.Range(-8.0f, 8.0f);
        spawnPosition.y = 0.5f;
        spawnPosition.z = Random.Range(-8.0f, 8.0f);
        Instantiate(objects[Random.Range(0, objects.Length-1)], spawnPosition, Quaternion.identity);
    }
    */
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(knockBackStage==0){
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.03f);
            transform.position=new Vector3(transform.position.x,transform.position.y,-4);
            if(Mathf.Abs(transform.position.x-player.transform.position.x)<2.0f && Mathf.Abs(transform.position.y-player.transform.position.y)<2.0f && !swinging) {
                swinging=true;
                StartCoroutine(swingswing());
            }
            if(Mathf.Abs(transform.position.x-player.transform.position.x)>2.0f || Mathf.Abs(transform.position.y-player.transform.position.y)>2.0f){
                swinging=false;
            }
        }else{
            swinging=false;
        }
        
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
    private IEnumerator iFrames(){
        iFrame=true;
        yield return new WaitForSeconds(0.1f);
        iFrame=false;
    }
    private IEnumerator swingswing(){
        //Debug.Log("HELLLOOOOO");
        yield return new WaitForSeconds(Random.Range(0.3f,0.8f));
        this.gameObject.transform.GetChild(1).GetComponent<EnemySword>().swing=true;
        //Debug.Log("SWINGWININSGIWNINRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");
        if(swinging){
            StartCoroutine(swingswing());
        }
    }
    public void moreKnockBack(GameObject sender){
        knockBackStage++;
        iFrame=true;
        //Debug.Log("IFRAMEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE"+iFrame);
        health-=20;
        //Debug.Log("hit");
        if(health<=60){
            hBar.color=Color.yellow;
        }
        if(health<=30){
            hBar.color=Color.red;
        }
        if(health<=0){
            Destroy(gameObject);
        }
        hBar.GetComponent<RectTransform>().sizeDelta=new Vector2(0.5f*(health/maxHealth),0.1f);
        //Debug.Log("KNOCBKACK: "+knockBackStage);
        
        transform.GetChild(1).gameObject.GetComponent<EnemySword>().animation=-1;
        
        if(knockBackStage==5){
            StopAllCoroutines();
            direction=(transform.position-sender.transform.position);
            knockBackStage=0;
            GetComponent<Rigidbody2D>().AddForce(direction*knockForce*2f,ForceMode2D.Impulse);
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color=new Color(0,0,0,1);
            StartCoroutine(stopstop());
        }else{
            StopAllCoroutines();
            StartCoroutine(combo(sender));
        }
    }
    private IEnumerator combo(GameObject sender){
        direction=(transform.position-sender.transform.position);
        GetComponent<Rigidbody2D>().AddForce(direction*knockForce*knockBackStage*0.3f,ForceMode2D.Impulse);
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color=new Color(0,0,0,1);
        
        StartCoroutine(stopstopstop());
        yield return new WaitForSeconds(0.4f-knockBackStage*0.02f);
        direction=(transform.position-sender.transform.position);
        GetComponent<Rigidbody2D>().AddForce(direction*knockForce*knockBackStage*0.8f,ForceMode2D.Impulse);
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color=new Color(0,0,0,1);
        StartCoroutine(stopstop());
        knockBackStage=0;

    }
private IEnumerator stopstopstop(){
        iFrame=true;
        yield return new WaitForSeconds(0.08f);
        StartCoroutine(waitwait());
        
transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color=new Color(1,0,0,1);
        GetComponent<Rigidbody2D>().velocity=Vector3.zero;
    }
    private IEnumerator waitwait(){
        yield return new WaitForSeconds(0.3f);
        iFrame=false;
    }
    private IEnumerator stopstop(){
        iFrame=true;
        yield return new WaitForSeconds(0.3f);
        iFrame=false;
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color=new Color(1,0,0,1);
        GetComponent<Rigidbody2D>().velocity=Vector3.zero;
    }
    public void hello(GameObject sender){
        knocking=true;
        direction=(transform.position-sender.transform.position);
        //Debug.Log("HEY");
        //Debug.Log("ENEMY BIG KNOCKBACK");
        StartCoroutine(stop());
    }
    public void hello2(GameObject sender){
        knocking=true;
        direction=(transform.position-sender.transform.position);
        //Debug.Log("HEY");
        //Debug.Log("ENEMY SMALL KNOCKBACK");
        StartCoroutine(stop2());
    }
    private IEnumerator stop2(){
        
        GetComponent<Rigidbody2D>().AddForce(direction*knockForce*0.6f,ForceMode2D.Impulse);
        transform.position=new Vector3(transform.position.x,transform.position.y,-4);
        yield return new WaitForSeconds(0.3f);
        transform.position=new Vector3(transform.position.x,transform.position.y,-4);
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
