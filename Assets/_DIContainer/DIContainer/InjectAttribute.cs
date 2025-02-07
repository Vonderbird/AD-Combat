using System;

// Attribute for marking dependencies
[AttributeUsage(AttributeTargets.Field)]
public class InjectAttribute : Attribute { }