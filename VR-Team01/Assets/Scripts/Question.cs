using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    public GameObject questionCanvas;
    
    public GameObject phone;
    public GameObject shoulder;
    public GameObject hintPanel;
    public GameObject guidePanel;
    public Text questionText;
    public Text hintText;
    public Text guideText;

    public static bool hitShoulder;
    public bool activeCancas;
    public float Timer;
    public bool canShowHint;
    public string[] question;
    private string guess;
    public string hint;
    public int questionNo;

    //select paper
    public int curSelect;
    public bool slot1;
    public bool slot2;
    public bool slot3;
    public bool slot4;

    public InputField InputAnswer;
   // public int[] answer;

    //private Text answerText;

    // Start is called before the first frame update
    void Start()
    {
        curSelect = 0;
        guidePanel.SetActive(false);
        hintPanel.SetActive(false);
        canShowHint = false;
        guideText.text = "";
        hitShoulder = false;
        hint = "";
        guess = "";
        hintText.text = "";

        StartCoroutine(preQuestion1());

        //paper system
        slot1 = false;
        slot2 = false;
        slot3 = false;
        slot4 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gameStart)
        {
            CprStart();
        }
        if (!GameController.gameStart)
        {
            if (Input.GetKeyDown(KeyCode.Return) && canShowHint) //Send Answer
            {
                ProcessText();
                //ShowHint();
                Timer = 0;
                questionCanvas.SetActive(false);
            }
        }

        //Shoulder Check
        if (hitShoulder)
        {
            StartCoroutine(preQuestion2());
            hitShoulder = false;
        }
        InputAnswer.ActivateInputField();

    }

    private void CprStart()
    {
        if (Input.GetKeyDown(KeyCode.Return) && canShowHint) //Send Answer
        {
            ProcessText();
            InputAnswer.clearInputField();
            Timer = 0;
            questionCanvas.SetActive(false);
        }
        /*
        if (Timer <= 10)
        {
            // string input = InputAnswer.ToString();
            Timer += Time.deltaTime;
            questionNo = Random.Range(0, 4);
            GameController.pushHp = 20.0f;
            canShowHint = false;
            //Debug.Log("questtion:" + questionNo);
            questionCanvas.SetActive(false);

        }
        else if (Timer > 10 && Timer <= 20)
        {
            GameController.pushHp = 2.0f;
            questionCanvas.SetActive(true);
            Timer += Time.deltaTime;
            canShowHint = true;
            InputAnswer.ActivateInputField();
            if (questionNo == 0)
            {
                hint = "4 นาที";
                questionText.text = "" + question[0];
            }
            if (questionNo == 1)
            {
                hint = "100 ครั้ง:นาที";
                questionText.text = "" + question[1];
            }
            if (questionNo == 2)
            {
                hint = "ไม่เกิน120ครั้ง:นาที";
                questionText.text = "" + question[2];
            }
            if (questionNo == 3)
            {
                hint = "5cm";
                questionText.text = "" + question[3];
            }
        }

        else
        {
            Timer = 0;
            questionCanvas.SetActive(false);
        }
        */
    }
    void ProcessText()
    {
        /*guess = InputAnswer.text;
        //Debug.Log(guess);
        switch (questionNo)
        {
            case 0:
                if (guess == "4")
                {
                    Debug.Log("Correct");
                    ResetAnswer();
                }
                else
                {
                    Debug.Log("Wrong");
                    ResetAnswer();
                }
                break;
            case 1:
                if (guess == "100")
                {
                    Debug.Log("Correct");
                    ResetAnswer();
                }
                else
                {
                    Debug.Log("Wrong");
                    ResetAnswer();
                }
                break;
            case 2:
                if (guess == "120")
                {
                    Debug.Log("Correct");
                    ResetAnswer();
                }
                else
                {
                    Debug.Log("Wrong");
                    ResetAnswer();
                }
                break;
            case 3:
                if (guess == "5")
                {
                    Debug.Log("Correct");
                    ResetAnswer();
                }
                else
                {
                    Debug.Log("Wrong");
                    ResetAnswer();
                }
                break;

                //Pre question before CPR
            case 101:
                if (guess == "4")
                {
                    Debug.Log("Correct");
                    ResetAnswer();
                    StartCoroutine(DelayShoulderText());
                }
                else
                {
                    Debug.Log("Wrong");
                    ResetAnswer();
                    StartCoroutine(DelayShoulderText());
                }
                break;
            case 102:
                if (guess == "1669")
                {
                    Debug.Log("Correct");
                    ResetAnswer();
                    StartCoroutine(PrepareCall());
                }
                else
                {
                    Debug.Log("Wrong");
                    ResetAnswer();
                    StartCoroutine(PrepareCall());
                }
                break;
        }*/
    }

    public void ShowHint()
    {
        hintText.text = "เฉลย: "+hint;
        hintPanel.SetActive(true);
        StartCoroutine(ClearHint());
    }
    private void ResetAnswer()
    {
        ShowHint();
        guess = "";
        Timer = 0;
        questionCanvas.SetActive(false);
        //canShowHint = false;
        InputAnswer.clearInputField();
    }

    IEnumerator ClearHint()
    {
        yield return new WaitForSeconds(3.0f);
        hintPanel.SetActive(false);
        hintText.text = "";
    }
    IEnumerator preQuestion1()
    {
        yield return new WaitForSeconds(3.0f);
        questionCanvas.gameObject.SetActive(true);
        canShowHint = true;
        InputAnswer.ActivateInputField();
        questionNo = 101;
        questionText.text = "เวลาการเชคสติไม่ควรเกินกี่นาที";
        hint = "4";
    }
    IEnumerator preQuestion2()
    {
        guidePanel.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        questionCanvas.gameObject.SetActive(true);
        canShowHint = true;
        InputAnswer.ActivateInputField();
        questionNo = 102;
        questionText.text = "เบอร์โทรสพฉ.คือ?";
        hint = "1669"; 
    }
    IEnumerator DelayShoulderText()
    {
        yield return new WaitForSeconds(3.0f);
        shoulder.SetActive(true);
        guidePanel.SetActive(true);
        guideText.text = "P1 ตบบ่าสะกิดผู้ป่วย";
    }
    IEnumerator PrepareCall()
    {
        yield return new WaitForSeconds(3.0f);
        ReadyToCall();
    }
    void ReadyToCall()
    {
        Debug.Log("Phpne spawn");
        guidePanel.SetActive(true);
        guideText.text = "P1 ใช้โทรศัพท์ที่พื้นโทรเรียกสพฉ.";
        phone.SetActive(true);
    }
    
    public void RemovePhoneText()
    {
        StartCoroutine(ReadyCpr());
        guideText.text = "หน่วยแพทย์กำลังมา เตรียมนับเวลาถอยหลัง";
    }
    IEnumerator ReadyCpr()
    {
        yield return new WaitForSeconds(3.0f);
        guideText.text = "";
        StartCoroutine(CprBegin());
    }
    IEnumerator CprBegin()
    {
        yield return new WaitForSeconds(1.0f);
        GameController.gameStart = true;
        GameController.timeStart = true;
        guideText.text = "เริ่มนับบเวลาถอยหลัง 2 นาที";
    }
    IEnumerator CprContinue()
    {
        yield return new WaitForSeconds(3.0f);
        guideText.text = "P1 ทำ CPR / P2 คอยตอบคำถาม";
    }
}

public static class Extension
{
    public static void clearInputField(this InputField inputField)
    {
        inputField.Select();
        inputField.text = "";
        //Debug.Log("clear");
    }
}

