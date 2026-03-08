using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    [SerializeField] private AudioSource SFXObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySFX(AudioClip audioClip, Transform spawnTransform, float volume = 1.0f)
    {
        AudioSource src = Instantiate(SFXObject, spawnTransform.position, Quaternion.identity);
        src.clip = audioClip;
        src.volume = volume;
        src.Play();
        float len = src.clip.length;
        Destroy(src.gameObject, len);
    }
}
