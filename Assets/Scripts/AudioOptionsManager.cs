using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class AudioOptionsManager : MonoBehaviour
{
    public static float musicVolume { get; private set; }
    public static float soundEffectsVolume { get; private set; }
    [SerializeField] private TextMeshProUGUI musicSliderText;
    [SerializeField] private TextMeshProUGUI soundEffectSliderText;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundeffSlider;

    DataHolder stor;

    private void Start()
    {
        OnMusicSliderValueChange(0.5f);
        OnSoundEffectSliderValueChange(0.5f);       
    }
    public void OnMusicSliderValueChange(float value)
    {
        musicVolume = value;
        musicSliderText.text = ((int)(value * 100)).ToString();
        musicSlider.value = value;
        AudioManager.instance.UpdateMixerVolume();
        PlayerPrefs.SetFloat("musicVolume", value);
    }

    public void OnSoundEffectSliderValueChange(float value)
    {
        soundEffectsVolume = value;
        soundEffectSliderText.text = ((int)(value * 100)).ToString();
        soundeffSlider.value = value;
        AudioManager.instance.UpdateMixerVolume();
        PlayerPrefs.SetFloat("soundEffectVolume", value);
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) return;
        stor = GameObject.Find("DataHolder").GetComponent<DataHolder>();
        musicSliderText = stor.mSliderText;
        soundEffectSliderText = stor.seSliderText;
        musicSlider = stor.mSlider;
        soundeffSlider = stor.seSlider;
        OnMusicSliderValueChange(PlayerPrefs.GetFloat("musicVolume"));
        OnSoundEffectSliderValueChange(PlayerPrefs.GetFloat("soundEffectVolume"));
        musicSlider.onValueChanged.AddListener(OnMusicSliderValueChange);
        soundeffSlider.onValueChanged.AddListener(OnSoundEffectSliderValueChange);
        AudioManager.instance.UpdateMixerVolume();
    }

}
