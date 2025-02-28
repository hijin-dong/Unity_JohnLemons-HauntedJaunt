using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 5f;

    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public CanvasGroup rankingBackgroundImageCanvasGroup;
    public static bool RankPresenter = false;
    public TMP_Text[] textElements;
    public AudioSource exitAudio;
    public AudioSource caughtAudio;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsPlayerAtExit)
        {
            Timer.StopTimer();
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (m_IsPlayerCaught)
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            m_IsPlayerAtExit = true;
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration && !GameEnding.RankPresenter)
        {
            if (doRestart)
                SceneManager.LoadScene(1);
            else
            {
                if (!RankPresenter)
                    Ranking.ShowRanking(rankingBackgroundImageCanvasGroup, textElements);
            }
        }
    }
}
