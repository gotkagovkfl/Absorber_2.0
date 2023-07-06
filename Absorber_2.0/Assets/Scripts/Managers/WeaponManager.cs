using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//=================== 테스트 환경 용 =====================
// Weapon Manager : 게임에서 사용할 무기의 정보를 초기화함
//===============================================
public class WeaponManager : MonoBehaviour
{
    public static WeaponManager wm;

    // 식별번호, 게임오브젝트>의 자료구조 
    public Dictionary<string, GameObject> dic_weapons = new Dictionary<string, GameObject>();     

    
    //=====================함수=============================================
    //======================================
    //  세팅 : 사용 가능한 무기 목록을 설정한다.
    //======================================
    public void  InitWeaponDictionary()
    {
        // 리소스 파일에서 무기 오브젝트 정보를 가져온다.
        GameObject[] list_weapons = Resources.LoadAll<GameObject>("Prefabs/W/Weapons");

        // 일단 가져온 무기 오브젝트 정보들을 무기목록(사전)에 등록
        for(int i = 0;i<list_weapons.Length;i++)
        {
            GameObject obj_weapon = list_weapons[i];
            Weapon weapon = obj_weapon.GetComponent<Weapon>();  // 무기오브젝트의 무기 스크립트

            weapon.InitEssentialWeaponInfo();         // 무기 정보 초기화 - 무기 번호, 이름 얻으려고 필수 정보 초기화 했음. 

            dic_weapons.Add ( weapon.id_weapon, weapon.gameObject );   // 무기 목록에 추가             
        }
    }

    //=======================================================================
    void Awake()
    {
        wm = this;
        InitWeaponDictionary();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }
}
