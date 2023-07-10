using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestLoading : MonoBehaviour
{
    public Slider slider;
    public Text text;
    public string sceneName;

    public float time;
    
    // Start is called before the first frame update
    void Start()
    {
        sceneName = "Scene_Lobby";
        StartCoroutine( LoadAsyncScene_c() );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadAsyncScene_c()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            text.text = time.ToString();
            time = Time.time;
            slider.value = time/10f;

            Debug.Log(op.isDone);
            Debug.Log(op.progress);
            Debug.Log("------------");

            if (time >10 )
            {
                op.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
