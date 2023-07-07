using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

using TMPro;

public class LevelUpManager : MonoBehaviour
{
    public GameObject Popup;
    public int[] numList;
    public GameObject PlayerUi;
    List<Dictionary<string, object>> data1;
    List<Dictionary<string, object>> data2;
    List<Dictionary<string, object>> data3;
    List<Dictionary<string, object>> data4;
    // Start is called before the first frame update
    void Start()
    {
        PlayerUi = GameObject.Find("PlayerUI");
        
        
        Popup.SetActive(false);
        data1 = GameObject.Find("CSVManager").GetComponent<test>().data1;
        data2 = GameObject.Find("CSVManager").GetComponent<test>().data2;
        data3 = GameObject.Find("CSVManager").GetComponent<test>().data3;
        data4 = GameObject.Find("CSVManager").GetComponent<test>().data4;
    }
    public void LevelUp()
    {
        Player.Instance.OnSelecting = true;

        int Luk         = Player.Instance.Luk;
        int[,] Luk_Table = Player.Instance.Luk_Table;
        int grade = 0;
        int tmp = Random.Range(1, 101); // 1단계 2단계 3단계 확률이 90 7 3 이라면 1~90은 1단계 91~97은 2단계 98 ~ 100은 3단계로 되도록 난수 추출
        if (tmp <= Luk_Table[Luk, 0])
        {
            grade = 0;
            numList = getList(0, data1.Count);
        }
        else if (tmp <= Luk_Table[Luk, 0] + Luk_Table[Luk, 1])
        {
            grade = 1;
            numList = getList(0, data2.Count);
        }
        else if (tmp <= Luk_Table[Luk, 0] + Luk_Table[Luk, 1] + Luk_Table[Luk, 2])
        {
            grade = 2;
            numList = getList(0, data3.Count);
        }
        else
        {
            grade = 3;
            numList = new int[] {0, 0, 0};
        }
        // 버튼에 연동시키기 // 
        PlayerUi.SetActive(false);
        Popup.SetActive(true);
        
        Button Btn = Popup.GetComponent<UiScript>().First;
        switch (grade)
        {
            case 0:
                for (int i = 0; i < 3; i++)
                { 
                    int status = (int)data1[numList[i]]["Status"];
                    string table = data1[numList[i]]["Table"].ToString();
                    string image = data1[numList[i]]["Image"].ToString();
                    if (numList[i] < 3)
                        Btn.GetComponentInChildren<TextMeshProUGUI>().text = table + " 을(를) " + status + " 증가시킨다";
                    else if (numList[i] < 9)
                        Btn.GetComponentInChildren<TextMeshProUGUI>().text = table + " 을(를) " + status + "% 증가시킨다";
                    else
                        Btn.GetComponentInChildren<TextMeshProUGUI>().text = table;

                    if(i == 0)
                    {
                        Popup.GetComponent<UiScript>().First_status = table;
                        Popup.GetComponent<UiScript>().First_Amount = status;
                        Popup.GetComponent<UiScript>().First_Image_Num = image;
                        Popup.GetComponent<UiScript>().First_Image.sprite = Resources.Load<Sprite>("Pictogram/" + image);
                        Btn = Popup.GetComponent<UiScript>().Second;
                    }
                    else if (i == 1)
                    {
                        Popup.GetComponent<UiScript>().Second_status = table;
                        Popup.GetComponent<UiScript>().Second_Amount = status;
                        Popup.GetComponent<UiScript>().Second_Image_Num = image;
                        Popup.GetComponent<UiScript>().Second_Image.sprite = Resources.Load<Sprite>("Pictogram/" + image);
                        Btn = Popup.GetComponent<UiScript>().Third;
                    }
                    else if (i == 2)
                    {
                        Popup.GetComponent<UiScript>().Third_status = table;
                        Popup.GetComponent<UiScript>().Third_Amount = status;
                        Popup.GetComponent<UiScript>().Third_Image_Num = image;
                        Popup.GetComponent<UiScript>().Third_Image.sprite = Resources.Load<Sprite>("Pictogram/" + image);
                    }
                }
                break;
            case 1:
                for (int i = 0; i < 3; i++)
                {
                    int status = (int)data2[numList[i]]["Status"];
                    string table = data2[numList[i]]["Table"].ToString();
                    string image = data2[numList[i]]["Image"].ToString();
                    if (numList[i] < 6)
                        Btn.GetComponentInChildren<TextMeshProUGUI>().text = table + " 을(를) " + status + " 증가시킨다";
                    else if (numList[i] < 12)
                        Btn.GetComponentInChildren<TextMeshProUGUI>().text = table + " 을(를) " + status + "% 증가시킨다";
                    else
                    {
                        Btn.GetComponentInChildren<TextMeshProUGUI>().text = table;
                    }
                    if (i == 0)     
                    {
                        Popup.GetComponent<UiScript>().First_status = table;
                        Popup.GetComponent<UiScript>().First_Amount = status;
                        Popup.GetComponent<UiScript>().First_Image_Num = image;
                        Popup.GetComponent<UiScript>().First_Image.sprite = Resources.Load<Sprite>("Pictogram/" + image);
                        Btn = Popup.GetComponent<UiScript>().Second;
                    }
                    else if (i == 1)
                    {
                        Popup.GetComponent<UiScript>().Second_status = table;
                        Popup.GetComponent<UiScript>().Second_Amount = status;
                        Popup.GetComponent<UiScript>().Second_Image_Num = image;
                        Popup.GetComponent<UiScript>().Second_Image.sprite = Resources.Load<Sprite>("Pictogram/" + image);
                        Btn = Popup.GetComponent<UiScript>().Third;
                    }
                    else if (i == 2)
                    {
                        Popup.GetComponent<UiScript>().Third_status = table;
                        Popup.GetComponent<UiScript>().Third_Amount = status;
                        Popup.GetComponent<UiScript>().Third_Image_Num = image;
                        Popup.GetComponent<UiScript>().Third_Image.sprite = Resources.Load<Sprite>("Pictogram/" + image);
                    }
                }
                break;
            case 2:
                for (int i = 0; i < 3; i++)
                {
                    int status = (int)data3[numList[i]]["Status"];
                    string table = data3[numList[i]]["Table"].ToString();
                    string image = data3[numList[i]]["Image"].ToString();
                    if (numList[i] < 4)
                        Btn.GetComponentInChildren<TextMeshProUGUI>().text = table + " 을(를) " + status + " 증가시킨다";
                    else if (numList[i] < 10)
                        Btn.GetComponentInChildren<TextMeshProUGUI>().text = table + " 을(를) " + status + "% 증가시킨다";
                    else
                    {
                        Btn.GetComponentInChildren<TextMeshProUGUI>().text = table;
                        if (numList[i] != 10)
                            status = 0;
                    }
                    if (i == 0)
                    {
                        Popup.GetComponent<UiScript>().First_status = table;
                        Popup.GetComponent<UiScript>().First_Amount = status;
                        Popup.GetComponent<UiScript>().First_Image_Num = image;
                        Popup.GetComponent<UiScript>().First_Image.sprite = Resources.Load<Sprite>("Pictogram/" + image);
                        Btn = Popup.GetComponent<UiScript>().Second;
                    }
                    else if (i == 1)
                    {
                        Popup.GetComponent<UiScript>().Second_status = table;
                        Popup.GetComponent<UiScript>().Second_Amount = status;
                        Popup.GetComponent<UiScript>().Second_Image_Num = image;
                        Popup.GetComponent<UiScript>().Second_Image.sprite = Resources.Load<Sprite>("Pictogram/" + image);
                        Btn = Popup.GetComponent<UiScript>().Third;
                    }
                    else if (i == 2)
                    {
                        Popup.GetComponent<UiScript>().Third_status = table;
                        Popup.GetComponent<UiScript>().Third_Amount = status;
                        Popup.GetComponent<UiScript>().Third_Image_Num = image;
                        Popup.GetComponent<UiScript>().Third_Image.sprite = Resources.Load<Sprite>("Pictogram/" + image);
                    }
                }
                break;
            default:
                for (int i = 0; i < 3; i++)
                {
                    string table = data4[numList[i]]["Table"].ToString();
                    int status = 0;
                    string image = data4[numList[i]]["Image"].ToString();
                    Btn.GetComponentInChildren<TextMeshProUGUI>().text = table;
                    if (i == 0)
                    {
                        Popup.GetComponent<UiScript>().First_status = table;
                        Popup.GetComponent<UiScript>().First_Amount = status;
                        Popup.GetComponent<UiScript>().First_Image_Num = image;
                        Popup.GetComponent<UiScript>().First_Image.sprite = Resources.Load<Sprite>("Pictogram/" + image);
                        Btn = Popup.GetComponent<UiScript>().Second;
                    }
                    else if (i == 1)
                    {
                        Popup.GetComponent<UiScript>().Second_status = table;
                        Popup.GetComponent<UiScript>().Second_Amount = status;
                        Popup.GetComponent<UiScript>().Second_Image_Num = image;
                        Popup.GetComponent<UiScript>().Second_Image.sprite = Resources.Load<Sprite>("Pictogram/" + image);
                        Btn = Popup.GetComponent<UiScript>().Third;
                    }
                    else if (i == 2)
                    {
                        Popup.GetComponent<UiScript>().Third_status = table;
                        Popup.GetComponent<UiScript>().Third_Amount = status;
                        Popup.GetComponent<UiScript>().Third_Image_Num = image;
                        Popup.GetComponent<UiScript>().Third_Image.sprite = Resources.Load<Sprite>("Pictogram/" + image);
                    }
                }
                break;
        }
        GameManager.gm.PauseGame(true);
    }

    public void AutoLevelUp()
    {
        int Luk = GameObject.Find("Player").GetComponent<Player>().Luk;
        int[,] Luk_Table = GameObject.Find("Player").GetComponent<Player>().Luk_Table;
        int grade = 0;
        int tmp = Random.Range(1, 101); // 1단계 2단계 3단계 확률이 90 7 3 이라면 1~90은 1단계 91~97은 2단계 98 ~ 100은 3단계로 되도록 난수 추출
        if (tmp <= Luk_Table[Luk, 0])
        {
            grade = 0;
            numList = getList(0, data1.Count);
        }
        else if (tmp <= Luk_Table[Luk, 0] + Luk_Table[Luk, 1])
        {
            grade = 1;
            numList = getList(0, data2.Count);
        }
        else if (tmp <= Luk_Table[Luk, 0] + Luk_Table[Luk, 1] + Luk_Table[Luk, 2])
        {
            grade = 2;
            numList = getList(0, data3.Count);
        }
        else
        {
            grade = 3;
            numList = new int[] { 0, 0, 0 };
        }
        int choose = Random.Range(0, 3);
        string image;
        switch (grade)
        {
            case 0:
                image = data1[numList[choose]]["Image"].ToString();
                Popup.GetComponent<UiScript>().select(image, data1[numList[choose]]["Table"].ToString(), (int)data1[numList[choose]]["Status"]);
                break;
            case 1:
                image = data2[numList[choose]]["Image"].ToString();
                Popup.GetComponent<UiScript>().select(image, data2[numList[choose]]["Table"].ToString(), (int)data2[numList[choose]]["Status"]);
                break;
            case 2:
                image = data3[numList[choose]]["Image"].ToString();
                Popup.GetComponent<UiScript>().select(image,data3[numList[choose]]["Table"].ToString(), (int)data3[numList[choose]]["Status"]);
                break;
            default:
                image = data4[numList[choose]]["Image"].ToString();
                Popup.GetComponent<UiScript>().select(image,data4[numList[choose]]["Table"].ToString(), (int)data4[numList[choose]]["Status"]);
                break;
        }
    }

    // 중복없는 난수 추출 //
    int[] getList(int min, int max)
    {
        int[] chooseset = new int[3];
        int[] ranArr = Enumerable.Range(min, max).ToArray();
        for (int i=0; i<3; i++)
        {
            int rand = Random.Range(i, max);
            chooseset[i] = ranArr[rand];
            int tmp = ranArr[rand];
            ranArr[rand] = ranArr[i];
            ranArr[i] = tmp;
        }
        return chooseset;
    }
}
