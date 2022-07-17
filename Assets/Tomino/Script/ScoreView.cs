using UnityEngine;
using UnityEngine.UI;
using Tomino;

public class ScoreView : MonoBehaviour
{
    public Text scoreText;
    public Game game;

    void Update()
    {
        var padLength = Constant.ScoreFormat.Length;
        var padCharacter = Constant.ScoreFormat.PadCharacter;
        int scoreValue = game.matchScore.Value;
        bool isNegative = scoreValue < 0;
        if (isNegative)
        {
            scoreValue = -scoreValue;
            padLength--;
        }
        string negativePrefix = isNegative ? "-" : "";
        scoreText.text = negativePrefix + scoreValue.ToString().PadLeft(padLength, padCharacter);
    }
}
