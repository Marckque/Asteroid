using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private Text m_ScoreText;

    public int Score { get; set; }

    public void UpdateScoreUI()
    {
        m_ScoreText.text = Score.ToString();
    }
}