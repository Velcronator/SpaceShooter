using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasFader : MonoBehaviour
{
    public static CanvasFader instance;

    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Image _loadingBar;
    [SerializeField] private float _changeValue = 0.04f;
    [SerializeField] private float _waitTime = 0.01f;
    [SerializeField] private bool _fadeStarted = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (_canvasGroup == null)
        {
            _canvasGroup = GetComponent<CanvasGroup>(); // Check and assign CanvasGroup if null
        }
        StartCoroutine(FadeIn());
    }

    public void FadeOutToLevel(object level)
    {
        StartCoroutine(FadeOut(level));
    }

    IEnumerator FadeIn()
    {
        _loadingScreen.SetActive(false); // Hide loading screen when fading in
        _fadeStarted = false;
        while (_canvasGroup.alpha > 0)
        {
            if (_fadeStarted)
            {
                yield break;
            }
            _canvasGroup.alpha -= _changeValue;
            yield return new WaitForSeconds(_waitTime);
        }
    }

    IEnumerator FadeOut(object level)
    {
        if (_fadeStarted)
        {
            yield break;
        }
        _fadeStarted = true;

        // Fade out canvas
        while (_canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha += _changeValue;
            yield return new WaitForSeconds(_waitTime);
        }

        // Start loading the scene asynchronously
        AsyncOperation asyncOperation = null;
        if (level is string levelName)
        {
            asyncOperation = SceneManager.LoadSceneAsync(levelName);
        }
        else if (level is int levelIndex)
        {
            asyncOperation = SceneManager.LoadSceneAsync(levelIndex);
        }

        _loadingScreen.SetActive(true); // Show loading screen

        // Update loading bar while the scene is loading
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            _loadingBar.fillAmount = progress;

            // Allow scene activation once the loading is complete
            if (asyncOperation.progress >= 0.9f)
            {
                _loadingBar.fillAmount = 1f; // Ensure loading bar is full
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

        // Small delay before fading into the new scene
        yield return new WaitForSeconds(_waitTime);
        StartCoroutine(FadeIn());
    }
}
