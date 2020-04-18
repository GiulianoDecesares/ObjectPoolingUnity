using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReusable
{
    event System.Action<IReusable> OnReleased;
}
