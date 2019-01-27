using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

class CharacterPanelManager : MonoBehaviour
{
    public event Action<int> OnCharacterSelected;

    public Button[] characters;
    public Transform[] draggables;
    public Button submit;

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();

        int length = characters.Length;
        for (int i = 0; i < length; ++i)
        {
            int index = i;
            draggables[i].gameObject.SetActive(false);
            characters[i].onClick.AddListener(
                () =>
                {
                    OnCharacterSelected(index);
                }
                );
            var triggers = characters[i].GetComponent<EventTrigger>().triggers;
            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.BeginDrag;
            entry.callback.AddListener((data) => { OnBeginDrag(index); });
            triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Drag;
            entry.callback.AddListener((data) => { OnDrag(index); });
            triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.EndDrag;
            entry.callback.AddListener((data) => { OnEndDrag(index); });
            triggers.Add(entry);
        }

        submit.onClick.AddListener(Submit);
        terrainMask = 1 << LayerMask.NameToLayer("Terrain");

        // I have to do this because the cells are instantiated in real time.
        boardCells = new Collider[9];
        boardCells[0] = GameObject.Find("CharTile__Mountains_River").GetComponent<Collider>();
        boardCells[1] = GameObject.Find("CharTile__Mountains").GetComponent<Collider>();
        boardCells[2] = GameObject.Find("CharTile__Mountains_Forest").GetComponent<Collider>();
        boardCells[3] = GameObject.Find("CharTile__River").GetComponent<Collider>();
        boardCells[4] = GameObject.Find("CharTile_").GetComponent<Collider>();
        boardCells[5] = GameObject.Find("CharTile__Forest").GetComponent<Collider>();
        boardCells[6] = GameObject.Find("CharTile__Beach_River").GetComponent<Collider>();
        boardCells[7] = GameObject.Find("CharTile__Beach").GetComponent<Collider>();
        boardCells[8] = GameObject.Find("CharTile__Beach_Forest").GetComponent<Collider>();

        cellPivots = new Vector3[9];

        for (int i = 0; i < 9; ++i)
        {
            var origin = boardCells[i].transform.position + Vector3.up * 10.0f;
            RaycastHit hit;
            if (Physics.Raycast(origin, Vector3.down, out hit, terrainMask))
            {
                cellPivots[i] = hit.point;
            }
            else
                Debug.LogWarning("Terrain collider not found.");
        }
    }

    void Submit()
    {
        Debug.Log("Submit");
        // Characters say if they're happy or not. Maybe explode.
    }

    void OnBeginDrag(int index)
    {
        draggables[index].gameObject.SetActive(true);
    }

    void OnDrag(int index)
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, terrainMask))
        {
            // Some point above the ground.
            const float HEIGHT = 1.0f;
            draggables[index].position = hit.point + Vector3.up * HEIGHT;
        }
        else
            Debug.LogWarning("Terrain collider not found.");
    }

    void OnEndDrag(int index)
    {
        // Cast a ray, see where in the grid the character was dropped.
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, terrainMask))
        {
            int length = boardCells.Length;
            for (int i = 0; i < length; ++i)
            {
                if (boardCells[i].bounds.Contains(hit.point))
                {
                    draggables[index].position = cellPivots[i];
                    return;
                }
            }
        }

        // if we didn't hit a cell.
        draggables[index].gameObject.SetActive(false);
    }

    Transform draggedObject;
    Camera mainCamera;
    HintPanelManager hintPanel;
    LayerMask terrainMask;
    Collider[] boardCells;
    Vector3[] cellPivots;
}
