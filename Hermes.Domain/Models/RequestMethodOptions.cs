using System.Collections.Generic;

namespace Hermes.Domain.Models;

public class RequestMethodOption
{
    private RequestMethodOption(string value) { Value = value; }

    public string Value { get; }
    
    public static RequestMethodOption Get => new("GET");
    public static RequestMethodOption Post => new("POST");
    public static RequestMethodOption Put => new("PUT");
    public static RequestMethodOption Patch => new("PATCH");
    public static RequestMethodOption Delete => new("DELETE");
    public static RequestMethodOption Options => new("OPTIONS");

    public static List<RequestMethodOption> MethodOptionsList => [Get, Post, Put, Patch, Delete, Options];
}