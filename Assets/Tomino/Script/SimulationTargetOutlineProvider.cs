using System;
using System.Collections.Generic;

namespace Tomino
{
    public class SimulationTargetOutlineProvider : ITargetOutlineProvider
    {
        private Random random = new Random();

        private Simulation simulation;

        private int numIterations;

        public SimulationTargetOutlineProvider(int width, int height, int numIterations)
        {
            simulation = new Simulation(width, height);
            this.numIterations = numIterations;
        }

        public TargetOutline GetTargetOutline()
        {
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