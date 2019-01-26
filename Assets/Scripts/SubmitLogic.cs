using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitLogic : MonoBehaviour
{
    public void SubmitButtonPressed()
    {
        // Get all character tiles boxes
        GameObject[] colliders = GameObject.FindGameObjectsWithTag(CheckBoardMaker.CHECKBOARDBOXESTAG);
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");

        for (int i = 0; i < colliders.Length; i++)
        {
            BoxCollider boxCollider = colliders[i].GetComponent<BoxCollider>();
            for (int j = 0; j < characters.Length; j++)
            {
                if(boxCollider.bounds.Contains(characters[j].transform.position))
                {
                    //Character referenceChar = Character
                    //if(boxCollider.name.Contains(characters[j]))
                    //{

                    //}
                }
            }
        }
                // sum malus and bonus
                // start Playing Epilogue dialogues
    }


}
