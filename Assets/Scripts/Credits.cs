using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private VideoPlayer player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<VideoPlayer>();

        // vp.playbackSpeed = 7.0f;

        player.loopPointReached += EndReached;
    }

    void Update()
    {
        if (Input.anyKey)
        {
            player.playbackSpeed = 8.0f;
        } else
        {
            player.playbackSpeed = 1.0f;
        }
    }

    void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene("MainMenu");
    }
}
