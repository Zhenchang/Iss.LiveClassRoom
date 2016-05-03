using Iss.LiveClassRoom.Core.Services;
using Iss.LiveClassRoom.FrontEnd.App_Start;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Iss.LiveClassRoom.FrontEnd.Models {
    public class QuizHub : Hub {

        private IQuizService _quizService;
        public QuizHub(IQuizService quizService) {
            _quizService = quizService;
        }

        public void Send(string name, string message) {
            if (Context.User.Identity.Name != name) return;
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(Context.User.Identity.GetGivenName(), message);
        }
    }

    public class ChatHub : Hub {
        private ICommentService _service;
        private IUserService _userService;

        public ChatHub(ICommentService service, IUserService userService) {
            _service = service;
            _userService = userService;
        }

        public void Send(string name, string message) {
            if (Context.User.Identity.Name != name) return;

            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(Context.User.Identity.GetGivenName(), message);
        }
    }

}