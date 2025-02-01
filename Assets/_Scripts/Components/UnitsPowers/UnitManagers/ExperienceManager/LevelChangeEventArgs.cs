namespace ADC
{
    public struct LevelChangeEventArgs
    {
        public int NewLevel;
        public int AddedLevel;

        public LevelChangeEventArgs(int newLevel, int addedLevel)
        {
            NewLevel = newLevel;
            AddedLevel = addedLevel;
        }
    }
}