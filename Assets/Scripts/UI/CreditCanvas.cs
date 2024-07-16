using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

public class CreditCanvas : MonoBehaviour
{
    [SerializeField] private VideoPlayer m_VideoPlayer;

    void Start()
    {
        m_VideoPlayer.SetLoopPointReached(() => ReturnTitle().FireAndForget());
    }

    private async Task ReturnTitle()
    {
        if (SceneLoadExtension.IsFading) { return; }

        SoundManager.Instance.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeIn);
        await SceneLoadExtension.StartFadeIn();

        await SceneLoadExtension.StartFadeWait(Config.SceneNames.Title);
        SoundManager.Instance.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeOut);

        await SceneLoadExtension.StartFadeOut();
    }
}

public static class VidoPlayerExtension
{
    public static void SetLoopPointReached(this VideoPlayer player, Action action)
    {
        player.loopPointReached += (player) =>
        {
            action();
        };
    }
}