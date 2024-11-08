using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // Check if the current scene is "Main" and do something if it is
        if (IsCurrentSceneMain())
        {
            Debug.Log("The current scene is Main!");
        }

        // Log a warning if Animator is not assigned
        if (animator == null)
        {
            Debug.LogWarning("Animator is not assigned in LevelManager.");
        }
    }

    // Method to start loading a scene with a transition effect
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    // Coroutine for loading a scene asynchronously
    private IEnumerator LoadSceneAsync(string Main)
    {
        // Check if animator is assigned before setting the trigger
        if (animator != null)
        {
            animator.SetTrigger("StartTransitions");

            // Wait for the length of the current animation to finish
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }
        else
        {
            Debug.LogWarning("Animator is null, skipping end transition.");
            yield return null;
        }

        yield return new WaitForSeconds(2);

        // Load the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Main);

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Player.Instance.transform.position = new(0, -4.5f);

        // Check if animator is assigned before setting the start transition trigger
        if (animator != null)
        {
            
            animator.SetTrigger("EndTransitions");
        }
        else
        {
            Debug.LogWarning("Animator is null, skipping start transition.");
        }
    }

    // Method to check if the current scene is "Main"
    public bool IsCurrentSceneMain()
    {
        return SceneManager.GetActiveScene().name == "Main";
    }
}
