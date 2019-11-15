using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuLoader : MonoBehaviour
{
    private Stage[] stages;
    public StageListItem stageListItemPrefab;
    public Transform stageList;
    public Stage selectedStage;
    public StageDisplay stageDisplay;
    private GameConfig gameConfig;
    private GameObject app;
    void Awake()
    {
        if (GameObject.Find("__app") == null)
        {
            SceneManager.LoadScene("_preload", LoadSceneMode.Additive);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        app = GameObject.Find("__app");
        gameConfig = app.GetComponent<GameConfig>();

        stages = Resources.LoadAll<Stage>("Prefabs/Stages");
        foreach (var stage in stages)
        {
            Debug.Log(stage.name);
            StageListItem stageListItem = Instantiate(stageListItemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            stageListItem.stage = stage;
            stageListItem.transform.SetParent(stageList);
            stageListItem.GetComponent<Button>().onClick.AddListener(() => { changeSelectedStage(stage); }) ;
        }
    }

    void changeSelectedStage(Stage stage)
    {
        stageDisplay.setStage(stage);
        gameConfig.stage = stage;
        Debug.Log(gameConfig.stage);
    }

    public void Play()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
