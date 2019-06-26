namespace Growth.Util
{
    internal class Configs
    {
        internal static readonly string DB_ConnectionString = 
            "server=118.25.157.138;uid=admin;pwd=trymesoft;database=film";
    }

    public enum TomatoState
    {
        breakState = -1,
        runState = 0,
        endState = 1
    }
}