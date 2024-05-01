using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//======================= 부모 클래스 ==========================
// 몬스터를 잡거나, 상자를 부숴 필드에 드랍되어 플레이어가 획득 가능한 아이템에 대한 스크립트
// 플레이어와의 거리를 계산하여, 범위안에 들어오면 플레이어쪽으로 당겨지며, 플레이어와 충돌 시 획득된다.
//===================================================
public abstract class DropItem : MonoBehaviour , IPoolObject
{
    Rigidbody2D rb;


    // 필수정보
    public string id_dropItem;

    public Transform myTransform;
    public Vector3 originalScale;
    
    // 속도 관련
    public float speed; // 아이템이 플레이어 쪽으로 이동하는 속도 
    public float itemRange=3f;
    public float weight_range = 0.5f;
    public float effectValue;       // 아이템 효과의 값 ( 경험치 or 회복량 or 능력치 증가량 등 )
    
    public bool captured = false;   // 

    // 경험치가 플레이어의 획득 반경 안에 있는지
    public bool inRange
    {
        get
        {
            float dist = Vector3.Distance(Player.player.t_player.position, myTransform.position);
            float pickupRange = itemRange *(100 + weight_range * Player.player.range_plus) *0.01f;

            
            if (dist <= pickupRange  && !captured) // 25는 획득범위의 제곱 - 나중에 수정해야함. 
            {
                // Debug.Log(dist);
                // Debug.Log(pickupRange);
                // Debug.Log("---------------");
                
                return true;
            }
            return false;

        }
    }

    //=========================================================================================================
    //===========================
    // IPoolObject : resourceManager에 로드시에 id 초기화 
    //===========================
    public void InitEssentialInfo()
    {
        InitEssentialInfo_item();
    }
    //
    
    protected abstract void InitEssentialInfo_item();
    //================
    // GetID
    //==============
    public string GetId()
    {
        return id_dropItem;
    }
    //===========================
    // IPoolObject : 처음 생성될때 
    //===========================
    public void OnCreatedInPool()
    {

    }

    //===========================
    // IPoolObject : 다시 사용될때, 
    //===========================
    public void OnGettingFromPool()
    {

    }

    //================================================================

    //==================================
    // 아이템 정보 초기화  : 기본값
    //==================================
    public void InitItem(float value)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = true;

        myTransform.localScale = originalScale;

        captured = false;

        speed = 3;
        itemRange=3f;
        weight_range = 0.5f;

        effectValue = value;

        InitItem_custom();
    }

    public virtual void InitItem_custom()
    {

    }
    //----------------------------------------------------------------------------------
    //==============================================
    // 지정된 위치에 생성후 연출
    //==============================================
    public void DropAnimation(Vector3 pos)
    {
        // myTransform.position = pos;
        rb.position = pos;
        captured = false;

        StartCoroutine(Grow());
        StartCoroutine(MoveALittle());
    }

    //==============================================
    // 생성 직후 점차적으로 커지도록 하여 자연스러운 연출
    //==============================================
    public IEnumerator Grow()
    {
        for (int i=0;i<6;i++)
        {
            myTransform.localScale = originalScale * 0.2f * i;
            yield return new WaitForSeconds(0.05f);
        }
    }

    //==============================================
    // 약간의 랜덤 방향으로 움직이도록 하여 자연스러운연출
    //==============================================
    public IEnumerator MoveALittle()
    {
        Vector3 randDir = new Vector3 ( Random.Range(-1.6f,1.6f),Random.Range(-1f,1f),0 ).normalized;

        rb.velocity = randDir * 4;
        yield return new WaitForSeconds(0.2f);
        rb.velocity = rb.velocity * 0.5f;
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector3.zero;
    }


    //----------------------------------------------------------------------------------
    //==============================================
    // 아이템 파괴 : 
    //==============================================1
    public void ItemDestroy()
    {
        rb.simulated = false;
        captured = false;
        StartCoroutine(Shrink());
    }

    //====================
    // 아이템 쪼그라들고 아이템 풀 반납 : 자연스러운 연출을 위함 
    //=====================
    IEnumerator Shrink()
    {
        for (int i=4;i>0;i--)
        {
            myTransform.localScale = originalScale * 0.2f * i;
            yield return new WaitForSeconds(0.05f);
        }
        myTransform.position = new Vector3(0, 40);
        ItemPoolManager.instance.TakeToPool(this);
    }

    //=========================================================================================================

    void Awake()
    {
        myTransform = transform;
        originalScale = myTransform.localScale;
    }

    //==============================================
    // 업데이트 -  
    //      1) 플레이어가 해당 아이템에 근접했는지 판별하여 '캡처'
    //      2) '캡처'된 아이템이 플레이어 쪽으로 이동
    //==============================================
    void FixedUpdate()
    {
        // 아이템이 범위 안인지
        if(inRange)
        {
            captured = true;
        }


        // 캡처시 플레이어쪽으로 이동
        if(captured)
        {
            
            // 방향구하기
            Vector3 dir = (Player.player.t_player.position - transform.position).normalized;
            
            rb.velocity = dir * speed;

            speed += 10 * Time.fixedDeltaTime;  // 아이템 속도가 점점 빨라짐
        }
    }
    
    //==============================================
    // 충돌 판정
    //==============================================
    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어와 충돌시 (플레이어가 아이템 획득시 )
        if (other.CompareTag("Player"))
        {            
            //pickup effect
            string id = (id_dropItem.Equals("000"))?"013":"004";

            Effect effect = EffectPoolManager.instance.GetFromPool(id);
            effect.InitEffect(Player.player.center.position);
            effect.ActionEffect();

            
            
            PickupEffect(); // 아이템 습득 효과 발동

            ItemDestroy(); // 풀에서 재활용 될때 생겨나자마자 바로 빨려오지 않게           
        }
    }

    
    //==============================================
    // 아이템 습득 효과 - 플레이어와 충돌시 발동
    //==============================================
    public abstract void PickupEffect();
}
