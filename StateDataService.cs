using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace state_server
{
    //By convention, this should be in another file, trying to keep down the complexity
    //for not-dotnet developers
    public interface IStateDataService
    {
        List<StateDTO> LoadStatesFromJson();
    }

    public class StateDataService : IStateDataService
    {
        public List<StateDTO> LoadStatesFromJson()
        {
            List<StateDTO> states = new List<StateDTO>();

            using (StreamReader r = new StreamReader("data/states.json"))
            {
                //I'm sure there's a nicer JsonLD way to do this, but streamreader only needs to run once on startup
                //doesn't seem like a great place to optimize on a first pass.
                while (r.Peek() >= 0)
                {
                    states.Add(JsonConvert.DeserializeObject<StateDTO>(r.ReadLine()));
                }
                Console.WriteLine(states.Count() + " states loaded");
            }

            return states;
        }
    }
}