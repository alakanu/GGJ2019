using System;
using System.Collections.Generic;
using UnityEngine;

public class SubmitLogic : MonoBehaviour
{
    public static event Action ScoresComputed;

    public int likedMapSideBonus = 1;
    public int dislikedMapSideMalus = 1;

    public int likedCharacterBonus = 1;
    public int dislikedCharacterMalus = 1;

    public int notDislikeBonus = 1;

    int[] maskForCheckIndex = { -1, +1, -3, +3 };

    Dictionary<int, Character> _characterMap = new Dictionary<int, Character>();

    public void SubmitButtonPressed()
    {
        _characterMap.Clear();

        // Get all character tiles boxes
        GameObject[] colliders = GameObject.FindGameObjectsWithTag(CheckBoardMaker.CHECKBOARDBOXESTAG);
        //if no dislike +1 points
        foreach (var item in colliders)
        {
            GridTile tile = item.GetComponent<GridTile>();
            tile.character.totalScore = 0;
            _characterMap.Add(tile.Index, tile.character);

            Character referenceChar = tile.character;
            string objectName = item.gameObject.name;
            bool dislikeSomeTerrain = false;
            if (objectName.Contains(referenceChar.LikedMapSide.ToString()))
            {
                referenceChar.totalScore += likedMapSideBonus;
            }
            if (objectName.Contains(referenceChar.DislikedMapSide.ToString()))
            {
                referenceChar.totalScore -= dislikedMapSideMalus;
                dislikeSomeTerrain = true;
            }
            if (!dislikeSomeTerrain)
                referenceChar.totalScore += notDislikeBonus;
        }

        foreach (var evaluatedCharacter in _characterMap)
        {
            bool dislikedSomeone = false;
            foreach (var index in maskForCheckIndex)
            {
                Character otherCharacter;

                int valueToTest = index + evaluatedCharacter.Key;
                int originalYposition = evaluatedCharacter.Key / 3;
                int newYPosition = ( evaluatedCharacter.Key + index ) / 3;

                //when adding or removing 1 from the index we don't want to check
                //different rows. So we skip.
                if (Mathf.Abs(index) == 1 &&
                    originalYposition != newYPosition)
                {
                    continue;
                }

                if (_characterMap.TryGetValue(evaluatedCharacter.Key + index, out otherCharacter))
                {
                    if (evaluatedCharacter.Value.LikedCharacter == otherCharacter.Name)
                    {
                        evaluatedCharacter.Value.totalScore += likedCharacterBonus;
                    }
                    else if (evaluatedCharacter.Value.DislikedCharacter == otherCharacter.Name)
                    {
                        evaluatedCharacter.Value.totalScore -= dislikedCharacterMalus;
                        dislikedSomeone = true;
                    }
                }
            }

            if(!dislikedSomeone)
                evaluatedCharacter.Value.totalScore += notDislikeBonus;
        }

        ScoresComputed();
    }
}
