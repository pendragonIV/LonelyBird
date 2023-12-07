using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public SceneChanger sceneChanger;
    public GameScene gameScene;

    public GameObject player;
    public PlayerData playerData;

    Level currentLevelData;
    #region Game status
    [SerializeField]
    private bool isGameWin = false;
    [SerializeField]
    private bool isGameLose = false;
    #endregion

    private void Start()
    { 
        currentLevelData = LevelManager.instance.levelData.GetLevelAt(LevelManager.instance.currentLevelIndex);
        GameObject map = Instantiate(currentLevelData.map);

        SetUpPlayer();
        SetUpLine();

        Time.timeScale = 1;
    }

    private void SetUpLine()
    {
        LineController.instance.SetLine(currentLevelData.line);
        LineController.instance.InitNewLine(currentLevelData.lineStart, currentLevelData.lineEnd);
    }

    private void SetUpPlayer()
    {
        GameObject playerPref = playerData.GetMonsterAt(playerData.GetCurrentMonsterIndex()).prefab;
        GameObject playerBody = Instantiate(playerPref);
        playerBody.transform.parent = player.transform;
        playerBody.transform.localPosition = Vector3.zero;
        playerBody.transform.localRotation = Quaternion.identity;
        playerBody.transform.localScale = Vector3.one * .5f;

        player.transform.position = currentLevelData.playerSpawnPosition;
    }

    public void Win()
    {
        isGameWin = true;

        gameScene.ShowWinPanel();

        playerData.AddGold(100);

        Time.timeScale = 0;
        LevelManager.instance.levelData.SaveDataJSON();
    }
    public bool IsGameWin()
    {
        return isGameWin;
    }

    public void Lose()
    {
        isGameLose = true;

        gameScene.ShowLosePanel();
        Time.timeScale = 0;
    }
    public bool IsGameLose()
    {
        return isGameLose;
    }
}

