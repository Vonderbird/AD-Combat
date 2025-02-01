namespace ADC
{
    public struct XpChangeEventArgs
    {
        public int NewXp;
        public int AddedXp;

        public XpChangeEventArgs(int newXp, int addedXp)
        {
            NewXp = newXp;
            AddedXp = addedXp;
        }
    }
}