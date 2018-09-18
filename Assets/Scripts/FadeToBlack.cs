using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour {

    public Image Fade;

    private static float _fadeStart = 0;
    private static float _fadeTime = 1;

    private static bool _shouldFade = true;
    private static bool _isFaded = false;

    private static int _levelToLoad = -1;

	// Use this for initialization
	void Start () {
        _fadeTime = 1;
        _fadeStart = Time.time;
        _isFaded = false;
        _shouldFade = true;
        _levelToLoad = -1;
    }
	
	// Update is called once per frame
	void Update () {
        if (_shouldFade)
        {
            if (_isFaded)
            {
                Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, (Time.time - _fadeStart) / _fadeTime);
                Debug.Log(Time.time - _fadeStart);
                if (Time.time - _fadeStart >= _fadeTime)
                {

                    _shouldFade = false;
                    _isFaded = false;
                    Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, 1);
                    if (_levelToLoad >= 0)
                    {
                        Debug.Log(_levelToLoad);
                        UnityEngine.SceneManagement.SceneManager.LoadScene(_levelToLoad);
                        //Application.LoadLevel(_levelToLoad);
                    }
                }
            }
            else
            {
                Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, 1 - ((Time.time - _fadeStart) / _fadeTime));
                if (Time.time - _fadeStart >= _fadeTime)
                {
                    _shouldFade = false;
                    _isFaded = true;
                    Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, 0);
                }
            }
        }
    }

    public static void DoFade(float pTime, bool pShouldEnable, int pLevelToLoad = -1)
    {
        _levelToLoad = pLevelToLoad;
        _isFaded = pShouldEnable;
        _shouldFade = true;
        _fadeTime = pTime;
        _fadeStart = Time.time;
    }
}
