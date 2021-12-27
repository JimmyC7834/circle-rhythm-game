using UnityEngine;

[CreateAssetMenu(menuName = "Factory/DotNote")]
public class DotNoteFactory : Game.Framework.Factory.FactorySO<DotNote>
{
    [SerializeField] private GameObject prefab;
    
    public override DotNote Create()
    {
        return Instantiate(prefab).GetComponent<DotNote>();
    }
}