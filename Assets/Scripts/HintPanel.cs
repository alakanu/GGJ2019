using UnityEngine;
using UnityEngine.UI;

class HintPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        lines = GetComponentsInChildren<Text>();
    }

    public void DisplayCharacterHints(Button characterButton, Character character)
    {

    }

    Text[] lines;
}
