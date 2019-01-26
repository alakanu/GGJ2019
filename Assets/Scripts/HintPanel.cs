using UnityEngine;
using UnityEngine.UI;

class HintPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        lines = GetComponentsInChildren<Text>();
        gameObject.SetActive(false);
    }

    public void DisplayCharacterHints(Character character)
    {
        int i = 0;

        if (character.likeCharacter != null)
        {
            lines[i].gameObject.SetActive(true);
            lines[i].text = "Likes " + character.likeCharacter;
            ++i;
        }

        if (character.dislikeCharacter != null)
        {
            lines[i].gameObject.SetActive(true);
            lines[i].text = "Dislikes " + character.dislikeCharacter;
            ++i;
        }

        if (character.likeMapSide != MapSide.NotDiscovered)
        {
            lines[i].gameObject.SetActive(true);
            lines[i].text = "Likes " + character.likeMapSide;
            ++i;
        }

        if (character.dislikeMapSide != MapSide.NotDiscovered)
        {
            lines[i].gameObject.SetActive(true);
            lines[i].text = "Dislikes " + character.dislikeMapSide;
            ++i;
        }

        for (; i < lines.Length; i++)
        {
            lines[i].gameObject.SetActive(false);
        }

    }

    Text[] lines;
}
