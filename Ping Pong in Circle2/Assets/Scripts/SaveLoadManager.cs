using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveLoadManager : MonoBehaviour
{
    public int countOfSkins;
    public int currentSkin;
    public int starCount;
    private string filePath;
    public event Action ChangeSkin;


    private void Start()
    {
        try
        {
            MainMenu.instance.ChangeSkin += OnChangeSkin;
        }
        catch (Exception)
        {

        }
        filePath = Application.persistentDataPath + "/save.gamesave";
        if (!File.Exists(filePath))
        {
            countOfSkins = 0;
            currentSkin = 0;
            starCount = 0;
        }
        else
        {
            LoadGame();
            Debug.Log(currentSkin);
        }
    }

    private void OnChangeSkin(object sender, ChangeSkinEventArgs e)
    {
        currentSkin = e.CurrentSkin;
        ChangeSkin?.Invoke();
        SaveGame();
    }
    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Create);

        var save = new Save();
        save.CountOfSkins = countOfSkins;
        save.CurrentSkin = currentSkin;
        save.StarCount = starCount;

        bf.Serialize(fs, save);
        fs.Close();
    }

    public void LoadGame()
    {
        if (!File.Exists(filePath))
        {
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Open);

        var save = (Save)bf.Deserialize(fs);

        countOfSkins = save.CountOfSkins;
        currentSkin = save.CurrentSkin;
        starCount = save.StarCount;


        fs.Close();
    }
}
[System.Serializable]
internal sealed class Save
{
    public int CountOfSkins { get; set; }
    public int CurrentSkin { get; set; }
    public int StarCount { get; set; }
}
