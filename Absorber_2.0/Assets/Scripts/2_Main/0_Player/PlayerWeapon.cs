using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//====================================
// 플레이어가 장착하고 있는 무기에 대한 정보
//====================================
public class PlayerWeapon : MonoBehaviour
{
    // 무기 장착 위치
    public Transform[] hands = new Transform[1];   
    // public Transform h;
    
    // 현재 들고 있는 무기 
    // public GameObject currWeapon;
    public Weapon[] currWeapon = new Weapon[1];


    //=======================================================================
    //==========================
    // 무기 교체 : 현재 무기를 전달받은 무기 정보로 교체한다.
    // handNum - 무기 위치 정보, weapon - 무기정보
    //==========================
    public void ChangeWeapon(int handNum, string weaponId)
    {
        
        if (currWeapon[handNum])
        {
            // 기존 무기 제거
            Destroy(currWeapon[handNum].gameObject); 
        }

        
        // 무기 장착 
        GameObject newWeapon = Instantiate( PrefabManager.GetWeapon(weaponId), hands[0]);
        newWeapon.transform.position = hands[handNum].position;

        // 무기 리스트 갱신
        currWeapon[handNum] = newWeapon.GetComponent<Weapon>();

        currWeapon[handNum].InitWeapon();
    }

    //==========================
    // 스탯 업데이트 시 보유 무기 능력치 초기화 작업 (변한 능력치 반영)
    //==========================
    public void InitWeapons()
    {
        currWeapon[0].InitWeapon();
    }

    //=========================================================================    
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        // hands[1] = transform.Find("hand1");
        // hands[1].gameObject.SetActive(false);
        
        hands[0] = transform.Find("hand0");
        foreach (var weapon in hands[0].GetComponentsInChildren<Weapon>())
        {
            Destroy(weapon.gameObject);
        }


        
        yield return new WaitUntil( ()=>Player.initialized );
        
        Debug.Log("초기 무기 세팅합니다.");
        ChangeWeapon(0, "001");

        Debug.Log("초기 무기 세팅 완료 .");
    }
}
