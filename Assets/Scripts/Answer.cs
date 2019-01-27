class Option
{
    public string OptionText;
    public Dialogue NextDialogue;
    public string Hint;

    public bool YieldsHint
    {
        get
        {
            return Hint != null;
        }
    }
}

[System.Serializable]
class OptionJson
{
    public string OptionText;
    public string Hint;
    public string NextDialogue;
}