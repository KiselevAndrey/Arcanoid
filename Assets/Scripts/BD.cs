
public static class AxesNames
{
    public static string Horizontal = "Horizontal";
}

public static class TagsNames
{
    public const string Ball = "Ball";
    public const string Block = "Block";
    public const string Bonus = "Bonus";
    public const string Wall = "Wall";
    public const string Player = "Player";
    public const string Respawn = "Respawn";
    public const string BlockInstantiater = "Block Instantiater";
    public const string Savior = "Savior";
}

public static class BonusNames
{
    public const string Life = "Life";
    public const string Speed = "Speed";
    public const string Damage = "Damage";
}

public static class LVLNames
{
    public static string IntToLVLName(int lvl) => lvl + "_LVL";
    public static int LVLNameToInt(string sceneName) => sceneName[0];
}

public static class MixerGroup
{
    public static string VolumeBackGround = "Volume BackGround";
    public static string VolumeMaster = "Volume Master";
}