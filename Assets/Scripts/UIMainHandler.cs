using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMainHandler : MonoBehaviour
{
    [SerializeField] private RectTransform formsPanel;
    [SerializeField] private RectTransform formsAndLogoPanel;
    [SerializeField] private RectTransform mainColor;
    [SerializeField] private RectTransform colorPanel;
    [SerializeField] private Sprite color;
    [SerializeField] private Sprite selectedColor;
    [SerializeField] private GameObject targetButton;
    [SerializeField] private Button selectColorButton;
    [SerializeField] private GameObject activForm;
    [SerializeField] private GameObject selectedForm;
    [SerializeField] private Color[] curentColors = new Color[3];
    [SerializeField] private Color[] randomColor = new Color[3];
    [SerializeField] private List<Color> ColorPalette = new List<Color>();
    private int nextCount = 0;

    private void Start()
    {
        activForm = formsPanel.GetChild(0).gameObject;
        InitColors();
        InitColorsPalette();
        InitTargetButton();
    }

    public void RandomColors()
    {                   
        for (int i = 0; i < randomColor.Length; i++)
        {
            randomColor[i] = ColorPalette[Random.Range(0, ColorPalette.Count)];

            for (int j = 0; j < randomColor.Length; j++)
            {
                if (i == j)
                {
                    continue;
                }

                if (randomColor[i].Equals(randomColor[j]))
                {
                    Debug.Log("Одинаковый цвет");
                    RandomColors();
                }
            }
        }
        
        activForm.transform.Find("Layer_1").GetComponent<Image>().color = randomColor[0];
        activForm.transform.Find("Layer_2").GetComponent<Image>().color = randomColor[1];
        activForm.transform.Find("Layer_3").GetComponent<Image>().color = randomColor[2];
        InitColors();
    }

    public void SelectColor()
    { 
        selectColorButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        for (int i = 0; i < curentColors.Length; i++)
        {
            if (selectColorButton.colors.normalColor.Equals(curentColors[i])) return;
        }
                       
        targetButton.transform.GetChild(0).GetComponent<Image>().color = selectColorButton.colors.normalColor;

        switch (targetButton.name)
        {
             case "ButtonLeft":
                 activForm.transform.Find("Layer_1").GetComponent<Image>().color = selectColorButton.colors.normalColor;
                 curentColors[0] = selectColorButton.colors.normalColor;
                 break;
             case "ButtonMidlle":
                 activForm.transform.Find("Layer_2").GetComponent<Image>().color = selectColorButton.colors.normalColor;
                 curentColors[1] = selectColorButton.colors.normalColor;
                 break;
             case "ButtonRight":
                 activForm.transform.Find("Layer_3").GetComponent<Image>().color = selectColorButton.colors.normalColor;
                 curentColors[2] = selectColorButton.colors.normalColor;
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
                activForm = formsPanel.GetChild(i).gameObject;
                InitColors();
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
                activForm = formsPanel.GetChild(i).gameObject;
                InitColors();
                return;
            }
        }
    }

    public void InitColors()
    {
        for (int i = 0; i < mainColor.childCount; i++)
        {
            targetButton = mainColor.GetChild(i).gameObject;
            targetButton.transform.GetChild(0).GetComponent<Image>().color = activForm.transform.GetChild(i + 1).GetComponent<Image>().color;
            curentColors[i] = targetButton.transform.GetChild(0).GetComponent<Image>().color;
        }
    }

    public void InitColorsPalette()
    {
        for (int i = 0; i < colorPanel.childCount; i++)
        {
            ColorPalette.Add(colorPanel.GetChild(i).gameObject.GetComponent<Button>().colors.normalColor);
        }
    }

    public void InitTargetButton()
    {
        targetButton = mainColor.GetChild(0).gameObject;
        targetButton.GetComponent<Image>().sprite = selectedColor;
    }

    public void Logo()
    {
        if (nextCount == 0)
        {
            nextCount++;

            formsAndLogoPanel.GetComponent<Animator>().SetBool("isNext", true);

            selectedForm.transform.Find("Layer_1").GetComponent<Image>().color = activForm.transform.Find("Layer_1").GetComponent<Image>().color;
            selectedForm.transform.Find("Layer_2").GetComponent<Image>().color = activForm.transform.Find("Layer_2").GetComponent<Image>().color;
            selectedForm.transform.Find("Layer_3").GetComponent<Image>().color = activForm.transform.Find("Layer_3").GetComponent<Image>().color;

            formsPanel = GameObject.Find("Logo Panel").GetComponent<RectTransform>();

            SaveFormColor();

            activForm = formsAndLogoPanel.Find("Logo").GetChild(2).GetChild(0).gameObject;
           
            InitColors();           
        }

        else
        {
            SaveLogoColor();
            SceneManager.LoadScene(1);
        } 
    }

    public void SaveFormColor()
    {
        for (int i = 0; i < 5; i++)
        {
            MainManager.Instance.formSprite[i] = activForm.transform.Find("Layer_" + i).GetComponent<Image>().sprite;          
            MainManager.Instance.formColor[i] = activForm.transform.Find("Layer_" + i).GetComponent<Image>().color;
        }
        
    }

    public void SaveLogoColor()
    {
        for (int i = 0; i < 5; i++)
        {
            MainManager.Instance.logoSprite[i] = activForm.transform.Find("Layer_" + i).GetComponent<Image>().sprite;
            MainManager.Instance.logoColor[i] = activForm.transform.Find("Layer_" + i).GetComponent<Image>().color;
        }         
    }
}
