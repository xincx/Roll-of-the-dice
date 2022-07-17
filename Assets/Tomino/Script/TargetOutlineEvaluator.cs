using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tomino
{
    public class Metrics
    {
        public int holes = 0;
        public int height = 0;

        public override string ToString()
        {
            return "holes = " + holes.ToString() + " height = " + height.ToString();
        }
    }

    public class TargetOutlineEvaluator
    {
        public static float Evaluate(int width, int height, TargetOutline targetOutline)
        {
            Metrics metrics = ComputeMetrics(width, height, targetOutline);
            int size = width * height;
            int score = size * metrics.height + metrics.holes; // weight height more than holes
            return score;
        }

        public static Metrics ComputeMetrics(int width, int height, TargetOutline targetOutline)
        {
            int[] highestInCol = new int[width];
            int[] countPerCol = new int[width];
            foreach (var position in targetOutline.positions)
            {
                int row = position.Row;
                int col = position.Column;
                highestInCol[col] = Math.Max(highestInCol[col], row);
                countPerCol[col]++;
            }

            var metrics = new Metrics();
            for (int col = 0; col < width; col++)
            {
                int holesInCol = highestInCol[col] + 1 - countPerCol[col];
                metrics.holes += holesInCol;
                metrics.height = Math.Max(metrics.height, highestInCol[col] + 1);
            }
            return metrics;
        }
    }

}