using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPoolManager : PoolManager<DropItem>
{
    public static ItemPoolManager ipm;  // 아이템 풀 매니저 객체 
    
    //========================================================
    protected override void Awake()
    {
        base.Awake();
        ipm = this;
    }

    void Start()
    {
        // 스테이지 종료 이벤트 발생시 필드위 아이템 제거
        EventManager.em.onStageClear.AddListener( CleanEveryObjects_item );
    }

    

    //=======================================================
    protected override void SetCategory()
    {
        id_category = "05";
    }


    // public override void SetDir()
    // {
    //     dir = "Prefabs/W/DropItems";
    // }

    // public override string GetId(DropItem item)
    // {
    //     return item.id_dropItem;
    // }


    //=======================================================
    public override void GetFromPool_custom(DropItem item)
    {
        
    }

    public override void TakeToPool_custom(DropItem item)
    {
        item.captured = false; 
    }
    //==============================================================================================

    //=================================================================
    // 아이템 생성 : id, 아이템 효과 값, 위치 
    //=================================================================
    public DropItem SpawnItem(string id, float value, Vector3 pos)
    {
        DropItem item = GetFromPool(id);
        item.InitItem(value);
        item.DropAnimation(pos);

        return item;
    }

    //=================================================
    // 모든 아이템 청소 - 스테이지 종료시 발동  : 경험치와 회복아이템만. ( 나머지는 발동되어도 쓸모가 없기 때문 )
    //=================================================
    public void CleanEveryObjects_item()
    {
        DropItem[] items = GetComponentsInChildren<DropItem>();

        foreach(var item in items)
        {
            // mana와 회복템만 캡처 
            if (item.id_dropItem.Equals("000") || item.id_dropItem.Equals("001"))
            {
                item.captured = true;
            }
            // 나머지는 파괴 ㅋ
            else
            {
                item.ItemDestroy();
            }
        }

    }

}
