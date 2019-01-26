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
        gameObject.SetActive(true);

        int hintCount = 0;
        bool[] discoveries = character.Discoveries;

        if (discoveries[0])
        {
            lines[hintCount].gameObject.SetActive(true);
            lines[hintCount].text = "Likes " + character.LikedCharacter;
            ++hintCount;
        }

        if (discoveries[1])
        {
            lines[hintCount].gameObject.SetActive(true);
            lines[hintCount].text = "Dislikes " + character.DislikedCharacter;
            ++hintCount;
        }

        if (discoveries[2])
        {
            lines[hintCount].gameObject.SetActive(true);
            lines[hintCount].text = "Likes " + character.LikedMapSide;
            ++hintCount;
        }

        if (discoveries[3])
        {
            lines[hintCount].gameObject.SetActive(true);
            lines[hintCount].text = "Dislikes " + character.DislikedMapSide;
            ++hintCount;
        }

        if (hintCount == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            for (; hintCount < lines.Length; hintCount++)
            {
                lines[hintCount].gameObject.SetActive(false);
            }
        }
    }

    Text[] lines;
}
