class Dialogue
{
    public DialogueLine[] DialogueBody;
    public Option[] Options;
    public bool HasOptions { get { return Options.Length != 0; } }
}

[System.Serializable]
class DialogueJson
{
    public DialogueLine[] DialogueBody;
    public OptionJson[] Options;
}

[System.Serializable]
class DialogueLine
{
    public string Speaker;
    public string Line;
}

