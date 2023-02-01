using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GraphicController : MonoBehaviour
{
    [Header("Music Settings")]
    public AudioSource musicController = null;
    public Toggle musicToggle = null;
    public AudioSource soundEffectController = null;
    public Toggle soundEffectToggle = null;
    [Header("FielOfView Settings")]
    public Camera mainCamera = null;
    [Header("Shadow Settings")]
    public Toggle shadowToggle = null;
    [Header("V-Sync Settings")]
    public Toggle vsyncToggle = null;

    [Header("Default-Values")]
    public bool defaultMusic = true;
    public bool defaultSoundEffects = true;
    public FieldOfViewSetting defaultFieldOfView = FieldOfViewSetting.Normal;
    public bool defaultShadows = true;
    public bool defaultVSync = false;

    public enum FieldOfViewSetting
    {
        Close,
        Normal,
        Far
    };

    private void Start()
    {
        loadMusicSetting();
        loadSoundEffectSetting();
        loadFieldOfViewSetting();
        loadShadowSetting();
        loadVSyncSetting();
    }

    private bool convertIntToBool(int number)
    {
        bool convertedBool;
        switch (number)
        {
            case 0: convertedBool = false; break;
            case 1: convertedBool = true; break;
            default:
                throw new InvalidOperationException("Integer value is not valid");
        }
        return convertedBool;
    }

    //
    // Music
    //

    public void toggleMusic()
    {
        if (musicToggle.isOn)
        {
            saveMusicSetting(true);
            loadMusicSetting();
        }
        else
        {
            saveMusicSetting(false);
            loadMusicSetting();
        }
    }

    public void loadMusicSetting()
    {
        if (!PlayerPrefs.HasKey("SettingsMusic"))
        {
            saveMusicSetting(defaultMusic);
        }

        bool value = convertIntToBool(PlayerPrefs.GetInt("SettingsMusic"));
        musicController.enabled = value;
        if (value)
        {
            musicToggle.isOn = true;
        } else
        {
            musicToggle.isOn = false;
        }
    }

    public void saveMusicSetting(bool isMusicOn)
    {
        PlayerPrefs.SetInt("SettingsMusic", Convert.ToInt32(isMusicOn));
        Debug.Log("Toggled music: " + isMusicOn);
    }

    //
    // Sound Effects
    //

    public void toggleSoundEffect()
    {
        if (soundEffectToggle.isOn)
        {
            saveSoundEffectSetting(true);
            loadSoundEffectSetting();
        }
        else
        {
            saveSoundEffectSetting(false);
            loadSoundEffectSetting();
        }
    }

    public void loadSoundEffectSetting()
    {
        if (!PlayerPrefs.HasKey("SettingsSoundEffects"))
        {
            saveMusicSetting(defaultSoundEffects);
        }

        bool value = convertIntToBool(PlayerPrefs.GetInt("SettingsSoundEffects"));
        soundEffectController.enabled = value;
        if (value)
        {
            soundEffectToggle.isOn = true;
        }
        else
        {
            soundEffectToggle.isOn = false;
        }
    }

    public void saveSoundEffectSetting(bool isSoundEffectOn)
    {
        PlayerPrefs.SetInt("SettingsSoundEffects", Convert.ToInt32(isSoundEffectOn));
        Debug.Log("Toggled sound effects: " + isSoundEffectOn);
    }


    //
    // FieldOfView
    //

    public void switchFieldOfViewSetting(string memberOfFieldOfViewSetting)
    {
        FieldOfViewSetting fieldOfViewSetting;
        if (!Enum.TryParse<FieldOfViewSetting>(memberOfFieldOfViewSetting, true, out fieldOfViewSetting))
        {
            throw new NotImplementedException();
        }
        saveFieldOfViewSetting(fieldOfViewSetting);

        int fieldOfView = 0;
        switch (fieldOfViewSetting)
        {
            case FieldOfViewSetting.Close: fieldOfView = 50; break;
            case FieldOfViewSetting.Normal: fieldOfView = 75; break;
            case FieldOfViewSetting.Far: fieldOfView = 90; break;
        }
        mainCamera.fieldOfView = fieldOfView;
    }

    public void loadFieldOfViewSetting()
    {
        if (!PlayerPrefs.HasKey("SettingsFieldOfView"))
        {
            saveFieldOfViewSetting(defaultFieldOfView);
        }

        switchFieldOfViewSetting(PlayerPrefs.GetString("SettingsFieldOfView"));
    }

    public void saveFieldOfViewSetting(FieldOfViewSetting fieldOfViewSetting)
    {
        PlayerPrefs.SetString("SettingsFieldOfView", fieldOfViewSetting.ToString());
        Debug.Log("Changed fieldOfView to: " + fieldOfViewSetting.ToString());
    }


    //
    // Shadow
    //

    public void toggleShadow()
    {
        if (shadowToggle.isOn)
        {
            saveShadowSetting(true);
            loadShadowSetting();
        }
        else
        {
            saveShadowSetting(false);
            loadShadowSetting();
        }
    }
    public void loadShadowSetting()
    {
        if (!PlayerPrefs.HasKey("SettingsShadow"))
        {
            saveMusicSetting(defaultShadows);
        }

        bool value = convertIntToBool(PlayerPrefs.GetInt("SettingsShadow"));
        if (value)
        {
            shadowToggle.isOn = true;
        }
        else
        {
            shadowToggle.isOn = false;
        }
    }

    public void saveShadowSetting(bool isShadowOn)
    {
        PlayerPrefs.SetInt("SettingsShadow", Convert.ToInt32(isShadowOn));
        Debug.Log("Toggled shadows: " + isShadowOn);
    }


    //
    // V-Sync
    //

    public void toggleVSync()
    {
        if (vsyncToggle.isOn)
        {
            saveVSyncSetting(true);
            loadVSyncSetting();
        }
        else
        {
            saveVSyncSetting(false);
            loadVSyncSetting();
        }
    }

    public void loadVSyncSetting()
    {
        if (!PlayerPrefs.HasKey("SettingsVSync"))
        {
            saveMusicSetting(defaultVSync);
        }

        bool value = convertIntToBool(PlayerPrefs.GetInt("SettingsVSync"));
        if (value)
        {
            QualitySettings.vSyncCount = 1;
            vsyncToggle.isOn = true;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            vsyncToggle.isOn = false;
        }
    }

    public void saveVSyncSetting(bool isVSyncOn)
    {
        PlayerPrefs.SetInt("SettingsVSync", Convert.ToInt32(isVSyncOn));
        Debug.Log("Toggled vsync: " + isVSyncOn);
    }
}