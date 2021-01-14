namespace Tactile.TactileMatch3Challenge.Model
{
    public class LevelObjective
    {
        public int Type { get; }
        public int Target { get; }
        
        public int MaximumMoves { get; }
        
        // Added to prevent class instantiation from any other source
        private LevelObjective() {}

        private LevelObjective(int type, int target, int maximumMoves)
        {
            Type = type;
            Target = target;
            MaximumMoves = maximumMoves;
        }
        
        public static LevelObjective CreateDefaultLevel()
        {
            return new LevelObjective(3, 15, 10);
        }
    }
}