using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tomino
{
    public class SimulationTargetOutlineProvider : ITargetOutlineProvider
    {
        private int width;

        private int height;

        private int numIterationsPerSample;

        private int numSamples;

        public SimulationTargetOutlineProvider(int width, int height, int numIterationsPerSample, int numSamples = 1)
        {
            this.width = width;
            this.height = height;
            this.numIterationsPerSample = numIterationsPerSample;
            this.numSamples = numSamples;
        }

        public TargetOutline GetTargetOutline()
        {
            var candidateOutlines = new List<TargetOutline>();
            for (int i = 0; i < numSamples; i++)
            {
                candidateOutlines.Add(GetPotentialTargetOutline());
            }
            var candidateScores = candidateOutlines.ConvertAll(outline => TargetOutlineEvaluator.Evaluate(width, height, outline));

            int bestCandidate = 0;
            for (int i = 0; i < numSamples; i++)
            {
                if (candidateScores[i] < candidateScores[bestCandidate])
                {
                    bestCandidate = i;
                }
            }
            Metrics metrics = TargetOutlineEvaluator.ComputeMetrics(width, height, candidateOutlines[bestCandidate]);
            Debug.Log("Metrics: " + metrics.ToString() + " => " + candidateScores[bestCandidate]);
            return candidateOutlines[bestCandidate];
        }

        TargetOutline GetPotentialTargetOutline()
        {
            var simulation = new Simulation(width, height);
            for (int i = 0; i < numIterationsPerSample; i++)
            {
                simulation.AfterNextStep();
            }
            Board finalBoard = simulation.GetBoard();
            Position[] finalPositions = finalBoard.Blocks.ConvertAll(block => block.Position).ToArray();
            return new TargetOutline(finalPositions);
        }
    }
}