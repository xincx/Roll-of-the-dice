using UnityEngine;

namespace Tomino
{
    public class TargetOutline
    {
        public readonly Position[] positions;

        public TargetOutline()
        {
        }

        public TargetOutline(Position[] positions)
        {
            this.positions = positions;
        }
    }
}
