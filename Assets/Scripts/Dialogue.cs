class Dialogue
{
    public DialogueLine[] DialogueBody;
    public Answer[] Options;
    public bool HasOptions { get { return Options.Length != 0; } }
}

[System.Serializable]
class DialogueJson
{
    public DialogueLine[] DialogueBody;
    public AnswerJson[] Options;
}

[System.Serializable]
class DialogueLine
{
    public string Speaker;
    public string Line;
}

