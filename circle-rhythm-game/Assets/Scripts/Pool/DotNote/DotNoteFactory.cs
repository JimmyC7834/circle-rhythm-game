using UnityEngine;

[CreateAssetMenu(menuName = "Factory/DotNote")]
public class DotNoteFactory : Game.Framework.Factory.FactorySO<DotNote>
{
    [SerializeField] private GameObject dotNote;
    
    public override DotNote Create()
    {
        return (Instantiate(dotNote) as GameObject).GetComponent<DotNote>();
    }
}