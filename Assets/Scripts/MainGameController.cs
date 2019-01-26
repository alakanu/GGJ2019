using System.Collections.Generic;
using UnityEngine;

class MainGameController : MonoBehaviour
{
    Dictionary<string, Dialogue> dialogues;
    // Start is called before the first frame update
    void Start()
    {
        dialogues = MyJsonUtility.LoadDialogues();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
