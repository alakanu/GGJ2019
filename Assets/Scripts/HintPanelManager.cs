using UnityEngine;
using UnityEngine.UI;

class HintPanelManager : MonoBehaviour
{
    void Awake()
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
                lines[i].gameObject.SetActive(true);
                lines[i++].text = item;
            }

            while (i < lines.Length)
            {
                lines[i++].gameObject.SetActive(false);
            }
        }
    }

    public void ClearHintPanel()
    {
        gameObject.SetActive(false);
    }

    Text[] lines;
}
