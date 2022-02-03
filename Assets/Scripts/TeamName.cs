using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TeamName : MonoBehaviour
{
    [SerializeField] private GameObject form;
    [SerializeField] private GameObject logo;
    [SerializeField] private InputField teamName;
    [SerializeField] private Text inputName;
    [SerializeField] private Button nextButton;

    void Start()
    {
        teamName.GetComponent<InputField>().text = MainManager.Instance.teamName.ToString();

        for (int i = 0; i < 5; i++)
        {
            form.transform.Find("Layer_" + i).GetComponent<Image>().sprite = MainManager.Instance.formSprite[i];
            form.transform.Find("Layer_" + i).GetComponent<Image>().color = MainManager.Instance.formColor[i];

            logo.transform.Find("Layer_" + i).GetComponent<Image>().sprite = MainManager.Instance.logoSprite[i];
            logo.transform.Find("Layer_" + i).GetComponent<Image>().color = MainManager.Instance.logoColor[i];
        }
     
    }

    public void UpdateInput()
    {
        if (teamName.text.Length > 10)
        {
            inputName.color = Color.red;
            nextButton.interactable = false;
        }

        else 
        {
            inputName.color = Color.white;
            nextButton.interactable = true;
        }
    }

    public void SaveAndExit()
    {
        MainManager.Instance.teamName = inputName.text;
        MainManager.Instance.SaveState();


#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
