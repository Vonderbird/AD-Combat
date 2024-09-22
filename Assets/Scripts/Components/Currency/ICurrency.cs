using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICurrency
{
    public decimal Value { get; }
    public bool IsEmpty { get; }
}
