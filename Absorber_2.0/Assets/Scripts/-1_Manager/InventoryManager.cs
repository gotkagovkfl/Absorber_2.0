using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//=================== 테스트 환경 용 =====================
// 인벤토리 매니저 : 구현한 무기 목록 UI를 보여주고 무기를 선택하여 장착할 수 있게 해줌.
//===============================================
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager im;

    // 인벤토리 UI
    public GameObject inventory;


    // 버튼 리스트
    public Button[] btns = new Button[9];


    
    
    //=====================함수=============================================
    //======================================
    // 리스트 세팅 : 사용 가능한 무기 목록을 설정한다.
    //======================================
    public void  InitInventory()
    {
        btns = inventory.transform.GetComponentsInChildren<Button>();
        for (int i=0;i<9;i++)
        {
            btns[i].onClick.AddListener( ()=> OpenInventory(false) );
            
            string currId = "00"+(i+1).ToString();      // 이부분 수정해야함. 
            
            btns[i].onClick.AddListener( ()=> ChangeWeapon( currId ));
        }


    }


    //======================================
    // 인벤토리 오픈 : 게임 일시정지 후, 인벤토리 창을 연다
    //======================================
    public void OpenInventory(bool flag)
    {       
        // Fade.fade.BtnClickSound();                         ************* 이거 
        
        GameManager.gm.PauseGame(flag);
        inventory.SetActive(flag);
    }

    //======================================
    // 무기교체 - 버튼 클릭시 버튼의 InventoryBtn 스크립트에서 호출됨
    //======================================
    public void ChangeWeapon(string weaponId)
    {   
        Player.player.GetComponent<PlayerWeapon>().ChangeWeapon(0,weaponId);

    }



    
    //=======================================================================
    void Awake()
    {
        im = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Canvas").transform.Find("WeaponSwap").gameObject;
        OpenInventory(false);
        InitInventory();
    }

    void Update()
    {
        // close
        if (inventory.activeSelf && Input.GetKeyDown(KeyCode.Escape) )
        {
            inventory.SetActive(false);
        }
    }

}
