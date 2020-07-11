using System;

[Serializable]
public class SaveData
{
    public static SaveData CurrentSd;

    public static SaveData Current
    {
        get => CurrentSd ?? (CurrentSd = new SaveData());
        set
        {
            if(value != null)
            {
                CurrentSd = value;
            }
        }
    }

    public PlayerProfile profile;
}
