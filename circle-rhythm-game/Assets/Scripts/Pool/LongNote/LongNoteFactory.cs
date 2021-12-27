using UnityEngine;

[CreateAssetMenu(menuName = "Factory/LongNote")]
public class LongNoteFactory : Game.Framework.Factory.FactorySO<LongNote>
{
    [SerializeField] private GameObject prefab;
    
    public override LongNote Create()
    {
        return Instantiate(prefab).GetComponent<LongNote>();
    }
}