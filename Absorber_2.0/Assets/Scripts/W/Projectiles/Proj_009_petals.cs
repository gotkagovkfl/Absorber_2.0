using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//==================================================
// Weapon_testBingbing의 투사체 : 플레이어 주위를 빙빙 돈다. 
//==================================================
public class Proj_009_petals : Projectile
{
    public GameObject parent;   // 분열의 중심
    

    float rx = 4f;      // 장반경 
    float ry = 2.5f;      // 단반경
    // public float deg = 0;       // 현재 각도 


    // =========== 오버라이드 =============
    // 필수 정보 초기화 
    // ===================================    
    public override void InitEssentialProjInfo()
    {
        id_proj = "009";
        splitWhenHit= false;
        shrinkOnDes =true;


    }


    
    // =========== 오버라이드 =============
    // 생성되면 바로 분열
    // ===================================
    public override void Action_custom()
    {                
        rx = 3.2f   * (scale);
        ry = 2f * (scale);
        speed = speed * 2;
        
        SetPosition();  // 초기 위치 설정

        Split();        // 분열하고 시작

        // StartCoroutine(ControlSpeed());
        StartCoroutine(EllipticalMotion());
    }

    // ================================
    // 설정된 각도로 타원 궤도 상 현재 위치 설정 
    // ===================================
    public void SetPosition()
    {
        float rad = Mathf.Deg2Rad * rotAngle;
        
        float x =  rx * Mathf.Cos(rad);
        float y =  ry * Mathf.Sin(rad);

        myTransform.position = new Vector3( target.position.x + x,  target.position.y + y , 0);
        myTransform.rotation = Quaternion.Euler(new Vector3(0, 0, rotAngle-90));
    }

    // ===================================
    // 회전운동 ( 타원 운동함 )
    // ===================================
    IEnumerator EllipticalMotion()
    {
        var wf = new WaitForFixedUpdate();
        while(true)
        {
            // 현재 회전의 중심이 사라지면 첫번재 꽃잎를 회전함. 
            if (!targetProj.isAlive)
            {
                target = mainTransform;
            }
            
            rotAngle += (Time.fixedDeltaTime* speed) % 360;
            SetPosition(); 

            yield return wf;
        }
    }

    // =========== 오버라이드 =============
    // 분열   
    // ===================================
    public override void Split()
    {
        // 분열 횟수가 남아있으면
        if(splitNum>0 && projNum >0)
        {
            StartCoroutine(ArrangeProj());
        }
    }

    // ===================================
    // 분열을 위해 배치해야함.
    // ===================================
    public IEnumerator ArrangeProj()
    {
        yield return new WaitForSeconds(lifeTime *0.05f);


        float rotationPerUnit = 360/projNum;       // 투사체 당 각도
        float currRotation = Random.Range(0,360);    // 이건 그냥 랜덤성을 위해 
        for (int i=0;i<projNum;i++)
        {
            string id = id_proj;
            Projectile proj = ProjPoolManager.ppm.GetFromPool(id);

            float splitWeight = 0.4f + splitNum * 0.15f;            
            proj.SetUp(damage * splitWeight , speed,  scale * splitWeight,  projNum-1, Player.Instance.penetration,  splitNum -1 , lifeTime*0.9f); 
            proj.SetSpecialStat(explosionLevel -1 , weight_critDamage , knockBackPower* splitWeight);
            
            proj.myTransform.rotation = Quaternion.Euler(new Vector3(0, 0, currRotation));
            
            proj.SetTarget(myTransform);       // 해당 투사체 주위를 회전
            proj.targetProj = this;
            proj.mainTransform = mainTransform;
            

            proj.rotAngle = currRotation;
            proj.Action();

            currRotation+=rotationPerUnit;
        }
    }



    public override void ProjDestroy_custom()
    {
        
    }




 
}
