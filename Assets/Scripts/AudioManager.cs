using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip[] zombieSounds;
    [Range(0, 1)]
    public float zombieSoundVolume;
    [Space]
    public AudioClip[] shootSound;
    [Range(0, 1)]
    public float shootSoundVolume;
    [Space]
    public AudioClip[] eating;
    [Range(0, 1)]
    public float eatingVolume;
    [Space]
    public AudioClip[] hitSound;
    [Range(0, 1)]
    public float hitSoundVolume;
    [Space]
    public AudioClip[] explousion;
    [Range(0, 1)]
    public float explousionVolume;
    [Space]
    public AudioClip[] headshot;
    [Range(0, 1)]
    public float headshotVolume;
    [Space]
    public AudioClip[] reload;
    [Range(0, 1)]
    public float reloadVolume;
    [Space]
    public AudioClip mainTheme;
    [Range(0, 1)]
    public float mainThemeVolume;
    [Space]

    public AudioSource[] source;


    private void Awake()
    {
        Application.targetFrameRate = 300;
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(this);
        }

       AudioSource srs = source[1];
        srs.volume = mainThemeVolume;
        srs.loop = true;
        srs.clip = mainTheme;
        srs.Play();


    }
    public void PlayZombieSound()
    {
        AudioSource srs = source[0];
        AudioClip clip = srs.clip = zombieSounds[Random.Range(0, zombieSounds.Length - 1)];
        StartCoroutine(PlaySound(srs, source[1], clip, zombieSoundVolume));

    }
    public void PlayShootSound()
    {
        AudioSource srs = source[0];

        AudioClip clip = srs.clip = shootSound[Random.Range(0, shootSound.Length - 1)];

        StartCoroutine(PlaySound(srs, source[1], clip,shootSoundVolume));

    }
    public void PlayEatSound()
    {
        AudioSource srs = source[0];

        AudioClip clip = srs.clip = eating[Random.Range(0, eating.Length - 1)];
        StartCoroutine(PlaySound(srs, source[1], clip,eatingVolume));

    }
    public void PlayHitSound()
    {
        AudioSource srs = source[0];

        AudioClip clip = srs.clip = hitSound[Random.Range(0, hitSound.Length - 1)];
        StartCoroutine(PlaySound(srs, source[1], clip,hitSoundVolume));

    }
    public void PlayHeadshotSound()
    {
        AudioSource srs = source[0];

        AudioClip clip = srs.clip = headshot[Random.Range(0, headshot.Length - 1)];
        StartCoroutine(PlaySound(srs, source[1], clip,headshotVolume));

    }
    public void PlayExplousionSound()
    {
        AudioSource srs = source[0];

        AudioClip clip = srs.clip = explousion[Random.Range(0, explousion.Length - 1)];
        StartCoroutine(PlaySound(srs, source[1], clip,explousionVolume));
    }
    public void PlayReloadSound()
    {
        AudioSource srs = source[0];

        AudioClip clip = srs.clip = reload[Random.Range(0, reload.Length - 1)];
        StartCoroutine(PlaySound(srs, source[1], clip, reloadVolume));
    }

    private IEnumerator PlaySound(AudioSource srs, AudioSource main, AudioClip clip,float volume)
    {
        float time = clip.length;
        srs.clip = clip;
        main.volume -= main.volume * 0.5f;

        srs.PlayOneShot(clip,volume);
        yield return new WaitForSeconds(time);
        main.volume = mainThemeVolume;

    }
    private float deltaTime = 0.0f;

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperCenter;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = Color.yellow;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}
