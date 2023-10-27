using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButtonHandler : MonoBehaviour
{
    public AudioSource start;
    private void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnPlayButtonClick);
        start.Play();
        Debug.Log("played");
    }
    public void LoadMainGameScene()
    {
        start.Stop();
        SceneManager.LoadScene("SampleScene");
        Debug.Log("stopped");
        
    }
    private void OnPlayButtonClick()
    {
        LoadMainGameScene();
    }
}
