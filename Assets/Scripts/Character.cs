class Character
{
    public string Name;
    public string WhatIsHome;
    public Dialogue CurrentDialogue;

    public string likeCharacter;
    public string dislikeCharacter;
    public MapSide likeMapSide;
    public MapSide dislikeMapSide;

    public int totalScore;
}

enum MapSide
{
    NotDiscovered = 0,
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
}

