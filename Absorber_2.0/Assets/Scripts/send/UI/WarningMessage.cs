using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningMessage : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator warningMessage;
    void Start()
    {
        warningMessage = GetComponent<Animator>();
        warningMessage.SetBool("boss", false);
    }

    public void MessageOn()
    {
        warningMessage.SetBool("boss", true);
        //yield return null;
    }
    public void MessageOff()
    {
        warningMessage.SetBool("boss", false);
        //yield return null;
    }
}
