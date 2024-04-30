using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    AudioSource audioSource;
    
    Rigidbody2D rb;

    public IEnumerator RigidOn()
    {
        yield return new WaitForSeconds(4f);
        rb.simulated = true;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;

        StartCoroutine( RigidOn());
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot( audioSource.clip );
            
            // Debug.Log("다음 스테이지로 ~");

            Effect effect = EffectPoolManager.instance.GetFromPool("008");
            effect.InitEffect(Player.player.t_player.position );
            effect.ActionEffect();
            
            // 스테이지 전환 및 포탈 1초후에 파괴 (1초는 페이드 아웃 시간)
            StageManager.sm.GoToNextStage();
            Destroy(gameObject,1f); 
        }
    }
}
