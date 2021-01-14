namespace Tactile.TactileMatch3Challenge.Model
{
    public class Game
    {
        public LevelObjective LevelObjective { get; private set; }
        public int MovesUsed { get; private set; }
        public int TargetAchieved { get; private set; }

        public void Initialize(LevelObjective objective)
        {
            LevelObjective = objective;
            ResetLevel();
        }

        public void ResetLevel()
        {
            MovesUsed = 0;
            TargetAchieved = 0;
        }

        public void IncrementMoveCount()
        {
            ++MovesUsed;
        }

        public void IncrementTargetAchieved()
        {
            ++TargetAchieved;
        }
    }
}
