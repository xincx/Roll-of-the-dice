using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tomino
{
    public class MatchScoreCalculator
    {
        enum Correctness
        {
            Correct, Incorrect
        }

        enum ExistenceInTarget
        {
            Exists, Missing
        }

        class CorrectnessExistenceCounts
        {
            public int[,] counts;

            public CorrectnessExistenceCounts() : this(new int[2, 2])
            {
            }

            public CorrectnessExistenceCounts(int[,] counts)
            {
                this.counts = counts;
            }
        }

        static CorrectnessExistenceCounts ComputeCorrectnessExistenceCounts(Board board)
        {
            int[,] counts = new int[2, 2];
            List<Position> playerPositions = board.Blocks.ConvertAll(block => block.Position);
            List<Position> targetPositions = board.targetOutline.positions.ToList();
            for (int row = 0; row < board.width; row++)
            {
                for (int col = 0; col < board.height; col++)
                {
                    var position = new Position(row, col);
                    bool inPlayerPositions = playerPositions.Contains(position);
                    bool inTargetPositions = targetPositions.Contains(position);

                    var correctness = inPlayerPositions == inTargetPositions ? Correctness.Correct : Correctness.Incorrect;
                    var existence = inTargetPositions ? ExistenceInTarget.Exists : ExistenceInTarget.Missing;
                    counts[(int)correctness, (int)existence]++;
                }
            }

            string countMsg = "From counts: ";
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    countMsg += counts[i, j].ToString();
                    if (j == 0)
                    {
                        countMsg += ", ";
                    }
                }
                if (i == 0)
                {
                    countMsg += '\n';
                }
            }
            Debug.Log(countMsg);
            return new CorrectnessExistenceCounts(counts);
        }

        static public int ComputeMatchScore(Board board)
        {
            var counts = ComputeCorrectnessExistenceCounts(board).counts;
            var correct = counts[(int)Correctness.Correct, (int)ExistenceInTarget.Exists];
            var incorrect = counts[(int)Correctness.Incorrect, (int)ExistenceInTarget.Missing];
            int score = correct - incorrect;
            Debug.Log("Match score: " + score);
            return score;
        }
    }
}
