using System;
using System.Collections.Generic;

namespace Tomino
{
    public class SimulationTargetOutlineProvider : ITargetOutlineProvider
    {
        private int width;

        private int height;

        private int numIterations;

        public SimulationTargetOutlineProvider(int width, int height, int numIterations)
        {
            this.width = width;
            this.height = height;
            this.numIterations = numIterations;
        }

        public TargetOutline GetTargetOutline()
        {
            var simulation = new Simulation(width, height);
            for (int i = 0; i < numIterations; i++)
            {
                simulation.AfterNextStep();
            }
            Board finalBoard = simulation.GetBoard();
            Position[] finalPositions = finalBoard.Blocks.ConvertAll(block => block.Position).ToArray();
            return new TargetOutline(finalPositions);
        }
    }
}