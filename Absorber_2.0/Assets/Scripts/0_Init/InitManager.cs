using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InitManager : MonoBehaviour
{
    [SerializeField] InitUI initUI;

    
    Queue<InitWork> initWorks =new();
    
    InitWork currWork;

    int count_initWorks;
    int count_initWorksRemain => initWorks.Count;


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        Debug.Log("게임 초기화");


        initUI?.Init();

    
        StartCoroutine( InitProgress() );
    }

    /// <summary>
    /// 초기화 작업 목록을 세팅한다.
    /// </summary>
    void SetWorks()
    {
        for(int i=0;i<7;i++)
        {
            initWorks.Enqueue(new InitWork_TestInit());
        }

        count_initWorks = count_initWorksRemain;
        Debug.Log("초기화 작업 개수 : "+ count_initWorks);

        initUI?.Init_loadingBar(count_initWorks);
    }

    /// <summary>
    /// 초기화 작업을 진행한다. 
    /// </summary>
    /// <returns></returns>
    IEnumerator InitProgress()
    {
        //초기화 할일 목록 세팅
        SetWorks();

        // 지정된 할일이 전부 끝날 때 까지 일련의 과정 진행

        Debug.Log(" =============초기화 작업 진행=========== ");
        while ( count_initWorksRemain >0 )
        {
            currWork = initWorks.Dequeue();
            
            initUI?.OnStartInitWork(currWork);
            
            Debug.Log("초기화 작업 시작 : " + currWork.workName);

            yield return StartCoroutine(currWork.WorkProgress());


            initUI?.OnFinishInitWork();
            Debug.Log("초기화 작업 종료 : " + currWork.workName);
        }

        //다음 화면으로 이동 
        Debug.Log("==========모든 초기화 작업 종료됨.============");

        SceneHanlder.LoadScene_lobby();
    }
}
