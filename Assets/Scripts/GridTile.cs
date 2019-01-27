using UnityEngine;

class GridTile : MonoBehaviour
{
    public Character character;
    public int Index;

    public bool enabler
    {
        get
        {
            return this.transform.GetChild(0).gameObject.activeSelf;
        }
        set
        {
            this.transform.GetChild(0).gameObject.SetActive(value);
        }
    }
}
