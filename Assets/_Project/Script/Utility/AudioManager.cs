using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgmSource; // BGM�p��AudioSource
    public float fadeTime = 2.0f; // �t�F�[�h�ɂ����鎞�ԁi�b�j

    // �t�F�[�h�A�E�g���J�n����
    public void FadeOut()
    {
        StartCoroutine(FadeOutMusic());
    }

    // �t�F�[�h�C�����J�n����
    public void FadeIn()
    {
        StartCoroutine(FadeInMusic());
    }

    // �ʏ�Đ����J�n����
    public void NormalIn()
    {
        bgmSource.Play();
    }

    // �ʏ�Đ����~����
    public void NormalOut()
    {
        bgmSource.Stop();
    }

    // BGM���t�F�[�h�A�E�g����R���[�`��
    private IEnumerator FadeOutMusic()
    {
        float startVolume = bgmSource.volume;

        while (bgmSource.volume > 0)
        {
            bgmSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        bgmSource.Stop();
        bgmSource.volume = startVolume;
    }

    // BGM���t�F�[�h�C������R���[�`��
    private IEnumerator FadeInMusic()
    {
        bgmSource.volume = 0;
        bgmSource.Play();

        while (bgmSource.volume < 1.0f)
        {
            bgmSource.volume += Time.deltaTime / fadeTime;
            yield return null;
        }
    }
}
