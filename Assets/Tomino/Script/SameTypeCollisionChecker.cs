using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Tomino
{
    public class SameTypeCollisionChecker
    {
        private static int[] dx = { 0, -1, 0, 1 };
        private static int[] dy = { -1, 0, 1, 0 };

        static bool InBounds(Board board, Position position)
        {
            return 0 <= position.Row &&
                   position.Row < board.height &&
                   0 <= position.Column &&
                   position.Column < board.width;
        }

        static List<Block> NeighborBlocks(Board board, Position position)
        {
            var neighborBlocks = new List<Block>();
            var row = position.Row;
            var col = position.Column;
            for (int direction = 0; direction < 4; direction++)
            {
                var newRow = row + dx[direction];
                var newCol = col + dy[direction];
                var candidateNeighbor = new Position(newRow, newCol);
                if (!InBounds(board, candidateNeighbor))
                {
                    continue;
                }
                Block blockForNeighbor = board.Blocks.Find(block => block.Position.Equals(candidateNeighbor));
                if (blockForNeighbor != null)
                {
                    neighborBlocks.Add(blockForNeighbor);
                }
            }
            return neighborBlocks;
        }

        static bool MatchingNumbers(Block a, Block b)
        {
            return a.BlockNum == b.BlockNum;
        }

        static bool MatchingColors(Block a, Block b)
        {
            return a.BlockNum == b.BlockNum || (a.BlockNum + 1) + (b.BlockNum + 1) == 7;
        }

        // Two blocks of the same piece cannot have the same color or number
        static public bool ValidBlocksForSamePiece(Block[] blocks)
        {
            for (int i = 0; i < blocks.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    bool haveMatch = MatchingColors(blocks[i], blocks[j]) || MatchingNumbers(blocks[i], blocks[j]);
                    bool areNeighbors = blocks[i].Position.IsNeighborOf(blocks[j].Position);
                    if (haveMatch && areNeighbors)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static public int ComputeBonusScoreDelta(Board board, Piece justFallenPiece)
        {
            int numMatchColor = 0;
            int numMatchNumber = 0;
            foreach (var block in justFallenPiece.blocks)
            {
                foreach (var neighborBlock in NeighborBlocks(board, block.Position))
                {
                    if (justFallenPiece.blocks.ToList().Exists(selfBlocks => selfBlocks.Position.Equals(neighborBlock.Position)))
                    {
                        continue;
                    }

                    if (MatchingNumbers(block, neighborBlock))
                    {
                        Debug.Log("Matching numbers " + block.Position.ToString() + " " + neighborBlock.Position.ToString());
                        numMatchNumber++;
                    }
                    if (MatchingColors(block, neighborBlock))
                    {
                        Debug.Log("Matching colors " + block.Position.ToString() + " " + neighborBlock.Position.ToString());
                        numMatchColor++;
                    }
                }
            }
            Debug.Log("After this piece fell, got " + numMatchColor + " matching colors and " + numMatchNumber + " matchingNumbers");
            return 50 * numMatchColor + 50 * numMatchNumber;
        }
    }
}
