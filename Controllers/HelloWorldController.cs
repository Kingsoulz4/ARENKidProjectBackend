using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjectBackend.Models;


namespace ProjectBackend.Controllers;

public class HelloWorldController :Controller
{
    // 
    // GET: /HelloWorld/
    public IActionResult Index()
    { 
        return View();
    }

    // 
    // GET: /HelloWorld/Welcome
    public IActionResult Welcome(string message, int repeatTimes)
    {
        ViewData["message"] = "Hello DH";
        ViewData["repeatTimes"] = repeatTimes;
        return View();
    }

    // 
    // GET: /HelloWorld/MethodPassParamSample/
    public string MethodPassParamSample(string name = "something", int code = 1)
    {
        
        return $"My name is {name} id = {code}";
    }

    
}

