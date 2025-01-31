using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Library : MonoBehaviour
{
  
}

public static class ForceVector
{
    public static bool TargetTagResarch(Collider2D collder2d, params string[] tags)
    {
        return tags.Any(tag => collder2d.CompareTag(tag));
    }

    public static bool SetFlag()
    {
        return true;
    }
}
