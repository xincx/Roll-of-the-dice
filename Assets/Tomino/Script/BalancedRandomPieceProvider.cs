using System;
using System.Collections.Generic;

namespace Tomino
{
    public class BalancedRandomPieceProvider : IPieceProvider
    {
        private Random random = new Random();
        private List<Piece> pool = new List<Piece>();
        private const int numDuplicates = 4;

        public Piece GetPiece() => GetPopulatedPool().TakeFirst();

        public Piece GetNextPiece() => GetPopulatedPool()[0];

        private List<Piece> GetPopulatedPool()
        {
            if (pool.Count == 0)
            {
                PopulatePool();
            }
            return pool;
        }

        private void PopulatePool()
        {
            for (int repeat = 0; repeat < numDuplicates; repeat++)
            {
                var generatedPieces = AvailablePieces.All();
                pool.AddRange(generatedPieces);
            }
            pool.Shuffle(random);
        }
    }
}