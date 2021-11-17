using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Word2VecWebinar.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public WordModel WordModel { get; set; }
        public void OnGet()
        {
            WordModel = new();
            WordModel.Positive1 = "King";
            WordModel.Negative1 = "Man";
            WordModel.Positive2 = "Woman";
        }

        public void OnPost()
        {
            string url = $"https://smartword2vec.azurewebsites.net/api/MostSimilar?positive1={WordModel.Positive1}&positive2={WordModel.Positive2}&negative={WordModel.Negative1}".ToLower();
            WebRequest request = WebRequest.Create(url);
            var stream = request.GetResponse().GetResponseStream();
            StreamReader objReader = new StreamReader(stream);
            var response = objReader.ReadToEnd();

            WordModel.Result = response;
        }
    }
}
