using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary> エフェクトの表示、非表示、生成をするクラス </summary>
public class EffectContainer : MonoBehaviour
{
    private static EffectContainer instance;
    public static EffectContainer Instance
    {
        get
        {
            if(instance == null)
            {
                instance = Instantiate(GeneralSettings.Instance.Prehab.EffectContainer);
            }
            return instance;
        }
    }

    [SerializeField] private int m_MaxContain = 20;

    private List<IContainableEffect> m_Effects;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        m_Effects = new List<IContainableEffect>(m_MaxContain);
    }

    /// <summary> 指定のエフェクトを再生する </summary>
    public void PlayEffect<T>(T playEff, Vector3 vec) where T : MonoBehaviour, IContainableEffect
    {
        IContainableEffect eff = null;
        vec += GeneralSettings.Instance.Priorities.EffectLayer;

        if (m_Effects.Count != 0)
        {
            if(TryGetEffects<T>(out IContainableEffect[] effects))
            {
                var inactives = effects.Where(effects => !effects.IsActive).ToArray();
                if(inactives.Length != 0)
                {
                    eff = effects.Where(effects => !effects.IsActive).First();
                }
            }
        }

        if(eff == null)
        {
            eff = playEff.Create(vec, Quaternion.identity, transform);
            m_Effects.Add(eff);
        }

        eff.Play(vec);
    }

    /// <summary> シーン遷移時などでエフェクトを非表示にしたいときに使う </summary>
    public void StopAllEffect()
    {
        foreach (var effect in m_Effects)
        {
            effect.StopEffect();
        }
    }

    /// <summary> 使用するエフェクトを取得する </summary>
    /// <remarks>
    ///     <para>使用していない(非表示中の)エフェクトがあればそれを使う</para>
    ///     <para>なければ生成し、それを使う</para>
    /// </remarks>
    public T GetEffect<T>(T effect) where T : MonoBehaviour, IContainableEffect
    {
        T useEffect;
        if (TryGetEffects<T>(out IContainableEffect[] effects)) 
        {
            var sameEffects = effects.Where(effect => !effect.IsActive).ToArray();
            if(sameEffects.Length == 0)
            {
                useEffect = (T)effect.Create(Vector3.zero, Quaternion.identity, transform);
                m_Effects.Add(useEffect);
            }
            else
            {
                useEffect = (T)sameEffects[0];
                useEffect.Play(Vector2.zero);
            }
            
            return useEffect;
        }

        useEffect = (T)effect.Create(Vector3.zero, Quaternion.identity, transform);
        m_Effects.Add(useEffect);
        return useEffect;
    }

    /// <summary> 使用したいエフェクトがあるかどうかを取得する </summary>
    /// <remarks>
    ///     <para> 使いたいエフェクトがあれば effects から取得できる</para>
    /// </remarks>
    private bool TryGetEffects<T>(out IContainableEffect[] effects) where T : MonoBehaviour, IContainableEffect
    {
        effects = m_Effects.Where(effects => effects is T)
                           .ToArray();

        if(effects.Length == 0) { return false; }
        return true;
    }

}
