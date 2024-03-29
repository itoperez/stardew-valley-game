using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] ScreenTint screenTint;
    [SerializeField] CameraConfiner cameraConfiner;
    string currentScene;
    AsyncOperation unload;
    AsyncOperation load;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void InitSwitchScene(string to, Vector3 targetPostion)
    {
        StartCoroutine(Transition(to, targetPostion));
    }

    IEnumerator Transition(string to, Vector3 targetPostion)
    {
        screenTint.Tint();

        yield return new WaitForSeconds(1f / screenTint.speed + 0.1f);

        SwitchScene(to, targetPostion);

        while (load != null & unload != null)
        {
            if (load.isDone) { load = null; }
            if (unload.isDone) { unload = null; }
            yield return new WaitForSeconds(0.1f);
        }               

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));

        cameraConfiner.UpdateBounds();
        screenTint.UnTint();        
    }

    public void SwitchScene(string to, Vector3 targetPostion)
    {
        load = SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        unload = SceneManager.UnloadSceneAsync(currentScene);
        currentScene = to;

        Transform playerTransform = GameManager.instance.player.transform;

        CinemachineBrain currentCamera = Camera.main.GetComponent<CinemachineBrain>();
        currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(
                    playerTransform,
                    targetPostion - playerTransform.position
                    );

        playerTransform.position = targetPostion;
    }
}
