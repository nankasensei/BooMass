using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextManeger : MonoBehaviour
{
    public bool NowGameFlag;
    public int SoundFlag;
    //public bool StartSoundFlag;

    public int BombNum;

    public Text SystemText;
    public Text BombNumText;
	public Text ButtonGuideText;

    public AudioClip ReadySound;
    public AudioClip StartSound;
    public AudioClip GameOverSound;
    public AudioClip ClearSound;

	public AudioSource audio;
	public AudioSource Button;
	public float delay = 0.5f;

	Timer m_timer = new Timer();
	bool m_isMoveScene = false;
	bool m_isSceneNext = false;

    private Timer timer = new Timer();

	public AnimationCurve popCurve;

	private Vector3 originalScale;
	// Start is called before the first frame update
	void Start()
    {
        audio = GetComponent<AudioSource>();
        NowGameFlag = false;
        SoundFlag = 0;
        //StartSoundFlag = false;

        BombNumText.enabled = false;
        timer.Start();

		originalScale = SystemText.transform.localScale;
		ButtonGuideText.enabled = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (NowGameFlag)
        {
			if (GameSceneManager.instance.isGameOver == true)
			{
				BombNumText.enabled = false;
				SystemText.text = "GameOver";
				SystemText.enabled = true;
				ButtonGuideText.enabled = true;

				if (SoundFlag == 4)
				{
					audio.PlayOneShot(GameOverSound);
					SoundFlag++;
				}

				if (!m_isMoveScene)
				{
					if (Input.GetKeyDown(KeyCode.Return))
					{
						m_isMoveScene = true;
						m_isSceneNext = true;
						m_timer.Start();
						Button.PlayOneShot(Button.clip);
					}
					else if (Input.GetKeyDown(KeyCode.Escape))
					{
						m_isMoveScene = true;
						m_isSceneNext = false;
						m_timer.Start();
						Button.PlayOneShot(Button.clip);
					}
				}
				else if (m_timer.elapasedTime > delay)
				{
					if (m_isSceneNext)
						GameSceneManager.instance.MoveNextScene();
					else
						GameSceneManager.instance.ReloadScene();
				}
			}
			else if (GameSceneManager.instance.isGameClear == true)
			{
				BombNumText.enabled = false;
				SystemText.text = "Clear!";
				SystemText.enabled = true;
				ButtonGuideText.enabled = true;
				if (SoundFlag == 4)
				{
					audio.PlayOneShot(ClearSound);
					SoundFlag++;
				}

				if (!m_isMoveScene)
				{
					if (Input.GetKeyDown(KeyCode.Return))
					{
						GameSceneManager.instance.MoveNextScene();
						m_isMoveScene = true;
						Button.PlayOneShot(Button.clip);
					}
					else if (Input.GetKeyDown(KeyCode.Escape))
					{
						GameSceneManager.instance.MoveNextScene();
						m_isMoveScene = true;
						Button.PlayOneShot(Button.clip);
					}
				}
			}
			else
			{
				SystemText.enabled = false;
				BombNumText.text = "Required Bombs : " + GameSceneManager.instance.numAllBomb
					+ "\nHave Bomb : " + GameSceneManager.instance.numPlayerHaveBomb;
				BombNumText.enabled = true;
			}
        }
        else {
            if (timer.elapasedTime < 1)
            {
                SystemText.text = "3";
				changeScale();
				if (SoundFlag == 0) {
                    audio.PlayOneShot(ReadySound);
                    SoundFlag++;
                }
            }
            else if (timer.elapasedTime < 2)
            {
                SystemText.text = "2";
				changeScale();
				if (SoundFlag == 1) {
                    audio.PlayOneShot(ReadySound);
                    SoundFlag++;
                }

            }
            else if (timer.elapasedTime < 3)
            {
                SystemText.text = "1";
				changeScale();
				if (SoundFlag == 2) {
                    audio.PlayOneShot(ReadySound);
                    SoundFlag++;
                }
            }
            else if (timer.elapasedTime < 4)
            {
                SystemText.text = "Start!";
				changeScale();
				if (SoundFlag == 3) {
                    audio.PlayOneShot(StartSound);
                    SoundFlag++;
                }
            }
            else {
                NowGameFlag = true;
				GameSceneManager.instance.GameStart();
            }


        }
    }

	private void changeScale()
	{
		var fracTime = timer.elapasedTime - Mathf.Floor(timer.elapasedTime);
		SystemText.transform.localScale = originalScale * popCurve.Evaluate(fracTime);
	}

}
