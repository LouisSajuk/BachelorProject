using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource sound;

    [SerializeField] private AudioClip hub_music;
    [SerializeField] private AudioClip lvl1_music;
    [SerializeField] private AudioClip lvl2_music;
    [SerializeField] private AudioClip lvl3_music;
    [SerializeField] private float music_volume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        if (next.name == "StartingScene")
        {
            PlayBGM(hub_music);
        }
    }

    private void PlayBGM(AudioClip bgm)
    {
        AudioSource audioSource = Instantiate(sound, Vector3.zero, Quaternion.identity);
        audioSource.clip = bgm;
        audioSource.volume = music_volume;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void startMusic()
    {
        if (SceneManager.GetActiveScene().name == "Level_1")
        {
            PlayBGM(lvl1_music);
        }
        else if (SceneManager.GetActiveScene().name == "Level_2")
        {
            PlayBGM(lvl2_music);
        }
        else if (SceneManager.GetActiveScene().name == "Level_3")
        {
            PlayBGM(lvl3_music);
        }
    }

    public void PlaySoundClip(AudioClip audioClip, Transform position, float volume)
    {
        AudioSource audioSource = Instantiate(sound, position.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float length = audioSource.clip.length;

        Destroy(audioSource.gameObject, length);
    }
}
