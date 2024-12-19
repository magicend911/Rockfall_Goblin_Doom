using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Rock _rock;
    [SerializeField] private TMP_Text _score;

    private void OnEnable()
    {
        _rock.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _rock.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _score.text = score.ToString();
    }
}
