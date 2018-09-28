using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace state_server
{
    [Route("/")]
    [ApiController]
    public class StateController : ControllerBase
    {
        IStateDataService _stateService;
        IStatePointFinderService _statePointFinderService;

        public StateController(IStateDataService stateDataService, IStatePointFinderService stateFinderService)
        {
            _stateService = stateDataService;
            _statePointFinderService = stateFinderService;
        }

        // GET api/values
        [HttpGet()]
        public async Task<IActionResult> GetStates()
        {
            return Ok(_stateService.LoadStatesFromJson());
        }

        //shoutout to http://alienryderflex.com/polygon/ for a great walkthrough of a point-in-polygon example
        //short version, if a ray drawn from the point we're testing breaks an odd amount of edges, it's within the polygon
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> Post([FromBody]InputObject inputObj)
        {
            double inputlatitude;
            double inputlongitude;

            if(!double.TryParse(inputObj.latitude, out inputlatitude) || !double.TryParse(inputObj.longitude, out inputlongitude))
            {
                return BadRequest("bad input params: got lat as " + inputObj.latitude + " and long as " + inputObj.longitude);
            }


            // this is extremely unoptimized, taping together the oddball not-really-json (json objects delinieted by newlines
            // and the array happy version of the algo I'm using aren't super-compatible
            var states = _stateService.LoadStatesFromJson();


            string matchingstate = "";

            foreach (StateDTO s in states)
            {

                List<PointF> stateboarders = new List<PointF>();

                for(int i = 0; i < s.border.Count(); i ++)
                {
                    stateboarders.Add(new PointF { X = s.border[i][0], Y = s.border[i][1]});
                }

                bool stateresult = _statePointFinderService.IsPointInPolygon(stateboarders.ToArray(), new PointF { X = inputlatitude, Y = inputlongitude } );
                
                if(stateresult)
                {
                   matchingstate = s.state;
                }
                else
                {
                    Console.WriteLine(s.state + " does not match");
                }
            }

            return Ok(matchingstate);
        }
    }
}
