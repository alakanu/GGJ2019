class Dialogue
{
    public string DialogueText;
    public Answer[] Answers;
    public bool IsFinalDialogue { get { return Answers.Length == 0; } }
}

[System.Serializable]
class DialogueJson
{
    public string DialogueText;
    public AnswerJson[] Answers;
}

