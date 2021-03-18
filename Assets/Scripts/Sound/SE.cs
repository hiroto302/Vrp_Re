using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 各オブジェクトのSEを制御するスクリプト

public class SE : MonoBehaviour
{
    [SerializeField]
    public AudioSource audioSource = null;
    [SerializeField]
    public AudioClip[] se = null;
    void Reset()
    {
        audioSource = GetComponent<AudioSource>();
        DefaultSet();
    }
    // AudioSourceのデフォルト設定
    void DefaultSet()
    {
        audioSource.priority = 128;
        audioSource.volume = 0.2f;
        audioSource.loop = false;
        audioSource.spatialBlend = 1;
        audioSource.playOnAwake = false;
    }
    // SE再生メソッド
    public void PlaySE(int n)
    {
        audioSource.PlayOneShot(se[n], audioSource.volume);
    }
    public void PlaySE(int n, float volume)
    {
        audioSource.PlayOneShot(se[n], volume);
    }
    public void PlaySE(int n, float volume, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(se[n], position, volume);
    }
}
