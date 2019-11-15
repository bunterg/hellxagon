using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{

    public GameObject MapPrefab;
    public SpawnManager spawnManager;
    public TileSelector tileSelector;
    private Tilemap LowerLayer;
    private Tilemap UpperLayer;
    private Tilemap SpawnLayer;
    private Tilemap HighlightLayer;


    // Start is called before the first frame update
    void Start()
    {
        GameObject map = Instantiate(MapPrefab);
        Tilemap[] tilemaps = map.transform.GetComponentsInChildren<Tilemap>();

        foreach (Tilemap tilemap in tilemaps)
        {
            switch (tilemap.name)
            {
                case "LowerLayer":
                    LowerLayer = tilemap;
                    break;
                case "UpperLayer":
                    UpperLayer = tilemap;
                    break;
                case "SpawnLayer":
                    SpawnLayer = tilemap;
                    break;
                case "HighlightLayer":
                    HighlightLayer = tilemap;
                    break;
            }
        }

        spawnManager.Tilemap = SpawnLayer;
        tileSelector.PlayerLayer = SpawnLayer;
        tileSelector.HighlightLayer = HighlightLayer;
        tileSelector.UntargetableLayer = UpperLayer;

        spawnManager.enabled = true;
        tileSelector.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
