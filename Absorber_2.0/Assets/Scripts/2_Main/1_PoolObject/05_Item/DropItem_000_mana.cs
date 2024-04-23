using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//===========================================
// 마나 = 경험치 
//==============================================
public class DropItem_000_mana : DropItem
{   
    SpriteRenderer spriter;
    
    //================== 오버라이드 =========================
    // 필수정보 초기화
    //==============================================
    protected override void InitEssentialInfo_item()
    {
        id_dropItem = "000";
    }


    //================== 오버라이드 =========================
    // 마나 처음에 초기화시에 색깔도 마나량에 따라 다르게 적용
    //==============================================  
    public override void InitItem_custom()
    {
        
        // 지금은 랜덤으로 되어 있음 
        spriter = transform.Find("Color").GetComponent<SpriteRenderer>();

        Color newColor = new Color32 (  150, 0,  255 , 255   );

        if (effectValue < 5)
        {
            newColor = new Color32 (    110,    230,  255 , 255  );
        }
        else if (effectValue < 10)
        {
            newColor = new Color32 (    50,     230,  255 , 255  );
        }
        else if (effectValue <15)
        {
            newColor = new Color32 (    0,      230,  255 , 255  );
        }
        else if (effectValue <20)
        {
            newColor = new Color32 (    0,      170,  255 , 255  );
        }
        else if (effectValue <30)
        {
            newColor = new Color32 (    0,      120,  255 , 255  );
        }
        else if (effectValue <60)
        {
            newColor = new Color32 (    0,      70,  255 , 255  );
        }
        else if (effectValue < 250)
        {
            newColor = new Color32 (    0,      0,    255 , 255  );
        }

        // Color newColor = Color.red;
        



        spriter.color = newColor;



    }


    //================== 오버라이드 =========================
    // 경험치  획득 효과 - 플레이어와 충돌시 발동
    //==============================================
    public override void PickupEffect()
    {
        Player.Instance.ChangeExp(effectValue);
    }
}
