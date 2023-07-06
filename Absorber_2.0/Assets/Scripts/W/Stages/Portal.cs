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

            Effect effect = EffectPoolManager.epm.GetFromPool("008");
            effect.InitEffect(Player.Instance.myTransform.position );
            effect.ActionEffect();
            
            StageManager.sm.GoToNextStage();
        }
    }
}
