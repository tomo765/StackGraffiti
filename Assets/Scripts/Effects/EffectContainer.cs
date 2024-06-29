using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectContainer : MonoBehaviour  //ToDo : Manager�ɖ��O�ς������������H
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
        IContainEffectBase clickEff = null;
        vec += GeneralSettings.Instance.Priorities.EffectLayer;

        if (m_Effects.Count != 0)
        {
            if(TryGetEffects<T>(out IContainEffectBase[] effects))
            {
                clickEff = effects.First();
            }
        }

        if(clickEff == null)
        {
            clickEff = effect.CreateEffect(vec, Quaternion.identity, m_Transform);
            m_Effects.Add(clickEff);
        }

        clickEff.PlayEffect(vec);
    }

    private bool TryGetEffects<T>(out IContainEffectBase[] effects) where T : IContainEffectBase, new()
    {
        effects = m_Effects.Where(effects => !effects.IsActive)
                           .Where(inactives => inactives is T)
                           .ToArray();

        if(effects.Length == 0) { return false; }
        return true;
    } 
}
