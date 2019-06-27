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
    private bool activateQuestion;
    public bool canShowHint;
    public string[] question;
    private string guess;
    public string hint;

    //select paper
    public GameObject paper1;
    public GameObject paper2;
    public GameObject paper3;
    public GameObject paper4;
    public GameObject paper1Select;
    public GameObject paper2Select;
    public GameObject paper3Select;
    public GameObject paper4Select;
    public Text question1Text;
    public Text question2Text;
    public Text question3Text;
    public Text question4Text;
    public InputField paper1InputField;
    public InputField paper2InputField;
    public InputField paper3InputField;
    public InputField paper4InputField;

    public int paperOnScreen;
    public int curSelect;
    public bool slot1Full;
    public bool slot2Full;
    public bool slot3Full;
    public bool slot4Full;

    public int questionNo;

    public int question1No;
    public int question2No;
    public int question3No;
    public int question4No;

    public InputField InputAnswer;
   // public int[] answer;

    //private Text answerText;

    // Start is called before the first frame update
    void Start()
    {
        guidePanel.SetActive(false);
        hintPanel.SetActive(false);
        canShowHint = false;
        guideText.text = "";
        hitShoulder = false;
        hint = "";
        guess = "";
        hintText.text = "";
        Timer = 0;

        StartCoroutine(preQuestion1());

        //paper system
        paperOnScreen = 0;
        curSelect = 0;

        slot1Full = false;
        slot2Full = false;
        slot3Full = false;
        slot4Full = false;
        paper1.SetActive(false);
        paper2.SetActive(false);
        paper3.SetActive(false);
        paper4.SetActive(false);

        question1Text.text = "";
        question2Text.text = "";
        question3Text.text = "";
        question4Text.text = "";
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
                questionCanvas.SetActive(false);
                
            }
        }
        if (GameController.gameEnd)
        {
            guidePanel.SetActive(false);
            guideText.text = "";
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
            questionCanvas.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.R)) //Send Answer
        {
            removePaper();
            slot4Full = false;
        }

        //paper slot
        if (slot1Full)
        {
            paper1.SetActive(true);
        }
        else if(!slot1Full)
        {
            paper1.SetActive(false);
        }
        if (slot2Full)
        {
            paper2.SetActive(true);
        }
        else if(!slot2Full)
        {
            paper2.SetActive(false);
        }
        if (slot3Full)
        {
            paper3.SetActive(true);
        }
        else if (!slot3Full)
        {
            paper3.SetActive(false);
        }
        if (slot4Full)
        {
            paper4.SetActive(true);
        }
        else if (!slot4Full)
        {
            paper4.SetActive(false);
        }
    }
    void ProcessText()
    {
        guess = InputAnswer.text;
        //Debug.Log(guess);
        switch (questionNo)
        {
            /*case 0:
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
                break;*/

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
        }
    }
    public void SpawnQuestion()
    {
        if (slot1Full) //if slot 1 full
        {
            if (slot2Full) // if slot 2 full
            {
                if (slot3Full) // if slot 3 full
                {
                    if (!slot4Full) // if slot 4 empty place into slot 4
                    {
                        slot4Full = true;
                        question4Text.text = question[Random.Range(0, question.Length)];
                    }
                }
                if (!slot3Full) //if slot 3 empty place into slot 3
                {
                    slot3Full = true;
                    question3Text.text = question[Random.Range(0, question.Length)];
                }
            }
            if (!slot2Full)//if slot 2 is empty place into slot 2
            {
                slot2Full = true;
                question2Text.text = question[Random.Range(0, question.Length)];
            }
        }
        if (!slot1Full) //if slot 1 is empty place in slot 1
        {
            slot1Full = true;
            question1Text.text = question[Random.Range(0, question.Length)];
        }
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
        InvokeRepeating("spawnPaperEvery10Sec", 0.0f, 10.0f);
        guideText.text = "เริ่มนับบเวลาถอยหลัง 2 นาที";
        StartCoroutine(CprContinue());
    }
    IEnumerator CprContinue()
    {
        yield return new WaitForSeconds(3.0f);
        guideText.text = "P1 ทำ CPR / P2 คอยตอบคำถาม";
    }
    void spawnPaperEvery10Sec()
    {
        if (paperOnScreen < 4)
        { 
            Debug.Log("SpawnQuestion");
            SpawnQuestion();
            paperOnScreen++;
        }
    }
    void removePaper()
    {
        Debug.Log("remove 1 paper");
        paperOnScreen--;
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

