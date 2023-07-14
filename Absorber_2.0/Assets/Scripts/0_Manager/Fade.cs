// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

// public class Fade : MonoBehaviour
// {
//     public static Fade fade;


//     AudioSource audioSource;

//     [SerializeField]
//     AudioClip sound_click;   
    
//     Animator animator;


//     public delegate void Delegate_fadeOut();


//     //=========================================================================================================
//     void Awake()
//     {
//         animator = transform.GetChild(0).GetComponent<Animator>();
        
//         if (fade == null)
//         {
//             fade = this;
//             DontDestroyOnLoad(gameObject);
//         }
//         else
//         {
//             Destroy(gameObject);

//         }

//         //
//         audioSource = GetComponent<AudioSource>();
//         if (audioSource !=null)
//         {
//             audioSource.playOnAwake = false;
//             audioSource.loop = false;
//         }
//         sound_click = Resources.Load<AudioClip>("Sound/2.Click");
//     }

//     //================================

//     //===============
//     // 페이드 아웃
//     //===============
//     public void FadeOut( Delegate_fadeOut d)
//     {
//         if (animator!=null)
//         {
//             animator.SetTrigger("fadeOut");
//         }
//         // ******************* onDirecting = true 해야함
//         StartCoroutine(Fade_c( d ));
//     }

//     //===============
//     // 페이드인
//     //===============
//     public void FadeIn( Delegate_fadeOut d )
//     {
//         if (animator!=null)
//         {
//             animator.SetTrigger("fadeIn");
//         }
//         // ******************* onDirecting = false 해야함

//         StartCoroutine(Fade_c( d) );
//     }

//     //
//     IEnumerator Fade_c( Delegate_fadeOut d )
//     {
//         yield return new WaitForSeconds(1f);
//         d();
//     }





//     public void BtnClickSound()
//     {
//         audioSource.PlayOneShot(sound_click);
//     }

    
//     void Start()
//     {
//         EventManager.em.onAnyButtonClick.AddListener( BtnClickSound );
//     }



// }
