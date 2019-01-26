﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CheckBoardMaker : MonoBehaviour
{
    public float Distance;
    public float Separation;
    public int lateralSize = 3;
    public List<BoxCollider> BoxColliders = new List<BoxCollider>();
    public List<GameObject> debugCanvases = new List<GameObject>();
    public GameObject CanvasDebug;

    public const string CHECKBOARDBOXESTAG = "CharacterBoxes";

#if !UNITY_EDITOR
    void Start()
    {
        DestroyDebugCanvases();
    }

#endif

    // Start is called before the first frame update
#if UNITY_EDITOR
    void Update()
    {
        DestroyEverything();
        CreateCheckBoard();
        ShowDebugCanvases();
    }
#endif

    void ShowDebugCanvases()
    {
        foreach (var item in BoxColliders)
        {
            GameObject canvas = GameObject.Instantiate(CanvasDebug);
            Text text = canvas.GetComponent<Text>();
            text.text = item.name;
            canvas.transform.parent = this.transform;
            canvas.transform.position = item.transform.position + new Vector3(0f,3f,0f);
        }
    }

    void CreateCheckBoard()
    {
        Vector3 colliderSize = new Vector3(Distance - Separation, 15f, Distance - Separation);
        Vector3 offSetGridToCenter = new Vector3(((Distance * lateralSize) / 2f) - Distance / 2f, 0f, ((Distance * lateralSize) / 2f) - Distance / 2f);
        //Fuck the for loops, It's a JAMMMMMM!
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
        } else if (mountainOrSea == 1)
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
        BoxColliders[currentIndex].name = tileName;
        BoxColliders[currentIndex].tag = CHECKBOARDBOXESTAG;
        // Ignore Raycast layer
        BoxColliders[currentIndex].gameObject.layer = 2;
        BoxColliders[currentIndex].transform.parent = this.transform;
        BoxColliders[currentIndex].size = boxSize;
        BoxColliders[currentIndex].transform.localPosition = new Vector3(((currentIndex / lateralSize) * Distance ) - offSetGridToCenter.x, 0f, ( (currentIndex % lateralSize) * Distance) - offSetGridToCenter.z);
    }

    void DestroyEverything()
    {
        Transform[] allgameObjects = this.transform.GetComponentsInChildren<Transform>();
        foreach (var item in allgameObjects)
        {
            if (item == this.transform)
                continue;
            DestroyImmediate(item.gameObject); 
        }

        BoxColliders.Clear();
        debugCanvases.Clear();
    }


}