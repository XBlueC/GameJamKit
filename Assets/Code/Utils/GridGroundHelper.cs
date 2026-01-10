using UnityEngine;

namespace Code.Utils
{
    [RequireComponent(typeof(MeshRenderer))]
    public class GridGroundHelper : MonoBehaviour
    {
        void Start()
        {
            var mat = new Material(Shader.Find("Standard"))
            {
                color = new Color(0.3f, 0.3f, 0.3f)
            };
            GetComponent<MeshRenderer>().material = mat;
        }
    }
}