using UnityEngine;

namespace Tomino
{
    public class TargetOutline
    {
        public readonly Position[] positions;

        public TargetOutline()
        {
            positions = new Position[] {
                new Position(1, 1),
                new Position(2, 1)
            };
        }

        public TargetOutline(Position[] positions)
        {
            this.positions = positions;
        }
    }
}
