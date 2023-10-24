using UnityEngine;
using UnityEngine.Tilemaps;

namespace Buildings
{
    [CreateAssetMenu(menuName = "Buildings/New Buildable Structure", fileName = "New Structure")]
    public class BuildableStructure : MonoBehaviour
    {
        [SerializeField]
        public string Name { get; private set; }
        
        [SerializeField]
        public TileBase tile { get; private set; }
    }
}
