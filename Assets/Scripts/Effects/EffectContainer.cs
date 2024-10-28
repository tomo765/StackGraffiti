using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectContainer : MonoBehaviour  //ToDo : ManagerÇ…ñºëOïœÇ¶ÇΩï˚Ç™Ç¢Ç¢ÅH
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

    private List<IContainEffectBase> m_Effects;

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

        m_Effects = new List<IContainEffectBase>(m_MaxContain);
    }

    public void PlayEffect<T>(T playEff, Vector3 vec) where T : IContainEffectBase, new()
    {
        IContainEffectBase eff = null;
        vec += GeneralSettings.Instance.Priorities.EffectLayer;

        if (m_Effects.Count != 0)
        {
            if(TryGetEffects<T>(out IContainEffectBase[] effects))
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

    public void StopEffect<T>() where T : IContainEffectBase, new()
    {
        if (!TryGetEffects<T>(out IContainEffectBase[] effects)) { return; }

        effects[0].StopEffect();
    }
    public void StopAllEffect()
    {
        foreach (var effect in m_Effects)
        {
            effect.StopEffect();
        }
    }

    public T GetEffect<T>(T effect) where T : IContainEffectBase, new()
    {
        T newEffect;
        if (TryGetEffects<T>(out IContainEffectBase[] effects)) 
        {
            var sameEffects = effects.Where(effect => !effect.IsActive).ToArray();
            if(sameEffects.Length == 0)
            {
                newEffect = (T)effect.Create(Vector3.zero, Quaternion.identity, transform);
                m_Effects.Add(newEffect);
            }
            else
            {
                newEffect = (T)sameEffects[0];
                newEffect.Play(Vector2.zero);
            }
            
            return newEffect;
        }

        newEffect = (T)effect.Create(Vector3.zero, Quaternion.identity, transform);
        m_Effects.Add(newEffect);
        return newEffect;
    }
    private bool TryGetEffects<T>(out IContainEffectBase[] effects) where T : IContainEffectBase, new()
    {
        effects = m_Effects.Where(effects => effects is T)
                           .ToArray();

        if(effects.Length == 0) { return false; }
        return true;
    }

}
