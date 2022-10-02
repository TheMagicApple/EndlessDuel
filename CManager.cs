using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CManager : MonoBehaviour
{
    private float shakeTimeRemaining, shakePower;
    public static bool shakeState=false;
    private Vector3 oldPos;
    // Start is called before the first frame update
    void Start()
    {
 oldPos= transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeState)
        {
            shakeState=false;
            ShakeCamera(0.1f, 0.1f);
            transform.position=oldPos;
        }

    }

    private void LateUpdate() {
        if(shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;
            float xAmount = Random.Range(-0.5f, 0.5f) * shakePower;
            float yAmount = Random.Range(-0.5f, 0.5f) * shakePower;
            transform.position += new Vector3(xAmount, yAmount, 0f);
        }
    }

    public void ShakeCamera(float length, float power)
    {
        shakeTimeRemaining = length;
        shakePower = power;
    }
}
