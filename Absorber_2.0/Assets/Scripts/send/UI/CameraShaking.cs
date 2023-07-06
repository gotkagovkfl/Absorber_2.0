using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaking : MonoBehaviour
{
    public Camera mainCamera;
    Vector3 cameraPos;

    [SerializeField] float shakeRange = 0.1f;
    [SerializeField] float duration = 5f;

    void Start()
    {
        mainCamera = Camera.main;
    }

    public void Shake()
    {
        cameraPos = mainCamera.transform.position;
        InvokeRepeating("StartShake", 0f, 0.005f);
        Invoke("StopShake", duration);
    }

    void StartShake()
    {
        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        mainCamera.transform.position = cameraPos;
    }

    void StopShake()
    {
        CancelInvoke("StartShake");
        mainCamera.transform.position = cameraPos;
    }

}
