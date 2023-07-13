using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

namespace UI.Menu
{
    public class UISettingsManager : MonoBehaviour
    {
        [Header("VIDEO SETTINGS")]
        public GameObject fullscreentext;

        public GameObject shadowofftextLINE;
        public GameObject shadowlowtextLINE;
        public GameObject shadowhightextLINE;

        public GameObject vsynctext;

        public GameObject texturelowtextLINE;
        public GameObject texturemedtextLINE;
        public GameObject texturehightextLINE;

        public GameObject musicSlider;


        public void Start()
        {

            //check slider
            musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");

            // check full screen
            if (Screen.fullScreen == true)
            {
                fullscreentext.GetComponent<TMP_Text>().text = "On";
            }
            else if (Screen.fullScreen == false)
            {
                fullscreentext.GetComponent<TMP_Text>().text = "Off";
            }

            // check shadow distance/enabled
            if (PlayerPrefs.GetInt("Shadows") == 0)
            {
                QualitySettings.shadowCascades = 0;
                QualitySettings.shadowDistance = 0;
                shadowofftextLINE.gameObject.SetActive(true);
                shadowlowtextLINE.gameObject.SetActive(false);
                shadowhightextLINE.gameObject.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("Shadows") == 1)
            {
                QualitySettings.shadowCascades = 2;
                QualitySettings.shadowDistance = 75;
                shadowofftextLINE.gameObject.SetActive(false);
                shadowlowtextLINE.gameObject.SetActive(true);
                shadowhightextLINE.gameObject.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("Shadows") == 2)
            {
                QualitySettings.shadowCascades = 4;
                QualitySettings.shadowDistance = 500;
                shadowofftextLINE.gameObject.SetActive(false);
                shadowlowtextLINE.gameObject.SetActive(false);
                shadowhightextLINE.gameObject.SetActive(true);
            }


            //vsync
            if (QualitySettings.vSyncCount == 0)
            {
                vsynctext.GetComponent<TMP_Text>().text = "Off";
            }
            else if (QualitySettings.vSyncCount == 1)
            {
                vsynctext.GetComponent<TMP_Text>().text = "On";
            }



            // check texture quality
            if (PlayerPrefs.GetInt("Textures") == 0)
            {
                QualitySettings.globalTextureMipmapLimit = 2;
                texturelowtextLINE.gameObject.SetActive(true);
                texturemedtextLINE.gameObject.SetActive(false);
                texturehightextLINE.gameObject.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("Textures") == 1)
            {
                QualitySettings.globalTextureMipmapLimit = 1;
                texturelowtextLINE.gameObject.SetActive(false);
                texturemedtextLINE.gameObject.SetActive(true);
                texturehightextLINE.gameObject.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("Textures") == 2)
            {
                QualitySettings.globalTextureMipmapLimit = 0;
                texturelowtextLINE.gameObject.SetActive(false);
                texturemedtextLINE.gameObject.SetActive(false);
                texturehightextLINE.gameObject.SetActive(true);
            }
        }

        public void Update()
        {
            //sliderValue = musicSlider.GetComponent<Slider>().value;

        }

        public void FullScreen()
        {
            Screen.fullScreen = !Screen.fullScreen;

            if (Screen.fullScreen == true)
            {
                fullscreentext.GetComponent<TMP_Text>().text = "On";
            }
            else if (Screen.fullScreen == false)
            {
                fullscreentext.GetComponent<TMP_Text>().text = "Off";
            }
        }

        public void MusicSlider()
        {
            //PlayerPrefs.SetFloat("MusicVolume", sliderValue);
            PlayerPrefs.SetFloat("MusicVolume", musicSlider.GetComponent<Slider>().value);
        }


        public void ShadowsOff()
        {
            PlayerPrefs.SetInt("Shadows", 0);
            QualitySettings.shadowCascades = 0;
            QualitySettings.shadowDistance = 0;
            shadowofftextLINE.gameObject.SetActive(true);
            shadowlowtextLINE.gameObject.SetActive(false);
            shadowhightextLINE.gameObject.SetActive(false);
        }

        public void ShadowsLow()
        {
            PlayerPrefs.SetInt("Shadows", 1);
            QualitySettings.shadowCascades = 2;
            QualitySettings.shadowDistance = 75;
            shadowofftextLINE.gameObject.SetActive(false);
            shadowlowtextLINE.gameObject.SetActive(true);
            shadowhightextLINE.gameObject.SetActive(false);
        }

        public void ShadowsHigh()
        {
            PlayerPrefs.SetInt("Shadows", 2);
            QualitySettings.shadowCascades = 4;
            QualitySettings.shadowDistance = 500;
            shadowofftextLINE.gameObject.SetActive(false);
            shadowlowtextLINE.gameObject.SetActive(false);
            shadowhightextLINE.gameObject.SetActive(true);
        }


        public void vsync()
        {
            if (QualitySettings.vSyncCount == 0)
            {
                QualitySettings.vSyncCount = 1;
                vsynctext.GetComponent<TMP_Text>().text = "On";
            }
            else if (QualitySettings.vSyncCount == 1)
            {
                QualitySettings.vSyncCount = 0;
                vsynctext.GetComponent<TMP_Text>().text = "Off";
            }
        }

        public void TexturesLow()
        {
            PlayerPrefs.SetInt("Textures", 0);
            QualitySettings.globalTextureMipmapLimit = 2;
            texturelowtextLINE.gameObject.SetActive(true);
            texturemedtextLINE.gameObject.SetActive(false);
            texturehightextLINE.gameObject.SetActive(false);
        }

        public void TexturesMed()
        {
            PlayerPrefs.SetInt("Textures", 1);
            QualitySettings.globalTextureMipmapLimit = 1;
            texturelowtextLINE.gameObject.SetActive(false);
            texturemedtextLINE.gameObject.SetActive(true);
            texturehightextLINE.gameObject.SetActive(false);
        }

        public void TexturesHigh()
        {
            PlayerPrefs.SetInt("Textures", 2);
            QualitySettings.globalTextureMipmapLimit = 0;
            texturelowtextLINE.gameObject.SetActive(false);
            texturemedtextLINE.gameObject.SetActive(false);
            texturehightextLINE.gameObject.SetActive(true);
        }
    }
}