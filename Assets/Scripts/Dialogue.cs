class Dialogue
{
    public DialogueLine[] DialogueBody;
    public Answer[] Answers;
    public bool HasFinalChoice { get { return Answers.Length != 0; } }
}

[System.Serializable]
class DialogueJson
{
    public DialogueLine[] DialogueBody;
    public AnswerJson[] Answers;
}

[System.Serializable]
class DialogueLine
{
    public string Speaker;
    public string Line;
}

