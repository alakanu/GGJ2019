using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSubmitOperations : MonoBehaviour
{
    Character[] characters = new Character[9];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i] = new Character();
        }

        characters[0].Name = "Piero";
        characters[0].DislikedCharacter = "Gian";
        characters[0].DislikedMapSide = MapSide.Mountains;
        characters[0].LikedCharacter = "Gian";
        characters[0].LikedMapSide = MapSide.Beach;

        characters[1].Name = "Gian";
        characters[1].DislikedCharacter = "Piero";
        characters[1].DislikedMapSide = MapSide.Beach;
        characters[1].LikedCharacter = "Tony";
        characters[1].LikedMapSide = MapSide.Mountains;

        characters[2].Name = "Carlo";
        characters[2].DislikedCharacter = "Figa";
        characters[2].DislikedMapSide = MapSide.Mountains;
        characters[2].LikedCharacter = "Gian";
        characters[2].LikedMapSide = MapSide.Beach;

        characters[3].Name = "Tony";
        characters[3].DislikedCharacter = "Tony";
        characters[3].DislikedMapSide = MapSide.Mountains;
        characters[3].LikedCharacter = "Dio";
        characters[3].LikedMapSide = MapSide.Beach;

        characters[4].Name = "Simo";
        characters[4].DislikedCharacter = "Dio";
        characters[4].DislikedMapSide = MapSide.Mountains;
        characters[4].LikedCharacter = "Gian";
        characters[4].LikedMapSide = MapSide.Beach;

        characters[5].Name = "Dio";
        characters[5].DislikedCharacter = "Gian";
        characters[5].DislikedMapSide = MapSide.Mountains;
        characters[5].LikedCharacter = "Cane";
        characters[5].LikedMapSide = MapSide.Beach;

        characters[6].Name = "Cane";
        characters[6].DislikedCharacter = "Tony";
        characters[6].DislikedMapSide = MapSide.Mountains;
        characters[6].LikedCharacter = "Gian";
        characters[6].LikedMapSide = MapSide.Beach;

        characters[7].Name = "Porco";
        characters[7].DislikedCharacter = "Gian";
        characters[7].DislikedMapSide = MapSide.River;
        characters[7].LikedCharacter = "Gian";
        characters[7].LikedMapSide = MapSide.Beach;

        characters[8].Name = "Cazzo";
        characters[8].DislikedCharacter = "Dio";
        characters[8].DislikedMapSide = MapSide.Mountains;
        characters[8].LikedCharacter = "Porco";
        characters[8].LikedMapSide = MapSide.Beach;

        var colliders = GameObject.FindGameObjectsWithTag(CheckBoardMaker.CHECKBOARDBOXESTAG);

        int counter = 0;
        foreach (var item in colliders)
        {
            GridTile tile = item.GetComponent<GridTile>();
            tile.character = characters[counter];
            counter++;
        }

        SubmitLogic logic = GameObject.FindObjectOfType<SubmitLogic>();
        logic.SubmitButtonPressed();

        foreach (var item in characters)
        {
            Debug.Log(item.Name + " " + item.totalScore);
        }
    }
}
