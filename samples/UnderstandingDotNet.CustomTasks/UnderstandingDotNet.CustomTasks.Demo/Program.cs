using System;
using System.Threading.Tasks;
using UnderstandingDotNet.CustomTasks.Domain;

namespace UnderstandingDotNet.CustomTasks.Demo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var task = GetName();
            var name = await task;

            Console.WriteLine($"Hello, {name}");

            async TaskTrace<string> GetName()
            {
                await Task.Delay(100);

                return "Jon Doe";
            }
        }
    }
}