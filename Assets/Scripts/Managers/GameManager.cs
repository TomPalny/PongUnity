using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Private members
    private Ball _ball;
    private int _leftPlayerScore;
    private int _rightPlayerScore;
    #endregion

    #region Editor exposed properties
    [SerializeField] private int _scoreToWin = 3;
    [SerializeField] private int _matchWaitSeconds = 3;
    #endregion

    // Singleton
    public static GameManager Instance { get; private set; }

    private void Start()
    {
        // Singleton init
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        // Find the ball
        _ball = FindObjectOfType<Ball>();
        if (_ball == null)
        {
            Debug.LogError("Ball not found!");
            Application.Quit();
            return;
        }

        // Basic init
        UI.Instance.UpdatePlayersScores(_leftPlayerScore, _rightPlayerScore);
        _ball.EnteredEndZone += BallOnEnteredEndZone;

        StartCoroutine(StartNewMatch());
    }

    /// <summary>
    /// Starts a new match
    /// </summary>
    private IEnumerator StartNewMatch()
    {
        _ball.Reset();

        // Sanity
        if (_matchWaitSeconds <= 0)
        {
            _matchWaitSeconds = 3;
        }

        // Wait before starting new match (Countdown)
        // TODO: Using this coroutine, show a countdown message for _matchWaitSeconds seconds (Use UI.Instance.ChangeMainMessage)
        while (_matchWaitSeconds > 0)
        {
            UI.Instance.ChangeMainMessage(_matchWaitSeconds.ToString("N0"));
            yield return new WaitForSeconds(1);
            _matchWaitSeconds--;
        }
        UI.Instance.ChangeMainMessage("");
        // Start
        _ball.GiveRandomVelocity();

        // TODO: Remove this yield break
        
    }

    /// <summary>
    /// Restarts the entire game
    /// </summary>
    private void StartNewGame()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Event listener to handle goal
    /// </summary>
    /// <param name="endZoneType">The goal side that the ball entered</param>
    private void BallOnEnteredEndZone(EndZone.EndZoneType endZoneType)
    {
        StartCoroutine(ShowGoalMessageAndHandleGoal(endZoneType == EndZone.EndZoneType.Left ? Player.PlayerType.Right : Player.PlayerType.Left));
    }

    /// <summary>
    /// Shows a message for the goal
    /// </summary>
    /// <param name="endZoneType">The end zone that was</param>
    /// <returns></returns>
    private IEnumerator ShowGoalMessageAndHandleGoal(Player.PlayerType scoringPlayer)
    {
        string playerScored;
        bool isGameOver = false;
        // TODO: Increase the correct player's score
        if (scoringPlayer.Equals(Player.PlayerType.Left))
        {
            _leftPlayerScore++;
            if (_leftPlayerScore >= 3) isGameOver = true;
            playerScored = "Left";
        }
        else
        {
            _rightPlayerScore++;
            if (_rightPlayerScore >= 3) isGameOver = true;
            playerScored = "Right";
        }
        // Update score
        UI.Instance.UpdatePlayersScores(_leftPlayerScore, _rightPlayerScore);


        // Show message / Handle game victory
        // TODO: Handle victory condition (_scoreToWin)
        
        if (isGameOver)
        {
            // TODO: Show message which player has won
            // TODO: Wait 3 seconds before starting a new game
            UI.Instance.ChangeMainMessage("The " + playerScored + " Player WON!");
            yield return new WaitForSeconds(3);
            StartNewGame();
        }
        else
        {
            // TODO: Show message which player has scored
            // TODO: Wait 3 seconds before starting a new match
            UI.Instance.ChangeMainMessage("The "+ playerScored +" Player scored!");
            yield return new WaitForSeconds(3);
            StartCoroutine(StartNewMatch());
        }

        // TODO: Remove this "Yield break" after you've implemented the above
        
    }
}