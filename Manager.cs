using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Manager : MonoBehaviour
{
    private Vector3 spawnPosition;
    public GameObject[] objects;
    public GameObject star;
    private int enemies=1;
    public static int wave=1;
    public float timeRemaining = 10.0f;
    public static bool timerIsRunning = true;
    public TMP_Text timer;
    public static int score=0;
    public int highScore = 0;
    public TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        if(scoreText!=null){
            scoreText.text="HIGHSCORE: "+(PlayerPrefs.GetInt("HighScore",0)*10
            );
        }
        for(int i=0;i<70;i++){
            float x=Random.Range(-7.82f,7.82f);
            float y=Random.Range(-4.45f,4.45f);
            Instantiate(star,new Vector3(x,y,6),Quaternion.identity);
        }
        StartCoroutine(spawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            //Debug.Log("TIMER RUNNING");
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                //Debug.Log("TIME: "+timeRemaining);
                timer.text=""+Mathf.Round(timeRemaining*100.0f)/100.0f;
            }
            else
            {
                timeRemaining = 10.0f;
            }
        }
    }

    void checkHighScore(){
        if(score>highScore){
            highScore=score;
        }
    }

    
    private IEnumerator spawn(){
        for(int i=0;i<enemies;i++){
            spawnPosition.x = Random.Range(-2.92f, 2.7f);
            spawnPosition.y = Random.Range(-1.75f, 1.44f);
            Instantiate(objects[Random.Range(0, objects.Length-1)], spawnPosition, Quaternion.identity);
        }
        //increase enemies every other time
        if(wave%2==0 && wave > 4){
            enemies++;
        } else {
            enemies++;
        }
        yield return new WaitForSeconds(10f);
        wave++;
        StartCoroutine(spawn());
    }

    public void tryAgain(){
        timerIsRunning=true;
        score=0;
        wave=1;
        CManager.shakeState=false;
        SceneManager.LoadScene("Game");
    }
    public void tutorial(){
        SceneManager.LoadScene("Tutorial");
    }
    public void home(){
        SceneManager.LoadScene("MainMenu");
    }

    public void countDown() {
        StartCoroutine(countDownCoroutine());
    }

    private IEnumerator countDownCoroutine() {
        yield return new WaitForSeconds(1f);
    }
    
}
