class Character
{
    public string Name;
    public string WhatIsHome;
    public Dialogue CurrentDialogue;

    public string LikedCharacter;
    public string DislikedCharacter;
    public MapSide LikedMapSide;
    public MapSide DislikedMapSide;

    public bool[] Discoveries;

    public int totalScore;
}

enum MapSide
{
    Mountains,
    Beach,
    Forest,
    River
}

[System.Serializable]
class CharacterJson
{
    public string StartingDialogue;
    public string WhatIsHome;

    public string LikedCharacter;
    public string DislikedCharacter;
    public string LikedMapSide;
    public string DislikedMapSide;
}

