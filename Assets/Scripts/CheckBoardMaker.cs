using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CheckBoardMaker : MonoBehaviour
{
    public float Distance;
    public float Separation;
    public int lateralSize = 3;
    public List<BoxCollider> BoxColliders = new List<BoxCollider>();

    // Start is called before the first frame update
#if UNITY_EDITOR
    void Update()
    {
        DestroyEverything();
        CreateCheckBoard();
    }
#endif

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
        BoxColliders[currentIndex].name = "CharTile_" + currentIndex / lateralSize + "_" + currentIndex % lateralSize;
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
    }
}
