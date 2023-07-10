using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAppearanceEffectEffect : MonoBehaviour
{
    public WarningMessage warningMessage;
    
    public CameraShaking shake;

    // Start is called before the first frame update

    public void BAEffect()
    {
        warningMessage = GameObject.FindObjectOfType<WarningMessage>();
        shake = GameObject.FindObjectOfType<CameraShaking>();
        StartCoroutine(Effect());
    }

    IEnumerator Effect()
    {
        warningMessage.MessageOn();
        yield return new WaitForSeconds(3);
        warningMessage.MessageOff();
        shake.Shake();
    }
}
