using UnityEditor;
using TOHDragonFight3D;

public class HitBoxEditorAsset
{
    [MenuItem("Assets/Create/U.F.E./Custom Hit Boxes")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<CustomHitBoxesInfo>();
    }
}