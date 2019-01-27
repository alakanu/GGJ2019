using System;
using System.Collections.Generic;
using UnityEngine;

public class SubmitLogic : MonoBehaviour
{
    public static event Action ScoresComputed;

    public int likedMapSideBonus = 1;
    public int dislikedMapSideMalus = 1;

    public int likedCharacterBonus = 1;
    public int dislikedCharacterBonus = 1;

    int[] maskForCheckIndex = { -1, +1, -3, +3 };

    Dictionary<int, Character> _characterMap = new Dictionary<int, Character>();

    public void SubmitButtonPressed()
    {
        _characterMap.Clear();

        // Get all character tiles boxes
        GameObject[] colliders = GameObject.FindGameObjectsWithTag(CheckBoardMaker.CHECKBOARDBOXESTAG);

        foreach (var item in colliders)
        {
            GridTile tile = item.GetComponent<GridTile>();
            tile.character.totalScore = 0;
            _characterMap.Add(tile.Index, tile.character);

            Character referenceChar = tile.character;

            if (name.Contains(referenceChar.LikedMapSide.ToString()))
            {
                referenceChar.totalScore += likedMapSideBonus;
            }
            if (name.Contains(referenceChar.DislikedMapSide.ToString()))
            {
                referenceChar.totalScore -= dislikedMapSideMalus;
            }
        }

        foreach (var evaluatedCharacter in _characterMap)
        {
            foreach (var index in maskForCheckIndex)
            {
                Character otherCharacter;
                if (_characterMap.TryGetValue(evaluatedCharacter.Key + index, out otherCharacter))
                {
                    if (evaluatedCharacter.Value.LikedCharacter == otherCharacter.Name)
                    {
                        evaluatedCharacter.Value.totalScore += likedCharacterBonus;
                    }
                    else if (evaluatedCharacter.Value.DislikedCharacter == otherCharacter.Name)
                    {
                        evaluatedCharacter.Value.totalScore += likedCharacterBonus;
                    }
                }
            }
        }

        ScoresComputed();
    }
}
