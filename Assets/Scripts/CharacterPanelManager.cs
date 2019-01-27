using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

class CharacterPanelManager : MonoBehaviour
{
    public event Action<int> OnCharacterSelected = i => { };

    public Button[] characterButtons;
    public Transform[] draggables;
    public Button submit;

    public void SetCharacters(Character[] chars)
    {
        characters = chars;
    }

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();

        int length = characterButtons.Length;
        for (int i = 0; i < length; ++i)
        {
            int index = i;
            draggables[i].gameObject.SetActive(false);
            characterButtons[i].onClick.AddListener(
                () =>
                {
                    OnCharacterSelected(index);
                }
                );
            var triggers = characterButtons[i].GetComponent<EventTrigger>().triggers;
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
        boardCells = new GridTile[9];
        var cell = GameObject.Find("CharTile__Mountains_River").GetComponent<GridTile>();
        boardCells[cell.Index] = cell;
        cell = GameObject.Find("CharTile__Mountains").GetComponent<GridTile>();
        boardCells[cell.Index] = cell;
        cell = GameObject.Find("CharTile__Mountains_Forest").GetComponent<GridTile>();
        boardCells[cell.Index] = cell;
        cell = GameObject.Find("CharTile__River").GetComponent<GridTile>();
        boardCells[cell.Index] = cell;
        cell = GameObject.Find("CharTile_").GetComponent<GridTile>();
        boardCells[cell.Index] = cell;
        cell = GameObject.Find("CharTile__Forest").GetComponent<GridTile>();
        boardCells[cell.Index] = cell;
        cell = GameObject.Find("CharTile__Beach_River").GetComponent<GridTile>();
        boardCells[cell.Index] = cell;
        cell = GameObject.Find("CharTile__Beach").GetComponent<GridTile>();
        boardCells[cell.Index] = cell;
        cell = GameObject.Find("CharTile__Beach_Forest").GetComponent<GridTile>();
        boardCells[cell.Index] = cell;

        cellColliders = new Collider[9];
        cellPivots = new Vector3[9];

        for (int i = 0; i < 9; ++i)
        {
            cellColliders[i] = boardCells[i].GetComponent<Collider>();
            var origin = cellColliders[i].transform.position + Vector3.up * 10.0f;
            RaycastHit hit;
            if (Physics.Raycast(origin, Vector3.down, out hit, terrainMask))
                cellPivots[i] = hit.point;
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
            // How high above the ground.
            const float HEIGHT = 0.0f;
            draggables[index].position = hit.point + Vector3.up * HEIGHT;
        }
        else
            Debug.LogWarning("Terrain collider not found.");
    }

    void OnEndDrag(int index)
    {
        // Remove character from board if it was already there.
        for (int i = 0; i < 9; ++i)
        {
            var charInCell = boardCells[i].character;
            if (charInCell != null && charInCell.Name == characters[index].Name)
                boardCells[i].character = null;
        }

        int cellHit = GetCellUnderCursor();

        if (cellHit != -1 && boardCells[cellHit].character == null)
        {
            boardCells[cellHit].character = characters[index];
            draggables[index].position = cellPivots[cellHit];
        }
        else
        {
            draggables[index].gameObject.SetActive(false);
        }
    }

    // cellHit == -1 means no cell hit.
    int GetCellUnderCursor()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        int cellHit = -1;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, terrainMask))
        {
            int length = cellColliders.Length;
            for (int i = 0; i < length; ++i)
            {
                if (cellColliders[i].bounds.Contains(hit.point))
                {
                    cellHit = i;
                    break;
                }
            }
        }

        return cellHit;
    }

    void RemoveCharacterFromBoard(Character c)
    {
        for (int i = 0; i < 9; ++i)
        {
            if (boardCells[i].character.Name == characters[i].Name)
                boardCells[i].character = null;
        }
    }

    // Check if we are dragging from the board.
    void Update()
    {
        // Begin drag.
        if (Input.GetMouseButtonDown(0))
        {
            int cellHit = GetCellUnderCursor();

            if (cellHit != -1 && boardCells[cellHit].character != null)
            {
                draggingFromCell = true;

                // Get index from character.
                int characterIndex = -1;
                var character = boardCells[cellHit].character;
                for (int i = 0; i < 9; ++i)
                {
                    if (characters[i].Name == character.Name)
                        characterIndex = i;
                }

                characterDraggedFromCell = characterIndex;
            }
        }

        // Drag.
        if (Input.GetMouseButton(0) && draggingFromCell)
        {
            OnDrag(characterDraggedFromCell);
        }

        // End drag.
        if (Input.GetMouseButtonUp(0) && draggingFromCell)
        {
            draggingFromCell = false;
            OnEndDrag(characterDraggedFromCell);
        }
    }

    Transform draggedObject;
    Camera mainCamera;
    HintPanelManager hintPanel;
    LayerMask terrainMask;
    Character[] characters;
    GridTile[] boardCells;
    Collider[] cellColliders;
    Vector3[] cellPivots;
    bool draggingFromCell = false;
    int characterDraggedFromCell;
}
