using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public void SwitchScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
}