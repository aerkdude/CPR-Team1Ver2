using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject greenCube;
    public Image bar;
    public Text timeText;
    public Text hpPercText;
    public static bool gameStart;
    public static bool gameEnd;
    public static bool timeStart;
    private int hpPerc;
    private int hpPercCeil;
    public float curTime;
    public float maxHP = 480f;
    private float slideValue;
    private float hpLeft;
    public float startTime;
    public bool canPush;
    public int curHp;
    public static float pushHp;
    public static float falseHp;

    //Score System
    public GameObject resultPanel;
    public Text pumpScoreText;
    public Text resultText;
    public int pumpScore;

    // Start is called before the first frame update
    void Start()
    {
        gameEnd = false;
        resultPanel.SetActive(false);
        pumpScoreText.text = "";
        resultText.text = "";
        pumpScore = 0;
        timeStart = false;
        timeText.text = "";
        hpPercText.text = "";
        gameStart = false;
        slideValue = curTime / maxHP;
        hpLeft = 120f;
        curTime = 120f;
        canPush = true;
        startTime = 120.0f;
        pushHp = 5.0f;
        //falseHp = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            CprStart();
        }
        /*if (Input.GetKeyDown(KeyCode.S))
        {
            gameStart = true;
        }*/
        if(timeStart)
        {
            InvokeRepeating("GoTime", 0.0f, 1.0f);
            timeStart = false;
        }

        if (curTime <= 0)
        {
            curTime = 0;
            gameStart = false;
            CancelInvoke("GoTime");
            showResult();
        }
        if (Input.GetKey(KeyCode.Z))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void showResult()
    {
        resultPanel.SetActive(true);
        pumpScoreText.text = "ปั๊มจำนวน: "+pumpScore;
        if(pumpScore >= 100)
        {
            resultText.text = "คุณผ่านการทดสอบ";
        }
        else
        {
            resultText.text = "คุณไม่ผ่านการทดสอบ";
        }
        gameEnd = true;
    }
    private void CprStart()
    {
        slideValue = hpLeft / maxHP;
        timeText.text = "Time: " + curTime;
        bar.fillAmount = slideValue;
        hpPerc = (Mathf.CeilToInt(hpLeft / maxHP * 100.0f));
        hpPercText.text = hpPerc + " / 100%";

        if (hpLeft > 0)
        {
            hpLeft -= curHp * Time.deltaTime;
        }
        if (canPush)
        {
            /*if (Input.GetKeyDown(KeyCode.A))
            {
                Heal();
                StartCoroutine(DelayPush());
                Debug.Log("Hit");
            }*/
        }
        if (canPush)
        {
            greenCube.gameObject.SetActive(true);
        }
        if (!canPush)
        {
            greenCube.gameObject.SetActive(false);
            //hpLeft -= falseHp;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canPush)
        {
            if (other.gameObject.CompareTag("CheckHand"))
            {
                Heal();
                StartCoroutine(DelayPush());
                Debug.Log("Hit");
            }
        }
        
    }

    void GoTime()
    {
        //Debug.Log("count");
        curTime--;
        //Debug.Log("hpLeft:"+hpLeft+"slideValue"+slideValue);
    }
    void Heal()
    {
        pumpScore++;
        hpLeft += pushHp;
        Debug.Log("heal:" + pushHp);
        if(hpLeft>=maxHP)
        {
            hpLeft = maxHP;
        }
    }

    IEnumerator DelayPush()
    {
        canPush = false;
        yield return new WaitForSeconds(0.5f);
        canPush = true;
    }
    
    ///
   /* string nameA, nameB;
    void test()
    {
        for (int i = 0; i < nameA.Length; i++)
        {
            if (nameA[i] == nameB[i])
            {
            }
            }
    }*/
    ///
}