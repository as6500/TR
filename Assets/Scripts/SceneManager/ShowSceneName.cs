using UnityEngine;

public class ShowSceneName : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void Awake()
    {
        FadeInFadeOut fadeInFadeOut = FindObjectOfType<FadeInFadeOut>();
        if (fadeInFadeOut != null)
        {
            fadeInFadeOut.SetText(sceneName);
        }
    }
}
