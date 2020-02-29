using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public GameObject ImageTeam1;
    public GameObject ImageTeam2;
    public AudioSource WinSound;
    public AudioClip WinSoundTeam1;
    public AudioClip WinSoundTeam2;
    // Use this for initialization
    void Start()
    {
        ImageTeam1.SetActive(false);
        ImageTeam2.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Win(Team team)
    {
        Debug.Log("Win!");

        MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();
        musicPlayer.StopPlayingMusic();

        //Win condition is reversed
        if (team != Team.One)
        {
            WinSound.clip = WinSoundTeam1;
            ImageTeam1.SetActive(true);
        }
        else
        {
            WinSound.clip = WinSoundTeam2;
            ImageTeam2.SetActive(true);
        }
        WinSound.Play();
        //SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
    }
}
