class Answer
{
    public string AnswerText;
    public Dialogue NextDialogue;
    public DiscoveryType DiscoveryType;

    public bool HasDiscovery
    {
        get
        {
            return DiscoveryType != DiscoveryType.None;
        }
    }
}

[System.Serializable]
class AnswerJson
{
    public string AnswerText;
    public string DiscoveryType;
    public string NextDialogue;
}

enum DiscoveryType
{
    None,
    CharacterLike,
    CharacterDislike,
    MapSideLike,
    MapSideDislike
}
