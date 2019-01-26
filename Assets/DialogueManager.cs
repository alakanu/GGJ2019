using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	public Text text;
	public Button button1;
	public Button button2;
	public Button button3;
	public Text answer1;
	public Text answer2;
	public Text answer3;

	void Start()
	{
		button1.onClick.AddListener(() => OnAnswerSelected(1));
		button2.onClick.AddListener(() => OnAnswerSelected(2));
		button3.onClick.AddListener(() => OnAnswerSelected(3));
	}

	public void OnCharacterSelected(int index)
	{
		// Load character step.
		text.text = "I am character " + index;
		answer1.text = "asdf";
		answer2.text = "asdf";
		answer3.text = "asdf";
	}

	void OnAnswerSelected(int index)
	{
		Debug.Log("Answer " + index);
		// Move to next character step.
	}
}
