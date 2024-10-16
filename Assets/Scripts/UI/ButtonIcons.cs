using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonIcons : MonoBehaviour
{
    [SerializeField] private Button[] lvlButton;
    [SerializeField] private Sprite unLockedIcon;
    [SerializeField] private Sprite lockedIcon;
    [SerializeField] private int firstLevelBuildIndex = 2;

    private void Awake()
    {
        int unlockedLvl = PlayerPrefs.GetInt(EndGameManager.instance.levelUnlock,firstLevelBuildIndex);
        for (int i = 0; i < lvlButton.Length; i++)
        {
            if (i + firstLevelBuildIndex <= unlockedLvl)
            {
                lvlButton[i].interactable = true;
                lvlButton[i].GetComponent<Image>().sprite = unLockedIcon;
                TextMeshProUGUI textButton = lvlButton[i].GetComponentInChildren<TextMeshProUGUI>();
                textButton.text = (i + 1).ToString();
                textButton.enabled = true;
            }
            else
            {
                lvlButton[i].interactable = false;
                lvlButton[i].GetComponent<Image>().sprite = lockedIcon;
                lvlButton[i].GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            }

        }
    }
}
