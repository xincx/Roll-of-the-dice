using UnityEngine;

namespace Tomino
{
    public class Simulation
    {
        Board board;
        int numIterations;
        bool gameOver;
        public Simulation(int width, int height)
        {
            Debug.Assert(height >= 2, "Need height to be at least 2 so we can trim it for simulation");
            board = new Board(width, height - 2, new BalancedRandomPieceProvider(), new EmptyTargetOutlineProvider());
        }

        public void AfterNextStep()
        {
            if (gameOver)
            {
                return;
            }

            board.AddPiece();
            if (board.HasCollisions())
            {
                gameOver = true;
                return;
            }

            PlacePiece();
            board.RemoveFullRows();
        }

        public Board GetBoard()
        {
            return board;
        }

        void PlacePiece()
        {
            for (int i = 0; i < board.width; i++)
            {
                board.MovePieceLeft();
            }
            int shiftRightAmount = Random.Range(0, board.width);
            for (int i = 0; i < shiftRightAmount; i++)
            {
                board.MovePieceRight();
            }
            int rotateRightAmount = Random.Range(0, 4);
            for (int i = 0; i < rotateRightAmount; i++)
            {
                board.RotatePieceRight();
            }
            board.FallPiece();
        }
    }
}