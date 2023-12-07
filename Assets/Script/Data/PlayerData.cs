using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum MonsterType
{
    Chicken,
    OneEye,
    Worm,
    Ghost,
}

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]    
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private int gold;
    [SerializeField]
    private List<Monster> monsters;
    [SerializeField]
    private int currentMonsterIndex;

    public void AddGold(int gold) { this.gold += gold; }
    public int GetGold(){ return gold; }
    public List<Monster> GetMonsters() { return monsters; }
    public Monster GetMonsterAt(int index) { return monsters[index]; }
    public int GetCurrentMonsterIndex() { return currentMonsterIndex; }
    public void SetCurrentMonsterIndex(int index) { currentMonsterIndex = index; }

    public void BuyMonster(int index)
    {
        if (gold >= monsters[index].price)
        {
            gold -= monsters[index].price;
            monsters[index].isBought = true;
        }
    }

    public void UseMonster(int index)
    {
        if (monsters[index].isBought)
        {
            monsters[currentMonsterIndex].isUsing = false;
            monsters[index].isUsing = true;
            currentMonsterIndex = index;
        }
    }

    #region Save and Load
    public void SaveDataJSON()
    {
        string data = JsonUtility.ToJson(this, true);
        WriteFile(data, "/PlayerData.json");
    }

    public void LoadDataJSON()
    {
        string content = ReadFile("/PlayerData.json");
        if (content != null && content != "{}")
        {
            JsonUtility.FromJsonOverwrite(content, this);
        }
    }

    private void WriteFile(string content, string path)
    {
        FileStream file = new FileStream(Application.persistentDataPath + path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(file))
        {
            writer.Write(content);
        }
    }

    private string ReadFile(string path)
    {
        if (File.Exists(Application.persistentDataPath + path))
        {
            FileStream file = new FileStream(Application.persistentDataPath + path, FileMode.Open);

            using (StreamReader reader = new StreamReader(file))
            {
                return reader.ReadToEnd();
            }
        }
        else
        {
            return null;
        }
    }
    #endregion
}

[System.Serializable]
public class Monster
{
    public MonsterType monsterType;
    public GameObject prefab;
    public int price;
    public bool isBought;
    public bool isUsing;
}
