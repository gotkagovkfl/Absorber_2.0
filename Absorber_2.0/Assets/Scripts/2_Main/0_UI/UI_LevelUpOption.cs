using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_LevelUpOption : MonoBehaviour
{
    ILevelUpOption levelUpOption;

    public void Init(ILevelUpOption levelUpOption)
    {   
        this.levelUpOption = levelUpOption;

        transform.Find("Pictogram").GetComponent<Image>().sprite = ResourceManager.GetPictogram(levelUpOption.id);
        transform.Find("Description").GetComponent<TextMeshProUGUI>().text = levelUpOption.description;
        
        GetComponent<Button>().onClick.AddListener(levelUpOption.OnSelect);
    }
}
