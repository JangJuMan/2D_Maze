using UnityEngine;

public class AudioHandler : MonoBehaviour
{    
    public static AudioHandler Instance;
    public AudioClip clip;
    AudioSource audioSource;

    private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
