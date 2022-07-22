using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : SingletonDontDestroy<LevelManager>
{
    [SerializeField] private GameObject _loadingScreenCanvas;
    [SerializeField] private Image _progressBar;
    
    private void Awake() {
        Initialize();
    }

    public async Task LoadSceneAsync(string sceneName)
    {
        _progressBar.fillAmount = 0;
        
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        
        _loadingScreenCanvas.SetActive(true);

        do
        {
            await Task.Delay(100); // to make progress bar visible
            _progressBar.fillAmount = scene.progress;
        } while (scene.progress < 0.9f);
        
        await Task.Delay(1000);
        
        scene.allowSceneActivation = true;
        _loadingScreenCanvas.SetActive(false);
    }
}
