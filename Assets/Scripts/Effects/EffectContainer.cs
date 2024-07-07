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
    private Transform m_Transform;

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
        m_Transform = transform;
    }

    public void PlayEffect<T>(T effect, Vector3 vec) where T : IContainEffectBase, new()
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
            eff = effect.CreateEffect(vec, Quaternion.identity, m_Transform);
            m_Effects.Add(eff);
        }

        eff.PlayEffect(vec);
    }

    public void StopEffect<T>() where T : IContainEffectBase, new()
    {
        if (!TryGetEffects<T>(out IContainEffectBase[] effects)) { return; }

        effects[0].StopEffect();
    }

    private bool TryGetEffects<T>(out IContainEffectBase[] effects) where T : IContainEffectBase, new()
    {
        effects = m_Effects.Where(effects => effects is T)
                           .ToArray();

        if(effects.Length == 0) { return false; }
        return true;
    } 
}
