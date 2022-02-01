using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMainHandler : MonoBehaviour
{
    [SerializeField] private RectTransform formsPanel;
    [SerializeField] private RectTransform mainColor;
    [SerializeField] private Sprite color;
    [SerializeField] private Sprite selectedColor;
    [SerializeField] private Image targetButtonImage;
    [SerializeField] private GameObject targetButton;
    [SerializeField] private Button selectColor;
    [SerializeField] private GameObject activForm;

    public void SelectColor()
    {
        selectColor = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        targetButton.transform.GetChild(0).GetComponent<Image>().color = selectColor.colors.normalColor;
        targetButton.transform.GetChild(0).GetComponent<Image>().gameObject.SetActive(true);

        for (int i = 0; i < formsPanel.childCount; i++)
        {
            if (formsPanel.GetChild(i).gameObject.activeInHierarchy)
            {
                activForm = formsPanel.GetChild(i).gameObject;
            }
        }

        switch (targetButton.name)
        {
            case "ButtonLeft":
                activForm.transform.Find("Layer_1").GetComponent<Image>().color = selectColor.colors.normalColor;
                break;
            case "ButtonMidlle":
                activForm.transform.Find("Layer_2").GetComponent<Image>().color = selectColor.colors.normalColor;
                break;
            case "ButtonRight":
                activForm.transform.Find("Layer_3").GetComponent<Image>().color = selectColor.colors.normalColor;
                break;
        }
            
    }

    public void FormsColorPart()
    {
        for (int i = 0; i < mainColor.childCount; i++)
        {
            targetButton = mainColor.GetChild(i).gameObject;
            targetButton.GetComponent<Image>().sprite = color;          
        }

        targetButton = EventSystem.current.currentSelectedGameObject;
        targetButton.GetComponent<Image>().sprite = selectedColor;      
    }

    public void NextForm()
    {       
        for (int i = 0; i < formsPanel.childCount; i++)
        {
            if (formsPanel.GetChild(i).gameObject.activeInHierarchy)
            {                   
                formsPanel.GetChild(i).gameObject.SetActive(false);
                i++;

                if (i >= formsPanel.childCount) i--;

                formsPanel.GetChild(i).gameObject.SetActive(true);
                return;
            }
        }
    }

    public void PreviousForm()
    {
        for (int i = 0; i < formsPanel.childCount; i++)
        {
            if (formsPanel.GetChild(i).gameObject.activeInHierarchy)
            {
                formsPanel.GetChild(i).gameObject.SetActive(false);
                i--;

                if (i < 0) i++;

                formsPanel.GetChild(i).gameObject.SetActive(true);
                return;
            }
        }
    }
}
