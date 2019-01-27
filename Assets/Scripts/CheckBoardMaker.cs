using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CheckBoardMaker : MonoBehaviour
{
    public GameObject GridSelector;
    public float GridSelectorSize = 1.0f;
    public float VerticalOffset = 1.0f; 
    public float Distance;
    public float Separation;
    public int lateralSize = 3;
    public List<BoxCollider> BoxColliders = new List<BoxCollider>();
    public List<GameObject> debugCanvases = new List<GameObject>();
    public bool CreateDebugCanvas = false;
    public GameObject CanvasDebug;

    public const string CHECKBOARDBOXESTAG = "CharacterBoxes";

#if !UNITY_EDITOR
    void Start()
    {
        foreach (var item in debugCanvases)
        {
            Destroy(item);
        }
        debugCanvases.Clear();
    }

#endif

    // Start is called before the first frame update
#if UNITY_EDITOR
    void Update()
    {
        if (Application.isPlaying)
        {
            return;
        }
        DestroyEverything();
        CreateCheckBoard();
        if (CreateDebugCanvas)
            CreateAndShowDebugCanvas();
    }
#endif

    void CreateAndShowDebugCanvas()
    {
        foreach (var item in BoxColliders)
        {
            GameObject canvas = GameObject.Instantiate(CanvasDebug, transform, false);
            Text text = canvas.GetComponent<Text>();
            text.text = item.name;
            canvas.transform.position = item.transform.position + new Vector3(0f, 3f, 0f);
        }
    }

    void CreateCheckBoard()
    {
        Vector3 colliderSize = new Vector3(Distance - Separation, 15f, Distance - Separation);
        Vector3 offSetGridToCenter = new Vector3(((Distance * lateralSize) / 2f) - Distance / 2f, 0f, ((Distance * lateralSize) / 2f) - Distance / 2f);
        BoxColliders = new List<BoxCollider>();
        int totalBoxes = lateralSize * lateralSize;

        for (int i = 0; i < totalBoxes; i++)
        {
            AddBoxCollider(colliderSize, offSetGridToCenter, i);
        }
    }

    void AddBoxCollider(Vector3 boxSize, Vector3 offSetGridToCenter, int currentIndex)
    {
        GameObject gameObject = new GameObject();
        BoxColliders.Add(gameObject.AddComponent<BoxCollider>());
        string tileName = "CharTile_";
        int mountainOrSea = (currentIndex % lateralSize) - 1;
        if (mountainOrSea == -1)
        {
            tileName += "_Mountains";
        }
        else if (mountainOrSea == 1)
        {
            tileName += "_Beach";
        }
        int riverOrSea = (currentIndex / lateralSize) - 1;
        if (riverOrSea == -1)
        {
            tileName += "_Forest";
        }
        else if (riverOrSea == 1)
        {
            tileName += "_River";
        }
        //Don't add after this index!
        tileName += "ind:" + currentIndex;
        GridTile gridTile = BoxColliders[currentIndex].gameObject.AddComponent<GridTile>();
        gridTile.Index = currentIndex;
        BoxColliders[currentIndex].name = tileName;
        BoxColliders[currentIndex].tag = CHECKBOARDBOXESTAG;
        // Ignore Raycast layer
        BoxColliders[currentIndex].gameObject.layer = 2;
        BoxColliders[currentIndex].transform.parent = this.transform;
        BoxColliders[currentIndex].size = boxSize;
        BoxColliders[currentIndex].transform.localPosition = new Vector3(((currentIndex / lateralSize) * Distance) - offSetGridToCenter.x, 0f, ((currentIndex % lateralSize) * Distance) - offSetGridToCenter.z);

        GameObject gridSelector = GameObject.Instantiate(GridSelector, gameObject.transform);
        gridSelector.transform.localScale = new Vector3(GridSelectorSize, GridSelectorSize, GridSelectorSize);
        Vector3 gridPosition = gridSelector.transform.position;
        gridSelector.transform.position = new Vector3(gridPosition.x, gridPosition.y + VerticalOffset, gridPosition.z);
    }

    void DestroyEverything()
    {
        Transform[] allgameObjects = this.transform.GetComponentsInChildren<Transform>();
        foreach (var item in allgameObjects)
        {
            if (item== null ||
                item == this.transform)
                continue;
            DestroyImmediate(item.gameObject);
        }

        BoxColliders.Clear();
        debugCanvases.Clear();
    }


}
