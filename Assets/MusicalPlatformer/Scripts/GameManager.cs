using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioSource instrumentals;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] float restartDelay = 1.5f;
    [SerializeField] float slowness = 10f;
    [SerializeField] float pitchSlowness = 0.2f;
    private bool gameHasEnded = false;

    /*
    public void EndGame()
    {
        if (!gameHasEnded)
        {
            playerMovement.enabled = false;
            gameHasEnded = true;
            Debug.Log("EndGame() RESTART GAME");
            Invoke("Restart", restartDelay);
        }        
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    */

    //public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }


    public void EndGame()
    {
        StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        //playerMovement.enabled = false;
        Time.timeScale = 1f / slowness;
        Time.fixedDeltaTime = Time.fixedDeltaTime / slowness; //not sure this line is necessary idk if i use fixedDeltaTime anywhere.
        instrumentals.pitch = pitchSlowness;
        //StartCoroutine(AudioFadeOut.FadeOut(sound_open, 0.1f));
        StartCoroutine(FadeOut(instrumentals, restartDelay));

        yield return new WaitForSeconds(restartDelay / slowness);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.fixedDeltaTime * slowness;
        instrumentals.pitch = 1f;
        //playerMovement.enabled = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
