using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

class CharacterPanelManager : MonoBehaviour
{
    public event Action<int> OnCharacterSelected;

    public Button[] characters;
    public Transform[] draggables;
    public EventTrigger[] dropLocations;
    public Button submit;

    void Start()
    {
        mainCamera = Camera.main;

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
        draggables[index].position = Input.mousePosition;
    }

    void OnEndDrag(int index)
    {
        draggables[index].gameObject.SetActive(false);

        // Cast a ray, see where in the grid the character was dropped.
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Hit " + hit.collider.gameObject.name);
            // Now spawn a 3d character.
        }
    }

    Transform draggedObject;
    Camera mainCamera;
    HintPanel hintPanel;
}
