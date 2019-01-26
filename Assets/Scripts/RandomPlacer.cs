using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider)), ExecuteInEditMode]
public class RandomPlacer : MonoBehaviour
{
    public GameObject[] ObjectsToRandomlyPlace;
    public int RandomSeed;
    public int NumOfObjectsToPlace;
    public float MinDistance;
    public int MaxTries;
    public Vector3 OffsetForRayCast;
    BoxCollider _boxCollider;
    List<GameObject> _placedObjects = new List<GameObject>();

#if !UNITY_EDITOR
    void Start()
    {
        //Remove Unwanted Stuff
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
        SpawnEverything();
    }
#endif

    void Start()
    {
    }

    void SpawnEverything()
    {
        Random.InitState(RandomSeed);
        _boxCollider = GetComponent<BoxCollider>();
        for (int i = 0; i < NumOfObjectsToPlace; i++)
        {
            GameObject possibleObjectToPlace = ObjectsToRandomlyPlace[Random.Range(0, ObjectsToRandomlyPlace.Length - 1)];

            for (int j = 0; j < MaxTries; j++)
            {
                Vector3 possiblePosition = RandomPointInBounds(_boxCollider.bounds);
                Quaternion rotation = Quaternion.Euler(possibleObjectToPlace.transform.rotation.eulerAngles.x, possibleObjectToPlace.transform.rotation.eulerAngles.y + Random.Range(0f, 360f), possibleObjectToPlace.transform.rotation.eulerAngles.z);

                bool isTooCloseToOthers = false;

                RaycastHit hitInfo;
                //Ray ray = new Ray(possiblePosition , -Vector3.up);
                Ray ray = new Ray(possiblePosition + OffsetForRayCast, -Vector3.up);

                if (Physics.Raycast(ray, out hitInfo, 1000f) == false)
                {
                    //LineRenderer lineRenderer = new LineRenderer();
                    //lineRenderer.SetPositions(new Vector3[](){ ray.origin, ray.or  } );
                    continue;
                }

                for (int o = 0; o < _placedObjects.Count; o++)
                {

                    // hit point is too close to others? if so we contine, else we set the possible position to the hit position
                    if ((_placedObjects[o].transform.position - hitInfo.point).magnitude < MinDistance)
                    {
                        isTooCloseToOthers = true;
                    }
                }


                if (isTooCloseToOthers == false)
                {
                    possiblePosition = hitInfo.point;
                    GameObject cloneObject = GameObject.Instantiate(possibleObjectToPlace, possiblePosition, rotation, this.transform);

                    _placedObjects.Add(cloneObject);
                    break;
                }
            }
        }
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
        _placedObjects.Clear();
    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
