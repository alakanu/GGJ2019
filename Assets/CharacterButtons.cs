using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterButtons : MonoBehaviour
{
	public UIDialogueManager dialogueManager;
	public Button[] characters;
	public Transform[] draggables;
	public EventTrigger[] dropLocations;
	public Button submit;

	void Start()
	{
		int length = characters.Length;
		for (int i = 0; i < length; ++i)
		{
			int index = i;
			characters[i].onClick.AddListener(() => dialogueManager.OnCharacterSelected(index));
			var triggers = characters[i].GetComponent<EventTrigger>().triggers;
			var entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.BeginDrag;
			entry.callback.AddListener( (data) => { OnBeginDrag(index); });
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
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			Debug.Log("Hit " + hit.collider.gameObject.name);
			// Now spawn a 3d character.
		}
	}

	private Transform draggedObject;
}
