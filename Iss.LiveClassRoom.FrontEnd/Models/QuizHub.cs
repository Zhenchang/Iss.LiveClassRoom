using Iss.LiveClassRoom.Core.Services;
using Iss.LiveClassRoom.FrontEnd.App_Start;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Iss.LiveClassRoom.FrontEnd.Models {
    public class QuizHub : Hub {

        private IQuizService _quizService;
        public QuizHub(IQuizService quizService) {
            _quizService = quizService;
        }

        public void Send(string name, string message) {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(Context.User.Identity.GetGivenName(), message);
        }
    }

    public class ChatHub : Hub {
        private ICommentService _service;
        private IUserService _userService;
        private IFeedService _feedService;

        public ChatHub(ICommentService service, IUserService userService, IFeedService feedService) {
            _service = service;
            _userService = userService;
            _feedService = feedService;
        }

        public async Task Send(string name, string message) {
            var feed = await _feedService.GetById(name);
            var user = await _userService.GetById(Context.User.Identity.Name);
            await _service.Add(new Core.Models.Comment()
            {
                Feed = feed,
                Text = message,
                User = user
            }, Context.User.Identity.Name);
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(Context.User.Identity.GetGivenName(), message, DateTime.UtcNow.ToString());
        }
    }

}