using System;

public class DiException : Exception
{
    public DiException(): base() { }
    public DiException(string message): base(message) { }
    public DiException(string message, Exception innerException): base(message, innerException) { }
}