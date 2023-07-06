using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public static Fade fade;


    public AudioSource audioSource;

    public AudioClip sound_click;   
    
    public Animator animator;

    void Awake()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        
        if (fade == null)
        {
            fade = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);

        }
        SceneManager.sceneLoaded += OnSceneLoaded;

        //
        audioSource = GetComponent<AudioSource>();
        if (audioSource !=null)
        {
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
        sound_click = Resources.Load<AudioClip>("Sound/2.Click");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FadeIn();
    }

    public void FadeOut()
    {
        // Debug.Log("fade out");
        // animator.SetBool("fade", true);
        if (animator!=null)
        {
            animator.SetTrigger("fadeOut");
        }
        
    }
    public void FadeIn()
    {
        // Debug.Log("fade in");
        // animator.SetBool("fade", false);
        if (animator!=null)
        {
            animator.SetTrigger("fadeIn");
        }
        
    }

    public void BtnClickSound()
    {
        audioSource.PlayOneShot(sound_click);
    }



}
