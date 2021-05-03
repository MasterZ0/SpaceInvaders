using Cinemachine;
using FMOD.Studio;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Options : MonoBehaviour
{
    [Header("Navigator Label")]
    [SerializeField] private string orthographic;
    [SerializeField] private string perspective;

    [Header(" - Config")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Navigator cameraNavigator;
    [SerializeField] private CinemachineVirtualCamera orthographicCM;
    [SerializeField] private CinemachineVirtualCamera perspectiveCM;
    [SerializeField] private TextMeshProUGUI masterText;

    [Header(" - Audio")]
    [EventRef]
    [SerializeField] private string sfxTest;

    private static Bus masterBus;
    private EventInstance sfxTestEvent;

    public static void Setup() {
        masterBus = RuntimeManager.GetBus("bus:/Master");

        float volume = PlayerPrefs.GetFloat(Constants.PlayerPrefs.MasterVolume, 1f);
        masterBus.setVolume(volume);
    }

    private void Awake() {
        masterBus.getVolume(out float volume);
        masterText.text = $"{volume * 100}%";
        masterSlider.value = volume;

        sfxTestEvent = RuntimeManager.CreateInstance(sfxTest);

        int cameraIndex = PlayerPrefs.GetInt(Constants.PlayerPrefs.Camera, 0);    // 0 = perspective, 1 = orthographic
        string[] cameraOptions = new string[2] { perspective, orthographic };
        cameraNavigator.Init(cameraOptions, cameraIndex);
    }
    public void OnSetMasterVolume(float volume) {
        volume = Mathf.Round(volume * 100) / 100;
        masterBus.setVolume(volume);
        masterText.text = $"{volume * 100}%";
        PlayerPrefs.SetFloat(Constants.PlayerPrefs.MasterVolume, volume);

        PLAYBACK_STATE pbState;
        sfxTestEvent.getPlaybackState(out pbState);
        if (pbState != PLAYBACK_STATE.PLAYING) {
            sfxTestEvent.start();
        }
    }
    public void OnSetCamera(int cameraType) {
        perspectiveCM.gameObject.SetActive(cameraType == 0);
        orthographicCM.gameObject.SetActive(cameraType == 1);
    }
}
