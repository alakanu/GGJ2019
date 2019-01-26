using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
public class RandomPlacer : MonoBehaviour
{
    public GameObject[] ObjectsToRandomlyPlace;
    public int NumOfObjectsToPlace;
    public float MinDistance;
    public int MaxTries;
    public Vector3 OffsetForRayCast;
    BoxCollider _boxCollider;
    List<GameObject> _placedObjects = new List<GameObject>();

    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        for (int i = 0; i < NumOfObjectsToPlace; i++)
        {
            GameObject possibleObjectToPlace = ObjectsToRandomlyPlace[Random.Range(0, ObjectsToRandomlyPlace.Length - 1)];

            for (int j = 0; j < MaxTries; j++)
            {
                Vector3 possiblePosition = RandomPointInBounds(_boxCollider.bounds);
                Quaternion rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

                bool isTooCloseToOthers = false;

                RaycastHit hitInfo;
                //Ray ray = new Ray(possiblePosition , -Vector3.up);
                Ray ray = new Ray(possiblePosition + OffsetForRayCast, -Vector3.up);

                if (Physics.Raycast(ray, out hitInfo, 1000f) == false)
                {
                    //LineRenderer lineRenderer = new LineRenderer();
                    //lineRenderer.SetPositions(new Vector3[](){ ray.origin, ray.or  } );
                    Debug.Log("no hits");
                    continue;
                }

                for (int o = 0; o < _placedObjects.Count; o++)
                {

                    // hit point is too close to others? if so we contine, else we set the possible position to the hit position
                    if ((_placedObjects[o].transform.position - hitInfo.point).magnitude < MinDistance)
                    {
                        isTooCloseToOthers = true;
                        Debug.Log("too close to others");
                    }
                    else
                    {
                        possiblePosition = hitInfo.point;
                    }
                }

                if (isTooCloseToOthers == false)
                {
                    GameObject cloneObject = GameObject.Instantiate(possibleObjectToPlace, possiblePosition, rotation, this.transform);

                    _placedObjects.Add(cloneObject);
                    break;
                }
            }
        }
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
