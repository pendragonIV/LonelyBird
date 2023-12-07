using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScene : MonoBehaviour
{
    public static LevelScene instance;

    [SerializeField]
    private Transform levelHolderPrefab;
    [SerializeField]
    private Transform levelsContainer;

    public Transform sceneTransition;

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

    void Start()
    {
        PrepareLevels();
    }
    public void PlayChangeScene()
    {
        sceneTransition.GetComponent<Animator>().Play("SceneTransitionReverse");
    }

    private void PrepareLevels()
    {
        for (int i = 0; i < LevelManager.instance.levelData.GetLevels().Count; i++)
        {
            Transform holder = Instantiate(levelHolderPrefab, levelsContainer);
            holder.name = i.ToString();
            Level level = LevelManager.instance.levelData.GetLevelAt(i);
            if (LevelManager.instance.levelData.GetLevelAt(i).isPlayable)
            {
                holder.GetComponent<LevelHolder>().EnableHolder();
            }
            else
            {
                holder.GetComponent<LevelHolder>().DisableHolder();
            }
            //holder.rotation = Quaternion.Euler(0, 120, 0);
            holder.DORotate(new Vector3(0, 0, 0), 0.5f).SetDelay(i * 0.2f);   
            holder.DOScale(new Vector3(1, 1, 1), 0.2f).SetDelay(i * 0.2f).OnComplete(() => holder.DOKill());
        }
    }

}
