using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public Color[] formColorToSave = new Color[10];
    public Color[] logoColorToSave = new Color[10];
    public Sprite[] formSprite = new Sprite[5];
    public Sprite[] logoSprite = new Sprite[5];
    public Color[] formColor = new Color[5];
    public Color[] logoColor = new Color[5];
    public string teamName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveState()
    {       
        SaveData data = new SaveData();      
        data.savedformColor = formColorToSave;       
        data.savedlogoColor = logoColorToSave;
        data.savedteamName = teamName;
        
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/SavedParam.json", json);
        Debug.Log(json);
    }

    public void LoadState()
    {
        string path = Application.persistentDataPath + "/SavedParam.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            formColorToSave = data.savedformColor;
            logoColorToSave = data.savedlogoColor;
            teamName = data.savedteamName;
        }
        else Debug.Log("The file is missing");
    }

    [Serializable]
    public class SaveData
    {
        public Color[] savedformColor = new Color[10];
        public Color[] savedlogoColor = new Color[10];
        public string savedteamName;
    }
}
