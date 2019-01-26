using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitLogic : MonoBehaviour
{
    public int likedMapSideBonus = 1;
    public int dislikedMapSideMalus = 1;

    public int likedCharacterBonus = 1;
    public int dislikedCharacterBonus = 1;

    int[] maskForCheckIndex = { -1, +1, -3, +3 };

    const string INDSUBSTRING = "_ind:";

    Dictionary<int, Character> _characterMap = new Dictionary<int, Character>();

    public void SubmitButtonPressed()
    {
        _characterMap.Clear();

        // Get all character tiles boxes
        GameObject[] colliders = GameObject.FindGameObjectsWithTag(CheckBoardMaker.CHECKBOARDBOXESTAG);
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");

        foreach (var item in characters)
        {
            (item.GetComponent<CharacterObject>()).characterRef.totalScore = 0;
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            BoxCollider boxCollider = colliders[i].GetComponent<BoxCollider>();
            string name = boxCollider.name;

            // First count the points for nearby MapSides
            for (int j = 0; j < characters.Length; j++)
            {
                if (boxCollider.bounds.Contains(characters[j].transform.position))
                {
                    Character referenceChar = (characters[j].GetComponent<CharacterObject>()).characterRef;

                    if (name.Contains(referenceChar.likeMapSide.ToString()))
                    {
                        referenceChar.totalScore += likedMapSideBonus;
                    }
                    if (name.Contains(referenceChar.dislikeMapSide.ToString()))
                    {
                        referenceChar.totalScore -= dislikedMapSideMalus;
                    }

                    string indString = name.Substring(name.IndexOf(INDSUBSTRING) + INDSUBSTRING.Length);
                    int index = int.Parse(indString);
                    if (_characterMap.ContainsKey(index) == false)
                    {
                        _characterMap.Add(index, referenceChar);
                    }
                }
            }

            foreach (var item in _characterMap)
            {
                if(_characterMap.ContainsKey())
            }
        }
                // sum malus and bonus
                // start Playing Epilogue dialogues
    }


}
