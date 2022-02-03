using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
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
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
          + "/MySaveData.dat");
        SaveData data = new SaveData();
        //data.savedformSprite = formSprite;       
       // data.savedlogoSprite = logoSprite;
        //data.savedformColor = formColor;
        //data.savedlogoColor = logoColor;
        data.savedteamName = teamName;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }

    [Serializable]
    class SaveData
    {
       //public Sprite[] savedformSprite = new Sprite[5];
       //public Sprite[] savedlogoSprite = new Sprite[5];
        //public Color[] savedformColor = new Color[5];
        //public Color[] savedlogoColor = new Color[5];
        public string savedteamName;
    }
}
