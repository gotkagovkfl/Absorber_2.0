using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Kill : MonoBehaviour
{
    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void show()
    {
        rect.localScale = Vector3.one;
    }

    public void hide()
    {
        rect.localScale = Vector3.zero;
    }
}
