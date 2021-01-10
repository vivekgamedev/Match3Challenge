namespace Tactile.TactileMatch3Challenge.Model {
    
    public class Piece {
        
        public int type { get; set; }

        public bool isHorizontalPowerPiece => type == 5;
        public bool isVerticalPowerPiece => type == 6;
        
        public override string ToString() {
            return string.Format("(type:{0})",type);
        }
        
    }
    
}