using System;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveController : MonoBehaviour
{
    private InventoryController inventoryController;
    private String saveLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryController = FindAnyObjectByType<InventoryController>();
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    // Update is called once per frame
    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
          playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
          inventorySaveData = inventoryController.GetInventoryItems()
        };
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));

    }
    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
            inventoryController.SetInventoryItems(saveData.inventorySaveData);
        }
        else
        {
            SaveGame();
        }
    }
}
