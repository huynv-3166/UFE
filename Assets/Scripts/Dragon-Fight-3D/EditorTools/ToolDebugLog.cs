#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace TOHDragonFight3D
{
    public class ToolDebugLog : MonoBehaviour
    {
        // String
        public static string Difference<T>(T arg1, T arg2, string varName = null)
        {
            var bindingFlags = BindingFlags.Instance |
                   BindingFlags.NonPublic |
                   BindingFlags.Public;

            var fieldValues1 = arg1.GetType()
                                 .GetFields(bindingFlags)
                                 .Select(field => field.GetValue(arg1))
                                 .ToList();

            var fieldValues2 = arg2.GetType()
                                 .GetFields(bindingFlags)
                                 .Select(field => field.GetValue(arg1))
                                 .ToList();

            if (fieldValues1.Count != fieldValues2.Count) return "DiffCount";
            else
            {
                List<string> diffs = new List<string>();
                foreach (var obj1 in fieldValues1)
                {
                    //var obj2 = fieldValues2.Find((obj) => obj.)
                }
            }

            return string.Empty;
        }

        // Reflection
        [ContextMenu("TestReflection")]
        public void TestReflection()
        {
            ParticleInfo foo = new ParticleInfo();

            var bindingFlags = BindingFlags.Instance |
                   BindingFlags.NonPublic |
                   BindingFlags.Public;

            var fieldValues = foo.GetType()
                                 .GetFields(bindingFlags)
                                 .Select(field => field.GetValue(foo))
                                 .ToList();

            foo.GetType()
                     .GetFields()
                     .Select(field => field.GetValue(foo))
                     .ToList();

            Debug.Log(JsonUtility.ToJson(foo));
        }
    }
}
#endif