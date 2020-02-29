using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public Image ImageTeam1;
    public Image ImageTeam2;
    public AudioSource WinSound;
    public AudioClip WinSoundTeam1;
    public AudioClip WinSoundTeam2;
    // Use this for initialization
    void Start()
    {

    }

    public void Win(Team team)
    {
        if (team == Team.One)
        {

        }
    }
 
}
