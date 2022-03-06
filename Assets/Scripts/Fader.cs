using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    private const string FADER_PATH = "Fader"; //Путь к префабу

    [SerializeField] private Animator animator;

    /*Синглтон*/
    private static Fader _instance;

    public static Fader instance 
    { 
        get

        {
            if (_instance == null) //Создаем объект из ресурсов
            {
                var prefab = Resources.Load<Fader>(FADER_PATH);
                _instance = Instantiate(prefab);
                DontDestroyOnLoad(_instance.gameObject); //Фейдер не уничтожится при смене сцены
            }

            return _instance;
        }
    }

    public bool isFading { get; private set; } //Вспомогательный флаг

    /*Колбеки для обратной связи*/
    private Action _fadedInCallback;
    private Action _fadedOutCallback;

    public void FadeIn(Action fadedinCallback)
    {
        if (isFading)
            return;

        isFading = true;
        _fadedInCallback = fadedinCallback;
        animator.SetBool("faded", true);
    }

    public void FadeOut(Action fadedOutCallback)
    {
        if (isFading)
            return;

        isFading = true;
        _fadedOutCallback = fadedOutCallback;
        animator.SetBool("faded", false);
    }

    private void Handle_FadeInAnimationOver()
    {
        _fadedInCallback?.Invoke();
        _fadedInCallback = null;
        isFading = false;
    }

    private void Handle_FadeOutAnimationOver()
    {
        _fadedOutCallback?.Invoke();
        _fadedOutCallback = null;
        isFading = false;
    }
}
