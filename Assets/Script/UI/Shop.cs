using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    [SerializeField]
    private Transform characterHolder;
    [SerializeField]
    private Transform buyButton;

    private int currentCharacterIndex = 0;

    [SerializeField]
    public PlayerData playerData;

    [SerializeField]
    private Text coinText;

    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color disabledColor;

    private void Awake()
    {
        //HidePopup();
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        playerData.LoadDataJSON();
    }

    private void Start()
    {
        ShopInitialize();
        currentCharacterIndex = playerData.GetCurrentMonsterIndex();
    }

    public void ShopInitialize()
    {
        foreach (Transform child in characterHolder)
        {
            Destroy(child.gameObject);
        }

        int index = 0;

        int monsterCount = playerData.GetMonsters().Count;
        for(int i = 0; i < monsterCount; i++)
        {
            GameObject character = Instantiate(playerData.GetMonsterAt(i).prefab, characterHolder);
            character.transform.localScale = new Vector3(100, 100, 100);
            character.name = index++.ToString();

            character.SetActive(false);

            if(i == playerData.GetCurrentMonsterIndex())
            {
                character.SetActive(true);
                Debug.Log("Current monster index: " + playerData.GetCurrentMonsterIndex()); 
            }
        }
    }

    public void NextChar()
    {
        characterHolder.GetChild(currentCharacterIndex).gameObject.SetActive(false);
        currentCharacterIndex++;
        if(currentCharacterIndex >= playerData.GetMonsters().Count)
        {
            currentCharacterIndex = 0;
        }
        characterHolder.GetChild(currentCharacterIndex).gameObject.SetActive(true);
        SetBoughtStatus(currentCharacterIndex);
        if (playerData.GetMonsterAt(currentCharacterIndex).isBought)
        {
            playerData.SetCurrentMonsterIndex(currentCharacterIndex);
            playerData.SaveDataJSON();
        }
    }

    public void PreviousChar()
    {
        characterHolder.GetChild(currentCharacterIndex).gameObject.SetActive(false);
        currentCharacterIndex--;
        if (currentCharacterIndex < 0)
        {
            currentCharacterIndex = playerData.GetMonsters().Count - 1;
        }
        characterHolder.GetChild(currentCharacterIndex).gameObject.SetActive(true);
        SetBoughtStatus(currentCharacterIndex);
        if (playerData.GetMonsterAt(currentCharacterIndex).isBought)
        {
            playerData.SetCurrentMonsterIndex(currentCharacterIndex);
            playerData.SaveDataJSON();
        }
    }

    public void BuyCharacter()
    {
        if(playerData.GetMonsterAt(currentCharacterIndex).price <= playerData.GetGold())
        {
            playerData.BuyMonster(currentCharacterIndex);
            SetBoughtStatus(currentCharacterIndex);
            playerData.SetCurrentMonsterIndex(currentCharacterIndex);
            playerData.SaveDataJSON();
        }
    }

    private void SetBoughtStatus(int index)
    {
        if (playerData.GetMonsterAt(currentCharacterIndex).isBought)
        {
            buyButton.gameObject.SetActive(false);
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            if(playerData.GetMonsterAt(currentCharacterIndex).price <= playerData.GetGold())
            {
                buyButton.GetChild(0).GetComponent<Button>().interactable = true;
            }
            else
            {
                buyButton.GetChild(0).GetComponent<Button>().interactable = false;
            }
        }
    }
    
}
