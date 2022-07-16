using UnityEngine;

public class Tetronimo : MonoBehaviour
{
    public enum TetronimoShape
    {
        I,
        O,
        T,
        J,
        L,
        S,
        Z
    }

    [System.Serializable]
    public struct TetronimoData
    {
        public TetronimoShape shape;
        public Vector3Int[] positions;
    }
}
