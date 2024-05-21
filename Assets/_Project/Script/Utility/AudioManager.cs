using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgmSource; // BGM用のAudioSource
    public float fadeTime = 2.0f; // フェードにかける時間（秒）

    // フェードアウトを開始する
    public void FadeOut()
    {
        StartCoroutine(FadeOutMusic());
    }

    // フェードインを開始する
    public void FadeIn()
    {
        StartCoroutine(FadeInMusic());
    }

    // 通常再生を開始する
    public void NormalIn()
    {
        bgmSource.Play();
    }

    // 通常再生を停止する
    public void NormalOut()
    {
        bgmSource.Stop();
    }

    // BGMをフェードアウトするコルーチン
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

    // BGMをフェードインするコルーチン
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
