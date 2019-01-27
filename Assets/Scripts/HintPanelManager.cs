using UnityEngine;
using UnityEngine.UI;

class HintPanelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        lines = GetComponentsInChildren<Text>();
        gameObject.SetActive(false);
    }

    public void DisplayCharacterHints(Character character)
    {
        if (character.DiscoveredHints.Count != 0)
        {
            gameObject.SetActive(true);
            int i = 0;
            foreach (var item in character.DiscoveredHints)
            {
                lines[i++].text = item;
            }

        }
    }

    Text[] lines;
}
