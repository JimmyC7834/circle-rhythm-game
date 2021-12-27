using UnityEngine;

[CreateAssetMenu(menuName = "Factory/CircleNote")]
public class CircleNoteFactory : Game.Framework.Factory.FactorySO<CircleNote>
{
    [SerializeField] private GameObject prefab;
    
    public override CircleNote Create()
    {
        return Instantiate(prefab).GetComponent<CircleNote>();
    }
}