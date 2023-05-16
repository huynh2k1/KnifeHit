using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public static UIController instance;
    public GameObject restartButton; //restart button

    [Header("Knife Count Display")]
    public Transform panelKnives; // panel knife
    public GameObject iconKnife; // prefab icon knife
    public Color usedKnifeIconColor; // màu của icon knife

    private void Awake()
    {
        if (instance == null) { 
            instance = this;
        }
    }
    public void ShowRestartButton()
    {
        restartButton.SetActive(true);
    }
    public void InitKnifeIcon(int count)
    {
        for(int i = 0; i < count; i++)
        {
            Instantiate(iconKnife, panelKnives);
        }
    }
    private int knifeIconIndexToChange = 0;
    public void DecrementDisplayedKnifeCount()
    {
        panelKnives.GetChild(knifeIconIndexToChange++).GetComponent<Image>().color = usedKnifeIconColor;
    }
}
