public static class IdUtility
{
    public const int BuffPrefix = 101;
    public const int CharacterPrefix = 102;

    // id生成规则是三位数前缀*Capacity 范围是 xxx00000 ~ xxx99999
    public const int Capacity = 100000;

    public static int BuffStartId()
    {
        return BuffPrefix * Capacity;
    }
    
    public static int CharacterStartId()
    {
        return CharacterPrefix * Capacity;
    }

    public static bool IsValidId(int id, int startId)
    {
        return id >= startId && id < startId + Capacity;
    }
}