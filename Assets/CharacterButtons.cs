using UnityEngine;
using UnityEngine.UI;

public class CharacterButtons : MonoBehaviour
{
	public DialogueManager dialogueManager;
	public Button character1;
	public Button character2;
	public Button character3;
	public Button character4;
	public Button character5;
	public Button character6;
	public Button character7;
	public Button character8;
	public Button character9;
	public Button submit;

	void Start()
	{
		character1.onClick.AddListener(() => dialogueManager.OnCharacterSelected(1));
		character2.onClick.AddListener(() => dialogueManager.OnCharacterSelected(2));
		character3.onClick.AddListener(() => dialogueManager.OnCharacterSelected(3));
		character4.onClick.AddListener(() => dialogueManager.OnCharacterSelected(4));
		character5.onClick.AddListener(() => dialogueManager.OnCharacterSelected(5));
		character6.onClick.AddListener(() => dialogueManager.OnCharacterSelected(6));
		character7.onClick.AddListener(() => dialogueManager.OnCharacterSelected(7));
		character8.onClick.AddListener(() => dialogueManager.OnCharacterSelected(8));
		character9.onClick.AddListener(() => dialogueManager.OnCharacterSelected(9));
		submit.onClick.AddListener(Submit);
	}

	void Submit()
	{
		Debug.Log("Submit");
		// Characters say if they're happy or not. Maybe explode.
	}
}
