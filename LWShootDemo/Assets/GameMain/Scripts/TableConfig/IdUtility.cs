public static class IdUtility
{
    public const int BuffPrefix = 101;
    public const int CharacterPrefix = 102;
    public const int EntityPrefix = 103;
    
    public const int ProjectilePrefix = 201;
    public const int ProjectileLauncherPrefix = 202;
    
    public const int AoePrefix = 301;

    public const int ItemPrefix = 601;

    // id生成规则是三位数前缀*Capacity 范围是 xxx00000 ~ xxx99999
    public const int Capacity = 100000;

    public static int ItemId()
    {
        return ItemPrefix * Capacity;
    }

    public static int AoeStartId()
    {
        return AoePrefix * Capacity;
    }

    public static int BuffStartId()
    {
        return BuffPrefix * Capacity;
    }
    
    public static int CharacterStartId()
    {
        return CharacterPrefix * Capacity;
    }
    
    public static int EntityStartId()
    {
        return EntityPrefix * Capacity;
    }
    
    public static int ProjectileStartId()
    {
        return ProjectilePrefix * Capacity;
    }
    
    public static int ProjectileLauncherStartId()
    {
        return ProjectileLauncherPrefix * Capacity;
    }

    public static bool IsValidId(int id, int startId)
    {
        return id >= startId && id < startId + Capacity;
    }
}