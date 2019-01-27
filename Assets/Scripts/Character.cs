using System.Collections.Generic;

class Character
{
    public string Name;
    public Dialogue CurrentDialogue;
    public Dialogue StartingDialogue;

    public string LikedCharacter;
    public string DislikedCharacter;
    public MapSide LikedMapSide;
    public MapSide DislikedMapSide;

    public string HappyEnding;
    public string SadEnding;

    public HashSet<string> DiscoveredHints;

    public int totalScore;
}

enum MapSide
{
    None,
    Mountain,
    Beach,
    Forest,
    River
}

[System.Serializable]
class CharacterJson
{
    public string StartingDialogue;

    public string LikedCharacter;
    public string DislikedCharacter;
    public string LikedMapSide;
    public string DislikedMapSide;
    public string HappyEnding;
    public string SadEnding;
}

