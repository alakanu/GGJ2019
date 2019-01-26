class Character
{
    public string Name;
    public string WhatIsHome;
    public Dialogue CurrentDialogue;

     string likeCharacter;
     string dislikeCharacter;
     MapSide likeMapSide;
     MapSide dislikeMapSide;
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
}

